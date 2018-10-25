/*
 * 版本：4.0
 * 时间：2014-6-12
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
	/// 实体类 Activity。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Activity  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "Activity" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ActivityID = "ActivityID" ;

		/// <summary>
		/// 活动名称
		/// </summary>
		public const string _Title = "Title" ;

		/// <summary>
		/// 排序
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 图片地址
		/// </summary>
		public const string _ImageUrl = "ImageUrl" ;

		/// <summary>
		/// 活动时间
		/// </summary>
		public const string _Time = "Time" ;

		/// <summary>
		/// 活动描述
		/// </summary>
		public const string _Describe = "Describe" ;

		/// <summary>
		/// 是否推荐至首页
		/// </summary>
		public const string _IsRecommend = "IsRecommend" ;

		/// <summary>
		/// 录入日期
		/// </summary>
		public const string _InputDate = "InputDate" ;
		#endregion

		#region 私有变量
		private int m_activityID;			//
		private string m_title;				//活动名称
		private int m_sortID;				//排序
		private string m_imageUrl;			//图片地址
		private string m_time;				//活动时间
		private string m_describe;			//活动描述
		private bool m_isRecommend;			//是否推荐至首页
		private DateTime m_inputDate;		//录入日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化Activity
		/// </summary>
		public Activity()
		{
			m_activityID=0;
			m_title="";
			m_sortID=0;
			m_imageUrl="";
			m_time="";
			m_describe="";
			m_isRecommend=false;
			m_inputDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int ActivityID
		{
			get { return m_activityID; }
			set { m_activityID = value; }
		}

		/// <summary>
		/// 活动名称
		/// </summary>
		public string Title
		{
			get { return m_title; }
			set { m_title = value; }
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
		/// 图片地址
		/// </summary>
		public string ImageUrl
		{
			get { return m_imageUrl; }
			set { m_imageUrl = value; }
		}

		/// <summary>
		/// 活动时间
		/// </summary>
		public string Time
		{
			get { return m_time; }
			set { m_time = value; }
		}

		/// <summary>
		/// 活动描述
		/// </summary>
		public string Describe
		{
			get { return m_describe; }
			set { m_describe = value; }
		}

		/// <summary>
		/// 是否推荐至首页
		/// </summary>
		public bool IsRecommend
		{
			get { return m_isRecommend; }
			set { m_isRecommend = value; }
		}

		/// <summary>
		/// 录入日期
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}
		#endregion
	}
}
