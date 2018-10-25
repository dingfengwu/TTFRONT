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
    public partial class SpreadBalance : UCPageBase
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
                UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(Fetch.GetUserCookie().UserID);
                this.lblAccounts.Text = userInfo.Accounts;
                this.lblGameID.Text = userInfo.GameID.ToString();

                Message umsg = FacadeManage.aideTreasureFacade.GetUserSpreadInfo(Fetch.GetUserCookie().UserID);
                if (umsg.Success)
                {
                    RecordSpreadInfo spreader = umsg.EntityList[0] as RecordSpreadInfo;
                    this.lblInsure.Text = spreader.InsureScore.ToString();
                    this.lblRecord.Text = spreader.RecordID.ToString();
                    this.lblScore.Text = spreader.Score.ToString();

                    this.txtScore.Text = spreader.RecordID.ToString();
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
            Message umsg = FacadeManage.aideTreasureFacade.GetUserSpreadBalance(Utility.StrToInt(this.txtScore.Text.Trim(), 0), Fetch.GetUserCookie().UserID, GameRequest.GetUserIP());
            if (umsg.Success)
            {
                ShowAndRedirect("推广结算成功!", "/Member/SpreadBalance.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}