using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// ReadEmail 的摘要说明
    /// </summary>
    public class ReadEmail : IHttpHandler
    {
        public class MailRetureCode : RetureCode
        {
            public List<JsonEMail> emails = new List<JsonEMail>();
        }
        public void ProcessRequest(HttpContext context)
        {
            MailRetureCode newMailRetureCode = new MailRetureCode();
            string DataString = CommonTools.GetRequest(context);
            UserAccount newPhoneLoginInfo = LitJson.JsonMapper.ToObject<UserAccount>(DataString);
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];
//             string MyConn = "server=103.105.58.140;uid=testdb;pwd=123abc;database=RYAccountsDB;Trusted_Connection=no";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = MyConnection;
                cmd.CommandText = "GSP_MB_AccountsEmailRead";
                cmd.Parameters.Add(new SqlParameter("@dwUserID", newPhoneLoginInfo.dwUserID));
                cmd.Parameters.Add(new SqlParameter("@szDescribe", ""));
                
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader _SqlDataReader = cmd.ExecuteReader();
                while (_SqlDataReader.Read())
                {
                    JsonEMail newJsonEMail = new JsonEMail();
                    newJsonEMail.dwUserID = newPhoneLoginInfo.dwUserID;
                    newJsonEMail.dwMail = int.Parse(_SqlDataReader.GetSqlValue(0).ToString());
                    //newJsonEMail.dwMail = _SqlDataReader.GetInt32(1);
                    newJsonEMail.szTitle = _SqlDataReader.GetSqlValue(2).ToString();
                    newJsonEMail.nType = int.Parse(_SqlDataReader.GetSqlValue(3).ToString());
                    newJsonEMail.nStatus = int.Parse(_SqlDataReader.GetSqlValue(4).ToString());
                    newJsonEMail.szSendTime = _SqlDataReader.GetSqlValue(5).ToString();
                    newJsonEMail.szMessage = _SqlDataReader.GetSqlValue(6).ToString();
                    newJsonEMail.szMessage = newJsonEMail.szMessage.Replace("\\n", "\n");
                    newJsonEMail.szSender = _SqlDataReader.GetSqlValue(7).ToString();
                    
                    newMailRetureCode.emails.Add(newJsonEMail);

                }
                _SqlDataReader.Close();
                CommonTools.SendStringToClient(context, newMailRetureCode);
                return;
            }
            catch (Exception exp)
            {
                newMailRetureCode.code = 1;
                CommonTools.SendStringToClient(context, 1, "ErrorJson:" + exp.Message.ToString()+"-"+exp.StackTrace.ToString());
            }
            finally
            {
                MyConnection.Close();
            }
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