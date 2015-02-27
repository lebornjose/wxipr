/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:26:48
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class FollowMapper
	{
		#region	Instance
		static FollowMapper _mapper;
		public static FollowMapper I {
			get {
				if (_mapper == null)
					_mapper = new FollowMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		FollowMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public FollowVO Create (String openId)
		{
			DataRow row = query.Select ("SELECT * FROM follow WHERE open_id=?open_id").Param ("open_id", openId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public FollowVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM follow ORDER BY open_id DESC").Limit (index, size).All ();
			FollowVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(open_id) FROM follow").
				Count ();
		}

		public bool Exist (string OpenId)
		{
			int count = query.Select ("SELECT COUNT(open_id) FROM follow WHERE open_id=?OpenId").
				Param ("OpenId", OpenId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public FollowVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			FollowVO[] result = new FollowVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new FollowVO ();
			}
			return result;
		}

		FollowVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			FollowVO VO = new FollowVO ();
        	VO.openId = dr["open_id"] as string;
        	VO.nickname = dr["nickname"] as string;
        	VO.sex = dr["sex"] as string;
        	VO.province = dr["province"] as string;
        	VO.city = dr["city"] as string;
        	VO.country = dr["country"] as string;
        	VO.headimgurl = dr["headimgurl"] as string;
        	VO.group = dr["group"] as string;
        	VO.createtime = Convert.ToInt32(dr["createtime"]);
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("follow").
            	Value("open_id", data["openId"], DataType.Varchar, 50).
            	Value("nickname", data["nickname"], DataType.Varchar, 30).
            	Value("sex", data["sex"], DataType.Char, 1).
            	Value("province", data["province"], DataType.Varchar, 20).
            	Value("city", data["city"], DataType.Varchar, 20).
            	Value("country", data["country"], DataType.Varchar, 10).
            	Value("headimgurl", data["headimgurl"], DataType.Varchar, 50).
            	Value("group", data["group"], DataType.Varchar, 20).
            	Value("createtime", data["createtime"], DataType.Int).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("follow").
            	Set("nickname", data["nickname"], DataType.Varchar, 30).
            	Set("sex", data["sex"], DataType.Char, 1).
            	Set("province", data["province"], DataType.Varchar, 20).
            	Set("city", data["city"], DataType.Varchar, 20).
            	Set("country", data["country"], DataType.Varchar, 10).
            	Set("headimgurl", data["headimgurl"], DataType.Varchar, 50).
            	Set("group", data["group"], DataType.Varchar, 20).
            	Set("createtime", data["createtime"], DataType.Int).
            	Where("open_id", data["openId"], DataType.Varchar, 50).
				Excute ();
		}

		public int DeleteByPrimary (String openId)
		{
			return query.Delete ("follow").Where ("open_id", openId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		// 获取关注者根类
		public FollowVO[] Category (string group)
		{
			DataTable table = query.Select ("SELECT * FROM `follow` WHERE `group`=?group").
	            Param("group", group, DataType.Varchar, 20).
				All ();
			FollowVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i].openId = table.Rows[i]["open_id"] as string;
				result[i].nickname = table.Rows[i]["nickname"] as string;
				result[i].sex = table.Rows[i]["sex"] as string;
				result[i].province = table.Rows[i]["province"] as string;
				result[i].city = table.Rows[i]["city"] as string;
				result[i].country = table.Rows[i]["country"] as string;
				result[i].headimgurl = table.Rows[i]["headimgurl"] as string;
				result[i].group = table.Rows[i]["group"] as string;
				result[i].createtime = Convert.ToInt32(table.Rows[i]["createtime"]);
			}
			return result;
		}
		
		#endregion
	}
}