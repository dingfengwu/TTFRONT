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
    public partial class InsureRecord : UCPageBase
    {
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

                DataBindInsure();
            }
        }

        /// <summary>
        /// 绑定查询数据
        /// </summary>
        private void DataBindInsure()
        {
            StringBuilder sWhere = new StringBuilder();
            sWhere.Append(" WHERE ");

            if (Convert.ToInt32(ddlType.SelectedValue) != 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) < 3)
                {
                    sWhere.AppendFormat(" TradeType = {0} AND SourceUserID = {1}", Convert.ToInt32(ddlType.SelectedValue) == 1 ? 1 : 2, Fetch.GetUserCookie().UserID);
                }
                else
                {
                    if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                        sWhere.AppendFormat(" SourceUserID = {0} AND TradeType = 3 and TargetUserID <> {0}", Fetch.GetUserCookie().UserID);
                    else if (Convert.ToInt32(ddlType.SelectedValue) == 4)
                    {
                        sWhere.AppendFormat(" SourceUserID <> {0} AND TradeType = 3 and TargetUserID = {0}", Fetch.GetUserCookie().UserID);
                    }
                }
            }
            else
            {
                sWhere.AppendFormat(" SourceUserID = {0} OR TargetUserID = {0}", Fetch.GetUserCookie().UserID);
            }
            sWhere.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate <= '{1}'", Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00"), Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtEndDate) + " 23:59:59"));

            int sPageIndex = anpPage.CurrentPageIndex;
            int sPageSize = anpPage.PageSize;

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetInsureTradeRecord(sWhere.ToString(), sPageIndex, sPageSize);
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptInsureList.DataSource = pagerSet.PageSet;
                rptInsureList.DataBind();

                this.rptInsureList.Visible = true;
                this.litNoData.Visible = false;
            }
            else
            {
                this.rptInsureList.Visible = false;
                this.litNoData.Visible = true;
            }
        }


        protected void anpPage_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            anpPage.CurrentPageIndex = e.NewPageIndex;
            DataBindInsure();
        }

        /// <summary>
        /// 查询按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            anpPage.CurrentPageIndex = 1;
            DataBindInsure();
        }
    }
}