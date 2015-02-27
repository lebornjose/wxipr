/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 17:08:31
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class TokenMapper
	{
		#region	Instance
		static TokenMapper _mapper;
		public static TokenMapper I {
			get {
				if (_mapper == null)
					_mapper = new TokenMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		TokenMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public TokenVO Create (String tokenId)
		{
			DataRow row = query.Select ("SELECT * FROM token WHERE token_id=?token_id").Param ("token_id", tokenId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public TokenVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM token ORDER BY token_id DESC").Limit (index, size).All ();
			TokenVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(token_id) FROM token").
				Count ();
		}

		public bool Exist (string TokenId)
		{
			int count = query.Select ("SELECT COUNT(token_id) FROM token WHERE token_id=?TokenId").
				Param ("TokenId", TokenId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public TokenVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			TokenVO[] result = new TokenVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new TokenVO ();
			}
			return result;
		}

		TokenVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			TokenVO VO = new TokenVO ();
        	VO.tokenId = dr["token_id"] as string;
        	VO.value = dr["value"] as string;
        	VO.date = Convert.ToInt32(dr["date"]);
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("token").
            	Value("token_id", data["tokenId"], DataType.Char, 24).
            	Value("value", data["value"], DataType.Varchar, 150).
            	Value("date", data["date"], DataType.Int).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("token").
            	Set("value", data["value"], DataType.Varchar, 150).
            	Set("date", data["date"], DataType.Int).
            	Where("token_id", data["tokenId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String tokenId)
		{
			return query.Delete ("token").Where ("token_id", tokenId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}