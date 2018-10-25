using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using com.yeepay.bank;

namespace Game.Web.Pay
{
    public partial class PayYB : UCPageBase
    {
        #region Fields

        protected int rateGameBean = 1;   //RMB与游戏豆的汇率

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //查询汇率
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.RateCurrency.ToString());
            if (ssi != null)
                rateGameBean = ssi.StatusValue;

            if (!IsPostBack)
            {
                sPaySidebar.PayID = 4;
                SwitchStep(1);

                if (Fetch.GetUserCookie() != null)
                {
                    this.txtPayAccounts.Text = Fetch.GetUserCookie().Accounts;
                    this.txtPayReAccounts.Text = Fetch.GetUserCookie().Accounts;
                }
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("网银充值 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string strAccounts = CtrlHelper.GetTextAndFilter(txtPayAccounts);
            string strReAccounts = CtrlHelper.GetTextAndFilter(txtPayReAccounts);
            int amount = CtrlHelper.GetInt(txtPayAmount, 0);

            if (strAccounts == "")
            {
                RenderAlertInfo(true, "抱歉，请输入充值帐号。", 2);
                return;
            }
            if (strReAccounts != strAccounts)
            {
                RenderAlertInfo(true, "抱歉，两次输入的帐号不一致。", 2);
                return;
            }
            if (amount <= 0)
            {
                RenderAlertInfo(true, "请输入正确的充值金额", 2);
                return;
            }

            OnLineOrder onlineOrder = new OnLineOrder();
            onlineOrder.ShareID = 3;
            onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix("YB");

            #region 订单处理

            if (Fetch.GetUserCookie() == null)
            {
                onlineOrder.OperUserID = 0;
            }
            else
            {
                onlineOrder.OperUserID = Fetch.GetUserCookie().UserID;
            }
            onlineOrder.Accounts = strAccounts;
            onlineOrder.OrderAmount = amount;
            onlineOrder.IPAddress = GameRequest.GetUserIP();

            //生成订单
            Message umsg = FacadeManage.aideTreasureFacade.RequestOrder(onlineOrder);
            if (!umsg.Success)
            {
                RenderAlertInfo(true, umsg.Content, 2);
                return;
            }
            #endregion

            #region 提交给易宝处理

            // 商户订单号,选填.
            //若不为""，提交的订单号必须在自身账户交易中唯一;为""时，易宝支付会自动生成随机的商户订单号.
            string p2_Order = onlineOrder.OrderID;

            // 支付金额,必填.
            //单位:元，精确到分.                   
            string p3_Amt = amount.ToString("f0");

            //交易币种,固定值"CNY".
            string p4_Cur = "CNY";

            //商品名称
            //用于支付时显示在易宝支付网关左侧的订单产品信息.
            string p5_Pid = "游戏豆";

            //商品种类
            string p6_Pcat = "";

            //商品描述
            string p7_Pdesc = "";

            //商户接收支付成功数据的地址,支付成功后易宝支付会向该地址发送两次成功通知.
            string p8_Url = "http://" + HttpContext.Current.Request.Url.Authority + "/Pay/PayYBReceive.aspx";

            //送货地址
            //为“1”: 需要用户将送货地址留在易宝支付系统;为“0”: 不需要，默认为 ”0”.
            string p9_SAF = "0";

            //商户扩展信息
            //商户可以任意填写1K 的字符串,支付成功时将原样返回.	
            string pa_MP = onlineOrder.Accounts;

            //银行编码
            //默认为""，到易宝支付网关.若不需显示易宝支付的页面，直接跳转到各银行、神州行支付、骏网一卡通等支付页面，该字段可依照附录:银行列表设置参数值.
            string pd_FrpId = "";

            //应答机制
            //默认为"1": 需要应答机制;
            string pr_NeedResponse = "1";

            string url = Buy.CreateBuyUrl(p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse);

            Response.Redirect(url);
            #endregion
        }
    }
}