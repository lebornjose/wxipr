/*
 * Created:  by JabinfoCoder
 * Contact:  http://www.jabinfo.com
 * Date   :  2015/1/9 15:27:12
 */
using System;
using Jabinfo.Crm.Model;
using Jabinfo.Crm.VO;

namespace Jabinfo.Crm
{
	class TokenController : Jabinfo.JabinfoController
	{
		#region Constructor
		public TokenController ()
		{
			this.Access = JabinfoRight.Administrator;
		}
		#endregion

		#region home
		public void home (JabinfoContext context, int index)
		{
			int size = 30;
			context.Variable ["total"] = TokenMapper.I.CountAll ();
			context.Variable ["size"] = size;
			context.Variable ["index"] = index;
			context.Variable ["tokenList"] = TokenMapper.I.SelectByPage (index, size);
		}
		#endregion

		#region add
		public void add (JabinfoContext context)
		{
			if (!context.IsPost) {
				return;
			}
			context.Post ["tokenId"] = Jabinfo.Help.Basic.JabId;
			TokenMapper.I.Insert(context.Post);
			context.Refresh ();
		}
		#endregion

		#region edit
		public void edit (JabinfoContext context, String tokenId)
		{
			if (!context.IsPost) {
				context.Variable ["token"] = TokenMapper.I.Create (tokenId);
				return;
			}
			TokenMapper.I.UpdateByPrimary(context.Post);
			context.Refresh ();
		}
		#endregion

		#region remove
		public void remove (JabinfoContext context, String tokenId)
		{
			TokenMapper.I.DeleteByPrimary (tokenId);
			context.Refresh ();
		}
		#endregion
	}
}