/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 17:40:01
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 页面管理
	/// </summary>
	[Serializable]
	public class PageVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  pageId  { get; set; }
    	
		
        /// <summary>
        /// 标题
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 摘要
        ///</summary>
        public String  describe  { get; set; }
    	
		
        /// <summary>
        /// 评论
        ///</summary>
        public String  ispost  { get; set; }
    	
		
        /// <summary>
        /// 分类
        ///</summary>
        public String  group  { get; set; }
    	
		
        /// <summary>
        /// 排序
        ///</summary>
        public Int32  index  { get; set; }
    	
		
        /// <summary>
        /// 分类
        ///</summary>
        public String  jtype  { get; set; }
    	
		
        /// <summary>
        /// 内容
        ///</summary>
        public String  content  { get; set; }
    	
		
	}
}