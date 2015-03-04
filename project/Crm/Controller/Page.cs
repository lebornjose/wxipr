/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 11:11:01
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class PageController : Jabinfo.JabinfoController
	{
		#region Constructor
		public PageController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = PageMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["status"] = Jabinfo.Help.Config.Get ("article.post");
			context.Variable ["pageList"] = PageMapper.I.SelectByPage (index, size);
		}
		#endregion

		public void set_post(JabinfoContext context,string pageId)
		{
			JabinfoKeyValue data = new JabinfoKeyValue ();
			PageVO pageVO = PageMapper.I.Create (pageId);
			data ["pageId"] = pageId;
			if (pageVO.ispost == "0")
				data ["ispost"] = "1";
			if(pageVO.ispost=="1")
				data["ispost"]="0";
			PageMapper.I.UpdateByPrimary (data);
			context.Refresh ();
			return;
		}


		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("5");
				return;
			}
			context.Post ["pageId"] = Jabinfo.Help.Basic.JabId;
			if (string.IsNullOrEmpty (context.Post ["describe"])) {
				string content = Jabinfo.Help.Formate.HtmlClear(context.Post["content"]);
				if (content.Length < 360)
					context.Post["describe"] = content;
				else
					context.Post["describe"] = content.Substring(0, 360);
			}
			if(context.Files["file_20"].ContentLength > 10)
			{
				Jabinfo.Help.Image.Save(context.Post ["pageId"], context.Files["file"]);
			}
			PageMapper.I.Insert(context.Post);
			context.Jump("/crm/page/home","添加成功");
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String pageId)
		{
			if (!context.IsPost) {
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("5");
				context.Variable ["page"] = PageMapper.I.Create (pageId);
				return;
			}
			if (string.IsNullOrEmpty (context.Post ["describe"])) {
				string content = Jabinfo.Help.Formate.HtmlClear(context.Post["content"]);
				if (content.Length < 360)
					context.Post["describe"] = content;
				else
					context.Post["describe"] = content.Substring(0, 360);
			}
			if(context.Files["file_20"].ContentLength > 10)
			{
				Jabinfo.Help.Image.Save(context.Post ["pageId"], context.Files["file"]);
			}
			PageMapper.I.UpdateByPrimary(context.Post);
			context.Jump(string.Format("/crm/page/edit/{0}",context.Post["pageId"]),"编辑成功");
			return;
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String pageId)
		{
			PageMapper.I.DeleteByPrimary (pageId);
			context.Refresh ();
		}
		#endregion
	}
}