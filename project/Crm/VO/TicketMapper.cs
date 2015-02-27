/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:45:34
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class TicketMapper
	{
		#region	Instance
		static TicketMapper _mapper;
		public static TicketMapper I {
			get {
				if (_mapper == null)
					_mapper = new TicketMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		TicketMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public TicketVO Create (String ticket)
		{
			DataRow row = query.Select ("SELECT * FROM ticket WHERE ticket=?ticket").Param ("ticket", ticket, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public TicketVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM ticket ORDER BY ticket DESC").Limit (index, size).All ();
			TicketVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(ticket) FROM ticket").
				Count ();
		}

		public bool Exist (string Ticket)
		{
			int count = query.Select ("SELECT COUNT(ticket) FROM ticket WHERE ticket=?Ticket").
				Param ("Ticket", Ticket, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public TicketVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			TicketVO[] result = new TicketVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new TicketVO ();
			}
			return result;
		}

		TicketVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			TicketVO VO = new TicketVO ();
        	VO.ticket = dr["ticket"] as string;
        	VO.url = dr["url"] as string;
        	VO.createtime = Convert.ToInt32(dr["createtime"]);
        	VO.expireSeconds = Convert.ToInt32(dr["expire_seconds"]);
        	VO.title = dr["title"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("ticket").
            	Value("ticket", data["ticket"], DataType.Varchar, 150).
            	Value("url", data["url"], DataType.Varchar, 200).
            	Value("createtime", data["createtime"], DataType.Int).
            	Value("expire_seconds", data["expireSeconds"], DataType.Int).
            	Value("title", data["title"], DataType.Varchar, 30).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("ticket").
            	Set("url", data["url"], DataType.Varchar, 200).
            	Set("createtime", data["createtime"], DataType.Int).
            	Set("expire_seconds", data["expireSeconds"], DataType.Int).
            	Set("title", data["title"], DataType.Varchar, 30).
            	Where("ticket", data["ticket"], DataType.Varchar, 150).
				Excute ();
		}

		public int DeleteByPrimary (String ticket)
		{
			return query.Delete ("ticket").Where ("ticket", ticket, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}