/*
 * 版本：4.0
 * 时间：2015/10/12
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
	/// <summary>
	/// 实体类 AccountsAgent。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccountsAgent  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AccountsAgent" ;

		/// <summary>
		/// 代理标识
		/// </summary>
		public const string _AgentID = "AgentID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 代理姓名
		/// </summary>
		public const string _Compellation = "Compellation" ;

		/// <summary>
		/// 代理域名
		/// </summary>
		public const string _Domain = "Domain" ;

		/// <summary>
		/// 分成类型(1:充值分成，2:税收分成)
		/// </summary>
		public const string _AgentType = "AgentType" ;

		/// <summary>
		/// 分成比例
		/// </summary>
		public const string _AgentScale = "AgentScale" ;

		/// <summary>
		/// 日累计充值返现
		/// </summary>
        public const string _PayBackScore = "PayBackScore";

		/// <summary>
		/// 返现比例
		/// </summary>
        public const string _PayBackScale = "PayBackScale";

		/// <summary>
		/// 电话
		/// </summary>
		public const string _MobilePhone = "MobilePhone" ;

		/// <summary>
		/// 邮箱
		/// </summary>
		public const string _EMail = "EMail" ;

		/// <summary>
		/// 详细地址
		/// </summary>
		public const string _DwellingPlace = "DwellingPlace" ;

		/// <summary>
		/// 禁用标识
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 备注
		/// </summary>
		public const string _AgentNote = "AgentNote" ;

		/// <summary>
		/// 创建时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_agentID;					//代理标识
		private int m_userID;					//用户标识
		private string m_compellation;			//代理姓名
		private string m_domain;				//代理域名
		private int m_agentType;				//分成类型(1:充值分成，2:税收分成)
		private decimal m_agentScale;			//分成比例
        private long m_payBackScore;			//日累计充值返现
        private decimal m_payBackScale;			//返现比例
		private string m_mobilePhone;			//电话
		private string m_eMail;					//邮箱
		private string m_dwellingPlace;			//详细地址
		private byte m_nullity;					//禁用标识
		private string m_agentNote;				//备注
		private DateTime m_collectDate;			//创建时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AccountsAgent
		/// </summary>
		public AccountsAgent()
		{
			m_agentID=0;
			m_userID=0;
			m_compellation="";
			m_domain="";
			m_agentType=0;
			m_agentScale=0;
            m_payBackScore = 0;
            m_payBackScale = 0;
			m_mobilePhone="";
			m_eMail="";
			m_dwellingPlace="";
			m_nullity=0;
			m_agentNote="";
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 代理标识
		/// </summary>
		public int AgentID
		{
			get { return m_agentID; }
			set { m_agentID = value; }
		}

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 代理姓名
		/// </summary>
		public string Compellation
		{
			get { return m_compellation; }
			set { m_compellation = value; }
		}

		/// <summary>
		/// 代理域名
		/// </summary>
		public string Domain
		{
			get { return m_domain; }
			set { m_domain = value; }
		}

		/// <summary>
		/// 分成类型(1:充值分成，2:税收分成)
		/// </summary>
		public int AgentType
		{
			get { return m_agentType; }
			set { m_agentType = value; }
		}

		/// <summary>
		/// 分成比例
		/// </summary>
		public decimal AgentScale
		{
			get { return m_agentScale; }
			set { m_agentScale = value; }
		}

		/// <summary>
		/// 日累计充值返现
		/// </summary>
        public long PayBackScore
		{
			get { return m_payBackScore; }
            set { m_payBackScore = value; }
		}

		/// <summary>
		/// 返现比例
		/// </summary>
        public decimal PayBackScale
		{
			get { return m_payBackScale; }
            set { m_payBackScale = value; }
		}

		/// <summary>
		/// 电话
		/// </summary>
		public string MobilePhone
		{
			get { return m_mobilePhone; }
			set { m_mobilePhone = value; }
		}

		/// <summary>
		/// 邮箱
		/// </summary>
		public string EMail
		{
			get { return m_eMail; }
			set { m_eMail = value; }
		}

		/// <summary>
		/// 详细地址
		/// </summary>
		public string DwellingPlace
		{
			get { return m_dwellingPlace; }
			set { m_dwellingPlace = value; }
		}

		/// <summary>
		/// 禁用标识
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string AgentNote
		{
			get { return m_agentNote; }
			set { m_agentNote = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
