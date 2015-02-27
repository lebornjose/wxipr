/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 13:45:16
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class PageMapper
	{
		#region	Instance
		static PageMapper _mapper;
		public static PageMapper I {
			get {
				if (_mapper == null)
					_mapper = new PageMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		PageMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public PageVO Create (String pageId)
		{
			DataRow row = query.Select ("SELECT * FROM page WHERE page_id=?page_id").Param ("page_id", pageId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public PageVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM page ORDER BY page_id DESC").Limit (index, size).All ();
			PageVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(page_id) FROM page").
				Count ();
		}

		public bool Exist (string PageId)
		{
			int count = query.Select ("SELECT COUNT(page_id) FROM page WHERE page_id=?PageId").
				Param ("PageId", PageId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public PageVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			PageVO[] result = new PageVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new PageVO ();
			}
			return result;
		}

		PageVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			PageVO VO = new PageVO ();
        	VO.pageId = dr["page_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.describe = dr["describe"] as string;
        	VO.ispost = dr["ispost"] as string;
        	VO.group = dr["group"] as string;
        	VO.index = Convert.ToInt32(dr["index"]);
        	VO.jtype = dr["jtype"] as string;
        	VO.content = dr["content"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("page").
            	Value("page_id", data["pageId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 100).
            	Value("describe", data["describe"], DataType.Varchar, 500).
            	Value("ispost", data["ispost"], DataType.Char, 1).
            	Value("group", data["group"], DataType.Char, 1).
            	Value("index", data["index"], DataType.Int).
            	Value("jtype", data["jtype"], DataType.Char, 24).
            	Value("content", data["content"], DataType.Text).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("page").
            	Set("title", data["title"], DataType.Varchar, 100).
            	Set("describe", data["describe"], DataType.Varchar, 500).
            	Set("ispost", data["ispost"], DataType.Char, 1).
            	Set("group", data["group"], DataType.Char, 1).
            	Set("index", data["index"], DataType.Int).
            	Set("jtype", data["jtype"], DataType.Char, 24).
            	Set("content", data["content"], DataType.Text).
            	Where("page_id", data["pageId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String pageId)
		{
			return query.Delete ("page").Where ("page_id", pageId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}