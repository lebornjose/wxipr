using System;
using System.Text;
using System.Collections.Generic;
using Jabinfo.Crm.VO;
using Jabinfo.Crm.Model;

namespace Jabinfo.Crm
{
	public class API
	{
		private static API _Instance;
		public static API Instance { get { if (_Instance == null)_Instance = new API(); return _Instance; } }
		private API()
		{
		}

		/// <summary>
		/// 递归获取分类树形结构
		/// </summary>
		/// <param name="parent_id">父级编号</param>
		/// <param name="i">input显示偏移位计算</param>
		/// <returns></returns>
		public string categoryOption(string parent_id, int i)
		{
			StringBuilder stringBuilder = new StringBuilder(1500);
			CategoryVO[] categorys = this.group(parent_id);
			if (categorys == null)
				return string.Empty;
			i = i + 1;
			foreach (CategoryVO category in categorys)
			{
				if (i == 1)
				{
					stringBuilder.AppendFormat("<li class=\"jibie{0}\">", i);
				}
				else
				{
					stringBuilder.AppendFormat("<li style=\"display:none\" class=\"cls{0} jibie{1}\">", category.parentId, i);
				}
				for (int j = 1; j < i; j++)
				{
					stringBuilder.Append("&nbsp;&nbsp;");
				}
				stringBuilder.AppendFormat("<input type=\"checkbox\" name=\"categorys\" onclick=\"setTag(this,{2},{3})\" value=\"{0}\"  style=\"width:30px;\" />&nbsp;<span>{1}</span></li>", category.categoryId, category.title, i, category.parentId);
				if (category.categoryId != "0")
				{
					stringBuilder.Append(categoryOption(category.categoryId, i));
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// 获取一组分类
		/// </summary>
		/// <param name="parent_id">父编号</param>
		/// <returns></returns>
		public CategoryVO[] group(string parent_id)
		{
			return CategoryMapper.I.Select1 (parent_id);
		}
	}
}

