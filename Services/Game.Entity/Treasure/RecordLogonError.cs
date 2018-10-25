/*
 * 版本：4.0
 * 时间：2013-12-23
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
	/// 实体类 RecordLogonError。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordLogonError  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordLogonError" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ServerID = "ServerID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Score = "Score" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _InsureScore = "InsureScore" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LogonIP = "LogonIP" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LogonMachine = "LogonMachine" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//
		private int m_userID;					//
		private int m_kindID;					//
		private int m_serverID;					//
		private long m_score;					//
		private long m_insureScore;				//
		private string m_logonIP;				//
		private string m_logonMachine;			//
		private DateTime m_collectDate;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordLogonError
		/// </summary>
		public RecordLogonError()
		{
			m_recordID=0;
			m_userID=0;
			m_kindID=0;
			m_serverID=0;
			m_score=0;
			m_insureScore=0;
			m_logonIP="";
			m_logonMachine="";
			m_collectDate=DateTime.Now;
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
		/// 
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int ServerID
		{
			get { return m_serverID; }
			set { m_serverID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long Score
		{
			get { return m_score; }
			set { m_score = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long InsureScore
		{
			get { return m_insureScore; }
			set { m_insureScore = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string LogonIP
		{
			get { return m_logonIP; }
			set { m_logonIP = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string LogonMachine
		{
			get { return m_logonMachine; }
			set { m_logonMachine = value; }
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
