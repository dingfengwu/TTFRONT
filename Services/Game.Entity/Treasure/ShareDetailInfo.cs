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
	/// 实体类 ShareDetailInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShareDetailInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ShareDetailInfo" ;

		/// <summary>
		/// 充值标识
		/// </summary>
		public const string _DetailID = "DetailID" ;

		/// <summary>
		/// 操作用户
		/// </summary>
		public const string _OperUserID = "OperUserID" ;

		/// <summary>
		/// 服务标识
		/// </summary>
		public const string _ShareID = "ShareID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 游戏ID
		/// </summary>
		public const string _GameID = "GameID" ;

		/// <summary>
		/// 用户名
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 实卡类型
		/// </summary>
		public const string _CardTypeID = "CardTypeID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _SerialID = "SerialID" ;

		/// <summary>
		/// 订单号码
		/// </summary>
		public const string _OrderID = "OrderID" ;

		/// <summary>
		/// 订单金额
		/// </summary>
		public const string _OrderAmount = "OrderAmount" ;

		/// <summary>
		/// 折扣比例
		/// </summary>
		public const string _DiscountScale = "DiscountScale" ;

		/// <summary>
		/// 实付金额
		/// </summary>
		public const string _PayAmount = "PayAmount" ;

		/// <summary>
		/// 充值货币
		/// </summary>
		public const string _Currency = "Currency" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _BeforeCurrency = "BeforeCurrency" ;

		/// <summary>
		/// 充值地址
		/// </summary>
		public const string _IPAddress = "IPAddress" ;

		/// <summary>
		/// 充值日期
		/// </summary>
		public const string _ApplyDate = "ApplyDate" ;
		#endregion

		#region 私有变量
		private int m_detailID;						//充值标识
		private int m_operUserID;					//操作用户
		private int m_shareID;						//服务标识
		private int m_userID;						//用户标识
		private int m_gameID;						//游戏ID
		private string m_accounts;					//用户名
		private int m_cardTypeID;					//实卡类型
		private string m_serialID;					//
		private string m_orderID;					//订单号码
		private decimal m_orderAmount;				//订单金额
		private decimal m_discountScale;			//折扣比例
		private decimal m_payAmount;				//实付金额
		private decimal m_currency;					//充值货币
		private decimal m_beforeCurrency;			//
		private string m_iPAddress;					//充值地址
		private DateTime m_applyDate;				//充值日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ShareDetailInfo
		/// </summary>
		public ShareDetailInfo()
		{
			m_detailID=0;
			m_operUserID=0;
			m_shareID=0;
			m_userID=0;
			m_gameID=0;
			m_accounts="";
			m_cardTypeID=0;
			m_serialID="";
			m_orderID="";
			m_orderAmount=0;
			m_discountScale=0;
			m_payAmount=0;
			m_currency=0;
			m_beforeCurrency=0;
			m_iPAddress="";
			m_applyDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 充值标识
		/// </summary>
		public int DetailID
		{
			get { return m_detailID; }
			set { m_detailID = value; }
		}

		/// <summary>
		/// 操作用户
		/// </summary>
		public int OperUserID
		{
			get { return m_operUserID; }
			set { m_operUserID = value; }
		}

		/// <summary>
		/// 服务标识
		/// </summary>
		public int ShareID
		{
			get { return m_shareID; }
			set { m_shareID = value; }
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
		/// 游戏ID
		/// </summary>
		public int GameID
		{
			get { return m_gameID; }
			set { m_gameID = value; }
		}

		/// <summary>
		/// 用户名
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 实卡类型
		/// </summary>
		public int CardTypeID
		{
			get { return m_cardTypeID; }
			set { m_cardTypeID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string SerialID
		{
			get { return m_serialID; }
			set { m_serialID = value; }
		}

		/// <summary>
		/// 订单号码
		/// </summary>
		public string OrderID
		{
			get { return m_orderID; }
			set { m_orderID = value; }
		}

		/// <summary>
		/// 订单金额
		/// </summary>
		public decimal OrderAmount
		{
			get { return m_orderAmount; }
			set { m_orderAmount = value; }
		}

		/// <summary>
		/// 折扣比例
		/// </summary>
		public decimal DiscountScale
		{
			get { return m_discountScale; }
			set { m_discountScale = value; }
		}

		/// <summary>
		/// 实付金额
		/// </summary>
		public decimal PayAmount
		{
			get { return m_payAmount; }
			set { m_payAmount = value; }
		}

		/// <summary>
		/// 充值货币
		/// </summary>
		public decimal Currency
		{
			get { return m_currency; }
			set { m_currency = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal BeforeCurrency
		{
			get { return m_beforeCurrency; }
			set { m_beforeCurrency = value; }
		}

		/// <summary>
		/// 充值地址
		/// </summary>
		public string IPAddress
		{
			get { return m_iPAddress; }
			set { m_iPAddress = value; }
		}

		/// <summary>
		/// 充值日期
		/// </summary>
		public DateTime ApplyDate
		{
			get { return m_applyDate; }
			set { m_applyDate = value; }
		}
		#endregion
	}
}
