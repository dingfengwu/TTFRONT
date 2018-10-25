using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Entity.Accounts;
using System.Data;
using Game.Entity.Platform;

namespace Game.IData
{
    /// <summary>
    /// 平台库数据层接口
    /// </summary>
    public interface IPlatformDataProvider //: IProvider
    {
        /// <summary>
        /// 根据服务器地址获取数据库信息
        /// </summary>
        /// <param name="addrString"></param>
        /// <returns></returns>
        DataBaseInfo GetDatabaseInfo( string addrString );

        /// <summary>
        /// 根据游戏ID获取服务器地址信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        GameGameItem GetDBAddString( int kindID );

        /// <summary>
        /// 获取游戏类型列表
        /// </summary>
        /// <returns></returns>
        IList<GameTypeItem> GetGameTypes();

        /// <summary>
        /// 根据类型ID获取游戏列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        IList<GameKindItem> GetGameKindsByTypeID( int typeID );

        /// <summary>
        /// 获取热门游戏
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<GameKindItem> GetRecommendGame();

        /// <summary>
        /// 得到所有的游戏
        /// </summary>
        /// <returns></returns>
        IList<GameKindItem> GetAllKinds();

        /// <summary>
        /// 得到积分的游戏
        /// </summary>
        /// <returns></returns>
        IList<GameKindItem> GetIntegralKinds();

        /// <summary>
        /// 得到游戏列表
        /// </summary>
        /// <returns></returns>
        IList<GameGameItem> GetGameList();

        /// <summary>
        /// 获取签到配置
        /// </summary>
        /// <returns>签到配置数据集</returns>
        DataSet GetSigninConfigList();

        /// <summary>
        /// 获取手机游戏列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        DataSet GetMobileKindList(int typeID);

        #region 等级配置

        /// <summary>
        /// 获取等级配置
        /// </summary>
        /// <returns></returns>
        DataSet GetGrowLevelConfigList();
      
        #endregion

        #region 道具管理

        /// <summary>
        /// 获取手机道具类型
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        IList<GamePropertyType> GetMobilePropertyType(int tagID);

        /// <summary>
        /// 获取道具列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        IList<GameProperty> GetMobileProperty();

        /// <summary>
        /// 根据类型标识获取道具列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        IList<GameProperty> GetMobileProperty(int typeID);
        #endregion

        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        object GetObjectBySql( string sqlQuery );

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        T GetEntity<T>( string commandText );

        #endregion
    }
}
