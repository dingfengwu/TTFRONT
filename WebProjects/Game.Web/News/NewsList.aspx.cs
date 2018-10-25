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

namespace Game.Web.News
{
    public partial class NewsList : UCPageBase
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewsData();
            }
            Themes.Standard.Common_Header sHeader = (Themes.Standard.Common_Header)this.FindControl("sHeader");
            sHeader.title = "新闻公告";
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("新闻列表 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        private void BindNewsData()
        {
            int sPageSize = anpPage.PageSize;
            PagerSet pagerSet = null;
            if (IntParam != 0)
            {
                pagerSet = FacadeManage.aideNativeWebFacade.GetNewsList(PageIndex, sPageSize, IntParam);
            }
            else
            {
                pagerSet = FacadeManage.aideNativeWebFacade.GetNewsList(PageIndex, sPageSize);
            }
            
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                this.rptNewsList.DataSource = pagerSet.PageSet;
                this.rptNewsList.DataBind();
            }

            if (pagerSet.RecordCount < sPageSize)
            {
                anpPage.Visible = false;
            }
        }
    }
}