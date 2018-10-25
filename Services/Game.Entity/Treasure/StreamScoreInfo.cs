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
	/// 实体类 StreamScoreInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class StreamScoreInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "StreamScoreInfo" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _WinCount = "WinCount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LostCount = "LostCount" ;

		/// <summary>
		/// 税收
		/// </summary>
		public const string _Revenue = "Revenue" ;

		/// <summary>
		/// 游戏时长
		/// </summary>
		public const string _PlayTimeCount = "PlayTimeCount" ;

		/// <summary>
		/// 在线时长
		/// </summary>
		public const string _OnlineTimeCount = "OnlineTimeCount" ;

		/// <summary>
		/// 开始统计时间
		/// </summary>
		public const string _FirstCollectDate = "FirstCollectDate" ;

		/// <summary>
		/// 最后统计时间
		/// </summary>
		public const string _LastCollectDate = "LastCollectDate" ;
		#endregion

		#region 私有变量
		private int m_dateID;						//日期标识
		private int m_userID;						//用户标识
		private int m_winCount;						//
		private int m_lostCount;					//
		private decimal m_revenue;					//税收
		private int m_playTimeCount;				//游戏时长
		private int m_onlineTimeCount;				//在线时长
		private DateTime m_firstCollectDate;		//开始统计时间
		private DateTime m_lastCollectDate;			//最后统计时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化StreamScoreInfo
		/// </summary>
		public StreamScoreInfo()
		{
			m_dateID=0;
			m_userID=0;
			m_winCount=0;
			m_lostCount=0;
			m_revenue=0;
			m_playTimeCount=0;
			m_onlineTimeCount=0;
			m_firstCollectDate=DateTime.Now;
			m_lastCollectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

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
		/// 
		/// </summary>
		public int WinCount
		{
			get { return m_winCount; }
			set { m_winCount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int LostCount
		{
			get { return m_lostCount; }
			set { m_lostCount = value; }
		}

		/// <summary>
		/// 税收
		/// </summary>
		public decimal Revenue
		{
			get { return m_revenue; }
			set { m_revenue = value; }
		}

		/// <summary>
		/// 游戏时长
		/// </summary>
		public int PlayTimeCount
		{
			get { return m_playTimeCount; }
			set { m_playTimeCount = value; }
		}

		/// <summary>
		/// 在线时长
		/// </summary>
		public int OnlineTimeCount
		{
			get { return m_onlineTimeCount; }
			set { m_onlineTimeCount = value; }
		}

		/// <summary>
		/// 开始统计时间
		/// </summary>
		public DateTime FirstCollectDate
		{
			get { return m_firstCollectDate; }
			set { m_firstCollectDate = value; }
		}

		/// <summary>
		/// 最后统计时间
		/// </summary>
		public DateTime LastCollectDate
		{
			get { return m_lastCollectDate; }
			set { m_lastCollectDate = value; }
		}
		#endregion
	}
}
