/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 10:07:31
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;
using System.Collections.Generic;
using System.IO;

namespace Jabinfo.Crm
{
	class CategoryController : Jabinfo.JabinfoController
	{
		#region Constructor
		public CategoryController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, string parentId)
		{
			string parent_title = "顶级分类";
			string last_id = parentId;
			string currnt_title = "";
			string top_id = string.Empty;
			if (parentId.Length > 2)
			{
				CategoryVO categoryVO = CategoryMapper.I.Create (parentId);
				if (categoryVO.categoryId != null)
				{
					currnt_title = categoryVO.title;
					last_id = categoryVO.parentId;
					CategoryVO lastVO = CategoryMapper.I.Create(last_id);
					parent_title = lastVO.title;
				}
				top_id = topid(categoryVO);
			}
			else
			{
				top_id =parentId;
			}
			context.SetView (string.Format("home{0}",top_id));
			context.Variable ["categoryList"] = CategoryMapper.I.Select1 (parentId);
			context.Variable["parentId"] = parentId;
			context.Variable["lastId"] = last_id;
			context.Variable["currnt_title"] = currnt_title;
			context.Variable["parent_title"] = parent_title;
		}
		#endregion

		/// <summary>
		/// 通过递归找到顶级分类id
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		private string topid(CategoryVO category)
		{
			if (category.parentId.Length < 2)
				return category.parentId;
			CategoryVO parent =CategoryMapper.I.Create(category.parentId);
			return topid(parent);
		}
			
		#region add
		public void add (JabinfoContext context,string parentId)
		{
			if (context.IsPost) {
				string keyword = context.Post ["keyword"].Trim ();
				context.Post ["categoryId"] = Jabinfo.Help.Basic.AutoId ("cat_tag");
				foreach (string name in context.Files.AllKeys) {
					if (context.Files [name].ContentLength > 5) {
						System.Web.HttpPostedFile temp = context.Files [name];
						Jabinfo.Help.Image.Save (string.Format ("{0}_{1}", context.Post ["categoryId"], name.Split ('_') [1]), context.Files [name]);
					}
				}
				if (context.Post ["parentId"].Length > 1) {
					CategoryVO categoryVO = CategoryMapper.I.Create (context.Post ["parentId"]);
					if (categoryVO.categoryId == null) {
						context.Jump ("/crm/category/home/" + parentId, "添加失败，不存在父级分类");
						return;
					}
					if (categoryVO.childen == "0") {
						JabinfoKeyValue data = new JabinfoKeyValue ();
						data ["categoryId"] = context.Post ["parentId"];
						data ["childen"] = "1";
						CategoryMapper.I.UpdateByPrimary (data);
					}
					JabinfoKeyValue cate_Data = new JabinfoKeyValue ();
					context.Post ["childen"] = (Convert.ToInt32 (categoryVO.childen) + 1).ToString ();
				}
				CategoryMapper.I.Insert (context.Post);
				context.Jump ("/crm/category/home/" + parentId, "添加成功");
				return;
			}
			context.Variable["parentId"] = parentId;
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String categoryId)
		{
			if (categoryId == null)
			{
				CategoryVO category = CategoryMapper.I.Create(context.Post["categoryId"]);
				string keyword = context.Post["keyword"].Trim();
				if (keyword.Contains(",") || keyword == "")
				{
					CategoryMapper.I.UpdateByPrimary(context.Post);
					foreach (string name in context.Files.AllKeys)
					{
						if (context.Files[name].ContentLength > 5)
						{
							Jabinfo.Help.Image.Save(string.Format("{0}_{1}", context.Post["categoryId"], name.Split('_')[1]), context.Files[name]);
						}
					}
					context.Jump("crm/category/home/"+category.parentId, "修改成功");
					return;
				}
				else
				{
					context.Jump("crm/category/home/"+category.parentId, "关键字请用逗号隔开");
					return;
				}
			}
			context.Variable["category"] = CategoryMapper.I.Create(categoryId);
		}
		#endregion

		/// <summary>
		/// 微信添加用户分组
		/// </summary>
		/// <param name="">.</param>
		public void addGroup(JabinfoContext context)
		{
			if (!context.IsPost)
				return;
			Weixin wx = new Weixin ();
			string url = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token=" + wx.GetAccessToken ();
			string groupname = "{\"group\":{\"name\":\"" + context.Post["title"] + "\"}}";
			string json = Jabinfo.Help.Http.PostHttps (url, groupname);
			context.Post ["categoryId"] = wx.GetJsonValue (json, "id");
			context.Post ["name"] = wx.GetJsonValue (json, "name");
			context.Post ["parentId"]="8";
			CategoryMapper.I.Insert (context.Post);
			context.Refresh ();
		}

		/// <summary>
		/// 查看分组
		/// </summary>
		/// <param name="context">Context.</param>
		public void Getgroup(JabinfoContext context)
		{
			Weixin wx = new Weixin ();
			Utils Util = new Utils ();
			string url = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token="+wx.GetAccessToken();

			string json = Jabinfo.Help.Http.GetHttps(url);
			var dict = Util.JsonTo<Dictionary<string, List<Dictionary<string, object>>>>(json);
			var gs = new Groups();
			var gilist = dict["groups"];
			foreach (var gidict in gilist)
			{
				var gi = new GroupVO();
				gi.name = gidict["name"].ToString();
				gi.id = Convert.ToInt32(gidict["id"]);
				gi.count = Convert.ToInt32(gidict["count"]);
				gs.Add(gi);
			}
			context.Variable ["groupList"] = gs;
		}


		#region remove
		public void remove (JabinfoContext context, String categoryId)
		{
			CategoryMapper.I.DeleteByPrimary (categoryId);
			context.Refresh ();
		}
		#endregion
	}
}