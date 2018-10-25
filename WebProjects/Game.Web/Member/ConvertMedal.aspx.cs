using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using Game.Entity.Treasure;

namespace Game.Web.Member
{
    public partial class ConvertMedal : UCPageBase
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
            SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.MedalExchangeRate.ToString());
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
                    lblAccounts.Text = user.Accounts;
                    lblMedals.Text = user.UserMedal.ToString();
                    lblGameID.Text = user.GameID.ToString();
                    txtMedals.Text = user.UserMedal.ToString();
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
            int medals = Utility.StrToInt(txtMedals.Text.Trim(), 0);
            if (medals <= 0)
            {
                Show("兑换的元宝数必须为正整数！");
                return;
            }
            Message umsg = FacadeManage.aideAccountsFacade.UserConvertMedal(Fetch.GetUserCookie().UserID, medals, GameRequest.GetUserIP());
            if (umsg.Success)
            {
                ShowAndRedirect("元宝兑换成功!", "/Member/ConvertMedal.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}