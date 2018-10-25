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
	/// 实体类 RecordMachinePresent。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordMachinePresent  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordMachinePresent" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 机器码
		/// </summary>
		public const string _MachineID = "MachineID" ;

		/// <summary>
		/// 赠送金币
		/// </summary>
		public const string _PresentGold = "PresentGold" ;

		/// <summary>
		/// 赠送次数
		/// </summary>
		public const string _PresentCount = "PresentCount" ;

		/// <summary>
		/// 赠送ID串
		/// </summary>
		public const string _UserIDString = "UserIDString" ;

		/// <summary>
		/// 开始赠送时间
		/// </summary>
		public const string _FirstGrantDate = "FirstGrantDate" ;

		/// <summary>
		/// 最后赠送时间
		/// </summary>
		public const string _LastGrantDate = "LastGrantDate" ;
		#endregion

		#region 私有变量
		private int m_dateID;						//日期标识
		private string m_machineID;					//机器码
		private long m_presentGold;					//赠送金币
		private int m_presentCount;					//赠送次数
		private string m_userIDString;				//赠送ID串
		private DateTime m_firstGrantDate;			//开始赠送时间
		private DateTime m_lastGrantDate;			//最后赠送时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordMachinePresent
		/// </summary>
		public RecordMachinePresent()
		{
			m_dateID=0;
			m_machineID="";
			m_presentGold=0;
			m_presentCount=0;
			m_userIDString="";
			m_firstGrantDate=DateTime.Now;
			m_lastGrantDate=DateTime.Now;
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
		/// 机器码
		/// </summary>
		public string MachineID
		{
			get { return m_machineID; }
			set { m_machineID = value; }
		}

		/// <summary>
		/// 赠送金币
		/// </summary>
		public long PresentGold
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
		/// 赠送ID串
		/// </summary>
		public string UserIDString
		{
			get { return m_userIDString; }
			set { m_userIDString = value; }
		}

		/// <summary>
		/// 开始赠送时间
		/// </summary>
		public DateTime FirstGrantDate
		{
			get { return m_firstGrantDate; }
			set { m_firstGrantDate = value; }
		}

		/// <summary>
		/// 最后赠送时间
		/// </summary>
		public DateTime LastGrantDate
		{
			get { return m_lastGrantDate; }
			set { m_lastGrantDate = value; }
		}
		#endregion
	}
}
