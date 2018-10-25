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
    public partial class PayRecord : UCPageBase
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

                BindPayData();
            }
        }

        private void BindPayData()
        {
            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat(" WHERE UserID = {0} ", Fetch.GetUserCookie().UserID);
            sWhere.AppendFormat(" AND ApplyDate >= '{0}' AND ApplyDate <= '{1}'", Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00"), Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtEndDate) + " 23:59:59"));

            int sPageIndex = anpPage.CurrentPageIndex;
            int sPageSize = anpPage.PageSize;

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetPayRecord(sWhere.ToString(), sPageIndex, sPageSize);
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptPayList.DataSource = pagerSet.PageSet;
                rptPayList.DataBind();

                this.rptPayList.Visible = true;
                this.litNoData.Visible = false;
            }
            else
            {
                this.rptPayList.Visible = false;
                this.litNoData.Visible = true;
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
            BindPayData();
        }

        /// <summary>
        /// 查询按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            anpPage.CurrentPageIndex = 1;
            BindPayData();
        }
    }
}