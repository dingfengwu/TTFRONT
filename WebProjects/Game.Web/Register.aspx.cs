using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.NativeWeb;
using Game.Entity.Platform;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Utils.Cache;

namespace Game.Web
{
    public partial class Register : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //检查代理域名
            string agentDomain = Request.Url.Authority;
            int userID = FacadeManage.aideAccountsFacade.GetAccountAgentByDomain(agentDomain).UserID;
            string agentAccounts = FacadeManage.aideAccountsFacade.GetAccountsByUserID(userID);
            if (!string.IsNullOrEmpty(agentAccounts))
            {
                txtSpreader.Text = agentAccounts;
                txtSpreader.ReadOnly = true;
                liSpreader.Visible = false;
                return;
            }
            else
            {
                //检查推广域名
                string subDomain = GameRequest.GetSubDomain();
                if (!string.IsNullOrEmpty(subDomain) && subDomain != "www" && Utils.Validate.IsNumeric(subDomain))
                {
                    string accounts = FacadeManage.aideAccountsFacade.GetAccountsBySubDomain(subDomain);
                    if (!string.IsNullOrEmpty(accounts))
                    {
                        txtSpreader.Text = accounts;
                        txtSpreader.ReadOnly = true;
                        return;
                    }
                }
            }

            //添加推广cookie
            if (IntParam != 0)
            {
                WHCache.Default.Save<CookiesCache>("SpreadID", IntParam);
            }

            //不存在二级域名推广，则检查URL参数推广
            object obj = WHCache.Default.Get<CookiesCache>("SpreadID");
            if (obj != null && Utils.Validate.IsNumeric(obj))
            {
                txtSpreader.ReadOnly = true;
                txtSpreader.Text = GetAccountsByUserID(Convert.ToInt32(obj));
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("用户注册 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (TextUtility.EmptyTrimOrNull(txtAccounts.Text.Trim()) || TextUtility.EmptyTrimOrNull(txtLogonPass.Text.Trim()))
            {
                Show("抱歉！您输入的帐号或密码错误了。");
                this.txtAccounts.Focus();
                return;
            }

            //验证码错误
            if (!txtCode.Text.Trim().Equals(Fetch.GetVerifyCode(), StringComparison.InvariantCultureIgnoreCase))
            {
                Show("抱歉！您输入的验证码错误了。");
                this.txtAccounts.Focus();
                return;
            }

            Message umsg = FacadeManage.aideAccountsFacade.IsAccountsExist(CtrlHelper.GetTextAndFilter(txtAccounts));
            if (!umsg.Success)
            {
                Show(umsg.Content);
                this.txtAccounts.Focus();
                return;
            }

            UserInfo user = new UserInfo();
            user.Accounts = CtrlHelper.GetTextAndFilter(txtAccounts);
            user.FaceID = Convert.ToInt16(hfFaceID.Value.Trim());
            user.Gender = Convert.ToByte(ddlGender.SelectedValue);
            user.InsurePass = TextEncrypt.EncryptPassword(CtrlHelper.GetTextAndFilter(txtLogonPass));
            user.LastLogonDate = DateTime.Now;
            user.LastLogonIP = GameRequest.GetUserIP();
            user.LogonPass = TextEncrypt.EncryptPassword(CtrlHelper.GetText(txtLogonPass));
            user.NickName = CtrlHelper.GetTextAndFilter(txtNickname);
            user.RegisterDate = DateTime.Now;
            user.RegisterIP = GameRequest.GetUserIP();
            user.DynamicPass = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            Message msg = FacadeManage.aideAccountsFacade.Register(user, CtrlHelper.GetText(txtSpreader));
            if (msg.Success)
            {
                UserInfo ui = msg.EntityList[0] as UserInfo;
                ui.LogonPass = TextEncrypt.EncryptPassword(CtrlHelper.GetText(txtLogonPass));
                Fetch.SetUserCookie(ui.ToUserTicketInfo());

                ShowAndRedirect("恭喜您注册成功！请下载游戏并完成实名认证获得奖励！", "/Member/Index.aspx");
            }
            else
            {
                Show(msg.Content);
                this.txtAccounts.Focus();
            }
        }
    }
}