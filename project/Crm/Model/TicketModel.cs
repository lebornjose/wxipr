/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:45:34
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class TicketModel : JabinfoModel
	{
		private static TicketModel _model;

		public static TicketModel I {
			get {
				if (_model == null)
					_model = new TicketModel ();
				return _model;
			}
		}

		private TicketModel () : base ("crm", "ticket")
		{
		}
	}
}