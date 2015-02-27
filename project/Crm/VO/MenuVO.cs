/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 9:36:40
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 自定义菜单
	/// </summary>
	[Serializable]
	public class MenuVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  menuId  { get; set; }
    	
		
        /// <summary>
        /// 类型
        ///</summary>
        public String  type  { get; set; }
    	
		
        /// <summary>
        /// 名称
        ///</summary>
        public String  name  { get; set; }
    	
		
        /// <summary>
        /// 动作
        ///</summary>
        public String  key  { get; set; }
    	
		
        /// <summary>
        /// 父级
        ///</summary>
        public String  parentId  { get; set; }
    	
		
	}
}