using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Entity.Accounts;
using System.Data;
using Game.Entity.NativeWeb;

namespace Game.IData
{
    /// <summary>
    /// 网站库数据层接口
    /// </summary>
    public interface INativeWebDataProvider //: IProvider
    {
        #region 网站新闻

        /// <summary>
        /// 获取置顶新闻列表
        /// </summary>
        /// <param name="newsType"></param>
        /// <param name="hot"></param>
        /// <param name="elite"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<News> GetTopNewsList( int typeID, int hot, int elite, int top );

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        IList<News> GetNewsList();

        /// <summary>
        /// 获取分页新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetNewsList( int pageIndex, int pageSize );

        /// <summary>
        /// 获取分页新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="classID"></param>
        /// <returns></returns>
        PagerSet GetNewsList(int pageIndex, int pageSize, int classID);

        /// <summary>
        /// 获取新闻 by newsID
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        News GetNewsByNewsID( int newsID, byte mode );

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        Notice GetNotice( int noticeID );

        /// <summary>
        /// 获取移动版本公告
        /// </summary>
        /// <returns></returns>
        IList<News> GetMobileNotcie();

        /// <summary>
        /// 获取移动公告列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="classID"></param>
        /// <returns></returns>
        PagerSet GetMobileNotcieList(int pageIndex, int pageSize);

        /// <summary>
        /// 获取新闻的阅读状态
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<NewsReader> GetNewsState(int newsId, int userId);

        /// <summary>
        /// 更新新闻阅读状态
        /// </summary>
        /// <param name="newsId">NewsId</param>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        void UpdateNewsState(int newsId, int userId);

        #endregion

        #region 网站问题

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<GameIssueInfo> GetTopIssueList( int top );

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="top"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        IList<GameIssueInfo> GetTopIssueList(int top, int typeID);

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <returns></returns>
        IList<GameIssueInfo> GetIssueList();

        /// <summary>
        /// 获取分页问题列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetIssueList( int pageIndex, int pageSize );

        /// <summary>
        /// 获取分页问题列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetIssueList(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        GameIssueInfo GetIssueByIssueID( int issueID, byte mode );

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        GameIssueInfo GetIssueByIssueID(int typeID, int issueID, byte mode);
        #endregion

        #region 反馈意见

        /// <summary>
        /// 获取分页反馈意见列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetFeedbacklist(int pageIndex, int pageSize, int userId);

        /// <summary>
        /// 获取反馈意见实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        GameFeedbackInfo GetGameFeedBackInfo( int feedID, byte mode );

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="feedID"></param>
        void UpdateFeedbackViewCount( int feedID );

        /// <summary>
        /// 发表留言
        /// </summary>
        /// <returns></returns>
        Message PublishFeedback(GameFeedbackInfo info, string accounts);

        #endregion

        #region 游戏帮助数据

        /// <summary>
        /// 获取推荐游戏详细列表
        /// </summary>
        /// <returns></returns>
        DataSet GetGameHelps( int top );

        /// <summary>
        /// 获取游戏详细信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        GameRulesInfo GetGameHelp( int kindID );

        /// <summary>
        /// 获取手机版游戏
        /// </summary>
        /// <returns></returns>
        DataSet GetMoblieGame();

        #endregion

        #region 游戏比赛信息

        /// <summary>
        /// 得到比赛列表
        /// </summary>
        /// <returns></returns>
        IList<GameMatchInfo> GetMatchList();

        /// <summary>
        /// 得到比赛详细信息
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        GameMatchInfo GetMatchInfo( int matchID );

        /// <summary>
        /// 比赛报名
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Message AddGameMatch( GameMatchUserInfo userInfo, string password );

        #endregion

        #region 配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ConfigInfo GetConfigInfo( string key );
        #endregion

        #region 游戏商城
        /// <summary>
        /// 获取商品分类实体
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        AwardType GetAwardType( int typeID );

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        DataSet GetShopList( int counts );

        /// <summary>
        /// 获取商品顶级分类
        /// </summary>
        /// <returns></returns>
        DataSet GetShopTypeListByParentId( int parentID );

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="awradID"></param>
        /// <returns></returns>
        AwardInfo GetAwardInfo( int AwardID );

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="awardOrder"></param>
        /// <returns></returns>
        Message BuyAward( AwardOrder awardOrder );

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        PagerSet GetOrderList( int pageIndex, int pageSize, string where, string orderBy );

        /// <summary>
        /// 查询处理中订单数
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        int GetProcessingOrderCount( int userID );

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        AwardOrder GetAwardOrder( int orderID, int userID );

        /// <summary>
        /// 更新订单实体
        /// </summary>
        /// <param name="awardOrder"></param>
        int UpdateAwardOrderStatus( AwardOrder awardOrder );

        /// <summary>
        /// 查询最近一批订单
        /// </summary>
        /// <param name="count">查询条数</param>
        /// <returns>订单数据集</returns>
        DataSet GetTopOrder( int count );
        #endregion

        #region 广告

        /// <summary>
        /// 获取指定数目的广告数
        /// </summary>
        /// <param name="counts">查询数量</param>
        DataSet GetWebHomeAdsList( int counts );

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="ID">广告ID</param>
        /// <returns>广告实体</returns>
        Ads GetAds( int ID );

        #endregion

        #region 独立页管理

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        SinglePage GetSinglePage( string keyValue );

        #endregion

        #region 账号申诉

        /// <summary>
        /// 账号申诉
        /// </summary>
        /// <param name="lossReport">申诉实体</param>
        void SaveLossReport( LossReport lossReport );

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportNo">申诉号</param>
        /// <param name="account">游戏账号</param>
        /// <returns></returns>
        LossReport GetLossReport( string reportNo, string account );

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportNo">申诉号</param>
        /// <returns>申诉实体</returns>
        LossReport GetLossReport( string reportNo );

        #endregion

        #region 推荐活动

        /// <summary>
        /// 获取推荐活动
        /// </summary>
        /// <param name="counts"></param>
        /// <returns></returns>
        DataSet GetActivityList(int counts);

        /// <summary>
        /// 获取活动实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Activity GetActivity(int id);

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
        PagerSet GetList( string tableName, int pageIndex, int pageSize, string pkey, string whereQuery );

        #endregion
    }
}
