using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Game.Web.GameWebApplication
{
    /// <summary>
    /// PayGetMoneyRequest 的摘要说明
    /// </summary>
    public class PayGetMoneyRequest : IHttpHandler
    {
        public class PayGetMoneyRequestCell
        {
            public string RequestTime;
            public string CheckTime;
            public float Amount;
            public int BankAccountType;
            public string ChangeStatus;
        }
        public class PayGetMoneyRequestCellMgr
        {
            public int code = 0;
            public List<PayGetMoneyRequestCell> PayGetMoneyRequestCells = new List<PayGetMoneyRequestCell>();
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                PayGetMoneyRequestCellMgr newPayGetMoneyRequestCellMgr = new PayGetMoneyRequestCellMgr();
                context.Response.ContentType = "text/plain";
                string UserId = context.Request.QueryString["UserId"];
                string sql = string.Format("SELECT * FROM AccontPayChangeMoney WHERE UserID={0}", UserId);
                {
                    DataSet ds = FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().ExecuteDataset(CommandType.Text, sql);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PayGetMoneyRequestCell newMoneyRequest = new PayGetMoneyRequestCell();
                        newMoneyRequest.RequestTime = ds.Tables[0].Rows[i]["RequestTime"].ToString().Trim();
                        newMoneyRequest.CheckTime = ds.Tables[0].Rows[i]["RequestTime"].ToString().Trim();
                        newMoneyRequest.BankAccountType = Convert.ToInt32(ds.Tables[0].Rows[i]["BankAccountType"]);
                        newMoneyRequest.Amount = Convert.ToSingle(ds.Tables[0].Rows[i]["Amount"]);
                        int ChangeStatus = Convert.ToInt32(ds.Tables[0].Rows[i]["ChangeStatus"]);
                        if (ChangeStatus == 0)
                            newMoneyRequest.ChangeStatus = "等待处理";
                        else
                            newMoneyRequest.ChangeStatus = "兑换成功";

                        newPayGetMoneyRequestCellMgr.PayGetMoneyRequestCells.Add(newMoneyRequest);
                    }
                }
                context.Response.Write(LitJson.JsonMapper.ToJson(newPayGetMoneyRequestCellMgr));
            }
            catch(Exception exp)
            {
                Debug.LogException(exp);
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