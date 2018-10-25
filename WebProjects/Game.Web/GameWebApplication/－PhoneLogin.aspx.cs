using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication
{
    public partial class PhoneLogin : System.Web.UI.Page
    {
        public class RetureCode
        {
            public int code = 1;
        }
        static String accesskey = "5afd2ed80cf2cc2100449ac1";
        static String secretkey = "fcf5bad3dead4fd0ae465b473c4c9a9b";
        static String url = "https://live.kewail.com/sms/v1/sendsinglesms";
        static System.Random rd = new Random();
        static string CreatePassWord()
        {
            string str = "";
            for (int i = 0; i < 6; i++)
            {
                str += (char)(((int)'0') + rd.Next(10));
            }
            return str;
        }
        public static string GetMD5Hash(string fileName)
        {
            try
            {
                MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(fileName));
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(ms);
                ms.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }  
        public static void SendStringToClient(System.Web.UI.Page Page, string Str)
        {
            if (Page == null)
                return;
            Page.Response.AddHeader("Access-Control-Allow-Origin", "*");
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Str);
            Page.Response.ContentType = "text/plain;charset=UTF-8";
            Page.Response.OutputStream.Write(bytes, 0, bytes.Length);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Stream sm = Request.GetBufferedInputStream();
            StreamReader inputData = new StreamReader(sm);
            string DataString = inputData.ReadToEnd();
            PhoneGetCodeInfo newPhoneLoginInfo = LitJson.JsonMapper.ToObject<PhoneGetCodeInfo>(DataString);
            Send(newPhoneLoginInfo, this);
        }

        public static void Send(PhoneGetCodeInfo newPhoneLoginInfo, System.Web.UI.Page page)
        {
            RetureCode newRetureCode = new RetureCode();
            try
            {
                string Ps = CreatePassWord();
                if (newPhoneLoginInfo != null)
                {
                    String singleSenderResult = send(0, newPhoneLoginInfo.CountryCode, newPhoneLoginInfo.PhoneNum,
                   "【Kewail科技】尊敬的用户：您的验证码：" + Ps + "，工作人员不会索取，请勿泄漏。", "", "");
                    if (singleSenderResult.Contains("\"errmsg\":\"OK\""))
                    {
                        Ps = GetMD5Hash(Ps);
                        AddAccount(newPhoneLoginInfo.PhoneNum, Ps, newPhoneLoginInfo.Mac);
                        newRetureCode.code = 0;
                        SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
                    }
                    else
                    {
                        newRetureCode.code = 1;
                        SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
                    }
                }
                else
                {
                    newRetureCode.code = 2;
                    SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
                }
            }
            catch (Exception ep)
            {
                newRetureCode.code = 3;
                SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
            }
        }
        public static bool AddAccount(string PhoneName, string Password, string Mac)
        {
            string insetsql = "";
            {
                {
                     insetsql = string.Format(@"INSERT INTO [RYAccountsDB].[dbo].[AccountsInfo]
           ([ProtectID]
           ,[PasswordID]
           ,[SpreaderID]
           ,[Accounts]
           ,[NickName]
           ,[RegAccounts]
           ,[UnderWrite]
           ,[PassPortID]
           ,[Compellation]
           ,[LogonPass]
           ,[InsurePass]
           ,[DynamicPass]
           ,[DynamicPassTime]
           ,[FaceID]
           ,[CustomID]
           ,[Present]
           ,[UserMedal]
           ,[Experience]
           ,[GrowLevelID]
           ,[LoveLiness]
           ,[UserRight]
           ,[MasterRight]
           ,[ServiceRight]
           ,[MasterOrder]
           ,[MemberOrder]
           ,[MemberOverDate]
           ,[MemberSwitchDate]
           ,[CustomFaceVer]
           ,[Gender]
           ,[Nullity]
           ,[NullityOverDate]
           ,[StunDown]
           ,[MoorMachine]
           ,[IsAndroid]
           ,[WebLogonTimes]
           ,[GameLogonTimes]
           ,[PlayTimeCount]
           ,[OnLineTimeCount]
           ,[LastLogonIP]
           ,[LastLogonDate]
           ,[LastLogonMobile]
           ,[LastLogonMachine]
           ,[RegisterIP]
           ,[RegisterDate]
           ,[RegisterMobile]
           ,[RegisterMachine]
           ,[RegisterOrigin]
           ,[PlatformID]
           ,[UserUin]
           ,[RankID]
           ,[AgentID])
     VALUES(
           0,
           0,
           0,
           '{0}',
           '{1}',
           '',
           '',
           '',
           '',
           {2},
           '',
           '',
           '',
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           0,
           '',
           '',
           '',
           '',
           '',
           '',
           '{0}',
           '{3}',
           0,
           0,
           '',
           0,
           0)", PhoneName, PhoneName, Password, Mac);

                }
            }
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBAccounts"];
//             string MyConn = "server=103.105.58.140;uid=testdb;pwd=123abc;database=RYAccountsDB;Trusted_Connection=no";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
                {
                    string selStr = "select Accounts from AccountsInfo where Accounts='" + PhoneName + "'";
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (!_Reader.HasRows)
                    {
                        _Reader.Close();
                    }
                    else
                    {
                        _Reader.Close();
                        string updateSql = " update AccountsInfo set LogonPass='" + Password + "'";
                        updateSql += " where Accounts='" + PhoneName + "'";
                        SqlCommand UpdateCommand = new SqlCommand(updateSql, MyConnection);
                        UpdateCommand.ExecuteNonQuery();
                        return true;
                    }
                }
                {
                    SqlCommand UpdateCommand = new SqlCommand(insetsql, MyConnection);
                    UpdateCommand.ExecuteNonQuery();
                }
                MyConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            return false;
        }


        /// <summary>
        /// 普通单发短信接口，明确指定内容，如果有多个签名，请在内容中以【】的方式添加到信息内容中，否则系统将使用默认签名
        /// </summary>
        /// <param name="type">短信类型，0 为普通短信，1 营销短信</param>
        ///<param name="nationCode">国家码，如 86 为中国</param>
        /// <param name="phoneNumber">不带国家码的手机号</param>
        /// <param name="msg">信息内容，必须与申请的模板格式一致，否则将返回错误</param>
        /// <param name="extend">扩展码，可填空</param>
        /// <param name="ext">服务端原样返回的参数，可填空</param>
        /// <returns>返回发送结果字符串</returns>
        private static String send(int type, String nationCode, String phoneNumber, String msg, String extend, String ext)
        {
            // 校验 type 类型
            if (0 != type && 1 != type)
            {
                throw new Exception("type " + type + " error");
            }
            if (null == extend)
            {
                extend = "";
            }
            if (null == ext)
            {
                ext = "";
            }
            long random = getRandom();
            String curTime = getCurTime();

            String wholeUrl = url + "?accesskey=" + accesskey + "&random=" + random;

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(wholeUrl);


            string sig = "secretkey=" + secretkey + "&random=" + random + "&time=" + curTime + "&mobile=" + phoneNumber;
            sig = SHA256Encrypt(sig);


            Encoding encoding = new UTF8Encoding();
            String tel = "{\"nationcode\":\"" + nationCode + "\",\"mobile\":\"" + phoneNumber + "\"}";
            string postData = "{\"type\":\"" + type + "\",\"msg\":\"" + msg + "\",\"sig\":\"" + sig + "\",\"tel\":" + tel + ",\"time\":\"" + curTime + "\",\"extend\":\"" + extend + "\",\"ext\":\"" + ext + "\"}";
            byte[] data = encoding.GetBytes(postData);

            httpWReq.ProtocolVersion = HttpVersion.Version11;
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/json";//charset=UTF-8";
            httpWReq.Headers.Add("X-Amzn-Type-Version",
                                                "com.amazon.device.messaging.ADMMessage@1.0");
            httpWReq.Headers.Add("X-Amzn-Accept-Type",
                                            "com.amazon.device.messaging.ADMSendResult@1.0");
            // httpWReq.Headers.Add(HttpRequestHeader.Authorization,
            //"Bearer " + accessToken);
            httpWReq.ContentLength = data.Length;

            Stream stream = httpWReq.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            string s = response.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String jsonresponse = "";
            String temp = null;
            while ((temp = reader.ReadLine()) != null)
            {
                jsonresponse += temp;
            }
            return jsonresponse;
        }
        public static long getRandom()
        {
            Random random = new Random();
            return (random.Next(999999)) % 900000 + 100000;
        }

        /// <summary>    
        /// 获取时间戳    
        /// </summary>  
        public static string getCurTime()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// SHA256加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA256Encrypt(string str)
        {
            //System.Security.Cryptography.SHA256 s256 = new System.Security.Cryptography.SHA256Managed();
            //byte[] byte1;
            //byte1 = s256.ComputeHash(Encoding.UTF8.GetBytes(str));
            //s256.Clear();
            //return Convert.ToBase64String(byte1);

            byte[] bytes = Encoding.UTF8.GetBytes(str);
            HashAlgorithm algorithm = null;
            algorithm = new SHA256Managed();
            return BitConverter.ToString(algorithm.ComputeHash(bytes)).Replace("-", "").ToUpper();
        }
    }
}
public class PhoneGetCodeInfo
{
    public int OptionType = 0;  //０登录　１绑定
    public string CountryCode = "86";
    public string PhoneNum = "123456789";
    public string Mac = "OOIJJJJFFJFJSS";
}
public class CheckPhoneLoginInfo
{
    public string CountryCode = "86";
    public string PhoneNum = "123456789";
    public string Mac = "OOIJJJJFFJFJSS";
    public string PhoneCode = "";
}
public class BindPhoneNum
{
    public string CountryCode = "86";
    public int AccountId = 0;
    public string PhoneNum = "123456789";
    public string Mac = "OOIJJJJFFJFJSS";
    public string PhoneCode = "";
}
public class JsonEMail
{
    public int dwUserID;    // 用户 I D
    public int dwMail;      //邮件ID
    public string szTitle = "邮件名称";  // 邮件名称	
    public int nType;       // 邮件类型
    public int nStatus;      // 邮件状态
    public string szSendTime=""; //收件时间　
    public string szMessage="";// 邮件消息
    public string szSender="";  //发件人
}
//
public class UserAccount
{
    public string Mac = "OOIJJJJFFJFJSS";
    public int dwUserID;    // 用户 I D
    public int dwMailId;    //邮件ID
    public int nType;       // 邮件类型
    public int nStatus;     // 邮件状态
}
public class MailUserAccount
{
    public string Mac = "OOIJJJJFFJFJSS";
    public int dwUserID;    // 用户 I D
    public List<int> dwMailIds = new List<int>();    //邮件ID
    public int nType;       // 邮件类型
    public int nStatus;     // 邮件状态
}
