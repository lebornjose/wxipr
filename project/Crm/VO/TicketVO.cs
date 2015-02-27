/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:45:34
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// QR二维码
	/// </summary>
	[Serializable]
	public class TicketVO
	{
		
        /// <summary>
        /// 密文
        ///</summary>
        public String  ticket  { get; set; }
    	
		
        /// <summary>
        /// 链接
        ///</summary>
        public String  url  { get; set; }
    	
		
        /// <summary>
        /// 创建时间
        ///</summary>
        public Int32  createtime  { get; set; }
    	
		
        /// <summary>
        /// 有效时长
        ///</summary>
        public Int32  expireSeconds  { get; set; }
    	
		
        /// <summary>
        /// 标题
        ///</summary>
        public String  title  { get; set; }
    	
		
	}
}