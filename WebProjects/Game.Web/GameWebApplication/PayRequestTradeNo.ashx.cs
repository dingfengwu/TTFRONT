using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// PayServerInfo 的摘要说明
    /// </summary>
    public class RequestPayTradeNo : IHttpHandler
    {
        public class CRequestPayTradeNo
        {
            public int code = 0;
            public string msg = "";
            public string TradeNo = "";
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            CRequestPayTradeNo newPayServerInfoCellMgr = new CRequestPayTradeNo();
            newPayServerInfoCellMgr.TradeNo = DateTime.Now.Ticks.ToString();
            context.Response.Write(LitJson.JsonMapper.ToJson(newPayServerInfoCellMgr));
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