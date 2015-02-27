/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/8 9:17:13
 */
using System;
using System.Text;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;
using System.Text.RegularExpressions;
using System.Web;

namespace Jabinfo.Crm
{
	class ArticleController : Jabinfo.JabinfoController
	{
		#region Constructor
		public ArticleController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = ArticleMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable["start"] = index * size;
			context.Variable ["type"] = Jabinfo.Help.Config.Get ("article.jtype");
			context.Variable ["articleList"] = ArticleMapper.I.SelectByPage (index, size);
		}
		#endregion

		public void option(JabinfoContext context)
		{
			context.Variable ["categorylist"] = CategoryMapper.I.Select1 ("0");
		}

		public void search(JabinfoContext context,int index)
		{
			int size = 30;
			string where = string.Empty;
			if (context.IsPost) {
				string title = context.Post ["title"];
				string categorys = context.Post ["jtype"];
				if (!string.IsNullOrEmpty (title))
					where = string.Format ("and title like '%{0}%'", title);
				if (!string.IsNullOrEmpty (categorys))
					where = string.Format ("{0} and categorys like '%,{1},%'", where, categorys);
				if (!string.IsNullOrEmpty (where)) 
				{
					where = where.Substring (4);
					context.Session.Add ("article_search", where);
				}
			} else {
					where = context.Session.Get ("article_search").ToString ();
			}
			context.Variable ["total"] = ArticleMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable["start"] = index * size;
			context.Variable ["type"] = Jabinfo.Help.Config.Get ("article.jtype");
			context.Variable ["articleList"] = ArticleModel.I.Select(where,index, size);
		}

		/// <summary>
		/// 上传媒体文件
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="articles">Articles.</param>
		public void upload(JabinfoContext context,string articles)
		{
			if (!context.IsPost) {
				context.Variable ["articles"] = articles;
				return;
			}
			Weixin wx = new Weixin ();
			Utils util = new Utils ();
			string[] article =context.Post["articles"].Split (',');
			StringBuilder str = new StringBuilder ("{ \r\n");
			StringBuilder con = new StringBuilder ();
			str.Append (" \"articles\": [ \r\n");
			for (int i = 0; i < article.Length - 1; i++) {
				ArticleVO ar = ArticleMapper.I.Create (article [i]);
				ArticleDetailVO d = ArticleDetailMapper.I.Create (article [i]);
				con.Append("{ \r\n");
				Regex reg = new Regex(@"(?i)</?a\b[^>]*>");    //去掉字符串的<a>标签
				Regex reg1=new Regex(@"<img[^>]*?/>");         //去掉img标签
				Regex reg2=new Regex(@"style=""[^""]*""");     //去掉style标签
				string result = reg.Replace(d.content, "");
				result = reg1.Replace (result, "");
				result = reg2.Replace (result, "");
				string file = Jabinfo.Help.Upload.PysPath (ar.articleId,"jpg");
				string url = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token="+wx.GetAccessToken();
				url = url + "&type=image";
				string json = util.HttpUpload (url, file);
				con.Append (string.Format(" \"thumb_media_id\":\"{0}\", \r\n",wx.GetJsonValue (json,"media_id")));
				con.Append("\"author\":\"" + ar.author.ToString() + "\",");
				con.Append("\"title\":\"" + ar.title.ToString() + "\",");
				con.Append(string.Format(" \"content_source_url\":\"{0}\", \r\n","http://wx.zento.me/article/home/detail/"+ ar.articleId));
				con.Append("\"content\":\"" + Jabinfo.Help.Formate.ClearSpace(result) + "\",");
				con.Append("\"digest\":\"" +  Jabinfo.Help.Formate.ClearSpace(ar.summary) + "\",");
				con.Append (" \"show_cover_pic\":\"0\" \r\n");
				con.Append (" },");
			}
			string mian = con.ToString ();
			string text = mian.Substring (0, mian.Length - 1);
			str.Append (text);
			str.Append ("  ] \r\n");
			str.Append ("} \r\n");
			wx.UpNews (str.ToString (),context.Post["title"]);
			context.Refresh ();
		}



		#region edit
		public void edit (JabinfoContext context, String articleId)
		{
			if (!context.IsPost) {
				context.Variable ["article"] = ArticleMapper.I.Create (articleId);
				context.Variable ["detail"] = ArticleDetailMapper.I.Create (articleId);
				return;
			}
			context.Post ["pubtime"] = Jabinfo.Help.Date.StringToDate (context.Post ["pubtime"]).ToString ();
			//记录图片
			if (context.Files["image"] != null && context.Files["image"].ContentLength > 10)
			{
				Jabinfo.Help.Image.Save(context.Post["article_id"], context.Files["image"]);
				JabinfoKeyValue sizes = Jabinfo.Help.Config.Get("article.photosize");//切图保存
				foreach (string key in sizes.Keys)
				{
					string[] size = sizes[key].Split('x');
					Jabinfo.Help.Image.Resize(string.Format("{0}_{1}", context.Post["article_id"], key), context.Post["article_id"], Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
				}
				context.Post["attach"] = "1";//有预览图
			}
			if (context.Files["afile"] != null && context.Files["afile"].ContentLength > 10)
			{
				string fileName = context.Files["afile"].FileName;
				context.Post["model"] = fileName.Substring(fileName.LastIndexOf('.') + 1);
				Jabinfo.Help.Upload.Save(context.Post["article_id"] + "_file", context.Files["afile"], context.Post["model"]);
			}
			if (context.Post["iscash"] == null)
				context.Post["iscash"] = "0";
			ArticleMapper.I.UpdateByPrimary (context.Post);
			context.Post["categorys"] = context.Post["category_id"];
			context.Post["tags"] = context.Post["tag_id"];
			ArticleDetailMapper.I.UpdateByPrimary(context.Post);
			context.Jump(string.Format("/crm/article/edit/{0}", context.Post["articleId"]), "编辑成功");
			return;
		}
		#endregion


		public void category(JabinfoContext context,string articleId)
		{
			ArticleVO article = null;
			if (!context.IsPost) {
				string[] categorys = new String[0];
				article = ArticleMapper.I.Create(articleId);
				if (!string.IsNullOrEmpty(article.categorys))
					categorys =article.categorys.Split(',');
				string initTag = string.Empty;
				for (int i = 1; i < categorys.Length-1; i++)
				{
					CategoryVO categoryVO = CategoryMapper.I.Create(categorys[i]);
					initTag = string.Format("{0}<li id=\"{1}\"><strong>{2}</strong>&nbsp;", initTag, categoryVO.categoryId, categoryVO.title);
					initTag = string.Format("{0}</li>", initTag);
				}
				context.Variable["initTag"] = initTag;
			//	context.Variable["top_id"] = Jabinfo.Help.Config.Get("core.top", "article");
				context.Variable["article"] =ArticleMapper.I.Create(articleId);
				context.Variable["detail"] = article;
				context.Variable["articleId"] = articleId;
				return;
			}
			article =ArticleMapper.I.Create(context.Post["articleId"]);
			JabinfoKeyValue data = new JabinfoKeyValue();
			data["articleId"] = context.Post["articleId"];
			data["categorys"] = string.IsNullOrEmpty(context.Post["categorys"]) ? "" : string.Format(",{0},", context.Post["categorys"]);
			ArticleDetailMapper.I.UpdateByPrimary (data);
            if (article.iscash == "0" && !string.IsNullOrEmpty(data["categorys"]))
			{
				string[] lt = data["categorys"].Split(',');
				data["categoryId"] = lt[lt.Length - 2];
				ArticleMapper.I.UpdateByPrimary(data);
			}
			context.Alert("保存成功");
			return;
		}

		/// <summary>
		/// 文章批量删除
		/// </summary>
		/// <param name="articles">Articles.</param>
		public void batch(JabinfoContext context,string articles)
		{
			string[] article = articles.Split (',');
			for (int i = 0; i < article.Length - 1; i++) {
				ArticleVO article1 = ArticleMapper.I.Create (article [i]);
				//文章进入回收站
				Jabinfo.JabinfoLoger.DeleteVO (article1.title, article1.articleId, "article_del", context.Role.Uid, article);
				ArticleMapper.I.DeleteByPrimary (article [i]);
			}
			context.End ();
		}

		/// <summary>
		/// 分类
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="categoryId">Category identifier.</param>
		/// <param name="index">Index.</param>
		public void index(JabinfoContext context,string categoryId,int index)
		{
			int size = 30;
     		context.Variable ["total"] = ArticleMapper.I.Ctotal (categoryId);
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["start"] = index * size;
  			context.Variable ["articleList"] = ArticleMapper.I.Select1 (categoryId,index,size);
			context.Variable["category_id"] = categoryId;
    		context.Variable ["category"] = CategoryMapper.I.Create (categoryId);
			context.Variable["articletype"] = Jabinfo.Help.Config.Get("article.jtype");
		}

		#region add
		public void add (JabinfoContext context,string categoryId)
		{
			if (!context.IsPost) {
				context.Variable["categoryId"] = categoryId;
				context.Variable ["now"] = Jabinfo.Help.Date.Now;
				return;
			}
			context.Post ["articleId"] = Jabinfo.Help.Basic.JabId;
			context.Post ["pubtime"] = Jabinfo.Help.Date.StringToDate (context.Post ["pubtime"]).ToString ();
			context.Post ["uid"] = context.Role.Uid;
			//上传图片
			if (context.Files["image"] != null && context.Files["image"].ContentLength > 10)
			{
				Jabinfo.Help.Image.Save(context.Post["articleId"], context.Files["image"]);
				JabinfoKeyValue sizes = Jabinfo.Help.Config.Get("article.photosize");
				foreach (string key in sizes.Keys)
				{
					string[] size = sizes[key].Split('x');
					Jabinfo.Help.Image.Resize(string.Format("{0}_{1}", context.Post["articleId"], key), context.Post["articleId"], Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
				}
				context.Post["attach"] = "1";//有预览图
			}
			else
			{
				context.Post["attach"] = "0";
			}
			//上传文件
			if (context.Files["afile"] != null && context.Files["afile"].ContentLength > 10)
			{
				string fileName = context.Files["afile"].FileName;
				context.Post["model"] = fileName.Substring(fileName.LastIndexOf('.') + 1);
				Jabinfo.Help.Upload.Save(context.Post["article_id"] + "_file", context.Files["afile"], context.Post["model"]);
			}
			if (string.IsNullOrEmpty(context.Post["summary"]))
			{
				string content = Jabinfo.Help.Formate.HtmlClear(context.Post["content"]);
				if (content.Length < 200)
					context.Post["summary"] = content;
				else
					context.Post["summary"] = content.Substring(0, 200);
			}
			ArticleMapper.I.Insert(context.Post);
			ArticleDetailMapper.I.Insert(context.Post);
			if (context.Post ["category_id"] == string.Empty) {
				context.Jump (string.Format ("/article/article/category/{0}", context.Post ["articleId"]), "添加成功,请设置文章分类");
				return;
			} 
			else {
				context.Jump(string.Format("article/article/edit/{0}", context.Post["articleId"]),"添加成功");
				return;
			}
		}
		#endregion

		public void recycle(JabinfoContext context,int index)
		{
			int size = 30;
			context.Variable ["index"] = index;
			context.Variable ["size"] = size;
			context.Variable ["total"] = JabinfoLoger.Count ("logtype='article_del'");
//			context.Variable["logs"]= JabinfoLoger(index, 30, "logtype='article_del'",context.Role.Uid);
		}

		/// <summary>
		/// 文章回收站恢复
		/// </summary>
		/// <param name="logId">Log identifier.</param>
		public void restore(JabinfoContext context, string logId)
		{
			object result = JabinfoLoger.Restore (logId, typeof(ArticleVO));
			if (result == null)
				context.Alert ("恢复失败");
			else
				context.Alert ("恢复成功");
		}
			
		/// <summary>
		/// 将文章移入回收站
		/// </summary>
		/// <param name="article_id"></param>
		public void remove(JabinfoContext context,string articleId)
		{
			ArticleVO article = ArticleMapper.I.Create(articleId);
			//文章进入回收站
			Jabinfo.JabinfoLoger.DeleteVO(article.title, article.articleId, "article_del", context.Uid, article);
			ArticleMapper.I.DeleteByPrimary(articleId);
			context.Print("删除成功！");
			context.End ();
		}

		/// <summary>
		/// 回收站彻底删除
		/// </summary>
		/// <param name="article_id"></param>
		/// <param name="logid"></param>
		public void clear(string articleId, string logid)
		{
			ArticleDetailMapper.I.DeleteByPrimary (articleId);
			//删除文章附件
			JabinfoLoger.Delete(logid);
		}
	}
}

