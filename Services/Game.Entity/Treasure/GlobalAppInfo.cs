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
	/// 实体类 GlobalAppInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GlobalAppInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GlobalAppInfo" ;

		/// <summary>
		/// 主键标识
		/// </summary>
		public const string _AppID = "AppID" ;

		/// <summary>
		/// 商品标识
		/// </summary>
		public const string _ProductID = "ProductID" ;

		/// <summary>
		/// 商品名称
		/// </summary>
		public const string _ProductName = "ProductName" ;

		/// <summary>
		/// 商品描述
		/// </summary>
		public const string _Description = "Description" ;

		/// <summary>
		/// 商品价格
		/// </summary>
		public const string _Price = "Price" ;

		/// <summary>
		/// 商品标识(1:手机使用,2:PAD使用)
		/// </summary>
		public const string _TagID = "TagID" ;

		/// <summary>
		/// 创建日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_appID;					//主键标识
		private string m_productID;				//商品标识
		private string m_productName;			//商品名称
		private string m_description;			//商品描述
		private decimal m_price;				//商品价格
        private decimal m_attachCurrency;       //首充奖励
		private int m_tagID;					//商品标识(1:手机使用,2:PAD使用)
		private DateTime m_collectDate;			//创建日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GlobalAppInfo
		/// </summary>
		public GlobalAppInfo()
		{
			m_appID=0;
			m_productID="";
			m_productName="";
			m_description="";
			m_price=0;
            m_attachCurrency = 0;
			m_tagID=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 主键标识
		/// </summary>
		public int AppID
		{
			get { return m_appID; }
			set { m_appID = value; }
		}

		/// <summary>
		/// 商品标识
		/// </summary>
		public string ProductID
		{
			get { return m_productID; }
			set { m_productID = value; }
		}

		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductName
		{
			get { return m_productName; }
			set { m_productName = value; }
		}

		/// <summary>
		/// 商品描述
		/// </summary>
		public string Description
		{
			get { return m_description; }
			set { m_description = value; }
		}

		/// <summary>
		/// 商品价格
		/// </summary>
		public decimal Price
		{
			get { return m_price; }
			set { m_price = value; }
		}

        /// <summary>
        /// 首充奖励
        /// </summary>
        public decimal AttachCurrency
        {
            get { return m_attachCurrency; }
            set { m_attachCurrency = value; }
        }

		/// <summary>
		/// 商品标识(1:手机使用,2:PAD使用)
		/// </summary>
		public int TagID
		{
			get { return m_tagID; }
			set { m_tagID = value; }
		}

		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
