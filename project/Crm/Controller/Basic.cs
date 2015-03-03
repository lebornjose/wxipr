/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 10:09:49
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class BasicController : Jabinfo.JabinfoController
	{
		#region Constructor
		public BasicController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = BasicMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["basicList"] = BasicMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["basicId"] = Jabinfo.Help.Basic.JabId;
			BasicMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String basicId)
		{
			if (!context.IsPost) {
				context.Variable ["basic"] = BasicMapper.I.Create (basicId);
				return;
			}
			BasicMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String basicId)
		{
			BasicMapper.I.DeleteByPrimary (basicId);
			context.Refresh ();
		}
		#endregion
	}
}