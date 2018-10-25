using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using Game.Entity.Treasure;
using Game.Kernel;
using System.Text;
using System.Text.RegularExpressions;

namespace Game.Web.UserService.JFT
{
    public partial class Main : UCPageBase
    {
        protected int rateGameBean = 1;   //RMB与游戏豆的汇率
        protected string formData = string.Empty;
        protected string iconClass = string.Empty;
        protected string infoClass = string.Empty;
        protected string msg = string.Empty;
        protected string btClass = "fn-hide";
        protected string js = string.Empty;

        public string payType = string.Empty;
        public string payName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //查询汇率
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.RateCurrency.ToString());
            if (ssi != null)
                rateGameBean = ssi.StatusValue;

            payType = GameRequest.GetString("paytype").ToLower();
            switch (payType)
            {
                case "alipay":
                    payName = "支付宝充值";
                    break;
                case "wechat":
                    payName = "微信支付";
                    break;
                case "bank":
                default:
                    payName = "网上银行";
                    break;
            }

            if (!IsPostBack)
            {
                SwitchStep(1);

                if (Fetch.GetUserCookie() != null)
                {
                    this.txtPayAccounts.Text = Fetch.GetUserCookie().Accounts;
                    this.txtPayReAccounts.Text = Fetch.GetUserCookie().Accounts;
                    this.txtPayAccounts.Focus();
                }
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle(payName + "充值 - " + ApplicationSettings.Get("title"));
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
            string p9_paymethod = "";
            string payPrefix = string.Empty;
            int shareID = 0;
            switch (payType)
            {
                case "alipay":
                    payPrefix = "JFTZFB";
                    shareID = 14;
                    p9_paymethod = "4";
                    break;
                case "wechat":
                    payPrefix = "JFTWX";
                    shareID = 15;
                    p9_paymethod = "3";
                    break;
                case "bank":
                default:
                    payPrefix = "JFTBank";
                    shareID = 13;
                    p9_paymethod = "1";
                    break;
            }

            OnLineOrder onlineOrder = new OnLineOrder();
            onlineOrder.ShareID = shareID;
            onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix(payPrefix);

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

            // 商户ID
            string p1_usercode = ApplicationSettings.Get("jftBankID");
            // 秘钥
            string key = ApplicationSettings.Get("jftBankKey");
            // 回调页面
            string p4_returnurl = "http://" + HttpContext.Current.Request.Url.Authority + "/UserService/JFT/PublicReturn.aspx";
            // 通知地址
            string p5_notifyurl = "http://" + HttpContext.Current.Request.Url.Authority + "/UserService/JFT/PublicAdvice.aspx";
            // 请求时间
            string p6_ordertime = DateTime.Now.ToString("yyyyMMddHHmmss");
            // 签名信息
            string strEncryption = string.Format("{0}&{1}&{2}&{3}&{4}&{5}{6}",
                p1_usercode,
                onlineOrder.OrderID,
                amount,
                p4_returnurl,
                p5_notifyurl,
                p6_ordertime,
                key
            );
            string p7_sign = Utility.MD5(strEncryption).ToUpper();

            // 加密方法
            string p8_signtype = "";
            // 银行编码
            string p10_paychannelnum = "";
            // 商户进行支付的银行卡类型
            string p11_cardtype = "";
            // 银行支付类型
            string p12_channel = "";
            // 订单失效时间
            string p13_orderfailertime = "";
            // 客户或卖家名称
            string p14_customname = "";
            // 客户联系方式类型 1、email，2、phone，3、地址
            string p15_customcontacttype = "";
            // 客户联系方式
            string p16_customcontact = "";
            // 客户IP地址
            string p17_customip = "";
            // 商品名称
            string p18_product = "";
            // 商品种类
            string p19_productcat = "";
            // 商品数量
            string p20_productnum = "";
            // 商品描述
            string p21_pdesc = "";
            // 接口版本
            string p22_version = "2.0";
            // 编码类型
            string p23_charset = "";
            // 备注信息
            string p24_remark = "";

            #region 整理参数

            pnlContinue.Visible = false;
            RenderAlertInfo(false, "页面正跳转到支付平台，请稍候。。。", 2);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(CreateInputHidden("p1_usercode", p1_usercode));
            builder.AppendLine(CreateInputHidden("p2_order", onlineOrder.OrderID));
            builder.AppendLine(CreateInputHidden("p3_money", amount.ToString()));
            builder.AppendLine(CreateInputHidden("p4_returnurl", p4_returnurl));
            builder.AppendLine(CreateInputHidden("p5_notifyurl", p5_notifyurl));
            builder.AppendLine(CreateInputHidden("p6_ordertime", p6_ordertime));
            builder.AppendLine(CreateInputHidden("p7_sign", p7_sign));
            builder.AppendLine(CreateInputHidden("p8_signtype", p8_signtype));
            builder.AppendLine(CreateInputHidden("p9_paymethod", p9_paymethod));
            builder.AppendLine(CreateInputHidden("p10_paychannelnum", p10_paychannelnum));
            builder.AppendLine(CreateInputHidden("p11_cardtype", p11_cardtype));
            builder.AppendLine(CreateInputHidden("p12_channel", p12_channel));
            builder.AppendLine(CreateInputHidden("p13_orderfailertime", p13_orderfailertime));
            builder.AppendLine(CreateInputHidden("p14_customname", p14_customname));
            builder.AppendLine(CreateInputHidden("p15_customcontacttype", p15_customcontacttype));
            builder.AppendLine(CreateInputHidden("p16_customcontact", p16_customcontact));
            builder.AppendLine(CreateInputHidden("p17_customip", p17_customip));
            builder.AppendLine(CreateInputHidden("p18_product", p18_product));
            builder.AppendLine(CreateInputHidden("p19_productcat", p19_productcat));
            builder.AppendLine(CreateInputHidden("p20_productnum", p20_productnum));
            builder.AppendLine(CreateInputHidden("p21_pdesc", p21_pdesc));
            builder.AppendLine(CreateInputHidden("p22_version", p22_version));
            builder.AppendLine(CreateInputHidden("p23_charset", p23_charset));
            builder.AppendLine(CreateInputHidden("p24_remark", p24_remark));

            formData = builder.ToString();

            #endregion

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