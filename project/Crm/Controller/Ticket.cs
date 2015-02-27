/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:45:34
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;
using System.Web.Script.Serialization;

namespace Jabinfo.Crm
{
	class TicketController : Jabinfo.JabinfoController
	{
		#region Constructor
		public TicketController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = TicketMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["ticketList"] = TicketMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			bool istemp = true;
			Weixin wx = new Weixin ();
			JavaScriptSerializer serializer = new JavaScriptSerializer ();
			if (!context.IsPost) {
				return;
			}
			int sceneId = Convert.ToInt32(context.Post ["sceneId"]);
			string action = context.Post ["action"];
			if (action == "false")
				istemp = false;
			string resutl= wx.QRCodeTicket (istemp, sceneId);
			if (resutl == "0") {
				context.Alert ("添加失败");
				return;
			}
			string seconds = istemp ? wx.GetJsonValue (resutl, "expire_seconds") : "0";   //如果用为用二维码;则二维码时间为 100000000；
			TicketVO tk= serializer.Deserialize<TicketVO> (resutl);
			context.Post ["createtime"] = Jabinfo.Help.Date.Now.ToString();
			context.Post ["ticket"] = tk.ticket;
			context.Post ["url"] = tk.url;
			context.Post ["expireSeconds"] = seconds;
			TicketMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String ticket)
		{
			if (!context.IsPost) {
				context.Variable ["ticket"] = TicketMapper.I.Create (ticket);
				return;
			}
			TicketMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String ticket)
		{
			TicketMapper.I.DeleteByPrimary (ticket);
			context.Refresh ();
		}
		#endregion
	}
}