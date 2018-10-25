using System;
using System.Collections.Generic;
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
    /// PhoneBind 的摘要说明
    /// </summary>
    public class PhoneBind : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            RetureCode newRetureCode = new RetureCode();
            Stream sm = context.Request.InputStream;
            StreamReader inputData = new StreamReader(sm);
            string DataString = inputData.ReadToEnd();

            try
            {
                BindPhoneNum newPhoneLoginInfo = LitJson.JsonMapper.ToObject<BindPhoneNum>(DataString);
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
                if (BindAccount(newPhoneLoginInfo.AccountId,
                     _CodeData.PhoneNum, CommonTools.GetMD5Hash(Passw), newPhoneLoginInfo.Mac, newRetureCode))
                {
                    newRetureCode.code = 0;
                    newRetureCode.msg = Passw;
                    AppleInapp.AddScoreByBinding(300, newPhoneLoginInfo.AccountId);
                    JsonEMail newJsonEMail = new JsonEMail();
                    newJsonEMail.dwUserID = newPhoneLoginInfo.AccountId;
                    newJsonEMail.nStatus = 0;
                    newJsonEMail.szTitle = "绑定成功";
                    newJsonEMail.szMessage = "绑定成功手机号，赠送3.00";
                    newJsonEMail.szSender = "系统";
                    newJsonEMail.nType = 0;
                    newJsonEMail.nStatus = 0;
                    EmailAdd.AddEmail(newJsonEMail);
                }
            }
            catch(Exception exp)
            {
                newRetureCode.code = 100;
                newRetureCode.msg = DataString+"--"+exp.Message.ToString() + "-" + exp.StackTrace;
            }
            PhoneCodeLogin.SendStringToClient(context, LitJson.JsonMapper.ToJson(newRetureCode));
        }
        public static bool HaveBind(string PhoneName, RetureCode newRetureCode)
        {
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];
//             string MyConn = "Data Source=.; Initial Catalog=RYAccountsDB; User ID=sa_games; Password=sa_gamesluatest; Pooling=true";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
                {
                    string selStr = "select Accounts from AccountsInfo where Accounts='" + PhoneName + "'";
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (_Reader.HasRows)
                    {
                        _Reader.Close();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                newRetureCode.code = 5;
                newRetureCode.msg = ex.Message.ToString();
                Console.WriteLine("{0} Exception caught.", ex);
                return true;
            }
            finally
            {
                if (MyConnection != null)
                    MyConnection.Close();
            }
        }
        public static bool BindAccount(int AccountId, string PhoneName, string Password, string Mac, RetureCode newRetureCode)
        {
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];
//             string MyConn = "Data Source=.; Initial Catalog=RYAccountsDB; User ID=sa_games; Password=sa_gamesluatest; Pooling=true";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
//                 {
//                     string selStr = "select Accounts from AccountsInfo where Accounts='" + PhoneName + "'";
//                     SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
//                     SqlDataReader _Reader = MyCommand.ExecuteReader();
//                     if (_Reader.HasRows)
//                     {
//                         _Reader.Close();
//                         return false;
//                     }
//                 }
                {
                    string updateSql = " update AccountsInfo set LogonPass='" + Password + "',";
                    updateSql += "Accounts='" + PhoneName + "'";
                    updateSql += " where UserID=" + AccountId + " and Accounts<> '" + PhoneName + "'";
                    SqlCommand UpdateCommand = new SqlCommand(updateSql, MyConnection);
                    return UpdateCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                newRetureCode.code = 5;
                newRetureCode.msg = ex.Message.ToString();
                Console.WriteLine("{0} Exception caught.", ex);
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