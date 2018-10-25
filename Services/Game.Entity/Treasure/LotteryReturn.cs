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
	public partial class LotteryReturn  
	{
		#region 私有变量
		private int m_wined;		    //输赢标识
		private int m_itemIndex;		//奖品索引
		private int m_itemType;		    //奖品类型
		private int m_itemQuota;		//中奖数量
        private Int64 m_score;			//游戏币
        private decimal m_currency;		//游戏豆
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化LotteryItem
		/// </summary>
        public LotteryReturn()
		{
            m_wined = 0;
            m_itemIndex = 0;
            m_itemType = 0;
            m_itemQuota = 0;
            m_score = 0;
            m_currency = 0;
		}

		#endregion

		#region 公共属性

		/// <summary>
        /// 输赢标识
		/// </summary>
        public int Wined
		{
            get { return m_wined; }
            set { m_wined = value; }
		}

		/// <summary>
        /// 奖品索引
		/// </summary>
        public int ItemIndex
		{
            get { return m_itemIndex; }
            set { m_itemIndex = value; }
		}

		/// <summary>
        /// 奖品类型
		/// </summary>
        public int ItemType
		{
            get { return m_itemType; }
            set { m_itemType = value; }
		}

		/// <summary>
        /// 中奖数量
		/// </summary>
        public int ItemQuota
		{
            get { return m_itemQuota; }
            set { m_itemQuota = value; }
		}

        /// <summary>
        /// 游戏币
        /// </summary>
        public Int64 Score
        {
            get { return m_score; }
            set { m_score = value; }
        }

        /// <summary>
        /// 游戏豆
        /// </summary>
        public decimal Currency
        {
            get { return m_currency; }
            set { m_currency = value; }
        }
		#endregion
	}
}
