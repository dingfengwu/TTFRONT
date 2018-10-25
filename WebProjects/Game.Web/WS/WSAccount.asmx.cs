using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using System.Text;
using System.Collections.Generic;
using Game.Kernel;
using System.Web.Script.Serialization;
using Game.Entity.NativeWeb;

namespace Game.Web.WS
{
    /// <summary>
    /// WSAccount 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class WSAccount : System.Web.Services.WebService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string Logon(string userName, string userPass, string code)
        {
            string msg = "";
            
            if(TextUtility.EmptyTrimOrNull(userName) || TextUtility.EmptyTrimOrNull(userPass))
            {
                msg = "抱歉！您输入的帐号或密码错误了。";
                return "{success:'error',msg:'" + msg + "'}";
            }

            //验证码错误
            if(!code.Equals(Fetch.GetVerifyCode(), StringComparison.InvariantCultureIgnoreCase))
            {
                msg = "抱歉！您输入的验证码错误了。";
                return "{success:'error',msg:'" + msg + "'}";
            }

            Message umsg = FacadeManage.aideAccountsFacade.Logon(userName, userPass);
            if(umsg.Success)
            {
                UserInfo ui = umsg.EntityList[0] as UserInfo;
                Fetch.SetUserCookie(ui.ToUserTicketInfo());
                Template tm = new Template("/Template/UserInfo.html");
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("accounts", ui.Accounts);
                dic.Add("gameID", ui.GameID);
                dic.Add("userType", ui.MemberOrder == 0 ? "普通会员" : ui.MemberOrder == 1 ? "蓝钻会员" : ui.MemberOrder == 2 ? "黄钻会员" : ui.MemberOrder == 3 ? "白钻会员" : ui.MemberOrder == 4 ? "红钻会员" : "VIP");
                dic.Add("loveLiness", ui.LoveLiness);
                dic.Add("faceUrl", FacadeManage.aideAccountsFacade.GetUserFaceUrl(ui.FaceID, ui.CustomID));
                tm.VariableDataScoureList = dic;
                Dictionary<string, object> jsonDic = new Dictionary<string, object>();
                jsonDic.Add("success", "success");
                jsonDic.Add("html", tm.OutputHTML());
                return new JavaScriptSerializer().Serialize(jsonDic);
            }
            else
            {
                msg = "{success:'error',msg:'" + umsg.Content + "'}";
            }
            return msg;
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetHeadUserInfo()
        {
            AjaxJsonValid ajaxJson = new AjaxJsonValid();
            Template tm;
            if(Fetch.IsUserOnline())
            {
                tm = new Template("/Template/HeadUserInfo.html");
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("accounts", Fetch.GetUserCookie().Accounts);
                tm.VariableDataScoureList = dic;
            }
            else
            {
                tm = new Template("/Template/HeadNotLogon.html");
            }

            ajaxJson.AddDataItem("html", tm.OutputHTML());
            ajaxJson.SetValidDataValue(true);
            return ajaxJson.SerializeToJson();
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetUserInfo()
        {
            UserTicketInfo userTick = Fetch.GetUserCookie();
            if(userTick == null)
                return "{}";
            Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(userTick.UserID, 0, "");
            if(umsg.Success)
            {
                UserInfo ui = umsg.EntityList[0] as UserInfo;
                string mOrder = ui.MemberOrder == 0 ? "普通会员" : ui.MemberOrder == 1 ? "蓝钻会员" : ui.MemberOrder == 2 ? "黄钻会员" : ui.MemberOrder == 3 ? "白钻会员" : ui.MemberOrder == 4 ? "红钻会员" : "VIP";
                return "{success:'success',account:'" + ui.Accounts + "',gid:'" + ui.GameID + "',loves:'" + ui.LoveLiness + "',morder:'" + mOrder + "',fid:'" + ui.FaceID + "'}";
            }
            return "{}";
        }

        /// <summary>
        /// 检测帐号
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [WebMethod]
        public string CheckName(string userName)
        {
            Message umsg = FacadeManage.aideAccountsFacade.IsAccountsExist(userName);
            if(umsg.Success)
            {
                return "{success:'success'}";
            }
            return "{success:'error',msg:'" + umsg.Content + "'}";
        }

        /// <summary>
        /// 检测昵称
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [WebMethod]
        public string CheckNickName(string nickName)
        {
            Message umsg = FacadeManage.aideAccountsFacade.IsNickNameExist(nickName);
            if(umsg.Success)
            {
                return "{success:'success'}";
            }
            return "{success:'error',msg:'" + umsg.Content + "'}";
        }

        /// <summary>
        /// 用户魅力排名
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetUserLoves()
        {
            StringBuilder msg = new StringBuilder();
            IList<UserInfo> users = FacadeManage.aideAccountsFacade.GetUserInfoOrderByLoves();
            if(users == null)
                return "{}";
            msg.Append("[");
            foreach(UserInfo user in users)
            {
                msg.Append("{userName:'" + user.NickName + "',loves:'" + user.LoveLiness + "'},");
            }
            msg.Remove(msg.Length - 1, 1);
            msg.Append("]");
            return msg.ToString();
        }

        /// <summary>
        /// 帐号申诉
        /// </summary>
        /// <param name="context"></param>
        [WebMethod]
        public string AccountReport()
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
                return ajaxJson.SerializeToJson();
            }

            //验证申诉帐号
            msg = InputDataValidate.CheckingUserNameFormat(account);
            if(!msg.Success)
            {
                ajaxJson.msg = "申诉帐号输入有误";
                return ajaxJson.SerializeToJson();
            }

            //验证注册日期
            if(!string.IsNullOrEmpty(regDate))
            {
                if(!Utils.Validate.IsShortDate(regDate))
                {
                    ajaxJson.msg = "注册日期输入有误";
                    return ajaxJson.SerializeToJson();
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
                    return ajaxJson.SerializeToJson();
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
                    return ajaxJson.SerializeToJson();
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
                    return ajaxJson.SerializeToJson();
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
                    return ajaxJson.SerializeToJson();
                }
                inputItem++;
            }
            if(!string.IsNullOrEmpty(nicknameTwo))
            {
                msg = InputDataValidate.CheckingNickNameFormat(nicknameTwo);
                if(!msg.Success)
                {
                    ajaxJson.msg = "历史昵称2输入有误";
                    return ajaxJson.SerializeToJson();
                }
                if(nicknameTwo == nicknameOne)
                {
                    ajaxJson.msg = "历史昵称不能相同";
                    return ajaxJson.SerializeToJson();
                }
                inputItem++;
            }
            if(!string.IsNullOrEmpty(nicknameThree))
            {
                msg = InputDataValidate.CheckingNickNameFormat(nicknameThree);
                if(!msg.Success)
                {
                    ajaxJson.msg = "历史昵称3输入有误";
                    return ajaxJson.SerializeToJson();
                }
                if(nicknameThree == nicknameOne || nicknameThree == nicknameTwo)
                {
                    ajaxJson.msg = "历史昵称不能相同";
                    return ajaxJson.SerializeToJson();
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
                    return ajaxJson.SerializeToJson();
                }
                inputItem++;
            }
            if(!string.IsNullOrEmpty(passwordThree))
            {
                if(passwordThree == passwordTwo || passwordThree == passwordOne)
                {
                    ajaxJson.msg = "历史密码不能相同";
                    return ajaxJson.SerializeToJson();
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
                    return ajaxJson.SerializeToJson();
                }
                inputItem++;
            }
            if(questionTwo != "0")
            {
                if(questionOne == questionTwo)
                {
                    ajaxJson.msg = "密保问题不能相同";
                    return ajaxJson.SerializeToJson();
                }
                msg = InputDataValidate.CheckingProtectAnswer(answerOne, 2, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = msg.Content;
                    return ajaxJson.SerializeToJson();
                }
                inputItem++;
            }
            if(questionThree != "0")
            {
                if(questionThree == questionOne || questionThree == questionTwo)
                {
                    ajaxJson.msg = "密保问题不能相同";
                    return ajaxJson.SerializeToJson();
                }
                msg = InputDataValidate.CheckingProtectAnswer(answerOne, 3, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = msg.Content;
                    return ajaxJson.SerializeToJson();
                }
                inputItem++;
            }

            //验证补充资料
            msg = InputDataValidate.CheckingProtectAnswer(suppInfo, true);
            if(!msg.Success)
            {
                ajaxJson.msg = "补全资料太长，最长不能超过200个字符";
                return ajaxJson.SerializeToJson();
            }

            //申诉项数验证
            if(inputItem < 4)
            {
                ajaxJson.msg = "为了保证您的申诉请求审核通过，请输入至少4项资料，不包括补充资料";
                return ajaxJson.SerializeToJson();
            }
            #endregion

            //检测帐号
            Message userMsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(0, 0, account);
            if(!userMsg.Success)
            {
                ajaxJson.msg = "您所申诉的帐号不存在";
                return ajaxJson.SerializeToJson();
            }
            UserInfo userInfo = userMsg.EntityList[0] as UserInfo;
            if(userInfo == null)
            {
                ajaxJson.msg = "您所申诉的帐号不存在";
                return ajaxJson.SerializeToJson();
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
            return ajaxJson.SerializeToJson();
        }
    }
}
