using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Phone 的摘要说明
    /// </summary>
    public class Phone : IHttpHandler
    {
        public static System.Random rd = new Random();

        public static void SendStringToClient(HttpContext hd, System.Object obj)
        {
            hd.Response.ContentType = "text/plain;charset=UTF-8";
            hd.Response.Write(LitJson.JsonMapper.ToJson(obj));
        }
        public static void SendStringToClient(HttpContext hd, string obj)
        {
            hd.Response.ContentType = "text/plain;charset=UTF-8";
            hd.Response.Write(obj);
        }
        public void ProcessRequest(HttpContext context)
        {
            Page_Load(context);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        
        public class RetureCode
        {
            public int code = 1;
            public string msg = "";
        }
        static String sdkappid = "1400116402";
        static String appkey = "e09c2ed4def3ad23d6afd94e143ed4eb";
        static String url
        {
            get
            {
                return "https://yun.tim.qq.com/v5/tlssmssvr/sendsms?sdkappid=1400116402&random=";
            }
        }
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
//         public static void SendStringToClient(System.Web.UI.Page Page, string Str)
//         {
//             if (Page == null)
//                 return;
//             Page.Response.AddHeader("Access-Control-Allow-Origin", "*");
//             byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Str);
//             Page.Response.ContentType = "text/plain;charset=UTF-8";
//             Page.Response.OutputStream.Write(bytes, 0, bytes.Length);
//         }
        protected void Page_Load(HttpContext e)
        {
            try
            {
                Stream sm = e.Request.InputStream;
                StreamReader inputData = new StreamReader(sm);
                string DataString = inputData.ReadToEnd();
                PhoneGetCodeInfo newPhoneLoginInfo = LitJson.JsonMapper.ToObject<PhoneGetCodeInfo>(DataString);
                Send(newPhoneLoginInfo, e);
            }
            catch (Exception exp)
            {
                SendStringToClient(e, exp.Message.ToString()+"-"+exp.StackTrace.ToString());
            }
        }
        public static void Send(PhoneGetCodeInfo newPhoneLoginInfo, HttpContext page)
        {
            RetureCode newRetureCode = new RetureCode();
            try
            {
                string Ps = CreatePassWord();
                if (newPhoneLoginInfo != null)
                {
                    string str = string.Format("【TT棋牌】您的验证码是:{0}。如非本人操作，请忽略本短信", Ps);
                    String singleSenderResult = send(0, newPhoneLoginInfo.CountryCode, newPhoneLoginInfo.PhoneNum,
                    str, "", "");
                    if (singleSenderResult.Contains("\"errmsg\":\"OK\""))
                    {
                        Ps = GetMD5Hash(Ps);
                        if (AddAccount(newPhoneLoginInfo.PhoneNum, Ps, newPhoneLoginInfo.Mac, newRetureCode))
                             newRetureCode.code = 0;
                        else
                            newRetureCode.code = 4;
                        SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
                    }
                    else
                    {
                        newRetureCode.code = 1;
                        SendStringToClient(page, singleSenderResult);
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
                SendStringToClient(page, ep.Message.ToString()+ep.StackTrace.ToString());
            }
        }
        public static bool AddAccount(string PhoneName, string Password, string Mac, RetureCode newRetureCode)
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
           '{2}',
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
            //string MyConn = "Data Source=.; Initial Catalog=RYAccountsDB; User ID=sa_games; Password=sa_gamesluatest; Pooling=true";
            //string MyConn = "server=103.105.58.140;uid=testdb;pwd=123abc;database=RYAccountsDB;Trusted_Connection=no";
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
                        return UpdateCommand.ExecuteNonQuery() > 0;
                    }
                }
                {
                    SqlCommand UpdateCommand = new SqlCommand(insetsql, MyConnection);

                    return UpdateCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                newRetureCode.msg = ex.Message.ToString();
                Console.WriteLine("{0} Exception caught.", ex);
            }
            finally
            {
                if (MyConnection!=null)
                    MyConnection.Close();
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
            long random = qcloudsms_csharp.SmsSenderUtil.getRandom();
            long curTime = qcloudsms_csharp.SmsSenderUtil.getCurrentTime();

            String wholeUrl = url + random;// url + "?accesskey=" + accesskey + "&random=" + random;

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(wholeUrl);

//                     public static string calculateSignature(string appkey, long random, long time,
//             string phoneNumber)

            string sig = qcloudsms_csharp.SmsSenderUtil.calculateSignature(appkey, random, curTime, phoneNumber);
//             string sig = "secretkey=" + secretkey + "&random=" + random + "&time=" + curTime + "&mobile=" + phoneNumber;
//             sig = SHA256Encrypt(sig);
// string postData = string.Format(@"{
//     ""ext"": """",
//     ""extend"": """",
//     ""msg"": ""您的验证码是{0}"",
//     ""sig"": ""{1}"",
//     ""tel"": {
//         ""mobile"": ""{2}"",
//         ""nationcode"": ""{3}""
//     },
//     ""time"": {4},
//     ""type"": 0
// }", 123, sig, phoneNumber, nationCode, curTime);

            Encoding encoding = new UTF8Encoding();
            String tel = "{\"nationcode\":\"" + nationCode + "\",\"mobile\":\"" + phoneNumber + "\"}";
            string postData = "{\"type\":\"" + type + "\",\"msg\":\"" + msg + "\",\"sig\":\"" + sig + "\",\"tel\":" + tel + ",\"time\":\"" + curTime + "\",\"extend\":\"" + extend + "\",\"ext\":\"" + ext + "\"}";
            byte[] data = encoding.GetBytes(postData);

            Debug.LogError("Sig", postData);
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