/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/7 15:49:15
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class CompanyMapper
	{
		#region	Instance
		static CompanyMapper _mapper;
		public static CompanyMapper I {
			get {
				if (_mapper == null)
					_mapper = new CompanyMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		CompanyMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public CompanyVO Create (String conpanyId)
		{
			DataRow row = query.Select ("SELECT * FROM company WHERE conpany_id=?conpany_id").Param ("conpany_id", conpanyId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public CompanyVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM company ORDER BY conpany_id DESC").Limit (index, size).All ();
			CompanyVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(conpany_id) FROM company").
				Count ();
		}

		public bool Exist (string ConpanyId)
		{
			int count = query.Select ("SELECT COUNT(conpany_id) FROM company WHERE conpany_id=?ConpanyId").
				Param ("ConpanyId", ConpanyId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public CompanyVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			CompanyVO[] result = new CompanyVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new CompanyVO ();
			}
			return result;
		}

		CompanyVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			CompanyVO VO = new CompanyVO ();
        	VO.conpanyId = dr["conpany_id"] as string;
        	VO.name = dr["name"] as string;
        	VO.city = dr["city"] as string;
        	VO.date = Convert.ToInt32(dr["date"]);
        	VO.address = dr["address"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("company").
            	Value("conpany_id", data["conpanyId"], DataType.Char, 24).
            	Value("name", data["name"], DataType.Varchar, 30).
            	Value("city", data["city"], DataType.Varchar, 10).
            	Value("date", data["date"], DataType.Int).
            	Value("address", data["address"], DataType.Varchar, 100).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("company").
            	Set("name", data["name"], DataType.Varchar, 30).
            	Set("city", data["city"], DataType.Varchar, 10).
            	Set("date", data["date"], DataType.Int).
            	Set("address", data["address"], DataType.Varchar, 100).
            	Where("conpany_id", data["conpanyId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String conpanyId)
		{
			return query.Delete ("company").Where ("conpany_id", conpanyId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}