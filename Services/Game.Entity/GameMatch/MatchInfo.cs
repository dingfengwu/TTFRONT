/*
 * 版本：4.0
 * 时间：2016/5/18
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.GameMatch
{
	/// <summary>
	/// 实体类 MatchInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MatchInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MatchInfo" ;

		/// <summary>
		/// 比赛标识
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 比赛名称
		/// </summary>
		public const string _MatchName = "MatchName" ;

		/// <summary>
		/// 比赛时间
		/// </summary>
		public const string _MatchDate = "MatchDate" ;

		/// <summary>
		/// 比赛摘要
		/// </summary>
		public const string _MatchSummary = "MatchSummary" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchImage = "MatchImage" ;

		/// <summary>
		/// 比赛介绍
		/// </summary>
		public const string _MatchContent = "MatchContent" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 是否禁用
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 添加时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_matchID;					//比赛标识
		private string m_matchName;				//比赛名称
		private string m_matchDate;				//比赛时间
		private string m_matchSummary;			//比赛摘要
		private string m_matchImage;			//
		private string m_matchContent;			//比赛介绍
		private int m_sortID;					//
		private bool m_nullity;					//是否禁用
		private DateTime m_collectDate;			//添加时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MatchInfo
		/// </summary>
		public MatchInfo()
		{
			m_matchID=0;
			m_matchName="";
			m_matchDate="";
			m_matchSummary="";
			m_matchImage="";
			m_matchContent="";
			m_sortID=0;
			m_nullity=false;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 比赛标识
		/// </summary>
		public int MatchID
		{
			get { return m_matchID; }
			set { m_matchID = value; }
		}

		/// <summary>
		/// 比赛名称
		/// </summary>
		public string MatchName
		{
			get { return m_matchName; }
			set { m_matchName = value; }
		}

		/// <summary>
		/// 比赛时间
		/// </summary>
		public string MatchDate
		{
			get { return m_matchDate; }
			set { m_matchDate = value; }
		}

		/// <summary>
		/// 比赛摘要
		/// </summary>
		public string MatchSummary
		{
			get { return m_matchSummary; }
			set { m_matchSummary = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string MatchImage
		{
			get { return m_matchImage; }
			set { m_matchImage = value; }
		}

		/// <summary>
		/// 比赛介绍
		/// </summary>
		public string MatchContent
		{
			get { return m_matchContent; }
			set { m_matchContent = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}

		/// <summary>
		/// 是否禁用
		/// </summary>
		public bool Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
