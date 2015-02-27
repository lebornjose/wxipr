/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 15:48:08
 */
using System;
using System.Text;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class MenuController : Jabinfo.JabinfoController
	{
		#region Constructor
		public MenuController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context)
		{
			context.Variable ["total"] = MenuMapper.I.DireCount ("0");
			context.Variable["type"]=Jabinfo.Help.Config.Get("wx_system.type");
			context.Variable ["menuList"] = MenuMapper.I.DireList ("0");
		}
		#endregion

		/// <summary>
		/// 子目录
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="meunId">Meun identifier.</param>
		public void dire(JabinfoContext context,string meunId)
		{
			context.Variable ["total"] = MenuMapper.I.DireCount (meunId);
			context.Variable ["parentId"] = meunId;
			context.Variable ["title"] = MenuMapper.I.Create (meunId).name;
			context.Variable["type"]=Jabinfo.Help.Config.Get("wx_system.type");
			context.Variable ["menuList"] = MenuMapper.I.DireList (meunId);
		}


		#region add
		public void add (JabinfoContext context,string parentId)
		{
			if (!context.IsPost) {
				return;
			}
			if(string.IsNullOrEmpty(parentId))
				context.Post["parentId"]="0";
			else
				context.Post["parentId"]=parentId;
			context.Post ["menuId"] = Jabinfo.Help.Basic.JabId;
			MenuMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String menuId)
		{
			if (!context.IsPost) {
				context.Variable ["menu"] = MenuMapper.I.Create (menuId);
				return;
			}
			MenuMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String menuId)
		{
			MenuMapper.I.DeleteByPrimary (menuId);
			context.Refresh ();
		}
		#endregion
	}
}