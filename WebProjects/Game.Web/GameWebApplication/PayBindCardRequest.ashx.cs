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
    /// BindCardRequest 的摘要说明
    /// </summary>
    public class BindCardRequest : IHttpHandler
    {
        public class MoneyRequest
        {
            public int UserId;
            public int BankAccountType; //0银行卡 , 1 支付宝
            public string AccountNo;    //账号,卡号或支付宝ID
            public string Bank = "";    //工商银行
            public string BankAccountUsername; //持卡人姓名
        }
        public class MoneyRequestMgr
        {
            public int code = 0;
            public string msg = "";
            public List<MoneyRequest> Banks = new List<MoneyRequest>();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string UserId = context.Request.QueryString["UserId"];
                MoneyRequestMgr _MoneyRequestMgr = new MoneyRequestMgr();

                DataSet ds = FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().ExecuteDataset(CommandType.Text, "select* from AccountBankBind where UserID=" + UserId);
                List<MoneyRequest> MoneyRequests = new List<MoneyRequest>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    MoneyRequest newMoneyRequest = new MoneyRequest();
                    newMoneyRequest.UserId = Convert.ToInt32(UserId);
                    newMoneyRequest.BankAccountType = Convert.ToInt32(ds.Tables[0].Rows[i]["BindType"]);
                    newMoneyRequest.AccountNo = ds.Tables[0].Rows[i]["BindId"].ToString();
                    newMoneyRequest.BankAccountUsername = ds.Tables[0].Rows[i]["BindAccountName"].ToString();
                    newMoneyRequest.Bank = ds.Tables[0].Rows[i]["BindBankName"].ToString();
                    _MoneyRequestMgr.Banks.Add(newMoneyRequest);
                }
                context.Response.Write(LitJson.JsonMapper.ToJson(_MoneyRequestMgr));
            }
            catch (Exception exp)
            {
                MoneyRequestMgr _MoneyRequestMgr = new MoneyRequestMgr();
                _MoneyRequestMgr.code = 1;
                _MoneyRequestMgr.msg = exp.Message.ToString();
                context.Response.Write(LitJson.JsonMapper.ToJson(_MoneyRequestMgr));
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