/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 9:32:29
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class ArticleDetailController : Jabinfo.JabinfoController
	{
		#region Constructor
		public ArticleDetailController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = ArticleDetailMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["articleDetailList"] = ArticleDetailMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["articleId"] = Jabinfo.Help.Basic.JabId;
			ArticleDetailMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String articleId)
		{
			if (!context.IsPost) {
				context.Variable ["articleDetail"] = ArticleDetailMapper.I.Create (articleId);
				return;
			}
			ArticleDetailMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String articleId)
		{
			ArticleDetailMapper.I.DeleteByPrimary (articleId);
			context.Refresh ();
		}
		#endregion
	}
}