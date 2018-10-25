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
    public partial class FaqInfo : UCPageBase
    {
        public string title = "";
        public string content = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameIssueInfo issure = FacadeManage.aideNativeWebFacade.GetIssueByIssueID(IntParam, 0);
                if (issure != null)
                {
                    title = issure.IssueTitle;
                    content = Utility.HtmlDecode(issure.IssueContent);
                }

                GameIssueInfo issure1 = FacadeManage.aideNativeWebFacade.GetIssueByIssueID(1, IntParam, 1);
                if (issure1 != null)
                {
                    this.linLast.Title = "上一篇新闻：" + issure1.IssueTitle;
                    this.linLast.HRef = "/Service/FaqInfo.aspx?param=" + issure1.IssueID;
                }
                else
                {
                    this.linLast.Title = "已经是第一篇了！";
                    this.linLast.Disabled = true;
                    this.linLast.Visible = false;
                }

                GameIssueInfo issure2 = FacadeManage.aideNativeWebFacade.GetIssueByIssueID(1, IntParam, 2);
                if (issure2 != null)
                {
                    this.linNext.Title = "下一篇新闻：" + issure2.IssueTitle;
                    this.linNext.HRef = "/Service/FaqInfo.aspx?param=" + issure2.IssueID;
                }
                else
                {
                    this.linNext.Title = "已经是最后一篇了！";
                    this.linNext.Disabled = true;
                    this.linNext.Visible = false;
                }
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            GameIssueInfo issure = FacadeManage.aideNativeWebFacade.GetIssueByIssueID(IntParam, 0);
            if (issure != null)
            {
                AddMetaTitle(issure.IssueTitle + " - " + ApplicationSettings.Get("title"));
            }
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}