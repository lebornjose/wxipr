/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 9:32:29
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class ArticleDetailModel : JabinfoModel
	{
		private static ArticleDetailModel _model;

		public static ArticleDetailModel I {
			get {
				if (_model == null)
					_model = new ArticleDetailModel ();
				return _model;
			}
		}

		private ArticleDetailModel () : base ("crm", "article_detail")
		{
		}
	}
}