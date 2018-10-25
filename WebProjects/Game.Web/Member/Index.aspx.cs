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
    public partial class Index : UCPageBase
    {
        #region Fields

        protected string accounts = string.Empty;
        protected string nickName = string.Empty;
        protected string gameID = string.Empty;
        protected string gender = string.Empty;
        protected string experience = string.Empty;
        protected string member = string.Empty;
        protected string underWrite = string.Empty;
        protected string loveLiness = string.Empty;
        protected string faceUrl = string.Empty;

        protected string score = string.Empty;
        protected string insureScore = string.Empty;
        protected string currency = string.Empty;
        protected string medal = string.Empty;
        #endregion

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
                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
                if (umsg.Success)
                {
                    UserInfo ui = umsg.EntityList[0] as UserInfo;
                    accounts = ui.Accounts;
                    nickName = ui.NickName;
                    gameID = ui.GameID.ToString();
                    gender = ui.Gender == 0 ? "女" : "男";
                    experience = ui.Experience.ToString("N0");
                    if (ui.MemberOrder == 0)
                    {
                        member = GetMemberInfo(ui.MemberOrder);
                    }
                    else
                    {
                        member = GetMemberInfo(ui.MemberOrder) + "&nbsp;[" + ui.MemberOverDate.ToString("yyyy-MM-dd") + "]";
                    }
                    underWrite = ui.UnderWrite;
                    loveLiness = ui.LoveLiness.ToString("N0");
                    medal = ui.UserMedal.ToString("N0");
                    faceUrl = FacadeManage.aideAccountsFacade.GetUserFaceUrl(ui.FaceID, ui.CustomID);
                }

                GameScoreInfo scoreInfo = FacadeManage.aideTreasureFacade.GetTreasureInfo2(Fetch.GetUserCookie().UserID);
                if (scoreInfo != null)
                {
                    score = scoreInfo.Score.ToString("N0");
                    insureScore = scoreInfo.InsureScore.ToString("N0");
                }

                UserCurrencyInfo currencyInfo = FacadeManage.aideTreasureFacade.GetUserCurrencyInfo(Fetch.GetUserCookie().UserID);
                if (currencyInfo != null)
                {
                    currency = currencyInfo.Currency.ToString("N");
                }
                else
                {
                    currency = "0";
                }
            }
        }
    }
}