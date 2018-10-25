using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using LitJson;
using Game.Web.AppPay;

namespace Game.Web.GameWebApplication.BankPay
{
    /// <summary>
    /// BankPay 的摘要说明
    /// </summary>
    public class BankPay : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            
            var payKey = ConfigurationManager.AppSettings["payKey"];
            var paySecretKey= ConfigurationManager.AppSettings["paySecretKey"];

            string AmountString = context.Request.QueryString["Amount"];
            string UserIdString = context.Request.QueryString["UserId"];
            string TradeNo = context.Request.QueryString["TradeNo"];
            float Amount = 0;
            float.TryParse(AmountString, out Amount);
            if (Amount <= 0)
                return;
            int UserId = 0;
            if (!int.TryParse(UserIdString, out UserId))
                return;
            //检查交易号是否存在
            if (!WebApplication1.GetTradeNo.CheckRemoveTradeNo(TradeNo))
                return;

            var bankPayReq = new BankPayRequest();
            bankPayReq.AddParams("payKey", payKey);
            bankPayReq.AddParams("productName", "load");
            bankPayReq.AddParams("orderNo", TradeNo);
            bankPayReq.AddParams("orderPrice", AmountString);
            bankPayReq.AddParams("payWayCode", "ZITOPAY");
            bankPayReq.AddParams("payTypeCode", "ZITOPAY_185374_BANK_SCAN");
            bankPayReq.AddParams("orderIp", "");
            bankPayReq.AddParams("orderDate", DateTime.Now.ToString("yyyyMMdd"));
            bankPayReq.AddParams("orderTime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            bankPayReq.AddParams("returnUrl", "http://" + context.Request.Url.Authority + "/GameWebApplication/BankPay/BankReturn_Url.aspx");
            bankPayReq.AddParams("notifyUrl", "http://" + context.Request.Url.Authority + "/GameWebApplication/BankPay/BankNotify_Url.ashx");
            bankPayReq.AddParams("orderPeriod", 60);
            bankPayReq.AddParams("remark", "");
            bankPayReq.AddParams("field1", "");
            bankPayReq.AddParams("field2", "");
            bankPayReq.AddParams("field3", "");
            bankPayReq.AddParams("field4", "");
            bankPayReq.AddParams("field5", "");

            var sign = bankPayReq.GetSign(paySecretKey);
            bankPayReq.AddParams("sign", sign);
            var param= bankPayReq.ToParams();

            var url = "http://api.quanyinzf.com:8050/rb-pay-web-gateway/scanPay/initPayIntf?" + param;
            //url = "http://47.75.201.136:9000/proxy.ashx?url=" + HttpUtility.UrlEncode(url);
            var request = System.Net.HttpWebRequest.Create(url);
            request.Method = "GET";
            var sendresponse = request.GetResponse();
            string sendresponsetext = "";
            using (var streamReader = new StreamReader(sendresponse.GetResponseStream()))
            {
                sendresponsetext = streamReader.ReadToEnd().Trim();
            }

            var data = JsonMapper.ToObject(sendresponsetext);
            var result = data["result"].ToString();
            if(result == "success")
            {
                PayData.Add(TradeNo, new PayData()
                {
                    TradeNo = TradeNo,
                    Amount = float.Parse(AmountString),
                    CreateDate = DateTime.Now,
                    Status = 0,
                    PayType=PayType.BANK_CARD,
                    UserId = int.Parse(context.Request["UserId"])
                });
                url = data["code_url"].ToString();
                context.Response.Redirect(url);
            }
            else
            {
                var message = data["msg"].ToString();
                var content = string.Format("<script>alert('{0}');</script>", message);
                context.Response.Write(content);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}