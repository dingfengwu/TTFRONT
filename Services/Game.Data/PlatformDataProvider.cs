using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Game.Kernel;
using Game.IData;
using Game.Entity.Platform;
using System.Data.Common;

namespace Game.Data
{
    /// <summary>
    /// 平台数据访问层
    /// </summary>
    public class PlatformDataProvider : BaseDataProvider, IPlatformDataProvider
    {
        #region 构造方法

        public PlatformDataProvider(string connString)
            : base(connString)
        {

        }
        #endregion

        #region  数据库连接串
        /// <summary>
        /// 获取积分库的连接串
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public string GetConn(int kindID)
        {
            StringBuilder sb = new StringBuilder();
            GameGameItem game = GetDBAddString(kindID);
            if(game == null)
                return "";
            DataBaseInfo database = GetDatabaseInfo(game.DataBaseAddr);
            if(database == null)
                return "";
            string userID = Utils.CWHEncryptNet.XorCrevasse(database.DBUser);
            string password = Utils.CWHEncryptNet.XorCrevasse(database.DBPassword);
            sb.AppendFormat("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}; Pooling=true", game.DataBaseAddr + (string.IsNullOrEmpty(database.DBPort.ToString()) ? "" : ("," + database.DBPort.ToString())), game.DataBaseName, userID, password);
            return sb.ToString();
        }
        #endregion

        #region 游戏相关

        /// <summary>
        /// 根据服务器地址获取数据库信息
        /// </summary>
        /// <param name="addrString"></param>
        /// <returns></returns>
        public DataBaseInfo GetDatabaseInfo(string addrString)
        {
            string sqlQuery = string.Format("SELECT * FROM DataBaseInfo(NOLOCK) WHERE DBAddr=@DBAddr");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("DBAddr", addrString));

            DataBaseInfo dbInfo = Database.ExecuteObject<DataBaseInfo>(sqlQuery, prams);
            return dbInfo;
        }

        /// <summary>
        /// 根据游戏ID获取服务器地址信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameGameItem GetDBAddString(int kindID)
        {
            string sqlQuery = string.Format("SELECT GameName, DataBaseAddr, DataBaseName, ServerVersion, ClientVersion, ServerDLLName, ClientExeName FROM GameGameItem g,GameKindItem k WHERE KindID={0} AND g.GameID=k.GameID", kindID);
            GameGameItem game = Database.ExecuteObject<GameGameItem>(sqlQuery);

            return game;
        }

        /// <summary>
        /// 获取游戏类型列表
        /// </summary>
        /// <returns></returns>
        public IList<GameTypeItem> GetGameTypes()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT TypeID,TypeName ")
                    .Append("FROM GameTypeItem ")
                    .Append("WHERE Nullity=0 ")
                    .Append("ORDER By SortID ASC,TypeID ASC");

            return Database.ExecuteObjectList<GameTypeItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 根据类型ID获取游戏列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameKindItem> GetGameKindsByTypeID(int typeID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID, GameID, TypeID, SortID, KindName, ProcessName, GameRuleUrl, DownLoadUrl, Nullity ")
                    .Append("FROM GameKindItem ")
                    .AppendFormat("WHERE Nullity=0 AND TypeID={0} ", typeID)
                    .Append(" ORDER By SortID ASC,KindID ASC");
            return Database.ExecuteObjectList<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取热门游戏
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<GameKindItem> GetRecommendGame()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID, GameID, TypeID, SortID, KindName, ProcessName, GameRuleUrl, DownLoadUrl, Nullity ")
                    .Append("FROM GameKindItem ")
                    .Append("WHERE Nullity=0 AND JoinID=2")
                    .Append(" ORDER By SortID ASC,KindID ASC");

            return Database.ExecuteObjectList<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 得到所有的游戏
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetAllKinds()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID, GameID, TypeID, SortID, KindName, ProcessName, GameRuleUrl, DownLoadUrl, Nullity ")
                    .Append("FROM GameKindItem ")
                    .AppendFormat("WHERE Nullity=0 ")
                    .Append(" ORDER By SortID ASC,KindID ASC ");
            return Database.ExecuteObjectList<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 得到积分游戏
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetIntegralKinds()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID, GameID, TypeID, SortID, KindName, ProcessName, GameRuleUrl, DownLoadUrl, Nullity ")
                    .Append("FROM GameKindItem ")
                    .AppendFormat("WHERE Nullity=0 AND GameID NOT IN( SELECT GameID FROM GameGameItem WHERE DataBaseName = 'THTreasureDB' )")
                    .Append(" ORDER By SortID ASC,KindID ASC ");
            return Database.ExecuteObjectList<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 得到游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<GameGameItem> GetGameList()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT a.KindID,a.KindName,b.DataBaseAddr,b.DataBaseName ")
                    .Append(" FROM GameKindItem a,GameGameItem b ")
                    .Append("WHERE a.GameID = b.GameID ORDER BY SortID");
            return Database.ExecuteObjectList<GameGameItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取手机游戏列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public DataSet GetMobileKindList(int typeID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("SELECT KindID,KindName,TypeID,ModuleName,ClientVersion,ResVersion FROM MobileKindItem WHERE Nullity=0 AND KindMark&{0}>0", typeID);
            return Database.ExecuteDataset(sqlQuery.ToString());
        }

        #endregion

        #region 签到
        /// <summary>
        /// 获取签到配置
        /// </summary>
        /// <returns>签到配置数据集</returns>
        public DataSet GetSigninConfigList()
        {
            string sqlQuery = "SELECT * FROM SigninConfig ORDER BY DayID ASC";
            return Database.ExecuteDataset(sqlQuery);
        }
        #endregion

        #region 等级配置

        /// <summary>
        /// 获取等级配置
        /// </summary>
        /// <returns></returns>
        public DataSet GetGrowLevelConfigList()
        {
            string sql = "SELECT * FROM GrowLevelConfig ORDER BY LevelID DESC";
            return Database.ExecuteDataset(sql);
        }

        #endregion

        #region 道具管理

        /// <summary>
        /// 获取手机道具类型
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public IList<GamePropertyType> GetMobilePropertyType(int tagID)
        {
            string sqlQuery = string.Format("SELECT * FROM GamePropertyType(NOLOCK) WHERE TagID=@TagID AND Nullity=0");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("TagID", tagID));

            return Database.ExecuteObjectList<GamePropertyType>(sqlQuery, prams);
        }

        /// <summary>
        /// 获取道具列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameProperty> GetMobileProperty()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT * FROM GameProperty(NOLOCK) WHERE SuportMobile=1 ORDER BY ID ASC "); 
            return Database.ExecuteObjectList<GameProperty>(sqlQuery.ToString());
        }

        /// <summary>
        /// 根据类型标识获取道具列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameProperty> GetMobileProperty(int typeID)
        {
            string sqlQuery = string.Format("SELECT * FROM GameProperty(NOLOCK) WHERE MTypeID=@MTypeID AND SuportMobile=1");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MTypeID", typeID));

            return Database.ExecuteObjectList<GameProperty>(sqlQuery, prams);
        }
        #endregion

        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object GetObjectBySql(string sqlQuery)
        {
            return Database.ExecuteScalar(System.Data.CommandType.Text, sqlQuery);
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

        #endregion
    }
}
