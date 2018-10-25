using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Pay
{
    public partial class PayCardFill : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sPaySidebar.PayID = 1;

                SwitchStep(1);
                
                if (Fetch.GetUserCookie() != null)
                {
                    this.txtAccounts.Text = Fetch.GetUserCookie().Accounts;
                    this.txtAccounts2.Text = Fetch.GetUserCookie().Accounts;
                    this.txtAccounts.Focus();
                }
            }
            
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("实卡充值 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 充值按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPay_Click(object sender, EventArgs e)
        {
            string strAccounts = CtrlHelper.GetText(txtAccounts);
            string strReAccounts = CtrlHelper.GetText(txtAccounts2);
            string serialID = CtrlHelper.GetText(txtSerialID);
            string password = CtrlHelper.GetText(txtPassword);

            if (strAccounts == "")
            {
                RenderAlertInfo(true, "抱歉，请输入游戏帐号。", 2);
                return;
            }
            if (strReAccounts != strAccounts)
            {
                RenderAlertInfo(true, "抱歉，两次输入的帐号不一致。", 2);
                return;
            }
            if (serialID == "")
            {
                RenderAlertInfo(true, "抱歉，请输入充值卡号。", 2);
                return;
            }
            if (password == "")
            {
                RenderAlertInfo(true, "抱歉，请输入卡号密码。", 2);
                return;
            }

            //充值信息
            ShareDetialInfo detailInfo = new ShareDetialInfo();
            detailInfo.SerialID = CtrlHelper.GetText(txtSerialID);
            if (userTicket == null)
            {
                detailInfo.OperUserID = 0;
            }
            else
            {
                detailInfo.OperUserID = userTicket.UserID;
            }
            detailInfo.Accounts = strAccounts;
            detailInfo.ShareID = 1;             //实卡充值服务标识
            detailInfo.IPAddress = Utility.UserIP;

            #region 充值

            Message umsg = FacadeManage.aideTreasureFacade.FilledLivcard(detailInfo, TextEncrypt.EncryptPassword(txtPassword.Text.Trim()));
            if (umsg.Success)
            {
                RenderAlertInfo(false, "实卡充值成功。", 2);
            }
            else
            {
                RenderAlertInfo(true, umsg.Content, 2);
            }

            #endregion
        }
    }
}