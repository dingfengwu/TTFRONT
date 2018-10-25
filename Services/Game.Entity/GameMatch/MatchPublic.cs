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
	/// 实体类 MatchPublic。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MatchPublic  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MatchPublic" ;

		/// <summary>
		/// 比赛标识
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 比赛状态（0x00： 为空闲   0x02：比赛中  0x08： 比赛结束）
		/// </summary>
		public const string _MatchStatus = "MatchStatus" ;

		/// <summary>
		/// 类型标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 比赛名称
		/// </summary>
		public const string _MatchName = "MatchName" ;

		/// <summary>
		/// 比赛类型 0:定时赛 1:即时赛
		/// </summary>
		public const string _MatchType = "MatchType" ;

		/// <summary>
		/// 报名方式  1:扣除金币 2:跳转网站
		/// </summary>
		public const string _SignupMode = "SignupMode" ;

		/// <summary>
		/// 费用类型（0：金币   1： 奖牌）
		/// </summary>
		public const string _FeeType = "FeeType" ;

		/// <summary>
		/// 扣费数量
		/// </summary>
		public const string _SignupFee = "SignupFee" ;

		/// <summary>
		/// 扣费区域（0： 房间扣费 1： 网页扣费）
		/// </summary>
		public const string _DeductArea = "DeductArea" ;

		/// <summary>
		/// 参赛条件  (1 表示会员等级 2表示经验值 4表示比赛玩家)
		/// </summary>
		public const string _JoinCondition = "JoinCondition" ;

		/// <summary>
		/// 会员等级
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 玩家经验
		/// </summary>
		public const string _Experience = "Experience" ;

		/// <summary>
		/// 赛事来源
		/// </summary>
		public const string _FromMatchID = "FromMatchID" ;

		/// <summary>
		/// 筛选方式
		/// </summary>
		public const string _FilterType = "FilterType" ;

		/// <summary>
		/// 最大名次
		/// </summary>
		public const string _MaxRankID = "MaxRankID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchEndDate = "MatchEndDate" ;

		/// <summary>
		/// 比赛玩家所属比赛的开始统计日期
		/// </summary>
		public const string _MatchStartDate = "MatchStartDate" ;

		/// <summary>
		/// 排名方式（0:  按总成绩排名   1:  按特定局数成绩排名）
		/// </summary>
		public const string _RankingMode = "RankingMode" ;

		/// <summary>
		/// 统计局数( 比赛多少局统计一次成绩)
		/// </summary>
		public const string _CountInnings = "CountInnings" ;

		/// <summary>
		/// 筛选方式(0: 取最优成绩  1：取平均成绩 2：去掉最优和最差,取平均成绩  )
		/// </summary>
		public const string _FilterGradesMode = "FilterGradesMode" ;

		/// <summary>
		/// 分配规则
		/// </summary>
		public const string _DistributeRule = "DistributeRule" ;

		/// <summary>
		/// 最小分组人数
		/// </summary>
		public const string _MinDistributeUser = "MinDistributeUser" ;

		/// <summary>
		/// 分组时间间隔
		/// </summary>
		public const string _DistributeTimeSpace = "DistributeTimeSpace" ;

		/// <summary>
		/// 最小游戏人数
		/// </summary>
		public const string _MinPartakeGameUser = "MinPartakeGameUser" ;

		/// <summary>
		/// 最大游戏人数
		/// </summary>
		public const string _MaxPartakeGameUser = "MaxPartakeGameUser" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchRule = "MatchRule" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ServiceMachine = "ServiceMachine" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 创建日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_matchID;						//比赛标识
		private byte m_matchStatus;					//比赛状态（0x00： 为空闲   0x02：比赛中  0x08： 比赛结束）
		private int m_kindID;						//类型标识
		private string m_matchName;					//比赛名称
		private byte m_matchType;					//比赛类型 0:定时赛 1:即时赛
		private byte m_signupMode;					//报名方式  1:扣除金币 2:跳转网站
		private byte m_feeType;						//费用类型（0：金币   1： 奖牌）
		private long m_signupFee;					//扣费数量
		private byte m_deductArea;					//扣费区域（0： 房间扣费 1： 网页扣费）
		private byte m_joinCondition;				//参赛条件  (1 表示会员等级 2表示经验值 4表示比赛玩家)
		private byte m_memberOrder;					//会员等级
		private int m_experience;					//玩家经验
		private int m_fromMatchID;					//赛事来源
		private byte m_filterType;					//筛选方式
		private short m_maxRankID;					//最大名次
		private DateTime m_matchEndDate;			//
		private DateTime m_matchStartDate;			//比赛玩家所属比赛的开始统计日期
		private byte m_rankingMode;					//排名方式（0:  按总成绩排名   1:  按特定局数成绩排名）
		private short m_countInnings;				//统计局数( 比赛多少局统计一次成绩)
		private byte m_filterGradesMode;			//筛选方式(0: 取最优成绩  1：取平均成绩 2：去掉最优和最差,取平均成绩  )
		private byte m_distributeRule;				//分配规则
		private short m_minDistributeUser;			//最小分组人数
		private short m_distributeTimeSpace;		//分组时间间隔
		private short m_minPartakeGameUser;			//最小游戏人数
		private short m_maxPartakeGameUser;			//最大游戏人数
		private string m_matchRule;					//
		private string m_serviceMachine;			//
		private bool m_nullity;						//
		private DateTime m_collectDate;				//创建日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MatchPublic
		/// </summary>
		public MatchPublic()
		{
			m_matchID=0;
			m_matchStatus=0;
			m_kindID=0;
			m_matchName="";
			m_matchType=0;
			m_signupMode=0;
			m_feeType=0;
			m_signupFee=0;
			m_deductArea=0;
			m_joinCondition=0;
			m_memberOrder=0;
			m_experience=0;
			m_fromMatchID=0;
			m_filterType=0;
			m_maxRankID=0;
			m_matchEndDate=DateTime.Now;
			m_matchStartDate=DateTime.Now;
			m_rankingMode=0;
			m_countInnings=0;
			m_filterGradesMode=0;
			m_distributeRule=0;
			m_minDistributeUser=0;
			m_distributeTimeSpace=0;
			m_minPartakeGameUser=0;
			m_maxPartakeGameUser=0;
			m_matchRule="";
			m_serviceMachine="";
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
		/// 比赛状态（0x00： 为空闲   0x02：比赛中  0x08： 比赛结束）
		/// </summary>
		public byte MatchStatus
		{
			get { return m_matchStatus; }
			set { m_matchStatus = value; }
		}

		/// <summary>
		/// 类型标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
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
		/// 比赛类型 0:定时赛 1:即时赛
		/// </summary>
		public byte MatchType
		{
			get { return m_matchType; }
			set { m_matchType = value; }
		}

		/// <summary>
		/// 报名方式  1:扣除金币 2:跳转网站
		/// </summary>
		public byte SignupMode
		{
			get { return m_signupMode; }
			set { m_signupMode = value; }
		}

		/// <summary>
		/// 费用类型（0：金币   1： 奖牌）
		/// </summary>
		public byte FeeType
		{
			get { return m_feeType; }
			set { m_feeType = value; }
		}

		/// <summary>
		/// 扣费数量
		/// </summary>
		public long SignupFee
		{
			get { return m_signupFee; }
			set { m_signupFee = value; }
		}

		/// <summary>
		/// 扣费区域（0： 房间扣费 1： 网页扣费）
		/// </summary>
		public byte DeductArea
		{
			get { return m_deductArea; }
			set { m_deductArea = value; }
		}

		/// <summary>
		/// 参赛条件  (1 表示会员等级 2表示经验值 4表示比赛玩家)
		/// </summary>
		public byte JoinCondition
		{
			get { return m_joinCondition; }
			set { m_joinCondition = value; }
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
		/// 玩家经验
		/// </summary>
		public int Experience
		{
			get { return m_experience; }
			set { m_experience = value; }
		}

		/// <summary>
		/// 赛事来源
		/// </summary>
		public int FromMatchID
		{
			get { return m_fromMatchID; }
			set { m_fromMatchID = value; }
		}

		/// <summary>
		/// 筛选方式
		/// </summary>
		public byte FilterType
		{
			get { return m_filterType; }
			set { m_filterType = value; }
		}

		/// <summary>
		/// 最大名次
		/// </summary>
		public short MaxRankID
		{
			get { return m_maxRankID; }
			set { m_maxRankID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime MatchEndDate
		{
			get { return m_matchEndDate; }
			set { m_matchEndDate = value; }
		}

		/// <summary>
		/// 比赛玩家所属比赛的开始统计日期
		/// </summary>
		public DateTime MatchStartDate
		{
			get { return m_matchStartDate; }
			set { m_matchStartDate = value; }
		}

		/// <summary>
		/// 排名方式（0:  按总成绩排名   1:  按特定局数成绩排名）
		/// </summary>
		public byte RankingMode
		{
			get { return m_rankingMode; }
			set { m_rankingMode = value; }
		}

		/// <summary>
		/// 统计局数( 比赛多少局统计一次成绩)
		/// </summary>
		public short CountInnings
		{
			get { return m_countInnings; }
			set { m_countInnings = value; }
		}

		/// <summary>
		/// 筛选方式(0: 取最优成绩  1：取平均成绩 2：去掉最优和最差,取平均成绩  )
		/// </summary>
		public byte FilterGradesMode
		{
			get { return m_filterGradesMode; }
			set { m_filterGradesMode = value; }
		}

		/// <summary>
		/// 分配规则
		/// </summary>
		public byte DistributeRule
		{
			get { return m_distributeRule; }
			set { m_distributeRule = value; }
		}

		/// <summary>
		/// 最小分组人数
		/// </summary>
		public short MinDistributeUser
		{
			get { return m_minDistributeUser; }
			set { m_minDistributeUser = value; }
		}

		/// <summary>
		/// 分组时间间隔
		/// </summary>
		public short DistributeTimeSpace
		{
			get { return m_distributeTimeSpace; }
			set { m_distributeTimeSpace = value; }
		}

		/// <summary>
		/// 最小游戏人数
		/// </summary>
		public short MinPartakeGameUser
		{
			get { return m_minPartakeGameUser; }
			set { m_minPartakeGameUser = value; }
		}

		/// <summary>
		/// 最大游戏人数
		/// </summary>
		public short MaxPartakeGameUser
		{
			get { return m_maxPartakeGameUser; }
			set { m_maxPartakeGameUser = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string MatchRule
		{
			get { return m_matchRule; }
			set { m_matchRule = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ServiceMachine
		{
			get { return m_serviceMachine; }
			set { m_serviceMachine = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
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
