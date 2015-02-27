/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 15:12:08
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class FeedbackController : Jabinfo.JabinfoController
	{
		#region Constructor
		public FeedbackController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = FeedbackMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["feedbackList"] = FeedbackMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["feedId"] = Jabinfo.Help.Basic.JabId;
			FeedbackMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String feedId)
		{
			if (!context.IsPost) {
				context.Variable ["feedback"] = FeedbackMapper.I.Create (feedId);
				return;
			}
			FeedbackMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String feedId)
		{
			FeedbackMapper.I.DeleteByPrimary (feedId);
			context.Refresh ();
		}
		#endregion
	}
}