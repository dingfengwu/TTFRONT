using LitJson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using static WebApplication1.AlipCheckTradeNo;

namespace Game.Web.GameWebApplication.BankPay
{
    /// <summary>
    /// Summary description for BankPayCheck
    /// </summary>
    public class BankPayCheck : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain;charset=UTF-8";

            var payKey = ConfigurationManager.AppSettings["payKey"];
            var paySecretKey = ConfigurationManager.AppSettings["paySecretKey"];
            string orderNo = context.Request.QueryString["TraderNo"];
            
            var request = new BankPayRequest();
            request.AddParams("payKey", payKey);
            request.AddParams("orderNo", orderNo);
            var sign = request.GetSign(paySecretKey);
            request.AddParams("sign", sign);
            var param = request.ToParams();
            var url = "http://api.quanyinzf.com:8050/rb-pay-web-gateway/scanPay/orderQuery?" + param;
            //url = "http://47.75.201.136:9000/proxy.ashx?url=" + HttpUtility.UrlEncode(url);
            var httpRequest = HttpWebRequest.Create(url);
            httpRequest.Method = "GET";
            using (var reader = new StreamReader(httpRequest.GetResponse().GetResponseStream()))
            {
                var content = reader.ReadToEnd().Trim();
                var data = JsonMapper.ToObject(content);
                if (data["result"].ToString() == "success")
                {
                    var payRes = data["pay_result"].ToString();
                    if (payRes != "payed")
                    {
                        WriteResult(false, "充值失败", context);
                        return;
                    }
                }
                else
                {
                    WriteResult(false, data["result_msg"].ToString(), context);
                    return;
                }
            }
            WriteResult(true,"成功", context);
        }

        private void WriteResult(bool result,string msg, HttpContext context)
        {
            Debug.LogError("Bank Pay Check", msg);

            var data = new CRequestPayTradeNo();
            data.code = result ? 0 : 1;
            data.msg = msg;
            context.Response.Write(LitJson.JsonMapper.ToJson(data));
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