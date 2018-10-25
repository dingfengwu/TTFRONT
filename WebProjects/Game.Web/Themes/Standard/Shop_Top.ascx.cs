using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Entity.Accounts;
using Game.Utils;

namespace Game.Web.Themes.Standard
{
    public partial class Shop_Top : System.Web.UI.UserControl
    {
        protected string faceUrl = string.Empty;            //头像地址
        protected string accounts = string.Empty;           //游戏帐号
        protected string memberIcon = string.Empty;         //会员图标
        protected int medal = 0;                            //奖牌数量
        protected string returnUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否登录
            if (Fetch.GetUserCookie() == null)
            {
                divLogon.Visible = false;
                divNoLogon.Visible = true;
                returnUrl = GameRequest.GetRawUrl();
                return;
            }

            //查询帐号
            UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "").EntityList[0] as UserInfo;
            if (userInfo == null)
            {
                divLogon.Visible = false;
                divNoLogon.Visible = true;
                return;
            }
            faceUrl = FacadeManage.aideAccountsFacade.GetUserFaceUrl(userInfo.FaceID, userInfo.CustomID);
            accounts = userInfo.Accounts;
            medal = userInfo.UserMedal;
        }
    }
}