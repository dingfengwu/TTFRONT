using System;
using System.Web;
using System.Web.SessionState;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System.IO;
using System.Drawing;

namespace Game.Web.WS
{
    /// <summary>
    /// Account 的摘要说明
    /// </summary>
    public class Account : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetQueryString("action").ToLower();
            switch(action)
            {
                case "accountreport":
                    AccountReport(context);
                    break;

                case "reportstate":
                    ReportState(context);
                    break;

                case "resetpwdbyreport":
                    ResetPwdByReport(context);
                    break;

                case "uploadface":
                    UploadFace(context);
                    break;

                default:
                    break;
            }
        }

        #region 帐号申诉

        /// <summary>
        /// 帐号申诉
        /// </summary>
        /// <param name="context"></param>
        public void AccountReport(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajaxJson = new AjaxJsonValid();
            int inputItem = 0;                                                          //输入项数

            string reportEmail = GameRequest.GetFormString("reportEmail");            //申诉邮箱
            string account = GameRequest.GetFormString("txtUser");                    //申诉帐号
            string regDate = GameRequest.GetFormString("regDate");                    //注册日期
            string realName = GameRequest.GetFormString("realName");                  //真实姓名
            string idCard = GameRequest.GetFormString("idCard");                      //身份证号
            string mobile = GameRequest.GetFormString("mobile");                      //手机号码
            string nicknameOne = GameRequest.GetFormString("nicknameOne");            //历史昵称1
            string nicknameTwo = GameRequest.GetFormString("nicknameTwo");            //历史昵称2
            string nicknameThree = GameRequest.GetFormString("nicknameThree");        //历史昵称3
            string passwordOne = GameRequest.GetFormString("passwordOne");            //历史密码1
            string passwordTwo = GameRequest.GetFormString("passwordTwo");            //历史密码2
            string passwordThree = GameRequest.GetFormString("passwordThree");        //历史密码3
            string questionOne = GameRequest.GetFormString("questionOne");            //密保问题1
            string answerOne = GameRequest.GetFormString("answerOne");                //密保答案1
            string questionTwo = GameRequest.GetFormString("questionTwo");            //密保问题2
            string answerTwo = GameRequest.GetFormString("answerTwo");                //密保答案2
            string questionThree = GameRequest.GetFormString("questionThree");        //密保问题3
            string answerThree = GameRequest.GetFormString("answerThree");            //密保答案3
            string suppInfo = GameRequest.GetFormString("suppInfo");                  //补充资料

            #region 参数验证

            //验证申诉邮箱
            msg = InputDataValidate.CheckingEmail(reportEmail);
            if(!msg.Success)
            {
                ajaxJson.msg = "申诉结果接受邮箱输入有误";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证申诉帐号
            msg = InputDataValidate.CheckingUserNameFormat(account);
            if(!msg.Success)
            {
                ajaxJson.msg = "申诉帐号输入有误";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            // 验证注册日期
            if(!string.IsNullOrEmpty(regDate))
            {
                if(!Utils.Validate.IsShortDate(regDate))
                {
                    ajaxJson.msg = "注册日期输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }

            //验证真实姓名
            if(!string.IsNullOrEmpty(realName))
            {
                msg = InputDataValidate.CheckingRealNameFormat(realName, true);
                if(!msg.Success)
                {
                    ajaxJson.msg = "真实姓名输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }

            //验证身份证号
            if(!string.IsNullOrEmpty(idCard))
            {
                msg = InputDataValidate.CheckingIDCardFormat(idCard, true);
                if(!msg.Success)
                {
                    ajaxJson.msg = "身份证号输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }

            //验证移动电话
            if(!string.IsNullOrEmpty(mobile))
            {
                msg = InputDataValidate.CheckingMobilePhoneNumFormat(mobile, true);
                if(!msg.Success)
                {
                    ajaxJson.msg = "移动电话输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }

            //验证历史昵称
            if(!string.IsNullOrEmpty(nicknameOne))
            {
                msg = InputDataValidate.CheckingNickNameFormat(nicknameOne);
                if(!msg.Success)
                {
                    ajaxJson.msg = "历史昵称1输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }
            if(!string.IsNullOrEmpty(nicknameTwo))
            {
                msg = InputDataValidate.CheckingNickNameFormat(nicknameTwo);
                if(!msg.Success)
                {
                    ajaxJson.msg = "历史昵称2输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                if(nicknameTwo == nicknameOne)
                {
                    ajaxJson.msg = "历史昵称不能相同";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }
            if(!string.IsNullOrEmpty(nicknameThree))
            {
                msg = InputDataValidate.CheckingNickNameFormat(nicknameThree);
                if(!msg.Success)
                {
                    ajaxJson.msg = "历史昵称3输入有误";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                if(nicknameThree == nicknameOne || nicknameThree == nicknameTwo)
                {
                    ajaxJson.msg = "历史昵称不能相同";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }

            //验证密码
            if(!string.IsNullOrEmpty(passwordOne))
            {
                inputItem++;
            }
            if(!string.IsNullOrEmpty(passwordTwo))
            {
                if(passwordTwo == passwordOne)
                {
                    ajaxJson.msg = "历史密码不能相同";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }
            if(!string.IsNullOrEmpty(passwordThree))
            {
                if(passwordThree == passwordTwo || passwordThree == passwordOne)
                {
                    ajaxJson.msg = "历史密码不能相同";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }

            //验证密保
            if(questionOne != "0")
            {
                msg = InputDataValidate.CheckingProtectAnswer(answerOne, 1, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = msg.Content;
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }
            else
            {
                if(!string.IsNullOrEmpty(answerOne))
                {
                    ajaxJson.msg = "你输入了密保答案1，必须选择密保问题1";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                questionOne = "";
            }
            if(questionTwo != "0")
            {
                if(questionOne == questionTwo)
                {
                    ajaxJson.msg = "密保问题不能相同";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                msg = InputDataValidate.CheckingProtectAnswer(answerTwo, 2, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = msg.Content;
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }
            else
            {
                if(!string.IsNullOrEmpty(answerTwo))
                {
                    ajaxJson.msg = "你输入了密保答案2，必须选择密保问题2";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                questionTwo = "";
            }
            if(questionThree != "0")
            {
                if(questionThree == questionOne || questionThree == questionTwo)
                {
                    ajaxJson.msg = "密保问题不能相同";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                msg = InputDataValidate.CheckingProtectAnswer(answerThree, 3, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = msg.Content;
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                inputItem++;
            }
            else
            {
                if(!string.IsNullOrEmpty(answerThree))
                {
                    ajaxJson.msg = "你输入了密保答案3，必须选择密保问题3";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                questionThree = "";
            }

            //验证补充资料
            msg = InputDataValidate.CheckingProtectAnswer(suppInfo, true);
            if(!msg.Success)
            {
                ajaxJson.msg = "补全资料太长，最长不能超过200个字符";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //申诉项数验证
            if(inputItem < 4)
            {
                ajaxJson.msg = "为了保证您的申诉请求审核通过，请输入至少4项资料，不包括补充资料";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            #endregion 参数验证

            //检测帐号
            Message userMsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(0, 0, account);
            if(!userMsg.Success)
            {
                ajaxJson.msg = "您所申诉的帐号不存在";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            UserInfo userInfo = userMsg.EntityList[0] as UserInfo;
            if(userInfo == null)
            {
                ajaxJson.msg = "您所申诉的帐号不存在";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //申诉实体信息
            LossReport lossReport = new LossReport();
            lossReport.ReportNo = Fetch.GetForgetPwdNumber();
            lossReport.ReportEmail = reportEmail;
            lossReport.Accounts = account;
            lossReport.RegisterDate = regDate;
            lossReport.Compellation = realName;
            lossReport.PassportID = idCard;
            lossReport.MobilePhone = mobile;
            lossReport.OldNickName1 = nicknameOne;
            lossReport.OldNickName2 = nicknameTwo;
            lossReport.OldNickName3 = nicknameThree;
            if(!string.IsNullOrEmpty(passwordOne))
            {
                lossReport.OldLogonPass1 = Utility.MD5(passwordOne);
            }
            if(!string.IsNullOrEmpty(passwordTwo))
            {
                lossReport.OldLogonPass2 = Utility.MD5(passwordTwo);
            }
            if(!string.IsNullOrEmpty(passwordThree))
            {
                lossReport.OldLogonPass3 = Utility.MD5(passwordThree);
            }
            lossReport.ReportIP = GameRequest.GetUserIP();
            lossReport.Random = Utils.TextUtility.CreateRandom(4, 1, 0, 0, 0, "");
            lossReport.GameID = userInfo.GameID;
            lossReport.UserID = userInfo.UserID;
            lossReport.OldQuestion1 = questionOne;
            lossReport.OldResponse1 = answerOne;
            lossReport.OldQuestion2 = questionTwo;
            lossReport.OldResponse2 = answerTwo;
            lossReport.OldQuestion3 = questionThree;
            lossReport.OldResponse3 = answerThree;
            lossReport.SuppInfo = suppInfo;

            //保存数据
            try
            {
                FacadeManage.aideNativeWebFacade.SaveLossReport(lossReport);
                ajaxJson.SetValidDataValue(true);
                string url = string.Format("Complaint-Setp-2.aspx?number={0}&account={1}", lossReport.ReportNo, account);
                ajaxJson.AddDataItem("uri", url);
                ajaxJson.msg = "申诉成功，系统将在2个工作日内处理，申诉结果将会以邮件的形式通知您！请注意查收邮件";
            }
            catch(Exception ex)
            {
                ajaxJson.msg = ex.ToString();
            }
            context.Response.Write(ajaxJson.SerializeToJson());
            return;
        }

        #endregion 帐号申诉

        #region 查询申诉状态

        /// <summary>
        /// 查询申诉状态
        /// </summary>
        /// <param name="context"></param>
        public void ReportState(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajaxJson = new AjaxJsonValid();

            string account = GameRequest.GetFormString("account");                    //申诉帐号
            string reportNo = GameRequest.GetFormString("reportNo");                  //申诉编号
            string verifyCode = GameRequest.GetFormString("code");                    //验证码

            //验证验证码
            if(!verifyCode.Equals(Fetch.GetVerifyCode(), StringComparison.InvariantCultureIgnoreCase))
            {
                ajaxJson.msg = "验证码输入有误";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证申诉帐号
            msg = InputDataValidate.CheckingUserNameFormat(account);
            if(!msg.Success)
            {
                ajaxJson.msg = "申诉帐号输入有误";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证申诉流失号
            msg = InputDataValidate.CheckingReportNo(reportNo, false);
            if(!msg.Success)
            {
                ajaxJson.msg = msg.Content;
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //查询申诉号
            LossReport lossReport = FacadeManage.aideNativeWebFacade.GetLossReport(reportNo, account);
            if(lossReport == null)
            {
                ajaxJson.msg = "帐号的申诉号不存在";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //返回数据
            string state = string.Empty;
            switch(lossReport.ProcessStatus)
            {
                case 0:
                    state = "客服处理中";
                    break;

                case 1:
                    state = "审核成功，注意查看邮件并重置密码";
                    break;

                case 2:
                    state = "审核失败，您的资料填写不正确或者不够详细，请重新申诉";
                    break;

                case 3:
                    state = "更新密码成功";
                    break;
            }
            ajaxJson.AddDataItem("acount", account);
            ajaxJson.AddDataItem("reportNo", reportNo);
            ajaxJson.AddDataItem("state", state);
            ajaxJson.SetValidDataValue(true);
            context.Response.Write(ajaxJson.SerializeToJson());
        }

        #endregion 查询申诉状态

        #region 通过申诉重置密码

        /// <summary>
        /// 通过申诉重置密码
        /// </summary>
        /// <param name="context"></param>
        public void ResetPwdByReport(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajaxJson = new AjaxJsonValid();
            int userId = 0;
            string validateCode = GameRequest.GetFormString("txtCode");

            //验证码验证
            if(!validateCode.Equals(Fetch.GetVerifyCode(), StringComparison.InvariantCultureIgnoreCase))
            {
                ajaxJson.msg = "验证码不正确";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //链接验证
            string number = Utils.GameRequest.GetFormString("number");
            string sign = Utils.GameRequest.GetFormString("sign");
            LossReport lossReport = FacadeManage.aideNativeWebFacade.GetLossReport(number);
            if(lossReport == null)
            {
                ajaxJson.msg = "重置失败，非法操作";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if(lossReport.ProcessStatus == 3)
            {
                ajaxJson.msg = "重置失败，该申诉号已被处理，不能重复操作";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            string key = AppConfig.ReportForgetPasswordKey;
            string confirmSign = Utility.MD5(number + lossReport.UserID + lossReport.ReportDate.ToString() + lossReport.Random + key);
            if(sign != confirmSign)
            {
                ajaxJson.msg = "重置失败，签名错误";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if(DateTime.Now > lossReport.OverDate)
            {
                ajaxJson.msg = "重置失败，该申诉链接已经过期，链接有效期为24个小时";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            userId = lossReport.UserID;

            string password = GameRequest.GetFormString("txtPassword");
            string confirmPassword = GameRequest.GetFormString("txtConfirmPassword");

            //验证密码
            if(password != confirmPassword)
            {
                ajaxJson.msg = "两次输入的密码不一直";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            msg = InputDataValidate.CheckingPasswordFormat(password);
            if(!msg.Success)
            {
                ajaxJson.msg = msg.Content;
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证帐号
            UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userId);
            if(!msg.Success)
            {
                ajaxJson.msg = msg.Content;
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //重置密码
            string oldPass = userInfo.LogonPass;
            userInfo.LogonPass = Utility.MD5(password);
            msg = FacadeManage.aideAccountsFacade.ResetLoginPasswdByLossReport(userInfo, number);
            ajaxJson.msg = msg.Content;
            ajaxJson.SetValidDataValue(msg.Success);
            context.Response.Write(ajaxJson.SerializeToJson());
        }

        #endregion 通过申诉重置密码

        #region 上传自定义头像

        /// <summary>
        /// 上传自定义头像
        /// </summary>
        /// <param name="context"></param>
        public void UploadFace(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajv = new AjaxJsonValid();

            int userID = GameRequest.GetFormInt("userID", 0);               // 用户标识
            string signature = GameRequest.GetFormString("signature");      // 签名数据
            string time = GameRequest.GetFormString("time");                // 过期时间
            string clientIP = GameRequest.GetFormString("clientIP");        // 客户端IP
            string machineID = GameRequest.GetFormString("machineID");      // 机器码ID

            // 验证签名
            //Message message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            //if (!message.Success)
            //{
            //    ajv.msg = message.Content;
            //    context.Response.Write(ajv.SerializeToJson());
            //    return;
            //}

            //// 验证数据
            //if (string.IsNullOrEmpty(clientIP))
            //{
            //    ajv.msg = "请传入IP地址！";
            //    context.Response.Write(ajv.SerializeToJson());
            //    return;
            //}
            //if (string.IsNullOrEmpty(machineID))
            //{
            //    ajv.msg = "请传入机器码ID！";
            //    context.Response.Write(ajv.SerializeToJson());
            //    return;
            //}

            // 最大上传 1M
            int maxSize = 1048576;

            // 验证文件格式
            if (context.Request.Files.Count == 0)
            {
                ajv.msg = "请选择一个头像！";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            HttpPostedFile file = context.Request.Files[0];
            if (file.InputStream == null || file.InputStream.Length == 0)
            {
                ajv.msg = "请上传有效的头像！";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }            
            // 验证头像大小
            if (file.InputStream.Length > maxSize)
            {
                msg.Content = string.Format("头像不能超过 {0} M！", 1);
                context.Response.Write(ajv.SerializeToJson());
                return;
            }            
            // 尝试转化为图片
            System.Drawing.Image image = null;
            try
            {
                image = System.Drawing.Image.FromStream(file.InputStream);
            }
            catch
            {
                image.Dispose();
                msg.Content = string.Format("非法文件，目前只支持图片格式文件,对您使用不便感到非常抱歉。");
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //缩放图片
            Bitmap bitmap = new Bitmap(48, 48);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(image, 0, 0, 48, 48);

            //获取像素
            int x, y, site = 0;
            byte[] b = new byte[48 * 48 * 4];
            for (y = 0; y < 48; y++)
            {
                for (x = 0; x < 48; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    b[site] = pixelColor.B;
                    b[site + 1] = pixelColor.G;
                    b[site + 2] = pixelColor.R;
                    b[site + 3] = 0;
                    site = site + 4;
                }
            }
            
            //保存图片
            AccountsFace accountsFace = new AccountsFace();
            accountsFace.UserID = userID;
            accountsFace.CustomFace = b;
            accountsFace.InsertAddr = GameRequest.GetUserIP();
            accountsFace.InsertTime = DateTime.Now;
            msg = FacadeManage.aideAccountsFacade.InsertCustomFace(accountsFace);
            if (msg.Success)
            {
                AccountsInfo model = msg.EntityList[0] as AccountsInfo;
                ajv.AddDataItem("CustomID", model.CustomID);
            }
            ajv.msg = "上传成功！";
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());

            //释放资源
            image.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}