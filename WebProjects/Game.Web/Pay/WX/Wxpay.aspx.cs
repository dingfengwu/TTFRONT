using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System.Text;

namespace Game.Web.Pay.WX
{
    public partial class Wxpay : UCPageBase
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
            AddMetaTitle("微信充值 - " + ApplicationSettings.Get("title"));
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
            string orderid = PayHelper.GetOrderIDByPrefix("WX");
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

            //随机字符串
            string nonce_str = WeiXinHelper.GetNonce_str();
            //商品描述
            string body = "充值游戏豆";
            //商户订单号
            string out_trade_no = orderid;
            //交易金额（分为单位）
            int total_fee = Convert.ToInt32(amount * 100);
            //终端IP
            string spbill_create_ip = Utility.UserIP;
            //回调地址
            string notify_url = "http://" + Request.Url.Authority + "/Pay/WX/WxpayNotify.aspx";
            //商品ID
            string product_id = orderid.Substring(2, orderid.Length - 2);

            SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
            dic.Add("nonce_str", nonce_str);
            dic.Add("body", body);
            dic.Add("trade_type", "NATIVE");
            dic.Add("out_trade_no", out_trade_no);
            dic.Add("total_fee", total_fee);
            dic.Add("spbill_create_ip", spbill_create_ip);
            dic.Add("notify_url", notify_url);
            dic.Add("product_id", product_id);

            pnlContinue.Visible = false;
            RenderAlertInfo(false, "页面正跳转到支付平台，请稍候。。。", 2);

            SortedDictionary<string, object> result = WeiXinHelper.UnifiedOrder(dic, 10);
            string code = result["return_code"].ToString();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(CreateInputHidden("return_code", code));
            builder.AppendLine(CreateInputHidden("return_msg", result["return_msg"].ToString()));
            if (code == "SUCCESS")
            {
                builder.AppendLine(CreateInputHidden("code_url", result["code_url"].ToString()));
                builder.AppendLine(CreateInputHidden("orderID", orderid));
                builder.AppendLine(CreateInputHidden("amount", amount.ToString()));
            }

            formData = builder.ToString();

            js = "<script>window.onload = function() { document.forms[0].submit(); }</script>";
        }

        #region 公共方法

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