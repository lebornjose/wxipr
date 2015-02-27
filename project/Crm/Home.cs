using System;
using Jabinfo;
using System.IO;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Web.Security;
using System.Configuration;
using System.Collections.Generic;
using Jabinfo.Crm.VO;
using Jabinfo.Crm.Model;

namespace Jabinfo
{
	public class Home:JabinfoController
	{
		public Home ()
		{
		}
		private string Token = "yunzhi"; //微信里面开发者模式Token


		public void index(JabinfoContext context)
		{
			Weixin weixin = new Weixin ();
			if (context.Request.HttpMethod.ToUpper()=="POST")
			{
				string strReset =Excute (context);
				context.Print (strReset);
			}
			else
			{
				Auth(context) ; //微信接入的测试
				weixin.SetMenu (context);
			}
		}

		/// <summary>
		/// 成为开发者的第一步，验证并相应服务器的数据
		/// </summary>
		public void Auth(JabinfoContext context)
		{
			string echoStr =context.Request.QueryString["echoStr"];
			if (CheckSignature(context))
			{
				if (!string.IsNullOrEmpty(echoStr))
				{
					context.Print(echoStr);
					context.End ();
				}
			}
		}

		/// <summary>
		/// 根据文件id回去图片地址
		/// </summary>
		/// <param name="articleId">Article identifier.</param>
		public string Attch(string articleId)
		{
			string one = articleId.Substring (0, 4);
			string two = articleId.Substring (4, 2);
			string three = articleId.Substring (6);
			string result = one +"/"+ two+"/" + three+".jpg";
			return result;
		}

		/// <summary>
		/// 验证微信签名
		/// </summary>
		public bool CheckSignature(JabinfoContext context)
		{
			string signature =context.Request.QueryString["signature"];
			string timestamp =context.Request.QueryString["timestamp"];
			string nonce = context.Request.QueryString["nonce"];
			string[] ArrTmp = { Token, timestamp, nonce };
			Array.Sort(ArrTmp);     //字典排序
			string tmpStr = string.Join("", ArrTmp);
			tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
			tmpStr = tmpStr.ToLower();
			if (tmpStr == signature)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public string Excute(JabinfoContext context)
		{
			Weixin weixin = new Weixin ();
			string strResult = string.Empty;
			string message = string.Empty;
			Msg msg = new Msg ();
			Stream s = context.Request.InputStream;
			byte[] b = new byte[s.Length];
			s.Read(b, 0, (int)s.Length);
			string postString = Encoding.UTF8.GetString(b);
			RequestVO requestXML = weixin.GetRequest (postString);
			string type = requestXML.msgType;
			switch (type)
			{
			case "text":
				if (requestXML.content == "音乐") {
					WXMusic music = new WXMusic ();
					music.Title = "喜欢你";
					music.Description = "邓紫琪";
					music.MusicUrl = "http://qzone.djsoso.com/mp3/7D67455E23D943ED09AA4772B0CC2B5A-54738/www.haoduoge.com.mp3";
					music.HQMusicUrl = "http://qzone.djsoso.com/mp3/7D67455E23D943ED09AA4772B0CC2B5A-54738/www.haoduoge.com.mp3";
					strResult = msg.SendMusic (requestXML, music);
				} else if(requestXML.content=="koo"||requestXML.content=="kooteam") {
					IList<WXArticle> articles = new List<WXArticle> ();
					WXArticle item = new WXArticle ();
					item.Title = "Kooteam";
					item.Description = "办公也扮酷";
					item.PicUrl = "http://p.kooteam.com/res/tma/banner/01.jpg";
					item.Url = "http://p.kooteam.com";
					articles.Add (item);
					strResult = msg.SendMsg (requestXML, articles);
				}else{
					string keywold = requestXML.content;
					ArticleVO[] articleVO =ArticleModel.I.Keywold(keywold, 0, 4);
					if (articleVO.Length == 0) {
						string joke = Jabinfo.Help.Http.Get ("http://apix.sinaapp.com/joke/?appkey=trialuser");
						string jo = joke.Substring (0, joke.Length-15);
						strResult = msg.SendMsg (requestXML, jo);
					} else {
						IList<WXArticle> articles = new List<WXArticle> ();
						foreach (ArticleVO a in articleVO) {
							WXArticle item = new WXArticle ();
							item.Title = a.title;
							item.Description = a.summary;
							item.PicUrl = "http://wx.zento.me/upload/" + Attch (a.articleId);
							item.Url = "http://wx.zento.me/article/home/detail/" + a.articleId;
							articles.Add (item);
						}
						strResult = msg.SendMsg (requestXML, articles);
					}
				}
				weixin.loger (requestXML);
				break;
			case "location":
				message = string.Format ("你发送的是位置，纬度为：{0}；经度为：{1}；缩放级别为：{2}；位置为：{3};", requestXML.Location_X, requestXML.Location_Y, requestXML.Scale, requestXML.Label);
				strResult = msg.SendMsg (requestXML, message);
				break;
			case "image":
				strResult = msg.SendPic (requestXML);
				break;
			case "vedio":
				strResult = msg.SendVedio (requestXML);
				break;
			case "voice":
				strResult = msg.SendVoice (requestXML);
				break;
			case "link":
				message = string.Format ("你发送的是链接，标题为：{0}；内容为：{1}；链接地址为：{2}", requestXML.Title, requestXML.content, requestXML.Url);
				strResult = msg.SendMsg (requestXML, message);
				break;
			case "event":
				string key = requestXML.Event;
				strResult= weixin.Event (requestXML, key);
				break;
			}
			return strResult;
		}

	}
}


