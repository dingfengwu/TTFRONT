using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.Pay.ZFB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Pay.ZFB
{
    public partial class Alipay : UCPageBase
    {
        protected int rateGameBean = 1;   //RMB与游戏豆的汇率
        protected string formData = string.Empty;
        protected string iconClass = string.Empty;
        protected string infoClass = string.Empty;
        protected string msg = string.Empty;
        protected string btClass = "fn-hide";
        protected string js = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //查询汇率
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.RateCurrency.ToString());
            if (ssi != null)
                rateGameBean = ssi.StatusValue;

            if (!IsPostBack)
            {
                this.fmStep1.Visible = true;

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
            AddMetaTitle("支付宝充值 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnPay_Click(object sender, EventArgs e)
        {
            string strAccounts = CtrlHelper.GetText(txtPayAccounts);
            string strReAccounts = CtrlHelper.GetText(txtPayReAccounts);
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

            // 验证是否合法的金额
            if (amount <= 0)
            {
                RenderAlertInfo(true, "请输入正确的充值金额", 2);
                return;
            }

            // 支付方法
            string orderid = PayHelper.GetOrderIDByPrefix("ZFB");
            OnLineOrder onlineOrder = new OnLineOrder();
            onlineOrder.ShareID = 401;
            onlineOrder.OrderID = orderid;

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

            //服务器异步通知页面路径
            string notify_url = "http://" + Request.Url.Authority + "/Pay/ZFB/AlipayNotify.aspx";
            //页面跳转同步通知页面路径
            string return_url = "http://" + Request.Url.Authority + "/Pay/Index.aspx";
            //商户网站唯一订单号
            string out_trade_no = orderid;
            //商品名称
            string subject = "充值游戏豆";
            //交易金额
            decimal total_fee = Convert.ToDecimal(amount);

            RenderAlertInfo(false, "页面正跳转到支付平台，请稍候。。。", 2);

            //获取数据签名
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee.ToString());

            formData = AliPayHelper.BuildRequest(sParaTemp);

            js = "<script>window.onload = function() { document.forms[0].submit(); }</script>";
        }

        #region 公共方法

        //提示样式类
        private static string[] ALERT_STYLE_CLASS = { "ui-result-pic-1", "ui-result-success", "ui-result-pic-2", "ui-result-fail" };

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="isError"></param>
        public new virtual void RenderAlertInfo(bool isError, string alertText, int step)
        {
            this.fmStep1.Visible = false;
            msg = alertText;
            if (isError)
            {
                iconClass = ALERT_STYLE_CLASS[2];
                infoClass = ALERT_STYLE_CLASS[3];

                btClass = "";
            }
            else
            {
                iconClass = ALERT_STYLE_CLASS[0];
                infoClass = ALERT_STYLE_CLASS[1];
                msg = alertText;
                btClass = "fn-hide";
            }
        }

        //功能函数。将变量值不为空的参数组成字符串
        private String AppendParam(String returnStr, String paramId, String paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    returnStr += "&" + paramId + "=" + paramValue;
                }
            }
            else
            {
                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }

            return returnStr;
        }

        //隐藏字段
        private string CreateInputHidden(string idName, string value)
        {
            return String.Format("<input type=\"hidden\" id=\"{0}\" value=\"{1}\" name=\"{0}\" />", idName, value);
        }
        #endregion
    }
}