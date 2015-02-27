using System;
using Jabinfo.Crm.VO;
using System.Collections.Generic;


namespace Jabinfo
{
	public class Msg
	{
		/// <summary>
		/// 回复文字消息
		/// </summary>
		/// <param name="requestXML">接收到的请求信息</param>
		/// <param name="txt">消息内容</param>
		/// <returns></returns>
		public string SendMsg(RequestVO requestXML, string txt)
		{
			string strResult = "";
			strResult = "<xml>"
				+ "<ToUserName><![CDATA[" + requestXML.fromUserName + "]]></ToUserName>"
				+ "<FromUserName><![CDATA[" + requestXML.toUserName + "]]></FromUserName>"
				+ "<CreateTime>" + Jabinfo.Help.Date.Now + "</CreateTime>"
				+ "<MsgType><![CDATA[text]]></MsgType>"
				+ "<Content><![CDATA[" + txt + "]]></Content>"
				+ "</xml>";
			return strResult;
		}

		/// <summary>
		/// 回复图文信息
		/// </summary>
		/// <param name="requestXML">接收到的请求信息</param>
		/// <param name="articleList">图文列表</param>
		/// <returns></returns>
		public string SendMsg(RequestVO requestXML, IList<WXArticle> articleList)
		{
			string strResult = "";
			var strItems = "";
			foreach (var item in articleList)
			{
				strItems += "<item>"
					+ "<Title><![CDATA[" + item.Title + "]]></Title>"
					+ "<Description><![CDATA[" + item.Description + "]]></Description>"
					+ "<PicUrl><![CDATA[" + item.PicUrl + "]]></PicUrl>"
					+ "<Url><![CDATA[" + item.Url + "]]></Url>"
					+ "</item>";
			}
			strResult = "<xml>"
				+ "<ToUserName><![CDATA[" + requestXML.fromUserName + "]]></ToUserName>"
				+ "<FromUserName><![CDATA[" + requestXML.toUserName + "]]></FromUserName>"
				+ "<CreateTime>" + Jabinfo.Help.Date.Now + "</CreateTime>"
				+ "<MsgType><![CDATA[news]]></MsgType>"
				+ "<ArticleCount>" + articleList.Count + "</ArticleCount>"
				+ "<Articles>"
				+ strItems
				+ "</Articles>"
				+ "</xml>";
			return strResult;
		}

		/// <summary>
		/// 发送图片消息
		/// </summary>
		/// <returns>The pic.</returns>
		/// <param name="requestXML">Request XM.</param>
		/// <param name="pic">Pic.</param>
		public string SendPic(RequestVO requestXML)
		{
			string strResult = "";
			strResult = " <xml>"
				+ "<ToUserName><![CDATA["+requestXML.fromUserName+"]]></ToUserName>"
				+ "<FromUserName><![CDATA["+requestXML.toUserName+"]]></FromUserName>"
				+ "<CreateTime>"+Jabinfo.Help.Date.Now+"</CreateTime> "
				+ "<MsgType><![CDATA[image]]></MsgType>"
				+ "<PicUrl><![CDATA["+requestXML.PicUrl+"]]></PicUrl>"
				+ "<MediaId><![CDATA["+requestXML.MediaId+"]]></MediaId>"
				+ "<MsgId>"+requestXML.msgId+"</MsgId>"
				+ "</xml>";
			return strResult;
		}

		/// <summary>
		/// 回复音乐消息
		/// </summary>
		/// <param name="requestXML"></param>
		/// <param name="music"></param>
		/// <returns></returns>
		public string SendMusic(RequestVO requestXML, WXMusic music)
		{
			string strResult = "";
			strResult = "<xml>"
				+ "<ToUserName><![CDATA[" + requestXML.fromUserName + "]]></ToUserName>"
				+ "<FromUserName><![CDATA[" + requestXML.toUserName + "]]></FromUserName>"
				+ "<CreateTime>" + Jabinfo.Help.Date.Now + "</CreateTime>"
				+ "<MsgType><![CDATA[music]]></MsgType>"
				+ "<Music>"
				+ "<Title><![CDATA[" + music.Title + "]]></Title>"
				+ "<Description><![CDATA[" + music.Description + "]]></Description>"
				+ "<MusicUrl><![CDATA[" + music.MusicUrl + "]]></MusicUrl>"
				+ "<HQMusicUrl><![CDATA[" + music.HQMusicUrl + "]]></HQMusicUrl>"
				+ "</Music>"
				+ "</xml>";
			return strResult;
		}

		/// <summary>
		/// 发送视频消息
		/// </summary>
		/// <returns>The vedio.</returns>
		/// <param name="requestXml">Request xml.</param>
		/// <param name="vedio">Vedio.</param>
		public string SendVedio(RequestVO requestXml)
		{
			string strResult = string.Empty;
			strResult="<xml>" 
				+ "<ToUserName><![CDATA["+requestXml.fromUserName+"]]></ToUserName>"
				+ "<FromUserName><![CDATA["+requestXml.toUserName+"]]></FromUserName>"
				+ "<CreateTime>"+Jabinfo.Help.Date.Now+"</CreateTime>"
				+ "<MsgType><![CDATA[video]]></MsgType>"
				+ "<Video>"
				+ "<MediaId><![CDATA["+requestXml.MediaId+"]]></MediaId>"
				+ "<Title><![CDATA["+requestXml.Title+"]]></Title>"
				+ "<Description><![CDATA["+requestXml.Description+"]]></Description>"
				+ "</Video> "
				+ "<xml>";
			return strResult;
		}

		/// <summary>
		/// 回复语音消息
		/// </summary>
		/// <returns>The voice.</returns>
		/// <param name="requestXML">Request XM.</param>
		public string SendVoice(RequestVO requestXML)
		{
			string strResult = string.Empty;
			strResult="<xml>"
				+ "<ToUserName><![CDATA["+requestXML.fromUserName+"]]></ToUserName>"
				+ "<FromUserName><![CDATA["+requestXML.toUserName+"]]></FromUserName>"
				+ "<CreateTime>"+Jabinfo.Help.Date.Now+"</CreateTime>"
				+ "<MsgType><![CDATA[voice]]></MsgType>"
				+ "<Voice>"
				+ "<MediaId><![CDATA["+requestXML.MediaId+"]]></MediaId>"
				+ "</Voice>"
				+ "</xml>";
			return strResult;
		}

	}
}

