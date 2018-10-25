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
	/// 实体类 ReturnAppDetailInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ReturnAppDetailInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnAppDetailInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _DetailID = "DetailID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OrderID = "OrderID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _PayAmount = "PayAmount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Status = "Status" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Quantity = "quantity" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Product_id = "product_id" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Transaction_id = "transaction_id" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Purchase_date = "purchase_date" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Original_transaction_id = "original_transaction_id" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Original_purchase_date = "original_purchase_date" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _App_item_id = "app_item_id" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Version_external_identifier = "version_external_identifier" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Bid = "bid" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Bvrs = "bvrs" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_detailID;									//
		private int m_userID;									//
		private string m_orderID;								//
		private decimal m_payAmount;							//
		private int m_status;									//
		private int m_quantity;									//
		private string m_product_id;							//
		private string m_transaction_id;						//
		private string m_purchase_date;							//
		private string m_original_transaction_id;				//
		private string m_original_purchase_date;				//
		private string m_app_item_id;							//
		private string m_version_external_identifier;			//
		private string m_bid;									//
		private string m_bvrs;									//
		private DateTime m_collectDate;							//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnAppDetailInfo
		/// </summary>
		public ReturnAppDetailInfo()
		{
			m_detailID=0;
			m_userID=0;
			m_orderID="";
			m_payAmount=0;
			m_status=0;
			m_quantity=0;
			m_product_id="";
			m_transaction_id="";
			m_purchase_date="";
			m_original_transaction_id="";
			m_original_purchase_date="";
			m_app_item_id="";
			m_version_external_identifier="";
			m_bid="";
			m_bvrs="";
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int DetailID
		{
			get { return m_detailID; }
			set { m_detailID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string OrderID
		{
			get { return m_orderID; }
			set { m_orderID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal PayAmount
		{
			get { return m_payAmount; }
			set { m_payAmount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int Status
		{
			get { return m_status; }
			set { m_status = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int Quantity
		{
			get { return m_quantity; }
			set { m_quantity = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Product_id
		{
			get { return m_product_id; }
			set { m_product_id = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Transaction_id
		{
			get { return m_transaction_id; }
			set { m_transaction_id = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Purchase_date
		{
			get { return m_purchase_date; }
			set { m_purchase_date = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Original_transaction_id
		{
			get { return m_original_transaction_id; }
			set { m_original_transaction_id = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Original_purchase_date
		{
			get { return m_original_purchase_date; }
			set { m_original_purchase_date = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string App_item_id
		{
			get { return m_app_item_id; }
			set { m_app_item_id = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Version_external_identifier
		{
			get { return m_version_external_identifier; }
			set { m_version_external_identifier = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Bid
		{
			get { return m_bid; }
			set { m_bid = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Bvrs
		{
			get { return m_bvrs; }
			set { m_bvrs = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
