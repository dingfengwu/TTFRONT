using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Kernel;
using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using System.Text;

namespace Game.Web.UserService
{
    public partial class InsureTransferRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取登录信息
            Fetch.GetUserCookie();

            //判断是否登录
            if (!Fetch.IsUserOnline())
            {
                return;
            }

            if (!IsPostBack)
            {
                DataBindInsure();
            }
        }

        /// <summary>
        /// 绑定查询数据
        /// </summary>
        private void DataBindInsure()
        {
            if (!Fetch.IsUserOnline())
            {
                return;
            }

            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat("WHERE SourceUserID = {0} OR TargetUserID = {0} ", Fetch.GetUserCookie().UserID);

            int sPageIndex = GameRequest.GetQueryInt("page", 1);
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

        /// <summary>
        /// 根据用户ID获取帐号
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetNickNameByUserID(int userID)
        {
            UserInfo user = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userID);
            if (user == null)
                return "";
            return user.NickName;
        }
    }
}