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
    public partial class AgentChildInfo : UCPageBase
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

                BindSpreaderData();
            }
        }

        private void BindSpreaderData()
        {
            int sPageIndex = anpPage.CurrentPageIndex;
            int sPageSize = anpPage.PageSize;

            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsInfo.Tablename, sPageIndex, sPageSize, SearchItems, Orderby);
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
            BindSpreaderData();
        }

        /// <summary>
        /// 查询按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            anpPage.CurrentPageIndex = 1;

            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat(" WHERE SpreaderID={0} ", Fetch.GetUserCookie().UserID);

            if (txtStartDate.Text.Trim() != "")
            {
                sWhere.AppendFormat(" AND RegisterDate >= '{0}' ", CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00");
            }

            if (txtEndDate.Text.Trim() != "")
            {
                sWhere.AppendFormat(" AND RegisterDate < '{0}'", Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtEndDate)).AddDays(1));
            }
            SearchItems = sWhere.ToString();
            BindSpreaderData();
        }

        #region 公共属性

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if (ViewState["SearchItems"] == null)
                {
                    StringBuilder condition = new StringBuilder();
                    condition.AppendFormat(" WHERE SpreaderID={0}", Fetch.GetUserCookie().UserID);
                    ViewState["SearchItems"] = condition.ToString();
                }

                return (string)ViewState["SearchItems"];
            }

            set
            {
                ViewState["SearchItems"] = value;
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY RegisterDate DESC";
                }

                return (string)ViewState["Orderby"];
            }

            set
            {
                ViewState["Orderby"] = value;
            }
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 贡献税收
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected Int64 GetChildRevenueProvide(int userID)
        {
            return FacadeManage.aideTreasureFacade.GetChildRevenueProvide(userID);
        }

        /// <summary>
        /// 贡献充值
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected Int64 GetChildPayProvide(int userID)
        {
            return FacadeManage.aideTreasureFacade.GetChildPayProvide(userID);
        }
        #endregion
    }
}