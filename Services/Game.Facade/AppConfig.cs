using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Entity.Platform;
using Game.Utils;

namespace Game.Facade
{
    public class AppConfig
    {
        #region 枚举

        /// <summary>
        /// 站点配置KEY
        /// </summary>
        public enum SiteConfigKey
        {
            /// <summary>
            /// 联系方式配置
            /// </summary>
            ContactConfig,
            /// <summary>
            /// 大厅下载配置
            /// </summary>
            SiteConfig,
            /// <summary>
            /// 大厅整包配置
            /// </summary>
            GameFullPackageConfig,
            /// <summary>
            /// 大厅简包配置
            /// </summary>
            GameJanePackageConfig,
            /// <summary>
            /// 安卓大厅配置
            /// </summary>
            GameAndroidConfig,

            /// <summary>
            /// 苹果大厅配置
            /// </summary>
            GameIosConfig,

            /// <summary>
            /// 移动版大厅配置
            /// </summary>
            MobilePlatformVersion
            
        }

        /// <summary>
        /// 系统设置KEY
        /// </summary>
        public enum SystemConfigKey
        {
            /// <summary>
            /// 人民币与游戏豆（货币）的兑换率
            /// </summary>
            RateCurrency,
            /// <summary>
            /// 游戏豆（货币）与游戏币的兑换率
            /// </summary>
            RateGold,
            /// <summary>
            /// 元宝与游戏币的兑换率
            /// </summary>
            MedalExchangeRate,
            /// <summary>
            /// 魅力与游戏币的兑换率
            /// </summary>
            PresentExchangeRate,
            /// <summary>
            /// 移动版大厅版本
            /// </summary>
            MobilePlatformVersion,

            /// <summary>
            /// 支付服务开关
            /// </summary>
            PayConfig,

            /// <summary>
            /// 分享赠送金币
            /// </summary>
            SharePresent,

            /// <summary>
            /// 微信登录
            /// </summary>
            WxLogon
        }

        /// <summary>
        /// 兑换奖品需要输入信息
        /// </summary>
        public enum AwardNeedInfoType
        {
            真实姓名 = 1,
            手机号码 = 2,
            QQ号码 = 4,
            收货地址及邮编 = 8
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum AwardOrderStatus
        {
            处理中 = 0,
            已发货 = 1,
            已收货 = 2,
            申请退货 = 3,
            同意退货等待您发货 = 4,
            拒绝退货 = 5,
            退货成功且退款成功 = 6
        }

        /// <summary>
        /// 广告类型
        /// </summary>
        public enum AdsGameType
        {
            大厅登录框广告位 = 1,
            大厅注册框广告位 = 2,
            大厅关闭框广告位 = 3,
            大厅右下角广告位 = 4,
            移动版网站广告位 = 5,
            游戏右上角广告位 = 6
        }

        /// <summary>
        /// 独立页key
        /// </summary>
        public enum SinglePageKey
        {
            /// <summary>
            /// 新手帮助
            /// </summary>
            NewbieHelp,            
            /// <summary>
            /// 用户协议
            /// </summary>
            ServiceAgreement,

            /// <summary>
            /// 关于我们
            /// </summary>
            AboutUs,

            /// <summary>
            /// 联系我们
            /// </summary>
            ContactUs,

            /// <summary>
            /// 家长监控
            /// </summary>
            Monitor,

            /// <summary>
            /// 交易纠纷
            /// </summary>
            Dispute
        }

        /// <summary>
        /// 骏付通充值类型
        /// </summary>
        public enum JFTPayCardType
        {
            /// <summary>
            /// 骏网一卡通
            /// </summary>
            [EnumDescription("JUNNET")]
            骏网一卡通 = 101,
            /// <summary>
            /// 盛大卡
            /// </summary>
            [EnumDescription("SNDACARD")]
            盛大卡 = 102,
            /// <summary>
            /// 神州行
            /// </summary>
            [EnumDescription("SZX")]
            神州行 = 103,
            /// <summary>
            /// 征途卡
            /// </summary>
            [EnumDescription("ZHENGTU")]
            征途卡 = 104,
            /// <summary>
            /// Q币卡
            /// </summary>
            [EnumDescription("QQCARD")]
            Q币卡 = 105,
            /// <summary>
            /// 联通卡
            /// </summary>
            [EnumDescription("UNICOM")]
            联通卡 = 106,
            /// <summary>
            /// 久游卡
            /// </summary>
            [EnumDescription("JIUYOU")]
            久游卡 = 107,
            /// <summary>
            /// 易宝e卡通
            /// </summary>
            [EnumDescription("YPCARD")]
            易宝e卡通 = 108,
            /// <summary>
            /// 网易卡
            /// </summary>
            [EnumDescription("NETEASE")]
            网易卡 = 109,
            /// <summary>
            /// 完美卡
            /// </summary>
            [EnumDescription("WANMEI")]
            完美卡 = 110,
            /// <summary>
            /// 搜狐卡
            /// </summary>
            [EnumDescription("SOHU")]
            搜狐卡 = 111,
            /// <summary>
            /// 电信卡
            /// </summary>
            [EnumDescription("TELECOM")]
            电信卡 = 112,
            /// <summary>
            /// 纵游一卡通
            /// </summary>
            [EnumDescription("ZONGYOU")]
            纵游一卡通 = 113,
            /// <summary>
            /// 天下一卡通
            /// </summary>
            [EnumDescription("TIANXIA")]
            天下一卡通 = 114,
            /// <summary>
            /// 天宏一卡通
            /// </summary>
            [EnumDescription("TIANHONG")]
            天宏一卡通 = 115
        }
        #endregion

        #region 配置

        /// <summary>
        /// 同步登录MD5加密KEY值
        /// </summary>
        public static string SyncLoginKey
        {
            get
            {
                try
                {
                    string key = ApplicationSettings.Get("SyncLoginKey");
                    if(!string.IsNullOrEmpty(key))
                    {
                        return key;
                    }
                    return "RYSyncLoginKey";
                }
                catch
                {
                    return "RYSyncLoginKey";
                }
            }
        }

        /// <summary>
        /// 同步登录链接过期时间 单位毫秒
        /// </summary>
        public static int SyncUrlTimeOut
        {
            get
            {
                try
                {
                    string time = ApplicationSettings.Get("SyncUrlTimeOut");
                    if(!string.IsNullOrEmpty(time))
                    {
                        return Convert.ToInt32(time);
                    }
                    return 30;
                }
                catch
                {
                    return 30;
                }
            }
        }

        /// <summary>
        /// 用户登陆Cookies名
        /// </summary>
        public static string UserLoginCacheKey
        {
            get
            {
                try
                {
                    string key = ApplicationSettings.Get("UserLoginCacheKey");
                    if(!string.IsNullOrEmpty(key))
                    {
                        return key;
                    }
                    return "RYLoginKey";
                }
                catch
                {
                    return "RYLoginKey";
                }
            }
        }

        /// <summary>
        /// 用户登陆Cookies过期时间 单位分钟
        /// </summary>
        public static int UserLoginTimeOut
        {
            get
            {
                try
                {
                    string time = ApplicationSettings.Get("UserLoginCacheTimeOut");
                    if(!string.IsNullOrEmpty(time))
                    {
                        return Convert.ToInt32(time);
                    }
                    return 30;
                }
                catch
                {
                    return 30;
                }
            }
        }

        /// <summary>
        /// 用户登陆Cookies值加密的KEY值
        /// </summary>
        public static string UserLoginCacheEncryptKey
        {
            get
            {
                try
                {
                    string key = ApplicationSettings.Get("UserLoginCacheEncryptKey");
                    if(!string.IsNullOrEmpty(key))
                    {
                        return key;
                    }
                    return "RYLoginCacheEncryptValue";
                }
                catch
                {
                    return "RYLoginCacheEncryptValue";
                }
            }
        }

        /// <summary>
        /// 申诉加密KEY
        /// </summary>
        public static string ReportForgetPasswordKey
        {
            get
            {
                try
                {
                    string key = ApplicationSettings.Get("ReportForgetPasswordKey");
                    if(!string.IsNullOrEmpty(key))
                    {
                        return key;
                    }
                    return "ReportForgetPasswordKeyValue";
                }
                catch
                {
                    return "ReportForgetPasswordKeyValue";
                }
            }
        }

        /// <summary>
        /// 是否开启移动版下载模块
        /// </summary>
        public static byte IsShowMoblieDownload
        {
            get
            {
                try
                {
                    string key = ApplicationSettings.Get("IsShowMoblieDownload");
                    if(!string.IsNullOrEmpty(key))
                    {
                        return Convert.ToByte(key);
                    }
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }
        #endregion

        #region 常量

        /// <summary>
        /// 验证码Session的KEY值
        /// </summary>
        public const string VerifyCodeKey = "VerifyCodeKey";
        /// <summary>
        /// 站点图片域名配置缓存KEY值
        /// </summary>
        public const string ImageSiteDomain = "RY_6603_Image_Domain";
        /// <summary>
        /// 账号最小宽度
        /// </summary>
        public const int userNameMinLength = 6;
        /// <summary>
        /// 账号最大宽度
        /// </summary>
        public const int userNameMaxLength = 32;
        /// <summary>
        /// 昵称最小宽度
        /// </summary>
        public const int nickNameMinLength = 4;
        /// <summary>
        /// 昵称最大宽度
        /// </summary>
        public const int nickNameMaxLength = 31;
        /// <summary>
        /// 密码最小宽度
        /// </summary>
        public const int passwordMinLength = 6;
        /// <summary>
        /// 密码最大宽度
        /// </summary>
        public const int passwordMaxLength = 32;
        /// <summary>
        /// 地址最大宽度
        /// </summary>
        public const int addressMaxLength = 128;
        /// <summary>
        /// 常用备注最大宽度
        /// </summary>
        public const int remarkMaxLength = 200;
        /// <summary>
        /// 职业最大宽度
        /// </summary>
        public const int professionMaxLength = 128;
        /// <summary>
        /// 签名最大宽度
        /// </summary>
        public const int underWriteMaxLength = 63;
        /// <summary>
        /// 真实姓名最大宽度
        /// </summary>
        public const int realNameMaxLength = 16;
        /// <summary>
        /// 密码保护答案最小宽度
        /// </summary>
        public const int protectAnswerMinLength = 4;
        /// <summary>
        /// 密码保护答案最大宽度
        /// </summary>
        public const int protectAnswerMaxLength = 40;
        /// <summary>
        /// QQ最小宽度
        /// </summary>
        public const int qqMinLength = 4;
        /// <summary>
        /// QQ最大宽度
        /// </summary>
        public const int qqMaxLength = 20;
        /// <summary>
        /// 域名后缀列表
        /// </summary>
        public const string domainSuffixList = "com|cn|top|wang|net|org|hk|co|cc|me|pw|la|asia|biz|mobi|net|org|gov|name|info|hk|tm|tv|tel|us|website|host|press|tw|ren|中国|香港|公司|网络|商标|移动";
        #endregion
    }
}
