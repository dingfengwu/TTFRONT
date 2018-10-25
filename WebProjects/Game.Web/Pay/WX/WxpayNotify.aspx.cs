using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Pay.WX
{
    public partial class WxpayNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string returnMsg = "<xml> <return_code><![CDATA[{0}]]></return_code> <return_msg><![CDATA[{1}]]></return_msg> </xml>";
            SortedDictionary<string, object> dic = WeiXinHelper.GetReturnData();

            string sign = dic["sign"].ToString();

            if (dic["return_code"].ToString() == "SUCCESS")
            {
                string signLocal = WeiXinHelper.GetMakeSign(dic);

                if (sign == signLocal)
                {
                    decimal amount = Convert.ToDecimal(dic["total_fee"]) / 100M;

                    if (dic["result_code"].ToString() == "SUCCESS")
                    {
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = dic["out_trade_no"].ToString();
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = amount;
                        Message umsg = FacadeManage.aideTreasureFacade.FilliedOnline(detailInfo, 0);

                        Response.Write(string.Format(returnMsg, "SUCCESS", "支付成功！"));
                    }
                    else
                    {
                        Response.Write(string.Format(returnMsg, "FAIL", "微信交易失败！"));
                    }
                }
                else
                {
                    Response.Write(string.Format(returnMsg, "FAIL", "签名错误！"));
                }
            }
            else
            {
                Response.Write(string.Format(returnMsg, "FAIL", "微信交易失败！"));
            }
        }
    }
}