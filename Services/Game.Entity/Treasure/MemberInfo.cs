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
	/// 实体类 MemberInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MemberInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MemberInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CardID = "CardID" ;

		/// <summary>
		/// 会员卡名称
		/// </summary>
		public const string _CardName = "CardName" ;

		/// <summary>
		/// 会员卡价格
		/// </summary>
		public const string _CardPrice = "CardPrice" ;

		/// <summary>
		/// 赠送金币数
		/// </summary>
		public const string _PresentGold = "PresentGold" ;

		/// <summary>
		/// 会员等级
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 会员天数
		/// </summary>
		public const string _MemberDays = "MemberDays" ;

		/// <summary>
		/// 用户权限
		/// </summary>
		public const string _UserRight = "UserRight" ;

		/// <summary>
		/// 服务权限
		/// </summary>
		public const string _ServiceRight = "ServiceRight" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _InputDate = "InputDate" ;
		#endregion

		#region 私有变量
		private int m_cardID;				//
		private string m_cardName;			//会员卡名称
		private int m_cardPrice;			//会员卡价格
		private int m_presentGold;			//赠送金币数
		private byte m_memberOrder;			//会员等级
		private int m_memberDays;			//会员天数
		private int m_userRight;			//用户权限
		private int m_serviceRight;			//服务权限
		private DateTime m_inputDate;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MemberInfo
		/// </summary>
		public MemberInfo()
		{
			m_cardID=0;
			m_cardName="";
			m_cardPrice=0;
			m_presentGold=0;
			m_memberOrder=0;
			m_memberDays=0;
			m_userRight=0;
			m_serviceRight=0;
			m_inputDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int CardID
		{
			get { return m_cardID; }
			set { m_cardID = value; }
		}

		/// <summary>
		/// 会员卡名称
		/// </summary>
		public string CardName
		{
			get { return m_cardName; }
			set { m_cardName = value; }
		}

		/// <summary>
		/// 会员卡价格
		/// </summary>
		public int CardPrice
		{
			get { return m_cardPrice; }
			set { m_cardPrice = value; }
		}

		/// <summary>
		/// 赠送金币数
		/// </summary>
		public int PresentGold
		{
			get { return m_presentGold; }
			set { m_presentGold = value; }
		}

		/// <summary>
		/// 会员等级
		/// </summary>
		public byte MemberOrder
		{
			get { return m_memberOrder; }
			set { m_memberOrder = value; }
		}

		/// <summary>
		/// 会员天数
		/// </summary>
		public int MemberDays
		{
			get { return m_memberDays; }
			set { m_memberDays = value; }
		}

		/// <summary>
		/// 用户权限
		/// </summary>
		public int UserRight
		{
			get { return m_userRight; }
			set { m_userRight = value; }
		}

		/// <summary>
		/// 服务权限
		/// </summary>
		public int ServiceRight
		{
			get { return m_serviceRight; }
			set { m_serviceRight = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}
		#endregion
	}
}
