/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/7 14:53:17
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class CompanyModel : JabinfoModel
	{
		private static CompanyModel _model;

		public static CompanyModel I {
			get {
				if (_model == null)
					_model = new CompanyModel ();
				return _model;
			}
		}

		private CompanyModel () : base ("crm", "company")
		{
		}
	}
}