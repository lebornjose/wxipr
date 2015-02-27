/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 13:55:38
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class CategoryMapper
	{
		#region	Instance
		static CategoryMapper _mapper;
		public static CategoryMapper I {
			get {
				if (_mapper == null)
					_mapper = new CategoryMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		CategoryMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public CategoryVO Create (String categoryId)
		{
			DataRow row = query.Select ("SELECT * FROM category WHERE category_id=?category_id").Param ("category_id", categoryId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public CategoryVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM category ORDER BY category_id DESC").Limit (index, size).All ();
			CategoryVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(category_id) FROM category").
				Count ();
		}

		public bool Exist (string CategoryId)
		{
			int count = query.Select ("SELECT COUNT(category_id) FROM category WHERE category_id=?CategoryId").
				Param ("CategoryId", CategoryId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public CategoryVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			CategoryVO[] result = new CategoryVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new CategoryVO ();
			}
			return result;
		}

		CategoryVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			CategoryVO VO = new CategoryVO ();
        	VO.categoryId = dr["category_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.parentId = dr["parent_id"] as string;
        	VO.index = Convert.ToInt32(dr["index"]);
        	VO.childen = dr["childen"] as string;
        	VO.pagesize = Convert.ToInt32(dr["pagesize"]);
        	VO.keyword = dr["keyword"] as string;
        	VO.describe = dr["describe"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("category").
            	Value("category_id", data["categoryId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 50).
            	Value("parent_id", data["parentId"], DataType.Char, 24).
            	Value("index", data["index"], DataType.Int).
            	Value("childen", data["childen"], DataType.Char, 1).
            	Value("pagesize", data["pagesize"], DataType.Int).
            	Value("keyword", data["keyword"], DataType.Varchar, 1000).
            	Value("describe", data["describe"], DataType.Varchar, 500).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("category").
            	Set("title", data["title"], DataType.Varchar, 50).
            	Set("parent_id", data["parentId"], DataType.Char, 24).
            	Set("index", data["index"], DataType.Int).
            	Set("childen", data["childen"], DataType.Char, 1).
            	Set("pagesize", data["pagesize"], DataType.Int).
            	Set("keyword", data["keyword"], DataType.Varchar, 1000).
            	Set("describe", data["describe"], DataType.Varchar, 500).
            	Where("category_id", data["categoryId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String categoryId)
		{
			return query.Delete ("category").Where ("category_id", categoryId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		// 父类ID搜索
		public CategoryVO[] Select1 (string parentId)
		{
			DataTable table = query.Select ("SELECT * FROM `category` WHERE `parent_id`=?parentId").
	            Param("parentId", parentId, DataType.Char, 24).
				All ();
			CategoryVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i].categoryId = table.Rows[i]["category_id"] as string;
				result[i].title = table.Rows[i]["title"] as string;
				result[i].parentId = table.Rows[i]["parent_id"] as string;
				result[i].index = Convert.ToInt32(table.Rows[i]["index"]);
				result[i].childen = table.Rows[i]["childen"] as string;
				result[i].pagesize = Convert.ToInt32(table.Rows[i]["pagesize"]);
				result[i].keyword = table.Rows[i]["keyword"] as string;
				result[i].describe = table.Rows[i]["describe"] as string;
			}
			return result;
		}
		
		#endregion
	}
}