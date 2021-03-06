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
	/// 实体类 ConfineMachine。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ConfineMachine  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ConfineMachine" ;

		/// <summary>
		/// 机器序列
		/// </summary>
		public const string _MachineSerial = "MachineSerial" ;

		/// <summary>
		/// 限制登录
		/// </summary>
		public const string _EnjoinLogon = "EnjoinLogon" ;

		/// <summary>
		/// 限制注册
		/// </summary>
		public const string _EnjoinRegister = "EnjoinRegister" ;

		/// <summary>
		/// 过期时间
		/// </summary>
		public const string _EnjoinOverDate = "EnjoinOverDate" ;

		/// <summary>
		/// 收集日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 输入备注
		/// </summary>
		public const string _CollectNote = "CollectNote" ;
		#endregion

		#region 私有变量
		private string m_machineSerial;			//机器序列
		private bool m_enjoinLogon;				//限制登录
		private bool m_enjoinRegister;			//限制注册
		private DateTime m_enjoinOverDate;		//过期时间
		private DateTime m_collectDate;			//收集日期
		private string m_collectNote;			//输入备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ConfineMachine
		/// </summary>
		public ConfineMachine()
		{
			m_machineSerial="";
			m_enjoinLogon=false;
			m_enjoinRegister=false;
			m_enjoinOverDate=DateTime.Now;
			m_collectDate=DateTime.Now;
			m_collectNote="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 机器序列
		/// </summary>
		public string MachineSerial
		{
			get { return m_machineSerial; }
			set { m_machineSerial = value; }
		}

		/// <summary>
		/// 限制登录
		/// </summary>
		public bool EnjoinLogon
		{
			get { return m_enjoinLogon; }
			set { m_enjoinLogon = value; }
		}

		/// <summary>
		/// 限制注册
		/// </summary>
		public bool EnjoinRegister
		{
			get { return m_enjoinRegister; }
			set { m_enjoinRegister = value; }
		}

		/// <summary>
		/// 过期时间
		/// </summary>
		public DateTime EnjoinOverDate
		{
			get { return m_enjoinOverDate; }
			set { m_enjoinOverDate = value; }
		}

		/// <summary>
		/// 收集日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 输入备注
		/// </summary>
		public string CollectNote
		{
			get { return m_collectNote; }
			set { m_collectNote = value; }
		}
		#endregion
	}
}
