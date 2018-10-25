/*
 * 版本：4.0
 * 时间：2016/2/1
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
	/// 实体类 UserGameInfo_Line。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserGameInfo_Line  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "UserGameInfo_Line" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 总局数
		/// </summary>
		public const string _LineGrandTotal = "LineGrandTotal" ;

		/// <summary>
		/// 单次最高
		/// </summary>
		public const string _LineWinMax = "LineWinMax" ;

		/// <summary>
		/// 修改日期
		/// </summary>
		public const string _LastModifyDate = "LastModifyDate" ;
		#endregion

		#region 私有变量
		private int m_dateID;					//日期标识
		private int m_kindID;					//游戏标识
		private int m_userID;					//用户标识
		private long m_lineGrandTotal;			//总局数
		private long m_lineWinMax;				//单次最高
		private DateTime m_lastModifyDate;		//修改日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化UserGameInfo_Line
		/// </summary>
		public UserGameInfo_Line()
		{
			m_dateID=0;
			m_kindID=0;
			m_userID=0;
			m_lineGrandTotal=0;
			m_lineWinMax=0;
			m_lastModifyDate=DateTime.Now;
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
		/// 游戏标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
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
		/// 总局数
		/// </summary>
		public long LineGrandTotal
		{
			get { return m_lineGrandTotal; }
			set { m_lineGrandTotal = value; }
		}

		/// <summary>
		/// 单次最高
		/// </summary>
		public long LineWinMax
		{
			get { return m_lineWinMax; }
			set { m_lineWinMax = value; }
		}

		/// <summary>
		/// 修改日期
		/// </summary>
		public DateTime LastModifyDate
		{
			get { return m_lastModifyDate; }
			set { m_lastModifyDate = value; }
		}
		#endregion
	}
}
