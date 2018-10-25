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
	/// 实体类 UserCurrencyInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserCurrencyInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "UserCurrencyInfo" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 用户货币
		/// </summary>
		public const string _Currency = "Currency" ;
		#endregion

		#region 私有变量
		private int m_userID;				//用户标识
		private decimal m_currency;			//用户货币
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化UserCurrencyInfo
		/// </summary>
		public UserCurrencyInfo()
		{
			m_userID=0;
			m_currency=0;
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
		/// 用户货币
		/// </summary>
		public decimal Currency
		{
			get { return m_currency; }
			set { m_currency = value; }
		}
		#endregion
	}
}
