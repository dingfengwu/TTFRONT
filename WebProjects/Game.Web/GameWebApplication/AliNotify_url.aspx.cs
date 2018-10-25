using Aop.Api.Util;
using Game.Facade;
using Game.Web.AppPay;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
    public partial class AliNotify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamReader sm = new System.IO.StreamReader(Request.InputStream);
                string MoneyRequestStr = sm.ReadToEnd();

                Debug.Log("AliNotify_url", MoneyRequestStr);


                /* 实际验证过程建议商户添加以下校验。
                1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
                2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
                3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
                4、验证app_id是否为该商户本身。
                */
                Dictionary<string, string> sArray = GetRequestPost();
                Debug.Log("AliNotify_url－sArray", sArray.Count.ToString());

                if (sArray.Count != 0)
                {
                    bool flag = true;
                    //bool flag = AlipaySignature.RSACheckV1(sArray, config.alipay_public_key, config.charset, config.sign_type, false);
                    if (flag)
                    {
                        //交易状态
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                        string trade_status = Request.Form["trade_status"];
                        string out_trade_no = Request.Form["out_trade_no"];//商户订单号              
                        string total_fee = Request.Form["total_amount"];//交易金额
                        string buyer_id = Request.Form["buyer_id"];//买家支付宝用户号
                        string buyer_email = Request.Form["buyer_email"];//买家支付宝账号
                        string trade_no = Request.Form["trade_no"];//支付宝交易号                   

                        /*
                        string notify_id = Request.Form["notify_id"];//通知校验ID
                        string sign = Request.Form["sign"];//签名             
                        string subject = Request.Form["subject"];//商品名称
                        string quantity = Request.Form["quantity"];//购买数量
                        string price = Request.Form["price"];//商品单价
                        string body = Request.Form["body"];//商品描述
                        string gmt_create = Request.Form["gmt_create"];//交易创建时间
                        string gmt_payment = Request.Form["gmt_payment"];//交易付款时间
                         * */
                        PayData _Data = PayData.Find(out_trade_no,PayType.ALI_PAY);
                        if (_Data == null)
                        {
                            Debug.Log("Not find out_trade_no", out_trade_no);
                            _Data = PayData.Find(trade_no, PayType.ALI_PAY);
                        }
                        if (_Data == null || _Data.Status == 1)
                        {
                            Debug.Log("Not find trade_no", trade_no);
                            return;
                        }
                        _Data.Status = 1;

                        //     @dwUserID INT,								-- 用户 I D
                        // 	@szTradeNo NVARCHAR(50),						-- 用户密码	
                        // 	@szPayTime NVARCHAR(50),					-- 连接地址
                        // 	@fAmount float,					-- 机器标识
                        // 	@PayStatus NVARCHAR(50),				-- 绑定帐号	
                        // 	@szBuyer_ID  NVARCHAR(50),
                        // 	@szBuyer_Email  NVARCHAR(50),
                        // 
                        var prams = new List<DbParameter>();
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwUserID", _Data.UserId));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szTradeNo", out_trade_no));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szPayTime", DateTime.Now.ToString()));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("fAmount", _Data.Amount));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("PayStatus", 1));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBuyer_ID", buyer_id));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBuyer_Email", ""));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szPayType", PayType.ALI_PAY));
                        prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("strErrorDescribe", "suss"));

                        FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().RunProc("GSP_GP_AccountPay", prams);


                        //     public int dwUserID;    // 用户 I D
                        //     public int dwMail;      //邮件ID
                        //     public string szTitle = "邮件名称";  // 邮件名称	
                        //     public int nType;       // 邮件类型
                        //     public int nStatus;      // 邮件状态
                        //     public string szSendTime=""; //收件时间　
                        //     public string szMessage="";// 邮件消息
                        //     public string szSender="";  //发件人

                        JsonEMail newEmail = new JsonEMail();
                        newEmail.dwUserID = _Data.UserId;
                        newEmail.nStatus = 0;
                        newEmail.szTitle = "支付成功";
                        newEmail.szMessage = "支付宝交易：支付成功[" + _Data.Amount.ToString() + "]";
                        newEmail.szSender = "系统";
                        newEmail.szSendTime = DateTime.Now.ToString();

                        if (trade_status == "TRADE_FINISHED")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            //注意：
                            //该种交易状态只在两种情况下出现
                            //1、开通了普通即时到账，买家付款成功后。
                            //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。

                            // 金币入库
                            //                         ShareDetialInfo detailInfo = new ShareDetialInfo();
                            //                         detailInfo.OrderID = out_trade_no;
                            //                         detailInfo.IPAddress = Utility.UserIP;
                            //                         detailInfo.PayAmount = Convert.ToDecimal(total_fee);
                            //                         FacadeManage.aideTreasureFacade.FilliedMobile(detailInfo);
                            float xx = float.Parse(total_fee);
                            WebApplication1.AppleInapp.AddScore((int)(xx * 100), _Data.UserId,trade_no);
                            WebApplication1.EmailAdd.AddEmail(newEmail);
                        }
                        else if (trade_status == "TRADE_SUCCESS")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            //注意：
                            //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。

                            // 金币入库
                            //                         ShareDetialInfo detailInfo = new ShareDetialInfo();
                            //                         detailInfo.OrderID = out_trade_no;
                            //                         detailInfo.IPAddress = Utility.UserIP;
                            //                         detailInfo.PayAmount = Convert.ToDecimal(total_fee);
                            //                         FacadeManage.aideTreasureFacade.FilliedMobile(detailInfo);
//                             WebApplication1.AppleInapp.AddScore((int)float.Parse(total_fee), _Data.UserId);

                            float xx = float.Parse(total_fee);
                            WebApplication1.AppleInapp.AddScore((int)(xx * 100), _Data.UserId, trade_no);


                            WebApplication1.EmailAdd.AddEmail(newEmail);
                        }
                        else
                        {
                        }

                        Response.Write("success");
                    }
                    else
                    {
                        Response.Write("fail");
                    }
                }
            }
            catch (Exception exp)
            {
                Debug.LogException(exp);
            }
        }

        public Dictionary<string, string> GetRequestPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //coll = Request.Form;
            coll = Request.Form;
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
                Debug.Log(requestItem[i], Request.Form[requestItem[i]]);
            }
            return sArray;

        }
    }
