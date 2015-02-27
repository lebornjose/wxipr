/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:24:43
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 分类列表
	/// </summary>
	[Serializable]
	public class CategoryVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  categoryId  { get; set; }
    	
		
        /// <summary>
        /// 名称
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 父级
        ///</summary>
        public String  parentId  { get; set; }
    	
		
        /// <summary>
        /// 排序
        ///</summary>
        public Int32  index  { get; set; }
    	
		
        /// <summary>
        /// 子标签
        ///</summary>
        public String  childen  { get; set; }
    	
		
        /// <summary>
        /// 分页
        ///</summary>
        public Int32  pagesize  { get; set; }
    	
		
        /// <summary>
        /// 关键字
        ///</summary>
        public String  keyword  { get; set; }
    	
		public String describe{ get; set;}
	}
}