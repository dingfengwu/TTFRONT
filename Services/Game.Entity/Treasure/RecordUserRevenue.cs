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
	/// 实体类 RecordUserRevenue。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordUserRevenue  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordUserRevenue" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 税收
		/// </summary>
		public const string _Revenue = "Revenue" ;

		/// <summary>
		/// 代理用户标识
		/// </summary>
        public const string _AgentUserID = "AgentUserID";

		/// <summary>
		/// 
		/// </summary>
		public const string _AgentScale = "AgentScale" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _AgentRevenue = "AgentRevenue" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CompanyScale = "CompanyScale" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CompanyRevenue = "CompanyRevenue" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//记录标识
		private int m_dateID;					//日期标识
		private int m_userID;					//用户标识
		private long m_revenue;					//税收
        private int m_agentUserID;				//代理标识
		private decimal m_agentScale;			//
		private long m_agentRevenue;			//
		private decimal m_companyScale;			//
		private long m_companyRevenue;			//
		private DateTime m_collectDate;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordUserRevenue
		/// </summary>
		public RecordUserRevenue()
		{
			m_recordID=0;
			m_dateID=0;
			m_userID=0;
			m_revenue=0;
            m_agentUserID = 0;
			m_agentScale=0;
			m_agentRevenue=0;
			m_companyScale=0;
			m_companyRevenue=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 记录标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
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
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 税收
		/// </summary>
		public long Revenue
		{
			get { return m_revenue; }
			set { m_revenue = value; }
		}

		/// <summary>
		/// 代理标识
		/// </summary>
        public int AgentUserID
		{
            get { return m_agentUserID; }
            set { m_agentUserID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal AgentScale
		{
			get { return m_agentScale; }
			set { m_agentScale = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long AgentRevenue
		{
			get { return m_agentRevenue; }
			set { m_agentRevenue = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal CompanyScale
		{
			get { return m_companyScale; }
			set { m_companyScale = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long CompanyRevenue
		{
			get { return m_companyRevenue; }
			set { m_companyRevenue = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
