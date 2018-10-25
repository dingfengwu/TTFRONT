using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// EmailAdd 的摘要说明
    /// </summary>
    public class EmailAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DataString = CommonTools.GetRequest(context);
            JsonEMail newPhoneLoginInfo = LitJson.JsonMapper.ToObject<JsonEMail>(DataString);
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];
//             string MyConn = "server=103.105.58.140;uid=testdb;pwd=123abc;database=RYAccountsDB;Trusted_Connection=no";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = MyConnection;
                cmd.CommandText = "GSP_MB_AccountsEmailAdd";
                cmd.Parameters.Add(new SqlParameter("@dwUserID", newPhoneLoginInfo.dwUserID));
                cmd.Parameters.Add(new SqlParameter("@szTitle", newPhoneLoginInfo.szTitle));
                cmd.Parameters.Add(new SqlParameter("@nType", newPhoneLoginInfo.nType));
                cmd.Parameters.Add(new SqlParameter("@nSatus", newPhoneLoginInfo.nStatus));
                cmd.Parameters.Add(new SqlParameter("@szMessage", newPhoneLoginInfo.szMessage));
                cmd.Parameters.Add(new SqlParameter("@szSender", newPhoneLoginInfo.szSender));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
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
        public static void AddEmail(JsonEMail newPhoneLoginInfo)
        {
            var prams = new List<DbParameter>();

            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwUserID", newPhoneLoginInfo.dwUserID));
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szTitle", newPhoneLoginInfo.szTitle));
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("nType", newPhoneLoginInfo.nType));
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("nSatus", newPhoneLoginInfo.nStatus));
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szMessage", newPhoneLoginInfo.szMessage));
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szDescribe", newPhoneLoginInfo.szTitle));
            prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szSender", newPhoneLoginInfo.szSender));
            
            FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().RunProc("GSP_MB_AccountsEmailAdd", prams);

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