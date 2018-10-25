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
    public partial class AdCourse : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindIssueData();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("高级教程 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        private void BindIssueData()
        {
            int sPageSize = anpPage.PageSize;
            string whereQuery = "WHERE TypeID=3 AND Nullity=0";

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetIssueList(whereQuery, PageIndex, sPageSize);
            anpPage.RecordCount = pagerSet.RecordCount;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                this.rptIssueList.DataSource = pagerSet.PageSet;
                this.rptIssueList.DataBind();
                anpPage.Visible = true;
            }
            else
            {
                anpPage.Visible = false;
            }
        }
    }
}