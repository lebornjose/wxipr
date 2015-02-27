/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 21:03:15
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class MsglogMapper
	{
		#region	Instance
		static MsglogMapper _mapper;
		public static MsglogMapper I {
			get {
				if (_mapper == null)
					_mapper = new MsglogMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		MsglogMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public MsglogVO Create (String msgId)
		{
			DataRow row = query.Select ("SELECT * FROM msglog WHERE msg_id=?msg_id").Param ("msg_id", msgId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public MsglogVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM msglog ORDER BY msg_id DESC").Limit (index, size).All ();
			MsglogVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(msg_id) FROM msglog").
				Count ();
		}

		public bool Exist (string MsgId)
		{
			int count = query.Select ("SELECT COUNT(msg_id) FROM msglog WHERE msg_id=?MsgId").
				Param ("MsgId", MsgId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public MsglogVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			MsglogVO[] result = new MsglogVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new MsglogVO ();
			}
			return result;
		}

		MsglogVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			MsglogVO VO = new MsglogVO ();
        	VO.msgId = dr["msg_id"] as string;
        	VO.errmsg = dr["errmsg"] as string;
        	VO.errcode = dr["errcode"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("msglog").
            	Value("msg_id", data["msgId"], DataType.Char, 24).
            	Value("errmsg", data["errmsg"], DataType.Varchar, 100).
            	Value("errcode", data["errcode"], DataType.Varchar, 30).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("msglog").
            	Set("errmsg", data["errmsg"], DataType.Varchar, 100).
            	Set("errcode", data["errcode"], DataType.Varchar, 30).
            	Where("msg_id", data["msgId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String msgId)
		{
			return query.Delete ("msglog").Where ("msg_id", msgId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}