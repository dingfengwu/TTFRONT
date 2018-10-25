using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Game.Web
{
    public class BankPayRequest
    {
        private Dictionary<string, object> _params;

        public BankPayRequest()
        {
            _params = new Dictionary<string, object>();
        }

        public void AddParams(string key,object value)
        {
            _params.Add(key, value);
        }

        public string ToParams()
        {
            var data = new StringBuilder();
            foreach(var param in _params)
            {
                data.Append(string.Format("{0}={1}", param.Key, param.Value));
                data.Append("&");
            }
            if (data.Length > 0)
                data.Remove(data.Length - 1, 1);

            return data.ToString();
        }

        public string GetSign(string paySecret)
        {
            var data = new StringBuilder();
            var list = _params.Where(p => p.Value != null && p.Value.ToString() != string.Empty).OrderBy(p => p.Key).ToList();
            foreach(var item in list)
            {
                data.Append(string.Format("{0}={1}", item.Key, item.Value));
                data.Append("&");
            }
            data.Append(string.Format("paySecret={0}", paySecret));

            var md5 = new MD5CryptoServiceProvider();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data.ToString()));
            return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
        }
    }
}