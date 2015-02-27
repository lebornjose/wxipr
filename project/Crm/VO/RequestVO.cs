/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 20:03:26
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 接收消息
	/// </summary>
	[Serializable]
	public class RequestVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  requestId  { get; set; }
    	
		
        /// <summary>
        /// 开发者微信号
        ///</summary>
        public String  toUserName  { get; set; }
    	
		
        /// <summary>
        ///  发送房Id
        ///</summary>
        public String  fromUserName  { get; set; }
    	
		
        /// <summary>
        /// 创建时间
        ///</summary>
        public Int32  createTime  { get; set; }
    	
		
        /// <summary>
        /// 消息类型
        ///</summary>
        public String  msgType  { get; set; }
    	
		
        /// <summary>
        /// 内容
        ///</summary>
        public String  content  { get; set; }
    	
		
        /// <summary>
        /// 消息id
        ///</summary>
        public String  msgId  { get; set; }
    	
		
        /// <summary>
        /// 回复
        ///</summary>
        public String  status  { get; set; }
    	
		
        /// <summary>
        /// 回复内容
        ///</summary>
        public String  reply  { get; set; }
    	
		/// <summary>
		/// 位置坐标
		/// </summary>
		private string _Location_X ;
		public string Location_X
		{
			get{return _Location_X;}
			set{_Location_X = value;}
		}

		private string _Location_Y;
		public string Location_Y
		{
			get{return _Location_Y;}
			set{_Location_Y = value;}
		}

		private string _Scale;
		public string Scale
		{
			get{return _Scale;}
			set{_Scale = value;}
		}

		private string _Label;
		public string Label
		{
			get{return _Label;}
			set{_Label = value;}
		}

		private string _PicUrl;
		public string PicUrl
		{
			get{return _PicUrl;}
			set{_PicUrl = value;}
		}

		private string _Title;
		public string Title
		{
			get{return _Title;}
			set{_Title = value;}
		}

		private string _Description;
		public string Description
		{
			get{return _Description;}
			set{_Description = value;}
		}

		private string _Url;
		public string Url
		{
			get{return _Url;}
			set{_Url = value;}
		}

		private string _MediaId;
		public string MediaId
		{
			get{return _MediaId;}
			set{_MediaId = value;}
		}

		private string _Event;
		public string Event
		{
			get{return _Event;}
			set{_Event = value;}
		}

		public string EventKey{ get; set;}
	}
}