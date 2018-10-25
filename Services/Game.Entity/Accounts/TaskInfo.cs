/*
 * 版本：4.0
 * 时间：2014-1-17
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
	/// 实体类 TaskInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TaskInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "TaskInfo" ;

		/// <summary>
		/// 任务标识
		/// </summary>
		public const string _TaskID = "TaskID" ;

		/// <summary>
		/// 任务名称
		/// </summary>
		public const string _TaskName = "TaskName" ;

		/// <summary>
		/// 任务描述
		/// </summary>
		public const string _TaskDescription = "TaskDescription" ;

		/// <summary>
		/// 任务类型 1:总赢局 2:总局数 4:首胜 8:比赛任务
		/// </summary>
		public const string _TaskType = "TaskType" ;

		/// <summary>
		/// 可领取任务用户类型
		/// </summary>
		public const string _UserType = "UserType" ;

		/// <summary>
		/// 任务所属游戏标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 比赛任务 比赛ID
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 完成任务需要局数
		/// </summary>
		public const string _Innings = "Innings" ;

		/// <summary>
		/// 普通玩家奖励金币
		/// </summary>
		public const string _StandardAwardGold = "StandardAwardGold" ;

		/// <summary>
		/// 普通玩家奖励元宝
		/// </summary>
		public const string _StandardAwardMedal = "StandardAwardMedal" ;

		/// <summary>
		/// 会员奖励金币
		/// </summary>
		public const string _MemberAwardGold = "MemberAwardGold" ;

		/// <summary>
		/// 会员奖励元宝
		/// </summary>
		public const string _MemberAwardMedal = "MemberAwardMedal" ;

		/// <summary>
		/// 时间限制 单位:分钟
		/// </summary>
		public const string _TimeLimit = "TimeLimit" ;

		/// <summary>
		/// 录入日期
		/// </summary>
		public const string _InputDate = "InputDate" ;
		#endregion

		#region 私有变量
		private int m_taskID;						//任务标识
		private string m_taskName;					//任务名称
		private string m_taskDescription;			//任务描述
		private int m_taskType;						//任务类型 1:总赢局 2:总局数 4:首胜 8:比赛任务
		private byte m_userType;					//可领取任务用户类型
		private int m_kindID;						//任务所属游戏标识
		private int m_matchID;						//比赛任务 比赛ID
		private int m_innings;						//完成任务需要局数
		private int m_standardAwardGold;			//普通玩家奖励金币
		private int m_standardAwardMedal;			//普通玩家奖励元宝
		private int m_memberAwardGold;				//会员奖励金币
		private int m_memberAwardMedal;				//会员奖励元宝
		private int m_timeLimit;					//时间限制 单位:分钟
		private DateTime m_inputDate;				//录入日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化TaskInfo
		/// </summary>
		public TaskInfo()
		{
			m_taskID=0;
			m_taskName="";
			m_taskDescription="";
			m_taskType=0;
			m_userType=0;
			m_kindID=0;
			m_matchID=0;
			m_innings=0;
			m_standardAwardGold=0;
			m_standardAwardMedal=0;
			m_memberAwardGold=0;
			m_memberAwardMedal=0;
			m_timeLimit=0;
			m_inputDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 任务标识
		/// </summary>
		public int TaskID
		{
			get { return m_taskID; }
			set { m_taskID = value; }
		}

		/// <summary>
		/// 任务名称
		/// </summary>
		public string TaskName
		{
			get { return m_taskName; }
			set { m_taskName = value; }
		}

		/// <summary>
		/// 任务描述
		/// </summary>
		public string TaskDescription
		{
			get { return m_taskDescription; }
			set { m_taskDescription = value; }
		}

		/// <summary>
		/// 任务类型 1:总赢局 2:总局数 4:首胜 8:比赛任务
		/// </summary>
		public int TaskType
		{
			get { return m_taskType; }
			set { m_taskType = value; }
		}

		/// <summary>
		/// 可领取任务用户类型
		/// </summary>
		public byte UserType
		{
			get { return m_userType; }
			set { m_userType = value; }
		}

		/// <summary>
		/// 任务所属游戏标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 比赛任务 比赛ID
		/// </summary>
		public int MatchID
		{
			get { return m_matchID; }
			set { m_matchID = value; }
		}

		/// <summary>
		/// 完成任务需要局数
		/// </summary>
		public int Innings
		{
			get { return m_innings; }
			set { m_innings = value; }
		}

		/// <summary>
		/// 普通玩家奖励金币
		/// </summary>
		public int StandardAwardGold
		{
			get { return m_standardAwardGold; }
			set { m_standardAwardGold = value; }
		}

		/// <summary>
		/// 普通玩家奖励元宝
		/// </summary>
		public int StandardAwardMedal
		{
			get { return m_standardAwardMedal; }
			set { m_standardAwardMedal = value; }
		}

		/// <summary>
		/// 会员奖励金币
		/// </summary>
		public int MemberAwardGold
		{
			get { return m_memberAwardGold; }
			set { m_memberAwardGold = value; }
		}

		/// <summary>
		/// 会员奖励元宝
		/// </summary>
		public int MemberAwardMedal
		{
			get { return m_memberAwardMedal; }
			set { m_memberAwardMedal = value; }
		}

		/// <summary>
		/// 时间限制 单位:分钟
		/// </summary>
		public int TimeLimit
		{
			get { return m_timeLimit; }
			set { m_timeLimit = value; }
		}

		/// <summary>
		/// 录入日期
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}
		#endregion
	}
}
