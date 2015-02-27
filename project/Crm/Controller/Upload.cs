/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:04:47
 */
using System;
using System.Text;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class UploadController : Jabinfo.JabinfoController
	{
		#region Constructor
		public UploadController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		Weixin wx=new Weixin();

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			Weixin weixin = new Weixin ();
			context.Variable ["total"] = UploadMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["access_token"] = weixin.GetAccessToken ();
			context.Variable ["uploadList"] = UploadMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			Weixin weixin = new Weixin ();
			Utils util = new Utils ();
			if (!context.IsPost) {
				return;
			}
			context.Post ["uploadId"] = Jabinfo.Help.Basic.JabId;
			string type = context.Post ["type"];
			string fileName = context.Files ["afile"].FileName;
			string model=fileName.Substring(fileName.LastIndexOf('.') + 1);
			Jabinfo.Help.Upload.Save (context.Post ["uploadId"], context.Files["afile"],model);
			string file = Jabinfo.Help.Upload.PysPath (context.Post ["uploadId"],model);
			string url = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token="+weixin.GetAccessToken();
			url = url + "&type=" + type;
			string json = util.HttpUpload (url, file);
			context.Post ["mediaId"] = weixin.GetJsonValue (json,"media_id");
			context.Post ["createdAt"] = weixin.GetJsonValue(json,"created_at");
			if (!string.IsNullOrEmpty (weixin.GetJsonValue (json, "errcode"))) {
				context.Jump ("/article/upload/home","无效媒体类型,请重新上传");
				return;
			}
			UploadMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String uploadId)
		{
			if (!context.IsPost) {
				context.Variable ["upload"] = UploadMapper.I.Create (uploadId);
				return;
			}
			UploadMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		/// <summary>
		/// 图文消息推送，以分组形式进行推送 
		/// </summary>
		public void send(JabinfoContext context,string mediaId)
		{
			if (!context.IsPost) {
				context.Variable ["mediaId"] = mediaId;
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("8");
				return;
			}
			string group = context.Post ["group"];
			StringBuilder str = new StringBuilder ("{ \r\n"); 
			str.Append("\"filter\":{ \r\n");
			if (string.IsNullOrEmpty (group))
				str.Append ("\"is_to_all\":true \r\n");
			else
				str.Append("\"is_to_all\":false \r\n");
			str.Append ("}, \r\n");
			str.Append (" \"mpnews\":{ \r\n");
			str.Append (" \"media_id\":\"" + context.Post ["mediaId"] + "\" \r\n");
			str.Append (" }, \r\n");
			str.Append ("  \"msgtype\":\"mpnews\" \r\n");
			str.Append ("}");

			string result=wx.Sendgroup(str.ToString ());
			if (result == "0") {
				context.Alert ("推送成功");
				return;
			} else {
				context.Alert ("推送失败");
				return;
			}
		}

		/// <summary>
		/// 文本推送
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="mediaId">Media identifier.</param>
		public void sendtext(JabinfoContext context,string mediaId)
		{
			if (!context.IsPost) {
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("8");;
				return;
			}
			string group = context.Post ["group"];
			StringBuilder str=new StringBuilder("{ \r\n");
			str.Append("\"filter\":{ \r\n");
			if (string.IsNullOrEmpty (group))
				str.Append ("\"is_to_all\":true \r\n");
			else
				str.Append("\"is_to_all\":false \r\n");
			str.Append ("}, \r\n");
			str.Append (" \"text\":{ \r\n");
			str.Append ("\"content\":\""+context.Post["context"]+"\" \r\n");
			str.Append (" }, \r\n");
			str.Append ("  \"msgtype\":\"text \" \r\n");
			str.Append ("}");
			string result = wx.Sendgroup (str.ToString ());
			if (result == "0") {
				context.Alert ("推送成功");
				return;
			} else {
				context.Alert ("推送失败");
				return;
			}
		}

		/// <summary>
		/// 推送语音消息
		/// </summary>
		/// <param name="context">上下文</param>
		/// <param name="mediaId">媒体标示.</param>
		public void sendvoice(JabinfoContext context,string mediaId)
		{
			if (!context.IsPost) {
				context.Variable["mediaId"]=mediaId;
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("8");
				return;
			}
			StringBuilder str=new StringBuilder("{ \r\n");
			string group = context.Post ["group"];
			StringBuilder con = new StringBuilder ();
			str.Append("\"filter\":{ \r\n");
			if (string.IsNullOrEmpty (group))
				str.Append ("\"is_to_all\":true \r\n");
			else
				str.Append("\"is_to_all\":false \r\n");
			str.Append ("}, \r\n");
			str.Append ("  \"voice\":{ \r\n");
			str.Append ("\"media_id\":\""+context.Post["mediaId"]+"\" \r\n");
			str.Append (" }, \r\n");
			str.Append (" \"msgtype\":\"voice\" \r\n");
			str.Append ("}");
			string result = wx.Sendgroup (str.ToString ());
			if (result == "0") {
				context.Alert ("推送成功");
				return;
			} else {
				context.Alert ("推送失败");
				return;
			}
		}

		/// <summary>
		/// 推送图片消息
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="mediaId">Media identifier.</param>
		public void sendimage(JabinfoContext context,string mediaId)
		{
			if (!context.IsPost) {
				context.Variable["mediaId"]=mediaId;
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("8");
				return;
			}
			StringBuilder str=new StringBuilder("{ \r\n");
			StringBuilder con = new StringBuilder ();
			string group=context.Post["group"];
			str.Append("\"filter\":{ \r\n");
			if (string.IsNullOrEmpty (group))
				str.Append ("\"is_to_all\":true \r\n");
			else
				str.Append("\"is_to_all\":false \r\n");
			str.Append ("}, \r\n");
			str.Append ("\"image\":{ \r\n");
			str.Append ("\"media_id\":\""+context.Post["mediaId"]+"\" \r\n");
			str.Append (" },");
			str.Append ("\"msgtype\":\"image\" \r\n");
			str.Append ("}");
			string result =wx.Sendgroup(str.ToString ());
			if (result == "0") {
				context.Alert ("推送成功");
				return;
			} else {
				context.Alert ("推送失败");
				return;
			}
		}

		/// <summary>
		/// 推送视频文件
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="mediaId">Media identifier.</param>
		public void sendvedio(JabinfoContext context,string mediaId)
		{
			if (!context.IsPost) {
				context.Variable["mediaId"]=mediaId;
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("8");
				return;
			}
			StringBuilder str=new StringBuilder("{ \r\n");
			StringBuilder con = new StringBuilder ();
			string group=context.Post["group"];
			str.Append("\"filter\":{ \r\n");
			if (string.IsNullOrEmpty (group))
				str.Append ("\"is_to_all\":true \r\n");
			else
				str.Append("\"is_to_all\":false \r\n");
			str.Append ("}, \r\n");
			str.Append (" \"mpvideo\":{ \r\n");
			str.Append (" \"media_id\":\""+context.Post["mediaId"]+"\", \r\n");
			str.Append (" }, \r\n");
			str.Append ("\"msgtype\":\"mpvideo\" \r\n");
			str.Append ("} \r\n");
			string result = wx.Sendgroup (str.ToString ());
			if (result == "0") {
				context.Alert ("推送成功");
				return;
			} else {
				context.Alert ("推送失败");
				return;
			}
		}

		#region remove
		public void remove (JabinfoContext context, String uploadId)
		{
			UploadMapper.I.DeleteByPrimary (uploadId);
			context.Refresh ();
		}
		#endregion
	}
}