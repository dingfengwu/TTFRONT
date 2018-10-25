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
	/// 实体类 Return91DetailInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Return91DetailInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "Return91DetailInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _DetailID = "DetailID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ProductId = "ProductId" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ProductName = "ProductName" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ConsumeStreamId = "ConsumeStreamId" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OrderID = "OrderID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Uin = "Uin" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GoodsID = "GoodsID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GoodsInfo = "GoodsInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GoodsCount = "GoodsCount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OriginalMoney = "OriginalMoney" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OrderMoney = "OrderMoney" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Note = "Note" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _PayStatus = "PayStatus" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CreateTime = "CreateTime" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Sign = "Sign" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MySign = "MySign" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_detailID;						//
		private int m_productId;					//
		private string m_productName;				//
		private string m_consumeStreamId;			//
		private string m_orderID;					//
		private int m_uin;							//
		private string m_goodsID;					//
		private string m_goodsInfo;					//
		private int m_goodsCount;					//
		private decimal m_originalMoney;			//
		private decimal m_orderMoney;				//
		private string m_note;						//
		private int m_payStatus;					//
		private DateTime m_createTime;				//
		private string m_sign;						//
		private string m_mySign;					//
		private DateTime m_collectDate;				//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化Return91DetailInfo
		/// </summary>
		public Return91DetailInfo()
		{
			m_detailID=0;
			m_productId=0;
			m_productName="";
			m_consumeStreamId="";
			m_orderID="";
			m_uin=0;
			m_goodsID="";
			m_goodsInfo="";
			m_goodsCount=0;
			m_originalMoney=0;
			m_orderMoney=0;
			m_note="";
			m_payStatus=0;
			m_createTime=DateTime.Now;
			m_sign="";
			m_mySign="";
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
		public int ProductId
		{
			get { return m_productId; }
			set { m_productId = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProductName
		{
			get { return m_productName; }
			set { m_productName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ConsumeStreamId
		{
			get { return m_consumeStreamId; }
			set { m_consumeStreamId = value; }
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
		public int Uin
		{
			get { return m_uin; }
			set { m_uin = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string GoodsID
		{
			get { return m_goodsID; }
			set { m_goodsID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string GoodsInfo
		{
			get { return m_goodsInfo; }
			set { m_goodsInfo = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int GoodsCount
		{
			get { return m_goodsCount; }
			set { m_goodsCount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal OriginalMoney
		{
			get { return m_originalMoney; }
			set { m_originalMoney = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal OrderMoney
		{
			get { return m_orderMoney; }
			set { m_orderMoney = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Note
		{
			get { return m_note; }
			set { m_note = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int PayStatus
		{
			get { return m_payStatus; }
			set { m_payStatus = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			get { return m_createTime; }
			set { m_createTime = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Sign
		{
			get { return m_sign; }
			set { m_sign = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string MySign
		{
			get { return m_mySign; }
			set { m_mySign = value; }
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
