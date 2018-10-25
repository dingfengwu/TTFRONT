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
	/// 实体类 ReturnDouwanDetailInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ReturnDouwanDetailInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnDouwanDetailInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _DetailID = "DetailID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OpenId = "OpenId" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ServerId = "ServerId" ;

		/// <summary>
		/// ''
		/// </summary>
		public const string _ServerName = "ServerName" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RoleId = "RoleId" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RoleName = "RoleName" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OrderId = "OrderId" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OrderStatus = "OrderStatus" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _PayType = "PayType" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Amount = "Amount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Remark = "Remark" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CallBackInfo = "CallBackInfo" ;

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
		private int m_detailID;					//
		private string m_openId;				//
		private string m_serverId;				//
		private string m_serverName;			//''
		private string m_roleId;				//
		private string m_roleName;				//
		private string m_orderId;				//
		private int m_orderStatus;				//
		private string m_payType;				//
		private decimal m_amount;				//
		private string m_remark;				//
		private string m_callBackInfo;			//
		private string m_sign;					//
		private string m_mySign;				//
		private DateTime m_collectDate;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnDouwanDetailInfo
		/// </summary>
		public ReturnDouwanDetailInfo()
		{
			m_detailID=0;
			m_openId="";
			m_serverId="";
			m_serverName="";
			m_roleId="";
			m_roleName="";
			m_orderId="";
			m_orderStatus=0;
			m_payType="";
			m_amount=0;
			m_remark="";
			m_callBackInfo="";
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
		public string OpenId
		{
			get { return m_openId; }
			set { m_openId = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ServerId
		{
			get { return m_serverId; }
			set { m_serverId = value; }
		}

		/// <summary>
		/// ''
		/// </summary>
		public string ServerName
		{
			get { return m_serverName; }
			set { m_serverName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string RoleId
		{
			get { return m_roleId; }
			set { m_roleId = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string RoleName
		{
			get { return m_roleName; }
			set { m_roleName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string OrderId
		{
			get { return m_orderId; }
			set { m_orderId = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int OrderStatus
		{
			get { return m_orderStatus; }
			set { m_orderStatus = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PayType
		{
			get { return m_payType; }
			set { m_payType = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal Amount
		{
			get { return m_amount; }
			set { m_amount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			get { return m_remark; }
			set { m_remark = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string CallBackInfo
		{
			get { return m_callBackInfo; }
			set { m_callBackInfo = value; }
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
