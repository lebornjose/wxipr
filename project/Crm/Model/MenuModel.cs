/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 15:48:08
 */
using System;
using System.Text;
using System.Data;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm.Model
{
	internal class MenuModel : JabinfoModel
	{
		private static MenuModel _model;

		public static MenuModel I {
			get {
				if (_model == null)
					_model = new MenuModel ();
				return _model;
			}
		}

		private MenuModel () : base ("crm", "menu")
		{
		}

		/// <summary>
		/// 自定义菜单json
		/// </summary>
		/// <returns>The menu date.</returns>
		public string createMenuDate()
		{
			StringBuilder postData = new StringBuilder ("{" + "\r\n");
			StringBuilder lever1 = new StringBuilder ();
			StringBuilder lever2 = new StringBuilder ();
			MenuVO [] menuVO = MenuMapper.I.DireList("0");
			postData.Append ("\"button\":[ " + "\r\n");
			foreach (MenuVO m in menuVO) {
				lever1.Append ("{ \r\n");
				lever1.Append(string.Format("\"type\":\"{0}\","+ "\r\n",m.type));
				lever1.Append (string.Format ("\"name\":\"{0}\"," + "\r\n", m.name));
				lever1.Append (string.Format ("\"key\":\"{0}\"," + "\r\n", m.key));
				lever1.Append("\"sub_button\": [");
				MenuVO[] menuVO1 = MenuMapper.I.DireList(m.menuId); 
				foreach (MenuVO m1 in menuVO1) {
					lever2.Append ("{ \r\n");
					lever2.Append (string.Format("\"type\": \"{0}\",",m1.type));
					lever2.Append (string.Format ("\"name\": \"{0}\",\n",m1.name));
					lever2.Append (string.Format ("\"url\": \"{0}\"\n", m1.key));
					lever2.Append ("}, \r\n");
				}
				string con=lever2.ToString();
				string con1 = con.Substring (0, con.Length - 3);
				lever2.Clear ();
				lever1.Append (con1);
				lever1.Append("]\r\n");
				lever1.Append ("}, \r\n");
			}
			string main = lever1.ToString ();
			string main1 = main.Substring (0, main.Length - 3);
			postData.Append (main1);
			postData.Append( "] \r\n");
			postData.Append("} \r\n") ; 
			return postData.ToString();
		} 
	}
}