using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// GetTradeNo 的摘要说明
    /// </summary>
    public class GetTradeNo : IHttpHandler
    {
        public class CRequestPayTradeNo
        {
            public int code = 0;
            public string msg = "";
            public string TradeNo = "";
        }
        public static Dictionary<string, string> CreateTradeNo = new Dictionary<string, string>();
        public int Num = 0;
        public static bool CheckRemoveTradeNo(string no)
        {
            lock (CreateTradeNo)
            {
                if (CreateTradeNo.ContainsKey(no))
                {
                    string dd = CreateTradeNo[no];
                    CreateTradeNo.Remove(no);
                    return true;
                }
                return false;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            CRequestPayTradeNo newPayServerInfoCellMgr = new CRequestPayTradeNo();
            while (true)
            {
                lock (CreateTradeNo)
                {
                    newPayServerInfoCellMgr.TradeNo = DateTime.Now.Ticks.ToString() + Num++.ToString();
                    if (CreateTradeNo.ContainsKey(newPayServerInfoCellMgr.TradeNo))
                        continue;
                    CreateTradeNo.Add(newPayServerInfoCellMgr.TradeNo, newPayServerInfoCellMgr.TradeNo);
                    break;
                }
            }
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