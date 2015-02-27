/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 9:32:29
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class ArticleDetailMapper
	{
		#region	Instance
		static ArticleDetailMapper _mapper;
		public static ArticleDetailMapper I {
			get {
				if (_mapper == null)
					_mapper = new ArticleDetailMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		ArticleDetailMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public ArticleDetailVO Create (String articleId)
		{
			DataRow row = query.Select ("SELECT * FROM article_detail WHERE article_id=?article_id").Param ("article_id", articleId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public ArticleDetailVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM article_detail ORDER BY article_id DESC").Limit (index, size).All ();
			ArticleDetailVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(article_id) FROM article_detail").
				Count ();
		}

		public bool Exist (string ArticleId)
		{
			int count = query.Select ("SELECT COUNT(article_id) FROM article_detail WHERE article_id=?ArticleId").
				Param ("ArticleId", ArticleId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public ArticleDetailVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			ArticleDetailVO[] result = new ArticleDetailVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new ArticleDetailVO ();
			}
			return result;
		}

		ArticleDetailVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			ArticleDetailVO VO = new ArticleDetailVO ();
        	VO.articleId = dr["article_id"] as string;
        	VO.content = dr["content"] as string;
        	VO.categorys = dr["categorys"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("article_detail").
            	Value("article_id", data["articleId"], DataType.Char, 24).
            	Value("content", data["content"], DataType.Text).
            	Value("categorys", data["categorys"], DataType.Varchar, 500).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("article_detail").
            	Set("content", data["content"], DataType.Text).
            	Set("categorys", data["categorys"], DataType.Varchar, 500).
            	Where("article_id", data["articleId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String articleId)
		{
			return query.Delete ("article_detail").Where ("article_id", articleId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		#endregion
	}
}