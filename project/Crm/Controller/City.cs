/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/17 11:36:51
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class CityController : Jabinfo.JabinfoController
	{
		#region Constructor
		public CityController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = CityMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["cityList"] = CityMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["cityId"] = Jabinfo.Help.Basic.AutoId ("cat_tag");
			CityMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String cityId)
		{
			if (!context.IsPost) {
				context.Variable ["city"] = CityMapper.I.Create (cityId);
				return;
			}
			CityMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String cityId)
		{
			CityMapper.I.DeleteByPrimary (cityId);
			context.Refresh ();
		}
		#endregion
	}
}