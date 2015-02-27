/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 21:03:15
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class MsglogController : Jabinfo.JabinfoController
	{
		#region Constructor
		public MsglogController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = MsglogMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["msglogList"] = MsglogMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["msgId"] = Jabinfo.Help.Basic.JabId;
			MsglogMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String msgId)
		{
			if (!context.IsPost) {
				context.Variable ["msglog"] = MsglogMapper.I.Create (msgId);
				return;
			}
			MsglogMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String msgId)
		{
			Weixin wx = new Weixin ();
			string url = "https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token=" + wx.GetAccessToken ();
			string con = "{\n   \"msg_id\":" + msgId + "\n}";
			string json = Jabinfo.Help.Http.PostHttps (url, con);
			if (wx.GetJsonValue (json, "errcode") == "0") {
				MsglogMapper.I.DeleteByPrimary (msgId);
				context.Alert ("删除成功");
			}
		}
		#endregion
	}
}