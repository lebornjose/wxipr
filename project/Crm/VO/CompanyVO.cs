/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/7 15:49:15
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 企业管理
	/// </summary>
	[Serializable]
	public class CompanyVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  conpanyId  { get; set; }
    	
		
        /// <summary>
        /// 名称
        ///</summary>
        public String  name  { get; set; }
    	
		
        /// <summary>
        /// 所在城市
        ///</summary>
        public String  city  { get; set; }
    	
		
        /// <summary>
        /// 添加时间
        ///</summary>
        public Int32  date  { get; set; }
    	
		
        /// <summary>
        /// 地址
        ///</summary>
        public String  address  { get; set; }
    	
		
	}
}