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
	/// 实体类 GlobalLivcard。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GlobalLivcard  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GlobalLivcard" ;

		/// <summary>
		/// 实卡类型
		/// </summary>
		public const string _CardTypeID = "CardTypeID" ;

		/// <summary>
		/// 实卡名称
		/// </summary>
		public const string _CardName = "CardName" ;

		/// <summary>
		/// 实卡价格
		/// </summary>
		public const string _CardPrice = "CardPrice" ;

		/// <summary>
		/// 赠送平台币
		/// </summary>
		public const string _Currency = "Currency" ;

		/// <summary>
		/// 录入日期
		/// </summary>
		public const string _InputDate = "InputDate" ;
		#endregion

		#region 私有变量
		private int m_cardTypeID;			//实卡类型
		private string m_cardName;			//实卡名称
		private decimal m_cardPrice;		//实卡价格
		private decimal m_currency;			//赠送平台币
		private DateTime m_inputDate;		//录入日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GlobalLivcard
		/// </summary>
		public GlobalLivcard()
		{
			m_cardTypeID=0;
			m_cardName="";
			m_cardPrice=0;
			m_currency=0;
			m_inputDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 实卡类型
		/// </summary>
		public int CardTypeID
		{
			get { return m_cardTypeID; }
			set { m_cardTypeID = value; }
		}

		/// <summary>
		/// 实卡名称
		/// </summary>
		public string CardName
		{
			get { return m_cardName; }
			set { m_cardName = value; }
		}

		/// <summary>
		/// 实卡价格
		/// </summary>
		public decimal CardPrice
		{
			get { return m_cardPrice; }
			set { m_cardPrice = value; }
		}

		/// <summary>
		/// 赠送平台币
		/// </summary>
		public decimal Currency
		{
			get { return m_currency; }
			set { m_currency = value; }
		}

		/// <summary>
		/// 录入日期
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}
		#endregion
	}
}
