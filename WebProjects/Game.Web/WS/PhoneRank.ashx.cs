using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Game.Web.WS
{
    public class ScoreRank
    {
        public int RankId;
        public int UserID;
        public string UserName;
        public int SumScore;
        public int RankType;
        public int FaceId;
    }
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    public class PhoneRank : IHttpHandler
    {
        public class ScoreRankCode : RetureCode
        {
            public List<ScoreRank> ScoreRanks = new List<ScoreRank>();
            public ScoreRank MyScore = new ScoreRank();
        }
        public void ProcessRequest( HttpContext context )
        {
            context.Response.ContentType = "text/plain";
            string action = GameRequest.GetQueryString( "action" );
            switch( action )
            {
                case "getscorerank":
                    GetScoreRank(context);
                    break;
                case "getscoreWeekrank":
                    GetWeekScoreRank(context);
                    break;
                case "getscorePreDayrank":
                    GetPreDayScoreRank(context);
                    break;
                case "getscorTodayhrank":
                    GetTodayScoreRank(context);
                    break;
                default:
                    break;
            }
        }
        void SendRank(DataSet ds, int userID, HttpContext context, DateTime start, DateTime end)
        {
            ScoreRankCode newScoreRankCode = new ScoreRankCode();

            string xx ="start:"+start.ToString()+" end:"+end.ToString()+ " Count:" + ds.Tables[0].Rows.Count+" ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ScoreRank newScoreRank = new ScoreRank();
                newScoreRank.RankId = Convert.ToInt32(ds.Tables[0].Rows[i]["RankId"]);
                newScoreRank.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
                newScoreRank.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                newScoreRank.SumScore = Convert.ToInt32(ds.Tables[0].Rows[i]["SumScore"]);
                newScoreRank.RankType = Convert.ToInt32(ds.Tables[0].Rows[i]["RankType"]);
                System.Object obj = ds.Tables[0].Rows[i]["FaceID"];
                if (obj.GetType() != typeof(DBNull))
                    newScoreRank.FaceId = Convert.ToInt32(obj);
                newScoreRank.RankId = i+1;
                newScoreRankCode.ScoreRanks.Add(newScoreRank);
                if (newScoreRank.UserID == userID)
                    newScoreRankCode.MyScore = newScoreRank;
                xx = "RankId:" + ds.Tables[0].Rows[i]["RankId"] + "  UserID:" + ds.Tables[0].Rows[i]["UserID"] + "  UserName:" +
                ds.Tables[0].Rows[i]["UserName"] + "  SumScore:" +
                ds.Tables[0].Rows[i]["SumScore"] + "  RankType:" +
                ds.Tables[0].Rows[i]["RankType"] + "  FaceID:" +
                ds.Tables[0].Rows[i]["FaceID"];
            }
            newScoreRankCode.msg = xx;
            CommonTools.SendStringToClient(context, newScoreRankCode);
        }
        protected void GetWeekScoreRank(HttpContext context)
        {
            try
            {
                int userID = GameRequest.GetInt("UserID", 0);
                DateTime dt = DateTime.Now;  //当前时间  
                DateTime startWeek = dt.AddDays(1 -(int)dt.DayOfWeek);  //本周周一  
                DateTime endWeek = startWeek.AddDays(6);  //本周周日  
                DataSet ds = FacadeManage.aideTreasureFacade.GetRank(startWeek, endWeek, userID);
                //
                SendRank(ds, userID, context, startWeek, endWeek);
            }
            catch(Exception exp)
            {
                ScoreRankCode newScoreRankCode = new ScoreRankCode();
                CommonTools.SendStringToClient(context, newScoreRankCode);
//                 context.Response.Write(exp.Message.ToString()+":"+exp.StackTrace.ToString());
            }
        }
        protected void GetTodayScoreRank(HttpContext context)
        {
            try
            {
                int userID = GameRequest.GetInt("UserID", 0);

                DateTime dt = DateTime.Now;  //当前时间  
                DateTime startMonth = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末  
                DataSet ds = FacadeManage.aideTreasureFacade.GetRank(startMonth, endMonth, userID);
                SendRank(ds, userID, context, startMonth, endMonth);
                //
//                 ScoreRankCode newScoreRankCode = new ScoreRankCode();
//                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
//                 {
//                     ScoreRank newScoreRank = new ScoreRank();
//                     newScoreRank.RankId = Convert.ToInt32(ds.Tables[0].Rows[i]["RankId"]);
//                     newScoreRank.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
//                     newScoreRank.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
//                     newScoreRank.SumScore = Convert.ToInt32(ds.Tables[0].Rows[i]["SumScore"]);
//                     newScoreRank.RankType = Convert.ToInt32(ds.Tables[0].Rows[i]["RankType"]);
//                     newScoreRank.FaceId = Convert.ToInt32(ds.Tables[0].Rows[i]["FaceID"].ToString());
//                     
//                     newScoreRankCode.ScoreRanks.Add(newScoreRank);
//                     if (newScoreRank.UserID == userID)
//                         newScoreRankCode.MyScore = newScoreRank;
//                 }
//                 string s = Newtonsoft.Json.JsonConvert.SerializeObject(newScoreRankCode);
//                 newScoreRankCode.msg = startMonth.ToString() + "--" + endMonth.ToString();
//                 CommonTools.SendStringToClient(context, newScoreRankCode);
            }
            catch(Exception exp)
            {
                ScoreRankCode newScoreRankCode = new ScoreRankCode();
                CommonTools.SendStringToClient(context, newScoreRankCode);

//                 context.Response.Write(exp.Message.ToString()+":"+exp.StackTrace.ToString());
            }

        }
        protected void GetPreDayScoreRank(HttpContext context)
        {
            try
            {
                int userID = GameRequest.GetInt("UserID", 0);

                DateTime dt = DateTime.Now.AddDays(-1);
                DateTime startYear = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);  //本年年初
                DateTime endYear = new DateTime(dt.Year, 12, 31);  //本年年末
                DataSet ds = FacadeManage.aideTreasureFacade.GetRank(startYear, endYear, userID);
                SendRank(ds, userID, context, startYear, endYear);
//                 //
//                 ScoreRankCode newScoreRankCode = new ScoreRankCode();
//                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
//                 {
//                     ScoreRank newScoreRank = new ScoreRank();
//                     newScoreRank.RankId = Convert.ToInt32(ds.Tables[0].Rows[i]["RankId"]);
//                     newScoreRank.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]);
//                     newScoreRank.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
//                     newScoreRank.SumScore = Convert.ToInt32(ds.Tables[0].Rows[i]["SumScore"]);
//                     newScoreRank.RankType = Convert.ToInt32(ds.Tables[0].Rows[i]["RankType"]);
//                     newScoreRank.FaceId = Convert.ToInt32(ds.Tables[0].Rows[i]["FaceID"]);
// 
//                     newScoreRankCode.ScoreRanks.Add(newScoreRank);
//                     if (newScoreRank.UserID == userID)
//                         newScoreRankCode.MyScore = newScoreRank;
//                 }
//                 string s = Newtonsoft.Json.JsonConvert.SerializeObject(newScoreRankCode);
//                 newScoreRankCode.msg = startYear.ToString() + "--" + endYear.ToString();
//                 CommonTools.SendStringToClient(context, newScoreRankCode);
            }
            catch (Exception exp)
            {
                ScoreRankCode newScoreRankCode = new ScoreRankCode();
                CommonTools.SendStringToClient(context, newScoreRankCode);

//                 context.Response.Write(exp.Message.ToString() + ":" + exp.StackTrace.ToString());
            }

        }
        /// <summary>
        /// 获取金币排行榜，前50
        /// </summary>
        /// <param name="context"></param>
        protected void GetScoreRank( HttpContext context )
        {
            StringBuilder msg = new StringBuilder( );
            int pageIndex = GameRequest.GetInt( "pageindex" , 1 );
            int pageSize = GameRequest.GetInt( "pagesize" , 10 );
            int userID = GameRequest.GetInt("UserID", 0);
            if( pageIndex <= 0 )
            {
                pageIndex = 1;
            }
            if( pageSize <= 0 )
            {
                pageSize = 10;
            }
            if( pageSize > 50 )
            {
                pageSize = 50;
            }

            //获取用户排行
            string sqlQuery = string.Format("SELECT a.*,b.FaceID,b.Experience,b.MemberOrder,b.GameID,b.UserMedal,b.UnderWrite FROM (SELECT ROW_NUMBER() OVER (ORDER BY Score DESC) as ChartID,UserID,Score FROM GameScoreInfo) a,RYAccountsDB.dbo.AccountsInfo b WHERE a.UserID=b.UserID AND a.UserID={0}", userID);
            DataSet dsUser = FacadeManage.aideTreasureFacade.GetDataSetByWhere( sqlQuery );
            int uChart = 0;
            Int64 uScore = 0;
            int uFaceID = 0;
            int uExperience = 0;
            int memberOrder = 0;
            int gameID = 0;
            int userMedal = 0;
            string underWrite = "";
            Int64 score = 0;
            decimal currency = 0;

            if (dsUser.Tables[0].Rows.Count != 0)
            {
                uChart = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ChartID"]);
                uScore = Convert.ToInt64(dsUser.Tables[0].Rows[0]["Score"]);
                uFaceID = Convert.ToInt32(dsUser.Tables[0].Rows[0]["FaceID"]);
                uExperience = Convert.ToInt32(dsUser.Tables[0].Rows[0]["Experience"]);
                memberOrder = Convert.ToInt32(dsUser.Tables[0].Rows[0]["MemberOrder"]);
                gameID = Convert.ToInt32(dsUser.Tables[0].Rows[0]["GameID"]);
                userMedal = Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserMedal"]);
                underWrite = dsUser.Tables[0].Rows[0]["UnderWrite"].ToString();
                score = GetUserScore(Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserID"]));
                currency = GetUserCurrency(Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserID"]));
            }

            //获取总排行
            DataSet ds = FacadeManage.aideTreasureFacade.GetList( "GameScoreInfo", pageIndex, pageSize, " ORDER BY Score DESC", " ", "UserID,Score" ).PageSet;
            if( ds.Tables[ 0 ].Rows.Count > 0 )
            {
                msg.Append( "[" );

                //添加用户排行
                msg.Append("{\"NickName\":\"" + Fetch.GetNickNameByUserID(userID) + "\",\"Score\":" + uScore + ",\"UserID\":" + userID + ",\"Rank\":" + uChart + ",\"FaceID\":" + uFaceID + ",\"Experience\":" + Fetch.GetGradeConfig(uExperience) + ",\"MemberOrder\":" + memberOrder + ",\"GameID\":" + gameID + ",\"UserMedal\":" + userMedal + ",\"szSign\":\"" + underWrite + "\",\"Score\":" + score + ",\"Currency\":" + currency + "},");
                foreach( DataRow dr in ds.Tables[ 0 ].Rows )
                {
                    msg.Append("{\"NickName\":\"" + Fetch.GetNickNameByUserID(Convert.ToInt32(dr["UserID"])) + "\",\"Score\":" + dr["Score"] + ",\"UserID\":" + dr["UserID"] + ",\"FaceID\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).FaceID + ",\"Experience\":" + Fetch.GetGradeConfig(Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).Experience) + ",\"MemberOrder\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).MemberOrder + ",\"GameID\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).GameID + ",\"UserMedal\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).UserMedal + ",\"szSign\":\"" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).UnderWrite + "\",\"Score\":" + GetUserScore(Convert.ToInt32(dr["UserID"])) + ",\"Currency\":" + GetUserCurrency(Convert.ToInt32(dr["UserID"])) + "},");
                }
                msg.Remove( msg.Length - 1 , 1 );
                msg.Append( "]" );
            }
            else
            {
                msg.Append( "{}" );
            }
            context.Response.Write( msg.ToString( ) );
        }

        /// <summary>
        /// 获取用户金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetUserScore(int userID)
        {
            GameScoreInfo model = FacadeManage.aideTreasureFacade.GetTreasureInfo2(userID);
            if (model != null)
            {
                return model.Score;
            }
            return 0;
        }

        /// <summary>
        /// 获取用户游戏豆
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public decimal GetUserCurrency(int userID)
        {
            UserCurrencyInfo model = FacadeManage.aideTreasureFacade.GetUserCurrencyInfo(userID);
            if (model != null)
            {
                return model.Currency;
            }
            return 0;
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
