using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Web.Pay.ZFB.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Pay.ZFB
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    string out_trade_no = Request.Form["out_trade_no"];//商户订单号   
                    string notify_id = Request.Form["notify_id"];//通知校验ID
                    string sign = Request.Form["sign"];//签名             
                    string trade_status = Request.Form["trade_status"];//交易状态  
                    string subject = Request.Form["subject"];//商品名称
                    string trade_no = Request.Form["trade_no"];//支付宝交易号                   
                    string buyer_id = Request.Form["buyer_id"];//买家支付宝用户号
                    string buyer_email = Request.Form["buyer_email"];//买家支付宝账号
                    string total_fee = Request.Form["total_fee"];//交易金额
                    string quantity = Request.Form["quantity"];//购买数量
                    string price = Request.Form["price"];//商品单价
                    string body = Request.Form["body"];//商品描述
                    string gmt_create = Request.Form["gmt_create"];//交易创建时间
                    string gmt_payment = Request.Form["gmt_payment"];//交易付款时间

                    if (Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。

                        // 金币入库
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = Convert.ToDecimal(total_fee);
                        FacadeManage.aideTreasureFacade.FilliedMobile(detailInfo);
                    }
                    else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。

                        // 金币入库
                        ShareDetialInfo detailInfo = new ShareDetialInfo();
                        detailInfo.OrderID = out_trade_no;
                        detailInfo.IPAddress = Utility.UserIP;
                        detailInfo.PayAmount = Convert.ToDecimal(total_fee);
                        FacadeManage.aideTreasureFacade.FilliedMobile(detailInfo);
                    }
                    else
                    {
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    Response.Write("success");  //请不要修改或删除

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}