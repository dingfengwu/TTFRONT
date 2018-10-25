/*
 * 版本：4.0
 * 时间：2015/10/15
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
	/// <summary>
	/// 实体类 RecordAgentInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordAgentInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordAgentInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RecordID = "RecordID" ;

        /// <summary>
        /// 日期标识
        /// </summary>
        public const string _DateID = "DateID";

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 分成比例
		/// </summary>
		public const string _AgentScale = "AgentScale" ;

        /// <summary>
        /// 返现比例
        /// </summary>
        public const string _PayBackScale = "PayBackScale";

		/// <summary>
		/// 类型(1:充值,2:返现,3:结算)
		/// </summary>
		public const string _TypeID = "TypeID" ;

		/// <summary>
		/// 充值金币
		/// </summary>
		public const string _PayScore = "PayScore" ;

		/// <summary>
		/// 变化金币
		/// </summary>
		public const string _Score = "Score" ;

		/// <summary>
		/// 下级用户标识
		/// </summary>
		public const string _ChildrenID = "ChildrenID" ;

		/// <summary>
		/// 银行金币
		/// </summary>
		public const string _InsureScore = "InsureScore" ;

		/// <summary>
		/// 时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 地址
		/// </summary>
		public const string _CollectIP = "CollectIP" ;

		/// <summary>
		/// 备注
		/// </summary>
		public const string _CollectNote = "CollectNote" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//
		private int m_userID;					//
        private int m_dateID;
		private decimal m_agentScale;			//分成比例
        private decimal m_payBackScale;         //返现比例
		private int m_typeID;					//类型(1:充值,2:返现,3:结算)
		private long m_payScore;				//充值金币
		private long m_score;					//变化金币
		private int m_childrenID;				//下级用户标识
		private string m_insureScore;			//银行金币
		private DateTime m_collectDate;			//时间
		private string m_collectIP;				//地址
		private string m_collectNote;			//备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordAgentInfo
		/// </summary>
		public RecordAgentInfo()
		{
			m_recordID=0;
			m_userID=0;
            m_dateID = 0;
			m_agentScale=0;
            m_payBackScale = 0;
			m_typeID=0;
			m_payScore=0;
			m_score=0;
			m_childrenID=0;
			m_insureScore="";
			m_collectDate=DateTime.Now;
			m_collectIP="";
			m_collectNote="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

        /// <summary>
        /// 日期标识
        /// </summary>
        public int DateID
        {
            get { return m_dateID; }
            set { m_dateID = value; }
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
        /// 返现比例
        /// </summary>
        public decimal PayBackScale
        {
            get { return m_payBackScale; }
            set { m_payBackScale = value; }
        }

		/// <summary>
		/// 类型(1:充值,2:返现,3:结算)
		/// </summary>
		public int TypeID
		{
			get { return m_typeID; }
			set { m_typeID = value; }
		}

		/// <summary>
		/// 充值金币
		/// </summary>
		public long PayScore
		{
			get { return m_payScore; }
			set { m_payScore = value; }
		}

		/// <summary>
		/// 变化金币
		/// </summary>
		public long Score
		{
			get { return m_score; }
			set { m_score = value; }
		}

		/// <summary>
		/// 下级用户标识
		/// </summary>
		public int ChildrenID
		{
			get { return m_childrenID; }
			set { m_childrenID = value; }
		}

		/// <summary>
		/// 银行金币
		/// </summary>
		public string InsureScore
		{
			get { return m_insureScore; }
			set { m_insureScore = value; }
		}

		/// <summary>
		/// 时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 地址
		/// </summary>
		public string CollectIP
		{
			get { return m_collectIP; }
			set { m_collectIP = value; }
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string CollectNote
		{
			get { return m_collectNote; }
			set { m_collectNote = value; }
		}
		#endregion
	}
}
