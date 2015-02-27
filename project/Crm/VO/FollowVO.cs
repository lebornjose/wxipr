/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 17:24:15
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 关注者列表
	/// </summary>
	[Serializable]
	public class FollowVO
	{
		
        /// <summary>
        /// 唯一标示
        ///</summary>
        public String  openId  { get; set; }
    	
		
        /// <summary>
        /// 昵称
        ///</summary>
        public String  nickname  { get; set; }
    	
		
        /// <summary>
        /// 性别
        ///</summary>
        public String  sex  { get; set; }
    	
		
        /// <summary>
        /// 省份
        ///</summary>
        public String  province  { get; set; }
    	
		
        /// <summary>
        /// 城市
        ///</summary>
        public String  city  { get; set; }
    	
		
        /// <summary>
        /// 国家
        ///</summary>
        public String  country  { get; set; }
    	
		
        /// <summary>
        /// 图像连接
        ///</summary>
        public String  headimgurl  { get; set; }
    	
		
        /// <summary>
        /// 分组
        ///</summary>
        public String  group  { get; set; }
    	
		
        /// <summary>
        /// 创建时间
        ///</summary>
        public Int32  createtime  { get; set; }
    	
		
	}
}