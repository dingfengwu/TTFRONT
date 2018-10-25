/*
 * 版本：4.0
 * 时间：2014-3-31
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
	/// <summary>
	/// 实体类 SigninConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SigninConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "SigninConfig" ;

		/// <summary>
		/// 签到标识[天]
		/// </summary>
		public const string _DayID = "DayID" ;

		/// <summary>
		/// 奖励金币
		/// </summary>
		public const string _RewardGold = "RewardGold" ;
		#endregion

		#region 私有变量
		private int m_dayID;				//签到标识[天]
		private long m_rewardGold;			//奖励金币
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化SigninConfig
		/// </summary>
		public SigninConfig()
		{
			m_dayID=0;
			m_rewardGold=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 签到标识[天]
		/// </summary>
		public int DayID
		{
			get { return m_dayID; }
			set { m_dayID = value; }
		}

		/// <summary>
		/// 奖励金币
		/// </summary>
		public long RewardGold
		{
			get { return m_rewardGold; }
			set { m_rewardGold = value; }
		}
		#endregion
	}
}
