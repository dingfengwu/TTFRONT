using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// PayServerInfo 的摘要说明
    /// </summary>
    public class PayServerInfo : IHttpHandler
    {
        public class CWxCell
        {
            public string WeiXinName;
            public string WeiXinID;
            public CWxCell(string _WeiXinName, string _WeiXinID)
            {
                WeiXinName = _WeiXinName;
                WeiXinID = _WeiXinID;
            }
        }
        public class PayServerInfoCellMgr
        {
            public int code = 0;
            public string msg = "";
            public List<PayServerInfoCell> PayServerInfoCells = new List<PayServerInfoCell>();
            public List<CWxCell> Wxs = new List<CWxCell>();
        }
        public class PayServerInfoCell
        {
            public string PayType = "ZhiFuBao";
            public string Addr = "";
            public List<double> Moneys = new List<double>();
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            PayServerInfoCell newPayServerInfoCell = new PayServerInfoCell();
            newPayServerInfoCell.Addr = "/GameWebApplication/Wappay.aspx";
            newPayServerInfoCell.Moneys.Add(20);
            newPayServerInfoCell.Moneys.Add(49);
            newPayServerInfoCell.Moneys.Add(99);
            newPayServerInfoCell.Moneys.Add(199);
            newPayServerInfoCell.Moneys.Add(299);
            newPayServerInfoCell.Moneys.Add(399);
            newPayServerInfoCell.Moneys.Add(499);
            newPayServerInfoCell.Moneys.Add(999);
            //
            PayServerInfoCellMgr newPayServerInfoCellMgr = new PayServerInfoCellMgr();
            newPayServerInfoCellMgr.PayServerInfoCells.Add(newPayServerInfoCell);

            CWxCell newCWxCell1 = new CWxCell("支持花呗", "ttvip0888");
            CWxCell newCWxCell2 = new CWxCell("蚂蚁花呗", "ttvip0007");
            CWxCell newCWxCell3 = new CWxCell("光速充值", "ttvip0009");
            newPayServerInfoCellMgr.Wxs.Add(newCWxCell1);
            newPayServerInfoCellMgr.Wxs.Add(newCWxCell2);
            newPayServerInfoCellMgr.Wxs.Add(newCWxCell3);

            //增加银行卡支付
            var cardPay = new PayServerInfoCell();
            cardPay.PayType = "BankPay";
            cardPay.Addr = "/GameWebApplication/BankPay/BankPay.ashx";
            cardPay.Moneys.Add(100);
            cardPay.Moneys.Add(199);
            cardPay.Moneys.Add(299);
            cardPay.Moneys.Add(399);
            cardPay.Moneys.Add(499);
            cardPay.Moneys.Add(999);
            newPayServerInfoCellMgr.PayServerInfoCells.Add(cardPay);

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