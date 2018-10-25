using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using System.Data;
using System.Collections;

namespace Game.Facade
{
    /// <summary>
    /// 用户外观
    /// </summary>
    public class AccountsFacade
    {
        #region Fields

        public IAccountsDataProvider accountsData;
        public ITreasureDataProvider treasureData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsFacade()
        {
            accountsData = ClassFactory.GetIAccountsDataProvider();
            treasureData = ClassFactory.GetITreasureDataProvider();
        }
        #endregion

        #region 用户登录、注册

        /// <summary>
        /// 用户登录
        /// </summary>
        public Message Logon( UserInfo user, bool isEncryptPasswd )
        {
            Message umsg;
            if( !isEncryptPasswd )
            {
                user.LogonPass = TextEncrypt.EncryptPassword( user.LogonPass );
            }

            umsg = accountsData.Login( user );
            return umsg;
        }

        /// <summary>
        /// 用户登录，登录密码必须是密文。并且验证登录参数
        /// </summary>
        /// <param name="stationID">站点标识</param>
        /// <param name="accounts">用户名</param>
        /// <param name="logonPasswd">密文密码</param>
        /// <param name="ip">登录地址</param>
        /// <returns>返回网站消息，若登录成功将携带用户对象</returns>
        public Message Logon( string accounts, string logonPasswd )
        {
            UserInfo suInfo = new UserInfo();
            suInfo.Accounts = accounts;
            suInfo.LogonPass = logonPasswd;
            suInfo.LastLogonIP = GameRequest.GetUserIP();
            return Logon( suInfo, false );
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message Register( UserInfo user, string parentAccount )
        {
            return accountsData.Register( user, parentAccount );
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Message IsAccountsExist( string accounts )
        {
            return accountsData.IsAccountsExist( accounts );
        }

        /// <summary>
        /// 判断昵称是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Message IsNickNameExist( string nickName )
        {
            return accountsData.IsNickNameExist( nickName );
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 根据用户昵称获取用户ID
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public int GetUserIDByNickName( string nickName )
        {
            return accountsData.GetUserIDByNickName( nickName );
        }

        /// <summary>
        /// 根据用户ID获取用户帐号
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetAccountsByUserID(int userID)
        {
            return accountsData.GetAccountsByUserID(userID);
        }

        /// <summary>
        /// 根据用户ID获取用户昵称
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetNickNameByUserID(int userID)
        {
            return accountsData.GetNickNameByUserID(userID);
        }

        /// <summary>
        /// 根据用户ID获取GameID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetGameIDByUserID(int userID)
        {
            return accountsData.GetGameIDByUserID(userID);
        }

        /// <summary>
        /// 获取基本用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserInfo GetUserBaseInfoByUserID( int userID )
        {
            return accountsData.GetUserBaseInfoByUserID( userID );
        }

        /// <summary>
        /// 获取用户名通过二级域名
        /// </summary>
        /// <param name="subDomain"></param>
        /// <returns></returns>
        public string GetAccountsBySubDomain( string subDomain )
        {
            return accountsData.GetAccountsBySubDomain( subDomain );
        }

        /// <summary>
        /// 获取指定用户联系信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns> 		
        public IndividualDatum GetUserContactInfoByUserID( int userID )
        {
            return accountsData.GetUserContactInfoByUserID( userID );
        }

        /// <summary>
        /// 获取用户全局信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gameID"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        public Message GetUserGlobalInfo( int userID, int gameID, String Accounts )
        {
            return accountsData.GetUserGlobalInfo( userID, gameID, Accounts );
        }

        /// <summary>
        /// 获取密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public string GetPasswordCardByUserID( int userId )
        {
            return accountsData.GetPasswordCardByUserID( userId );
        }

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <returns></returns>
        public AccountsFace GetAccountsFace( int customId )
        {
            return accountsData.GetAccountsFace( customId );
        }

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public AccountsFace GetAccountsFace( int customId, int userId )
        {
            return accountsData.GetAccountsFace( customId, userId );
        }

        /// <summary>
        /// 获取用户头像URL地址
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserFaceUrl(int userID)
        {
            string faceUrl = string.Empty;
            Message msg = GetUserGlobalInfo(userID, 0, "");
            if (msg.Success)
            {
                UserInfo ui = msg.EntityList[0] as UserInfo;
                faceUrl = GetUserFaceUrl(ui.FaceID, ui.CustomID);
            }
            return faceUrl;
        }

        /// <summary>
        /// 获取用户头像URL地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetUserFaceUrl( int faceID, int customID )
        {
            string faceUrl = string.Empty;
            if( customID == 0 )
            {
                // 使用系统头像
                faceUrl = string.Format("/gamepic/Avatar{0}.png", faceID);
            }
            else
            {
                AccountsFace faceModel = GetAccountsFace( customID );
                if( faceModel == null )
                {
                    // 使用系统头像
                    faceUrl = string.Format("/gamepic/Avatar{0}.png", faceID);
                }
                else
                {
                    // 生成随机数
                    Random rand = new Random();
                    double randomNumber = rand.NextDouble();

                    // 自定义头像
                    faceUrl = string.Format( "/WS/UserFace.ashx?customid={0}&x={1}", customID, randomNumber );
                }
            }
            return faceUrl;
        }

        /// <summary>
        /// 判断自定义头像是否存在
        /// </summary>
        /// <param name="customID">自定义头像标识</param>
        /// <returns>存在返回：True 否则返回：False</returns>
        public bool ExistCustomFace( int customID ) 
        {
            if( customID == 0 )
            {
                return false;
            }
            else
            {
                AccountsFace faceModel = GetAccountsFace( customID );
                if( faceModel == null )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 获取帐号列表
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public IList<AccountsInfo> GetAccountsInfoList(ArrayList arrID)
        {
            return accountsData.GetAccountsInfoList(arrID);
        }

        /// <summary>
        /// 获取经验排行
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet GetExperienceRank(int count)
        {
            return accountsData.GetExperienceRank(count);
        }

        /// <summary>
        /// 移动端通讯验证签名
        /// </summary>
        /// <returns></returns>
        public Message CheckUserSignature(int userID, object time, string signature)
        {
            Message message = new Message();
            message.Success = false;

            // 验证用户
            if (userID == 0 || time == null || string.IsNullOrEmpty(signature) || !Validate.IsDouble(time))
            {
                message.Content = "参数错误！";
                return message;
            }
            message = accountsData.GetUserGlobalInfo(userID, 0, "");
            if (!message.Success)
            {
                message.Content = "不存在的用户！";
                return message;
            }
            UserInfo userInfo = message.EntityList[0] as UserInfo;
            if (userInfo == null)
            {
                message.Content = "不存在的用户！";
                return message;
            }
            message.Success = false;

            // 验证是否有动态密码
            if (string.IsNullOrEmpty(userInfo.DynamicPass.Trim()))
            {
                message.Content = "用户数据错误！";
                return message;
            }

            // 验证签名
            // 签名加密方法 MD5(UserID+动态密码+Time+Key);
            string md5Str = userID.ToString() + userInfo.DynamicPass + time.ToString() + AppConfig.SyncLoginKey;
            string webSignature = Utility.MD5(md5Str);
            if (signature != webSignature)
            {
                message.Content = "签名错误！";
                return message;
            }

            // 验证链接有效期
            DateTime dtOut = userInfo.DynamicPassTime.AddMilliseconds(Convert.ToDouble(time) + Convert.ToDouble(AppConfig.SyncUrlTimeOut));
            if (dtOut < DateTime.Now)
            {
                message.Content = "请求超时！";
                return message;
            }


            message.Success = true;
            return message;
        }

        /// <summary>
        /// 移动端通讯验证签名
        /// </summary>
        /// <returns></returns>
        public Message CheckUserSignature2(int userID, object time, string signature, ref DateTime? dtTest)
        {
            Message message = new Message();
            message.Success = false;

            // 验证用户
            if (userID == 0 || time == null || string.IsNullOrEmpty(signature) || !Validate.IsDouble(time))
            {
                message.Content = "参数错误！";
                return message;
            }
            message = accountsData.GetUserGlobalInfo(userID, 0, "");
            if (!message.Success)
            {
                message.Content = "不存在的用户！";
                return message;
            }
            UserInfo userInfo = message.EntityList[0] as UserInfo;
            if (userInfo == null)
            {
                message.Content = "不存在的用户！";
                return message;
            }
            message.Success = false;

            // 验证是否有动态密码
            if (string.IsNullOrEmpty(userInfo.DynamicPass.Trim()))
            {
                message.Content = "用户数据错误！";
                return message;
            }

            // 验证签名
            // 签名加密方法 MD5(UserID+动态密码+Time+Key);
            string md5Str = userID.ToString() + userInfo.DynamicPass + time.ToString() + AppConfig.SyncLoginKey;
            string webSignature = Utility.MD5(md5Str);
            if (signature != webSignature)
            {
                message.Content = "签名错误！";
                return message;
            }

            // 验证链接有效期
            DateTime dtOut = userInfo.DynamicPassTime.AddMilliseconds(Convert.ToDouble(time) + Convert.ToDouble(AppConfig.SyncUrlTimeOut));
            dtTest = dtOut;
            if (dtOut < DateTime.Now)
            {
                message.Content = "请求超时！";
                return message;
            }


            message.Success = true;
            return message;
        }
        #endregion

        #region	 密码管理

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        public Message ResetLogonPasswd( AccountsProtect sInfo )
        {
            return accountsData.ResetLogonPasswd( sInfo );
        }

        /// <summary>
        /// 通过账号申诉重置登陆密码
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        /// <param name="reportNo">申诉号</param>
        /// <returns></returns>
        public Message ResetLoginPasswdByLossReport( UserInfo userInfo, string reportNo )
        {
            return accountsData.ResetLoginPasswdByLossReport( userInfo, reportNo );
        }

        /// <summary>
        /// 重置银行密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        public Message ResetInsurePasswd( AccountsProtect sInfo )
        {
            return accountsData.ResetInsurePasswd( sInfo );
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="srcPassword">旧密码</param>
        /// <param name="dstPassword">新密码</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ModifyLogonPasswd( int userID, string srcPassword, string dstPassword, string ip )
        {
            return accountsData.ModifyLogonPasswd( userID, srcPassword, dstPassword, ip );
        }

        /// <summary>
        /// 修改银行密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="srcPassword">旧密码</param>
        /// <param name="dstPassword">新密码</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ModifyInsurePasswd( int userID, string srcPassword, string dstPassword, string ip )
        {
            return accountsData.ModifyInsurePasswd( userID, srcPassword, dstPassword, ip );
        }

        #endregion

        #region  密码保护管理

        /// <summary>
        /// 申请帐号保护
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message ApplyUserSecurity( AccountsProtect sInfo )
        {
            return accountsData.ApplyUserSecurity( sInfo );
        }

        /// <summary>
        /// 修改帐号保护
        /// </summary>
        /// <param name="oldInfo">旧密保信息</param>
        /// <param name="newInfo">新密保信息</param>
        /// <returns></returns>
        public Message ModifyUserSecurity( AccountsProtect newInfo )
        {
            return accountsData.ModifyUserSecurity( newInfo );
        }

        /// <summary>
        /// 获取密保信息 (userID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByUserID( int userID )
        {
            return accountsData.GetUserSecurityByUserID( userID );
        }

        /// <summary>
        /// 获取密保信息 (gameID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByGameID( int gameID )
        {
            return accountsData.GetUserSecurityByGameID( gameID );
        }

        /// <summary>
        /// 获取密保信息 (Accounts)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByAccounts( string accounts )
        {
            return accountsData.GetUserSecurityByAccounts( accounts );
        }

        /// <summary>
        /// 密保确认
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message ConfirmUserSecurity( AccountsProtect info )
        {
            return accountsData.ConfirmUserSecurity( info );
        }

        #endregion

        #region 固定机器

        /// <summary>
        /// 申请机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message ApplyUserMoorMachine( AccountsProtect sInfo )
        {
            return accountsData.ApplyUserMoorMachine( sInfo );
        }

        /// <summary>
        /// 解除机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message RescindUserMoorMachine( AccountsProtect sInfo )
        {
            return accountsData.RescindUserMoorMachine( sInfo );
        }

        #endregion 固定机器结束

        #region 资料管理

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message ModifyUserIndividual( IndividualDatum user ,AccountsInfo info)
        {
            return accountsData.ModifyUserIndividual(user, info);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="faceID"></param>
        /// <returns></returns>
        public Message ModifyUserFace( int userID, int faceID )
        {
            return accountsData.ModifyUserFace( userID, faceID );
        }

        /// <summary>
        /// 更新用户昵称
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="faceID"></param>
        /// <returns></returns>
        public Message ModifyUserNickname( int userID, string nickName, string ip )
        {
            return accountsData.ModifyUserNickname( userID, nickName, ip );
        }

        #endregion

        #region 魅力兑换

        public Message UserConvertPresent( int userID, int present, string ip )
        {
            return accountsData.UserConvertPresent( userID, present, ip );
        }

        /// <summary>
        /// 根据用户魅力排名(前10名)
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> GetUserInfoOrderByLoves()
        {
            return accountsData.GetUserInfoOrderByLoves();
        }
        public IAccountsDataProvider DataProvider
        {
            get
            {
                return accountsData;
            }
        }

        /// <summary>
        /// 获取魅力排行榜
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataSet GetLovesRanking( int num )
        {
            return accountsData.GetLovesRanking( num );
        }

        #endregion

        #region 奖牌兑换

        public Message UserConvertMedal( int userID, int medals, string ip )
        {
            return accountsData.UserConvertMedal( userID, medals, ip );
        }

        #endregion

        #region 密保卡

        /// <summary>
        /// 检测密保卡序列号是否存在
        /// </summary>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool PasswordIDIsEnable( string serialNumber )
        {
            return accountsData.PasswordIDIsEnable( serialNumber );
        }

        /// <summary>
        /// 检测用户是否绑定了密保卡
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool userIsBindPasswordCard( int userId )
        {
            return accountsData.userIsBindPasswordCard( userId );
        }


        /// <summary>
        /// 更新用户密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool UpdateUserPasswordCardID( int userId, int serialNumber )
        {
            return accountsData.UpdateUserPasswordCardID( userId, serialNumber );
        }

        /// <summary>
        /// 取消密保卡绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ClearUserPasswordCardID( int userId )
        {
            return accountsData.ClearUserPasswordCardID( userId );
        }

        #endregion

        #region 配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key">配置Key值</param>
        /// <returns>SystemStatusInfo实体</returns>
        public SystemStatusInfo GetSystemStatusInfo( string key )
        {
            return accountsData.GetSystemStatusInfo( key );
        }

        /// <summary>
        /// 获取会员配置
        /// </summary>
        /// <param name="memberOrder"></param>
        /// <returns></returns>
        public MemberProperty GetMemberProperty(int memberOrder)
        {
            return accountsData.GetMemberProperty(memberOrder);
        }
        #endregion

        #region 签到

        /// <summary>
        /// 获取用户签到实体
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>签到实体</returns>
        public AccountsSignin GetAccountsSignin( int userId )
        {
            return accountsData.GetAccountsSignin( userId );
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Message Signin( int userId, string ip )
        {
            return accountsData.Signin( userId, ip );
        }
        #endregion

        #region 代理商管理

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByUserID(int userID)
        {
            return accountsData.GetAccountAgentByUserID(userID);
        }

        /// <summary>
        /// 获取代理商基本信息通过用户域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByDomain(string domain)
        {
            return accountsData.GetAccountAgentByDomain(domain);
        }

        /// <summary>
        /// 获取代理下级玩家数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentChildCount(int userID)
        {
            return accountsData.GetAgentChildCount(userID);
        }
        #endregion

        #region 自定义头像

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="accountsFace"></param>
        /// <returns></returns>
        public Message InsertCustomFace(AccountsFace accountsFace)
        {
            return accountsData.InsertCustomFace(accountsFace);
        }

        #endregion

        #region 公共

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            return accountsData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object GetObjectBySql( string sqlQuery )
        {
            return accountsData.GetObjectBySql( sqlQuery );
        }

        #endregion
    }
}
