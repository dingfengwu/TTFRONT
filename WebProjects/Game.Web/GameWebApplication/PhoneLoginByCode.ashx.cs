using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// PhoneLoginByCode 的摘要说明
    /// </summary>
    public class PhoneLoginByCode : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            Stream sm = context.Request.InputStream;
            StreamReader inputData = new StreamReader(sm);
            string DataString = inputData.ReadToEnd();
            LoginReturnCode newRetureCode = new LoginReturnCode();
            try
            {
                CheckPhoneLoginInfo newPhoneLoginInfo = LitJson.JsonMapper.ToObject<CheckPhoneLoginInfo>(DataString);
                CodeData _CodeData = PhoneCode.GPhoneCode.CheckCode(newPhoneLoginInfo.CountryCode,
                    newPhoneLoginInfo.PhoneNum, newPhoneLoginInfo.PhoneCode);
                if (_CodeData == null)
                {
                    newRetureCode.code = 4;
                    newRetureCode.msg = "无效验证码";
                    PhoneCodeLogin.SendStringToClient(context, LitJson.JsonMapper.ToJson(newRetureCode));
                    return;
                }
                string Passw = CommonTools.CreatePassWord(12);
                int UserId = -1;
                if (AddAccount(_CodeData.PhoneNum, CommonTools.GetMD5Hash(Passw), newPhoneLoginInfo.Mac, newRetureCode, 
                    out UserId))
                {
                    //AppleInapp.AddScore(600, UserId);
                    newRetureCode.code = 0;
                    newRetureCode.msg = Passw;
                }
            }
            catch(Exception exp)
            {
                newRetureCode.code = 100;
                newRetureCode.msg = DataString + "--" + exp.Message.ToString() + "-" + exp.StackTrace;
            }
            PhoneCodeLogin.SendStringToClient(context, LitJson.JsonMapper.ToJson(newRetureCode));
        }
        public static bool AddAccount(string PhoneName, string Password, string Mac, RetureCode newRetureCode, out int IntId)
        {
            IntId = -1;
            string insetsql = "";
            {
                {
                    insetsql = string.Format(@"INSERT INTO [RYAccountsDB].[dbo].[AccountsInfo]
           ([ProtectID]
           ,[PasswordID]
           ,[SpreaderID]
           ,[Accounts]
           ,[NickName]
           ,[RegAccounts]
           ,[UnderWrite]
           ,[PassPortID]
           ,[Compellation]
           ,[LogonPass]
           ,[InsurePass]
           ,[DynamicPass]
           ,[DynamicPassTime]
           ,[FaceID]
           ,[CustomID]
           ,[Present]
           ,[UserMedal]
           ,[Experience]
           ,[GrowLevelID]
           ,[LoveLiness]
           ,[UserRight]
           ,[MasterRight]
           ,[ServiceRight]
           ,[MasterOrder]
           ,[MemberOrder]
           ,[MemberOverDate]
           ,[MemberSwitchDate]
           ,[CustomFaceVer]
           ,[Gender]
           ,[Nullity]
           ,[NullityOverDate]
           ,[StunDown]
           ,[MoorMachine]
           ,[IsAndroid]
           ,[WebLogonTimes]
           ,[GameLogonTimes]
           ,[PlayTimeCount]
           ,[OnLineTimeCount]
           ,[LastLogonIP]
           ,[LastLogonDate]
           ,[LastLogonMobile]
           ,[LastLogonMachine]
           ,[RegisterIP]
           ,[RegisterDate]
           ,[RegisterMobile]
           ,[RegisterMachine]
           ,[RegisterOrigin]
           ,[PlatformID]
           ,[UserUin]
           ,[RankID]
           ,[AgentID])
     VALUES(
           0,
           0,
           0,
           '{0}',
           '{1}',
           '',
           '',
           '',
           '',
           '{2}',
           '',
           '',
           '',
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           '',
           '',
           '',
           '',
           '',
           '',
           '{0}',
           '{3}',
           0,
           0,
           '',
           0,
           0)", PhoneName, "游客", Password, Mac);

                }
            }
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];

            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
                {
                    string selStr = "select UserId from AccountsInfo where Accounts='" + PhoneName + "'";
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (!_Reader.Read())
                    {
                        _Reader.Close();
                    }
                    else
                    {
                        IntId = _Reader.GetInt32(0);
                        _Reader.Close();
                        string updateSql = " update AccountsInfo set LogonPass='" + Password + "'";
                        updateSql += " where Accounts='" + PhoneName + "'";
                        SqlCommand UpdateCommand = new SqlCommand(updateSql, MyConnection);
                        UpdateCommand.ExecuteNonQuery();

                        return true;
                    }
                }
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = MyConnection;
                    cmd.CommandText = "NET_PM_AddAccount";

                    cmd.Parameters.Add(new SqlParameter("@strAccounts", PhoneName));
                    cmd.Parameters.Add(new SqlParameter("@strNickName", "游客"));
                    cmd.Parameters.Add(new SqlParameter("@strLogonPass", Password));
                    cmd.Parameters.Add(new SqlParameter("@strInsurePass", ""));
                    cmd.Parameters.Add(new SqlParameter("@strDynamicPass", ""));
                    cmd.Parameters.Add(new SqlParameter("@strRegisterMachine", Mac));
                    cmd.Parameters.Add(new SqlParameter("@IsAndroid", "0"));
                    cmd.Parameters.Add(new SqlParameter("@dwFaceID", "0"));
                    cmd.Parameters.Add(new SqlParameter("@dwPhoneLoginScore", 300));
                    cmd.Parameters.Add(new SqlParameter("@strErrorDescribe", ""));
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                {
                    string selStr = "select UserId from AccountsInfo where Accounts='" + PhoneName + "'";
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (!_Reader.Read())
                    {
                        Debug.Log("error", "error new phone login " + PhoneName);
                        _Reader.Close();
                    }
                    else
                    {
                        IntId = _Reader.GetInt32(0);
                        Debug.Log("suss", "suss new phone login " + IntId);
                        _Reader.Close();
                    }   
                    
                    JsonEMail newJsonEMail = new JsonEMail();
                    newJsonEMail.dwUserID = IntId;
                    newJsonEMail.nStatus = 0;
                    newJsonEMail.szTitle = "绑定成功";
                    newJsonEMail.szMessage = "绑定成功手机号，赠送3.00";
                    newJsonEMail.szSender = "系统";
                    newJsonEMail.nType = 0;
                    newJsonEMail.nStatus = 0;
                    EmailAdd.AddEmail(newJsonEMail);

                }

//                 {
//                     SqlCommand UpdateCommand = new SqlCommand(insetsql, MyConnection);
//                      UpdateCommand.ExecuteNonQuery();
//                 {
//                     string selStr = "select UserId from AccountsInfo where Accounts='" + PhoneName + "'";
//                     SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
//                     SqlDataReader _Reader = MyCommand.ExecuteReader();
//                     if (_Reader.Read())
//                     {
//                         IntId = _Reader.GetInt32(0);
//                     }
//                     _Reader.Close();
//                 }
                MyConnection.Close();
                MyConnection = null;
//                 AppleInapp.AddScore(300, IntId);
//                 }
                return true;
            }
            catch (Exception ex)
            {
                newRetureCode.msg = ex.Message.ToString();
            }
            finally
            {
                if (MyConnection != null)
                    MyConnection.Close();
            }
            return false;
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