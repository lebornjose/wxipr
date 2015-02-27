/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 14:04:47
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 媒体文件
	/// </summary>
	[Serializable]
	public class UploadVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  uploadId  { get; set; }
    	
		
        /// <summary>
        /// 名称
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 类型
        ///</summary>
        public String  type  { get; set; }
    	
		
        /// <summary>
        /// 媒体编号
        ///</summary>
        public String  mediaId  { get; set; }
    	
		
        /// <summary>
        /// 添加时间
        ///</summary>
        public Int32  createdAt  { get; set; }
    	
		
	}
}