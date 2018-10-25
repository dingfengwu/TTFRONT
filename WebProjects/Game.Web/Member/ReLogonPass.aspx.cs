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
    public partial class ReLogonPass : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SwitchStep(1);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.radType2.Checked)
            {
                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(0, Utility.StrToInt(txtContect.Text.Trim(), 0), "");
                if (!umsg.Success)
                {
                    Show("您输入的游戏ID号码错误，请重新输入！");
                    this.txtContect.Text = "";
                    this.txtContect.Focus();
                }
                else
                {
                    UserInfo user = umsg.EntityList[0] as UserInfo;
                    ViewState["UserID"] = user.UserID;

                    GetUserSecurityInfo(user.UserID);
                }
            }
            else
            {
                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(0, 0, CtrlHelper.GetTextAndFilter(txtContect));
                if (umsg.Success)
                {
                    UserInfo user = umsg.EntityList[0] as UserInfo;
                    ViewState["UserID"] = user.UserID;
                    GetUserSecurityInfo(user.UserID);
                }
                else
                {
                    Show("您输入的帐号错误，请重新输入！");
                    this.txtContect.Text = "";
                    this.txtContect.Focus();
                }
            }
        }

        private void GetUserSecurityInfo(int userId)
        {
            Message umsg = FacadeManage.aideAccountsFacade.GetUserSecurityByUserID(userId);
            if (umsg.Success)
            {
                SwitchStep(2);

                AccountsProtect protect = umsg.EntityList[0] as AccountsProtect;

                this.lblQuestion1.Text = protect.Question1;
                this.lblQuestion2.Text = protect.Question2;
                this.lblQuestion3.Text = protect.Question3;
            }
            else
            {
                RenderAlertInfo3(true, "抱歉，此帐号还没有申请密码保护功能，请通过帐号申述找回密码！");
                return;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            AccountsProtect protect = new AccountsProtect();
            protect.UserID = Utility.StrToInt(ViewState["UserID"], 0);
            protect.LogonPass = TextEncrypt.EncryptPassword(CtrlHelper.GetTextAndFilter(txtNewPass));
            protect.Response1 = CtrlHelper.GetTextAndFilter(txtResponse1);
            protect.Response2 = CtrlHelper.GetTextAndFilter(txtResponse2);
            protect.Response3 = CtrlHelper.GetTextAndFilter(txtResponse3);
            protect.LastLogonIP = GameRequest.GetUserIP();

            Message umsg = FacadeManage.aideAccountsFacade.ResetLogonPasswd(protect);
            if (umsg.Success)
            {
                int userid = Utility.StrToInt(ViewState["UserID"], 0);
                if (Fetch.GetUserCookie() != null)
                {
                    if (userid == Fetch.GetUserCookie().UserID)
                    {
                        Fetch.DeleteUserCookie();
                        ShowAndRedirect("重置登录密码成功，请您重新登录！", "/Login.aspx");
                    }
                    else
                    {
                        RenderAlertInfo3(false, "重置登录密码成功，可用这个帐号重新登录！");
                    }
                }
                ShowAndRedirect("重置登录密码成功，请登录！", "/Login.aspx");
            }
            else
            {
                Show(umsg.Content);
                txtResponse1.Text = "";
                txtResponse2.Text = "";
                txtResponse3.Text = "";
            }
        }
    }
}