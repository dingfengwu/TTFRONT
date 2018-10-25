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
	public partial class LotteryItem  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "LotteryItem" ;

		/// <summary>
		/// 奖项索引
		/// </summary>
		public const string _ItemIndex = "ItemIndex" ;

		/// <summary>
		/// 奖项类型(0游戏币,1游戏豆)
		/// </summary>
		public const string _ItemType = "ItemType" ;

		/// <summary>
		/// 奖项额度
		/// </summary>
		public const string _ItemQuota = "ItemQuota" ;
		#endregion

		#region 私有变量
		private int m_itemIndex;		//奖项索引
		private int m_itemType;			//奖项类型(0游戏币,1游戏豆)
		private int m_itemQuota;		//奖项额度
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化LotteryItem
		/// </summary>
		public LotteryItem()
		{
			m_itemIndex=0;
			m_itemType=0;
			m_itemQuota=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 奖项索引
		/// </summary>
		public int ItemIndex
		{
			get { return m_itemIndex; }
			set { m_itemIndex = value; }
		}

		/// <summary>
		/// 奖项类型(0游戏币,1游戏豆)
		/// </summary>
		public int ItemType
		{
			get { return m_itemType; }
			set { m_itemType = value; }
		}

		/// <summary>
		/// 奖项额度
		/// </summary>
		public int ItemQuota
		{
			get { return m_itemQuota; }
			set { m_itemQuota = value; }
		}
		#endregion
	}
}
