/*
 * 版本：4.0
 * 时间：2014-3-28
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
	/// 实体类 AccountsSignin。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccountsSignin  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AccountsSignin" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _StartDateTime = "StartDateTime" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LastDateTime = "LastDateTime" ;

		/// <summary>
		/// 连续签到天数
		/// </summary>
		public const string _SeriesDate = "SeriesDate" ;
		#endregion

		#region 私有变量
		private int m_userID;						//
		private DateTime m_startDateTime;			//
		private DateTime m_lastDateTime;			//
		private short m_seriesDate;					//连续签到天数
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AccountsSignin
		/// </summary>
		public AccountsSignin()
		{
			m_userID=0;
			m_startDateTime=DateTime.Now;
			m_lastDateTime=DateTime.Now;
			m_seriesDate=0;
		}

		#endregion

		#region 公共属性

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
		public DateTime StartDateTime
		{
			get { return m_startDateTime; }
			set { m_startDateTime = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime LastDateTime
		{
			get { return m_lastDateTime; }
			set { m_lastDateTime = value; }
		}

		/// <summary>
		/// 连续签到天数
		/// </summary>
		public short SeriesDate
		{
			get { return m_seriesDate; }
			set { m_seriesDate = value; }
		}
		#endregion
	}
}
