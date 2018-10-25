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
using System.Web.UI.HtmlControls;
using System.Text;

namespace Game.Web.Pay
{
    public partial class PayVB : UCPageBase
    {
        #region Fields

        protected int rateCurrency = 1;   //RMB与游戏豆的汇率

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //查询汇率
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.RateCurrency.ToString());
            if (ssi != null)
                rateCurrency = ssi.StatusValue;

            if (!IsPostBack)
            {
                sPaySidebar.PayID = 5;
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
            AddMetaTitle("电话充值 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string strAccounts = CtrlHelper.GetText(txtPayAccounts);
            string strReAccounts = CtrlHelper.GetText(txtPayReAccounts);
            int salePrice = GameRequest.GetFormInt("rbSaleType", 0);

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
            if (salePrice < 10)
            {
                RenderAlertInfo(true, "抱歉，充值金额必须大于10元", 2);
                return;
            }

            OnLineOrder onlineOrder = new OnLineOrder();
            onlineOrder.ShareID = 12;
            onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix("SX");

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
            onlineOrder.OrderAmount = salePrice;
            onlineOrder.IPAddress = GameRequest.GetUserIP();

            //生成订单
            Message umsg = FacadeManage.aideTreasureFacade.RequestOrder(onlineOrder);
            if (!umsg.Success)
            {
                RenderAlertInfo(true, umsg.Content, 2);
                return;
            }

            #endregion

            #region 提交给V币网关

            string spid = ApplicationSettings.Get("spId");//换成商户sp号码 长度5位
            string sppwd = ApplicationSettings.Get("spKeyValue");//换成商户sp校验密钥  长度18位
            string spreq = "http://" + HttpContext.Current.Request.Url.Authority + "/Pay/PayVB.aspx";//换成商户请求地址
            string sprec = "http://" + HttpContext.Current.Request.Url.Authority + "/Pay/PayVBReceive.aspx";    //换成商户接收地址
            string spname = Server.UrlEncode(ApplicationSettings.Get("spName"));//需要 Server.UrlEncode编码
            string spcustom = onlineOrder.Rate.ToString("f0");//需要 Server.UrlEncode编码  '客户自定义 30字符内 只能是数字、字母或数字字母的组合。不能用汉字。
            string spversion = "vpay1001";//此接口的版本号码 此版本是"vpay1001"
            string money = onlineOrder.OrderAmount.ToString("f0");//接参数面值元    
            string userid = Server.UrlEncode(onlineOrder.Accounts);//接参数用户ID 需要 Server.UrlEncode编码
            string urlcode = "utf-8";//编码  gbk  gb2312   utf-8  unicode   big5(注意不能一个繁体和简体字交叉写)  你程序的编码
            string orderId = onlineOrder.OrderID;//客户订单 请妥善管理客户的订单  长度：30字符以内（只能是数字、字母或数字字母的组合。不能用汉字订单）
            string post_key = orderId + spreq + sprec + spid + sppwd + spversion + money;
            // '网站订单号码+ 请求地址+ 接收地址 + 5位spid+ 18位SP密码+支付的版本号+面值
            //'LCase函数是将字符转换为小写; Ucase函数是将字符转换为大写
            //'全国声讯支付联盟全国声讯电话支付接口对MD5值只认大写字符串，所以小写的MD5值得转换为大写
            string md5password = TextEncrypt.EncryptPassword(post_key).ToUpper();//  '先MD532 然后转大写
            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
              && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty)
              ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
              : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            #endregion

            #region 整理参数

            pnlContinue.Visible = false;
            RenderAlertInfo(false, "页面正跳转到支付平台，请稍候。。。", 2);

            HtmlGenericControl pnlSubmit = this.FindControl("pnlSubmit") as HtmlGenericControl;
            if (pnlSubmit != null)
            {
                pnlSubmit.Visible = true;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(CreateInputHidden("spid", spid));
            builder.AppendLine(CreateInputHidden("spname", spname));
            builder.AppendLine(CreateInputHidden("spoid", orderId));
            builder.AppendLine(CreateInputHidden("spreq", spreq));
            builder.AppendLine(CreateInputHidden("sprec", sprec));
            builder.AppendLine(CreateInputHidden("userid", userid));
            builder.AppendLine(CreateInputHidden("userip", IpAddress));
            builder.AppendLine(CreateInputHidden("spmd5", md5password));
            builder.AppendLine(CreateInputHidden("spcustom", spcustom));
            builder.AppendLine(CreateInputHidden("spversion", spversion));
            builder.AppendLine(CreateInputHidden("money", money));
            builder.AppendLine(CreateInputHidden("urlcode", urlcode));

            litVB.Text = builder.ToString();

            #endregion
        }

        #region 公共方法

        private string CreateInputHidden(string idName, string value)
        {
            return String.Format("<input type=\"hidden\" id=\"{0}\" value=\"{1}\" name=\"{0}\" />", idName, value);
        }
        #endregion
    }
}