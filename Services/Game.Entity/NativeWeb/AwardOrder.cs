/*
 * 版本：4.0
 * 时间：2013-12-18
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
	/// <summary>
	/// 实体类 AwardOrder。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AwardOrder  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AwardOrder" ;

		/// <summary>
		/// 订单号码
		/// </summary>
		public const string _OrderID = "OrderID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 奖品标识
		/// </summary>
		public const string _AwardID = "AwardID" ;

		/// <summary>
		/// 奖品价格
		/// </summary>
		public const string _AwardPrice = "AwardPrice" ;

		/// <summary>
		/// 购买数量
		/// </summary>
		public const string _AwardCount = "AwardCount" ;

		/// <summary>
		/// 花费金额
		/// </summary>
		public const string _TotalAmount = "TotalAmount" ;

		/// <summary>
		/// 真实姓名
		/// </summary>
		public const string _Compellation = "Compellation" ;

		/// <summary>
		/// 移动电话
		/// </summary>
		public const string _MobilePhone = "MobilePhone" ;

		/// <summary>
		/// QQ号码
		/// </summary>
		public const string _QQ = "QQ" ;

		/// <summary>
		/// 省份
		/// </summary>
		public const string _Province = "Province" ;

		/// <summary>
		/// 城市
		/// </summary>
		public const string _City = "City" ;

		/// <summary>
		/// 区
		/// </summary>
		public const string _Area = "Area" ;

		/// <summary>
		/// 详细地址
		/// </summary>
		public const string _DwellingPlace = "DwellingPlace" ;

		/// <summary>
		/// 邮编
		/// </summary>
		public const string _PostalCode = "PostalCode" ;

		/// <summary>
		/// 订单状态(0:新订单,1:已发货,2:已收货,3:申请退货,4:同意退货等待客户发货,5:拒绝退货,6:退货成功)
		/// </summary>
		public const string _OrderStatus = "OrderStatus" ;

		/// <summary>
		/// IP地址
		/// </summary>
		public const string _BuyIP = "BuyIP" ;

		/// <summary>
		/// 购买时间
		/// </summary>
		public const string _BuyDate = "BuyDate" ;

		/// <summary>
		/// 处理备注
		/// </summary>
		public const string _SolveNote = "SolveNote" ;

		/// <summary>
		/// 处理时间
		/// </summary>
		public const string _SolveDate = "SolveDate" ;
		#endregion

		#region 私有变量
		private int m_orderID;					//订单号码
		private int m_userID;					//用户标识
		private int m_awardID;					//奖品标识
		private int m_awardPrice;				//奖品价格
		private int m_awardCount;				//购买数量
		private int m_totalAmount;				//花费金额
		private string m_compellation;			//真实姓名
		private string m_mobilePhone;			//移动电话
		private string m_qQ;					//QQ号码
		private int m_province;					//省份
		private int m_city;						//城市
		private int m_area;						//区
		private string m_dwellingPlace;			//详细地址
		private string m_postalCode;			//邮编
		private int m_orderStatus;				//订单状态(0:新订单,1:已发货,2:已收货,3:申请退货,4:同意退货等待客户发货,5:拒绝退货,6:退货成功)
		private string m_buyIP;					//IP地址
		private DateTime m_buyDate;				//购买时间
		private string m_solveNote;				//处理备注
		private DateTime m_solveDate;			//处理时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AwardOrder
		/// </summary>
		public AwardOrder()
		{
			m_orderID=0;
			m_userID=0;
			m_awardID=0;
			m_awardPrice=0;
			m_awardCount=0;
			m_totalAmount=0;
			m_compellation="";
			m_mobilePhone="";
			m_qQ="";
			m_province=0;
			m_city=0;
			m_area=0;
			m_dwellingPlace="";
			m_postalCode="";
			m_orderStatus=0;
			m_buyIP="";
			m_buyDate=DateTime.Now;
			m_solveNote="";
			m_solveDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 订单号码
		/// </summary>
		public int OrderID
		{
			get { return m_orderID; }
			set { m_orderID = value; }
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
		/// 奖品标识
		/// </summary>
		public int AwardID
		{
			get { return m_awardID; }
			set { m_awardID = value; }
		}

		/// <summary>
		/// 奖品价格
		/// </summary>
		public int AwardPrice
		{
			get { return m_awardPrice; }
			set { m_awardPrice = value; }
		}

		/// <summary>
		/// 购买数量
		/// </summary>
		public int AwardCount
		{
			get { return m_awardCount; }
			set { m_awardCount = value; }
		}

		/// <summary>
		/// 花费金额
		/// </summary>
		public int TotalAmount
		{
			get { return m_totalAmount; }
			set { m_totalAmount = value; }
		}

		/// <summary>
		/// 真实姓名
		/// </summary>
		public string Compellation
		{
			get { return m_compellation; }
			set { m_compellation = value; }
		}

		/// <summary>
		/// 移动电话
		/// </summary>
		public string MobilePhone
		{
			get { return m_mobilePhone; }
			set { m_mobilePhone = value; }
		}

		/// <summary>
		/// QQ号码
		/// </summary>
		public string QQ
		{
			get { return m_qQ; }
			set { m_qQ = value; }
		}

		/// <summary>
		/// 省份
		/// </summary>
		public int Province
		{
			get { return m_province; }
			set { m_province = value; }
		}

		/// <summary>
		/// 城市
		/// </summary>
		public int City
		{
			get { return m_city; }
			set { m_city = value; }
		}

		/// <summary>
		/// 区
		/// </summary>
		public int Area
		{
			get { return m_area; }
			set { m_area = value; }
		}

		/// <summary>
		/// 详细地址
		/// </summary>
		public string DwellingPlace
		{
			get { return m_dwellingPlace; }
			set { m_dwellingPlace = value; }
		}

		/// <summary>
		/// 邮编
		/// </summary>
		public string PostalCode
		{
			get { return m_postalCode; }
			set { m_postalCode = value; }
		}

		/// <summary>
		/// 订单状态(0:新订单,1:已发货,2:已收货,3:申请退货,4:同意退货等待客户发货,5:拒绝退货,6:退货成功)
		/// </summary>
		public int OrderStatus
		{
			get { return m_orderStatus; }
			set { m_orderStatus = value; }
		}

		/// <summary>
		/// IP地址
		/// </summary>
		public string BuyIP
		{
			get { return m_buyIP; }
			set { m_buyIP = value; }
		}

		/// <summary>
		/// 购买时间
		/// </summary>
		public DateTime BuyDate
		{
			get { return m_buyDate; }
			set { m_buyDate = value; }
		}

		/// <summary>
		/// 处理备注
		/// </summary>
		public string SolveNote
		{
			get { return m_solveNote; }
			set { m_solveNote = value; }
		}

		/// <summary>
		/// 处理时间
		/// </summary>
		public DateTime SolveDate
		{
			get { return m_solveDate; }
			set { m_solveDate = value; }
		}
		#endregion
	}
}
