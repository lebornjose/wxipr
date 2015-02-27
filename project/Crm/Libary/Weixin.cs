using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Jabinfo.Crm.VO;
using Jabinfo.Crm.Model;
using System.Web.Script.Serialization;

namespace Jabinfo 
{
	public class Weixin:JabinfoController
	{
		private string devlopID = "wx6522a9d862ae7a0f";//微信里面开发者模式：开发者ID
		private string devlogPsw = "2742914b63c29a7084d81025790d506f";//微信里面开发者模式： 开发者密码
		public string AccessToken =string.Empty; //获取的通行证

		public string GetAccessToken() //获取通行证
		{    
			TokenVO token=TokenMapper.I.Create("AccessToken");
			int currentdate=Jabinfo.Help.Date.Now;
			if (currentdate - token.date >= 7000) {    // 7000 防止 误差的产生，改为7000
				string url_token = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + devlopID + "&secret=" + devlogPsw;
				string jsonStr = Jabinfo.Help.Http.GetHttps (url_token);
				AccessToken = GetJsonValue (jsonStr, "access_token");
				JabinfoKeyValue data = new JabinfoKeyValue ();
				data["tokenId"]="AccessToken";
				data ["value"] = AccessToken;
				data ["date"] = Jabinfo.Help.Date.Now.ToString();
				TokenMapper.I.UpdateByPrimary (data);
			} else {
				AccessToken = token.value;
			}
			return AccessToken;
		}

		/// <summary>
		/// 获取Json字符串某节点的值
		/// </summary>
		public string GetJsonValue(string jsonStr, string key)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(jsonStr))
			{
				key = "\"" + key.Trim('"') + "\"";
				int index = jsonStr.IndexOf(key) + key.Length + 1;
				if (index > key.Length + 1)
				{
					//先截逗号，若是最后一个，截“｝”号，取最小值
					int end = jsonStr.IndexOf(',', index);
					if (end == -1)
					{
						end = jsonStr.IndexOf('}', index);
					}

					result = jsonStr.Substring(index, end - index);
					result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
				}
			}
			return result;
		}


		/// <summary>
		/// 获取客户发送来的请求信息
		/// </summary>
		/// <param name="postStr"></param>
		/// <returns></returns>
		public RequestVO GetRequest(string postStr)
		{
			//封装请求类
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(postStr);
			XmlElement rootElement = doc.DocumentElement;
			XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
			RequestVO requestXML = new RequestVO();
			requestXML.toUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
			requestXML.fromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
			requestXML.createTime = Convert.ToInt32(rootElement.SelectSingleNode("CreateTime").InnerText);
			requestXML.msgType = MsgType.InnerText;
			if (requestXML.msgType == "text") {
				requestXML.content = rootElement.SelectSingleNode ("Content").InnerText;
				requestXML.msgId = rootElement.SelectSingleNode ("MsgId").InnerText;
			} else if (requestXML.msgType == "location") {
				requestXML.Location_X = rootElement.SelectSingleNode ("Location_X").InnerText;
				requestXML.Location_Y = rootElement.SelectSingleNode ("Location_Y").InnerText;
				requestXML.Scale = rootElement.SelectSingleNode ("Scale").InnerText;
				requestXML.Label = rootElement.SelectSingleNode ("Label").InnerText;
				requestXML.msgId = rootElement.SelectSingleNode ("MsgId").InnerText;
			} else if (requestXML.msgType == "image") {
				requestXML.PicUrl = rootElement.SelectSingleNode ("PicUrl").InnerText;
				requestXML.msgId = rootElement.SelectSingleNode ("MsgId").InnerText;
			} else if (requestXML.msgType == "vedio") {
				requestXML.MediaId = rootElement.SelectSingleNode ("MediaId").InnerText;
				requestXML.Title = rootElement.SelectSingleNode ("Title").InnerText;
				requestXML.Description = rootElement.SelectSingleNode ("Description").InnerText;
			} else if (requestXML.msgType == "voice") {
				requestXML.MediaId = rootElement.SelectSingleNode ("MediaId").InnerText;
			} else if (requestXML.msgType == "link")
			{
				requestXML.Title = rootElement.SelectSingleNode("Title").InnerText;
				requestXML.Description = rootElement.SelectSingleNode("Description").InnerText;
				requestXML.Url = rootElement.SelectSingleNode("Url").InnerText;
				requestXML.msgId = rootElement.SelectSingleNode("MsgId").InnerText;
			}
			else if (requestXML.msgType == "event")
			{
				requestXML.createTime = Convert.ToInt32(rootElement.SelectSingleNode ("CreateTime").InnerText);
				requestXML.Event = rootElement.SelectSingleNode("Event").InnerText;
				requestXML.EventKey = rootElement.SelectSingleNode ("EventKey").InnerText;
			}
			return requestXML;
		}
		/// <summary>
		/// 分局openid 推送消息
		/// </summary>
		/// <param name="users">Users.</param>
		public string Sendmsg(string users)
		{
			string url = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token=" + GetAccessToken ();
			string json = Jabinfo.Help.Http.PostHttps (url, users);
			string errorcode = GetJsonValue (json, "errcode");
			return errorcode;
		}

		/// <summary>
		/// 分组推送消息
		/// </summary>
		/// <param name="json">Json.</param>
		public string Sendgroup(string str)
		{
			string url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + GetAccessToken ();
			string json=Jabinfo.Help.Http.PostHttps(url,str);
			string errorcode = GetJsonValue (json, "errcode");
			JabinfoKeyValue data = new JabinfoKeyValue ();
			data["msgId"]= GetJsonValue (json, "msg_id");
			data ["errmsg"] = GetJsonValue (json, "errmsg");
			data ["errcode"] = GetJsonValue (json, "errcode");
			MsglogMapper.I.Insert (data);
			return errorcode;
		}

		/// <summary>
		/// 生成QR二维码
		/// </summary>
		/// <returns>The code ticket.</returns>
		/// <param name="isTemp">If set to <c>true</c> is temp.</param>
		/// <param name="scene_id">场景值Id</param>
		public string QRCodeTicket(bool isTemp, int scene_id)
		{
			string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + GetAccessToken ();
			string data;
			if (isTemp)
			{
				data = "{\"expire_seconds\": 1800, \"action_name\": \"QR_SCENE\", \"action_info\": {\"scene\": {\"scene_id\":" + scene_id + "}}}";
			}
			else
			{
				data = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + scene_id + "}}}";
			}

			string json = Jabinfo.Help.Http.Post(url, data);
			if (json.IndexOf("ticket") > 0)
			{
				return json;
			}
			else
			{
				return "0";
			}
		}

		public string Event(RequestVO requesXML,string key)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string message = string.Empty;
			string strResult = string.Empty;
			Msg msg = new Msg ();
			switch (key) 
			{
			case "subscribe":
				message = "感谢您关注【知识产权那点事】微信公众号，我们会定期为你推送我们最新的新闻资讯";
				strResult = msg.SendMsg (requesXML, message);
				// 将关注信息写入follow 表中
				string json = GetInfo (GetAccessToken (), requesXML.fromUserName);
				FollowVO follow = serializer.Deserialize<FollowVO> (json);
				JabinfoKeyValue data = new JabinfoKeyValue ();
				data ["openid"] = follow.openId;
				data ["nickname"] = follow.nickname;
				data ["sex"] = follow.sex;
				data ["province"] = follow.province;
				data ["city"] = follow.city;
				data ["country"] = follow.country;
				data ["headimgurl"] = follow.headimgurl;
				data ["createtime"] = requesXML.createTime.ToString();
				FollowMapper.I.Insert (data);
				break;
			case "unsubscribe":
				message = "取消关注";
				strResult = msg.SendMsg (requesXML, message);
				// 将关注者从数据库中取消
				FollowMapper.I.DeleteByPrimary (requesXML.fromUserName);
				break;
			case "CLICK":
				if (requesXML.EventKey == "inn") {
					IList<WXArticle> articles = new List<WXArticle> ();
					WXArticle article = new WXArticle ();
					article.Description = "上海知识产权研究所，提供最具价值的知识产权资讯";
					article.PicUrl = "http://www.iprlawyers.com/Files/2014.5.22fugang.jpg";
					article.Title = "知识产权那点事";
					article.Url = "http://www.iprlawyers.com/";
					articles.Add (article);
					strResult = msg.SendMsg (requesXML, articles);
				}
				break;
//			case "SCAN":
//				IList<WXArticle> articles1 = new List<WXArticle> ();
//				WXArticle item = new WXArticle ();
//				item.Title = "Kooteam";
//				item.Description = "KooTeam是一款轻量级的团队时间管理与项目管理的系统，免费提供项目管理，团队协作SaaS解决方案。不需要安装或升级任何软件，您只要注册一个KooTeam账户就马上可以拥有一个多功能、一体化的在线项目管理，团队协作办公系统——无需购买服务器，使用次数不限、使用时长不限、用户数不限、项目数量不限";
//				item.PicUrl = "http://p.kooteam.com/res/tma/banner/01.jpg";
//				item.Url = "http://p.kooteam.com";
//				articles1.Add (item);
//				strResult = msg.SendMsg (requesXML, articles1);
//				break;
			default:
				message = "receive a new event:" + requesXML.EventKey;
				strResult = msg.SendMsg (requesXML, message);
				break;
			}
			return strResult;
		}

		/// <summary>
		/// 获取用户基本信息
		/// </summary>
		/// <returns>The info.</returns>
		/// <param name="">.</param>
		/// <param name="open_id">Open identifier.</param>
		public string GetInfo(string AccessToken, string open_id)
		{
			string url = string.Format ("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", AccessToken, open_id);
			string json = Jabinfo.Help.Http.GetHttps (url);
			return json;
		}

		public string SetMenu(JabinfoContext context) //设置最新菜单
		{
			string url_Menu_Create = string.Format ("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", GetAccessToken ());
			string postData = MenuModel.I.createMenuDate ();
			string result = Jabinfo.Help.Http.PostHttps(url_Menu_Create,postData);
			context.Print (postData);
			return result;
		}

		/// <summary>
		/// 上传图文
		/// </summary>
		/// <returns>The news.</returns>
		/// <param name="json">Json.</param>
		public void UpNews(string articles,string title)
		{
			JabinfoKeyValue data = new JabinfoKeyValue ();
			string url = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=" + GetAccessToken ();
			string json = Jabinfo.Help.Http.PostHttps (url, articles);
			data ["uploadId"] = Jabinfo.Help.Basic.JabId;
			data ["title"] = title;
			data["type"]="news";
			data ["mediaId"] = GetJsonValue (json, "media_id");
			data ["createdAt"] =GetJsonValue (json, "created_at");
			UploadMapper.I.Insert (data);
		}

		/// <summary>
		/// 发送消息日志
		/// </summary>
		/// <param name="postString">Post string.</param>
		public void loger(RequestVO request)
		{
			JabinfoKeyValue data = new JabinfoKeyValue ();
			data ["requestId"] = Jabinfo.Help.Basic.JabId;
			data ["toUserName"] = request.toUserName;
			data["fromUserName"]=request.fromUserName;
			data ["createTime"] = request.createTime.ToString();
			data ["msgType"] = request.msgType;
			data ["content"] = request.content;
			data ["msgId"] = request.msgId;
			RequestMapper.I.Insert (data);
		}

	}
}

