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
    public partial class ModifyProtect : UCPageBase
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
                SwitchStep(1);

                Message umsg = FacadeManage.aideAccountsFacade.GetUserSecurityByUserID(Fetch.GetUserCookie().UserID);
                if (umsg.Success)
                {
                    AccountsProtect protect = umsg.EntityList[0] as AccountsProtect;
                    lblQuestion1.Text = protect.Question1;
                    lblQuestion2.Text = protect.Question2;
                    lblQuestion3.Text = protect.Question3;

                    SwitchStep(1);
                }
                else
                {
                    RenderAlertInfo3(true, "您还没有申请密码保护功能，请先<a href='/Member/ApplyProtect.aspx'>申请密保</a>");
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            AccountsProtect protect = new AccountsProtect();
            protect.Response1 = CtrlHelper.GetTextAndFilter(txtResponse1);
            protect.Response2 = CtrlHelper.GetTextAndFilter(txtResponse2);
            protect.Response3 = CtrlHelper.GetTextAndFilter(txtResponse3);

            if (string.IsNullOrEmpty(protect.Response1))
            {
                Show("请输入密保问题一的答案");
                return;
            }
            if (string.IsNullOrEmpty(protect.Response2))
            {
                Show("请输入密保问题二的答案");
                return;
            }
            if (string.IsNullOrEmpty(protect.Response3))
            {
                Show("请输入密保问题三的答案");
                return;
            }

            protect.UserID = Fetch.GetUserCookie().UserID;

            Message umsg = FacadeManage.aideAccountsFacade.ConfirmUserSecurity(protect);
            if (umsg.Success)
            {
                SwitchStep(2);

                BindProtectQuestion();
            }
            else
            {
                Show(umsg.Content);
            }
        }

        private void BindProtectQuestion()
        {
            ddlQuestion1.DataSource = Fetch.ProtectionQuestiongs;
            ddlQuestion1.DataBind();

            ddlQuestion2.DataSource = Fetch.ProtectionQuestiongs;
            ddlQuestion2.DataBind();

            ddlQuestion3.DataSource = Fetch.ProtectionQuestiongs;
            ddlQuestion3.DataBind();

            ddlQuestion1.Items.Insert(0, new ListItem("请选择密保问题", "0"));
            ddlQuestion2.Items.Insert(0, new ListItem("请选择密保问题", "0"));
            ddlQuestion3.Items.Insert(0, new ListItem("请选择密保问题", "0"));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            AccountsProtect protect = new AccountsProtect();
            protect.UserID = Fetch.GetUserCookie().UserID;
            protect.SafeEmail = "";
            protect.ModifyIP = GameRequest.GetUserIP();

            protect.Question1 = ddlQuestion1.SelectedValue;
            protect.Question2 = ddlQuestion2.SelectedValue;
            protect.Question3 = ddlQuestion3.SelectedValue;
            protect.Response1 = TextFilter.FilterScript(txtMResponse1.Text.Trim());
            protect.Response2 = TextFilter.FilterScript(txtMResponse2.Text.Trim());
            protect.Response3 = TextFilter.FilterScript(txtMResponse3.Text.Trim());

            if (protect.Question1 == "0")
            {
                Show("请选择密保问题一");
                return;
            }
            if (protect.Question2 == "0")
            {
                Show("请选择密保问题二");
                return;
            }
            if (protect.Question3 == "0")
            {
                Show("请选择密保问题三");
                return;
            }
            if (string.IsNullOrEmpty(protect.Response1))
            {
                Show("请输入密保问题一的答案");
                return;
            }
            if (string.IsNullOrEmpty(protect.Response2))
            {
                Show("请输入密保问题二的答案");
                return;
            }
            if (string.IsNullOrEmpty(protect.Response3))
            {
                Show("请输入密保问题三的答案");
                return;
            }

            Message umsg = FacadeManage.aideAccountsFacade.ModifyUserSecurity(protect);
            if (umsg.Success)
            {
                RenderAlertInfo3(false, "您已经成功修改了密码保护！");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}