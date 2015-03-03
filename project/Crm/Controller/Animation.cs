/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/3/2 8:44:09
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class AnimationController : Jabinfo.JabinfoController
	{
		#region Constructor
		public AnimationController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = AnimationMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["animationList"] = AnimationMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["animationId"] = Jabinfo.Help.Basic.JabId;
			if (context.Files["image"] != null && context.Files["image"].ContentLength > 10)
			{
				Jabinfo.Help.Image.Save(context.Post["animationId"], context.Files["image"]);
				JabinfoKeyValue sizes = Jabinfo.Help.Config.Get("article.photosize");
				foreach (string key in sizes.Keys)
				{
					string[] size = sizes[key].Split('x');
					Jabinfo.Help.Image.Resize(string.Format("{0}_{1}", context.Post["animationId"], key), context.Post["animationId"], Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
				}
			}
			AnimationMapper.I.Insert(context.Post);
			context.Jump ("/crm/animation/home", "添加成功");
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String animationId)
		{
			if (!context.IsPost) {
				context.Variable ["animation"] = AnimationMapper.I.Create (animationId);
				return;
			}
			if (context.Files["image"] != null && context.Files["image"].ContentLength > 10)
			{
				Jabinfo.Help.Image.Save(context.Post["animationId"], context.Files["image"]);
				JabinfoKeyValue sizes = Jabinfo.Help.Config.Get("article.photosize");
				foreach (string key in sizes.Keys)
				{
					string[] size = sizes[key].Split('x');
					Jabinfo.Help.Image.Resize(string.Format("{0}_{1}", context.Post["animationId"], key), context.Post["animationId"], Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
				}
			}
			AnimationMapper.I.UpdateByPrimary(context.Post);
			context.Jump ("/crm/animation/home", "编辑成功");
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String animationId)
		{
			AnimationMapper.I.DeleteByPrimary (animationId);
			context.Refresh ();
		}
		#endregion
	}
}