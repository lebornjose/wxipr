/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/17 11:36:51
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class CityModel : JabinfoModel
	{
		private static CityModel _model;

		public static CityModel I {
			get {
				if (_model == null)
					_model = new CityModel ();
				return _model;
			}
		}

		private CityModel () : base ("crm", "city")
		{
		}
	}
}