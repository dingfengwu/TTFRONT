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
	/// 实体类 AwardInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AwardInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AwardInfo" ;

		/// <summary>
		/// 奖品标识
		/// </summary>
		public const string _AwardID = "AwardID" ;

		/// <summary>
		/// 奖品名称
		/// </summary>
		public const string _AwardName = "AwardName" ;

		/// <summary>
		/// 奖品类型
		/// </summary>
		public const string _TypeID = "TypeID" ;

		/// <summary>
		/// 奖品价格
		/// </summary>
		public const string _Price = "Price" ;

		/// <summary>
		/// 库存数量
		/// </summary>
		public const string _Inventory = "Inventory" ;

		/// <summary>
		/// 已售数量
		/// </summary>
		public const string _BuyCount = "BuyCount" ;

		/// <summary>
		/// 展示小图
		/// </summary>
		public const string _SmallImage = "SmallImage" ;

		/// <summary>
		/// 展示大图
		/// </summary>
		public const string _BigImage = "BigImage" ;

		/// <summary>
		/// 购买是需要填写的信息(用2进制的位来配置，0为不需要，1为需要。位从右至左配置的项分别为真实姓名，手机号，QQ号，收货地址及邮编)
		/// </summary>
		public const string _NeedInfo = "NeedInfo" ;

		/// <summary>
		/// 是否需要蓝钻
		/// </summary>
		public const string _IsMember = "IsMember" ;

		/// <summary>
		/// 是否允许退货(0:不允许,1允许)
		/// </summary>
		public const string _IsReturn = "IsReturn" ;

		/// <summary>
		/// 排序
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 是否禁用
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 奖品描述
		/// </summary>
		public const string _Description = "Description" ;

		/// <summary>
		/// 收集时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_awardID;					//奖品标识
		private string m_awardName;				//奖品名称
		private int m_typeID;					//奖品类型
		private int m_price;					//奖品价格
		private int m_inventory;				//库存数量
		private int m_buyCount;					//已售数量
		private string m_smallImage;			//展示小图
		private string m_bigImage;				//展示大图
		private int m_needInfo;					//购买是需要填写的信息(用2进制的位来配置，0为不需要，1为需要。位从右至左配置的项分别为真实姓名，手机号，QQ号，收货地址及邮编)
		private bool m_isMember;				//是否需要蓝钻
		private bool m_isReturn;				//是否允许退货(0:不允许,1允许)
		private int m_sortID;					//排序
		private byte m_nullity;					//是否禁用
		private string m_description;			//奖品描述
		private DateTime m_collectDate;			//收集时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AwardInfo
		/// </summary>
		public AwardInfo()
		{
			m_awardID=0;
			m_awardName="";
			m_typeID=0;
			m_price=0;
			m_inventory=0;
			m_buyCount=0;
			m_smallImage="";
			m_bigImage="";
			m_needInfo=0;
			m_isMember=false;
			m_isReturn=false;
			m_sortID=0;
			m_nullity=0;
			m_description="";
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 奖品标识
		/// </summary>
		public int AwardID
		{
			get { return m_awardID; }
			set { m_awardID = value; }
		}

		/// <summary>
		/// 奖品名称
		/// </summary>
		public string AwardName
		{
			get { return m_awardName; }
			set { m_awardName = value; }
		}

		/// <summary>
		/// 奖品类型
		/// </summary>
		public int TypeID
		{
			get { return m_typeID; }
			set { m_typeID = value; }
		}

		/// <summary>
		/// 奖品价格
		/// </summary>
		public int Price
		{
			get { return m_price; }
			set { m_price = value; }
		}

		/// <summary>
		/// 库存数量
		/// </summary>
		public int Inventory
		{
			get { return m_inventory; }
			set { m_inventory = value; }
		}

		/// <summary>
		/// 已售数量
		/// </summary>
		public int BuyCount
		{
			get { return m_buyCount; }
			set { m_buyCount = value; }
		}

		/// <summary>
		/// 展示小图
		/// </summary>
		public string SmallImage
		{
			get { return m_smallImage; }
			set { m_smallImage = value; }
		}

		/// <summary>
		/// 展示大图
		/// </summary>
		public string BigImage
		{
			get { return m_bigImage; }
			set { m_bigImage = value; }
		}

		/// <summary>
		/// 购买是需要填写的信息(用2进制的位来配置，0为不需要，1为需要。位从右至左配置的项分别为真实姓名，手机号，QQ号，收货地址及邮编)
		/// </summary>
		public int NeedInfo
		{
			get { return m_needInfo; }
			set { m_needInfo = value; }
		}

		/// <summary>
		/// 是否需要蓝钻
		/// </summary>
		public bool IsMember
		{
			get { return m_isMember; }
			set { m_isMember = value; }
		}

		/// <summary>
		/// 是否允许退货(0:不允许,1允许)
		/// </summary>
		public bool IsReturn
		{
			get { return m_isReturn; }
			set { m_isReturn = value; }
		}

		/// <summary>
		/// 排序
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}

		/// <summary>
		/// 是否禁用
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 奖品描述
		/// </summary>
		public string Description
		{
			get { return m_description; }
			set { m_description = value; }
		}

		/// <summary>
		/// 收集时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
