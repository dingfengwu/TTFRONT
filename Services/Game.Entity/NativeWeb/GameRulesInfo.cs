/*
 * 版本：4.0
 * 时间：2015/4/15
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
	/// <summary>
	/// 实体类 GameRulesInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameRulesInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameRulesInfo" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 游戏名称
		/// </summary>
		public const string _KindName = "KindName" ;

		/// <summary>
		/// 缩略图地址
		/// </summary>
		public const string _ThumbnailUrl = "ThumbnailUrl" ;

		/// <summary>
		/// 游戏截图
		/// </summary>
		public const string _ImgRuleUrl = "ImgRuleUrl" ;

		/// <summary>
		/// 手机图片
		/// </summary>
		public const string _MobileImgUrl = "MobileImgUrl" ;

        /// <summary>
        /// 移动端大小
        /// </summary>
        public const string _MobileSize = "MobileSize";

        /// <summary>
        /// 移动端更新时间
        /// </summary>
        public const string _MobileDate = "MobileDate";

        /// <summary>
        /// 移动端版本号
        /// </summary>
        public const string _MobileVersion = "MobileVersion";

		/// <summary>
		/// 移动手机类型
		/// </summary>
		public const string _MobileGameType = "MobileGameType" ;

		/// <summary>
		/// 安卓下载地址
		/// </summary>
		public const string _AndroidDownloadUrl = "AndroidDownloadUrl" ;

		/// <summary>
		/// IOS版下载地址
		/// </summary>
		public const string _IOSDownloadUrl = "IOSDownloadUrl" ;

		/// <summary>
		/// 游戏介绍
		/// </summary>
		public const string _HelpIntro = "HelpIntro" ;

		/// <summary>
		/// 规则介绍
		/// </summary>
		public const string _HelpRule = "HelpRule" ;

		/// <summary>
		/// 等级介绍
		/// </summary>
		public const string _HelpGrade = "HelpGrade" ;

		/// <summary>
		/// 加入推荐
		/// </summary>
		public const string _JoinIntro = "JoinIntro" ;

		/// <summary>
		/// 冻结
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 新增日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 修改日期
		/// </summary>
		public const string _ModifyDate = "ModifyDate" ;
		#endregion

		#region 私有变量
		private int m_kindID;						//游戏标识
		private string m_kindName;					//游戏名称
		private string m_thumbnailUrl;				//缩略图地址
		private string m_imgRuleUrl;				//游戏截图
		private string m_mobileImgUrl;				//手机图片
        private string m_mobileSize;                //移动端大小
        private string m_mobileDate;                //移动端更新时间
        private string m_mobileVersion;             //移动端版本号
		private byte m_mobileGameType;				//移动手机类型
		private string m_androidDownloadUrl;		//安卓下载地址
		private string m_iOSDownloadUrl;			//IOS版下载地址
		private string m_helpIntro;					//游戏介绍
		private string m_helpRule;					//规则介绍
		private string m_helpGrade;					//等级介绍
		private byte m_joinIntro;					//加入推荐
		private byte m_nullity;						//冻结
		private DateTime m_collectDate;				//新增日期
		private DateTime m_modifyDate;				//修改日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameRulesInfo
		/// </summary>
		public GameRulesInfo()
		{
			m_kindID=0;
			m_kindName="";
			m_thumbnailUrl="";
			m_imgRuleUrl="";
			m_mobileImgUrl="";
            m_mobileSize = "";
            m_mobileDate = "";
            m_mobileVersion = "";
			m_mobileGameType=0;
			m_androidDownloadUrl="";
			m_iOSDownloadUrl="";
			m_helpIntro="";
			m_helpRule="";
			m_helpGrade="";
			m_joinIntro=0;
			m_nullity=0;
			m_collectDate=DateTime.Now;
			m_modifyDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 游戏标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 游戏名称
		/// </summary>
		public string KindName
		{
			get { return m_kindName; }
			set { m_kindName = value; }
		}

		/// <summary>
		/// 缩略图地址
		/// </summary>
		public string ThumbnailUrl
		{
			get { return m_thumbnailUrl; }
			set { m_thumbnailUrl = value; }
		}

		/// <summary>
		/// 游戏截图
		/// </summary>
		public string ImgRuleUrl
		{
			get { return m_imgRuleUrl; }
			set { m_imgRuleUrl = value; }
		}

		/// <summary>
		/// 手机图片
		/// </summary>
		public string MobileImgUrl
		{
			get { return m_mobileImgUrl; }
			set { m_mobileImgUrl = value; }
		}

        /// <summary>
        /// 手机游戏大小
        /// </summary>
        public string MobileSize
        {
            get { return m_mobileSize; }
            set { m_mobileSize = value; }
        }

        /// <summary>
        /// 手机游戏更新时间
        /// </summary>
        public string MobileDate
        {
            get { return m_mobileDate; }
            set { m_mobileDate = value; }
        }

        /// <summary>
        /// 手机游戏版本号
        /// </summary>
        public string MobileVersion
        {
            get { return m_mobileVersion; }
            set { m_mobileVersion = value; }
        }

		/// <summary>
		/// 移动手机类型
		/// </summary>
		public byte MobileGameType
		{
			get { return m_mobileGameType; }
			set { m_mobileGameType = value; }
		}

		/// <summary>
		/// 安卓下载地址
		/// </summary>
		public string AndroidDownloadUrl
		{
			get { return m_androidDownloadUrl; }
			set { m_androidDownloadUrl = value; }
		}

		/// <summary>
		/// IOS版下载地址
		/// </summary>
		public string IOSDownloadUrl
		{
			get { return m_iOSDownloadUrl; }
			set { m_iOSDownloadUrl = value; }
		}

		/// <summary>
		/// 游戏介绍
		/// </summary>
		public string HelpIntro
		{
			get { return m_helpIntro; }
			set { m_helpIntro = value; }
		}

		/// <summary>
		/// 规则介绍
		/// </summary>
		public string HelpRule
		{
			get { return m_helpRule; }
			set { m_helpRule = value; }
		}

		/// <summary>
		/// 等级介绍
		/// </summary>
		public string HelpGrade
		{
			get { return m_helpGrade; }
			set { m_helpGrade = value; }
		}

		/// <summary>
		/// 加入推荐
		/// </summary>
		public byte JoinIntro
		{
			get { return m_joinIntro; }
			set { m_joinIntro = value; }
		}

		/// <summary>
		/// 冻结
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 新增日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 修改日期
		/// </summary>
		public DateTime ModifyDate
		{
			get { return m_modifyDate; }
			set { m_modifyDate = value; }
		}
		#endregion
	}
}
