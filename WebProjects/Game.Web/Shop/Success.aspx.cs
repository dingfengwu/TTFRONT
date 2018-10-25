using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using Game.Entity.NativeWeb;

namespace Game.Web.Shop
{
    public partial class Success : UCPageBase
    {
        protected string orderID = string.Empty;         //订单号码
        protected int totalAmount = 0;                   //消费金额

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IntParam == 0)
            {
                return;
            }

            AwardOrder awardOrder = FacadeManage.aideNativeWebFacade.GetAwardOrder(IntParam, Fetch.GetUserCookie().UserID);
            if (awardOrder == null)
            {
                return;
            }

            lblAlertIcon.CssClass = "ui-result-pic-1";
            lblAlertInfo.CssClass = "ui-result-success";
            lblAlertInfo.Text = string.Format("恭喜您，兑换成功。您的订单号：{0}，本次消费元宝：{1}。", awardOrder.OrderID, awardOrder.TotalAmount);
        }
    }
}