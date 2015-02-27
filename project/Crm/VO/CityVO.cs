/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:44:22
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 城市管理
	/// </summary>
	[Serializable]
	public class CityVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  cityId  { get; set; }
    	
		
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