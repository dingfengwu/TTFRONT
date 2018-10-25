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

namespace Game.Web.Pay.JFTAPP
{
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string p1_usercode = Request.Form["p1_usercode"];//商户在骏付通的商户号   
            string p2_order = Request.Form["p2_order"];//商户提交的订单号
            string p3_money = Request.Form["p3_money"];//金额             
            string p4_status = Request.Form["p4_status"];//支付结果  
            string p5_jtpayorder = Request.Form["p5_jtpayorder"];//竣付通订单号
            string p6_paymethod = Request.Form["p6_paymethod"];//商户支付方式                   
            string p7_paychannelnum = Request.Form["p7_paychannelnum"];//支付通道编码
            string p8_charset = Request.Form["p8_charset"];//编码格式
            string p9_signtype = Request.Form["p9_signtype"];//签名验证方式
            string p10_sign = Request.Form["p10_sign"];//验证签名
            string p11_remark = Request.Form["p11_remark"];//备注，原样返回用户提交的p24_remark信息     
            string key = ApplicationSettings.Get("jftBankKey");

            //验证签名  
            String merchantSignMsgVal = p1_usercode + "&" + p2_order + "&" + p3_money + "&" + p4_status + "&" + p5_jtpayorder + "&" + p6_paymethod + "&" + p7_paychannelnum + "&" + p8_charset + "&" + p9_signtype + "&" + key;
            String merchantSignMsg = GetMD5(merchantSignMsgVal, "utf-8").ToUpper();

            //进行签名字符串验证
            if (p10_sign.ToUpper() == merchantSignMsg.ToUpper())
            {
                if (p4_status == "1")
                {
                    // 金币入库
                    ShareDetialInfo detailInfo = new ShareDetialInfo();
                    detailInfo.OrderID = p2_order;
                    detailInfo.IPAddress = Utility.UserIP;
                    detailInfo.PayAmount = Convert.ToDecimal(Convert.ToDecimal(p3_money));
                    Message umsg = FacadeManage.aideTreasureFacade.FilliedMobile(detailInfo);
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("FAILD");
                }
            }
            else
            {
                Response.Write("FAILD");
            }
        }

        #region 公共方法
        
        //功能函数。将字符串进行编码格式转换，并进行MD5加密，然后返回。开始
        private string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        #endregion
    }
}