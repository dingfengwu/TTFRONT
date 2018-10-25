using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;

using Game.Entity;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Platform;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Utils.Cache;

namespace Game.Facade
{
    /// <summary>
    /// 为网站提供全局服务，如：用户Cookie读写等等
    /// </summary>
    public sealed class Fetch
    {
        #region Fields

        private static List<string> m_protectionQuestiongs = null;                  //密保问题 

  
        #endregion

        #region 构造方法

        private Fetch()
        {
        }

        /// <summary>
        /// 静态构造方法，用于初始化变量
        /// </summary>
        static Fetch()
        {
            GetProtectionQuestions();
        }

        #endregion

        #region 网站相关

        /// <summary>
        /// 获取验证码数值
        /// </summary>
        /// <returns></returns>
        public static string GetVerifyCode()
        {
            string vcode = string.Empty;
            vcode = WHCache.Default.Get<SessionCache>(AppConfig.VerifyCodeKey) as string;
            if(string.IsNullOrEmpty(vcode))
            {
                vcode = WHCache.Default.Get<CookiesCache>(AppConfig.VerifyCodeKey) as string;
            }
            return vcode;
        }

        /// <summary>
        /// 获取上传的图片URL
        /// </summary>
        /// <returns></returns>
        public static string GetUploadFileUrl(string fileUrl)
        {
            string imageDomain = string.Empty;
            object obj = WHCache.Default.Get<AspNetCache>(AppConfig.ImageSiteDomain);
            if(obj != null)
            {
                imageDomain = obj.ToString();
            }
            else
            {
                ConfigInfo ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.SiteConfig.ToString());
                if(ci != null)
                {
                    imageDomain = ci.Field3;
                }
                WHCache.Default.Save<AspNetCache>(AppConfig.ImageSiteDomain, imageDomain, 30);
            }

            fileUrl = fileUrl.ToLower().Replace("upload/", "");
            return imageDomain + fileUrl;
        }

        #endregion

        #region 用户相关

        /// <summary>
        /// 根据用户ID获取用户名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetAccountsByUserID(int userID)
        {
            UserInfo user = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userID);
            return user == null ? "" : user.Accounts;
        }

        /// <summary>
        /// 根据用户ID获取用户昵称
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetNickNameByUserID(int userID)
        {
            UserInfo user = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userID);
            return user == null ? "" : user.NickName;
        }

        /// <summary>
        /// 获取用户全局实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static UserInfo GetUserGlobalInfo(int userID)
        {
            UserInfo model = new UserInfo();
            Message msg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(userID, 0, "");
            if (msg.Success)
            {
                model = msg.EntityList[0] as UserInfo;
            }
            return model;
        }

        /// <summary>
        /// 根据经验获取等级排行
        /// </summary>
        /// <param name="experience"></param>
        /// <returns></returns>
        public static int GetGradeConfig(int experience)
        {
            DataSet dsGradeConfig = FacadeManage.aidePlatformFacade.GetGrowLevelConfigList();
            if (dsGradeConfig.Tables[0].Rows.Count > 0)
            {
                DataView dv = (from n in dsGradeConfig.Tables[0].AsEnumerable()
                               where n.Field<int>("Experience") < experience
                               orderby n.Field<int>("Experience") descending
                               select n).AsDataView();
                if (dv.Count > 0)
                    return Convert.ToInt32(dv[0]["LevelID"]);
                else
                    return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据用户ID获取GameID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int GetGameIDByUserID(int userID)
        {
            UserInfo user = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userID);
            return user == null ? 0 : user.GameID;
        }

        /// <summary>
        /// 获取ID号码描述，没分配 显示 “尚未分配”
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public static string GetGameID(int gameID)
        {
            if(gameID <= 0)
                return "尚未分配";

            return gameID.ToString();
        }

        /// <summary>
        /// 获取用户标识
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public static int GetUserID(string strUserID)
        {
            int dwUserID = TypeParse.StrToInt(CWHEncryptNet.XorCrevasse(strUserID), 0);
            return dwUserID;
        }

        /// <summary>
        /// 获取加密后的用户标识
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetStrUserID(int userID)
        {
            return CWHEncryptNet.XorEncrypt(userID.ToString());
        }

        /// <summary>
        /// 获取用户推广域名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetSpreaderUrl(int userID)
        {
            string sDomain = "";
            AccountsAgent model = FacadeManage.aideAccountsFacade.GetAccountAgentByUserID(userID);
            sDomain = model.Domain;
            if (sDomain == "")
            {
                //获取推广域名
                int gameID = FacadeManage.aideAccountsFacade.GetGameIDByUserID(userID);
                // 域名以.分组第二个元素值
                string element = HttpContext.Current.Request.Url.Authority.ToString().Split('.')[1];
                // 域名所有后缀
                Array list = AppConfig.domainSuffixList.Split('|');
                if (Array.IndexOf(list, element) != -1)
                    // 顶级域名但不带www
                    sDomain = gameID + "." + HttpContext.Current.Request.Url.Authority;
                else
                    sDomain = gameID + "." + HttpContext.Current.Request.Url.Authority.Substring(HttpContext.Current.Request.Url.Authority.IndexOf('.') + 1);
            }
            return sDomain;
        }
        #endregion

        #region 密保问题

        //密保问题(配置信息)
        private static void GetProtectionQuestions()
        {
            m_protectionQuestiongs = new List<string>();

            DataSet ds = new DataSet();
            ds.ReadXml(TextUtility.GetFullPath("/config/protection.xml"));
            DataRow[] drSet = ds.Tables["Item"].Select("Questions_ID=0");

            foreach(DataRow dr in drSet)
            {
                m_protectionQuestiongs.Add(dr[0].ToString());
            }
        }

        /// <summary>
        /// 密保问题
        /// </summary>
        public static List<string> ProtectionQuestiongs
        {
            get
            {
                return m_protectionQuestiongs;
            }
        }

        /// <summary>
        /// 获取当前会话中保持的申请密保用户键
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetSessionProtectionKey(int userID)
        {
            return string.Format("question_userID_{0}", userID);
        }

        /// <summary>
        /// 记忆老的密保回答
        /// </summary>
        /// <param name="protection"></param>
        public static void SetOldProtectionInfo(AccountsProtect protection)
        {
            if(protection == null)
                return;
            string sessionKey = string.Format("old_{0}", Fetch.GetSessionProtectionKey(protection.UserID));
            WHCache.Default.Save<SessionCache>(sessionKey, protection, 10);
        }

        /// <summary>
        /// 获取老的密保回答
        /// </summary>
        /// <returns></returns>
        public static AccountsProtect GetOldProtectionInfo(int userID)
        {
            string sessionKey = string.Format("old_{0}", Fetch.GetSessionProtectionKey(userID));
            return WHCache.Default.Get<SessionCache>(sessionKey) as AccountsProtect;
        }

        #endregion

        #region 用户Cookies

        /// <summary>
        /// 设置用户cookie
        /// </summary>
        /// <param name="userTicket"></param>
        public static void SetUserCookie(UserTicketInfo userTicket)
        {
            if(userTicket == null)
            {
                return;
            }

            string jsonText = userTicket.SerializeText();
            string ciphertext = Utils.AES.Encrypt(jsonText, AppConfig.UserLoginCacheEncryptKey);
            WHCache.Default.Save<CookiesCache>(AppConfig.UserLoginCacheKey, ciphertext, AppConfig.UserLoginTimeOut);
        }

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <returns></returns>
        public static UserTicketInfo GetUserCookie()
        {
            object obj = WHCache.Default.Get<CookiesCache>(AppConfig.UserLoginCacheKey);
            string plaintext = string.Empty;

            //// 大厅cookie处理
            //object pfCookieUserName = HttpContext.Current.Request.Cookies["Accounts"];
            //object pfCookieUserID = HttpContext.Current.Request.Cookies["UserID"];

            //if( obj == null )
            //{
            //    // 大厅Cookie存在
            //    if( pfCookieUserName != null && pfCookieUserID != null )
            //    {
            //        plaintext = PalaformWriteCookie();
            //    }
            //}
            //else
            //{
            //    // 如果网页与大厅cookie都存在，验证是否一致
            //    plaintext = obj.ToString();
            //    string tempStr = Utils.AES.Decrypt( plaintext, AppConfig.UserLoginCacheEncryptKey );
            //    if( !string.IsNullOrEmpty( tempStr ) && pfCookieUserName != null && pfCookieUserID != null )
            //    {
            //        UserTicketInfo tempModel = UserTicketInfo.DeserializeObject( tempStr );
            //        if( tempModel.UserID.ToString() != pfCookieUserID.ToString() || tempModel.Accounts != pfCookieUserName.ToString() )
            //        {
            //            WHCache.Default.Delete<CookiesCache>( AppConfig.UserLoginCacheKey );
            //            plaintext = PalaformWriteCookie();
            //        }
            //    }
            //}

            if(obj == null)
                return null;
            plaintext = obj.ToString();

            //验证cookie格式
            string jsonText = Utils.AES.Decrypt(plaintext, AppConfig.UserLoginCacheEncryptKey);
            if(TextUtility.EmptyTrimOrNull(jsonText))
            {
                return null;
            }

            return UserTicketInfo.DeserializeObject(jsonText);
        }

        /// <summary>
        /// 大厅登陆写入cookie
        /// </summary>
        public static string PalaformWriteCookie()
        {
            if(HttpContext.Current.Request.Cookies["Accounts"] != null && HttpContext.Current.Request.Cookies["Password"] != null)
            {
                string accounts = HttpContext.Current.Request.Cookies["Accounts"].Value.ToString();
                string password = HttpContext.Current.Request.Cookies["Password"].Value.ToString();
                password = password.Trim();
                accounts = accounts.Trim();
                UserInfo suInfo = new UserInfo(0, accounts, 0);
                suInfo.LastLogonIP = GameRequest.GetUserIP();
                Message umsg = FacadeManage.aideAccountsFacade.Logon(suInfo, true);
                if(umsg.Success)
                {
                    UserInfo ui = umsg.EntityList[0] as UserInfo;
                    ui.LogonPass = password.Trim();
                    Fetch.SetUserCookie(ui.ToUserTicketInfo());
                    object obj = WHCache.Default.Get<CookiesCache>(AppConfig.UserLoginCacheKey);
                    if(obj != null)
                    {
                        return obj.ToString();
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 删除用户cookie
        /// </summary>
        public static void DeleteUserCookie()
        {
            WHCache.Default.Delete<CookiesCache>(AppConfig.UserLoginCacheKey);
        }

        /// <summary>
        /// 用户是否在线 判断用户登录 cookie
        /// 在线 true 离线 false
        /// </summary>
        /// <returns></returns>
        public static bool IsUserOnline()
        {
            UserTicketInfo uti = Fetch.GetUserCookie();
            if(uti == null || uti.UserID <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 登录并重写 Cookie
        /// </summary>
        public static void ReWriteUserCookie()
        {
            if(Fetch.IsUserOnline())
            {
                UserTicketInfo uti = Fetch.GetUserCookie();
                Message msg = FacadeManage.aideAccountsFacade.Logon(new UserInfo(uti), false);
                if(msg.Success)
                {
                    UserInfo usInfo = msg.EntityList[0] as UserInfo;
                    UserTicketInfo utiServer = usInfo.ToUserTicketInfo();

                    Fetch.SetUserCookie(utiServer);
                }
            }
        }
        #endregion

        #region 系统信息

        /// <summary>
        /// 获取缓存的使用状况
        /// </summary>
        /// <returns></returns>
        public static string GetCacheCurrentRunStatus()
        {
            StringBuilder builderCacheName = new StringBuilder();
            StringBuilder builderText = new StringBuilder();
            IDictionaryEnumerator a = HttpRuntime.Cache.GetEnumerator();

            a.Reset();
            a.MoveNext();

            for(int i = 0; i < HttpRuntime.Cache.Count; i++)
            {
                builderCacheName.Append(a.Key);
                if(i < HttpRuntime.Cache.Count - 1)
                    builderCacheName.Append("&#10;&#13;");

                a.MoveNext();
            }

            //输出状态   
            builderText.AppendFormat("内存使用量：{0}KB &nbsp; ", (GC.GetTotalMemory(false) / 1024).ToString("#,#"));
            builderText.AppendFormat("共使用 <span title=\"{1}\">{0}</span> 个系统缓存对象", HttpRuntime.Cache.Count, builderCacheName.ToString());

            return builderText.ToString();
        }

        #endregion

        #region 其他方法
        /// <summary>
        /// 生成编号 如 找回密码编号
        /// </summary>
        /// <returns></returns>
        public static string GetForgetPwdNumber()
        {
            //构造编号(如:20101201102322159111111)
            StringBuffer tradeNoBuffer = new StringBuffer();
            tradeNoBuffer += TextUtility.GetDateTimeLongString();
            tradeNoBuffer += TextUtility.CreateRandom(8, 1, 0, 0, 0, "");
            return tradeNoBuffer.ToString();
        }

        /// <summary>
        /// 客户端终端类型
        /// </summary>
        /// <returns>1:终端为android手机 2：终端为苹果的ipad、iphone、ipod</returns>
        public static int GetTerminalType(HttpRequest request)
        {
            string userAgent = request.Headers["User-Agent"].ToLower();
            if(userAgent.Contains("android"))
            {
                return 1;
            }
            else if(userAgent.Contains("ipad") || userAgent.Contains("iphone"))
            {
                return 2;
            }
            return 0;
        }

        /// <summary>
        /// 获取给定日期距离1900-01-01的天数
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public static int GetDateID(DateTime DateTime)
        {
            TimeSpan ts1 = new TimeSpan(DateTime.Ticks);
            TimeSpan ts2 = new TimeSpan(Convert.ToDateTime("1900-01-01").Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }
        /// <summary>
        ///  返回指定天数所对应的日期
        /// </summary>
        /// <param name="dateID"></param>
        /// <returns></returns>
        public static string ShowDate(int dateID)
        {
            return Convert.ToDateTime("1900-01-01").AddDays(dateID).ToString("yyyy-MM-dd");
        }
        #endregion
    }
}
