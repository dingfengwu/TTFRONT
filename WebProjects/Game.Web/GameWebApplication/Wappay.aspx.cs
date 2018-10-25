using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Game.Web.AppPay;

namespace WebApplication1
{
    public partial class Wappay : System.Web.UI.Page
    {
        float Amount;
        int UserId;
        string TradeNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            WIDsubject.Text = "支付宝充值";
            WIDbody.Text = "支付宝充值";

            string AmountString = Request.QueryString["Amount"];
            string UserIdString = Request.QueryString["UserId"];
            string UserName = Request.QueryString["UserName"];
            TradeNo = Request.QueryString["TradeNo"];
            float.TryParse(AmountString, out Amount);
            if (Amount <= 0)
                return;
            if (!int.TryParse(UserIdString, out UserId))
                return;
            if (!GetTradeNo.CheckRemoveTradeNo(TradeNo))
                return;
            WIDtotal_amount.Text = Amount.ToString("F2");
            WIDout_trade_no.Text = TradeNo;// DateTime.Now.Ticks.ToString();    
            WIDtotal_amount.ReadOnly = true;
            WIDbody.ReadOnly = true;
            WIDsubject.ReadOnly = true;
            WIDout_trade_no.ReadOnly = true;
            BtnAlipay_Click(null, null);

            PayData newAliPayData = new PayData();
            newAliPayData.Amount = float.Parse(AmountString);
            newAliPayData.UserId = UserId;
            newAliPayData.TradeNo = WIDout_trade_no.Text.Trim();
            newAliPayData.PayType = PayType.ALI_PAY;
            PayData.Add(newAliPayData.TradeNo, newAliPayData);
        }
        protected void BtnAlipay_Click(object sender, EventArgs e)
        {
            DefaultAopClient client = new DefaultAopClient(config.gatewayUrl, config.app_id, config.private_key, "json", "1.0", config.sign_type, config.alipay_public_key, config.charset, false);
            // 外部订单号，商户网站订单系统中唯一的订单号
            //WIDout_trade_no.Text = DateTime.Now.ToLongTimeString();
            string out_trade_no = WIDout_trade_no.Text.Trim();

            // 订单名称
            string subject = WIDsubject.Text.Trim();

            // 付款金额
            string total_amout = WIDtotal_amount.Text.Trim();

            // 商品描述
            string body = WIDbody.Text.Trim();

            // 支付中途退出返回商户网站地址
            string quit_url = WIDquit_url.Text.Trim();

            // 组装业务参数model
            AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
            model.Body = body;
            model.Subject = subject;
            model.TotalAmount = total_amout;
            model.OutTradeNo = out_trade_no;
            model.ProductCode = "QUICK_WAP_WAY";
            model.QuitUrl = quit_url;

            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            // 设置支付完成同步回调地址
            // request.SetReturnUrl("");
            // 设置支付完成异步通知接收地址
            request.SetNotifyUrl("http://" + Request.Url.Authority + "/GameWebApplication/AliNotify_url.aspx");
            Debug.Log("request.SetNotifyUrl", "http://" + Request.Url.Authority + "/GameWebApplication/AliNotify_url.aspx");
            Debug.Log("UserId", UserId.ToString());
            // 将业务model载入到request
            request.SetBizModel(model);

            AlipayTradeWapPayResponse response = null;
            try
            {
                response = client.pageExecute(request, null, "post");
                Response.Write(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}

//         protected void BtnAlipay_Click(object sender, EventArgs e)
//         {
//             string AmountString = Request.QueryString["Amount"];
//             float Amount;
//             float.TryParse(AmountString, out Amount);
//             if (Amount <= 0)
//                 return;
//             DefaultAopClient client = new DefaultAopClient(config.gatewayUrl, config.app_id, config.private_key, "json", "1.0", config.sign_type, config.alipay_public_key, config.charset, false);
//             // 外部订单号，商户网站订单系统中唯一的订单号
//             string out_trade_no = WIDout_trade_no.Text.Trim();
// 
//             // 订单名称
//             string subject = "支付宝充值";
// 
//             // 付款金额
//             string total_amout = Amount.ToString("f2");
// 
//             // 商品描述
//             string body = "支付宝充值";
// 
//             // 支付中途退出返回商户网站地址
// //             string quit_url = WIDquit_url.Text.Trim();
// 
//             // 组装业务参数model
//             AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
//             model.Body = body;
//             model.Subject = subject;
//             model.TotalAmount = total_amout;
//             model.OutTradeNo = out_trade_no;
//             model.ProductCode = "QUICK_WAP_WAY";
// //             model.QuitUrl = quit_url;
// 
//             AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
//             // 设置支付完成同步回调地址
//             // request.SetReturnUrl("");
//             // 设置支付完成异步通知接收地址
//             
//             request.SetNotifyUrl("http://"+Request.Url.Authority+"Pay/AliNotify_url.aspx");
//             // 将业务model载入到request
//             request.SetBizModel(model);
// 
//             AlipayTradeWapPayResponse response = null;
//             try
//             {
//                 response = client.pageExecute(request, null, "post");
//                 Response.Write(response.Body);
//             }
//             catch (Exception exp)
//             {
//                 throw exp;
//             }
//         }
