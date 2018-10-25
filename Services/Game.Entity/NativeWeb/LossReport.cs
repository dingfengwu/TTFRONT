/*
 * 版本：4.0
 * 时间：2014-4-11
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
	/// 实体类 LossReport。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LossReport  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "LossReport" ;

		/// <summary>
		/// 挂失标识
		/// </summary>
		public const string _ReportID = "ReportID" ;

		/// <summary>
		/// 申诉单号
		/// </summary>
		public const string _ReportNo = "ReportNo" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _GameID = "GameID" ;

		/// <summary>
		/// 登录帐号
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 申诉邮箱
		/// </summary>
		public const string _ReportEmail = "ReportEmail" ;

		/// <summary>
		/// 真实姓名
		/// </summary>
		public const string _Compellation = "Compellation" ;

		/// <summary>
		/// 证件号码
		/// </summary>
		public const string _PassportID = "PassportID" ;

		/// <summary>
		/// 移动电话号码
		/// </summary>
		public const string _MobilePhone = "MobilePhone" ;

		/// <summary>
		/// 固定电话号码
		/// </summary>
		public const string _FixedPhone = "FixedPhone" ;

		/// <summary>
		/// 注册时间
		/// </summary>
		public const string _RegisterDate = "RegisterDate" ;

		/// <summary>
		/// 历史昵称1
		/// </summary>
		public const string _OldNickName1 = "OldNickName1" ;

		/// <summary>
		/// 历史昵称2
		/// </summary>
		public const string _OldNickName2 = "OldNickName2" ;

		/// <summary>
		/// 历史昵称3
		/// </summary>
		public const string _OldNickName3 = "OldNickName3" ;

		/// <summary>
		/// 历史密码1
		/// </summary>
		public const string _OldLogonPass1 = "OldLogonPass1" ;

		/// <summary>
		/// 历史密码2
		/// </summary>
		public const string _OldLogonPass2 = "OldLogonPass2" ;

		/// <summary>
		/// 历史密码3
		/// </summary>
		public const string _OldLogonPass3 = "OldLogonPass3" ;

		/// <summary>
		/// 密保问题
		/// </summary>
		public const string _OldQuestion1 = "OldQuestion1" ;

		/// <summary>
		/// 密保答案
		/// </summary>
		public const string _OldResponse1 = "OldResponse1" ;

		/// <summary>
		/// 密保问题
		/// </summary>
		public const string _OldQuestion2 = "OldQuestion2" ;

		/// <summary>
		/// 密保答案
		/// </summary>
		public const string _OldResponse2 = "OldResponse2" ;

		/// <summary>
		/// 密保问题
		/// </summary>
		public const string _OldQuestion3 = "OldQuestion3" ;

		/// <summary>
		/// 密保答案
		/// </summary>
		public const string _OldResponse3 = "OldResponse3" ;

		/// <summary>
		/// 补充材料
		/// </summary>
		public const string _SuppInfo = "SuppInfo" ;

		/// <summary>
		/// 处理状态(0:未处理,1:发送成功邮件,2:发送失败邮件,3:更新密码成功)
		/// </summary>
		public const string _ProcessStatus = "ProcessStatus" ;

		/// <summary>
		/// 发送次数
		/// </summary>
		public const string _SendCount = "SendCount" ;

		/// <summary>
		/// 签名随机数
		/// </summary>
		public const string _Random = "Random" ;

		/// <summary>
		/// 解决日期
		/// </summary>
		public const string _SolveDate = "SolveDate" ;

		/// <summary>
		/// 用户处理时间
		/// </summary>
		public const string _OverDate = "OverDate" ;

		/// <summary>
		/// 申诉地址
		/// </summary>
		public const string _ReportIP = "ReportIP" ;

		/// <summary>
		/// 申诉日期
		/// </summary>
		public const string _ReportDate = "ReportDate" ;
		#endregion

		#region 私有变量
		private int m_reportID;					//挂失标识
		private string m_reportNo;				//申诉单号
		private int m_userID;					//用户标识
		private int m_gameID;					//游戏标识
		private string m_accounts;				//登录帐号
		private string m_reportEmail;			//申诉邮箱
		private string m_compellation;			//真实姓名
		private string m_passportID;			//证件号码
		private string m_mobilePhone;			//移动电话号码
		private string m_fixedPhone;			//固定电话号码
		private string m_registerDate;			//注册时间
		private string m_oldNickName1;			//历史昵称1
		private string m_oldNickName2;			//历史昵称2
		private string m_oldNickName3;			//历史昵称3
		private string m_oldLogonPass1;			//历史密码1
		private string m_oldLogonPass2;			//历史密码2
		private string m_oldLogonPass3;			//历史密码3
		private string m_oldQuestion1;			//密保问题
		private string m_oldResponse1;			//密保答案
		private string m_oldQuestion2;			//密保问题
		private string m_oldResponse2;			//密保答案
		private string m_oldQuestion3;			//密保问题
		private string m_oldResponse3;			//密保答案
		private string m_suppInfo;				//补充材料
		private byte m_processStatus;			//处理状态(0:未处理,1:发送成功邮件,2:发送失败邮件,3:更新密码成功)
		private int m_sendCount;				//发送次数
		private string m_random;				//签名随机数
		private DateTime m_solveDate;			//解决日期
		private DateTime m_overDate;			//用户处理时间
		private string m_reportIP;				//申诉地址
		private DateTime m_reportDate;			//申诉日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化LossReport
		/// </summary>
		public LossReport()
		{
			m_reportID=0;
			m_reportNo="";
			m_userID=0;
			m_gameID=0;
			m_accounts="";
			m_reportEmail="";
			m_compellation="";
			m_passportID="";
			m_mobilePhone="";
			m_fixedPhone="";
			m_registerDate="";
			m_oldNickName1="";
			m_oldNickName2="";
			m_oldNickName3="";
			m_oldLogonPass1="";
			m_oldLogonPass2="";
			m_oldLogonPass3="";
			m_oldQuestion1="";
			m_oldResponse1="";
			m_oldQuestion2="";
			m_oldResponse2="";
			m_oldQuestion3="";
			m_oldResponse3="";
			m_suppInfo="";
			m_processStatus=0;
			m_sendCount=0;
			m_random="";
			m_solveDate=DateTime.Now;
			m_overDate=DateTime.Now;
			m_reportIP="";
			m_reportDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 挂失标识
		/// </summary>
		public int ReportID
		{
			get { return m_reportID; }
			set { m_reportID = value; }
		}

		/// <summary>
		/// 申诉单号
		/// </summary>
		public string ReportNo
		{
			get { return m_reportNo; }
			set { m_reportNo = value; }
		}

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 游戏标识
		/// </summary>
		public int GameID
		{
			get { return m_gameID; }
			set { m_gameID = value; }
		}

		/// <summary>
		/// 登录帐号
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 申诉邮箱
		/// </summary>
		public string ReportEmail
		{
			get { return m_reportEmail; }
			set { m_reportEmail = value; }
		}

		/// <summary>
		/// 真实姓名
		/// </summary>
		public string Compellation
		{
			get { return m_compellation; }
			set { m_compellation = value; }
		}

		/// <summary>
		/// 证件号码
		/// </summary>
		public string PassportID
		{
			get { return m_passportID; }
			set { m_passportID = value; }
		}

		/// <summary>
		/// 移动电话号码
		/// </summary>
		public string MobilePhone
		{
			get { return m_mobilePhone; }
			set { m_mobilePhone = value; }
		}

		/// <summary>
		/// 固定电话号码
		/// </summary>
		public string FixedPhone
		{
			get { return m_fixedPhone; }
			set { m_fixedPhone = value; }
		}

		/// <summary>
		/// 注册时间
		/// </summary>
		public string RegisterDate
		{
			get { return m_registerDate; }
			set { m_registerDate = value; }
		}

		/// <summary>
		/// 历史昵称1
		/// </summary>
		public string OldNickName1
		{
			get { return m_oldNickName1; }
			set { m_oldNickName1 = value; }
		}

		/// <summary>
		/// 历史昵称2
		/// </summary>
		public string OldNickName2
		{
			get { return m_oldNickName2; }
			set { m_oldNickName2 = value; }
		}

		/// <summary>
		/// 历史昵称3
		/// </summary>
		public string OldNickName3
		{
			get { return m_oldNickName3; }
			set { m_oldNickName3 = value; }
		}

		/// <summary>
		/// 历史密码1
		/// </summary>
		public string OldLogonPass1
		{
			get { return m_oldLogonPass1; }
			set { m_oldLogonPass1 = value; }
		}

		/// <summary>
		/// 历史密码2
		/// </summary>
		public string OldLogonPass2
		{
			get { return m_oldLogonPass2; }
			set { m_oldLogonPass2 = value; }
		}

		/// <summary>
		/// 历史密码3
		/// </summary>
		public string OldLogonPass3
		{
			get { return m_oldLogonPass3; }
			set { m_oldLogonPass3 = value; }
		}

		/// <summary>
		/// 密保问题
		/// </summary>
		public string OldQuestion1
		{
			get { return m_oldQuestion1; }
			set { m_oldQuestion1 = value; }
		}

		/// <summary>
		/// 密保答案
		/// </summary>
		public string OldResponse1
		{
			get { return m_oldResponse1; }
			set { m_oldResponse1 = value; }
		}

		/// <summary>
		/// 密保问题
		/// </summary>
		public string OldQuestion2
		{
			get { return m_oldQuestion2; }
			set { m_oldQuestion2 = value; }
		}

		/// <summary>
		/// 密保答案
		/// </summary>
		public string OldResponse2
		{
			get { return m_oldResponse2; }
			set { m_oldResponse2 = value; }
		}

		/// <summary>
		/// 密保问题
		/// </summary>
		public string OldQuestion3
		{
			get { return m_oldQuestion3; }
			set { m_oldQuestion3 = value; }
		}

		/// <summary>
		/// 密保答案
		/// </summary>
		public string OldResponse3
		{
			get { return m_oldResponse3; }
			set { m_oldResponse3 = value; }
		}

		/// <summary>
		/// 补充材料
		/// </summary>
		public string SuppInfo
		{
			get { return m_suppInfo; }
			set { m_suppInfo = value; }
		}

		/// <summary>
		/// 处理状态(0:未处理,1:发送成功邮件,2:发送失败邮件,3:更新密码成功)
		/// </summary>
		public byte ProcessStatus
		{
			get { return m_processStatus; }
			set { m_processStatus = value; }
		}

		/// <summary>
		/// 发送次数
		/// </summary>
		public int SendCount
		{
			get { return m_sendCount; }
			set { m_sendCount = value; }
		}

		/// <summary>
		/// 签名随机数
		/// </summary>
		public string Random
		{
			get { return m_random; }
			set { m_random = value; }
		}

		/// <summary>
		/// 解决日期
		/// </summary>
		public DateTime SolveDate
		{
			get { return m_solveDate; }
			set { m_solveDate = value; }
		}

		/// <summary>
		/// 用户处理时间
		/// </summary>
		public DateTime OverDate
		{
			get { return m_overDate; }
			set { m_overDate = value; }
		}

		/// <summary>
		/// 申诉地址
		/// </summary>
		public string ReportIP
		{
			get { return m_reportIP; }
			set { m_reportIP = value; }
		}

		/// <summary>
		/// 申诉日期
		/// </summary>
		public DateTime ReportDate
		{
			get { return m_reportDate; }
			set { m_reportDate = value; }
		}
		#endregion
	}
}
