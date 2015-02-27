/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 15:27:12
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class TokenModel : JabinfoModel
	{
		private static TokenModel _model;

		public static TokenModel I {
			get {
				if (_model == null)
					_model = new TokenModel ();
				return _model;
			}
		}

		private TokenModel () : base ("crm", "token")
		{
		}
	}
}