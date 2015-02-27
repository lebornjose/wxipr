/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/13 10:15:27
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class RequestModel : JabinfoModel
	{
		private static RequestModel _model;

		public static RequestModel I {
			get {
				if (_model == null)
					_model = new RequestModel ();
				return _model;
			}
		}

		private RequestModel () : base ("crm", "request")
		{
		}
	}
}