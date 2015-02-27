/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 21:03:15
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class MsglogModel : JabinfoModel
	{
		private static MsglogModel _model;

		public static MsglogModel I {
			get {
				if (_model == null)
					_model = new MsglogModel ();
				return _model;
			}
		}

		private MsglogModel () : base ("crm", "msglog")
		{
		}
	}
}