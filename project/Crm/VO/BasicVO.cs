/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 10:09:49
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 基本信息
	/// </summary>
	[Serializable]
	public class BasicVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  basicId  { get; set; }
    	
		
        /// <summary>
        /// 文章标题
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 版权
        ///</summary>
        public String  copyright  { get; set; }
    	
		
        /// <summary>
        /// 邮箱
        ///</summary>
        public String  email  { get; set; }
    	
		
        /// <summary>
        /// 地址
        ///</summary>
        public String  address  { get; set; }
    	
		
        /// <summary>
        /// 传真
        ///</summary>
        public String  pro  { get; set; }
    	
		
        /// <summary>
        /// 电话
        ///</summary>
        public String  tel  { get; set; }
    	
		
	}
}