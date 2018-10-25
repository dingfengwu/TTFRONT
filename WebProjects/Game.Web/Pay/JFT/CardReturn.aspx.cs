using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Kernel;

namespace Game.Web.Pay.JFT
{
    public partial class CardReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string p1_usercode = Request["p1_usercode"];
            string p2_order = Request["p2_order"];
            string p3_money = Request["p3_money"];
            string p4_status = Request["p4_status"];
            string p5_jtpayorder = Request["p5_jtpayorder"];
            string p6_paymethod = Request["p6_paymethod"];
            string p7_paychannelnum = Request["p7_paychannelnum"];
            string p8_charset = Request["p8_charset"];
            string p9_signtype = Request["p9_signtype"];
            string p10_sign = Request["p10_sign"];

            // 秘钥
            string key = ApplicationSettings.Get("jftBankKey");

            // 验证签名
            string strEncryption = string.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}&{8}&{9}", p1_usercode, p2_order, p3_money, p4_status, p5_jtpayorder, p6_paymethod, p7_paychannelnum, p8_charset, p9_signtype, key);
            string sign = Utility.MD5(strEncryption).ToUpper();
            if (p10_sign == sign && p4_status == "1")
            {
                // 金币入库
                ShareDetialInfo detailInfo = new ShareDetialInfo();
                detailInfo.OrderID = p2_order;
                detailInfo.IPAddress = Utility.UserIP;
                detailInfo.PayAmount = Convert.ToDecimal(p3_money);
                Message umsg = FacadeManage.aideTreasureFacade.FilliedOnline(detailInfo, 0);
            }

            if (p4_status == "1")
            {
                lblAlertIcon.CssClass = "ui-result-pic-1";
                lblAlertInfo.CssClass = "ui-result-success";
                lblAlertInfo.Text = string.Format("恭喜您，充值成功！您的订单号为：{0}。", p2_order);
            }
            else if (!string.IsNullOrEmpty(p2_order))
            {
                lblAlertIcon.CssClass = "ui-result-pic-2";
                lblAlertInfo.CssClass = "ui-result-fail";
                lblAlertInfo.Text = string.Format("充值失败。请确认卡号与密码是否输入正确！您的订单号为：{0}。", p2_order); 
            }
            else
            {
                lblAlertIcon.CssClass = "ui-result-pic-2";
                lblAlertInfo.CssClass = "ui-result-fail";
                lblAlertInfo.Text = "提交完成。如游戏豆未到账，请确认卡号与密码是否输入正确！";
            }
        }
    }
}