/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/5 15:54:10
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class JobMapper
	{
		#region	Instance
		static JobMapper _mapper;
		public static JobMapper I {
			get {
				if (_mapper == null)
					_mapper = new JobMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		JobMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public JobVO Create (String jobId)
		{
			DataRow row = query.Select ("SELECT * FROM job WHERE job_id=?job_id").Param ("job_id", jobId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public JobVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM job ORDER BY job_id DESC").Limit (index, size).All ();
			JobVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(job_id) FROM job").
				Count ();
		}

		public bool Exist (string JobId)
		{
			int count = query.Select ("SELECT COUNT(job_id) FROM job WHERE job_id=?JobId").
				Param ("JobId", JobId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public JobVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			JobVO[] result = new JobVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new JobVO ();
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

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("job").
            	Value("job_id", data["jobId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 50).
            	Value("region", data["region"], DataType.Varchar, 30).
            	Value("reads", data["reads"], DataType.Int).
            	Value("sex", data["sex"], DataType.Char, 1).
            	Value("height", data["height"], DataType.Char, 1).
            	Value("overtime", data["overtime"], DataType.Int).
            	Value("context", data["context"], DataType.Text).
            	Value("j_time", data["jTime"], DataType.Varchar, 50).
            	Value("j_address", data["jAddress"], DataType.Varchar, 50).
            	Value("mobile", data["mobile"], DataType.Varchar, 20).
            	Value("spec", data["spec"], DataType.Varchar, 200).
            	Value("note", data["note"], DataType.Text).
            	Value("type", data["type"], DataType.Char, 1).
            	Value("jtype", data["jtype"], DataType.Varchar, 30).
            	Value("people", data["people"], DataType.Int).
            	Value("status", data["status"], DataType.Char, 1).
            	Value("price", data["price"], DataType.Char, 0).
            	Value("city", data["city"], DataType.Varchar, 30).
            	Value("extend", data["extend"], DataType.Varchar, 1000).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("job").
            	Set("title", data["title"], DataType.Varchar, 50).
            	Set("region", data["region"], DataType.Varchar, 30).
            	Set("reads", data["reads"], DataType.Int).
            	Set("sex", data["sex"], DataType.Char, 1).
            	Set("height", data["height"], DataType.Char, 1).
            	Set("overtime", data["overtime"], DataType.Int).
            	Set("context", data["context"], DataType.Text).
            	Set("j_time", data["jTime"], DataType.Varchar, 50).
            	Set("j_address", data["jAddress"], DataType.Varchar, 50).
            	Set("mobile", data["mobile"], DataType.Varchar, 20).
            	Set("spec", data["spec"], DataType.Varchar, 200).
            	Set("note", data["note"], DataType.Text).
            	Set("type", data["type"], DataType.Char, 1).
            	Set("jtype", data["jtype"], DataType.Varchar, 30).
            	Set("people", data["people"], DataType.Int).
            	Set("status", data["status"], DataType.Char, 1).
            	Set("price", data["price"], DataType.Char, 0).
            	Set("city", data["city"], DataType.Varchar, 30).
            	Set("extend", data["extend"], DataType.Varchar, 1000).
            	Where("job_id", data["jobId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String jobId)
		{
			return query.Delete ("job").Where ("job_id", jobId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		// 工作类型统计
		public int typeCount (string type)
		{
			return query.Select ("SELECT COUNT(*) FROM `job` WHERE `type`=?type").
            	Param("type", type, DataType.Char, 1).
			    Count ();
		}
		
		// 工作类型分类
		public JobVO[] typeSelect (string type, int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM `job` WHERE `type`=?type").
	            Param("type", type, DataType.Char, 1).Limit(index, size).
				All ();
			JobVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i].jobId = table.Rows[i]["job_id"] as string;
				result[i].title = table.Rows[i]["title"] as string;
				result[i].region = table.Rows[i]["region"] as string;
				result[i].reads = Convert.ToInt32(table.Rows[i]["reads"]);
				result[i].sex = table.Rows[i]["sex"] as string;
				result[i].height = table.Rows[i]["height"] as string;
				result[i].overtime = Convert.ToInt32(table.Rows[i]["overtime"]);
				result[i].context = table.Rows[i]["context"] as string;
				result[i].jTime = table.Rows[i]["j_time"] as string;
				result[i].jAddress = table.Rows[i]["j_address"] as string;
				result[i].mobile = table.Rows[i]["mobile"] as string;
				result[i].spec = table.Rows[i]["spec"] as string;
				result[i].note = table.Rows[i]["note"] as string;
				result[i].type = table.Rows[i]["type"] as string;
				result[i].jtype = table.Rows[i]["jtype"] as string;
				result[i].people = Convert.ToInt32(table.Rows[i]["people"]);
				result[i].status = table.Rows[i]["status"] as string;
				result[i].price = table.Rows[i]["price"] as string;
				result[i].city = table.Rows[i]["city"] as string;
				result[i].extend = table.Rows[i]["extend"] as string;
			}
			return result;
		}
		
		#endregion
	}
}