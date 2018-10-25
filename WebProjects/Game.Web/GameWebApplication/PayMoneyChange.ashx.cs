using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// MoneyChange 的摘要说明
    /// </summary>
    public class MoneyChange : IHttpHandler
    {
        public class MoneyRequest
        {
            public int UserId;
            public int BankAccountType; //0银行卡 , 1 支付宝
            public float Money;
        }
        public class MoneyReturn
        {
            public int code = 1;
            public string msg="";
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                MoneyReturn newMoneyReturn = new MoneyReturn();
                context.Response.ContentType = "text/plain";
                System.IO.StreamReader sm = new System.IO.StreamReader(context.Request.InputStream);
                string MoneyRequestStr = sm.ReadToEnd();
                MoneyRequest _MoneyRequest = LitJson.JsonMapper.ToObject<MoneyRequest>(MoneyRequestStr);
                _MoneyRequest.Money *= 100;
//                 string sql = string.Format("SELECT UserID FROM AccontPayChangeMoney WHERE UserID={0} and ChangeStatus=0", _MoneyRequest.UserId);
//                 {
//                     DataSet ds = FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().ExecuteDataset(CommandType.Text, sql);
//                     if (ds.Tables[0].Rows.Count > 0)
//                     {
//                         newMoneyReturn.code = 1;
//                         newMoneyReturn.msg = "不能重复申请";
//                         context.Response.Write(LitJson.JsonMapper.ToJson(newMoneyReturn));
//                         return;
//                     }
//                 }
                {
                    DataSet ds = FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().ExecuteDataset(CommandType.Text,
                            "select Score from GameScoreInfo where UserID=" + _MoneyRequest.UserId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int src = Convert.ToInt32(ds.Tables[0].Rows[0]["Score"]);
                        if (src < _MoneyRequest.Money)
                        {
                            newMoneyReturn.code = 2;
                            newMoneyReturn.msg = "余额不足";
                            context.Response.Write(LitJson.JsonMapper.ToJson(newMoneyReturn));
                            return;
                        }
                        else
                        {
                            string MyUpdate = "Update GameScoreInfo set Score=Score-" + ((int)_MoneyRequest.Money).ToString() +
                                " where UserID=" + _MoneyRequest.UserId;
                            FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().ExecuteDataset(CommandType.Text, MyUpdate);

                            

                        }
                    }
                }
                var prams = new List<DbParameter>();
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwUserID", _MoneyRequest.UserId));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szRequestTime", DateTime.Now.ToString()));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szCheckTime", DateTime.Now.ToString()));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("fAmount", _MoneyRequest.Money));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwBankAccountType", _MoneyRequest.BankAccountType));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwChangeStatus", 0));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("strErrorDescribe", ""));
                object payId = FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().ExecuteScalar(CommandType.StoredProcedure, "GSP_GP_AccountPayChangeMoney", prams.ToArray());

                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append("EXEC NET_PM_RecordScoreChanged ")
                    .AppendFormat("'{0}',", _MoneyRequest.UserId)
                    .AppendFormat("'{0}',", -_MoneyRequest.Money)
                    .AppendFormat("'{0}',", payId)
                    .AppendFormat("'{0}',", "GET_MONEY")
                    .AppendFormat("'{0}',", "提现")
                    .AppendFormat("'{0}'", "ChangedNumber为提现申请表中的PayId");
                FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().ExecuteNonQuery(sqlBuilder.ToString());

                newMoneyReturn.code = 0;
                newMoneyReturn.msg = "";
 
                context.Response.Write(LitJson.JsonMapper.ToJson(newMoneyReturn));
            }
            catch(Exception exp)
            {
                MoneyReturn newMoneyReturn = new MoneyReturn();
                newMoneyReturn.code = 1;
                newMoneyReturn.msg = exp.Message.ToString();
                context.Response.Write(LitJson.JsonMapper.ToJson(newMoneyReturn));
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