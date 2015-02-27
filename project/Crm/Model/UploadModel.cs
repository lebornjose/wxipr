/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:04:47
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class UploadModel : JabinfoModel
	{
		private static UploadModel _model;

		public static UploadModel I {
			get {
				if (_model == null)
					_model = new UploadModel ();
				return _model;
			}
		}

		private UploadModel () : base ("crm", "upload")
		{
		}
	}
}