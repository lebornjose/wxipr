/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 16:02:18
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 文章详情
	/// </summary>
	[Serializable]
	public class ArticleDetailVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  articleId  { get; set; }
    	
		
        /// <summary>
        /// 详情
        ///</summary>
        public String  content  { get; set; }
    	
		
        /// <summary>
        /// 分类序列
        ///</summary>
        public String  categorys  { get; set; }
    	
		
	}
}