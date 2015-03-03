/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 8:44:09
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 动画管理
	/// </summary>
	[Serializable]
	public class AnimationVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  animationId  { get; set; }
    	
		
        /// <summary>
        /// 标题
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 地址
        ///</summary>
        public String  url  { get; set; }
    	
		
        /// <summary>
        /// 排序
        ///</summary>
        public Int32  index  { get; set; }
    	
		
	}
}