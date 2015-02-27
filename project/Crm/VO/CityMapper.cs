/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:44:22
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class CityMapper
	{
		#region	Instance
		static CityMapper _mapper;
		public static CityMapper I {
			get {
				if (_mapper == null)
					_mapper = new CityMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		CityMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public CityVO Create (String cityId)
		{
			DataRow row = query.Select ("SELECT * FROM city WHERE city_id=?city_id").Param ("city_id", cityId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public CityVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM city ORDER BY city_id DESC").Limit (index, size).All ();
			CityVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(city_id) FROM city").
				Count ();
		}

		public bool Exist (string CityId)
		{
			int count = query.Select ("SELECT COUNT(city_id) FROM city WHERE city_id=?CityId").
				Param ("CityId", CityId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public CityVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			CityVO[] result = new CityVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new CityVO ();
			}
			return result;
		}

		CityVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			CityVO VO = new CityVO ();
        	VO.cityId = dr["city_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.index = Convert.ToInt32(dr["index"]);
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("city").
            	Value("city_id", data["cityId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 50).
            	Value("index", data["index"], DataType.Int).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("city").
            	Set("title", data["title"], DataType.Varchar, 50).
            	Set("index", data["index"], DataType.Int).
            	Where("city_id", data["cityId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String cityId)
		{
			return query.Delete ("city").Where ("city_id", cityId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}