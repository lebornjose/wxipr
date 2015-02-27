/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:25:30
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	///  学校管理
	/// </summary>
	[Serializable]
	public class SchoolVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  schoolId  { get; set; }
    	
		
        /// <summary>
        /// 城市编号
        ///</summary>
        public String  cityId  { get; set; }
    	
		
        /// <summary>
        /// 名称
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 地址
        ///</summary>
        public String  address  { get; set; }
    	
		
        /// <summary>
        /// 负责人
        ///</summary>
        public String  person  { get; set; }
    	
		
        /// <summary>
        /// 负责人电话
        ///</summary>
        public String  mobile  { get; set; }
    	
		
        /// <summary>
        /// 排序
        ///</summary>
        public Int32  index  { get; set; }
    	
		
	}
}