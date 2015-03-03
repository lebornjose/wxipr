/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/3 9:53:56
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class FeedbackMapper
	{
		#region	Instance
		static FeedbackMapper _mapper;
		public static FeedbackMapper I {
			get {
				if (_mapper == null)
					_mapper = new FeedbackMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		FeedbackMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public FeedbackVO Create (String feedId)
		{
			DataRow row = query.Select ("SELECT * FROM feedback WHERE feed_id=?feed_id").Param ("feed_id", feedId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public FeedbackVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM feedback ORDER BY feed_id DESC").Limit (index, size).All ();
			FeedbackVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(feed_id) FROM feedback").
				Count ();
		}

		public bool Exist (string FeedId)
		{
			int count = query.Select ("SELECT COUNT(feed_id) FROM feedback WHERE feed_id=?FeedId").
				Param ("FeedId", FeedId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public FeedbackVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			FeedbackVO[] result = new FeedbackVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new FeedbackVO ();
			}
			return result;
		}

		FeedbackVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			FeedbackVO VO = new FeedbackVO ();
        	VO.feedId = dr["feed_id"] as string;
        	VO.context = dr["context"] as string;
        	VO.addtime = Convert.ToInt32(dr["addtime"]);
        	VO.name = dr["name"] as string;
        	VO.email = dr["email"] as string;
        	VO.mobile = dr["mobile"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("feedback").
            	Value("feed_id", data["feedId"], DataType.Char, 24).
            	Value("context", data["context"], DataType.Text).
            	Value("addtime", data["addtime"], DataType.Int).
            	Value("name", data["name"], DataType.Varchar, 30).
            	Value("email", data["email"], DataType.Varchar, 30).
            	Value("mobile", data["mobile"], DataType.Varchar, 30).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("feedback").
            	Set("context", data["context"], DataType.Text).
            	Set("addtime", data["addtime"], DataType.Int).
            	Set("name", data["name"], DataType.Varchar, 30).
            	Set("email", data["email"], DataType.Varchar, 30).
            	Set("mobile", data["mobile"], DataType.Varchar, 30).
            	Where("feed_id", data["feedId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String feedId)
		{
			return query.Delete ("feedback").Where ("feed_id", feedId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}