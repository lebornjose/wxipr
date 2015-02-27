/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:17:47
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 报名
	/// </summary>
	[Serializable]
	public class RegVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  regId  { get; set; }
    	
		
        /// <summary>
        /// 兼职id
        ///</summary>
        public String  jobId  { get; set; }
    	
		
        /// <summary>
        /// 用户名
        ///</summary>
        public String  username  { get; set; }
    	
		
        /// <summary>
        /// 报民时间
        ///</summary>
        public Int32  addtime  { get; set; }
    	
		
        /// <summary>
        /// 状态
        ///</summary>
        public String  status  { get; set; }
    	
		
	}
}