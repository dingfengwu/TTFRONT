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
    public partial class AgentPayBackInfo : UCPageBase
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
                //判断是否为代理
                UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userTicket.UserID);
                if (userInfo.AgentID == 0)
                {
                    Response.Redirect("/Member/Index.aspx");
                }

                this.txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                BindData();
            }
        }

        private void BindData()
        {
            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat("WHERE UserID={0} AND TypeID=2", Fetch.GetUserCookie().UserID);

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

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordAgentInfo.Tablename, sPageIndex, sPageSize, "ORDER BY CollectDate DESC", sWhere.ToString());
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                this.rptSpreaderList.DataSource = pagerSet.PageSet;
                this.rptSpreaderList.DataBind();

                this.rptSpreaderList.Visible = true;
                this.litNoData.Visible = false;
            }
            else
            {
                this.rptSpreaderList.Visible = false;
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
            BindData();
        }

        /// <summary>
        /// 查询按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            anpPage.CurrentPageIndex = 1;
            BindData();
        }
    }
}