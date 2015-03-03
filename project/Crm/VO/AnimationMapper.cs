/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 8:44:09
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class AnimationMapper
	{
		#region	Instance
		static AnimationMapper _mapper;
		public static AnimationMapper I {
			get {
				if (_mapper == null)
					_mapper = new AnimationMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		AnimationMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public AnimationVO Create (String animationId)
		{
			DataRow row = query.Select ("SELECT * FROM animation WHERE animation_id=?animation_id").Param ("animation_id", animationId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public AnimationVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM animation ORDER BY animation_id DESC").Limit (index, size).All ();
			AnimationVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(animation_id) FROM animation").
				Count ();
		}

		public bool Exist (string AnimationId)
		{
			int count = query.Select ("SELECT COUNT(animation_id) FROM animation WHERE animation_id=?AnimationId").
				Param ("AnimationId", AnimationId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public AnimationVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			AnimationVO[] result = new AnimationVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new AnimationVO ();
			}
			return result;
		}

		AnimationVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			AnimationVO VO = new AnimationVO ();
        	VO.animationId = dr["animation_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.url = dr["url"] as string;
        	VO.index = Convert.ToInt32(dr["index"]);
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("animation").
            	Value("animation_id", data["animationId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 30).
            	Value("url", data["url"], DataType.Varchar, 100).
            	Value("index", data["index"], DataType.Int).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("animation").
            	Set("title", data["title"], DataType.Varchar, 30).
            	Set("url", data["url"], DataType.Varchar, 100).
            	Set("index", data["index"], DataType.Int).
            	Where("animation_id", data["animationId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String animationId)
		{
			return query.Delete ("animation").Where ("animation_id", animationId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}