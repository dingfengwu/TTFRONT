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
	/// 实体类 AwardType。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AwardType  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AwardType" ;

		/// <summary>
		/// 类别标识
		/// </summary>
		public const string _TypeID = "TypeID" ;

		/// <summary>
		/// 父类型
		/// </summary>
		public const string _ParentID = "ParentID" ;

		/// <summary>
		/// 类别名称
		/// </summary>
		public const string _TypeName = "TypeName" ;

		/// <summary>
		/// 排序字段
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 禁用标志
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 收集时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_typeID;					//类别标识
		private int m_parentID;					//父类型
		private string m_typeName;				//类别名称
		private int m_sortID;					//排序字段
		private byte m_nullity;					//禁用标志
		private DateTime m_collectDate;			//收集时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AwardType
		/// </summary>
		public AwardType()
		{
			m_typeID=0;
			m_parentID=0;
			m_typeName="";
			m_sortID=0;
			m_nullity=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 类别标识
		/// </summary>
		public int TypeID
		{
			get { return m_typeID; }
			set { m_typeID = value; }
		}

		/// <summary>
		/// 父类型
		/// </summary>
		public int ParentID
		{
			get { return m_parentID; }
			set { m_parentID = value; }
		}

		/// <summary>
		/// 类别名称
		/// </summary>
		public string TypeName
		{
			get { return m_typeName; }
			set { m_typeName = value; }
		}

		/// <summary>
		/// 排序字段
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}

		/// <summary>
		/// 禁用标志
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
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
