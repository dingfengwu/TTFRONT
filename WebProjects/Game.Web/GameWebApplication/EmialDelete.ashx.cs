using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// EmialDelete 的摘要说明
    /// </summary>
    public class EmialDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
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
                cmd.CommandText = "[GSP_GP_DeleteAccountEmail]";
                cmd.Parameters.Add(new SqlParameter("@dwUserID", newPhoneLoginInfo.dwUserID));
                cmd.Parameters.Add(new SqlParameter("@mailId", newPhoneLoginInfo.dwMailId));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                CommonTools.SendStringToClient(context, 0, "");
            }
            catch (Exception exp)
            {

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