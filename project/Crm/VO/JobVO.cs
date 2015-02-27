/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/2/5 15:54:09
 */
using System;

namespace Jabinfo.Crm.VO
{
	/// <summary>
	/// 兼职表
	/// </summary>
	[Serializable]
	public class JobVO
	{
		
        /// <summary>
        /// 编号
        ///</summary>
        public String  jobId  { get; set; }
    	
		
        /// <summary>
        /// 标题名称
        ///</summary>
        public String  title  { get; set; }
    	
		
        /// <summary>
        /// 工作区域
        ///</summary>
        public String  region  { get; set; }
    	
		
        /// <summary>
        /// 查看次数
        ///</summary>
        public Int32  reads  { get; set; }
    	
		
        /// <summary>
        /// 性别要求
        ///</summary>
        public String  sex  { get; set; }
    	
		
        /// <summary>
        /// 身高要求
        ///</summary>
        public String  height  { get; set; }
    	
		
        /// <summary>
        /// 报名截止日期
        ///</summary>
        public Int32  overtime  { get; set; }
    	
		
        /// <summary>
        /// 工作内容
        ///</summary>
        public String  context  { get; set; }
    	
		
        /// <summary>
        /// 集合时间
        ///</summary>
        public String  jTime  { get; set; }
    	
		
        /// <summary>
        /// 集合地点
        ///</summary>
        public String  jAddress  { get; set; }
    	
		
        /// <summary>
        /// 联系电话
        ///</summary>
        public String  mobile  { get; set; }
    	
		
        /// <summary>
        /// 特殊要求
        ///</summary>
        public String  spec  { get; set; }
    	
		
        /// <summary>
        /// 工作备注
        ///</summary>
        public String  note  { get; set; }
    	
		
        /// <summary>
        /// 工作类型
        ///</summary>
        public String  type  { get; set; }
    	
		
        /// <summary>
        /// 类型
        ///</summary>
        public String  jtype  { get; set; }
    	
		
        /// <summary>
        /// 要求人数
        ///</summary>
        public Int32  people  { get; set; }
    	
		
        /// <summary>
        /// 状态
        ///</summary>
        public String  status  { get; set; }
    	
		
        /// <summary>
        /// 工资发放
        ///</summary>
        public String  price  { get; set; }
    	
		
        /// <summary>
        /// 所在城市
        ///</summary>
        public String  city  { get; set; }
    	
		
        /// <summary>
        /// 其他
        ///</summary>
        public String  extend  { get; set; }
    	public JabinfoKeyValue extendKV{ get { return Jabinfo.Help.Basic.ToJKV(extend); } }
		
	}
}