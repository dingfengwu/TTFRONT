using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;

namespace WebApplication1
{
    /// <summary>
    /// PhoneCodeLogin 的摘要说明
    /// </summary>
    public class GetPhoneCode : IHttpHandler
    {
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
        static String sdkappid = "1400116402";
        static String appkey = "e09c2ed4def3ad23d6afd94e143ed4eb";
        static String url
        {
            get
            {
                return "https://yun.tim.qq.com/v5/tlssmssvr/sendsms?sdkappid=1400116402&random=";
            }
        }
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
        protected void Page_Load(HttpContext e)
        {
            RetureCode newRetureCode = new RetureCode();
            try
            {
                Stream sm = e.Request.InputStream;
                StreamReader inputData = new StreamReader(sm);
                string DataString = inputData.ReadToEnd();
                PhoneGetCodeInfo newPhoneLoginInfo = LitJson.JsonMapper.ToObject<PhoneGetCodeInfo>(DataString);
                if (newPhoneLoginInfo.OptionType == 1)
                {
                    if (PhoneBind.HaveBind(newPhoneLoginInfo.PhoneNum, newRetureCode))
                    {
                        newRetureCode.code = 1;
                        newRetureCode.msg = "此手机已经绑定了账号";
                        SendStringToClient(e, newRetureCode);
                        Debug.Log("GetPhoneCode", "此手机已经绑定了账号:" + newPhoneLoginInfo.PhoneNum);
                        return;
                    }
                }
                Send(newPhoneLoginInfo, e);
            }
            catch(Exception exp)
            {
                newRetureCode.code = 100;
                newRetureCode.msg = exp.Message.ToString();
                SendStringToClient(e, newRetureCode);
                Debug.LogException(exp);
            }
        }
        public static void Send(PhoneGetCodeInfo newPhoneLoginInfo, HttpContext page)
        {
            RetureCode newRetureCode = new RetureCode();
            try
            {
                CodeData OldData = PhoneCode.GPhoneCode.FindCode(newPhoneLoginInfo.CountryCode, newPhoneLoginInfo.PhoneNum);
                string Ps = CreatePassWord();
                if (OldData != null)
                    Ps = OldData.CodeNum;
                if (newPhoneLoginInfo != null)
                {
                   if(AliProgram.Send(newPhoneLoginInfo.PhoneNum, Ps))
                    {
                        newRetureCode.code = 0;
                        newRetureCode.msg = Ps;
                        SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
                        CodeData Data = new CodeData();
                        Data.CountryCode = newPhoneLoginInfo.CountryCode;
                        Data.PhoneNum = newPhoneLoginInfo.PhoneNum;
                        Data.CodeNum = Ps;
                        PhoneCode.GPhoneCode.Add(newPhoneLoginInfo.CountryCode + newPhoneLoginInfo.PhoneNum, Data);
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
                    Debug.LogError("GetPhoneCode", LitJson.JsonMapper.ToJson(newRetureCode));
                }
            }
            catch (Exception ep)
            {
                newRetureCode.code = 3;
                SendStringToClient(page, LitJson.JsonMapper.ToJson(newRetureCode));
                Debug.LogException(ep);
            }
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

            Debug.Log("Sig", postData);
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
public class AliProgram
{
    public  static String product = "Dysmsapi";//短信API产品名称
    public static String domain = "dysmsapi.aliyuncs.com";//短信API产品域名
    public static String accessId = "LTAItzteSMpdP2WG";
    public static String accessSecret = "r0Ja8La7yEAX1qOaHMXcMBlIiC6JpL";
    public static String regionIdForPop = "cn-hangzhou";

    public class AliCode
    {
        public string code = "";
    }

//     public string AccessKeyId { get; set; }
//     public string Action { get; set; }
//     public string OutId { get; set; }
//     public long? OwnerId { get; set; }
//     public string PhoneNumbers { get; set; }
//     public string ResourceOwnerAccount { get; set; }
//     public long? ResourceOwnerId { get; set; }
//     public string SignName { get; set; }
//     public string SmsUpExtendCode { get; set; }
//     public string TemplateCode { get; set; }
//     public string TemplateParam { get; set; }

    public static bool Send(string PhoneNumbers, string CheckCode)
    {
//            String product = "Dysmsapi";//短信API产品名称（短信产品名固定，无需修改）
//             String domain = "dysmsapi.aliyuncs.com";//短信API产品域名（接口地址固定，无需修改）
//             String accessKeyId = accessId;//你的accessKeyId，参考本文档步骤2
//             String accessKeySecret = accessSecret;//你的accessKeySecret，参考本文档步骤2
//             IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
//             //IAcsClient client = new DefaultAcsClient(profile);
//             // SingleSendSmsRequest request = new SingleSendSmsRequest();
//             //初始化ascClient,暂时不支持多region（请勿修改）
//             DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
//             IAcsClient acsClient = new DefaultAcsClient(profile);
//             SendSmsRequest request = new SendSmsRequest();
//             try
//             {
//                 //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式，发送国际/港澳台消息时，接收号码格式为00+国际区号+号码，如“0085200000000”
//                 request.PhoneNumbers = PhoneNumbers;
//                 //必填:短信签名-可在短信控制台中找到
//                 request.SignName = "xxxxxxxx";
//                 //必填:短信模板-可在短信控制台中找到，发送国际/港澳台消息时，请使用国际/港澳台短信模版
//                 request.TemplateCode = "SMS_00000001";
//                 //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
//                 request.TemplateParam ="{\"name\":\"Tom\"， \"code\":\"123\"}";
//                 //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
//                 request.OutId = "yourOutId";
//                 //请求失败这里会抛ClientException异常
//                 SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
//                 System.Console.WriteLine(sendSmsResponse.Message);
//             }
//             catch (ServerException e)
//             {
//                 System.Console.WriteLine("Hello World!");
//             }
//             catch (ClientException e)
//             {
//                 System.Console.WriteLine("Hello World!");
//             }
//         }
        IClientProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
        DefaultProfile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
        IAcsClient acsClient = new DefaultAcsClient(profile);
        SendSmsRequest request = new SendSmsRequest();
        request.AccessKeyId = accessId;
        try
        {
//             【TT棋牌】您的验证码是:{0}。如非本人操作，请忽略本短信
            //request.SignName = "上云预发测试";//"管理控制台中配置的短信签名（状态必须是验证通过）"
            //request.TemplateCode = "SMS_71130001";//管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）"
            //request.RecNum = "13567939485";//"接收号码，多个号码可以逗号分隔"
            //request.ParamString = "{\"name\":\"123\"}";//短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。"
            //SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
            request.PhoneNumbers = PhoneNumbers;
            request.SignName = "ttGame";
            request.TemplateCode = "SMS_143863346";

            AliCode newAliCode = new AliCode();
            newAliCode.code = CheckCode;
            request.TemplateParam = LitJson.JsonMapper.ToJson(newAliCode);

            //request.TemplateParam = "{\"code\":\"123\"}"; //string.Format("{\"code\":\"{0}\"}", CheckCode);
            request.OutId = "xxxxxxxx";
            //请求失败这里会抛ClientException异常
            SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
            //System.Console.WriteLine(sendSmsResponse.Message);
            Debug.Log("SMS", LitJson.JsonMapper.ToJson(sendSmsResponse));
            if (sendSmsResponse.Code == "OK")
                return true;
            return false;
        }
        catch (ServerException e)
        {
            Debug.LogError("SMS", e.Message.ToString());
        }
        catch (ClientException e)
        {
            Debug.LogError("SMS", e.Message.ToString());
        }
        return false;
    }
}
