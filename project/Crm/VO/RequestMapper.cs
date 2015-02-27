/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:41:48
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class RequestMapper
	{
		#region	Instance
		static RequestMapper _mapper;
		public static RequestMapper I {
			get {
				if (_mapper == null)
					_mapper = new RequestMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		RequestMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public RequestVO Create (String requestId)
		{
			DataRow row = query.Select ("SELECT * FROM request WHERE request_id=?request_id").Param ("request_id", requestId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public RequestVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM request ORDER BY request_id DESC").Limit (index, size).All ();
			RequestVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(request_id) FROM request").
				Count ();
		}

		public bool Exist (string RequestId)
		{
			int count = query.Select ("SELECT COUNT(request_id) FROM request WHERE request_id=?RequestId").
				Param ("RequestId", RequestId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public RequestVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			RequestVO[] result = new RequestVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new RequestVO ();
			}
			return result;
		}

		RequestVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			RequestVO VO = new RequestVO ();
        	VO.requestId = dr["request_id"] as string;
        	VO.toUserName = dr["ToUserName"] as string;
        	VO.fromUserName = dr["FromUserName"] as string;
        	VO.createTime = Convert.ToInt32(dr["CreateTime"]);
        	VO.msgType = dr["MsgType"] as string;
        	VO.content = dr["Content"] as string;
        	VO.msgId = dr["MsgId"] as string;
        	VO.status = dr["status"] as string;
        	VO.reply = dr["reply"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("request").
            	Value("request_id", data["requestId"], DataType.Char, 24).
            	Value("ToUserName", data["toUserName"], DataType.Varchar, 50).
            	Value("FromUserName", data["fromUserName"], DataType.Varchar, 50).
            	Value("CreateTime", data["createTime"], DataType.Int).
            	Value("MsgType", data["msgType"], DataType.Varchar, 20).
            	Value("Content", data["content"], DataType.Text).
            	Value("MsgId", data["msgId"], DataType.Char, 64).
            	Value("status", data["status"], DataType.Char, 1).
            	Value("reply", data["reply"], DataType.Varchar, 500).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("request").
            	Set("ToUserName", data["toUserName"], DataType.Varchar, 50).
            	Set("FromUserName", data["fromUserName"], DataType.Varchar, 50).
            	Set("CreateTime", data["createTime"], DataType.Int).
            	Set("MsgType", data["msgType"], DataType.Varchar, 20).
            	Set("Content", data["content"], DataType.Text).
            	Set("MsgId", data["msgId"], DataType.Char, 64).
            	Set("status", data["status"], DataType.Char, 1).
            	Set("reply", data["reply"], DataType.Varchar, 500).
            	Where("request_id", data["requestId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String requestId)
		{
			return query.Delete ("request").Where ("request_id", requestId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}