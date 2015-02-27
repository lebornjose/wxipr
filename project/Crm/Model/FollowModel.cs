/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 10:31:46
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class FollowModel : JabinfoModel
	{
		private static FollowModel _model;

		public static FollowModel I {
			get {
				if (_model == null)
					_model = new FollowModel ();
				return _model;
			}
		}

		private FollowModel () : base ("crm", "follow")
		{
		}
	}
}