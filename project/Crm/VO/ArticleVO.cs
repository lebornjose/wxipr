/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/13 15:19:55
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 列表
	/// </summary>
	[Serializable]
	public class ArticleVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  articleId  { get; set; }
    	
		
        /// <summary>
        /// 标题
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 副标题
        ///</summary>
        public String  caption  { get; set; }
    	
		
        /// <summary>
        /// 作者
        ///</summary>
        public String  author  { get; set; }
    	
		
        /// <summary>
        /// 模板类型
        ///</summary>
        public String  model  { get; set; }
    	
		
        /// <summary>
        /// 发布日期
        ///</summary>
        public Int32  pubtime  { get; set; }
    	
		
        /// <summary>
        /// 文章摘要
        ///</summary>
        public String  summary  { get; set; }
    	
		
        /// <summary>
        /// 阅读次数
        ///</summary>
        public Int32  reads  { get; set; }
    	
		
        /// <summary>
        /// 是否允许评论
        ///</summary>
        public String  ispost  { get; set; }
    	
		
        /// <summary>
        /// 网址
        ///</summary>
        public String  comeUrl  { get; set; }
    	
		
        /// <summary>
        /// 文章来源
        ///</summary>
        public String  come  { get; set; }
    	
		
        /// <summary>
        /// 关键字
        ///</summary>
        public String  keyword  { get; set; }
    	
		
        /// <summary>
        /// 上传人
        ///</summary>
        public String  uid  { get; set; }
    	
		
        /// <summary>
        /// 分类序列
        ///</summary>
        public String  categorys  { get; set; }
    	
		
        /// <summary>
        /// 文章类型
        ///</summary>
        public String  jtype  { get; set; }
    	
		
        /// <summary>
        /// 分类编号
        ///</summary>
        public String  categoryId  { get; set; }
    	
		
        /// <summary>
        /// 是否草稿
        ///</summary>
        public String  iscash  { get; set; }
    	
		
	}
}