/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/14 14:34:01
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class JobModel : JabinfoModel
	{
		private static JobModel _model;

		public static JobModel I {
			get {
				if (_model == null)
					_model = new JobModel ();
				return _model;
			}
		}

		private JobModel () : base ("crm", "job")
		{
		}

		public int Count(string where)
		{
			return this.Query.Count ("SELECT COUNT(*) from `job` WHERE "+where);
		}

		public JobVO[] Select(int index,int size,string where)
		{
			DataTable table = Query.Select ("SELECT * from `job` WHERE " + where+ "ORDER BY job_id DESC").Limit (index, size).All();
			JobVO[] result = JobMapper.I.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		JobVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			JobVO VO = new JobVO ();
			VO.jobId = dr["job_id"] as string;
			VO.title = dr["title"] as string;
			VO.region = dr["region"] as string;
			VO.reads = Convert.ToInt32(dr["reads"]);
			VO.sex = dr["sex"] as string;
			VO.height = dr["height"] as string;
			VO.overtime = Convert.ToInt32(dr["overtime"]);
			VO.context = dr["context"] as string;
			VO.jTime = dr["j_time"] as string;
			VO.jAddress = dr["j_address"] as string;
			VO.mobile = dr["mobile"] as string;
			VO.spec = dr["spec"] as string;
			VO.note = dr["note"] as string;
			VO.type = dr["type"] as string;
			VO.jtype = dr["jtype"] as string;
			VO.people = Convert.ToInt32(dr["people"]);
			VO.status = dr["status"] as string;
			VO.price = dr["price"] as string;
			VO.city = dr["city"] as string;
			VO.extend = dr["extend"] as string;
			return VO;
		}

	}
}