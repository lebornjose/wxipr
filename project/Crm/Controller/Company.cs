/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/7 14:53:17
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class CompanyController : Jabinfo.JabinfoController
	{
		#region Constructor
		public CompanyController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = CompanyMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["companyList"] = CompanyMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["conpanyId"] = Jabinfo.Help.Basic.JabId;
			CompanyMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String conpanyId)
		{
			if (!context.IsPost) {
				context.Variable ["company"] = CompanyMapper.I.Create (conpanyId);
				return;
			}
			CompanyMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String conpanyId)
		{
			CompanyMapper.I.DeleteByPrimary (conpanyId);
			context.Refresh ();
		}
		#endregion
	}
}