using System;

namespace Jabinfo
{
	public class WXMusic
	{
		// 回复音乐消息
		private string _Title;
		public string Title
		{
			get{return _Title;}
			set{_Title=value;}
		}

		private string _Description;
		public string Description
		{  
			get{return _Description;}
			set{_Description = value;}
		}

		private string _MusicUrl;
		public string MusicUrl
		{
			get{return _MusicUrl;}
			set{_MusicUrl = value;}
		}

		private string _HQMusicUrl;
		public string HQMusicUrl
		{
			get{return _HQMusicUrl;}
			set{_HQMusicUrl = value;}
		}
	}
}

