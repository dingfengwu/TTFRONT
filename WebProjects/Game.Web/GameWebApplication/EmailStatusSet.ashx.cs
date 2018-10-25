using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// EmailStatusSet 的摘要说明
    /// </summary>
    public class EmailStatusSet : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string DataString = CommonTools.GetRequest(context);
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];
//             string MyConn = "server=103.105.58.140;uid=testdb;pwd=123abc;database=RYAccountsDB;Trusted_Connection=no";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MailUserAccount newPhoneLoginInfo = LitJson.JsonMapper.ToObject<MailUserAccount>(DataString);
                MyConnection.Open();
                foreach (var item in newPhoneLoginInfo.dwMailIds)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = MyConnection;
                    cmd.CommandText = "GSP_MB_AccountsEmailStatus";
//                     cmd.Parameters.Add(new SqlParameter("@dwUserID", newPhoneLoginInfo.dwUserID));
                    cmd.Parameters.Add(new SqlParameter("@mailId", item));
                    cmd.Parameters.Add(new SqlParameter("@nStatus", newPhoneLoginInfo.nStatus));
                    cmd.Parameters.Add(new SqlParameter("@szDescribe", ""));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                CommonTools.SendStringToClient(context, 0, "Suss");
            }
            catch (Exception exp)
            {
                CommonTools.SendStringToClient(context, 1, "ErrorJson:" + exp.Message.ToString() + "-" + exp.StackTrace.ToString());
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