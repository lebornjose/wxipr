/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/13 10:15:27
 */
using System;
using System.Text;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class RequestController : Jabinfo.JabinfoController
	{
		#region Constructor
		public RequestController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = RequestMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Functions.Add ("info", this.info);
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("article.post");
			context.Variable ["requestList"] = RequestMapper.I.SelectByPage (index, size);
		}
		#endregion

		public string info(object[] args)
		{
			Weixin weixin = new Weixin ();
			string openId = Convert.ToString (args [0]);
			string json= weixin.GetInfo (weixin.GetAccessToken(), openId);
			return weixin.GetJsonValue (json, "nickname");
		}
	
		public void msg(JabinfoContext context,string requestId)
		{
			if (!context.IsPost) {
				context.Variable ["requestId"] = requestId;
				return;
			}
			Weixin wx = new Weixin ();
			StringBuilder str=new StringBuilder("{ \r\n");
			StringBuilder con = new StringBuilder ();
			string msg = context.Post ["context"];
			str.Append(" \"touser\":[ \r\n");
			con.Append (string.Format ("\"{0}\",\r\n", context.Post["openId"]));
			string main = con.ToString ();
			string text = main.Substring (0, main.Length - 3);
			str.Append (text);
			str.Append(" ], \r\n");
			str.Append ("\"msgtype\": \"text\", \r\n");
			//您好，宾弗科技微信公众号推出两款新功能，关键字搜索文章查看，资讯推送，感谢你的关注!
			str.Append (" \"text\": { \"content\": \""+msg+"\"}");
			str.Append("} \r\n");
			string result = wx.Sendmsg (str.ToString ());
			context.Post ["status"] = "1";
			RequestMapper.I.UpdateByPrimary (context.Post);
			context.Alert ("回复成功");
		}

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["requestId"] = Jabinfo.Help.Basic.JabId;
			RequestMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String requestId)
		{
			if (!context.IsPost) {
				context.Variable ["request"] = RequestMapper.I.Create (requestId);
				return;
			}
			RequestMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String requestId)
		{
			RequestMapper.I.DeleteByPrimary (requestId);
			context.Refresh ();
		}
		#endregion
	}
}