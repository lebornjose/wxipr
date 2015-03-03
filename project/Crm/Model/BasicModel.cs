/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 10:09:49
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class BasicModel : JabinfoModel
	{
		private static BasicModel _model;

		public static BasicModel I {
			get {
				if (_model == null)
					_model = new BasicModel ();
				return _model;
			}
		}

		private BasicModel () : base ("crm", "basic")
		{
		}
	}
}