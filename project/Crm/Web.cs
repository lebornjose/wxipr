using System;
using Jabinfo;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo
{
	public class Web:JabinfoController
	{
		public Web ()
		{
		}

		/// <summary>
		/// 首页部分
		/// </summary>
		/// <param name="context">Context.</param>
		public void index(JabinfoContext context)
		{
			context.Variable ["animationList"] = AnimationMapper.I.SelectByPage (0, 8);
		}

		/// <summary>
		/// 文章分类
		/// </summary>
		/// <param name="context">Context.</param>
		public void category(JabinfoContext context,string categoryId,int index)
		{ 
			int size = 20;
			context.Variable ["index"] = index;
			context.Variable ["size"] = size;
			context.Variable ["title"] = CategoryMapper.I.Create (categoryId).title;
			context.Variable ["articleList"] = ArticleModel.I.Category (categoryId, index, size);
			context.Variable ["total"] = ArticleModel.I.Ctotal (categoryId);
		}
         
		/// <summary>
		/// 文章详情
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="articleId">Article identifier.</param>
		public void detail(JabinfoContext context,string articleId)
		{
			context.Variable ["article"] = ArticleMapper.I.Create (articleId);
			context.Variable ["detail"] = ArticleDetailMapper.I.Create (articleId);
		}

		/// <summary>
		/// 阅读排行榜
		/// </summary>
		public void ranking(JabinfoContext context)
		{
			context.Variable ["articleList"] = ArticleMapper.I.Ranking (0, 20);
		}

	    /// <summary>
	    /// 反馈信息
	    /// </summary>
	    /// <param name="context">Context.</param>
		public void feed(JabinfoContext context)
		{
			if (!context.IsPost)
				return;
			context.Post ["feedId"] = Jabinfo.Help.Basic.JabId;
			context.Post ["addtime"] = Jabinfo.Help.Date.Now.ToString ();
			FeedbackMapper.I.Insert (context.Post);
			context.Refresh ();
		}

		/// <summary>
		/// 搜索页面
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="index">Index.</param>
		public void search(JabinfoContext context,int index)
		{
			int size = 20;
			string title = string.Empty;
			if (context.IsPost) {
				title = context.Post ["title"];
				context.Cookie.Add("search", title,1);
			} else {
				title = context.Cookie.Get ("search").ToString();
			}
			context.Variable ["title"] = title;
			context.Variable ["index"] = index;
			context.Variable ["size"] = size;
			context.Variable ["articleList"] = ArticleModel.I.Select (title, index, size);
			context.Variable ["total"] = ArticleModel.I.Total (title);
		}

		/// <summary>
		/// 页面管理
		/// </summary>
		/// <param name="pageId">Page identifier.</param>
		public void page(JabinfoContext context, string pageId)
		{
			context.Variable ["page"] = PageMapper.I.Create (pageId);
		}
	}
}

