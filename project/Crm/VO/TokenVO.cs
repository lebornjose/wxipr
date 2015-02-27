/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/10 17:08:30
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// accessToken
	/// </summary>
	[Serializable]
	public class TokenVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  tokenId  { get; set; }
    	
		
        /// <summary>
        /// 唯一标示
        ///</summary>
        public String  value  { get; set; }
    	
		
        /// <summary>
        /// 更新时间
        ///</summary>
        public Int32  date  { get; set; }
    	
		
	}
}