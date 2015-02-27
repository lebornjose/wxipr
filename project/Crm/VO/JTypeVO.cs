/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 20:03:34
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 工作类型
	/// </summary>
	[Serializable]
	public class JtypeVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  typeId  { get; set; }
    	
		
        /// <summary>
        /// 名称
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 排序
        ///</summary>
        public Int32  index  { get; set; }
    	
		
	}
}