using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Game.Facade;
using System.Text;
using Game.Utils;
using Game.Entity.NativeWeb;
using System.Web.Script.Serialization;
using Game.Entity.Accounts;
using System.Text.RegularExpressions;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Facade.DataStruct;
using System.Collections;
using Game.Entity.Platform;
using Game.Web.Pay.WX;

namespace Game.Web.WS
{
    /// <summary>
    /// MobileInterface 的摘要说明
    /// </summary>
    public class MobileInterface : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetQueryString("action").ToLower();
            switch(action)
            {
                case "getInsureScore":
                    GetInsureScore(context);
                    break;
                case "getscorerank":
                    GetScoreRank(context);
                    break;
                case "getscorerank2":
                    GetScoreRank2(context);
                    break;
                case "getmobilefeedback":
                    GetMobileFeedback(context);
                    break;
                case "getmobilefaq":
                    GetMobileFaq(context);
                    break;
                case "getmobilenotice":
                    GetMobileNotice(context);
                    break;
                case "getmobilerollnotice":
                    GetMobileRollNotice(context);
                    break;
                case "getmobilepropertytype":
                    GetMobilePropertyType(context);
                    break;
                case "getmobileproperty":
                    GetMobileProperty(context);
                    break;
                case "getmobileshare":
                    GetMobileShare(context);
                    break;
                case "getmobileshareconfig":
                    GetMobileShareConfig(context);
                    break;
                case "getscoreinfo":
                    GetScoreInfo(context);
                    break;
                case "getpayrate":
                    GetPayRate(context);
                    break;
                case "getpayproduct":
                    GetPayProduct(context);
                    break;
                case "getpayconfig":
                    GetPayConfig(context);
                    break;
                case "creatpayorderid":
                    CreatPayOrderID(context);
                    break;
                case "getiosversion":
                    GetIosVersion(context);
                    break;
                case "getandriodversion":
                    GetAndriodVersion(context);
                    break;
                case "getorderrecord":
                    GetOrderRecord(context);
                    break;
                case "getpayrecord":
                    GetPayRecord(context);
                    break;
                case "getbankrecord":
                    GetBankRecord(context);
                    break;
                case "getlotterynumberrank":
                    GetLotteryNumberRank(context);
                    break;
                case "getlotterywinrank":
                    GetLotteryWinRank(context);
                    break;
                case "getawardorderlist":
                    GetAwardOrderList(context);
                    break;
                case "getgamelist":
                    GetGameList(context);
                    break;
                case "getagentchildlist":
                    GetAgentChildList(context);
                    break;
                case "getagentpaylist":
                    GetAgentPayList(context);
                    break;
                case "getagentpaybacklist":
                    GetAgentPayBackList(context);
                    break;
                case "getagentrevenuelist":
                    GetAgentRevenueList(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取订单记录
        /// </summary>
        protected void GetOrderRecord(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            int number = GameRequest.GetQueryInt("number", 20);
            int page = GameRequest.GetQueryInt("page", 1);

            AjaxJsonValid ajv = new AjaxJsonValid();

            // 验证签名
            Message message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if(!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 查询数据
            string where = string.Format(" WHERE UserID={0}", userID);
            string orderQuery = "ORDER By BuyDate DESC";
            PagerSet ps = FacadeManage.aideNativeWebFacade.GetOrderList(1, number, where, orderQuery);

            IList<MobileAwardOrder> list = null;
            if(ps.RecordCount > 0)
            {
                list = Game.Utils.DataHelper.ConvertDataTableToObjects<MobileAwardOrder>(ps.PageSet.Tables[0]);
                for(int i = 0; i < list.Count(); i++)
                {
                    // 去掉敏感字段
                    list[i].SolveNote = "";
                    list[i].OrderStatusDescription = Enum.GetName(typeof(AppConfig.AwardOrderStatus), list[i].OrderStatus);
                }
            }

            ajv.AddDataItem("total", ps.RecordCount);
            ajv.AddDataItem("list", list);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }        

        /// <summary>
        /// 获取充值记录
        /// </summary>
        protected void GetPayRecord(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            int number = GameRequest.GetQueryInt("number", 20);
            int page = GameRequest.GetQueryInt("page", 1);

            AjaxJsonValid ajv = new AjaxJsonValid();

            // 验证签名
            Message message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if(!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 查询数据
            string where = string.Format(" WHERE UserID={0}", userID);
            string[] filed = new string[3] { "PayAmount", "Currency", "ApplyDate" };
            PagerSet ps = FacadeManage.aideTreasureFacade.GetPayRecord(where, 1, number, filed);
            IList<MobilePayRecordData> list = null;
            if(ps.RecordCount > 0)
            {
                list = Game.Utils.DataHelper.ConvertDataTableToObjects<MobilePayRecordData>(ps.PageSet.Tables[0]);
            }

            ajv.AddDataItem("total", ps.RecordCount);
            ajv.AddDataItem("list", list);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取银行记录
        /// </summary>
        /// <param name="context"></param>
        protected void GetBankRecord(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            int number = GameRequest.GetQueryInt("number", 20);
            int page = GameRequest.GetQueryInt("page", 1);

            AjaxJsonValid ajv = new AjaxJsonValid();

            // 验证签名
            Message message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if (!message.Success)
            {

                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 查询数据
            string where = string.Format(" WHERE SourceUserID={0} OR TargetUserID={0} ", userID);
            PagerSet ps = FacadeManage.aideTreasureFacade.GetInsureTradeRecord(where, 1, number);
            IList<MobileRecordInsure> listRecord = null;
            if(ps.RecordCount > 0)
            {
                listRecord = Game.Utils.DataHelper.ConvertDataTableToObjects<MobileRecordInsure>(ps.PageSet.Tables[0]);
                for(int i = 0; i < listRecord.Count(); i++)
                {
                    //存 1,取 2,转 3
                    if (listRecord[i].TradeType == 1)
                    {
                        listRecord[i].TradeTypeDescription = "银行存款";
                        listRecord[i].TransferAccounts = "";
                    }
                    else if (listRecord[i].TradeType == 2)
                    {
                        listRecord[i].TradeTypeDescription = "银行取款";
                        listRecord[i].TransferAccounts = "";
                    }
                    else
                    {
                        if (listRecord[i].SourceUserID == userID)
                        {
                            listRecord[i].SwapScore = -listRecord[i].SwapScore;                            
                            listRecord[i].TradeTypeDescription = "银行转帐";
                            listRecord[i].TransferAccounts = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(listRecord[i].TargetUserID).GameID.ToString();
                        }
                        else
                        {
                            listRecord[i].TradeTypeDescription = "银行收款";
                            listRecord[i].TransferAccounts = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(listRecord[i].SourceUserID).GameID.ToString();
                        }                        
                    }
                }
            }

            ajv.AddDataItem("total", ps.RecordCount);
            ajv.AddDataItem("list", listRecord);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }        

        /// <summary>
        /// 获取兑奖总局数排行
        /// </summary>
        /// <param name="context"></param>
        protected void GetLotteryNumberRank(HttpContext context)
        {
            int kindID = GameRequest.GetQueryInt("kindid", 0);                //游戏标识
            int userID = GameRequest.GetQueryInt("userID", 0);                //用户标识

            AjaxJsonValid ajv = new AjaxJsonValid();

            // 昨日排行
            IList<UserGameInfo> listUserYesterdayteGameInfo = FacadeManage.aideTreasureFacade.GetUserGameWinMaxRank(100, Fetch.GetDateID(DateTime.Now.AddDays(-1)));
            Dictionary<int, int> dicYesterdayRank = new Dictionary<int, int>();
            if(listUserYesterdayteGameInfo != null)
            {
                // 排行榜数据
                int listCount = listUserYesterdayteGameInfo.Count();
                int rankedNo = 0;
                int grandTotal = 0;
                int rankNumber = 0;
                for(rankNumber = 0; rankNumber < listCount; rankNumber++)
                {
                    if(listUserYesterdayteGameInfo[rankNumber].LineGrandTotal != grandTotal)
                    {
                        rankedNo++;
                        if(rankedNo > 20)
                            break;
                        grandTotal = listUserYesterdayteGameInfo[rankNumber].LineWinMax;
                    }
                    dicYesterdayRank.Add(listUserYesterdayteGameInfo[rankNumber].UserID, rankedNo);
                }
            }

            // 排行数据
            IList<UserGameInfo> listUserGameInfo = FacadeManage.aideTreasureFacade.GetUserGameGrandTotalRank(100, Fetch.GetDateID(DateTime.Now));
            IList<UserGameInfoRank> listRank = new List<UserGameInfoRank>();

            // 当前用户排名
            int currentUserRanking = 0;
            if(listUserGameInfo != null)
            {
                // 排行榜数据
                int listCount = listUserGameInfo.Count();
                int rankedNo = 0;
                int grandTotal = 0;
                int rankNumber = 0;
                for(rankNumber = 0; rankNumber < listCount; rankNumber++)
                {
                    UserGameInfoRank userGameInfoRank = new UserGameInfoRank();
                    if(listUserGameInfo[rankNumber].LineGrandTotal != grandTotal)
                    {
                        rankedNo++;
                        if(rankedNo > 20)
                        {
                            break;
                        }
                        grandTotal = listUserGameInfo[rankNumber].LineWinMax;
                    }

                    userGameInfoRank.Ranking = rankedNo;
                    userGameInfoRank.LineGrandTotal = listUserGameInfo[rankNumber].LineGrandTotal;
                    userGameInfoRank.UserID = listUserGameInfo[rankNumber].UserID;

                    // 计算趋势
                    if(dicYesterdayRank.Count > 0 && dicYesterdayRank.Keys.Contains(listUserGameInfo[rankNumber].UserID))
                    {
                        if(dicYesterdayRank[listUserGameInfo[rankNumber].UserID] < rankedNo)
                            userGameInfoRank.Trend = 0;
                        else if(dicYesterdayRank[listUserGameInfo[rankNumber].UserID] > rankedNo)
                            userGameInfoRank.Trend = 1;
                        else
                            userGameInfoRank.Trend = 2;
                    }
                    else
                        userGameInfoRank.Trend = 0;

                    listRank.Add(userGameInfoRank);
                }
                rankNumber--;

                // 查询昵称
                ArrayList arrID = new ArrayList();
                //int length = listCount > 20 ? 20 : listCount;
                for(int j = 0; j <= rankNumber; j++)
                {
                    if(listRank[j].UserID == userID)
                    {
                        currentUserRanking = listRank[j].Ranking;
                    }
                    arrID.Add(listRank[j].UserID);
                }
                IList<AccountsInfo> listUser = FacadeManage.aideAccountsFacade.GetAccountsInfoList(arrID);
                if(listUser != null && listUser.Count > 0)
                {
                    for(int j = 0; j <= rankNumber; j++)
                    {
                        AccountsInfo model = listUser.Where(e => e.UserID == listRank[j].UserID).FirstOrDefault();
                        if(model != null)
                        {
                            listRank[j].NickName = model.NickName;
                            listRank[j].FaceID = model.FaceID;
                            listRank[j].CustomID = model.CustomID;
                            listRank[j].Gender = model.Gender;
                        }
                    }
                }
            }

            ajv.AddDataItem("ranking", currentUserRanking);
            ajv.AddDataItem("list", listRank);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取单次最大赢值排行
        /// </summary>
        /// <param name="context"></param>
        protected void GetLotteryWinRank(HttpContext context)
        {
            int kindID = GameRequest.GetQueryInt("kindid", 0);                //游戏标识
            int userID = GameRequest.GetQueryInt("userID", 0);                //用户标识

            AjaxJsonValid ajv = new AjaxJsonValid();

            // 参数验证
            if(kindID == 0 || userID == 0)
            {
                ajv.msg = "缺少参数！";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 昨日排行
            IList<UserGameInfo> listUserYesterdayteGameInfo = FacadeManage.aideTreasureFacade.GetUserGameWinMaxRank(100, Fetch.GetDateID(DateTime.Now.AddDays(-1)));
            Dictionary<int, int> dicYesterdayRank = new Dictionary<int, int>();
            if(listUserYesterdayteGameInfo != null)
            {
                // 排行榜数据
                int listCount = listUserYesterdayteGameInfo.Count();
                int rankedNo = 0;
                int grandTotal = 0;
                int rankNumber = 0;
                for(rankNumber = 0; rankNumber < listCount; rankNumber++)
                {
                    if(listUserYesterdayteGameInfo[rankNumber].LineGrandTotal != grandTotal)
                    {
                        rankedNo++;
                        if(rankedNo > 20)
                            break;
                        grandTotal = listUserYesterdayteGameInfo[rankNumber].LineWinMax;
                    }
                    dicYesterdayRank.Add(listUserYesterdayteGameInfo[rankNumber].UserID, rankedNo);
                }
            }

            // 今日排行数据
            IList<UserGameInfo> listUserGameInfo = FacadeManage.aideTreasureFacade.GetUserGameWinMaxRank(100, Fetch.GetDateID(DateTime.Now));
            IList<UserGameInfoRank> listRank = new List<UserGameInfoRank>();

            // 当前用户排名
            int currentUserRanking = 0;
            if(listUserGameInfo != null)
            {
                // 排行榜数据
                int listCount = listUserGameInfo.Count();
                int rankedNo = 0;
                int grandTotal = 0;
                int rankNumber = 0;
                for(rankNumber = 0; rankNumber < listCount; rankNumber++)
                {
                    UserGameInfoRank userGameInfoRank = new UserGameInfoRank();
                    if(listUserGameInfo[rankNumber].LineGrandTotal != grandTotal)
                    {
                        rankedNo++;
                        if(rankedNo > 20)
                        {
                            break;
                        }
                        grandTotal = listUserGameInfo[rankNumber].LineWinMax;
                    }

                    userGameInfoRank.Ranking = rankedNo;
                    userGameInfoRank.LineWinMax = listUserGameInfo[rankNumber].LineWinMax;
                    userGameInfoRank.UserID = listUserGameInfo[rankNumber].UserID;

                    // 计算趋势
                    if(dicYesterdayRank.Count > 0 && dicYesterdayRank.Keys.Contains(listUserGameInfo[rankNumber].UserID))
                    {
                        if(dicYesterdayRank[listUserGameInfo[rankNumber].UserID] < rankedNo)
                            userGameInfoRank.Trend = 0;
                        else if(dicYesterdayRank[listUserGameInfo[rankNumber].UserID] > rankedNo)
                            userGameInfoRank.Trend = 1;
                        else
                            userGameInfoRank.Trend = 2;
                    }
                    else
                        userGameInfoRank.Trend = 0;

                    listRank.Add(userGameInfoRank);
                }
                rankNumber--;

                // 查询昵称
                ArrayList arrID = new ArrayList();
                //int length = listCount > 20 ? 20 : listCount;
                for(int j = 0; j <= rankNumber; j++)
                {
                    if(listRank[j].UserID == userID)
                    {
                        currentUserRanking = listRank[j].Ranking;
                    }
                    arrID.Add(listRank[j].UserID);
                }
                IList<AccountsInfo> listUser = FacadeManage.aideAccountsFacade.GetAccountsInfoList(arrID);
                if(listUser != null && listUser.Count > 0)
                {
                    for(int j = 0; j <= rankNumber; j++)
                    {
                        AccountsInfo model = listUser.Where(e => e.UserID == listRank[j].UserID).FirstOrDefault();
                        if(model != null)
                        {
                            listRank[j].NickName = model.NickName;
                            listRank[j].FaceID = model.FaceID;
                            listRank[j].CustomID = model.CustomID;
                            listRank[j].Gender = model.Gender;
                        }
                    }
                }
            }

            ajv.AddDataItem("ranking", currentUserRanking);
            ajv.AddDataItem("list", listRank);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取排名 处理并列排名的情况
        /// </summary>
        /// <param name="context"></param>
        protected void GetScoreRank2(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userID", 0);      //用户标识
            AjaxJsonValid ajv = new AjaxJsonValid();

            IList<UserScoreRank> listUserScoreRank = new List<UserScoreRank>();
            DataSet ds = FacadeManage.aideTreasureFacade.GetScoreRanking(100);
            int rankedNo = 1;
            int userRanking = 0;
            if(ds.Tables[0].Rows.Count > 0)
            {
                for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if(i != 0 && Convert.ToInt64(ds.Tables[0].Rows[i]["Score"]) != Convert.ToInt64(ds.Tables[0].Rows[i - 1]["Score"]))
                        rankedNo++;
                    if(rankedNo > 20)
                        break;
                    UserScoreRank userScoreRank = new UserScoreRank();
                    userScoreRank.Ranking = rankedNo;
                    userScoreRank.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                    userScoreRank.Score = Convert.ToInt64(ds.Tables[0].Rows[i]["Score"]);
                    userScoreRank.FaceID = Convert.ToInt32(ds.Tables[0].Rows[i]["FaceID"]);
                    userScoreRank.CustomID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomID"]);
                    userScoreRank.NickName = ds.Tables[0].Rows[i]["NickName"].ToString();
                    listUserScoreRank.Add(userScoreRank);
                    if(userScoreRank.UserID == userID)
                        userRanking = rankedNo;
                }
            }
            ajv.AddDataItem("UserRanking", userRanking);
            ajv.AddDataItem("list", listUserScoreRank);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取金币排行榜，前50
        /// </summary>
        /// <param name="context"></param>
        protected void GetScoreRank(HttpContext context)
        {
            StringBuilder msg = new StringBuilder();
            int pageIndex = GameRequest.GetInt("pageindex", 1);
            int pageSize = GameRequest.GetInt("pagesize", 10);
            int userID = GameRequest.GetInt("UserID", 0);
            if(pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if(pageSize <= 0)
            {
                pageSize = 10;
            }
            if(pageSize > 50)
            {
                pageSize = 50;
            }

            //获取用户排行
            string sqlQuery = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Score DESC) as ChartID,UserID,Score FROM GameScoreInfo) a WHERE UserID={0}", userID);
            DataSet dsUser = FacadeManage.aideTreasureFacade.GetDataSetByWhere(sqlQuery);
            int uChart = 0;
            Int64 uScore = 0;
            if(dsUser.Tables[0].Rows.Count != 0)
            {
                uChart = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ChartID"]);
                uScore = Convert.ToInt64(dsUser.Tables[0].Rows[0]["Score"]);
            }

            //获取总排行
            DataSet ds = FacadeManage.aideTreasureFacade.GetList("GameScoreInfo", pageIndex, pageSize, " ORDER BY Score DESC", " ", "UserID,Score").PageSet;
            if(ds.Tables[0].Rows.Count > 0)
            {
                msg.Append("[");

                //添加用户排行
                msg.Append("{\"NickName\":\"" + Fetch.GetNickNameByUserID(userID) + "\",\"Score\":" + uScore + ",\"Rank\":" + uChart + "},");
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    msg.Append("{\"NickName\":\"" + Fetch.GetNickNameByUserID(Convert.ToInt32(dr["UserID"])) + "\",\"Score\":" + dr["Score"] + "},");
                }
                msg.Remove(msg.Length - 1, 1);
                msg.Append("]");
            }
            else
            {
                msg.Append("{}");
            }
            context.Response.Write(msg.ToString());
        }

        /// <summary>
        /// 移动版获取反馈
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileFeedback(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            if (!Fetch.IsUserOnline())
            {
                ajv.code = 0;
                ajv.msg = "请先登录";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            StringBuilder msg = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetFeedbacklist(page, number, Fetch.GetUserCookie().UserID);
            Template tl = new Template("/Template/MobileFeedback.html");
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                pagerSet.PageSet.Tables[0].Columns.Add("Date");
                pagerSet.PageSet.Tables[0].Columns.Add("Time");
                pagerSet.PageSet.Tables[0].Columns.Add("RevertStatus");
                for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
                {
                    pagerSet.PageSet.Tables[0].Rows[i]["FeedbackTitle"] = Utils.TextUtility.CutLeft(pagerSet.PageSet.Tables[0].Rows[i]["FeedbackContent"].ToString(), 10);
                    pagerSet.PageSet.Tables[0].Rows[i]["Date"] = Convert.ToDateTime(pagerSet.PageSet.Tables[0].Rows[i]["FeedbackDate"]).ToString("yyyy-MM-dd");
                    pagerSet.PageSet.Tables[0].Rows[i]["Time"] = Convert.ToDateTime(pagerSet.PageSet.Tables[0].Rows[i]["FeedbackDate"]).ToString("HH:mm:ss");
                    pagerSet.PageSet.Tables[0].Rows[i]["FeedbackContent"] = Utility.HtmlDecode(pagerSet.PageSet.Tables[0].Rows[i]["FeedbackContent"].ToString());
                    if (pagerSet.PageSet.Tables[0].Rows[i]["RevertContent"].ToString() == "")
                    {
                        pagerSet.PageSet.Tables[0].Rows[i]["RevertStatus"] = "回复状态：客服未回复";
                    }
                    else
                    {
                        pagerSet.PageSet.Tables[0].Rows[i]["RevertStatus"] = "";
                    }
                }
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("list", pagerSet.PageSet.Tables[0]);
                tl.ForDataScoureList = dic;
                html = tl.OutputHTML();
            }
            else
            {
                html = "<tr><td colspan=\"6\">无兑换记录！</td></tr>";
            }
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 移动版获取常见问题
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileFaq(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            StringBuilder sb = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetIssueList("WHERE TypeID=1 AND Nullity=0", page, number);
            DataSet ds = pagerSet.PageSet;
            string html = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.AppendFormat("<h2>{0}</h2>", dr["IssueTitle"]);
                    sb.AppendLine();
                    sb.AppendFormat("<p>{0}</p>", Utility.HtmlDecode(dr["IssueContent"].ToString()));
                }
                html = sb.ToString();
            }
            else
            {
                html = "<tr><td colspan=\"6\">无记录！</td></tr>";
            }

            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 移动版获取公告
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileNotice(HttpContext context)
        {
            AjaxJsonValid aj = new AjaxJsonValid();

            StringBuilder msg = new StringBuilder();
            int number = GameRequest.GetQueryInt("number", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetMobileNotcieList(page, number);
            DataSet ds = pagerSet.PageSet;
            if (ds.Tables[0].Rows.Count > 0)
            {
                msg.Append("{\"total\":" + pagerSet.RecordCount);
                msg.Append(",\"list\":{");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    msg.Append("\"" + dr["NewsID"] + "\":");
                    msg.Append("{\"Subject\":\"" + dr["Subject"] + "\",\"Body\":\"" + dr["Body"] + "\",\"IssueDate\":\"" + dr["IssueDate"] + "\",\"ImageUrl\":\"" + Game.Facade.Fetch.GetUploadFileUrl(dr["ImageUrl"].ToString()) + "\"},");
                }
                msg.Remove(msg.Length - 1, 1);
                msg.Append("}}");
            }
            else
            {
                msg.Append("{}");
            }
            context.Response.Write(msg.ToString());
        }

        /// <summary>
        /// 移动版滚动公告
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileRollNotice(HttpContext context)
        {
            AjaxJsonValid aj = new AjaxJsonValid();
            List<Game.Entity.NativeWeb.News> newsList = FacadeManage.aideNativeWebFacade.GetMobileNotcie() as List<Game.Entity.NativeWeb.News>;
            List<MobileNotice> noticeList = new List<MobileNotice>();
            if (newsList != null)
            {
                foreach (Game.Entity.NativeWeb.News news in newsList)
                {
                    noticeList.Add(new MobileNotice(news.NewsID, news.Subject, news.IssueDate, news.Body));
                }
            }
            aj.AddDataItem("notice", noticeList);
            aj.SetValidDataValue(true);
            context.Response.Write(aj.SerializeToJson());
        }

        /// <summary>
        /// 移动版道具类型
        /// </summary>
        /// <param name="context"></param>
        private void GetMobilePropertyType(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            // 查询数据
            IList<Game.Entity.Platform.GamePropertyType> list = FacadeManage.aidePlatformFacade.GetMobilePropertyType(1);
            ajv.AddDataItem("list", list);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 移动版道具获取
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileProperty(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            int typeID = GameRequest.GetQueryInt("TypeID", 0);

            // 查询数据
            IList<Game.Entity.Platform.GameProperty> list = FacadeManage.aidePlatformFacade.GetMobileProperty(typeID);            
            ajv.AddDataItem("list", list);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        ///手机分享
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileShare(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            string machineID = GameRequest.GetQueryString("machineid");       //机器码
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
            //分享
            message = FacadeManage.aideTreasureFacade.SharePresent(userID, logonPass, machineID, Utility.UserIP);
            if (!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            //查询金币
            GameScoreInfo model = FacadeManage.aideTreasureFacade.GetTreasureInfo2(userID);
            Int64 score = model.Score;

            ajv.msg = message.Content;
            ajv.AddDataItem("Score", score);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取分享配置信息
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileShareConfig(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间            

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

            //分享赠送信息
            int presentScore = 0;            
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.SharePresent.ToString());
            if (ssi != null)
                presentScore = ssi.StatusValue;
            //获取转盘信息
            int freeCount = 3;
            LotteryConfig model = FacadeManage.aideTreasureFacade.GetLotteryConfig();
            if (model != null)
                freeCount = model.FreeCount;
            //获取玩家推广域名
            string spreaderUrl = "http://" + Fetch.GetSpreaderUrl(userID) + "/Mobile/Register.aspx";

            ajv.AddDataItem("SharePresent", presentScore);
            ajv.AddDataItem("FreeCount", freeCount);
            ajv.AddDataItem("SpreaderUrl", spreaderUrl);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取财富信息
        /// </summary>
        /// <param name="context"></param>
        private void GetScoreInfo(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间            

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
            //查询游戏豆
            decimal currency = 0;
            Int64 score = 0;
            UserCurrencyInfo cModel = FacadeManage.aideTreasureFacade.GetUserCurrencyInfo(userID);
            if (cModel != null)
            {
                currency = cModel.Currency;
            }
            //查询金币
            GameScoreInfo sModel = FacadeManage.aideTreasureFacade.GetTreasureInfo2(userID);
            if (sModel != null)
            {
                score = sModel.Score;
            }
            ajv.msg = message.Content;
            ajv.AddDataItem("Currency", currency);
            ajv.AddDataItem("Score", score);
            ajv.AddDataItem("InsureScore", sModel==null?0:sModel.InsureScore);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取充值汇率
        /// </summary>
        /// <param name="context"></param>
        public void GetPayRate(HttpContext context)
        {
            int rate = 1;
            // 查询汇率
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.RateCurrency.ToString());
            if(ssi != null)
                rate = ssi.StatusValue;
            AjaxJsonValid ajv = new AjaxJsonValid();
            ajv.AddDataItem("Rate", rate);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取充值道具
        /// </summary>
        /// <param name="context"></param>
        public void GetPayProduct(HttpContext context)
        {
            int userID = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间

            AjaxJsonValid ajv = new AjaxJsonValid();

            // 验证签名
            Message message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if (!message.Success)
            {
                ajv.msg = message.Content;
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //查询道具
            DataSet ds = FacadeManage.aideTreasureFacade.GetAppList();
            IList<GlobalAppInfo> list = Game.Utils.DataHelper.ConvertDataTableToObjects<GlobalAppInfo>(ds.Tables[0]);

            //充值记录
            int isPay = 0;
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetPayRecord(string.Format("WHERE UserID={0} AND DATEDIFF(d,ApplyDate,GETDATE())=0", userID), 1, 10);
            if (pagerSet.RecordCount > 0)
            {
                isPay = 1;
            }
            ajv.AddDataItem("IsPay", isPay);
            ajv.AddDataItem("list", list);            
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取充值配置
        /// </summary>
        /// <param name="context"></param>
        public void GetPayConfig(HttpContext context)
        {
            int version = 0;
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.PayConfig.ToString());
            if (ssi != null)
                version = ssi.StatusValue;
            AjaxJsonValid ajv = new AjaxJsonValid();
            ajv.AddDataItem("version", version);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }
        public void GetInsureScore(HttpContext context)
        {
//             int userID = GameRequest.GetQueryInt("userid", 0);
//             int version = 0;
//             int ssi = FacadeManage.aideTreasureFacade.GetInsureSocore(userID);
//             AjaxJsonValid ajv = new AjaxJsonValid();
//             ajv.AddDataItem("insuresocore", ssi);
//             ajv.SetValidDataValue(true);
//             context.Response.Write(ajv.SerializeToJson());
        }
        
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="context"></param>
        public void CreatPayOrderID(HttpContext context)
        {
            string payType = GameRequest.GetString("paytype");
            AjaxJsonValid ajv = new AjaxJsonValid();

            string accounts = GameRequest.GetString("accounts");
            if(string.IsNullOrEmpty(accounts))
            {
                ajv.msg = "请输入充值的账号！";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            string strAmount = GameRequest.GetString("amount");
            Regex regex = new Regex(@"^\d{1,18}(.\d{1,2})?$");
            if(!regex.IsMatch(strAmount))
            {
                ajv.msg = "请输入正确的充值金额！";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            decimal amount = Convert.ToDecimal(strAmount);

            // 生成订单
            OnLineOrder onlineOrder = new OnLineOrder();
            switch(payType)
            {
                case "jft":
                    onlineOrder.ShareID = 200;
                    onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix("JFTAPP");
                    break;
                case "zfb":
                    onlineOrder.ShareID = 300;
                    onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix("ZFBAPP");
                    break;
                case "wx":
                    onlineOrder.ShareID = 400;
                    onlineOrder.OrderID = PayHelper.GetOrderIDByPrefix("WXAPP");
                    break;
                default:
                    break;
            }
            onlineOrder.Accounts = accounts;
            onlineOrder.OrderAmount = amount;
            onlineOrder.IPAddress = GameRequest.GetUserIP();

            // 订单入库
            Message umsg = FacadeManage.aideTreasureFacade.RequestOrder(onlineOrder);
            if(!umsg.Success)
            {
                ajv.msg = umsg.Content;
            }
            else
            {
                if (payType == "wx")
                {
                    //获取prepay_id
                    XZAppPay appPay = new XZAppPay();
                    appPay.SetParameter("out_trade_no", onlineOrder.OrderID);
                    appPay.SetParameter("body", strAmount);
                    appPay.SetParameter("total_fee", (onlineOrder.OrderAmount * 100).ToString("F0"));

                    string payPackage = appPay.GetPrepayIDSign();
                    ajv.AddDataItem("PayPackage", payPackage);
                }
                ajv.AddDataItem("OrderID", onlineOrder.OrderID);
                ajv.msg = "下单成功";
            }
            ajv.SetValidDataValue(umsg.Success);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取IOS平台版本号
        /// </summary>
        /// <param name="context"></param>
        public void GetIosVersion(HttpContext context)
        {
            string version = "V1.0";
            string downloadURL = "";
            ConfigInfo configInfo = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameIosConfig.ToString());
            if(configInfo != null)
            {
                version = configInfo.Field2;
                downloadURL = configInfo.Field1;
            }
            AjaxJsonValid ajv = new AjaxJsonValid();
            ajv.AddDataItem("Version", version);
            ajv.AddDataItem("ForcedUpdate", configInfo.Field3 == "1" ? true : false);
            ajv.AddDataItem("DownloadURL", downloadURL);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取Andriod平台版本号
        /// </summary>
        /// <param name="context"></param>
        public void GetAndriodVersion(HttpContext context)
        {
            string version = "V1.0";
            string downloadURL = "";
            ConfigInfo configInfo = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameAndroidConfig.ToString());
            if(configInfo != null)
            {
                version = configInfo.Field2;
                downloadURL = configInfo.Field1;
            }
            AjaxJsonValid ajv = new AjaxJsonValid();
            ajv.AddDataItem("Version", version);
            ajv.AddDataItem("ForcedUpdate", configInfo.Field3 == "1" ? true : false);
            ajv.AddDataItem("DownloadURL", downloadURL);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 手机端异步获取奖牌订单列表
        /// </summary>
        /// <param name="context"></param>
        public void GetAwardOrderList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            //判断登录
            if(!Fetch.IsUserOnline())
            {
                ajv.code = 0;
                ajv.msg = "请先登录";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 参数
            int page = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("pageSize", 10);

            //绑定数据
            string where = string.Format(" WHERE UserID={0}", Fetch.GetUserCookie().UserID);
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetOrderList(page, pageSize, where, " ORDER BY BuyDate DESC");
            Template tl = new Template("/Template/MobileAwardOrder.html");
            string html = string.Empty;
            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                pagerSet.PageSet.Tables[0].Columns.Add("OrderStatusDescribe");
                pagerSet.PageSet.Tables[0].Columns.Add("TimeDescribe");
                for(int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
                {
                    pagerSet.PageSet.Tables[0].Rows[i]["OrderStatusDescribe"] = Enum.GetName(typeof(Game.Facade.AppConfig.AwardOrderStatus), pagerSet.PageSet.Tables[0].Rows[i]["OrderStatus"]);
                    pagerSet.PageSet.Tables[0].Rows[i]["TimeDescribe"] = Convert.ToDateTime(pagerSet.PageSet.Tables[0].Rows[i]["BuyDate"]).ToString("yyyy-MM-dd HH:mm");
                }
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("Order", pagerSet.PageSet.Tables[0]);
                tl.ForDataScoureList = dic;
                html = tl.OutputHTML();
            }
            else
            {
                html = "<tr><td colspan=\"6\">无兑换记录！</td></tr>";
            }
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取大厅游戏列表
        /// </summary>
        /// <param name="context"></param>
        public void GetGameList(HttpContext context)
        {
            int typeID = GameRequest.GetQueryInt("TypeID", 1);
            if (typeID != 1 && typeID != 2)
            {
                typeID = 1;
            }
            string version = "";
            string downloadURL = "";
            string resVersion = "";
            string ios_url = "";
            ConfigInfo configInfo = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.MobilePlatformVersion.ToString());
            if (configInfo != null)
            {                
                downloadURL = configInfo.Field1;
                version = configInfo.Field2;
                resVersion = configInfo.Field3;
                ios_url = configInfo.Field4;
            }

            //分享赠送信息
            int wxLogon = 0;
            SystemStatusInfo ssi = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.SystemConfigKey.WxLogon.ToString());
            if (ssi != null)
                wxLogon = ssi.StatusValue;

            //获取游戏列表
            DataSet ds = FacadeManage.aidePlatformFacade.GetMobileKindList(typeID);
            AjaxJsonValid ajv = new AjaxJsonValid();
            ajv.AddDataItem("downloadurl", downloadURL);
            ajv.AddDataItem("clientversion", version);
            ajv.AddDataItem("resversion", resVersion);
            ajv.AddDataItem("ios_url", ios_url);
            ajv.AddDataItem("wxLogon", wxLogon);
            ajv.AddDataItem("gamelist", ConventDataTableToJson.ConventDataTableToList(ds.Tables[0]));
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        #region 代理模块

        /// <summary>
        /// 获取注册信息
        /// </summary>
        /// <param name="context"></param>
        public void GetAgentChildList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            //判断登录
            if(!Fetch.IsUserOnline())
            {
                ajv.code = 0;
                ajv.msg = "请先登录";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 参数
            int page = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("pageSize", 10);

            //绑定数据
            string where = string.Format(" WHERE SpreaderID={0}", Fetch.GetUserCookie().UserID);
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsInfo.Tablename, page, pageSize, where, " ORDER BY RegisterDate DESC");
            Template tl = new Template("/Template/MobileAgentChild.html");
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                pagerSet.PageSet.Tables[0].Columns.Add("RevenueProvide");
                pagerSet.PageSet.Tables[0].Columns.Add("PayProvide");
                for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
                {                    
                    pagerSet.PageSet.Tables[0].Rows[i]["NickName"] = TextUtility.CutString(pagerSet.PageSet.Tables[0].Rows[i]["NickName"].ToString(), 0, 10);
                    pagerSet.PageSet.Tables[0].Rows[i]["RevenueProvide"] = FacadeManage.aideTreasureFacade.GetChildRevenueProvide(Convert.ToInt32(pagerSet.PageSet.Tables[0].Rows[i]["UserID"]));
                    pagerSet.PageSet.Tables[0].Rows[i]["PayProvide"] = FacadeManage.aideTreasureFacade.GetChildPayProvide(Convert.ToInt32(pagerSet.PageSet.Tables[0].Rows[i]["UserID"]));
                }
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("list", pagerSet.PageSet.Tables[0]);
                tl.ForDataScoureList = dic;
                html = tl.OutputHTML();
            }
            else
            {
                html = "<tr><td colspan=\"10\">无兑换记录！</td></tr>";
            }
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取充值信息
        /// </summary>
        /// <param name="context"></param>
        public void GetAgentPayList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            //判断登录
            if(!Fetch.IsUserOnline())
            {
                ajv.code = 0;
                ajv.msg = "请先登录";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 参数
            int page = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("pageSize", 10);

            //绑定数据
            string where = string.Format("WHERE UserID={0} AND TypeID=1", Fetch.GetUserCookie().UserID);
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordAgentInfo.Tablename, page, pageSize, "ORDER BY CollectDate DESC", where);
            Template tl = new Template("/Template/MobileAgentPay.html");
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                pagerSet.PageSet.Tables[0].Columns.Add("NickName");
                pagerSet.PageSet.Tables[0].Columns.Add("AgentScaleFomart");                
                for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
                {
                    pagerSet.PageSet.Tables[0].Rows[i]["NickName"] = TextUtility.CutString(FacadeManage.aideAccountsFacade.GetNickNameByUserID(Convert.ToInt32(pagerSet.PageSet.Tables[0].Rows[i]["ChildrenID"])), 0, 10);
                    pagerSet.PageSet.Tables[0].Rows[i]["AgentScaleFomart"] = Convert.ToInt32(Convert.ToDecimal(pagerSet.PageSet.Tables[0].Rows[i]["AgentScale"]) * 1000) + "‰";
                }
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("list", pagerSet.PageSet.Tables[0]);
                tl.ForDataScoureList = dic;
                html = tl.OutputHTML();
            }
            else
            {
                html = "<tr><td colspan=\"10\">无兑换记录！</td></tr>";
            }
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取税收信息
        /// </summary>
        /// <param name="context"></param>
        public void GetAgentRevenueList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            //判断登录
            if (!Fetch.IsUserOnline())
            {
                ajv.code = 0;
                ajv.msg = "请先登录";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 参数
            int page = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("pageSize", 10);

            //绑定数据
            string where = string.Format("WHERE AgentUserID={0}", Fetch.GetUserCookie().UserID);
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordUserRevenue.Tablename, page, pageSize, "ORDER BY CollectDate DESC", where);
            Template tl = new Template("/Template/MobileAgentRevenue.html");
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                pagerSet.PageSet.Tables[0].Columns.Add("DateIDFormat");
                pagerSet.PageSet.Tables[0].Columns.Add("NickName");
                pagerSet.PageSet.Tables[0].Columns.Add("AgentScaleFormat");
                for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
                {                    
                    pagerSet.PageSet.Tables[0].Rows[i]["DateIDFormat"] = Fetch.ShowDate(Convert.ToInt32(pagerSet.PageSet.Tables[0].Rows[i]["DateID"]));
                    pagerSet.PageSet.Tables[0].Rows[i]["NickName"] = TextUtility.CutString(FacadeManage.aideAccountsFacade.GetNickNameByUserID(Convert.ToInt32(pagerSet.PageSet.Tables[0].Rows[i]["UserID"])), 0, 10);
                    pagerSet.PageSet.Tables[0].Rows[i]["AgentScaleFormat"] = Convert.ToInt32(Convert.ToDecimal(pagerSet.PageSet.Tables[0].Rows[i]["AgentScale"]) * 1000) + "‰";
                }
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("list", pagerSet.PageSet.Tables[0]);
                tl.ForDataScoureList = dic;
                html = tl.OutputHTML();
            }
            else
            {
                html = "<tr><td colspan=\"10\">无兑换记录！</td></tr>";
            }
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取返现信息
        /// </summary>
        /// <param name="context"></param>
        public void GetAgentPayBackList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            //判断登录
            if(!Fetch.IsUserOnline())
            {
                ajv.code = 0;
                ajv.msg = "请先登录";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            // 参数
            int page = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("pageSize", 10);

            //绑定数据
            string where = string.Format("WHERE UserID={0} AND TypeID=2", Fetch.GetUserCookie().UserID);
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordAgentInfo.Tablename, page, pageSize, "ORDER BY CollectDate DESC", where);
            Template tl = new Template("/Template/MobileAgentPayBack.html");
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                pagerSet.PageSet.Tables[0].Columns.Add("DateIDFormat");
                pagerSet.PageSet.Tables[0].Columns.Add("PayBackScaleFormat");
                for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
                {                    
                    pagerSet.PageSet.Tables[0].Rows[i]["DateIDFormat"] = Fetch.ShowDate(Convert.ToInt32(pagerSet.PageSet.Tables[0].Rows[i]["DateID"]));
                    pagerSet.PageSet.Tables[0].Rows[i]["PayBackScaleFormat"] = Convert.ToInt32(Convert.ToDecimal(pagerSet.PageSet.Tables[0].Rows[i]["PayBackScale"]) * 1000) + "‰";
                }
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("list", pagerSet.PageSet.Tables[0]);
                tl.ForDataScoureList = dic;
                html = tl.OutputHTML();
            }
            else
            {
                html = "<tr><td colspan=\"10\">无兑换记录！</td></tr>";
            }
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}