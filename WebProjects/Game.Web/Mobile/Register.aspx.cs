using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Utils.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Mobile
{
    public partial class Register : UCPageBase
    {
        protected string accounts = "";
        protected string score = "0";
        protected string downLoadIosUrl = "";
        protected string downLoadAndroidUrl = "";
        protected string downLoadUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SwitchStep(1);

                //检查代理域名
                string agentDomain = Request.Url.Authority;
                int userID = FacadeManage.aideAccountsFacade.GetAccountAgentByDomain(agentDomain).UserID;
                string agentAccounts = FacadeManage.aideAccountsFacade.GetAccountsByUserID(userID);
                if (!string.IsNullOrEmpty(agentAccounts))
                {
                    txtSpreader.Text = agentAccounts;
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
                    txtSpreader.Text = GetAccountsByUserID(Convert.ToInt32(obj));
                }
            }

            if (IsPostBack)
            {
                RegisterAccounts();
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
        /// 注册
        /// </summary>
        private void RegisterAccounts()
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
            user.InsurePass = TextEncrypt.EncryptPassword(CtrlHelper.GetTextAndFilter(txtLogonPass));
            user.LastLogonDate = DateTime.Now;
            user.LastLogonIP = GameRequest.GetUserIP();
            user.LogonPass = TextEncrypt.EncryptPassword(CtrlHelper.GetText(txtLogonPass));
            user.NickName = CtrlHelper.GetTextAndFilter(txtAccounts);
            user.RegisterDate = DateTime.Now;
            user.RegisterIP = GameRequest.GetUserIP();
            user.DynamicPass = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            Message msg = FacadeManage.aideAccountsFacade.Register(user, CtrlHelper.GetText(txtSpreader));
            if (msg.Success)
            {
                UserInfo ui = msg.EntityList[0] as UserInfo;
                ui.LogonPass = TextEncrypt.EncryptPassword(CtrlHelper.GetText(txtLogonPass));
                Fetch.SetUserCookie(ui.ToUserTicketInfo());

                SwitchStep(2);
                accounts = CtrlHelper.GetTextAndFilter(txtAccounts);
                GameScoreInfo model = FacadeManage.aideTreasureFacade.GetTreasureInfo2(ui.UserID);
                if (model != null)
                {
                    score = model.Score.ToString();
                }
                ConfigInfo ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameAndroidConfig.ToString());
                if (ci != null)
                {
                    downLoadAndroidUrl = ci.Field1;
                }
                ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameIosConfig.ToString());
                if (ci != null)
                {
                    downLoadIosUrl = ci.Field1;
                }
                if (Fetch.GetTerminalType(Page.Request) == 1)
                {
                    downLoadUrl = downLoadAndroidUrl;
                }
                else
                {
                    downLoadUrl = downLoadIosUrl;
                }
            }
            else
            {
                Show(msg.Content);
                this.txtAccounts.Focus();
            }
        }
    }
}