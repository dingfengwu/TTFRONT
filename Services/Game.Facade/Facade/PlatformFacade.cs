using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Platform;
using System.Data;

namespace Game.Facade
{
    /// <summary>
    /// 平台外观
    /// </summary>
    public class PlatformFacade
    {
        #region Fields

        private IPlatformDataProvider platformData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformFacade()
        {
            platformData = ClassFactory.GetIPlatformDataProvider();
        }
        #endregion

        /// <summary>
        /// 根据服务器地址获取数据库信息
        /// </summary>
        /// <param name="addrString"></param>
        /// <returns></returns>
        public DataBaseInfo GetDatabaseInfo(string addrString)
        {
            return platformData.GetDatabaseInfo(addrString);
        }

        /// <summary>
        /// 根据游戏ID获取服务器地址信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameGameItem GetDBAddString(int kindID)
        {
            return platformData.GetDBAddString(kindID);
        }

        /// <summary>
        /// 获取游戏类型列表
        /// </summary>
        /// <returns></returns>
        public IList<GameTypeItem> GetGameTypes()
        {
            return platformData.GetGameTypes();
        }

        /// <summary>
        /// 根据类型ID获取游戏列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameKindItem> GetGameKindsByTypeID(int typeID)
        {
            return platformData.GetGameKindsByTypeID(typeID);
        }

        /// <summary>
        /// 获取热门游戏
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<GameKindItem> GetRecommendGame()
        {
            return platformData.GetRecommendGame();
        }

        /// <summary>
        /// 得到所有的游戏
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetAllKinds()
        {
            return platformData.GetAllKinds();
        }

        /// <summary>
        /// 得到所有的游戏
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetIntegralKinds( )
        {
            return platformData.GetIntegralKinds( );
        }

        /// <summary>
        /// 得到游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<GameGameItem> GetGameList()
        {
            return platformData.GetGameList();
        }

        /// <summary>
        /// 获取签到配置
        /// </summary>
        /// <returns>签到配置数据集</returns>
        public DataSet GetSigninConfigList()
        {
            return platformData.GetSigninConfigList();
        }

        /// <summary>
        /// 获取手机游戏列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public DataSet GetMobileKindList(int typeID)
        {
            return platformData.GetMobileKindList(typeID);
        }

        #region 等级配置

        /// <summary>
        /// 获取等级配置
        /// </summary>
        /// <returns></returns>
        public DataSet GetGrowLevelConfigList()
        {
            return platformData.GetGrowLevelConfigList();
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
            return platformData.GetMobilePropertyType(tagID);
        }

        /// <summary>
        /// 获取道具列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameProperty> GetMobileProperty()
        {
            return platformData.GetMobileProperty();
        }

        /// <summary>
        /// 根据类型标识获取道具列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<GameProperty> GetMobileProperty(int typeID)
        {
            return platformData.GetMobileProperty(typeID);
        }
        #endregion

        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object GetObjectBySql( string sqlQuery )
        {
            return platformData.GetObjectBySql( sqlQuery );
        }

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>( string commandText )
        {
            return platformData.GetEntity<T>( commandText );
        }

        #endregion
    }
}
