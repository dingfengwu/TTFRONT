using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace Game.Web.GameWebApplication
{
    /// <summary>
    /// SubmitMessage 的摘要说明
    /// </summary>
    public class SubmitMessage : IHttpHandler
    {
        public class CSubmitMsg
        {
            public int UserId;
//             public int  MsgStatus;
//             public string CreateTime;
            public string MsgContent;
            public string MsgTiltle;
            public int MsgType;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            RetureCode newRetureCode = new RetureCode();
            Stream sm = context.Request.InputStream;
            StreamReader inputData = new StreamReader(sm);
            string DataString = inputData.ReadToEnd();
            try
            {
                CSubmitMsg newPhoneLoginInfo = LitJson.JsonMapper.ToObject<CSubmitMsg>(DataString);
//                 newPhoneLoginInfo.CreateTime = DateTime.Now.ToString();
                var prams = new List<DbParameter>();
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("intUserId", newPhoneLoginInfo.UserId));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("intMsgStatus", 0));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("stringCreateTime", DateTime.Now.ToString()));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("stringMsgContent", newPhoneLoginInfo.MsgContent));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("stringMsgTiltle", newPhoneLoginInfo.MsgTiltle));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("intMsgType", newPhoneLoginInfo.MsgType));
                prams.Add(FacadeManage.aideTreasureFacade.DataProvider.GetDbHelper().MakeInParam("strErrorDescribe", "suss"));

                FacadeManage.aideAccountsFacade.DataProvider.GetDbHelper().RunProc("GSP_GP_SubmintClientMsg", prams);
                newRetureCode.code = 0;
            }
            catch (Exception exp)
            {
                Debug.LogException(exp);
                newRetureCode.code = 1;
                newRetureCode.msg = LitJson.JsonMapper.ToJson(new CSubmitMsg());
                newRetureCode.msg += exp.Message.ToString() + exp.StackTrace.ToString(); ;
            }
            context.Response.Write(LitJson.JsonMapper.ToJson(newRetureCode));
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