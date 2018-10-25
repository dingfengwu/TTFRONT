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
	/// 实体类 GlobalCheckIn。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GlobalCheckIn  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GlobalCheckIn" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 赠送金币
		/// </summary>
		public const string _PresentGold = "PresentGold" ;

		/// <summary>
		/// 备注
		/// </summary>
		public const string _CollectNote = "CollectNote" ;
		#endregion

		#region 私有变量
		private int m_iD;					//
		private int m_presentGold;			//赠送金币
		private string m_collectNote;		//备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GlobalCheckIn
		/// </summary>
		public GlobalCheckIn()
		{
			m_iD=0;
			m_presentGold=0;
			m_collectNote="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
		}

		/// <summary>
		/// 赠送金币
		/// </summary>
		public int PresentGold
		{
			get { return m_presentGold; }
			set { m_presentGold = value; }
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
