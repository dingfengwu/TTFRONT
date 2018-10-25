using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

namespace Game.Web.Pay.WX
{
    /// <summary>
    /// APP支付业务层
    /// </summary>
    public class XZAppPay
    {
        #region Fields

        private Dictionary<string, string> parameters;
        #endregion

        #region 构造函数

        public XZAppPay()
        {
            this.parameters = new Dictionary<string, string>();            
        }
        #endregion    

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value_ren"></param>
        public void SetParameter(string key, string value_ren)
        {
            parameters.Add(key, value_ren);
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetParameter(string key)
        {
            return parameters[key];
        }

        /// <summary>
        /// 统一下单接口返回正常的prepay_id，再按签名规范重新生成签名后，将数据传输给APP
        /// </summary>
        /// <returns></returns>
        public string GetPrepayIDSign()
        {
            WxPayData data = new WxPayData();
            data.SetValue("body", parameters["body"]);//商品描述
            data.SetValue("attach", parameters["body"]);//附加数据
            data.SetValue("out_trade_no", parameters["out_trade_no"]);//随机字符串
            data.SetValue("total_fee", parameters["total_fee"]);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("trade_type", "APP");//交易类型

            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            if (!result.IsSet("prepay_id"))
            {
                Log.Error(this.GetType().ToString(), "The prepay_id is null");
                return "";
            }

            string prepay_id = result.GetValue("prepay_id").ToString();//获得统一下单接口返回的预支付交易会话标识
            Log.Info(this.GetType().ToString(), "Get prepay_id : " + prepay_id);

            //签名
            string rValue = CreateAppPayPackage(prepay_id);
            return rValue;
        }

        /// <summary>
        /// 生成app支付请求json
        /// </summary>
        /// <param name="prepayid"></param>
        /// <returns></returns>
        public string CreateAppPayPackage(string prepayid)
        {
            WxPayData payData = new WxPayData();
            payData.SetValue("appid", WxPayConfig.APPID);
            payData.SetValue("noncestr", Guid.NewGuid().ToString().Replace("-", ""));
            payData.SetValue("package", "Sign=WXPay");
            payData.SetValue("partnerid", WxPayConfig.MCHID);
            payData.SetValue("prepayid", prepayid);
            payData.SetValue("timestamp", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());
            payData.SetValue("sign", payData.MakeSign());
            string json = payData.ToJson();
            return json;            
        }


        /**
        * 参数数组转换为url格式
        * @param map 参数名与参数值的映射表
        * @return URL字符串
        */
        private string ToUrlParams(SortedDictionary<string, object> map)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in map)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}
