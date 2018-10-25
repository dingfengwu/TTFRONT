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

namespace Game.Web
{
    public partial class Login : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("用户登录 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogon_Click(object sender, EventArgs e)
        {
            if (TextUtility.EmptyTrimOrNull(CtrlHelper.GetTextAndFilter(txtAccounts)) || TextUtility.EmptyTrimOrNull(CtrlHelper.GetTextAndFilter(txtLogonPass)))
            {
                Show("抱歉！您输入的帐号或密码错误了。");
                this.txtLogonPass.Text = "";
                this.txtCode.Text = "";
                return;
            }

            //验证码错误
            if (!txtCode.Text.Trim().Equals(Fetch.GetVerifyCode(), StringComparison.InvariantCultureIgnoreCase))
            {
                Show("抱歉！您输入的验证码错误了。");
                this.txtLogonPass.Text = "";
                this.txtCode.Text = "";
                this.txtLogonPass.Focus();
                return;
            }

            Message umsg = FacadeManage.aideAccountsFacade.Logon(CtrlHelper.GetTextAndFilter(txtAccounts), CtrlHelper.GetTextAndFilter(txtLogonPass));
            if (umsg.Success)
            {
                UserInfo ui = umsg.EntityList[0] as UserInfo;
                Fetch.SetUserCookie(ui.ToUserTicketInfo());
                if (GameRequest.GetQueryString("url") != "")
                    Response.Redirect(GameRequest.GetQueryString("url"));
                else
                    Response.Redirect("/Member/Index.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}