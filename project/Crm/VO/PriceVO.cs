/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:11:51
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 工资发放
	/// </summary>
	[Serializable]
	public class PriceVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  jobId  { get; set; }
    	
		
        /// <summary>
        /// 发放人数
        ///</summary>
        public Double  people  { get; set; }
    	
		
        /// <summary>
        /// 金额
        ///</summary>
        public Double  money  { get; set; }
    	
		
        /// <summary>
        /// 发放时间
        ///</summary>
        public Int32  addtime  { get; set; }
    	
		
	}
}