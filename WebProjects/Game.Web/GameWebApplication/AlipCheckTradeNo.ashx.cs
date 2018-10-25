using Game.Facade;
using Game.Web.AppPay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// AlipCheckTradeNo 的摘要说明
    /// </summary>
    public class AlipCheckTradeNo : IHttpHandler
    {
        public class CRequestPayTradeNo
        {
            public int code = 0;    //0 成功
            public string msg = "";
            public float Amount = 0;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain;charset=UTF-8";

            string TraderNo = context.Request.QueryString["TraderNo"];
            CRequestPayTradeNo newRequestPayTradeNo = new CRequestPayTradeNo();
            PayData _Data = PayData.Find(TraderNo, PayType.ALI_PAY);
            if (_Data == null)
            {
                newRequestPayTradeNo.code = 1;
                newRequestPayTradeNo.msg = "未找到此交易定单:" + TraderNo;
            }
            else if (_Data.Status == 0)
            {
                newRequestPayTradeNo.Amount = _Data.Amount;
                newRequestPayTradeNo.code = 2;
                newRequestPayTradeNo.msg = "等待支付宝返回结果";
            }
            else 
            {
                //                 DataSet ds = FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().ExecuteDataset(CommandType.Text,
                //                     "select Score from GameScoreInfo where UserID=" + _Data.UserId);
                //                 if (ds.Tables[0].Rows.Count > 0)
                //                 {
                //                     int src = Convert.ToInt32(ds.Tables[0].Rows[0]["Score"]);
                //                     newRequestPayTradeNo.Amount = src;
                //                 }
                newRequestPayTradeNo.Amount = _Data.Amount;
                newRequestPayTradeNo.code = 0;
                newRequestPayTradeNo.msg = "";
            }
            context.Response.Write(LitJson.JsonMapper.ToJson(newRequestPayTradeNo));
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