/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 9:36:41
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class MenuMapper
	{
		#region	Instance
		static MenuMapper _mapper;
		public static MenuMapper I {
			get {
				if (_mapper == null)
					_mapper = new MenuMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		MenuMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public MenuVO Create (String menuId)
		{
			DataRow row = query.Select ("SELECT * FROM menu WHERE menu_id=?menu_id").Param ("menu_id", menuId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public MenuVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM menu ORDER BY menu_id DESC").Limit (index, size).All ();
			MenuVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(menu_id) FROM menu").
				Count ();
		}

		public bool Exist (string MenuId)
		{
			int count = query.Select ("SELECT COUNT(menu_id) FROM menu WHERE menu_id=?MenuId").
				Param ("MenuId", MenuId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public MenuVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			MenuVO[] result = new MenuVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new MenuVO ();
			}
			return result;
		}

		MenuVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			MenuVO VO = new MenuVO ();
        	VO.menuId = dr["menu_id"] as string;
        	VO.type = dr["type"] as string;
        	VO.name = dr["name"] as string;
        	VO.key = dr["key"] as string;
        	VO.parentId = dr["parent_id"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("menu").
            	Value("menu_id", data["menuId"], DataType.Char, 24).
            	Value("type", data["type"], DataType.Varchar, 20).
            	Value("name", data["name"], DataType.Varchar, 20).
            	Value("key", data["key"], DataType.Varchar, 60).
            	Value("parent_id", data["parentId"], DataType.Char, 24).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("menu").
            	Set("type", data["type"], DataType.Varchar, 20).
            	Set("name", data["name"], DataType.Varchar, 20).
            	Set("key", data["key"], DataType.Varchar, 60).
            	Set("parent_id", data["parentId"], DataType.Char, 24).
            	Where("menu_id", data["menuId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String menuId)
		{
			return query.Delete ("menu").Where ("menu_id", menuId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		// 统计直接数目
		public int DireCount (string parentId)
		{
			return query.Select ("SELECT COUNT(*) FROM `menu` WHERE `parent_id`=?parentId").
            	Param("parentId", parentId, DataType.Char, 24).
			    Count ();
		}
		
		// 获取子目录菜单
		public MenuVO[] DireList (string parentId)
		{
			DataTable table = query.Select ("SELECT * FROM `menu` WHERE `parent_id`=?parentId").
	            Param("parentId", parentId, DataType.Char, 24).
				All ();
			MenuVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i].menuId = table.Rows[i]["menu_id"] as string;
				result[i].type = table.Rows[i]["type"] as string;
				result[i].name = table.Rows[i]["name"] as string;
				result[i].key = table.Rows[i]["key"] as string;
				result[i].parentId = table.Rows[i]["parent_id"] as string;
			}
			return result;
		}
		
		#endregion
	}
}