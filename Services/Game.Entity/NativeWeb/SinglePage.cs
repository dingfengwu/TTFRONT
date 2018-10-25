/*
 * 版本：4.0
 * 时间：2014-3-14
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
	/// 实体类 SinglePage。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SinglePage  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "SinglePage" ;

		/// <summary>
		/// 页标识
		/// </summary>
		public const string _PageID = "PageID" ;

		/// <summary>
		/// 唯一key
		/// </summary>
		public const string _KeyValue = "KeyValue" ;

		/// <summary>
		/// 页名称
		/// </summary>
		public const string _PageName = "PageName" ;

		/// <summary>
		/// 页关键字
		/// </summary>
		public const string _KeyWords = "KeyWords" ;

		/// <summary>
		/// 页描述
		/// </summary>
		public const string _Description = "Description" ;

		/// <summary>
		/// 内容详情
		/// </summary>
		public const string _Contents = "Contents" ;
		#endregion

		#region 私有变量
		private int m_pageID;					//页标识
		private string m_keyValue;				//唯一key
		private string m_pageName;				//页名称
		private string m_keyWords;				//页关键字
		private string m_description;			//页描述
		private string m_contents;				//内容详情
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化SinglePage
		/// </summary>
		public SinglePage()
		{
			m_pageID=0;
			m_keyValue="";
			m_pageName="";
			m_keyWords="";
			m_description="";
			m_contents="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 页标识
		/// </summary>
		public int PageID
		{
			get { return m_pageID; }
			set { m_pageID = value; }
		}

		/// <summary>
		/// 唯一key
		/// </summary>
		public string KeyValue
		{
			get { return m_keyValue; }
			set { m_keyValue = value; }
		}

		/// <summary>
		/// 页名称
		/// </summary>
		public string PageName
		{
			get { return m_pageName; }
			set { m_pageName = value; }
		}

		/// <summary>
		/// 页关键字
		/// </summary>
		public string KeyWords
		{
			get { return m_keyWords; }
			set { m_keyWords = value; }
		}

		/// <summary>
		/// 页描述
		/// </summary>
		public string Description
		{
			get { return m_description; }
			set { m_description = value; }
		}

		/// <summary>
		/// 内容详情
		/// </summary>
		public string Contents
		{
			get { return m_contents; }
			set { m_contents = value; }
		}
		#endregion
	}
}
