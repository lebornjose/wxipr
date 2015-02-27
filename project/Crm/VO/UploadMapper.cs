/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:04:47
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class UploadMapper
	{
		#region	Instance
		static UploadMapper _mapper;
		public static UploadMapper I {
			get {
				if (_mapper == null)
					_mapper = new UploadMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		UploadMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public UploadVO Create (String uploadId)
		{
			DataRow row = query.Select ("SELECT * FROM upload WHERE upload_id=?upload_id").Param ("upload_id", uploadId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public UploadVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM upload ORDER BY upload_id DESC").Limit (index, size).All ();
			UploadVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(upload_id) FROM upload").
				Count ();
		}

		public bool Exist (string UploadId)
		{
			int count = query.Select ("SELECT COUNT(upload_id) FROM upload WHERE upload_id=?UploadId").
				Param ("UploadId", UploadId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public UploadVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			UploadVO[] result = new UploadVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new UploadVO ();
			}
			return result;
		}

		UploadVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			UploadVO VO = new UploadVO ();
        	VO.uploadId = dr["upload_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.type = dr["type"] as string;
        	VO.mediaId = dr["media_id"] as string;
        	VO.createdAt = Convert.ToInt32(dr["created_at"]);
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("upload").
            	Value("upload_id", data["uploadId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 30).
            	Value("type", data["type"], DataType.Varchar, 10).
            	Value("media_id", data["mediaId"], DataType.Varchar, 150).
            	Value("created_at", data["createdAt"], DataType.Int).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("upload").
            	Set("title", data["title"], DataType.Varchar, 30).
            	Set("type", data["type"], DataType.Varchar, 10).
            	Set("media_id", data["mediaId"], DataType.Varchar, 150).
            	Set("created_at", data["createdAt"], DataType.Int).
            	Where("upload_id", data["uploadId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String uploadId)
		{
			return query.Delete ("upload").Where ("upload_id", uploadId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}