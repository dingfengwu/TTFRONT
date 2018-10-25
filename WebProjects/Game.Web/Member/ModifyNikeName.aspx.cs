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

namespace Game.Web.Member
{
    public partial class ModifyNikeName : UCPageBase
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
                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
                if (umsg.Success)
                {
                    UserInfo ui = umsg.EntityList[0] as UserInfo;
                    this.lblGameID.Text = ui.GameID.ToString();
                    this.lblNickname.Text = ui.NickName;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (TextUtility.EmptyTrimOrNull(CtrlHelper.GetTextAndFilter(txtNickName)))
            {
                Show("抱歉！您输入的昵称错误了。");
                return;
            }

            Message umsg = FacadeManage.aideAccountsFacade.ModifyUserNickname(Fetch.GetUserCookie().UserID, TextFilter.FilterScript(txtNickName.Text.Trim()), GameRequest.GetUserIP());
            if (umsg.Success)
            {
                ShowAndRedirect("昵称修改成功!", "/Member/ModifyNikeName.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}