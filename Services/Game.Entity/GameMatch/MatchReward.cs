/*
 * 版本：4.0
 * 时间：2016/5/19
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
	/// 实体类 MatchReward。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MatchReward  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MatchReward" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MatchRank = "MatchRank" ;

		/// <summary>
		/// 奖励的游戏币
		/// </summary>
		public const string _RewardGold = "RewardGold" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RewardIngot = "RewardIngot" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RewardExperience = "RewardExperience" ;

		/// <summary>
		/// 奖励描述
		/// </summary>
		public const string _RewardDescibe = "RewardDescibe" ;
		#endregion

		#region 私有变量
		private int m_matchID;					//
		private short m_matchRank;				//
		private long m_rewardGold;				//奖励的游戏币
		private long m_rewardIngot;				//
		private long m_rewardExperience;		//
		private string m_rewardDescibe;			//奖励描述
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MatchReward
		/// </summary>
		public MatchReward()
		{
			m_matchID=0;
			m_matchRank=0;
			m_rewardGold=0;
			m_rewardIngot=0;
			m_rewardExperience=0;
			m_rewardDescibe="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int MatchID
		{
			get { return m_matchID; }
			set { m_matchID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short MatchRank
		{
			get { return m_matchRank; }
			set { m_matchRank = value; }
		}

		/// <summary>
		/// 奖励的游戏币
		/// </summary>
		public long RewardGold
		{
			get { return m_rewardGold; }
			set { m_rewardGold = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long RewardIngot
		{
			get { return m_rewardIngot; }
			set { m_rewardIngot = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long RewardExperience
		{
			get { return m_rewardExperience; }
			set { m_rewardExperience = value; }
		}

		/// <summary>
		/// 奖励描述
		/// </summary>
		public string RewardDescibe
		{
			get { return m_rewardDescibe; }
			set { m_rewardDescibe = value; }
		}
		#endregion
	}
}
