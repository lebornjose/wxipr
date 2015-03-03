/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/3 9:25:09
 */
using System;
using System.Data;

namespace Jabinfo.Crm.VO
{
	internal class ArticleMapper
	{
		#region	Instance
		static ArticleMapper _mapper;
		public static ArticleMapper I {
			get {
				if (_mapper == null)
					_mapper = new ArticleMapper ();
				return _mapper;
			}
		}
		Jabinfo.DB.Interface.IQuery query;

		ArticleMapper ()
		{
			query = JabinfoDB.Instance ("crm");
		}
		#endregion

		#region Default Method
		public ArticleVO Create (String articleId)
		{
			DataRow row = query.Select ("SELECT * FROM article WHERE article_id=?article_id").Param ("article_id", articleId, DataType.Char, 24).Row ();
			return this.Init (row);
		}

		public ArticleVO[] SelectByPage (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM article ORDER BY article_id DESC").Limit (index, size).All ();
			ArticleVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i] = this.Init(table.Rows[i]);
			}
			return result;
		}

		public int CountAll ()
		{
			return query.
				Select ("SELECT COUNT(article_id) FROM article").
				Count ();
		}

		public bool Exist (string ArticleId)
		{
			int count = query.Select ("SELECT COUNT(article_id) FROM article WHERE article_id=?ArticleId").
				Param ("ArticleId", ArticleId, DataType.Char, 24).
				Count ();
			return count > 0;
		}

		public ArticleVO[] Collection (DataTable table)
		{
			int length = table.Rows.Count;
			ArticleVO[] result = new ArticleVO[length];
			for (int i = 0; i < length; i++) {
				result [i] = new ArticleVO ();
			}
			return result;
		}

		ArticleVO Init (DataRow dr)
		{
			if (dr == null)
				return null;
			ArticleVO VO = new ArticleVO ();
        	VO.articleId = dr["article_id"] as string;
        	VO.title = dr["title"] as string;
        	VO.caption = dr["caption"] as string;
        	VO.author = dr["author"] as string;
        	VO.model = dr["model"] as string;
        	VO.pubtime = Convert.ToInt32(dr["pubtime"]);
        	VO.summary = dr["summary"] as string;
        	VO.reads = Convert.ToInt32(dr["reads"]);
        	VO.ispost = dr["ispost"] as string;
        	VO.comeUrl = dr["come_url"] as string;
        	VO.come = dr["come"] as string;
        	VO.keyword = dr["keyword"] as string;
        	VO.uid = dr["uid"] as string;
        	VO.categorys = dr["categorys"] as string;
        	VO.jtype = dr["jtype"] as string;
        	VO.categoryId = dr["category_id"] as string;
        	VO.iscash = dr["iscash"] as string;
			return VO;
		}

		public int Insert (JabinfoKeyValue data)
		{
			return query.Insert ("article").
            	Value("article_id", data["articleId"], DataType.Char, 24).
            	Value("title", data["title"], DataType.Varchar, 200).
            	Value("caption", data["caption"], DataType.Varchar, 200).
            	Value("author", data["author"], DataType.Varchar, 50).
            	Value("model", data["model"], DataType.Varchar, 20).
            	Value("pubtime", data["pubtime"], DataType.Int).
            	Value("summary", data["summary"], DataType.Varchar, 600).
            	Value("reads", data["reads"], DataType.Int).
            	Value("ispost", data["ispost"], DataType.Char, 1).
            	Value("come_url", data["comeUrl"], DataType.Varchar, 100).
            	Value("come", data["come"], DataType.Varchar, 50).
            	Value("keyword", data["keyword"], DataType.Varchar, 200).
            	Value("uid", data["uid"], DataType.Char, 24).
            	Value("categorys", data["categorys"], DataType.Varchar, 200).
            	Value("jtype", data["jtype"], DataType.Char, 1).
            	Value("category_id", data["categoryId"], DataType.Varchar, 30).
            	Value("iscash", data["iscash"], DataType.Char, 1).
				Excute ();
		}

		public int UpdateByPrimary (JabinfoKeyValue data)
		{
			return query.Update ("article").
            	Set("title", data["title"], DataType.Varchar, 200).
            	Set("caption", data["caption"], DataType.Varchar, 200).
            	Set("author", data["author"], DataType.Varchar, 50).
            	Set("model", data["model"], DataType.Varchar, 20).
            	Set("pubtime", data["pubtime"], DataType.Int).
            	Set("summary", data["summary"], DataType.Varchar, 600).
            	Set("reads", data["reads"], DataType.Int).
            	Set("ispost", data["ispost"], DataType.Char, 1).
            	Set("come_url", data["comeUrl"], DataType.Varchar, 100).
            	Set("come", data["come"], DataType.Varchar, 50).
            	Set("keyword", data["keyword"], DataType.Varchar, 200).
            	Set("uid", data["uid"], DataType.Char, 24).
            	Set("categorys", data["categorys"], DataType.Varchar, 200).
            	Set("jtype", data["jtype"], DataType.Char, 1).
            	Set("category_id", data["categoryId"], DataType.Varchar, 30).
            	Set("iscash", data["iscash"], DataType.Char, 1).
            	Where("article_id", data["articleId"], DataType.Char, 24).
				Excute ();
		}

		public int DeleteByPrimary (String articleId)
		{
			return query.Delete ("article").Where ("article_id", articleId, DataType.Char, 24).Excute ();
		}
		#endregion

		#region	Self Method
		
		// 阅读排行榜
		public ArticleVO[] Ranking (int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM `article` ORDER BY `pubtime` desc").Limit(index, size).
				All ();
			ArticleVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i].articleId = table.Rows[i]["article_id"] as string;
				result[i].title = table.Rows[i]["title"] as string;
				result[i].caption = table.Rows[i]["caption"] as string;
				result[i].author = table.Rows[i]["author"] as string;
				result[i].model = table.Rows[i]["model"] as string;
				result[i].pubtime = Convert.ToInt32(table.Rows[i]["pubtime"]);
				result[i].summary = table.Rows[i]["summary"] as string;
				result[i].reads = Convert.ToInt32(table.Rows[i]["reads"]);
				result[i].ispost = table.Rows[i]["ispost"] as string;
				result[i].comeUrl = table.Rows[i]["come_url"] as string;
				result[i].come = table.Rows[i]["come"] as string;
				result[i].keyword = table.Rows[i]["keyword"] as string;
				result[i].uid = table.Rows[i]["uid"] as string;
				result[i].categorys = table.Rows[i]["categorys"] as string;
				result[i].jtype = table.Rows[i]["jtype"] as string;
				result[i].categoryId = table.Rows[i]["category_id"] as string;
				result[i].iscash = table.Rows[i]["iscash"] as string;
			}
			return result;
		}
		
		// 分类查询
		public ArticleVO[] Select1 (string categoryId, int index, int size)
		{
			DataTable table = query.Select ("SELECT * FROM `article` WHERE `categorys`=?categoryId ").
	            Param("categoryId", categoryId, DataType.Varchar, 30).Limit(index, size).
				All ();
			ArticleVO[] result = this.Collection (table);
			for (int i = 0; i < result.Length; i++) {
				result[i].articleId = table.Rows[i]["article_id"] as string;
				result[i].title = table.Rows[i]["title"] as string;
				result[i].caption = table.Rows[i]["caption"] as string;
				result[i].author = table.Rows[i]["author"] as string;
				result[i].model = table.Rows[i]["model"] as string;
				result[i].pubtime = Convert.ToInt32(table.Rows[i]["pubtime"]);
				result[i].summary = table.Rows[i]["summary"] as string;
				result[i].reads = Convert.ToInt32(table.Rows[i]["reads"]);
				result[i].ispost = table.Rows[i]["ispost"] as string;
				result[i].comeUrl = table.Rows[i]["come_url"] as string;
				result[i].come = table.Rows[i]["come"] as string;
				result[i].keyword = table.Rows[i]["keyword"] as string;
				result[i].uid = table.Rows[i]["uid"] as string;
				result[i].categorys = table.Rows[i]["categorys"] as string;
				result[i].jtype = table.Rows[i]["jtype"] as string;
				result[i].categoryId = table.Rows[i]["category_id"] as string;
				result[i].iscash = table.Rows[i]["iscash"] as string;
			}
			return result;
		}
		
		// 分类统计
		public int Ctotal (string categoryId)
		{
			return query.Select ("SELECT COUNT(*) FROM `article` WHERE `categorys`=?categoryId ").
            	Param("categoryId", categoryId, DataType.Varchar, 30).
			    Count ();
		}
		
		#endregion
	}
}