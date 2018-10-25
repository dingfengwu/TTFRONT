using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.IData;
using Game.Entity.Accounts;
using System.Data.Common;
using System.Data;
using System.Collections;

namespace Game.Data
{
    /// <summary>
    /// 用户数据访问层
    /// </summary>
    public class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
    {
        #region 构造方法

        public AccountsDataProvider(string connString)
            : base(connString)
        {

        }
        #endregion

        #region 用户登录、注册

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="names">用户信息</param>
        /// <returns></returns>
        public Message Login(UserInfo user)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", user.Accounts));
            prams.Add(Database.MakeInParam("strPassword", user.LogonPass));
            prams.Add(Database.MakeInParam("strClientIP", user.LastLogonIP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<UserInfo>(Database, "NET_PW_EfficacyAccounts", prams);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message Register(UserInfo user, string parentAccount)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strSpreader", parentAccount));
            prams.Add(Database.MakeInParam("strAccounts", user.Accounts));
            prams.Add(Database.MakeInParam("strNickname", user.NickName));

            prams.Add(Database.MakeInParam("strLogonPass", user.LogonPass));
            prams.Add(Database.MakeInParam("strInsurePass", user.InsurePass));
            prams.Add(Database.MakeInParam("strDynamicPass", user.DynamicPass));

            prams.Add(Database.MakeInParam("strCompellation", user.Compellation));
            prams.Add(Database.MakeInParam("strPassPortID", user.PassPortID));

            prams.Add(Database.MakeInParam("dwFaceID", user.FaceID));
            prams.Add(Database.MakeInParam("dwGender", user.Gender));

            prams.Add(Database.MakeInParam("strClientIP", user.RegisterIP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<UserInfo>(Database, "NET_PW_RegisterAccounts", prams);
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Message IsAccountsExist(string accounts)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_IsAccountsExists", prams);
        }

        /// <summary>
        /// 判断昵称是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Message IsNickNameExist(string nickName)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strNickName", nickName));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_IsNickNameExist", prams);
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 根据用户昵称获取用户ID
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public int GetUserIDByNickName(string nickName)
        {
            string sqlQuery = string.Format("SELECT NickName,UserID FROM AccountsInfo(NOLOCK) WHERE NickName=@NickName");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("NickName", nickName));

            UserInfo user = Database.ExecuteObject<UserInfo>(sqlQuery, prams);
            return user == null ? 0 : user.UserID;
        }
        public DbHelper GetDbHelper()
        {
            return Database;
        }

        /// <summary>
        /// 根据用户ID获取用户帐号
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetAccountsByUserID(int userID)
        {
            string sqlQuery = string.Format("SELECT Accounts FROM AccountsInfo(NOLOCK) WHERE UserID=@UserID");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userID));

            object o = Database.ExecuteScalar(CommandType.Text, sqlQuery, prams.ToArray());
            return o == null ? "" : o.ToString();
        }

        /// <summary>
        /// 根据用户ID获取用户昵称
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetNickNameByUserID(int userID)
        {
            string sqlQuery = string.Format("SELECT NickName FROM AccountsInfo(NOLOCK) WHERE UserID=@UserID");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userID));

            object o = Database.ExecuteScalar(CommandType.Text, sqlQuery, prams.ToArray());
            return o == null ? "" : o.ToString();
        }

        /// <summary>
        /// 根据用户ID获取GameID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetGameIDByUserID(int userID)
        {
            string sqlQuery = string.Format("SELECT GameID FROM AccountsInfo(NOLOCK) WHERE UserID=@UserID");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userID));

            object o = Database.ExecuteScalar(CommandType.Text, sqlQuery, prams.ToArray());
            return o == null ? 0 : Convert.ToInt32(o);
        }

        /// <summary>
        /// 获取基本用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserInfo GetUserBaseInfoByUserID(int userID)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return Database.RunProcObject<UserInfo>("NET_PW_GetUserBaseInfo", prams);
        }

        /// <summary>
        /// 获取用户名通过二级域名
        /// </summary>
        /// <param name="subDomain"></param>
        /// <returns></returns>
        public string GetAccountsBySubDomain(string subDomain)
        {
            string sqlQuery = "SELECT Accounts FROM AccountsInfo WHERE GameID=@SubDomain";

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("SubDomain", subDomain));

            AccountsInfo model = Database.ExecuteObject<AccountsInfo>(sqlQuery, prams);
            if(model != null)
            {
                return model.Accounts;
            }
            return "";
        }

        /// <summary>
        /// 获取指定用户联系信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns> 		
        public IndividualDatum GetUserContactInfoByUserID(int userID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return Database.RunProcObject<IndividualDatum>("NET_PW_GetUserContactInfo", parms);
        }

        /// <summary>
        /// 获取用户全局信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gameID"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        public Message GetUserGlobalInfo(int userID, int gameID, string Accounts)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwGameID", gameID));
            parms.Add(Database.MakeInParam("strAccounts", Accounts));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<UserInfo>(Database, "NET_PW_GetUserGlobalsInfo", parms);
        }

        /// <summary>
        /// 获取密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public string GetPasswordCardByUserID(int userId)
        {
            string sqlQuery = string.Format("SELECT PasswordID FROM AccountsInfo(NOLOCK) WHERE UserID=@UserID");
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("UserID", userId));

            return Database.ExecuteScalarToStr(System.Data.CommandType.Text, sqlQuery, parms.ToArray());
        }

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <returns></returns>
        public AccountsFace GetAccountsFace(int customId)
        {
            string sqlQuery = "SELECT * FROM AccountsFace WHERE ID=@ID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", customId));

            AccountsFace model = Database.ExecuteObject<AccountsFace>(sqlQuery, prams);
            return model;
        }

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public AccountsFace GetAccountsFace(int customId, int userId)
        {
            string sqlQuery = "SELECT * FROM AccountsFace WHERE ID=@ID AND UserID=@UserID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", customId));
            prams.Add(Database.MakeInParam("UserID", userId));

            AccountsFace model = Database.ExecuteObject<AccountsFace>(sqlQuery, prams);
            return model;
        }

        /// <summary>
        /// 获取帐号列表
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public IList<AccountsInfo> GetAccountsInfoList(ArrayList arrID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT UserID,Accounts,NickName,GameID,FaceID,CustomID,Gender FROM AccountsInfo WHERE UserID IN(");
            for (int i = 0; i < arrID.Count; i++)
            {
                if (i > 0)
                    sql.Append(",");
                sql.AppendFormat("{0}", arrID[i]);
            }
            sql.Append(")");

            return Database.ExecuteObjectList<AccountsInfo>(sql.ToString());
        }

        /// <summary>
        /// 获取经验排行
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet GetExperienceRank(int count)
        {
            string sqlQuery = string.Format("SELECT TOP {0} UserID,GameID,Accounts,NickName,Experience FROM AccountsInfo WHERE Nullity=0 AND Experience>0 AND IsAndroid=0 ORDER BY Experience DESC", count);
            return Database.ExecuteDataset(sqlQuery);
        }

        #endregion

        #region	 密码管理

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        public Message ResetLogonPasswd(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strPassword", sInfo.LogonPass));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ResetLogonPasswd", parms);
        }

        /// <summary>
        /// 通过账号申诉重置登陆密码
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        /// <param name="reportNo">申诉号</param>
        /// <returns></returns>
        public Message ResetLoginPasswdByLossReport(UserInfo userInfo, string reportNo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userInfo.UserID));
            parms.Add(Database.MakeInParam("strLogonPass", userInfo.LogonPass));
            parms.Add(Database.MakeInParam("strReportNo", reportNo));
            parms.Add(Database.MakeInParam("strClientIP", userInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ResetLoginPasswdByLossReport", parms);
        }

        /// <summary>
        /// 重置银行密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        public Message ResetInsurePasswd(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strInsurePass", sInfo.InsurePass));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ResetInsurePasswd", parms);
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="srcPassword">旧密码</param>
        /// <param name="dstPassword">新密码</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ModifyLogonPasswd(int userID, string srcPassword, string dstPassword, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strSrcPassword", srcPassword));
            parms.Add(Database.MakeInParam("strDesPassword", dstPassword));

            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyLogonPass", parms);
        }

        /// <summary>
        /// 修改银行密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="srcPassword">旧密码</param>
        /// <param name="dstPassword">新密码</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ModifyInsurePasswd(int userID, string srcPassword, string dstPassword, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strSrcPassword", srcPassword));
            parms.Add(Database.MakeInParam("strDesPassword", dstPassword));

            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyInsurePass", parms);
        }

        #endregion

        #region  密码保护管理

        /// <summary>
        /// 申请帐号保护
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message ApplyUserSecurity(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));

            parms.Add(Database.MakeInParam("strQuestion1", sInfo.Question1));
            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strQuestion2", sInfo.Question2));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strQuestion3", sInfo.Question3));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("dwPassportType", (byte)sInfo.PassportType));
            parms.Add(Database.MakeInParam("strPassportID", sInfo.PassportID));
            parms.Add(Database.MakeInParam("strSafeEmail", sInfo.SafeEmail));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.CreateIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_AddAccountProtect", parms);
        }

        /// <summary>
        /// 修改帐号保护
        /// </summary>
        /// <param name="oldInfo">旧密保信息</param>
        /// <param name="newInfo">新密保信息</param>
        /// <returns></returns>
        public Message ModifyUserSecurity(AccountsProtect newInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", newInfo.UserID));

            parms.Add(Database.MakeInParam("strQuestion1", newInfo.Question1));
            parms.Add(Database.MakeInParam("strResponse1", newInfo.Response1));
            parms.Add(Database.MakeInParam("strQuestion2", newInfo.Question2));
            parms.Add(Database.MakeInParam("strResponse2", newInfo.Response2));
            parms.Add(Database.MakeInParam("strQuestion3", newInfo.Question3));
            parms.Add(Database.MakeInParam("strResponse3", newInfo.Response3));

            parms.Add(Database.MakeInParam("strSafeEmail", newInfo.SafeEmail));

            parms.Add(Database.MakeInParam("strClientIP", newInfo.ModifyIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyAccountProtect", parms);
        }

        /// <summary>
        /// 获取密保信息 (userID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByUserID(int userID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "NET_PW_GetAccountProtectByUserID", parms);
        }

        /// <summary>
        /// 获取密保信息 (gameID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByGameID(int gameID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwGameID", gameID));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "NET_PW_GetAccountProtectByGameID", parms);
        }

        /// <summary>
        /// 获取密保信息 (Accounts)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByAccounts(string accounts)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strAccounts", accounts));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "NET_PW_GetAccountProtectByAccounts", parms);
        }

        /// <summary>
        /// 密保确认
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message ConfirmUserSecurity(AccountsProtect info)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", info.UserID));

            parms.Add(Database.MakeInParam("strResponse1", info.Response1));
            parms.Add(Database.MakeInParam("strResponse2", info.Response2));
            parms.Add(Database.MakeInParam("strResponse3", info.Response3));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ConfirmAccountProtect", parms);
        }

        #endregion

        #region 安全管理

        #region 固定机器

        /// <summary>
        /// 申请机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message ApplyUserMoorMachine(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ApplyMoorMachine", parms);
        }

        /// <summary>
        /// 解除机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message RescindUserMoorMachine(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_CancelMoorMachine", parms);
        }

        #endregion 固定机器结束

        #endregion

        #region 资料管理

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message ModifyUserIndividual(IndividualDatum user,AccountsInfo info)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", user.UserID));

            parms.Add(Database.MakeInParam("dwGender", (byte)info.Gender));
            parms.Add(Database.MakeInParam("strUnderWrite", info.UnderWrite));
          

            parms.Add(Database.MakeInParam("strQQ", user.QQ));
            parms.Add(Database.MakeInParam("strEmail", user.EMail));
            parms.Add(Database.MakeInParam("strSeatPhone", user.SeatPhone));
            parms.Add(Database.MakeInParam("strMobilePhone", user.MobilePhone));
            parms.Add(Database.MakeInParam("strDwellingPlace", user.DwellingPlace));
            parms.Add(Database.MakeInParam("strUserNote", user.UserNote));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyUserInfo", parms);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="faceID"></param>
        /// <returns></returns>
        public Message ModifyUserFace(int userID, int faceID)
        {
            List<DbParameter> prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwFaceID", faceID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyUserFace", prams);
        }

        /// <summary>
        /// 更新用户昵称
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="faceID"></param>
        /// <returns></returns>
        public Message ModifyUserNickname(int userID, string nickName, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("strNickName", nickName));
            prams.Add(Database.MakeInParam("clientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyUserNickName", prams);
        }

        #endregion

        #region 魅力

        public Message UserConvertPresent(int userID, int present, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwPresent", present));
            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ConvertPresent", parms);
        }

        /// <summary>
        /// 根据用户魅力排名(前10名)
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> GetUserInfoOrderByLoves()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT TOP 10 Accounts, NickName,GameID, LoveLiness, Present ")
                    .Append("FROM AccountsInfo ")
                    .Append("WHERE Nullity=0 ")
                    .Append(" ORDER By LoveLiness DESC,UserID ASC");
            return Database.ExecuteObjectList<UserInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 排行榜
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataSet GetLovesRanking(int num)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("SELECT TOP {0} Accounts,NickName,GameID,LoveLiness,Present ", num)
                    .Append("FROM AccountsInfo")
                    .Append(" WHERE Nullity=0 AND LoveLiness>0")
                    .Append(" ORDER By LoveLiness DESC,UserID ASC");
            return Database.ExecuteDataset(sqlQuery.ToString());
        }

        #endregion

        #region 奖牌兑换

        public Message UserConvertMedal(int userID, int medals, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwMedals", medals));
            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ConvertMedal", parms);
        }

        #endregion

        #region 电子密保卡

        /// <summary>
        /// 检测密保卡序列号是否存在
        /// </summary>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool PasswordIDIsEnable(string serialNumber)
        {
            string sqlQuery = string.Format("SELECT UserID FROM AccountsInfo(NOLOCK) WHERE PasswordID=@PasswordID");

            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("PasswordID", serialNumber));

            AccountsInfo user = Database.ExecuteObject<AccountsInfo>(sqlQuery, parms);
            if(user == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测用户是否绑定了密保卡
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool userIsBindPasswordCard(int userId)
        {
            string sqlQuery = string.Format("SELECT UserID FROM AccountsInfo(NOLOCK) WHERE PasswordID<>0 and userID=@UserID", userId);

            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("UserID", userId));

            AccountsInfo user = Database.ExecuteObject<AccountsInfo>(sqlQuery, parms);
            if(user == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新用户密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool UpdateUserPasswordCardID(int userId, int serialNumber)
        {
            string sqlQuery = string.Format("UPDATE AccountsInfo SET PasswordID=@PasswordID WHERE UserID=@UserID");
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("PasswordID", serialNumber));
            parms.Add(Database.MakeInParam("UserID", userId));

            int resutl = Database.ExecuteNonQuery(CommandType.Text, sqlQuery, parms.ToArray());
            if(resutl > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取消密保卡绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ClearUserPasswordCardID(int userId)
        {
            string sqlQuery = string.Format("UPDATE AccountsInfo SET PasswordID=0 WHERE UserID={0}", userId);
            int resutl = Database.ExecuteNonQuery(sqlQuery);
            if(resutl > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region 配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key">配置Key值</param>
        /// <returns>SystemStatusInfo实体</returns>
        public SystemStatusInfo GetSystemStatusInfo(string key)
        {
            string sqlQuery = "SELECT * FROM SystemStatusInfo WHERE StatusName=@StatusName";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("StatusName", key));
            return Database.ExecuteObject<SystemStatusInfo>(sqlQuery, parms);
        }

        /// <summary>
        /// 获取会员配置
        /// </summary>
        /// <param name="memberOrder"></param>
        /// <returns></returns>
        public MemberProperty GetMemberProperty(int memberOrder)
        {
            string sqlQuery = "SELECT * FROM MemberProperty WHERE MemberOrder=@MemberOrder";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("MemberOrder", memberOrder));
            return Database.ExecuteObject<MemberProperty>(sqlQuery, parms);
        }
        #endregion

        #region 签到
        /// <summary>
        /// 获取用户签到实体
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>签到实体</returns>
        public AccountsSignin GetAccountsSignin(int userId)
        {
            string sqlQuery = "SELECT * FROM AccountsSignin WHERE UserID=@UserID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userId));
            AccountsSignin model = Database.ExecuteObject<AccountsSignin>(sqlQuery, prams);
            return model;
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Message Signin(int userId, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userId));
            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_Signin", parms);
        }
        #endregion

        #region 代理商管理

        /// <summary>
        /// 获取代理商基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByUserID(int userID)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsAgent(NOLOCK) WHERE UserID= {0}", userID);
            AccountsAgent model = Database.ExecuteObject<AccountsAgent>(sqlQuery);
            if (model != null)
                return model;
            else
                return new AccountsAgent();
        }

        /// <summary>
        /// 获取代理商基本信息通过用户域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByDomain(string domain)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsAgent(NOLOCK) WHERE Domain= @Domain", domain);

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Domain", domain));
            AccountsAgent model = Database.ExecuteObject<AccountsAgent>(sqlQuery, prams);
            if (model != null)
                return model;
            else
                return new AccountsAgent();
        }

        /// <summary>
        /// 获取代理下级玩家数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentChildCount(int userID)
        {
            string sqlQuery = string.Format("SELECT COUNT(UserID) FROM AccountsInfo WHERE SpreaderID= {0}", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if (obj == null || obj.ToString().Length <= 0)
                return 0;
            return Convert.ToInt32(obj);
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
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", accountsFace.UserID));
            parms.Add(Database.MakeInParam("imgCustomFace", accountsFace.CustomFace));
            parms.Add(Database.MakeInParam("strClientIP", accountsFace.InsertAddr));
            parms.Add(Database.MakeInParam("strMachineID", accountsFace.InsertMachine));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
            return MessageHelper.GetMessageForObject<AccountsInfo>(Database, "NET_PW_InsertCustomFace", parms);
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
            PagerParameters pagerPrams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object GetObjectBySql(string sqlQuery)
        {
            return Database.ExecuteScalar(System.Data.CommandType.Text, sqlQuery);
        }

        #endregion

    }
}
