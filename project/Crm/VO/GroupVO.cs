using System;
using System.Collections.Generic;

namespace Jabinfo
{
	public class GroupVO
	{
		/// <summary>
		/// 分组id，由微信分配
		/// </summary>
		public int id { get; set; }
		/// <summary>
		/// 分组名字，UTF8编码
		/// </summary>
		public string name { get; set; }
		/// <summary>
		/// 分组内用户数量
		/// </summary>
		public int count { get; set; }

	}

	public class Groups : List<GroupVO>
	{
		public string error { get; set; }
	}

	public class GroupID
	{
		public int id { get; set; }
		public string error { get; set; }
	}
}

