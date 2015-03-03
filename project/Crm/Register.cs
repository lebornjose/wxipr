using System;

namespace Jabinfo.Crm
{
	public class Register:JabinfoRegister
	{
		public override Jabinfo.JabinfoController Instance (string name)
		{
			switch (name) {
			case "company":
				return new CompanyController ();
			case "article":
				return new ArticleController ();
			case "articleDetail":
				return new ArticleDetailController ();
			case "category":
				return new CategoryController ();
			case "page":
				return new PageController ();
			case "menu":
				return new MenuController ();
			case "feedback":
				return new FeedbackController ();
			case "follow":        //关注者列表
				return new FollowController ();
			case "upload":       // 媒体文件
				return new UploadController ();
			case "ticket":      //QR二维码
				return new TicketController ();
			case "request":    //消息管理
				return new RequestController ();
			case "msglog":
				return new MsglogController ();
			case "animation":
				return new AnimationController ();
			case "basic":
				return new BasicController ();
			case "home":
				return new Home ();
			case "web":
				return new Web ();
			}
			return null;
		}

		public override Object API()
		{
			return Jabinfo.Crm.API.Instance;
		}
	}
}

