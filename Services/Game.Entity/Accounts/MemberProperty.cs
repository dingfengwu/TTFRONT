/*
 * 版本：4.0
 * 时间：2016/3/31
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
	/// <summary>
	/// 实体类 MemberProperty。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MemberProperty  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MemberProperty" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 会员名称
		/// </summary>
		public const string _MemberName = "MemberName" ;

		/// <summary>
		/// 会员权限
		/// </summary>
		public const string _UserRight = "UserRight" ;

		/// <summary>
		/// 任务奖励
		/// </summary>
		public const string _TaskRate = "TaskRate" ;

		/// <summary>
		/// 商城折扣
		/// </summary>
		public const string _ShopRate = "ShopRate" ;

		/// <summary>
		/// 银行优惠
		/// </summary>
		public const string _InsureRate = "InsureRate" ;

		/// <summary>
		/// 每日赠送
		/// </summary>
		public const string _DayPresent = "DayPresent" ;

		/// <summary>
		/// 每日登录礼包
		/// </summary>
		public const string _DayGiftID = "DayGiftID" ;

		/// <summary>
		/// 创建时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 备注
		/// </summary>
		public const string _CollectNote = "CollectNote" ;
		#endregion

		#region 私有变量
		private int m_memberOrder;			//
		private string m_memberName;		//会员名称
		private int m_userRight;			//会员权限
		private int m_taskRate;				//任务奖励
		private int m_shopRate;				//商城折扣
		private int m_insureRate;			//银行优惠
		private int m_dayPresent;			//每日赠送
		private int m_dayGiftID;			//每日登录礼包
		private DateTime m_collectDate;		//创建时间
		private string m_collectNote;		//备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MemberProperty
		/// </summary>
		public MemberProperty()
		{
			m_memberOrder=0;
			m_memberName="";
			m_userRight=0;
			m_taskRate=0;
			m_shopRate=0;
			m_insureRate=0;
			m_dayPresent=0;
			m_dayGiftID=0;
			m_collectDate=DateTime.Now;
			m_collectNote="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int MemberOrder
		{
			get { return m_memberOrder; }
			set { m_memberOrder = value; }
		}

		/// <summary>
		/// 会员名称
		/// </summary>
		public string MemberName
		{
			get { return m_memberName; }
			set { m_memberName = value; }
		}

		/// <summary>
		/// 会员权限
		/// </summary>
		public int UserRight
		{
			get { return m_userRight; }
			set { m_userRight = value; }
		}

		/// <summary>
		/// 任务奖励
		/// </summary>
		public int TaskRate
		{
			get { return m_taskRate; }
			set { m_taskRate = value; }
		}

		/// <summary>
		/// 商城折扣
		/// </summary>
		public int ShopRate
		{
			get { return m_shopRate; }
			set { m_shopRate = value; }
		}

		/// <summary>
		/// 银行优惠
		/// </summary>
		public int InsureRate
		{
			get { return m_insureRate; }
			set { m_insureRate = value; }
		}

		/// <summary>
		/// 每日赠送
		/// </summary>
		public int DayPresent
		{
			get { return m_dayPresent; }
			set { m_dayPresent = value; }
		}

		/// <summary>
		/// 每日登录礼包
		/// </summary>
		public int DayGiftID
		{
			get { return m_dayGiftID; }
			set { m_dayGiftID = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string CollectNote
		{
			get { return m_collectNote; }
			set { m_collectNote = value; }
		}
		#endregion
	}
}
