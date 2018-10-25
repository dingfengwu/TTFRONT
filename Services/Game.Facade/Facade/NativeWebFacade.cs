using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Entity.NativeWeb;
using System.Data;

namespace Game.Facade
{
    /// <summary>
    /// 网站外观
    /// </summary>
    public class NativeWebFacade
    {
        #region Fields

        private INativeWebDataProvider webData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebFacade()
        {
            webData = ClassFactory.GetINativeWebDataProvider();
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
        public IList<News> GetTopNewsList( int typeID, int hot, int elite, int top )
        {
            return webData.GetTopNewsList( typeID, hot, elite, top );
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public IList<News> GetNewsList()
        {
            return webData.GetNewsList();
        }

        /// <summary>
        /// 获取分页新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetNewsList( int pageIndex, int pageSize )
        {
            return webData.GetNewsList( pageIndex, pageSize );
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
            return webData.GetNewsList(pageIndex, pageSize, classID);
        }

        /// <summary>
        /// 获取新闻 by newsID
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public News GetNewsByNewsID( int newsID, byte mode )
        {
            return webData.GetNewsByNewsID( newsID, mode );
        }

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public Notice GetNotice( int noticeID )
        {
            return webData.GetNotice( noticeID );
        }

        /// <summary>
        /// 获取移动版本公告
        /// </summary>
        /// <returns></returns>
        public IList<News> GetMobileNotcie()
        {
            return webData.GetMobileNotcie();
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
            return webData.GetMobileNotcieList(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取新闻的阅读状态
        /// </summary>
        public NewsReader GetNewsState(int newsId,int userId)
        {
            var list = webData.GetNewsState(newsId, userId);
            if (list == null || list.Count == 0)
                return new NewsReader();
            return list.FirstOrDefault();
        }

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        public void UpdateNewsState(int newsId,int userId)
        {
            webData.UpdateNewsState(newsId, userId);
        }

        #endregion

        #region 网站问题

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<GameIssueInfo> GetTopIssueList( int top )
        {
            return webData.GetTopIssueList( top );
        }

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="top"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameIssueInfo> GetTopIssueList(int top, int typeID)
        {
            return webData.GetTopIssueList(top, typeID);
        }

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <returns></returns>
        public IList<GameIssueInfo> GetIssueList()
        {
            return webData.GetIssueList();
        }

        /// <summary>
        /// 获取分页问题列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetIssueList( int pageIndex, int pageSize )
        {
            return webData.GetIssueList( pageIndex, pageSize );
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
            return webData.GetIssueList(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameIssueInfo GetIssueByIssueID( int issueID, byte mode )
        {
            return webData.GetIssueByIssueID( issueID, mode );
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameIssueInfo GetIssueByIssueID(int typeID, int issueID, byte mode)
        {
            return webData.GetIssueByIssueID(typeID, issueID, mode);
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
            return webData.GetFeedbacklist(pageIndex, pageSize, userId);
        }

        /// <summary>
        /// 获取反馈意见实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameFeedbackInfo GetGameFeedBackInfo( int feedID, byte mode )
        {
            return webData.GetGameFeedBackInfo( feedID, mode );
        }

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="feedID"></param>
        public void UpdateFeedbackViewCount( int feedID )
        {
            webData.UpdateFeedbackViewCount( feedID );
        }

        /// <summary>
        /// 发表留言
        /// </summary>
        /// <returns></returns>
        public Message PublishFeedback( GameFeedbackInfo info,string accouts )
        {
            return webData.PublishFeedback(info, accouts);
        }

        #endregion

        #region 游戏帮助数据

        /// <summary>
        /// 获取推荐游戏详细列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetGameHelps( int top )
        {
            return webData.GetGameHelps( top );
        }

        /// <summary>
        /// 获取游戏详细信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameRulesInfo GetGameHelp( int kindID )
        {
            return webData.GetGameHelp( kindID );
        }

        /// <summary>
        /// 获取手机版游戏
        /// </summary>
        /// <returns></returns>
        public DataSet GetMoblieGame()
        {
            return webData.GetMoblieGame();
        }

        #endregion

        #region 游戏比赛信息

        /// <summary>
        /// 得到比赛列表
        /// </summary>
        /// <returns></returns>
        public IList<GameMatchInfo> GetMatchList()
        {
            return webData.GetMatchList();
        }

        /// <summary>
        /// 得到比赛详细信息
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public GameMatchInfo GetMatchInfo( int matchID )
        {
            return webData.GetMatchInfo( matchID );
        }

        /// <summary>
        /// 比赛报名
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Message AddGameMatch( GameMatchUserInfo userInfo, string password )
        {
            return webData.AddGameMatch( userInfo, password );
        }

        #endregion

        #region 配置信息

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo( string key )
        {
            return webData.GetConfigInfo( key );
        }
        #endregion

        #region 游戏商城

        /// <summary>
        /// 获取商品分类实体
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public AwardType GetAwardType( int typeID )
        {
            return webData.GetAwardType( typeID );
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        public DataSet GetShopList( int counts )
        {
            return webData.GetShopList( counts );
        }

        /// <summary>
        /// 获取商品顶级分类
        /// </summary>
        /// <returns></returns>
        public DataSet GetShopTypeListByParentId( int parentID )
        {
            return webData.GetShopTypeListByParentId( parentID );
        }

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="awradID"></param>
        /// <returns></returns>
        public AwardInfo GetAwardInfo( int AwardID )
        {
            return webData.GetAwardInfo( AwardID );
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="awardOrder"></param>
        /// <returns></returns>
        public Message BuyAward( AwardOrder awardOrder )
        {
            return webData.BuyAward( awardOrder );
        }

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public PagerSet GetOrderList( int pageIndex, int pageSize, string where, string orderBy )
        {
            return webData.GetOrderList( pageIndex, pageSize, where, orderBy );
        }

        /// <summary>
        /// 查询处理中订单数
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetProcessingOrderCount( int userID )
        {
            return webData.GetProcessingOrderCount( userID );
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public AwardOrder GetAwardOrder( int orderID, int userID )
        {
            return webData.GetAwardOrder( orderID, userID );
        }

        /// <summary>
        /// 更新订单实体
        /// </summary>
        /// <param name="awardOrder"></param>
        public int UpdateAwardOrderStatus( AwardOrder awardOrder )
        {
            return webData.UpdateAwardOrderStatus( awardOrder );
        }

        /// <summary>
        /// 查询最近一批订单
        /// </summary>
        /// <param name="count">查询条数</param>
        /// <returns>订单数据集</returns>
        public DataSet GetTopOrder( int count )
        {
            return webData.GetTopOrder( count );
        }
        #endregion

        #region 广告

        /// <summary>
        /// 获取指定数目的广告数
        /// </summary>
        /// <param name="counts">查询数量</param>
        public DataSet GetWebHomeAdsList( int counts )
        {
            return webData.GetWebHomeAdsList( counts );
        }

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="ID">广告ID</param>
        /// <returns>广告实体</returns>
        public Ads GetAds( int ID )
        {
            return webData.GetAds( ID );
        }

        #endregion

        #region 独立页管理

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public SinglePage GetSinglePage( string keyValue )
        {
            return webData.GetSinglePage( keyValue );
        }

        #endregion

        #region 账号申诉

        /// <summary>
        /// 账号申诉
        /// </summary>
        /// <param name="lossReport">申诉实体</param>
        public void SaveLossReport( LossReport lossReport )
        {
            webData.SaveLossReport( lossReport );
        }

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportNo">申诉号</param>
        /// <param name="account">游戏账号</param>
        /// <returns></returns>
        public LossReport GetLossReport( string reportNo, string account )
        {
            return webData.GetLossReport( reportNo, account );
        }

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportNo">申诉号</param>
        /// <returns>申诉实体</returns>
        public LossReport GetLossReport( string reportNo )
        {
            return webData.GetLossReport( reportNo );
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
            return webData.GetActivityList(counts);
        }

        /// <summary>
        /// 获取活动实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Activity GetActivity(int id)
        {
            return webData.GetActivity(id);
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
        public PagerSet GetList( string tableName, int pageIndex, int pageSize, string pkey, string whereQuery )
        {
            return webData.GetList( tableName, pageIndex, pageSize, pkey, whereQuery );
        }

        #endregion
    }
}
