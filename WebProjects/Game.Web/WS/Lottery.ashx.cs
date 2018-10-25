using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.WS
{
    /// <summary>
    /// Lottery 的摘要说明
    /// </summary>
    public class Lottery : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetQueryString("action").ToLower();
            switch (action)
            {
                case "lotteryconfig":
                    LotteryConfig(context);
                    break;
                case "lotteryuserinfo":
                    LotteryUserInfo(context);
                    break;
                case "lotterystart":
                    LotteryStart(context);
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 获取转盘配置
        /// </summary>
        /// <param name="context"></param>
        protected void LotteryConfig(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            IList<LotteryItem> list = FacadeManage.aideTreasureFacade.GetLotteryItem();
            ajv.AddDataItem("list", list);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取用户抽奖信息
        /// </summary>
        /// <param name="context"></param>
        protected void LotteryUserInfo(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            string logonPass = "";

            AjaxJsonValid ajv = new AjaxJsonValid();
            Message message = new Message();

            // 验证签名
            message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if (!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            else
            {
                UserInfo userInfo = message.EntityList[0] as UserInfo;
                logonPass = userInfo.LogonPass;
            }

            //获取信息
            message = FacadeManage.aideTreasureFacade.GetLotteryUserInfo(userID, logonPass);
            if (!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //返回数据
            Game.Entity.Treasure.LotteryUserInfo model = message.EntityList[0] as Game.Entity.Treasure.LotteryUserInfo;
            ajv.AddDataItem("list", model);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());

        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="context"></param>
        protected void LotteryStart(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            string logonPass = "";

            AjaxJsonValid ajv = new AjaxJsonValid();
            Message message = new Message();

            // 验证签名
            message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if (!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            else
            {
                UserInfo userInfo = message.EntityList[0] as UserInfo;
                logonPass = userInfo.LogonPass;
            }

            //抽奖
            message = FacadeManage.aideTreasureFacade.GetLotteryStart(userID, logonPass, Utility.UserIP);
            if (!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //返回数据
            LotteryReturn model = message.EntityList[0] as LotteryReturn;
            ajv.AddDataItem("list", model);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
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