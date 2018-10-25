using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Service
{
    public partial class Index : UCPageBase
    {
        protected string accounts = "用户";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Fetch.GetUserCookie() != null)
                {
                    accounts = Fetch.GetUserCookie().Accounts;
                }
                BindIssueData();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("客服中心 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        private void BindIssueData()
        {
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetIssueList(1, 20);
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                this.rptIssueList.DataSource = pagerSet.PageSet;
                this.rptIssueList.DataBind();
            }
        }
    }
}