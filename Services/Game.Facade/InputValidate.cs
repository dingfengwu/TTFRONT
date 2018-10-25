using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Kernel;
using Game.Utils;
using System.Text.RegularExpressions;

namespace Game.Facade
{
    /// <summary>
    /// 项目常用输入数据验证
    /// </summary>
    public class InputDataValidate
    {
        /// <summary>
        /// 验证游戏账号格式
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Message CheckingUserNameFormat( string userName )
        {
            Message msg = new Message();
            if( string.IsNullOrEmpty( userName ) )
            {
                msg.Success = false;
                msg.Content = "游戏账号不能为空";
                return msg;
            }
            int userNameLength = Encoding.Default.GetBytes( userName ).Length;
            if( userNameLength > AppConfig.userNameMaxLength || userNameLength < AppConfig.userNameMinLength )
            {
                msg.Success = false;
                msg.Content = "游戏账号长度有误";
                return msg;
            }
            if( !Validate.IsUserName( userName ) )
            {
                msg.Content = "游戏账号只能有字母、数字、下划线、中文组成";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证昵称格式
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public static Message CheckingNickNameFormat( string nickName )
        {
            Message msg = new Message();
            if( string.IsNullOrEmpty( nickName ) )
            {
                msg.Success = false;
                msg.Content = "昵称不能为空";
                return msg;
            }
            int nickNameLength = Encoding.Default.GetBytes( nickName ).Length;
            if( nickNameLength > AppConfig.nickNameMaxLength || nickNameLength < AppConfig.nickNameMinLength )
            {
                msg.Success = false;
                msg.Content = "昵称长度有误";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Message CheckingPasswordFormat( string password )
        {
            Message msg = new Message();
            if( string.IsNullOrEmpty( password ) )
            {
                msg.Success = false;
                msg.Content = "密码不能为空";
                return msg;
            }
            int passwordLength = password.Length;
            if( passwordLength > AppConfig.passwordMaxLength || passwordLength < AppConfig.passwordMinLength )
            {
                msg.Success = false;
                msg.Content = "密码长度有误";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证移动电话号码
        /// </summary>
        /// <param name="mobilePhoneNum"></param>
        /// <returns></returns>
        public static Message CheckingMobilePhoneNumFormat( string mobilePhoneNum, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( mobilePhoneNum ) )
            {
                return msg;
            }
            if( !isAllowNull && string.IsNullOrEmpty( mobilePhoneNum ) )
            {
                msg.Success = false;
                msg.Content = "电话号码不能为空";
                return msg;
            }
            if( !Validate.IsPhoneCode( mobilePhoneNum ) )
            {
                msg.Success = false;
                msg.Content = "移动电话格式不正确";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证真实姓名
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public static Message CheckingRealNameFormat( string realName, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( realName ) )
            {
                return msg;
            }
            if( !isAllowNull && string.IsNullOrEmpty( realName ) )
            {
                msg.Success = false;
                msg.Content = "真实姓名不能为空";
                return msg;
            }
            if( realName.Length > AppConfig.realNameMaxLength )
            {
                msg.Success = false;
                msg.Content = "真实姓名太长";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="IDCard">身份证号码</param>
        /// <param name="IsAllowNull">是否允许为空</param>
        /// <returns></returns>
        public static Message CheckingIDCardFormat( string IDCard, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( IDCard ) )
            {
                return msg;
            }
            if( !Validate.IsIDCard( IDCard ) )
            {
                msg.Success = false;
                msg.Content = "身份证号码格式不正确";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证游戏ID
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="isAllowNull"></param>
        /// <returns></returns>
        public static Message CheckingGameIDFormat( string gameID, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( gameID ) )
            {
                return msg;
            }
            if( !Validate.IsNumeric( gameID ) )
            {
                msg.Success = false;
                msg.Content = "游戏ID格式不正确";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 充值中心参数验证
        /// </summary>
        /// <param name="userName">游戏账号</param>
        /// <param name="reUserName">重复游戏账号</param>
        /// <param name="amount">充值金额</param>
        /// <returns></returns>
        public static Message ChenckingPayParam( string userName, string reUserName, int amount )
        {
            //账号验证
            Message msg = new Message();
            msg = CheckingUserNameFormat( userName );
            if( !msg.Success )
            {
                msg.Content = "请输入正确的游戏账号";
                return msg;
            }
            if( userName != reUserName )
            {
                msg.Success = false;
                msg.Content = "两次输入的账户不一致";
                return msg;
            }

            //金额验证
            if( amount == 0 )
            {
                msg.Success = false;
                msg.Content = "请输入正确的充值金额";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证密码保护答案
        /// </summary>
        /// <param name="answer">密保答案</param>
        /// <isAllowNull>是否允许为空</isAllowNull>
        /// <returns></returns>
        public static Message CheckingProtectAnswer( string answer, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( answer ) )
            {
                return msg;
            }

            int minLength = AppConfig.protectAnswerMinLength;
            int maxLength = AppConfig.protectAnswerMaxLength;
            int length = Encoding.Default.GetBytes( answer ).Length;
            if( string.IsNullOrEmpty( answer ) )
            {
                msg.Success = false;
                msg.Content = "很抱歉！请输入密保答案";
                return msg;
            }
            if( length > maxLength )
            {
                msg.Success = false;
                msg.Content = "很抱歉！密保答案长度太长";
                return msg;
            }
            if( length < minLength )
            {
                msg.Success = false;
                msg.Content = "很抱歉！密保答案长度太短，至少是4个英文字符或者2个中文字";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证密码保护答案
        /// </summary>
        /// <param name="answer">密保答案</param>
        /// <param name="answer">密保答案序号</param>
        /// <isAllowNull>是否允许为空</isAllowNull>
        /// <returns></returns>
        public static Message CheckingProtectAnswer( string answer, int number, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( answer ) )
            {
                return msg;
            }

            int minLength = AppConfig.protectAnswerMinLength;
            int maxLength = AppConfig.protectAnswerMaxLength;
            int length = Encoding.Default.GetBytes( answer ).Length;
            if( string.IsNullOrEmpty( answer ) )
            {
                msg.Success = false;
                msg.Content = string.Format( "很抱歉！请输入密保答案{0}", number );
                return msg;
            }
            if( length > maxLength )
            {
                msg.Success = false;
                msg.Content = string.Format( "很抱歉！密保答案{0}长度太长", number );
                return msg;
            }
            if( length < minLength )
            {
                msg.Success = false;
                msg.Content = string.Format( "很抱歉！密保答案{0}长度太短，至少是4个英文字符或者2个中文字", number );
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证密码保护问题
        /// </summary>
        /// <param name="answer1">问题1</param>
        /// <param name="answer2">问题2</param>
        /// <param name="answer3">问题3</param>
        /// <returns></returns>
        public static Message CheckingProtectQuestion( string question1, string question2, string question3 )
        {
            Message msg = new Message();
            if( question1 == "0" )
            {
                msg.Success = false;
                msg.Content = "请选择密保问题1";
                return msg;
            }
            if( question2 == "0" )
            {
                msg.Success = false;
                msg.Content = "请选择密保问题2";
                return msg;
            }
            if( question3 == "0" )
            {
                msg.Success = false;
                msg.Content = "请选择密保问题3";
                return msg;
            }
            if( question1.Equals( question2 ) || question1.Equals( question3 ) || question2.Equals( question3 ) )
            {
                msg.Success = false;
                msg.Content = "很抱歉，密保问题不能相同";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证QQ号码
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="isAllowNull"></param>
        /// <returns></returns>
        public static Message CheckingQQFormat( string qq, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( qq ) )
            {
                return msg;
            }
            if( !isAllowNull && string.IsNullOrEmpty( qq ) )
            {
                msg.Success = false;
                msg.Content = "QQ号码不能为空";
                return msg;
            }
            if( !string.IsNullOrEmpty( qq ) && !Utils.Validate.IsNumeric( qq ) )
            {
                msg.Success = false;
                msg.Content = "QQ号码格式不对";
                return msg;
            }
            if( qq.Length > AppConfig.qqMaxLength )
            {
                msg.Success = false;
                msg.Content = "QQ号码太长";
                return msg;
            }
            if( qq.Length < AppConfig.qqMinLength )
            {
                msg.Success = false;
                msg.Content = "QQ号码太短";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Message CheckingEmail( string email )
        {
            Message msg = new Message();
            if( string.IsNullOrEmpty( email ) )
            {
                msg.Success = false;
                msg.Content = "邮箱不能为空";
                return msg;
            }
            if( email.Length > 128 )
            {
                msg.Success = false;
                msg.Content = "邮箱太长";
                return msg;
            }
            if( !Validate.IsEmail( email ) )
            {
                msg.Success = false;
                msg.Content = "邮箱格式不正确";
            }
            return msg;
        }

        /// <summary>
        /// 常用备注验证
        /// </summary>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static Message CheckingRemark( string remark, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( remark ) )
            {
                msg.Success = true;
                return msg;
            }
            if( !isAllowNull && string.IsNullOrEmpty( remark ) )
            {
                msg.Success = false;
                msg.Content = "请输入备注信息";
                return msg;
            }
            if( remark.Length > AppConfig.remarkMaxLength )
            {
                msg.Success = false;
                msg.Content = "备注太长，备注最长不能超过" + AppConfig.remarkMaxLength + "个字符";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证申诉号
        /// </summary>
        /// <param name="reportNo"></param>
        /// <returns></returns>
        public static Message CheckingReportNo( string reportNo, bool isAllowNull )
        {
            Message msg = new Message();
            if( isAllowNull && string.IsNullOrEmpty( reportNo ) )
            {
                msg.Success = true;
                return msg;
            }
            if( !isAllowNull && string.IsNullOrEmpty( reportNo ) )
            {
                msg.Success = false;
                msg.Content = "请输入申诉号";
                return msg;
            }
            return msg;
        }
    }
}
