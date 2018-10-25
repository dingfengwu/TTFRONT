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

namespace Game.Web.Pay.JFT
{
    public partial class Card : UCPageBase
    {
        protected int rateGameBean = 1;   //RMB与游戏豆的汇率
        protected string formData = string.Empty;
        protected string iconClass = string.Empty;
        protected string infoClass = string.Empty;
        protected string msg = string.Empty;
        protected string btClass = "fn-hide";
        protected string js = string.Empty;
        protected string cardName = string.Empty;
        public int cardType = 101;
        protected string strFaceValueOption = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 查询汇率
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.RateCurrency.ToString());
            if (ssi != null)
                rateGameBean = ssi.StatusValue;

            // 充值卡类型
            cardType = GameRequest.GetQueryInt("type", 101);

            // 充值卡类型ID不合法强制定义为101
            if (cardType > 115 || cardType < 101)
                cardType = 101;

            // 充值卡名称
            sPaySidebar.PayID = cardType;
            cardName = Enum.GetName(typeof(AppConfig.JFTPayCardType), cardType);

            if (!IsPostBack)
            {
                SwitchStep(1);
                if (Fetch.GetUserCookie() != null)
                {
                    this.txtPayAccounts.Text = Fetch.GetUserCookie().Accounts;
                    this.txtPayReAccounts.Text = Fetch.GetUserCookie().Accounts;
                }

                // 充值面值
                Dictionary<int, string> dicCardFaceValue = new Dictionary<int, string>();
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.骏网一卡通), "5,6,10,15,20,30,50,100,120,200,300,500,1000");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.盛大卡), "1,2,3,5,9,10,15,25,30,35,45,50,100,300,350,1000");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.神州行), "10,20,30,50,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.征途卡), "10,15,20,25,30,50,60,100,300,468,500,1000");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.Q币卡), "5,10,15,30,60,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.联通卡), "10,20,30,50,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.久游卡), "5,10,20,25,30,50,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.易宝e卡通), "1,2,3,5,9,10,15,25,30,35,45,50,100,300,350,1000");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.网易卡), "5,10,15,20,30,50");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.完美卡), "15,30,50,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.搜狐卡), "5,10,15,30,40,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.电信卡), "10,20,30,50,100,300");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.纵游一卡通), "10,15,30,50,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.天下一卡通), "5,6,10,15,30,50,100");
                dicCardFaceValue.Add(Convert.ToInt32(AppConfig.JFTPayCardType.天宏一卡通), "5,10,15,30,50,100");

                string[] arrCardFaceValue = dicCardFaceValue[cardType].Split(',');
                ddlAmount.Items.Clear();
                ddlAmount.Items.Add(new ListItem("---请选择卡面值---", "0"));
                foreach (string item in arrCardFaceValue)
                {
                    ddlAmount.Items.Add(new ListItem(item, item));
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

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnPay_Click(object sender, EventArgs e)
        {
            string strAccounts = CtrlHelper.GetText(txtPayAccounts);
            string strReAccounts = CtrlHelper.GetText(txtPayReAccounts);
            string strCardNumber = CtrlHelper.GetText(txtCardNumber);
            string strCardPassword = CtrlHelper.GetText(txtCardPassword);
            int amount = Convert.ToInt32(ddlAmount.SelectedValue);

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
            if (amount == 0)
            {
                RenderAlertInfo(true, "请选择卡面值。", 2);
                return;
            }
            if (string.IsNullOrEmpty(strCardNumber))
            {
                RenderAlertInfo(true, "抱歉，请输入卡号。", 2);
                return;
            }
            if (string.IsNullOrEmpty(strCardPassword))
            {
                RenderAlertInfo(true, "抱歉，请输入卡密码。", 2);
                return;
            }

            OnLineOrder onlineOrder = new OnLineOrder();
            onlineOrder.ShareID = cardType;
            onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix("JFTCard");

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
            string p4_returnurl = "http://" + HttpContext.Current.Request.Url.Authority + "/Pay/JFT/CardReturn.aspx";
            // 通知地址
            string p5_notifyurl = "http://" + HttpContext.Current.Request.Url.Authority + "/Pay/JFT/PublicAdvice.aspx";
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
            // 商户支付方式
            string p9_paymethod = "5";
            // 支付通道编码
            string p10_paychannelnum = EnumDescription.GetFieldText(typeof(AppConfig.JFTPayCardType), cardType);
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
            // 卡密
            string p19_productcat = strCardPassword;
            // 卡号
            string p20_productnum = strCardNumber;
            // 商品描述
            string p21_pdesc = "";
            // 接口版本
            string p22_version = "2.0";
            // 编码类型
            string p23_charset = "";
            // 备注信息
            string p24_remark = "";

            #region 整理参数

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