/*
 * 版本：4.0
 * 时间：2014-5-19
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
	/// 实体类 MatchLockTime。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MatchLockTime  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MatchLockTime" ;

		/// <summary>
		/// 比赛标识
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 比赛场次
		/// </summary>
		public const string _MatchNo = "MatchNo" ;

		/// <summary>
		/// 开始时间
		/// </summary>
		public const string _StartTime = "StartTime" ;

		/// <summary>
		/// 结束时间
		/// </summary>
		public const string _EndTime = "EndTime" ;

		/// <summary>
		/// 初始积分
		/// </summary>
		public const string _InitScore = "InitScore" ;

		/// <summary>
		/// 淘汰积分
		/// </summary>
		public const string _CullScore = "CullScore" ;

		/// <summary>
		/// 有效局数
		/// </summary>
		public const string _MinPlayCount = "MinPlayCount" ;
		#endregion

		#region 私有变量
		private int m_matchID;				//比赛标识
		private short m_matchNo;			//比赛场次
		private DateTime m_startTime;		//开始时间
		private DateTime m_endTime;			//结束时间
		private long m_initScore;			//初始积分
		private long m_cullScore;			//淘汰积分
		private int m_minPlayCount;			//有效局数
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MatchLockTime
		/// </summary>
		public MatchLockTime()
		{
			m_matchID=0;
			m_matchNo=0;
			m_startTime=DateTime.Now;
			m_endTime=DateTime.Now;
			m_initScore=0;
			m_cullScore=0;
			m_minPlayCount=0;
		}

		#endregion

		#region 公共属性

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
		public short MatchNo
		{
			get { return m_matchNo; }
			set { m_matchNo = value; }
		}

		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartTime
		{
			get { return m_startTime; }
			set { m_startTime = value; }
		}

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndTime
		{
			get { return m_endTime; }
			set { m_endTime = value; }
		}

		/// <summary>
		/// 初始积分
		/// </summary>
		public long InitScore
		{
			get { return m_initScore; }
			set { m_initScore = value; }
		}

		/// <summary>
		/// 淘汰积分
		/// </summary>
		public long CullScore
		{
			get { return m_cullScore; }
			set { m_cullScore = value; }
		}

		/// <summary>
		/// 有效局数
		/// </summary>
		public int MinPlayCount
		{
			get { return m_minPlayCount; }
			set { m_minPlayCount = value; }
		}
		#endregion
	}
}
