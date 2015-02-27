/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 10:07:31
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class CategoryModel : JabinfoModel
	{
		private static CategoryModel _model;

		public static CategoryModel I {
			get {
				if (_model == null)
					_model = new CategoryModel ();
				return _model;
			}
		}

		private CategoryModel () : base ("crm", "category")
		{
		}
	}
}