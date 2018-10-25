using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.UserService
{
    public partial class Feedback : UCPageBase
    {
        protected string pageInfo = string.Empty;           // 页信息

        #region 继承属性

        protected override bool IsAuthenticatedUser
        {
            get
            {
                return true;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFeedBackData();
            }
        }

        private void BindFeedBackData()
        {
            int sPageSize = anpPage.PageSize;

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetFeedbacklist(PageIndex, sPageSize, Fetch.GetUserCookie().UserID);
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptFeedBackList.DataSource = pagerSet.PageSet;
                rptFeedBackList.DataBind();
                litMessage.Visible = false;
            }
            else
            {
                litMessage.Visible = true;
            }

            //处理分页
            pageInfo = string.Format("<span class=\"ui-orange\">{0}</span><span class=\"ui-gray\">/</span><span class=\"ui-white\">{1}</span>", PageIndex, anpPage.PageCount);
            if (anpPage.PageCount == 1)
            {
                preLink.Attributes.Add("class", "ui-no-next");
                nextLink.Attributes.Add("class", "ui-no-next");
                return;
            }
            if (PageIndex == 1)
            {
                nextLink.Attributes.Add("href", TextUtility.SetQueryValueReturnUrl("page", (PageIndex + 1).ToString()));
                preLink.Attributes.Add("class", "ui-no-next");
                return;
            }
            if (PageIndex == anpPage.PageCount)
            {
                preLink.Attributes.Add("href", TextUtility.SetQueryValueReturnUrl("page", (PageIndex - 1).ToString()));
                nextLink.Attributes.Add("class", "ui-no-next");
                return;
            }
            preLink.Attributes.Add("href", TextUtility.SetQueryValueReturnUrl("page", (PageIndex - 1).ToString()));
            nextLink.Attributes.Add("href", TextUtility.SetQueryValueReturnUrl("page", (PageIndex + 1).ToString()));
        }

        /// <summary>
        /// 提交留言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPublish_Click(object sender, EventArgs e)
        {
            string accounts = "";
            if (Fetch.GetUserCookie() != null)
            {
                accounts = Fetch.GetUserCookie().Accounts;
            }

            GameFeedbackInfo info = new GameFeedbackInfo();
            info.FeedbackContent = CtrlHelper.GetTextAndFilter(txtContent);
            info.FeedbackTitle = "";
            info.ClientIP = GameRequest.GetUserIP();

            Message msg = FacadeManage.aideNativeWebFacade.PublishFeedback(info, accounts);
            if (msg.Success)
            {
                ShowAndRedirect("感谢您的问题反馈，我们将尽快给予回复，敬请留意！");
            }
            else
            {
                ShowMessage(msg.Content);
            }
        }

        public void ShowAndRedirect(string msg)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("msgBox('{0}');", msg);
            Builder.AppendFormat("document.location.href=document.location.href");
            Builder.Append("</script>");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", Builder.ToString());
        }

        public void ShowMessage(string msg)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script type='text/javascript'>msgBox('" + msg.ToString() + "');</script>");
        }
    }
}