/*
 * 版本：4.0
 * 时间：2016/1/7
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
	/// 实体类 UserGameData。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserGameInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "UserGameData" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 游戏数据（Index:Value;）
		/// </summary>
		public const string _UserGameData = "UserGameData" ;

		/// <summary>
		/// 总局数
		/// </summary>
		public const string _LineGrandTotal = "LineGrandTotal" ;

		/// <summary>
		/// 最大赢值
		/// </summary>
		public const string _LineWinMax = "LineWinMax" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LastModifyDate = "LastModifyDate" ;
		#endregion

		#region 私有变量
		private int m_userID;					//用户标识
		private int m_kindID;					//游戏标识
		private string m_userGameData;			//游戏数据（Index:Value;）
		private int m_lineGrandTotal;			//总局数
		private int m_lineWinMax;				//最大赢值
		private DateTime m_lastModifyDate;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化UserGameData
		/// </summary>
        public UserGameInfo()
		{
			m_userID=0;
			m_kindID=0;
			m_userGameData="";
			m_lineGrandTotal=0;
			m_lineWinMax=0;
			m_lastModifyDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
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
		/// 游戏数据（Index:Value;）
		/// </summary>
		public string UserGameData
		{
			get { return m_userGameData; }
			set { m_userGameData = value; }
		}

		/// <summary>
		/// 总局数
		/// </summary>
		public int LineGrandTotal
		{
			get { return m_lineGrandTotal; }
			set { m_lineGrandTotal = value; }
		}

		/// <summary>
		/// 最大赢值
		/// </summary>
		public int LineWinMax
		{
			get { return m_lineWinMax; }
			set { m_lineWinMax = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime LastModifyDate
		{
			get { return m_lastModifyDate; }
			set { m_lastModifyDate = value; }
		}
		#endregion
	}
}
