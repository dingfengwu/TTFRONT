/*
 * 版本：4.0
 * 时间：2014-5-5
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
	/// 实体类 MatchImmediate。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MatchImmediate  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MatchImmediate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchNo = "MatchNo" ;

		/// <summary>
		/// 开赛人数
		/// </summary>
		public const string _StartUserCount = "StartUserCount" ;

		/// <summary>
		/// 一组比赛中最大机器人数目
		/// </summary>
		public const string _AndroidUserCount = "AndroidUserCount" ;

		/// <summary>
		/// 初始基数(即初始游戏底分)
		/// </summary>
		public const string _InitBase = "InitBase" ;

		/// <summary>
		/// 初始比赛分(为0时 表示使用当前金币为比赛分)
		/// </summary>
		public const string _InitScore = "InitScore" ;

		/// <summary>
		/// 最少进入金币
		/// </summary>
		public const string _MinEnterGold = "MinEnterGold" ;

		/// <summary>
		/// 游戏局数
		/// </summary>
		public const string _PlayCount = "PlayCount" ;

		/// <summary>
		/// 换桌局数(为0时 表示不换桌)
		/// </summary>
		public const string _SwitchTableCount = "SwitchTableCount" ;

		/// <summary>
		/// 能得到优先分配的等待时间(单位 秒)
		/// </summary>
		public const string _PrecedeTimer = "PrecedeTimer" ;
		#endregion

		#region 私有变量
		private int m_matchID;					//
		private short m_matchNo;				//
		private int m_startUserCount;			//开赛人数
		private int m_androidUserCount;			//一组比赛中最大机器人数目
		private int m_initBase;					//初始基数(即初始游戏底分)
		private int m_initScore;				//初始比赛分(为0时 表示使用当前金币为比赛分)
		private int m_minEnterGold;				//最少进入金币
		private byte m_playCount;				//游戏局数
		private byte m_switchTableCount;		//换桌局数(为0时 表示不换桌)
		private int m_precedeTimer;				//能得到优先分配的等待时间(单位 秒)
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MatchImmediate
		/// </summary>
		public MatchImmediate()
		{
			m_matchID=0;
			m_matchNo=0;
			m_startUserCount=0;
			m_androidUserCount=0;
			m_initBase=0;
			m_initScore=0;
			m_minEnterGold=0;
			m_playCount=0;
			m_switchTableCount=0;
			m_precedeTimer=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int MatchID
		{
			get { return m_matchID; }
			set { m_matchID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short MatchNo
		{
			get { return m_matchNo; }
			set { m_matchNo = value; }
		}

		/// <summary>
		/// 开赛人数
		/// </summary>
		public int StartUserCount
		{
			get { return m_startUserCount; }
			set { m_startUserCount = value; }
		}

		/// <summary>
		/// 一组比赛中最大机器人数目
		/// </summary>
		public int AndroidUserCount
		{
			get { return m_androidUserCount; }
			set { m_androidUserCount = value; }
		}

		/// <summary>
		/// 初始基数(即初始游戏底分)
		/// </summary>
		public int InitBase
		{
			get { return m_initBase; }
			set { m_initBase = value; }
		}

		/// <summary>
		/// 初始比赛分(为0时 表示使用当前金币为比赛分)
		/// </summary>
		public int InitScore
		{
			get { return m_initScore; }
			set { m_initScore = value; }
		}

		/// <summary>
		/// 最少进入金币
		/// </summary>
		public int MinEnterGold
		{
			get { return m_minEnterGold; }
			set { m_minEnterGold = value; }
		}

		/// <summary>
		/// 游戏局数
		/// </summary>
		public byte PlayCount
		{
			get { return m_playCount; }
			set { m_playCount = value; }
		}

		/// <summary>
		/// 换桌局数(为0时 表示不换桌)
		/// </summary>
		public byte SwitchTableCount
		{
			get { return m_switchTableCount; }
			set { m_switchTableCount = value; }
		}

		/// <summary>
		/// 能得到优先分配的等待时间(单位 秒)
		/// </summary>
		public int PrecedeTimer
		{
			get { return m_precedeTimer; }
			set { m_precedeTimer = value; }
		}
		#endregion
	}
}
