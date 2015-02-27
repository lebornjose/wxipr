/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/19 22:00:08
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class JTypeController : Jabinfo.JabinfoController
	{
		#region Constructor
		public JTypeController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = JTypeMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["jTypeList"] = JTypeMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["typeId"] = Jabinfo.Help.Basic.JabId;
			JTypeMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String typeId)
		{
			if (!context.IsPost) {
				context.Variable ["jType"] = JTypeMapper.I.Create (typeId);
				return;
			}
			JTypeMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String typeId)
		{
			JTypeMapper.I.DeleteByPrimary (typeId);
			context.Refresh ();
		}
		#endregion
	}
}