/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 9:17:13
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class ArticleModel : JabinfoModel
	{
		private static ArticleModel _model;

		public static ArticleModel I {
			get {
				if (_model == null)
					_model = new ArticleModel ();
				return _model;
			}
		}

		private ArticleModel () : base ("crm", "article")
		{
		}

		/// <summary>
		/// 查询
		/// </summary>
		public ArticleVO[]  Select(string where,int index,int size)
		{
			DataTable table = this.Query.Select ("select * from article where " + where + "").Limit (index, size).All ();
			return ArticleMapper.I.Collection (table);
		}

		// 关键字找文章
		public ArticleVO[] Keywold (string keyword, int index, int size)
		{
			DataTable table = this.Query.Select ("SELECT * FROM `article` where `keyword` like ?keyword ").
				Param("keyword", string.Format("%{0}%",keyword), DataType.Varchar, 200).Limit(index, size).
				All ();
			ArticleVO[] result = ArticleMapper.I.Collection (table);
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
				result[i].come = table.Rows[i]["come"] as string;
				result[i].comeUrl = table.Rows[i]["come_url"] as string;
				result[i].keyword = table.Rows[i]["keyword"] as string;
				result[i].iscash = table.Rows[i]["iscash"] as string;
				result[i].uid = table.Rows[i]["uid"] as string;
				result[i].categorys = table.Rows[i]["categorys"] as string;
				result[i].jtype = table.Rows[i]["jtype"] as string;
				result[i].categoryId = table.Rows[i]["category_id"] as string;
			}
			return result;
		}
	}
}