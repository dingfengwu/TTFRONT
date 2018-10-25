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

namespace Game.Web.Member
{
    public partial class ConvertPresent : UCPageBase
    {
        protected int rate = 1;

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
            //获取兑换率
            SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.PresentExchangeRate.ToString());
            if (systemStatusInfo != null)
            {
                rate = systemStatusInfo.StatusValue;
            }

            if (!IsPostBack)
            {
                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
                if (umsg.Success)
                {
                    UserInfo user = umsg.EntityList[0] as UserInfo;
                    this.lblAccounts.Text = user.Accounts;
                    this.lblExchangeLoves.Text = user.Present.ToString();
                    this.lblGameID.Text = user.GameID.ToString();
                    this.lblTotalLoves.Text = user.LoveLiness.ToString();
                    this.lblUnExchangeLoves.Text = (user.LoveLiness - user.Present).ToString();

                    this.txtPresent.Text = (user.LoveLiness - user.Present).ToString();
                }

                GameScoreInfo scoreInfo = FacadeManage.aideTreasureFacade.GetTreasureInfo2(Fetch.GetUserCookie().UserID);
                if (scoreInfo != null)
                {
                    this.lblInsureScore.Text = scoreInfo.InsureScore.ToString();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int present = Utility.StrToInt(txtPresent.Text.Trim(), 0);
            if (present <= 0)
            {
                Show("兑换的魅力点必须为正整数！");
                return;
            }
            Message umsg = FacadeManage.aideAccountsFacade.UserConvertPresent(Fetch.GetUserCookie().UserID, present, GameRequest.GetUserIP());
            if (umsg.Success)
            {
                ShowAndRedirect("魅力兑换成功!", "/Member/ConvertPresent.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}