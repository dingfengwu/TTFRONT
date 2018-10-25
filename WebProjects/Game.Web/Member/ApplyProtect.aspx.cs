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
    public partial class ApplyProtect : UCPageBase
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
                    RenderAlertInfo2(false, "您已经成功申请了密码保护！");
                }
                else
                {
                    SwitchStep(1);
                }

                BindProtectQuestion();
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
            protect.PassportID = "";
            protect.PassportType = 0;
            protect.CreateIP = GameRequest.GetUserIP();

            protect.Question1 = ddlQuestion1.SelectedValue;
            protect.Question2 = ddlQuestion2.SelectedValue;
            protect.Question3 = ddlQuestion3.SelectedValue;
            protect.Response1 = TextFilter.FilterScript(txtResponse1.Text.Trim());
            protect.Response2 = TextFilter.FilterScript(txtResponse2.Text.Trim());
            protect.Response3 = TextFilter.FilterScript(txtResponse3.Text.Trim());

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

            Message umsg = FacadeManage.aideAccountsFacade.ApplyUserSecurity(protect);
            if (umsg.Success)
            {
                RenderAlertInfo2(false, "您已经成功申请了密码保护！");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}