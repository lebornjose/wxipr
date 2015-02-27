/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/19 22:00:08
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class JTypeModel : JabinfoModel
	{
		private static JTypeModel _model;

		public static JTypeModel I {
			get {
				if (_model == null)
					_model = new JTypeModel ();
				return _model;
			}
		}

		private JTypeModel () : base ("crm", "j_type")
		{
		}
	}
}