/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 11:11:01
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class PageModel : JabinfoModel
	{
		private static PageModel _model;

		public static PageModel I {
			get {
				if (_model == null)
					_model = new PageModel ();
				return _model;
			}
		}

		private PageModel () : base ("crm", "page")
		{
		}
	}
}