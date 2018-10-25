using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.Pay.ZFB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Pay.ZFB
{
    public partial class AlipayNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = AliPayHelper.GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                bool verifyResult = AliPayHelper.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)
                {
                    string out_trade_no = Request.Form["out_trade_no"];
                    string trade_no = Request.Form["trade_no"];
                    string trade_status = Request.Form["trade_status"];

                    if (Request.Form["trade_status"] == "TRADE_SUCCESS" || Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = Convert.ToDecimal(Request.Form["total_fee"]);
                        Message umsg = FacadeManage.aideTreasureFacade.FilliedOnline(detailInfo, 0);
                    }

                    Response.Write("success");
                }
                else
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }
    }
}