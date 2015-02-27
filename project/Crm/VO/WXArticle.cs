using System;

namespace Jabinfo
{
	public class WXArticle
	{

		//图文消息
		private string _Title;

		public string Title
		{
			get{ return _Title;}
			set{_Title = value;}
		}

		private string _Description;
		public string  Description
		{
			get{return _Description;}
			set{_Description = value;}
		}

		private string _PicUrl;
		public string PicUrl
		{
			get{return _PicUrl;}
			set{_PicUrl = value;}
		}

		private string _Url;
		public string Url
		{
			get{return _Url;}
			set{_Url = value;}
		}
	}
}

