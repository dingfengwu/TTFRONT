using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;

namespace Game.Web.Service
{
    public partial class Feedback : UCPageBase
    {
        protected string url = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //帐号文本框赋值
                string accounts = string.Empty;
                if (Fetch.GetUserCookie() != null)
                {
                    lbAccounts.Text = Fetch.GetUserCookie().Accounts;
                    accounts = Fetch.GetUserCookie().Accounts;
                }
                else
                {
                    lbAccounts.Text = "您未登录，匿名提问&nbsp;<a href=\"/Login.aspx?url=" + RawUrl + "\">【登录】</a>";
                }

                //当"StrParam"为"self"时，判断是否登录
                if (StrParam == "self")
                {
                    if (Fetch.GetUserCookie() == null)
                    {
                        divData.Visible = false;
                        anpPage.Visible = false;
                        return;
                    }
                    BindFeedBackData(Fetch.GetUserCookie().UserID);
                }
                else
                {
                    BindFeedBackData(0);
                }
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("在线反馈 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定反馈
        /// </summary>
        /// <param name="accounts"></param>
        private void BindFeedBackData(int userID)
        {
            int sPageSize = anpPage.PageSize;

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetFeedbacklist(PageIndex, sPageSize, userID);
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptFeedBackList.DataSource = pagerSet.PageSet;
                rptFeedBackList.DataBind();
                anpPage.Visible = true;
            }
            else
            {
                rptFeedBackList.Visible = false;
                anpPage.Visible = false;
            }
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
                ShowAndRedirect("感谢您的问题反馈，我们将尽快给予回复，敬请留意！", "/Service/Feedback.aspx");
            }
            else
            {
                Show(msg.Content);
            }
        }
    }
}