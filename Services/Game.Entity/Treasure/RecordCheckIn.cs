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
	/// 实体类 RecordCheckIn。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordCheckIn  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordCheckIn" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 赠送金币
		/// </summary>
		public const string _PresentGold = "PresentGold" ;

		/// <summary>
		/// 赠送次数
		/// </summary>
		public const string _PresentCount = "PresentCount" ;

		/// <summary>
		/// 连续次数
		/// </summary>
		public const string _LxCount = "LxCount" ;

		/// <summary>
		/// 连续获得金币数
		/// </summary>
		public const string _LxGold = "LxGold" ;

		/// <summary>
		/// 签到日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//
		private int m_userID;				//用户标识
		private int m_presentGold;			//赠送金币
		private int m_presentCount;			//赠送次数
		private int m_lxCount;				//连续次数
		private int m_lxGold;				//连续获得金币数
		private DateTime m_collectDate;		//签到日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordCheckIn
		/// </summary>
		public RecordCheckIn()
		{
			m_recordID=0;
			m_userID=0;
			m_presentGold=0;
			m_presentCount=0;
			m_lxCount=0;
			m_lxGold=0;
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
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 赠送金币
		/// </summary>
		public int PresentGold
		{
			get { return m_presentGold; }
			set { m_presentGold = value; }
		}

		/// <summary>
		/// 赠送次数
		/// </summary>
		public int PresentCount
		{
			get { return m_presentCount; }
			set { m_presentCount = value; }
		}

		/// <summary>
		/// 连续次数
		/// </summary>
		public int LxCount
		{
			get { return m_lxCount; }
			set { m_lxCount = value; }
		}

		/// <summary>
		/// 连续获得金币数
		/// </summary>
		public int LxGold
		{
			get { return m_lxGold; }
			set { m_lxGold = value; }
		}

		/// <summary>
		/// 签到日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
