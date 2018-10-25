/*
 * 版本：4.0
 * 时间：2016/8/31
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
	/// 实体类 LotteryConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LotteryConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "LotteryConfig" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 每日免费次数
		/// </summary>
		public const string _FreeCount = "FreeCount" ;

		/// <summary>
		/// 抽奖收费额度
		/// </summary>
		public const string _ChargeFee = "ChargeFee" ;

		/// <summary>
		/// 允许收费(0:不允许,1:允许)
		/// </summary>
		public const string _IsCharge = "IsCharge" ;
		#endregion

		#region 私有变量
		private int m_iD;				//
		private int m_freeCount;		//每日免费次数
		private int m_chargeFee;		//抽奖收费额度
		private byte m_isCharge;		//允许收费(0:不允许,1:允许)
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化LotteryConfig
		/// </summary>
		public LotteryConfig()
		{
			m_iD=0;
			m_freeCount=0;
			m_chargeFee=0;
			m_isCharge=0;
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
		/// 每日免费次数
		/// </summary>
		public int FreeCount
		{
			get { return m_freeCount; }
			set { m_freeCount = value; }
		}

		/// <summary>
		/// 抽奖收费额度
		/// </summary>
		public int ChargeFee
		{
			get { return m_chargeFee; }
			set { m_chargeFee = value; }
		}

		/// <summary>
		/// 允许收费(0:不允许,1:允许)
		/// </summary>
		public byte IsCharge
		{
			get { return m_isCharge; }
			set { m_isCharge = value; }
		}
		#endregion
	}
}
