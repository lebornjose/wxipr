/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/14 14:34:01
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class JobController : Jabinfo.JabinfoController
	{
		#region Constructor
		public JobController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = JobMapper.I.typeCount("2");
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("job_system.status");
			context.Variable ["jobList"] = JobMapper.I.typeSelect("1",index, size);
		}
		#endregion

		/// <summary>
		/// 实习全职
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="index">Index.</param>
		public void fulltime(JabinfoContext context,int index)
		{
			int size = 30;
			context.Variable ["total"] = JobMapper.I.typeCount("2");
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("job_system.status");
			context.Variable ["jobList"] = JobMapper.I.typeSelect("2",index, size);
		}

		/// <summary>
		/// 实习全职列表
		/// </summary>
		/// <param name="context">Context.</param>
		public void fulladd(JabinfoContext context)
		{
			if (!context.IsPost) {
				context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
				context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
				context.Variable ["typeList"] = JTypeMapper.I.SelectByPage (0, 50);
				context.Variable ["wageset"] = Jabinfo.Help.Config.Get ("job_system.wageset");
				return;
			}
			context.Post ["jobId"] = Jabinfo.Help.Basic.AutoId ("full_id");
			JabinfoKeyValue extend = new JabinfoKeyValue ();
			context.Post["type"]="2";         //type=’2‘  为全职
			extend ["company"] = context.Post ["company"];
			extend ["contact"] = context.Post ["contact"];
			extend ["wage"] = context.Post ["wage"];
			context.Post ["extend"] = extend.ToString ();
			context.Post ["overtime"] =  Jabinfo.Help.Date.StringToDate (context.Post ["overtime"]).ToString ();
			JobMapper.I.Insert(context.Post);
			context.Jump("/crm/job/fulledit/"+ context.Post["jobId"],"添加成功");
		}

		public void fulledit(JabinfoContext context,string jobId)
		{
			if (!context.IsPost) {
				context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
				context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
				context.Variable ["typeList"] = JTypeMapper.I.SelectByPage (0, 50);
				context.Variable ["job"] = JobMapper.I.Create (jobId);
				context.Variable ["extend"] = JobMapper.I.Create (jobId).extendKV;
				return;
			}
			JabinfoKeyValue extend = new JabinfoKeyValue ();
			extend ["company"] = context.Post ["company"];
			extend ["contact"] = context.Post ["contact"];
			extend ["wage"] = context.Post ["wage"];
			context.Post ["extend"] = extend.ToString ();
			context.Post ["overtime"] =  Jabinfo.Help.Date.StringToDate (context.Post ["overtime"]).ToString ();
			JobMapper.I.UpdateByPrimary(context.Post);
			context.Jump("/crm/job/fulledit/"+ context.Post["jobId"],"编辑成功");
		}

		/// <summary>
		/// 全职搜索
		/// </summary>
		/// <param name="context">Context.</param>
		public void full_win(JabinfoContext context,string type)
		{
			context.Variable ["type"] = type;
			context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
			context.Variable ["typeList"] = JTypeMapper.I.SelectByPage (0, 50);   //工作类型
		}

		public void fullsearch(JabinfoContext context,int index)
		{
			int size = 30;
			string where = string.Empty;
			if (context.IsPost) {
				string type = context.Post ["type"];
				string title = context.Post ["title"];
				string city = context.Post ["city"];
				string jtype = context.Post ["jtype"];
				string status = context.Post ["status"];
					where = string.Format ("and type='{0}'", type);
				if (!string.IsNullOrEmpty (title))
						where = string.Format ("{0} and title like '%{1}%'", where, title);
				if (!string.IsNullOrEmpty (city))
						where = string.Format ("{0} and city ='{1}'", where, city);
				if (!string.IsNullOrEmpty (jtype))
						where = string.Format ("{0} and jtype='{1}'", where, jtype);
				if (!string.IsNullOrEmpty (status))
						where = string.Format ("{0} and status='{1}'", where, status);
				if (!string.IsNullOrEmpty (where)) {
						where = where.Substring (4);
					context.Session.Add ("job_search", where);
				}
			} else {
				try {
						where = context.Session.Get ("job_search").ToString ();
				} catch (Exception ex) {
					context.Jump ("/crm/job/fulltim", "页面停留过期");
					return;
				}
			}
			context.Variable ["total"] =JobModel.I.Count(where);
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("job_system.status");
			context.Variable ["jobList"] = JobModel.I.Select(index, size,where);
		}

		/// <summary>
		/// 培训活动
		/// </summary>
		/// <param name="context">Context.</param>
		public void activehome(JabinfoContext context,int index)
		{
			int size = 30;
			context.Variable ["total"] = JobMapper.I.typeCount("3");
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("job_system.status");
			context.Variable ["jobList"] = JobMapper.I.typeSelect("3",index, size);
		}

		/// <summary>
		/// 活动添加
		/// </summary>
		/// <param name="context">Context.</param>
		public void activeadd(JabinfoContext context)
		{
			if (!context.IsPost) {
				context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
				context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
				return;
			}
			context.Post ["jobId"] = Jabinfo.Help.Basic.AutoId ("crm_id");
			JabinfoKeyValue extend = new JabinfoKeyValue ();
			extend ["contact"] = context.Post ["contact"];
			context.Post ["contact"] = extend.ToString ();
			context.Post["type"]="3";         //type=’3‘  活动
			context.Post ["overtime"] =  Jabinfo.Help.Date.StringToDate (context.Post ["overtime"]).ToString ();
			JobMapper.I.Insert(context.Post);
			context.Jump("/crm/job/activeedit/"+ context.Post["jobId"],"添加成功");
		}

		public void activeedit(JabinfoContext context,string jobId)
		{
			if (!context.IsPost) {
				context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
				context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
				context.Variable ["job"] = JobMapper.I.Create (jobId);
				context.Variable ["extend"] = JobMapper.I.Create (jobId).extendKV;
				return;
			}
			JabinfoKeyValue extend = new JabinfoKeyValue ();
			extend ["contact"] = context.Post ["contact"];   //联系人
			context.Post ["extend"] = extend.ToString ();
			context.Post ["overtime"] =  Jabinfo.Help.Date.StringToDate (context.Post ["overtime"]).ToString ();
			JobMapper.I.UpdateByPrimary(context.Post);
			context.Jump("/crm/job/activeedit/"+ context.Post["jobId"],"编辑成功");

		}

		public void active_win(JabinfoContext context,string type)
		{
			context.Variable ["type"] = type;
			context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
			context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
		}
			

	   /// <summary>
	   /// 活动搜索
	   /// </summary>
	   /// <param name="context">Context.</param>
	   /// <param name="index">Index.</param>
		public void activesearch(JabinfoContext context, int index)
		{
			int size = 30;
			string where = string.Empty;
			if (context.IsPost) {
				string type = context.Post ["type"];
				string title = context.Post ["title"];
				string city = context.Post ["city"];
				string status = context.Post ["status"];
					where = string.Format ("and type='{0}'", type);
				if (!string.IsNullOrEmpty (title))
						where = string.Format ("{0} and title like '%{1}%'", where, title);
				if (!string.IsNullOrEmpty (city))
						where = string.Format ("{0} and city ='{1}'", where, city);
				if (!string.IsNullOrEmpty (status))
						where = string.Format ("{0} and status='{1}'", where, status);
				if (!string.IsNullOrEmpty (where)) {
						where = where.Substring (4);
					context.Session.Add ("active_search", where,1);
				}
			} else {
				try {
						where = context.Session.Get ("active_search").ToString ();
				} catch (Exception ex) {
					context.Jump ("/crm/job/fulltim", "页面停留过期");
					return;
				}
			}
			context.Variable ["total"] =JobModel.I.Count(where);
			context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("job_system.status");
			context.Variable ["jobList"] = JobModel.I.Select(index, size,where);
		}

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
				context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
				context.Variable ["typeList"] = JTypeMapper.I.SelectByPage (0, 50);
				context.Variable ["wageset"] = Jabinfo.Help.Config.Get ("job_system.wageset");
				context.Variable ["height"] = Jabinfo.Help.Config.Get ("job_system.height");
				return;
			}
			context.Post ["jobId"] = Jabinfo.Help.Basic.AutoId ("crm_id");
			JabinfoKeyValue extend = new JabinfoKeyValue ();
			context.Post["type"]="1";         //type=’1‘  为兼职
			extend ["workdate"] = context.Post ["workdate"]; //工作日期
			extend ["contact"] = context.Post ["contact"];
			extend ["wage"] = context.Post ["wage"];
			extend ["wageSet"] = context.Post ["wageSet"];
			context.Post ["extend"] = extend.ToString ();
			context.Post ["overtime"] =  Jabinfo.Help.Date.StringToDate (context.Post ["overtime"]).ToString ();
			JobMapper.I.Insert(context.Post);
			context.Jump("/crm/job/edit/"+ context.Post["jobId"],"添加成功");
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String jobId)
		{
			if (!context.IsPost) {
				context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
				context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
				context.Variable ["typeList"] = JTypeMapper.I.SelectByPage (0, 50);
				context.Variable ["wageset"] = Jabinfo.Help.Config.Get ("job_system.wageset");
				context.Variable ["height"] = Jabinfo.Help.Config.Get ("job_system.height");
				context.Variable ["job"] = JobMapper.I.Create (jobId);
				context.Variable ["extend"] = JobMapper.I.Create (jobId).extendKV;
				return;
			}
			JabinfoKeyValue extend = new JabinfoKeyValue ();
			extend ["workdate"] = context.Post ["workdate"]; //工作日期
			extend ["contact"] = context.Post ["contact"];
			extend ["wage"] = context.Post ["wage"];
			extend ["wageSet"] = context.Post ["wageSet"];
			context.Post ["extend"] = extend.ToString ();
			context.Post ["overtime"] =  Jabinfo.Help.Date.StringToDate (context.Post ["overtime"]).ToString ();
			JobMapper.I.UpdateByPrimary(context.Post);
			context.Jump("/crm/job/edit/"+ context.Post["jobId"],"编辑成功");
		}
		#endregion

		public void search_win(JabinfoContext context,string type)
		{
			context.Variable ["type"] = type;
			context.Variable ["cityList"] = CityMapper.I.SelectByPage (0,50);
			context.Variable ["sex"] = Jabinfo.Help.Config.Get ("job_system.sex");
			context.Variable ["height"] = Jabinfo.Help.Config.Get ("job_system.height");   //身高
			context.Variable ["typeList"] = JTypeMapper.I.SelectByPage (0, 50);   //工作类型
			context.Variable ["wageset"] = Jabinfo.Help.Config.Get ("job_system.wageset");  // 工资结算类型
		}

		/// <summary>
		/// 搜索
		/// </summary>
		/// <param name="context">Context.</param>
		public void search(JabinfoContext context,int index)
		{
			int size = 30;
			string where = string.Empty;
			if (context.IsPost) {
				string type = context.Post ["type"];
				string title = context.Post ["title"];
				string city = context.Post ["city"];
				string sex = context.Post ["sex"];
				string height = context.Post ["height"];
				string jtype = context.Post ["jtype"];
				string wageset = context.Post ["wageSet"];
				string status = context.Post ["status"];
				where = string.Format ("and type='{0}'", type);
				if (!string.IsNullOrEmpty (title))
					where = string.Format ("{0} and title like '%{1}%'", where, title);
				if (!string.IsNullOrEmpty (city))
					where = string.Format ("{0} and city ='{1}'", where, city);
				if (!string.IsNullOrEmpty (sex))
					where = string.Format ("{0} and sex='{1}'", where, sex);
				if (!string.IsNullOrEmpty (height))
					where = string.Format ("{0} and height='{1}'", where, height);
				if (!string.IsNullOrEmpty (jtype))
					where = string.Format ("{0} and jtype='{1}'", where, jtype);
				if (!string.IsNullOrEmpty (wageset))
					where = string.Format ("{0} and extend like '%wageSet:{1}%", where, wageset);
				if (!string.IsNullOrEmpty (status))
						where = string.Format ("{0} and status='{1}'", where, status);
				if (!string.IsNullOrEmpty (where)) {
					where = where.Substring (4);
					context.Session.Add ("job_search", where);
				}
			} else {
				try {
					where = context.Session.Get ("job_search").ToString ();
				} catch (Exception ex) {
					context.Jump ("/crm/job/home", "页面停留过期");
					return;
				}
			}
			context.Variable ["total"] =JobModel.I.Count(where);
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("job_system.status");
			context.Variable ["jobList"] = JobModel.I.Select(index, size,where);
		}

		#region remove
		public void remove (JabinfoContext context, String jobId)
		{
			JobMapper.I.DeleteByPrimary (jobId);
			context.Refresh ();
		}
		#endregion
	}
}