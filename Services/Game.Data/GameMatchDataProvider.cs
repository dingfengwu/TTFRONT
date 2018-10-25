using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.IData;
using Game.Kernel;
using Game.Entity.GameMatch;
using System.Text;

namespace Game.Data
{
    public class GameMatchDataProvider : BaseDataProvider,IGameMatchProvider
    {
        #region 构造方法

        public GameMatchDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion 构造方法

        #region 公共

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="fields">查询字段</param>
        /// <param name="fieldAlias"></param>
        /// <returns></returns>
        public PagerSet GetList(string tableName,int pageIndex,int pageSize,string pkey,string whereQuery,string[] fields,string[] fieldAlias)
        {
            PagerParameters pager = new PagerParameters(tableName,pkey,whereQuery,pageIndex,pageSize,fields,fieldAlias);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="fields">查询字段</param>
        /// <returns></returns>
        public PagerSet GetList(string tableName,int pageIndex,int pageSize,string pkey,string whereQuery,string[] fields)
        {
            PagerParameters pager = new PagerParameters(tableName,pkey,whereQuery,pageIndex,pageSize,fields);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        /// <param name="whereQuery">查询条件</param>
        public PagerSet GetList(string tableName,int pageIndex,int pageSize,string pkey,string whereQuery)
        {
            PagerParameters pager = new PagerParameters(tableName,pkey,whereQuery,pageIndex,pageSize);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        public PagerSet GetList(string tableName,int pageIndex,int pageSize,string pkey)
        {
            PagerParameters pager = new PagerParameters(tableName,pkey,pageIndex,pageSize);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        public PagerSet GetList(string tableName,int pageIndex,int pageSize)
        {
            PagerParameters pager = new PagerParameters(tableName,"",pageIndex,pageSize);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">排序或分组</param>
        /// <param name="pageSize">页大小</param>
        public PagerSet GetAllList(string tableName,string pkey,string whereQuery)
        {
            PagerParameters pager = new PagerParameters(tableName,pkey,whereQuery,1,Int32.MaxValue);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">排序或分组</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageSize">查询的数量</param>
        public PagerSet GetNumberList(string tableName,string whereQuery,string pkey,int number)
        {
            PagerParameters pager = new PagerParameters(tableName,pkey,whereQuery,1,number);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 根据sql语句获取数据
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public DataSet GetDataSetByWhere(string sqlQuery)
        {
            return Database.ExecuteDataset(sqlQuery);
        }

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>(string commandText,List<DbParameter> parms)
        {
            return Database.ExecuteObject<T>(commandText,parms);
        }

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>(string commandText)
        {
            return Database.ExecuteObject<T>(commandText);
        }

        #endregion 公共

        #region 比赛

        /// <summary>
        /// 获取比赛列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetMatchList(int pageIndex,int pageSize)
        {
            string dataSource = "( SELECT A.*,B.MatchType,B.MatchStatus FROM MatchInfo AS A INNER JOIN MatchPublic AS B ON A.MatchID=B.MatchID) AS C";
            string where = string.Empty;
            PagerParameters pager = new PagerParameters(dataSource,"ORDER BY MatchID DESC",where,pageIndex,pageSize);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取比赛公共配置
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public MatchPublic GetMatchPublic(int matchId)
        {
            string sql = "SELECT * FROM MatchPublic WHERE MatchID=@MatchID";
            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("MatchID",matchId));
            return Database.ExecuteObject<MatchPublic>(sql,param);
        }

        /// <summary>
        /// 获取比赛展示信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public MatchInfo GetMatchInfo(int matchId)
        {
            string sql = "SELECT * FROM MatchInfo WHERE MatchID=@MatchID";
            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("MatchID",matchId));
            return Database.ExecuteObject<MatchInfo>(sql, param);
        }

        /// <summary>
        /// 获取比赛公共配置列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetMatchPublicList()
        {
            string sql = "SELECT * FROM MatchPublic";
            return Database.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取比赛奖励配置
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataSet GetMatchRewardList(int matchId)
        {
            string sql = "SELECT * FROM MatchReward WHERE MatchID=@MatchID";
            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("MatchID",matchId));
            return Database.ExecuteDataset(CommandType.Text,sql,param.ToArray());
        }
       
        #endregion

        #region 比赛排名

        /// <summary>
        /// 获取某场赛事的最近的排名
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataSet GetRecentlyMatchRank(int matchId)
        {
            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("MatchID",matchId));
            return Database.ExecuteDataset(CommandType.StoredProcedure,"NET_PW_GetRecentlyMatchRank",param.ToArray());
        }

        #endregion
    }
}