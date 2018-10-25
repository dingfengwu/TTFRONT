/*
 * 版本：4.0
 * 时间：2016/8/19
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
	/// <summary>
	/// 实体类 GameProperty。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameProperty  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameProperty" ;

		/// <summary>
		/// 道具标识
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 道具名字
		/// </summary>
		public const string _Name = "Name" ;

		/// <summary>
		/// 道具类型 1:礼物 2:宝石 3:双卡 4:防身 5:防踢 6:vip 7:大喇叭 8:小喇叭 9:负分清零 10:逃跑 11:礼包（可以拆分）12:金币
		/// </summary>
		public const string _Kind = "Kind" ;

		/// <summary>
		/// PC类型标识
		/// </summary>
		public const string _PTypeID = "PTypeID" ;

		/// <summary>
		/// 手机类型标识
		/// </summary>
		public const string _MTypeID = "MTypeID" ;

		/// <summary>
		/// 道具购买价格-游戏豆
		/// </summary>
		public const string _Cash = "Cash" ;

		/// <summary>
		/// 道具购买价格-金币
		/// </summary>
		public const string _Gold = "Gold" ;

		/// <summary>
		/// 道具购买价格-元宝
		/// </summary>
		public const string _UserMedal = "UserMedal" ;

		/// <summary>
		/// 道具购买价格-魅力值
		/// </summary>
		public const string _LoveLiness = "LoveLiness" ;

		/// <summary>
		/// 道具使用范围，1大厅,2房间,4游戏中，可以并列
		/// </summary>
		public const string _UseArea = "UseArea" ;

		/// <summary>
		/// 道具作用范围，1自己,2除自己玩家,4旁观,可以并列
		/// </summary>
		public const string _ServiceArea = "ServiceArea" ;

		/// <summary>
		/// 支持手机(0:不支持,1:支持)
		/// </summary>
		public const string _SuportMobile = "SuportMobile" ;

		/// <summary>
		/// 道具详细描述
		/// </summary>
		public const string _RegulationsInfo = "RegulationsInfo" ;

		/// <summary>
		/// 使用者增加魅力
		/// </summary>
		public const string _SendLoveLiness = "SendLoveLiness" ;

		/// <summary>
		/// 被使用者增加魅力
		/// </summary>
		public const string _RecvLoveLiness = "RecvLoveLiness" ;

		/// <summary>
		/// 使用增加金币
		/// </summary>
		public const string _UseResultsGold = "UseResultsGold" ;

		/// <summary>
		/// 使用持续时间，单位秒
		/// </summary>
		public const string _UseResultsValidTime = "UseResultsValidTime" ;

		/// <summary>
		/// 使用有效时间内积分倍率
		/// </summary>
		public const string _UseResultsValidTimeScoreMultiple = "UseResultsValidTimeScoreMultiple" ;

		/// <summary>
		/// 是否成为礼包
		/// </summary>
		public const string _UseResultsGiftPackage = "UseResultsGiftPackage" ;

		/// <summary>
		/// 是否推荐
		/// </summary>
		public const string _Recommend = "Recommend" ;

		/// <summary>
		/// 是否下架
		/// </summary>
		public const string _Nullity = "Nullity" ;
		#endregion

		#region 私有变量
		private int m_iD;										//道具标识
		private string m_name;									//道具名字
		private int m_kind;										//道具类型 1:礼物 2:宝石 3:双卡 4:防身 5:防踢 6:vip 7:大喇叭 8:小喇叭 9:负分清零 10:逃跑 11:礼包（可以拆分）12:金币
		private int m_pTypeID;									//PC类型标识
		private int m_mTypeID;									//手机类型标识
		private decimal m_cash;									//道具购买价格-游戏豆
		private long m_gold;									//道具购买价格-金币
		private int m_userMedal;								//道具购买价格-元宝
		private int m_loveLiness;								//道具购买价格-魅力值
		private short m_useArea;								//道具使用范围，1大厅,2房间,4游戏中，可以并列
		private short m_serviceArea;							//道具作用范围，1自己,2除自己玩家,4旁观,可以并列
		private byte m_suportMobile;							//支持手机(0:不支持,1:支持)
		private string m_regulationsInfo;						//道具详细描述
		private long m_sendLoveLiness;							//使用者增加魅力
		private long m_recvLoveLiness;							//被使用者增加魅力
		private long m_useResultsGold;							//使用增加金币
		private long m_useResultsValidTime;						//使用持续时间，单位秒
		private int m_useResultsValidTimeScoreMultiple;			//使用有效时间内积分倍率
		private int m_useResultsGiftPackage;					//是否成为礼包
		private int m_recommend;								//是否推荐
		private byte m_nullity;									//是否下架
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameProperty
		/// </summary>
		public GameProperty()
		{
			m_iD=0;
			m_name="";
			m_kind=0;
			m_pTypeID=0;
			m_mTypeID=0;
			m_cash=0;
			m_gold=0;
			m_userMedal=0;
			m_loveLiness=0;
			m_useArea=0;
			m_serviceArea=0;
			m_suportMobile=0;
			m_regulationsInfo="";
			m_sendLoveLiness=0;
			m_recvLoveLiness=0;
			m_useResultsGold=0;
			m_useResultsValidTime=0;
			m_useResultsValidTimeScoreMultiple=0;
			m_useResultsGiftPackage=0;
			m_recommend=0;
			m_nullity=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 道具标识
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
		}

		/// <summary>
		/// 道具名字
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		/// <summary>
		/// 道具类型 1:礼物 2:宝石 3:双卡 4:防身 5:防踢 6:vip 7:大喇叭 8:小喇叭 9:负分清零 10:逃跑 11:礼包（可以拆分）12:金币
		/// </summary>
		public int Kind
		{
			get { return m_kind; }
			set { m_kind = value; }
		}

		/// <summary>
		/// PC类型标识
		/// </summary>
		public int PTypeID
		{
			get { return m_pTypeID; }
			set { m_pTypeID = value; }
		}

		/// <summary>
		/// 手机类型标识
		/// </summary>
		public int MTypeID
		{
			get { return m_mTypeID; }
			set { m_mTypeID = value; }
		}

		/// <summary>
		/// 道具购买价格-游戏豆
		/// </summary>
		public decimal Cash
		{
			get { return m_cash; }
			set { m_cash = value; }
		}

		/// <summary>
		/// 道具购买价格-金币
		/// </summary>
		public long Gold
		{
			get { return m_gold; }
			set { m_gold = value; }
		}

		/// <summary>
		/// 道具购买价格-元宝
		/// </summary>
		public int UserMedal
		{
			get { return m_userMedal; }
			set { m_userMedal = value; }
		}

		/// <summary>
		/// 道具购买价格-魅力值
		/// </summary>
		public int LoveLiness
		{
			get { return m_loveLiness; }
			set { m_loveLiness = value; }
		}

		/// <summary>
		/// 道具使用范围，1大厅,2房间,4游戏中，可以并列
		/// </summary>
		public short UseArea
		{
			get { return m_useArea; }
			set { m_useArea = value; }
		}

		/// <summary>
		/// 道具作用范围，1自己,2除自己玩家,4旁观,可以并列
		/// </summary>
		public short ServiceArea
		{
			get { return m_serviceArea; }
			set { m_serviceArea = value; }
		}

		/// <summary>
		/// 支持手机(0:不支持,1:支持)
		/// </summary>
		public byte SuportMobile
		{
			get { return m_suportMobile; }
			set { m_suportMobile = value; }
		}

		/// <summary>
		/// 道具详细描述
		/// </summary>
		public string RegulationsInfo
		{
			get { return m_regulationsInfo; }
			set { m_regulationsInfo = value; }
		}

		/// <summary>
		/// 使用者增加魅力
		/// </summary>
		public long SendLoveLiness
		{
			get { return m_sendLoveLiness; }
			set { m_sendLoveLiness = value; }
		}

		/// <summary>
		/// 被使用者增加魅力
		/// </summary>
		public long RecvLoveLiness
		{
			get { return m_recvLoveLiness; }
			set { m_recvLoveLiness = value; }
		}

		/// <summary>
		/// 使用增加金币
		/// </summary>
		public long UseResultsGold
		{
			get { return m_useResultsGold; }
			set { m_useResultsGold = value; }
		}

		/// <summary>
		/// 使用持续时间，单位秒
		/// </summary>
		public long UseResultsValidTime
		{
			get { return m_useResultsValidTime; }
			set { m_useResultsValidTime = value; }
		}

		/// <summary>
		/// 使用有效时间内积分倍率
		/// </summary>
		public int UseResultsValidTimeScoreMultiple
		{
			get { return m_useResultsValidTimeScoreMultiple; }
			set { m_useResultsValidTimeScoreMultiple = value; }
		}

		/// <summary>
		/// 是否成为礼包
		/// </summary>
		public int UseResultsGiftPackage
		{
			get { return m_useResultsGiftPackage; }
			set { m_useResultsGiftPackage = value; }
		}

		/// <summary>
		/// 是否推荐
		/// </summary>
		public int Recommend
		{
			get { return m_recommend; }
			set { m_recommend = value; }
		}

		/// <summary>
		/// 是否下架
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}
		#endregion
	}
}
