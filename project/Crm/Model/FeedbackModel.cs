/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 15:12:08
 */
using System;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class FeedbackModel : JabinfoModel
	{
		private static FeedbackModel _model;

		public static FeedbackModel I {
			get {
				if (_model == null)
					_model = new FeedbackModel ();
				return _model;
			}
		}

		private FeedbackModel () : base ("crm", "feedback")
		{
		}
	}
}