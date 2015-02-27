/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 20:59:17
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 建议回馈
	/// </summary>
	[Serializable]
	public class FeedbackVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  feedId  { get; set; }
    	
		
        /// <summary>
        /// 标题
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 回馈详情
        ///</summary>
        public String  context  { get; set; }
    	
		
        /// <summary>
        /// 用户名
        ///</summary>
        public String  username  { get; set; }
    	
		
        /// <summary>
        /// 状态
        ///</summary>
        public String  status  { get; set; }
    	
		
        /// <summary>
        /// 添加时间
        ///</summary>
        public Int32  addtime  { get; set; }
    	
		
	}
}