/*
 * 版本：4.0
 * 时间：2016/8/26
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
	/// 实体类 LotteryItem。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LotteryUserInfo  
	{
		#region 私有变量
		private int m_freeCount;		//免费次数
		private int m_chargeFee;		//付款金额
		private int m_alreadyCount;		//使用次数
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化LotteryItem
		/// </summary>
        public LotteryUserInfo()
		{
            m_freeCount = 0;
            m_chargeFee = 0;
            m_alreadyCount = 0;
		}

		#endregion

		#region 公共属性

		/// <summary>
        /// 免费次数
		/// </summary>
        public int FreeCount
		{
            get { return m_freeCount; }
            set { m_freeCount = value; }
		}

		/// <summary>
        /// 付款金额
		/// </summary>
        public int ChargeFee
		{
            get { return m_chargeFee; }
            set { m_chargeFee = value; }
		}

		/// <summary>
        /// 使用次数
		/// </summary>
        public int AlreadyCount
		{
            get { return m_alreadyCount; }
            set { m_alreadyCount = value; }
		}
		#endregion
	}
}
