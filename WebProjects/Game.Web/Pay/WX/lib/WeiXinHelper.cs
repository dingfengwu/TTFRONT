using Game.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace Game.Web.Pay.WX
{
    public class WeiXinHelper
    {
        //公众账号ID
        private static string appid
        {
            get
            {
                return ApplicationSettings.Get("WXNATIVEAPPID");
            }
        }
        //商户号
        private static string mch_id
        {
            get
            {
                return ApplicationSettings.Get("WXNATIVEMCHID");
            }
        }
        //密钥
        private static string key
        {
            get
            {
                return ApplicationSettings.Get("WXNATIVEKEY");
            }
        }
        //设备号
        private static string device_info = "WEB";
        //交易类型
        private static string trade_type = "NATIVE";
        //统一下单地址
        private static string orderurl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        //订单查询地址
        private static string orderquery = "https://api.mch.weixin.qq.com/pay/orderquery";

        /// <summary>
        /// 将SortedDictionary数据集转换成xml
        /// </summary>
        /// <param name="m_values">数据集</param>
        /// <returns></returns>
        public static string WxPayDataToXml(SortedDictionary<string, object> m_values)
        {
            StringBuilder xml = new StringBuilder("<xml>");
            if (m_values.Count > 0)
            {
                foreach (KeyValuePair<string, object> pair in m_values)
                {
                    if (pair.Value != null && pair.Value.GetType() == typeof(int))
                    {
                        xml.AppendFormat("<{0}>{1}</{2}>", pair.Key, pair.Value, pair.Key);
                    }
                    else if (pair.Value != null && pair.Value.GetType() == typeof(string))
                    {
                        xml.AppendFormat("<{0}><![CDATA[{1}]]></{2}>", pair.Key, pair.Value, pair.Key);
                    }
                }
            }
            xml.Append("</xml>");
            return xml.ToString();
        }

        /// <summary>
        /// 将xml数据结果转化成SortedDictionary数据集
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> WxPayDataFromXml(string xml)
        {
            SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                dic[xe.Name] = xe.InnerText;
            }
            return dic;
        }

        public static SortedDictionary<string, object> UnifiedOrder(SortedDictionary<string, object> dic, int timeOut = 6)
        {
            dic.Add("appid", appid);
            dic.Add("mch_id", mch_id);
            dic.Add("device_info", device_info);
            dic.Add("sign", GetMakeSign(dic));
            string xml = WxPayDataToXml(dic);
            string response = Post(xml, orderurl, false, timeOut);
            return WxPayDataFromXml(response);
        }
        public static SortedDictionary<string, object> UnifiedOrderAPP(SortedDictionary<string, object> dic, string appkey, int timeOut = 6)
        {
            dic.Add("device_info", device_info);
            dic.Add("nonce_str", GetNonce_str());
            dic.Add("sign", GetMakeSignAPP(dic, appkey));
            string xml = WxPayDataToXml(dic);
            string response = Post(xml, orderurl, false, timeOut);
            return WxPayDataFromXml(response);
        }

        public static string GetMakeSign(SortedDictionary<string, object> dic)
        {
            //转url格式
            string str = ToUrl(dic);
            //在string后加入API KEY
            str += "&key=" + key;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
        public static string GetMakeSignAPP(SortedDictionary<string, object> dic, string appkey)
        {
            //转url格式
            string str = ToUrl(dic);
            //在string后加入API KEY
            str += "&key=" + appkey;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public static string ToUrl(SortedDictionary<string, object> m_values)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value != null && pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }
        public static string Post(string xml, string url, bool isUseCert, int timeout)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        public static string GetNonce_str()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// 获取返回数据
        /// </summary>
        /// <returns></returns>
        public static SortedDictionary<string, object> GetReturnData()
        {
            SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
            StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlData = reader.ReadToEnd();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlData);
            XmlNode oNode = xml.DocumentElement;
            XmlNodeList oList = oNode.ChildNodes;
            foreach (XmlNode item in oList)
            {
                dic.Add(item.Name, item.InnerText);
            }
            return dic;
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> OrderQuery(SortedDictionary<string, object> dic, int timeOut = 6)
        {
            dic.Add("appid", appid);
            dic.Add("mch_id", mch_id);
            dic.Add("nonce_str", GetNonce_str());
            dic.Add("sign", GetMakeSign(dic));
            string xml = WxPayDataToXml(dic);
            string response = Post(xml, orderquery, false, timeOut);
            return WxPayDataFromXml(response);
        }
    }
}