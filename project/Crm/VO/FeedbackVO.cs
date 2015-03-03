/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/3 9:53:56
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
        /// 回馈详情
        ///</summary>
        public String  context  { get; set; }
    	
		
        /// <summary>
        /// 添加时间
        ///</summary>
        public Int32  addtime  { get; set; }
    	
		
        /// <summary>
        /// 姓名
        ///</summary>
        public String  name  { get; set; }
    	
		
        /// <summary>
        /// 邮箱
        ///</summary>
        public String  email  { get; set; }
    	
		
        /// <summary>
        /// 电话
        ///</summary>
        public String  mobile  { get; set; }
    	
		
	}
}