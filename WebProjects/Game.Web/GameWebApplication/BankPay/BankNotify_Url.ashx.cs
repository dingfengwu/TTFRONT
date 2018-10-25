using Game.Entity.Accounts;
using Game.Facade;
using Game.Web.AppPay;
using LitJson;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Game.Web.GameWebApplication.BankPay
{
    /// <summary>
    /// Summary description for BankNotify_Url
    /// </summary>
    public class BankNotify_Url : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var configPayKey = ConfigurationManager.AppSettings["payKey"];
            var paySecretKey = ConfigurationManager.AppSettings["paySecretKey"];

            var requestContent = new StringBuilder();
            foreach(var item in context.Request.Params.Keys)
            {
                requestContent.AppendFormat("{0}={1},", item, context.Request.Params[item.ToString()]);
            }
            Debug.Log("银行卡付款成功提醒", requestContent.ToString());

            //验证签名
            var payKey = context.Request["payKey"];
            var productName = context.Request["productName"];
            var orderNo = context.Request["orderNo"];
            var amount = context.Request["orderPrice"];
            var payWayCode = context.Request["payWayCode"];
            var payPayCode = context.Request["payPayCode"];
            var orderDate = context.Request["orderDate"];
            var orderTime = context.Request["orderTime"];
            var remark = context.Request["remark"];
            var trxNo = context.Request["trxNo"];
            var field1 = context.Request["field1"];
            var field2 = context.Request["field2"];
            var field3 = context.Request["field3"];
            var field4 = context.Request["field4"];
            var field5 = context.Request["field5"];
            var tradeStatus = context.Request["tradeStatus"];
            var reqSign = context.Request["sign"];
            var validateRequest = new BankPayRequest();
            validateRequest.AddParams("payKey", payKey);
            validateRequest.AddParams("productName", productName);
            validateRequest.AddParams("orderNo", orderNo);
            validateRequest.AddParams("orderPrice", amount);
            validateRequest.AddParams("payWayCode", payWayCode);
            validateRequest.AddParams("orderDate", orderDate);
            validateRequest.AddParams("orderTime", orderTime);
            validateRequest.AddParams("remark", remark);
            validateRequest.AddParams("trxNo", trxNo);
            validateRequest.AddParams("field1", field1);
            validateRequest.AddParams("field2", field2);
            validateRequest.AddParams("field3", field3);
            validateRequest.AddParams("field4", field4);
            validateRequest.AddParams("field5", field5);
            validateRequest.AddParams("tradeStatus", tradeStatus);
            var generatedSign = validateRequest.GetSign(paySecretKey);
            if (generatedSign.Equals(reqSign, StringComparison.InvariantCultureIgnoreCase))
            {
                WriteError("签名不匹配", context);
                return;
            }


            //验证参数
            if (payKey != configPayKey)
            {
                WriteError("商户不匹配", context);
                return;
            }
            var prams= new List<DbParameter>();
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szTradeNo", orderNo));
            var result = FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().RunProcObjectList<AccountPay>("GSP_GP_QueryAccountPay", prams);
            if (result != null && result.Count > 0)
            {
                if(result[0].PayStatus == 1)
                {
                    WriteError("此订单已经充值成功", context);
                    return;
                }
            }
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
                        WriteError("充值失败", context);
                        return;
                    }
                }
                else
                {
                    WriteError(data["result_msg"].ToString(), context);
                    return;
                }
            }
            
            var buyer_id= context.Request["payKey"];
            var payData= PayData.Find(orderNo, PayType.BANK_CARD);
            if(tradeStatus == "SUCCESS")
            {
                JsonEMail newEmail = new JsonEMail();
                newEmail.dwUserID = payData.UserId;
                newEmail.nStatus = 0;
                newEmail.szTitle = "支付成功";
                newEmail.szMessage = "银行卡交易：支付成功[" + amount.ToString() + "]";
                newEmail.szSender = "系统";
                newEmail.szSendTime = DateTime.Now.ToString();

                prams = new List<DbParameter>();
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwUserID", payData.UserId));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szTradeNo", orderNo));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szPayTime", DateTime.Now.ToString()));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("fAmount", amount));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("PayStatus", 1));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBuyer_ID", buyer_id));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBuyer_Email", ""));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szPayType", PayType.BANK_CARD));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("strErrorDescribe", "suss"));
                FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().RunProc("GSP_GP_AccountPay", prams);

                WebApplication1.AppleInapp.AddScore((int)(decimal.Parse(amount) * 100), payData.UserId, orderNo);
                WebApplication1.EmailAdd.AddEmail(newEmail);
            }

            context.Response.Write("success");
        }

        private void WriteError(string errorInfo,HttpContext context)
        {
            Debug.LogError(this.GetType().ToString(), errorInfo);

            context.Response.Write(errorInfo);
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