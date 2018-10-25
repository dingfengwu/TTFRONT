/*
 * 版本：4.0
 * 时间：2016/5/19
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.GameMatch
{
	/// <summary>
	/// 实体类 StreamMatchHistory。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class StreamMatchHistory  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "StreamMatchHistory" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 比赛标识
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 比赛场次
		/// </summary>
		public const string _MatchNo = "MatchNo" ;

		/// <summary>
		/// 比赛类型
		/// </summary>
		public const string _MatchType = "MatchType" ;

		/// <summary>
		/// 房间ID
		/// </summary>
		public const string _ServerID = "ServerID" ;

		/// <summary>
		/// 比赛名次
		/// </summary>
		public const string _RankID = "RankID" ;

		/// <summary>
		/// 比赛得分
		/// </summary>
		public const string _MatchScore = "MatchScore" ;

		/// <summary>
		/// 用户权限 如：有进入下一论的权限
		/// </summary>
		public const string _UserRight = "UserRight" ;

		/// <summary>
		/// 奖励金币
		/// </summary>
		public const string _RewardGold = "RewardGold" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RewardIngot = "RewardIngot" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RewardExperience = "RewardExperience" ;

		/// <summary>
		/// 赢的局数
		/// </summary>
		public const string _WinCount = "WinCount" ;

		/// <summary>
		/// 输的局数
		/// </summary>
		public const string _LostCount = "LostCount" ;

		/// <summary>
		/// 平的局数
		/// </summary>
		public const string _DrawCount = "DrawCount" ;

		/// <summary>
		/// 逃跑局数
		/// </summary>
		public const string _FleeCount = "FleeCount" ;

		/// <summary>
		/// 比赛第一局开始时间
		/// </summary>
		public const string _MatchStartTime = "MatchStartTime" ;

		/// <summary>
		/// 比赛最后一句结束时间
		/// </summary>
		public const string _MatchEndTime = "MatchEndTime" ;

		/// <summary>
		/// 游戏时长 单位:秒
		/// </summary>
		public const string _PlayTimeCount = "PlayTimeCount" ;

		/// <summary>
		/// 在线时长 单位:秒
		/// </summary>
		public const string _OnlineTime = "OnlineTime" ;

		/// <summary>
		/// 机器码
		/// </summary>
		public const string _Machine = "Machine" ;

		/// <summary>
		/// 连接地址
		/// </summary>
		public const string _ClientIP = "ClientIP" ;

		/// <summary>
		/// 录入时间
		/// </summary>
		public const string _RecordDate = "RecordDate" ;
		#endregion

		#region 私有变量
		private int m_iD;						//
		private int m_userID;					//用户标识
		private int m_matchID;					//比赛标识
		private long m_matchNo;					//比赛场次
		private byte m_matchType;				//比赛类型
		private int m_serverID;					//房间ID
		private short m_rankID;					//比赛名次
		private int m_matchScore;				//比赛得分
		private int m_userRight;				//用户权限 如：有进入下一论的权限
		private long m_rewardGold;				//奖励金币
		private long m_rewardIngot;				//
		private long m_rewardExperience;		//
		private int m_winCount;					//赢的局数
		private int m_lostCount;				//输的局数
		private int m_drawCount;				//平的局数
		private int m_fleeCount;				//逃跑局数
		private DateTime m_matchStartTime;		//比赛第一局开始时间
		private DateTime m_matchEndTime;		//比赛最后一句结束时间
		private int m_playTimeCount;			//游戏时长 单位:秒
		private int m_onlineTime;				//在线时长 单位:秒
		private string m_machine;				//机器码
		private string m_clientIP;				//连接地址
		private DateTime m_recordDate;			//录入时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化StreamMatchHistory
		/// </summary>
		public StreamMatchHistory()
		{
			m_iD=0;
			m_userID=0;
			m_matchID=0;
			m_matchNo=0;
			m_matchType=0;
			m_serverID=0;
			m_rankID=0;
			m_matchScore=0;
			m_userRight=0;
			m_rewardGold=0;
			m_rewardIngot=0;
			m_rewardExperience=0;
			m_winCount=0;
			m_lostCount=0;
			m_drawCount=0;
			m_fleeCount=0;
			m_matchStartTime=DateTime.Now;
			m_matchEndTime=DateTime.Now;
			m_playTimeCount=0;
			m_onlineTime=0;
			m_machine="";
			m_clientIP="";
			m_recordDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
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
		/// 比赛标识
		/// </summary>
		public int MatchID
		{
			get { return m_matchID; }
			set { m_matchID = value; }
		}

		/// <summary>
		/// 比赛场次
		/// </summary>
		public long MatchNo
		{
			get { return m_matchNo; }
			set { m_matchNo = value; }
		}

		/// <summary>
		/// 比赛类型
		/// </summary>
		public byte MatchType
		{
			get { return m_matchType; }
			set { m_matchType = value; }
		}

		/// <summary>
		/// 房间ID
		/// </summary>
		public int ServerID
		{
			get { return m_serverID; }
			set { m_serverID = value; }
		}

		/// <summary>
		/// 比赛名次
		/// </summary>
		public short RankID
		{
			get { return m_rankID; }
			set { m_rankID = value; }
		}

		/// <summary>
		/// 比赛得分
		/// </summary>
		public int MatchScore
		{
			get { return m_matchScore; }
			set { m_matchScore = value; }
		}

		/// <summary>
		/// 用户权限 如：有进入下一论的权限
		/// </summary>
		public int UserRight
		{
			get { return m_userRight; }
			set { m_userRight = value; }
		}

		/// <summary>
		/// 奖励金币
		/// </summary>
		public long RewardGold
		{
			get { return m_rewardGold; }
			set { m_rewardGold = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long RewardIngot
		{
			get { return m_rewardIngot; }
			set { m_rewardIngot = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long RewardExperience
		{
			get { return m_rewardExperience; }
			set { m_rewardExperience = value; }
		}

		/// <summary>
		/// 赢的局数
		/// </summary>
		public int WinCount
		{
			get { return m_winCount; }
			set { m_winCount = value; }
		}

		/// <summary>
		/// 输的局数
		/// </summary>
		public int LostCount
		{
			get { return m_lostCount; }
			set { m_lostCount = value; }
		}

		/// <summary>
		/// 平的局数
		/// </summary>
		public int DrawCount
		{
			get { return m_drawCount; }
			set { m_drawCount = value; }
		}

		/// <summary>
		/// 逃跑局数
		/// </summary>
		public int FleeCount
		{
			get { return m_fleeCount; }
			set { m_fleeCount = value; }
		}

		/// <summary>
		/// 比赛第一局开始时间
		/// </summary>
		public DateTime MatchStartTime
		{
			get { return m_matchStartTime; }
			set { m_matchStartTime = value; }
		}

		/// <summary>
		/// 比赛最后一句结束时间
		/// </summary>
		public DateTime MatchEndTime
		{
			get { return m_matchEndTime; }
			set { m_matchEndTime = value; }
		}

		/// <summary>
		/// 游戏时长 单位:秒
		/// </summary>
		public int PlayTimeCount
		{
			get { return m_playTimeCount; }
			set { m_playTimeCount = value; }
		}

		/// <summary>
		/// 在线时长 单位:秒
		/// </summary>
		public int OnlineTime
		{
			get { return m_onlineTime; }
			set { m_onlineTime = value; }
		}

		/// <summary>
		/// 机器码
		/// </summary>
		public string Machine
		{
			get { return m_machine; }
			set { m_machine = value; }
		}

		/// <summary>
		/// 连接地址
		/// </summary>
		public string ClientIP
		{
			get { return m_clientIP; }
			set { m_clientIP = value; }
		}

		/// <summary>
		/// 录入时间
		/// </summary>
		public DateTime RecordDate
		{
			get { return m_recordDate; }
			set { m_recordDate = value; }
		}
		#endregion
	}
}
