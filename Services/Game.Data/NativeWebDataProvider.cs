using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.IData;
using Game.Entity.NativeWeb;
using System.Data.Common;
using System.Data;

namespace Game.Data
{
    /// <summary>
    /// 网站数据访问层
    /// </summary>
    public class NativeWebDataProvider : BaseDataProvider, INativeWebDataProvider
    {
        #region 字段

        private ITableProvider aideAdsProvider;
        private ITableProvider aideLossReport;

        #endregion

        #region 构造方法

        public NativeWebDataProvider(string connString)
            : base(connString)
        {
            aideAdsProvider = GetTableProvider(Ads.Tablename);
            aideLossReport = GetTableProvider(LossReport.Tablename);
        }
        #endregion

        #region 网站新闻

        /// <summary>
        /// 获取置顶新闻列表
        /// </summary>
        /// <param name="newsType"></param>
        /// <param name="hot"></param>
        /// <param name="elite"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<News> GetTopNewsList(int typeID, int hot, int elite, int top)
        {
            StringBuilder sqlQuery = new StringBuilder()
            .AppendFormat("SELECT TOP({0}) ", top)
            .Append("NewsID, Subject,OnTop,OnTopAll,IsElite,IsHot,IsLinks,LinkUrl,HighLight,ClassID,IssueDate,LastModifyDate ")
            .Append("FROM News ");

            //查询条件
            sqlQuery.Append(" WHERE IsLock=1 AND IsDelete=0 ");

            //新闻类别
            if(typeID != 0)
            {
                sqlQuery.AppendFormat(" AND {0}={1} ", "ClassID", typeID);
            }

            //新闻状态            
            if(hot > 0)
            {
                sqlQuery.AppendFormat(" AND {0}={1} ", "IsHot", hot);
            }

            if(elite > 0)
            {
                sqlQuery.AppendFormat(" AND {0}={1} ", "IsElite", elite);
            }

            //排序
            sqlQuery.Append(" ORDER By OnTopAll DESC,OnTop DESC,IssueDate DESC ,NewsID DESC");

            return Database.ExecuteObjectList<News>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public IList<News> GetNewsList()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT NewsID,Subject,OnTop,OnTopAll,IsElite,IsHot,IsLinks,LinkUrl,ClassID,IssueDate, HighLight ")
                    .Append("FROM News ")
                    .Append("WHERE  IsLock=1 AND IsDelete=0 AND IssueDate <= ' AND ClassID IN(1,2)")
                    .Append(DateTime.Now.Date.ToString())
                    .Append("' ORDER By OnTopAll DESC,OnTop DESC,IssueDate DESC ,NewsID DESC");

            return Database.ExecuteObjectList<News>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取分页新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetNewsList(int pageIndex, int pageSize)
        {
            string whereQuery = "WHERE  IsLock=1 AND IsDelete=0 ";
            string orderQuery = "ORDER By OnTopAll DESC,OnTop DESC,IssueDate DESC ,NewsID DESC";
            string[] returnField = new string[11] { "NewsID", "Subject", "OnTop", "OnTopAll", "IsElite", "IsHot", "Islinks", "LinkUrl", "ClassID", "IssueDate", "HighLight" };
            PagerParameters pager = new PagerParameters(News.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取分页新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="classID"></param>
        /// <returns></returns>
        public PagerSet GetNewsList(int pageIndex, int pageSize, int classID)
        {
            string whereQuery = string.Format("WHERE  IsLock=1 AND IsDelete=0 AND ClassID={0} ", classID);
            string orderQuery = "ORDER By OnTopAll DESC,OnTop DESC,IssueDate DESC ,NewsID DESC";
            string[] returnField = new string[11] { "NewsID", "Subject", "OnTop", "OnTopAll", "IsElite", "IsHot", "Islinks", "LinkUrl", "ClassID", "IssueDate", "HighLight" };
            PagerParameters pager = new PagerParameters(News.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取新闻 by newsID
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public News GetNewsByNewsID(int newsID, byte mode)
        {
            News news = null;

            switch(mode)
            {
                case 1:
                    List<DbParameter> prams = new List<DbParameter>();
                    prams.Add(Database.MakeInParam("dwNewsID", newsID));
                    prams.Add(Database.MakeInParam("dwMode", 1));
                    news = Database.RunProcObject<News>("NET_PW_GetNewsInfoByID", prams);
                    break;
                case 2:
                    List<DbParameter> pram = new List<DbParameter>();
                    pram.Add(Database.MakeInParam("dwNewsID", newsID));
                    pram.Add(Database.MakeInParam("dwMode", 2));
                    news = Database.RunProcObject<News>("NET_PW_GetNewsInfoByID", pram);
                    break;
                case 0:
                default:
                    news = Database.ExecuteObject<News>(string.Format("SELECT * FROM News(NOLOCK) WHERE IsLock=1 AND IsDelete=0 AND NewsID={0}", newsID));
                    break;
            }

            return news;
        }

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public Notice GetNotice(int noticeID)
        {
            string sqlQuery = string.Format("SELECT * FROM Notice(NOLOCK) WHERE NoticeID={0}", noticeID);
            Notice notice = Database.ExecuteObject<Notice>(sqlQuery);
            return notice;
        }

        /// <summary>
        /// 获取移动版本公告
        /// </summary>
        /// <returns></returns>
        public IList<News> GetMobileNotcie()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT NewsID,Subject,Body,IssueDate,GameRange,ImageUrl ")
                    .Append("FROM News ")
                    .Append("WHERE ClassID=3 ")
                    .Append("ORDER By OnTop DESC,IssueDate DESC");

            return Database.ExecuteObjectList<News>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取移动公告列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="classID"></param>
        /// <returns></returns>
        public PagerSet GetMobileNotcieList(int pageIndex, int pageSize)
        {
            string whereQuery = string.Format("WHERE ClassID=3");
            string orderQuery = "ORDER By OnTopAll DESC,OnTop DESC,IssueDate DESC ,NewsID DESC";
            string[] returnField = new string[5] { "NewsID", "Subject", "Body", "IssueDate", "ImageUrl"};
            PagerParameters pager = new PagerParameters(News.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取新闻的阅读状态
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<NewsReader> GetNewsState(int newsId, int userId)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT Id,NewsId,UserId,ReadTime ")
                    .Append("FROM NewReader ")
                    .AppendFormat("WHERE NewsId={0} AND UserId={1}", newsId, userId);

            return Database.ExecuteObjectList<NewsReader>(sqlQuery.ToString());
        }

        public void UpdateNewsState(int newsId,int userId)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(@"IF NOT EXISTS(SELECT * FROM NewReader WHERE NewsId=@NewsId AND UserId=@UserId) ")
                .Append("INSERT INTO NewReader(NewsId,UserId,ReadTime) VALUES(@NewsId,@UserId,GetDate())");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("NewsId", newsId));
            prams.Add(Database.MakeInParam("UserId", userId));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        #endregion

        #region 网站问题

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<GameIssueInfo> GetTopIssueList(int top)
        {
            StringBuilder sqlQuery = new StringBuilder()
            .AppendFormat("SELECT TOP({0}) ", top)
            .Append("IssueID,IssueTitle,IssueContent,Nullity,CollectDate,ModifyDate ")
            .Append("FROM GameIssueInfo ");

            //查询条件
            sqlQuery.Append(" WHERE Nullity=0 ");

            //排序
            sqlQuery.Append(" ORDER By CollectDate DESC");

            return Database.ExecuteObjectList<GameIssueInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="top"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameIssueInfo> GetTopIssueList(int top, int typeID)
        {
            StringBuilder sqlQuery = new StringBuilder()
            .AppendFormat("SELECT TOP({0}) ", top)
            .Append("IssueID,IssueTitle,IssueContent,Nullity,CollectDate,ModifyDate ")
            .Append("FROM GameIssueInfo ");

            //查询条件
            sqlQuery.AppendFormat(" WHERE Nullity=0 AND TypeID={0}", typeID);

            //排序
            sqlQuery.Append(" ORDER By CollectDate DESC");

            return Database.ExecuteObjectList<GameIssueInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <returns></returns>
        public IList<GameIssueInfo> GetIssueList()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT IssueID,IssueTitle,IssueContent,Nullity,CollectDate,ModifyDate ")
                    .Append("FROM GameIssueInfo ")
                    .Append("WHERE Nullity=0 AND CollectDate <= '")
                    .Append(DateTime.Now.Date.ToString())
                    .Append("' ORDER By CollectDate DESC");

            return Database.ExecuteObjectList<GameIssueInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取分页问题列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetIssueList(int pageIndex, int pageSize)
        {
            string whereQuery = "WHERE Nullity=0 ";
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[6] { "IssueID", "IssueTitle", "IssueContent", "Nullity", "CollectDate", "ModifyDate" };
            PagerParameters pager = new PagerParameters(GameIssueInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取分页问题列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetIssueList(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[6] { "IssueID", "IssueTitle", "IssueContent", "Nullity", "CollectDate", "ModifyDate" };
            PagerParameters pager = new PagerParameters(GameIssueInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameIssueInfo GetIssueByIssueID(int issueID, byte mode)
        {
            GameIssueInfo Issue = null;

            switch(mode)
            {
                case 2:
                    Issue = Database.ExecuteObject<GameIssueInfo>(string.Format("SELECT * FROM GameIssueInfo(NOLOCK) WHERE Nullity=0 AND IssueID<{0} ORDER BY CollectDate DESC", issueID));
                    break;
                case 1:
                    Issue = Database.ExecuteObject<GameIssueInfo>(string.Format("SELECT * FROM GameIssueInfo(NOLOCK) WHERE Nullity=0 AND IssueID>{0} ORDER BY CollectDate ASC", issueID));
                    break;
                case 0:
                default:
                    Issue = Database.ExecuteObject<GameIssueInfo>(string.Format("SELECT * FROM GameIssueInfo(NOLOCK) WHERE Nullity=0 AND IssueID={0}", issueID));
                    break;
            }

            return Issue;
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameIssueInfo GetIssueByIssueID(int typeID, int issueID, byte mode)
        {
            GameIssueInfo Issue = null;

            switch (mode)
            {
                case 2:
                    Issue = Database.ExecuteObject<GameIssueInfo>(string.Format("SELECT * FROM GameIssueInfo(NOLOCK) WHERE TypeID={0} AND Nullity=0 AND IssueID<{1} ORDER BY CollectDate DESC", typeID, issueID));
                    break;
                case 1:
                    Issue = Database.ExecuteObject<GameIssueInfo>(string.Format("SELECT * FROM GameIssueInfo(NOLOCK) WHERE TypeID={0} AND Nullity=0 AND IssueID>{1} ORDER BY CollectDate ASC", typeID, issueID));
                    break;
                case 0:
                default:
                    Issue = Database.ExecuteObject<GameIssueInfo>(string.Format("SELECT * FROM GameIssueInfo(NOLOCK) WHERE Nullity=0 AND IssueID={0}", issueID));
                    break;
            }

            return Issue;
        }
        #endregion

        #region 反馈意见

        /// <summary>
        /// 获取分页反馈意见列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetFeedbacklist(int pageIndex, int pageSize, int userId)
        {
            string whereQuery = "WHERE Nullity=0";
            if(userId != 0)
            {
                whereQuery = string.Format("WHERE UserID={0}", userId);
            }
            string orderQuery = "ORDER By FeedbackDate DESC";

            string[] returnField = new string[9] { "FeedbackID", "FeedbackTitle", "FeedbackContent", "Nullity", "UserID", "FeedbackDate", "ViewCount", "RevertContent", "RevertDate" };
            PagerParameters pager = new PagerParameters(GameFeedbackInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取反馈意见实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameFeedbackInfo GetGameFeedBackInfo(int feedID, byte mode)
        {
            GameFeedbackInfo Issue = null;

            switch(mode)
            {
                case 1:
                    Issue = Database.ExecuteObject<GameFeedbackInfo>(string.Format("SELECT * FROM GameFeedbackInfo(NOLOCK) WHERE Nullity=-0 AND FeedbackID<{0} ORDER BY FeedbackID DESC", feedID));
                    break;
                case 2:
                    Issue = Database.ExecuteObject<GameFeedbackInfo>(string.Format("SELECT * FROM GameFeedbackInfo(NOLOCK) WHERE Nullity=0 AND FeedbackID>{0} ORDER BY FeedbackID ASC", feedID));
                    break;
                case 0:
                default:
                    Issue = Database.ExecuteObject<GameFeedbackInfo>(string.Format("SELECT * FROM GameFeedbackInfo(NOLOCK) WHERE Nullity=0 AND FeedbackID={0}", feedID));
                    break;
            }

            return Issue;
        }

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="feedID"></param>
        public void UpdateFeedbackViewCount(int feedID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwFeedbackID", feedID));

            Database.RunProc("NET_PW_UpdateViewCount", parms);
        }

        /// <summary>
        /// 发表留言
        /// </summary>
        /// <returns></returns>
        public Message PublishFeedback(GameFeedbackInfo info, string accounts)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strAccounts", accounts));

            parms.Add(Database.MakeInParam("strTitle", info.FeedbackTitle));
            parms.Add(Database.MakeInParam("strContent", info.FeedbackContent));

            parms.Add(Database.MakeInParam("strClientIP", info.ClientIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_AddGameFeedback", parms);
        }

        #endregion

        #region 游戏帮助数据

        /// <summary>
        /// 获取推荐游戏详细列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetGameHelps(int top)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("SELECT TOP({0}) ", top)
                    .Append("KindID, KindName, ImgRuleUrl, ThumbnailUrl, HelpIntro, HelpRule, HelpGrade, JoinIntro, Nullity, CollectDate, ModifyDate ")
                    .Append("FROM GameRulesInfo ")
                    .Append("WHERE  Nullity=0")
                    .Append(" ORDER By CollectDate DESC");
            return Database.ExecuteDataset(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取游戏详细信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameRulesInfo GetGameHelp(int kindID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID, KindName, ImgRuleUrl, ThumbnailUrl, MobileImgUrl,HelpIntro, HelpRule, HelpGrade, JoinIntro, Nullity, CollectDate, ModifyDate,AndroidDownloadUrl,IOSDownloadUrl,MobileSize,MobileDate,MobileVersion ")
                    .Append("FROM GameRulesInfo ")
                    .AppendFormat("WHERE KindID={0} ", kindID)
                    .Append(" ORDER By CollectDate DESC");

            return Database.ExecuteObject<GameRulesInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取手机版游戏
        /// </summary>
        /// <returns></returns>
        public DataSet GetMoblieGame()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("SELECT ")
                    .Append("KindID,KindName,ImgRuleUrl,ThumbnailUrl,MobileGameType,AndroidDownloadUrl,IOSDownloadUrl ")
                    .Append("FROM GameRulesInfo ")
                    .Append("WHERE Nullity=0 AND MobileGameType!=0 ")
                    .Append("ORDER By CollectDate DESC ");
            return Database.ExecuteDataset(sqlQuery.ToString());
        }

        #endregion

        #region 游戏比赛信息

        /// <summary>
        /// 得到比赛列表
        /// </summary>
        /// <returns></returns>
        public IList<GameMatchInfo> GetMatchList()
        {
            string sql = "SELECT * FROM GameMatchInfo WHERE Nullity = 0 ORDER BY CollectDate DESC";
            return Database.ExecuteObjectList<GameMatchInfo>(sql);
        }

        /// <summary>
        /// 得到比赛详细信息
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public GameMatchInfo GetMatchInfo(int matchID)
        {
            string sql = @"SELECT * FROM GameMatchInfo WHERE MatchID = @MatchID";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("MatchID", matchID));
            return Database.ExecuteObject<GameMatchInfo>(sql, parms);
        }

        /// <summary>
        /// 比赛报名
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Message AddGameMatch(GameMatchUserInfo userInfo, string password)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwMatchID", userInfo.MatchID));
            prams.Add(Database.MakeInParam("strAccounts", userInfo.Accounts));
            prams.Add(Database.MakeInParam("strPassword", password));
            prams.Add(Database.MakeInParam("strCompellation", userInfo.Compellation));
            prams.Add(Database.MakeInParam("dwGender", userInfo.Gender));
            prams.Add(Database.MakeInParam("strPassportID", userInfo.PassportID));
            prams.Add(Database.MakeInParam("strMobilePhone", userInfo.MobilePhone));
            prams.Add(Database.MakeInParam("strEMail", userInfo.EMail));
            prams.Add(Database.MakeInParam("strQQ", userInfo.QQ));
            prams.Add(Database.MakeInParam("strDwellingPlace", userInfo.DwellingPlace));
            prams.Add(Database.MakeInParam("strPostalCode", userInfo.PostalCode));
            prams.Add(Database.MakeInParam("strClientIP", userInfo.ClientIP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_AddGameMatchUser", prams);
        }

        #endregion

        #region 配置信息

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string key)
        {
            string sql = @"SELECT * FROM ConfigInfo WHERE ConfigKey = @ConfigKey";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("ConfigKey", key));
            return Database.ExecuteObject<ConfigInfo>(sql, parms);
        }
        #endregion

        #region 游戏商城

        /// <summary>
        /// 获取商品分类实体
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public AwardType GetAwardType(int typeID)
        {
            string sql = "SELECT * FROM AwardType WHERE TypeID=@typeID";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("TypeID", typeID));
            return Database.ExecuteObject<AwardType>(sql, parms);
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public DataSet GetShopList(int counts)
        {
            string sql = string.Format("SELECT TOP {0} * FROM AwardInfo WHERE Nullity=0 ORDER BY SortID ASC", counts);
            return Database.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取商品顶级分类
        /// </summary>
        /// <returns></returns>
        public DataSet GetShopTypeListByParentId(int parentID)
        {
            string sql = string.Format("SELECT * FROM AwardType WHERE ParentID={0} AND Nullity=0 ORDER BY SortID ASC", parentID);
            return Database.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="awradID"></param>
        /// <returns></returns>
        public AwardInfo GetAwardInfo(int awardID)
        {
            string sql = "SELECT * FROM AwardInfo WHERE AwardID=@AwardID";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("AwardID", awardID));
            return Database.ExecuteObject<AwardInfo>(sql, parms);
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="awardOrder"></param>
        /// <returns></returns>
        public Message BuyAward(AwardOrder awardOrder)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("UserID", awardOrder.UserID));
            parms.Add(Database.MakeInParam("AwardID", awardOrder.AwardID));
            parms.Add(Database.MakeInParam("AwardPrice", awardOrder.AwardPrice));
            parms.Add(Database.MakeInParam("AwardCount", awardOrder.AwardCount));
            parms.Add(Database.MakeInParam("TotalAmount", awardOrder.TotalAmount));
            parms.Add(Database.MakeInParam("Compellation", awardOrder.Compellation));
            parms.Add(Database.MakeInParam("MobilePhone", awardOrder.MobilePhone));
            parms.Add(Database.MakeInParam("QQ", awardOrder.QQ));
            parms.Add(Database.MakeInParam("Province", awardOrder.Province));
            parms.Add(Database.MakeInParam("City", awardOrder.City));
            parms.Add(Database.MakeInParam("Area", awardOrder.Area));
            parms.Add(Database.MakeInParam("DwellingPlace", awardOrder.DwellingPlace));
            parms.Add(Database.MakeInParam("PostalCode", awardOrder.PostalCode));
            parms.Add(Database.MakeInParam("BuyIP", awardOrder.BuyIP));
            parms.Add(Database.MakeOutParam("OrderID", typeof(int)));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForObject<AwardOrder>(Database, "WSP_PW_BuyAward", parms);
            return msg;
        }

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public PagerSet GetOrderList(int pageIndex, int pageSize, string where, string orderBy)
        {
            string tabelName = "(SELECT A.*,B.AwardName,B.SmallImage,B.IsReturn FROM AwardOrder AS A LEFT JOIN AwardInfo AS B ON A.AwardID=B.AwardID) AS D ";
            return GetList(tabelName, pageIndex, pageSize, orderBy, where);
        }

        /// <summary>
        /// 查询处理中订单数
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetProcessingOrderCount(int userID)
        {
            string sql = "SELECT COUNT(OrderID) FROM AwardOrder WHERE OrderStatus=0 AND UserID=" + userID;
            object obj = Database.ExecuteScalar(CommandType.Text, sql);
            if(obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public AwardOrder GetAwardOrder(int orderID, int userID)
        {
            string sql = "SELECT * FROM AwardOrder WHERE OrderID=@OrderID AND UserID=@UserID";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("OrderID", orderID));
            parms.Add(Database.MakeInParam("UserID", userID));
            return Database.ExecuteObject<AwardOrder>(sql, parms);
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="awardOrder"></param>
        public int UpdateAwardOrderStatus(AwardOrder awardOrder)
        {
            string sql = "UPDATE AwardOrder SET OrderStatus=@OrderStatus WHERE OrderID=@OrderID";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("OrderStatus", awardOrder.OrderStatus));
            parms.Add(Database.MakeInParam("OrderID", awardOrder.OrderID));
            return Database.ExecuteNonQuery(CommandType.Text, sql, parms.ToArray());
        }

        /// <summary>
        /// 查询最近一批订单
        /// </summary>
        /// <param name="count">查询条数</param>
        /// <returns>订单数据集</returns>
        public DataSet GetTopOrder(int count)
        {
            string sql = string.Format("SELECT TOP {0} A.UserID,A.BuyDate,B.AwardName FROM AwardOrder AS A LEFT JOIN AwardInfo AS B ON A.AwardID=B.AwardID ORDER BY BuyDate DESC", count);
            return Database.ExecuteDataset(sql);
        }
        #endregion

        #region 广告

        /// <summary>
        /// 获取指定数目的广告数
        /// </summary>
        /// <param name="counts">查询数量</param>
        public DataSet GetWebHomeAdsList(int counts)
        {
            string sqlQuery = string.Format("SELECT TOP {0} * FROM Ads WHERE Type=0 ORDER BY SortID DESC", counts);
            return Database.ExecuteDataset(sqlQuery);
        }

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="ID">广告ID</param>
        /// <returns>广告实体</returns>
        public Ads GetAds(int ID)
        {
            string sqlQuery = string.Format(" WHERE ID={0}", ID);
            Ads ads = aideAdsProvider.GetObject<Ads>(sqlQuery);
            return ads;
        }

        #endregion

        #region 独立页管理

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public SinglePage GetSinglePage(string keyValue)
        {
            string sql = "SELECT * FROM SinglePage WHERE KeyValue=@KeyValue";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("KeyValue", keyValue));
            return Database.ExecuteObject<SinglePage>(sql, prams);
        }

        #endregion

        #region 账号申诉

        /// <summary>
        /// 账号申诉
        /// </summary>
        /// <param name="lossReport">申诉实体</param>
        public void SaveLossReport(LossReport lossReport)
        {
            DataRow dr = aideLossReport.NewRow();
            dr[LossReport._ReportNo] = lossReport.ReportNo;
            dr[LossReport._ReportEmail] = lossReport.ReportEmail;
            dr[LossReport._Accounts] = lossReport.Accounts;
            dr[LossReport._RegisterDate] = lossReport.RegisterDate;
            dr[LossReport._Compellation] = lossReport.Compellation;
            dr[LossReport._PassportID] = lossReport.PassportID;
            dr[LossReport._MobilePhone] = lossReport.MobilePhone;
            dr[LossReport._OldNickName1] = lossReport.OldNickName1;
            dr[LossReport._OldNickName2] = lossReport.OldNickName2;
            dr[LossReport._OldNickName3] = lossReport.OldNickName3;
            dr[LossReport._OldLogonPass1] = lossReport.OldLogonPass1;
            dr[LossReport._OldLogonPass2] = lossReport.OldLogonPass2;
            dr[LossReport._OldLogonPass3] = lossReport.OldLogonPass3;
            dr[LossReport._ReportIP] = lossReport.ReportIP;
            dr[LossReport._Random] = lossReport.Random;
            dr[LossReport._GameID] = lossReport.GameID;
            dr[LossReport._UserID] = lossReport.UserID;
            dr[LossReport._OldQuestion1] = lossReport.OldQuestion1;
            dr[LossReport._OldResponse1] = lossReport.OldResponse1;
            dr[LossReport._OldQuestion2] = lossReport.OldQuestion2;
            dr[LossReport._OldResponse2] = lossReport.OldResponse2;
            dr[LossReport._OldQuestion3] = lossReport.OldQuestion3;
            dr[LossReport._OldResponse3] = lossReport.OldResponse3;
            dr[LossReport._SuppInfo] = lossReport.SuppInfo;
            dr[LossReport._FixedPhone] = lossReport.FixedPhone;
            dr[LossReport._ProcessStatus] = lossReport.ProcessStatus;
            dr[LossReport._OverDate] = lossReport.OverDate;
            dr[LossReport._ReportDate] = lossReport.ReportDate;
            dr[LossReport._SendCount] = lossReport.SendCount;
            aideLossReport.Insert(dr);
        }

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportNo">申诉号</param>
        /// <param name="account">游戏账号</param>
        /// <returns></returns>
        public LossReport GetLossReport(string reportNo, string account)
        {
            string sqlQuery = "SELECT * FROM LossReport WHERE ReportNo=@ReportNo AND Accounts=@Accounts";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("ReportNo", reportNo));
            parms.Add(Database.MakeInParam("Accounts", account));
            return Database.ExecuteObject<LossReport>(sqlQuery, parms);
        }

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportNo">申诉号</param>
        /// <returns>申诉实体</returns>
        public LossReport GetLossReport(string reportNo)
        {
            string sqlQuery = "SELECT * FROM LossReport WHERE ReportNo=@ReportNo";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("ReportNo", reportNo));
            return Database.ExecuteObject<LossReport>(sqlQuery, parms);
        }
        #endregion

        #region 推荐活动

        /// <summary>
        /// 获取推荐活动
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public DataSet GetActivityList(int counts)
        {
            string sqlQuery = string.Format("SELECT TOP {0} * FROM Activity WHERE IsRecommend=1 ORDER BY SortID ASC,InputDate DESC", counts);
            return Database.ExecuteDataset(sqlQuery);
        }

        /// <summary>
        /// 获取活动实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Activity GetActivity(int id)
        {
            string sqlQuery = "SELECT * FROM Activity WHERE ActivityID=@ActivityID";
            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("ActivityID", id));
            return Database.ExecuteObject<Activity>(sqlQuery, param);
        }

        #endregion

        #region 公共

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        /// <param name="whereQuery">查询条件</param>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string pkey, string whereQuery)
        {
            PagerParameters pager = new PagerParameters(tableName, pkey, whereQuery, pageIndex, pageSize);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        #endregion
    }
}
