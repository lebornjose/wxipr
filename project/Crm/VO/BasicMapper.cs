/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 10:09:49
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class BasicMapper
	{
		#region	Instance
		static BasicMapper _mapper;
		public static BasicMapper I {
			get {
				if (_mapper == null)
					_mapper = new BasicMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		BasicMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public BasicVO Create (String basicId)
		{
			DataRow row = query.Select ("SELECT * FROM basic WHERE basic_id=?basic_id").Param ("basic_id", basicId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public BasicVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM basic ORDER BY basic_id DESC").Limit (index, size).All ();
			BasicVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(basic_id) FROM basic").
				Count ();
		}

		public bool Exist (string BasicId)
		{
			int count = query.Select ("SELECT COUNT(basic_id) FROM basic WHERE basic_id=?BasicId").
				Param ("BasicId", BasicId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public BasicVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			BasicVO[] result = new BasicVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new BasicVO ();
			}
			return result;
		}

		BasicVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			BasicVO VO = new BasicVO ();
        	VO.basicId = dr["basic_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.copyright = dr["copyright"] as string;
        	VO.email = dr["email"] as string;
        	VO.address = dr["address"] as string;
        	VO.pro = dr["pro"] as string;
        	VO.tel = dr["tel"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("basic").
            	Value("basic_id", data["basicId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 30).
            	Value("copyright", data["copyright"], DataType.Varchar, 100).
            	Value("email", data["email"], DataType.Varchar, 100).
            	Value("address", data["address"], DataType.Varchar, 100).
            	Value("pro", data["pro"], DataType.Varchar, 100).
            	Value("tel", data["tel"], DataType.Varchar, 100).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("basic").
            	Set("title", data["title"], DataType.Varchar, 30).
            	Set("copyright", data["copyright"], DataType.Varchar, 100).
            	Set("email", data["email"], DataType.Varchar, 100).
            	Set("address", data["address"], DataType.Varchar, 100).
            	Set("pro", data["pro"], DataType.Varchar, 100).
            	Set("tel", data["tel"], DataType.Varchar, 100).
            	Where("basic_id", data["basicId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String basicId)
		{
			return query.Delete ("basic").Where ("basic_id", basicId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}