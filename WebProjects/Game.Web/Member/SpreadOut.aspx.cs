using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;

namespace Game.Web.Member
{
    public partial class SpreadOut : UCPageBase
    {
        public string total = "";

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
                this.txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                BindSpreadRecordData();
            }

            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat(" WHERE UserID = {0} AND Score < 0 ", Fetch.GetUserCookie().UserID);

            if (txtStartDate.Text.Trim() != "")
            {
                sWhere.AppendFormat(" AND CollectDate >= '{0}' ", CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00");
            }

            if (txtEndDate.Text.Trim() != "")
            {
                sWhere.AppendFormat(" AND CollectDate < '{0}'", Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtEndDate)).AddDays(1));
            }

            total = (0 - FacadeManage.aideTreasureFacade.GetUserSpreaderTotal(sWhere.ToString())).ToString("N");
        }

        private void BindSpreadRecordData()
        {
            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat(" WHERE UserID = {0} AND Score < 0  ", Fetch.GetUserCookie().UserID);

            if (txtStartDate.Text.Trim() != "")
            {
                sWhere.AppendFormat(" AND CollectDate >= '{0}' ", CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00");
            }

            if (txtEndDate.Text.Trim() != "")
            {
                sWhere.AppendFormat(" AND CollectDate < '{0}'", Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtEndDate)).AddDays(1));
            }

            int sPageIndex = anpPage.CurrentPageIndex;
            int sPageSize = anpPage.PageSize;

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetSpreaderRecord( sWhere.ToString(), sPageIndex, sPageSize );
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                this.rptSpreaderList.DataSource = pagerSet.PageSet;
                this.rptSpreaderList.DataBind();

                this.rptSpreaderList.Visible = true;
                this.litNoData.Visible = false;
                this.divTotal.Visible = true;
            }
            else
            {
                this.rptSpreaderList.Visible = false;
                this.litNoData.Visible = true;
                this.divTotal.Visible = false;
            }
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void anpPage_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            anpPage.CurrentPageIndex = e.NewPageIndex;
            BindSpreadRecordData();
        }

        /// <summary>
        /// 查询按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            anpPage.CurrentPageIndex = 1;
            BindSpreadRecordData();
        }
    }
}