using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// BindCard 的摘要说明
    /// </summary>
    public class PayBindCard : IHttpHandler
    {
        public class MoneyRequest
        {
            public int UserId;
            public int BankAccountType=0; //0银行卡 , 1 支付宝
            public string AccountNo="";    //账号,卡号或支付宝ID
            public string Bank = "";    //工商银行
            public string BankAccountUsername=""; //持卡人姓名
        }
        public class MoneyReturn
        {
            public int code = 1;
            public string msg = "";
        }
        public void ProcessRequest(HttpContext context)
        {
            System.IO.StreamReader sm = new System.IO.StreamReader(context.Request.InputStream);
            string MoneyRequestStr = sm.ReadToEnd();
            try
            {
                context.Response.ContentType = "text/plain";
//                 MoneyRequest _MoneyRequest = new MoneyRequest();
//                 _MoneyRequest.UserId = 100;
                MoneyRequest _MoneyRequest = LitJson.JsonMapper.ToObject<MoneyRequest>(MoneyRequestStr);
                MoneyReturn newMoneyReturn = new MoneyReturn();
                newMoneyReturn.code = 0;
                newMoneyReturn.msg = "";
                {
                    var prams = new List<DbParameter>();
                    prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwUserID",  _MoneyRequest.UserId));
                    prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("dwBindType", _MoneyRequest.BankAccountType));
                    prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBindId", _MoneyRequest.AccountNo));
                    prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBindAccountName", _MoneyRequest.BankAccountUsername));
                    prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("szBindBankName", _MoneyRequest.Bank));
                    prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("strErrorDescribe", "suss"));
                    
                    FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().RunProc("GSP_GP_AccountBankBind", prams);
                }    
                context.Response.Write(LitJson.JsonMapper.ToJson(newMoneyReturn));
            }
            catch (Exception exp)
            {
                MoneyReturn newMoneyReturn = new MoneyReturn();
                newMoneyReturn.code = 1;
                newMoneyReturn.msg = exp.Message.ToString();
                context.Response.Write(MoneyRequestStr+"--"+LitJson.JsonMapper.ToJson(newMoneyReturn));
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