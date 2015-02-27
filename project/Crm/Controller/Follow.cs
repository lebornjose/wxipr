/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/12 10:31:46
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class FollowController : Jabinfo.JabinfoController
	{
		#region Constructor
		public FollowController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion


		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = FollowMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Functions.Add ("category", this.category);
			context.Variable ["followList"] = FollowMapper.I.SelectByPage (index, size);
		}

		public string category(object [] args)
		{
			string result = string.Empty;
			string categoryId = Convert.ToString (args [0]);
			CategoryVO categoryVO = CategoryMapper.I.Create (categoryId);
			if (string.IsNullOrEmpty(categoryId))
				result = "默认分组";
			else
				result = categoryVO.title;
			return result;
		}
		#endregion

		public void group(JabinfoContext context, string openid)
		{
			Weixin wx = new Weixin ();
			if (!context.IsPost) {
				context.Variable ["openid"] = openid;
				context.Variable ["categoryList"] = CategoryMapper.I.Select1 ("8");
				return;
			}
			string url = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token=" + wx.GetAccessToken ();
			string json = "{\"openid\":\"" + openid + "\",\"to_groupid\":" + context.Post ["group"] + "}";
			Jabinfo.Help.Http.PostHttps (url, json);
			FollowMapper.I.UpdateByPrimary (context.Post);
			context.Refresh ();
		}

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["openId"] = Jabinfo.Help.Basic.JabId;
			FollowMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String openId)
		{
			if (!context.IsPost) {
				context.Variable ["follow"] = FollowMapper.I.Create (openId);
				return;
			}
			FollowMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String openId)
		{
			FollowMapper.I.DeleteByPrimary (openId);
			context.Refresh ();
		}
		#endregion
	}
}