/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/19 22:00:21
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class JTypeMapper
	{
		#region	Instance
		static JTypeMapper _mapper;
		public static JTypeMapper I {
			get {
				if (_mapper == null)
					_mapper = new JTypeMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		JTypeMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public JTypeVO Create (String typeId)
		{
			DataRow row = query.Select ("SELECT * FROM j_type WHERE type_id=?type_id").Param ("type_id", typeId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public JTypeVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM j_type ORDER BY `index` DESC").Limit (index, size).All ();
			JTypeVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(type_id) FROM j_type").
				Count ();
		}

		public bool Exist (string TypeId)
		{
			int count = query.Select ("SELECT COUNT(type_id) FROM j_type WHERE type_id=?TypeId").
				Param ("TypeId", TypeId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public JTypeVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			JTypeVO[] result = new JTypeVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new JTypeVO ();
			}
			return result;
		}

		JTypeVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			JTypeVO VO = new JTypeVO ();
        	VO.typeId = dr["type_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.index = Convert.ToInt32(dr["index"]);
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("j_type").
            	Value("type_id", data["typeId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 50).
            	Value("index", data["index"], DataType.Int).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("j_type").
            	Set("title", data["title"], DataType.Varchar, 50).
            	Set("index", data["index"], DataType.Int).
            	Where("type_id", data["typeId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String typeId)
		{
			return query.Delete ("j_type").Where ("type_id", typeId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}