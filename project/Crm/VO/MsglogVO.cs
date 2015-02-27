/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 21:03:15
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 消息推送记录
	/// </summary>
	[Serializable]
	public class MsglogVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  msgId  { get; set; }
    	
		
        /// <summary>
        /// 错误记录
        ///</summary>
        public String  errmsg  { get; set; }
    	
		
        /// <summary>
        /// 返回编号
        ///</summary>
        public String  errcode  { get; set; }
    	
		
	}
}