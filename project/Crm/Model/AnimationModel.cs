/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 8:44:09
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class AnimationModel : JabinfoModel
	{
		private static AnimationModel _model;

		public static AnimationModel I {
			get {
				if (_model == null)
					_model = new AnimationModel ();
				return _model;
			}
		}

		private AnimationModel () : base ("crm", "animation")
		{
		}
	}
}