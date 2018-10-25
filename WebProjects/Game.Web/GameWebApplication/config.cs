using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// config 的摘要说明
/// </summary>
public class config
{
    public config()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    // 应用ID,您的APPID
    public static string app_id = "2018071060547790";

    // 支付宝网关
    public static string gatewayUrl = "https://openapi.alipay.com/gateway.do";

    // 商户私钥，您的原始格式RSA私钥
    public static string private_key = @"MIIEowIBAAKCAQEAzVfroSuD1KMmitOOQ0R6tTsh/AMQuul2eooRRXwk+/aw50+w
MuOsjpM6+zBH468pI0HOq3LXMscGAmp9WYXYon7Naa6kvuw3zkmi9nLD220FAI8v
O5ol+pLMz/hFauohSL6gS0z1oeTpXQicKLLcMIwgUwtcGCq3i3v4dSbXaBi5thGV
MHDWNsn9mtC2pY6fe9MVJcLhCk5z9d2sl+ODWoWm/XbebopjENI6NiHnfWKeRXNe
Oa/MOymNzBvHZA8ZvMbekBoyb2/b2SpuTdW/9t9k5KL3Ryws04brSowov9x54ocS
9zCJ4qVmikE0yInl1OBUgrx19yPn1R6poL+B9QIDAQABAoIBADbM6fNfO5V53QVb
pDHLirvnBhDNeJ+JQrc9NZLHqM8dbOSuXaWXISwDmtACeI0I5/+ixlb3FPtWJgJr
DPzhPYlQMd2sYAcb32DMQhNnWWGr3JPjooVThCM1Hje6WDxKM4vIY9r0tPD5uFW9
wH8UDLNgEhhXhcJlVAqDkTEipoX+6C0019ZDGgNEePPAbgcLKB4RTvGTfgzN1247
3pOHuHeSf6Z3zvmiYSReTUOWVEdVMxsUWXCAPn2UIFihU4ab5HjCqUKe7SY7wWaT
7B0gAgkqBYWsNrJLuzaukZkw+LPWcdek1s0ZCrC8GQ739W9AHTyPerT8s4U3HoP7
QgioOVkCgYEA/yVG/WJIRMhQj1URmgQDmVyp9SAwsIeCc5OV95iROI2d9QKENeV6
CMAiGZ7sRGMtDg7ZMbDMFD88nkh8DneO4Xr+uya+LfgxmMQPV6GWItYWbJm1ry/1
NkJnLU4MZmGtyFFgThmG2/ELKrVxl8/tV55PgdxBBpn6dCNSKwrpbocCgYEAzgfz
TDQ5BBeGGmZHYot0BuTp/u3pUGLF2b3FbSsF4y5m9csuTCA+XCYDzCMdhHUkOg5j
TqBXr++1lG6cLY1IDoC7udTKtI93/tcVmq7DprZ2JOGadd4YWZCopuSLcOqIqCs1
uLBZYmQjvHHwMGfq2yKPqaF8KNCq0S3dYqFHTqMCgYBGtxo52CeXiL1rPHSobzxg
ISKp4cYc5zHsvpbuDMcTGY0R/ySNm5B7JGVPHJD3U1WFc/AWqZ2mbvBqHkTj7ZcY
P3KihFZpf0SfxpdJ/msSNKv6ZY/Jgk1AQJ9AG0Wsip4TyxoaC1EpXGFv8OIO5X4u
rp3yrA0Ju1uDHNcFPvz7uQKBgE092c8F/SI1l4cqNTUSxysWg0uZ8lC61yYs6Wlm
KczkRqF7zR2pMPfnIKFVwOk56Z0Ca+S8ZGOHYPIHDfJd91fIl5ix2FUdPIWEKYtW
Xe+QlHZ7RidOXp6lhzUaldR9eUJjAL7/DmO+2075AG2FaB1DtcyIyD2dDY1ivo8N
m+g1AoGBAMHLgR+MZAX9rqlniMsGD4T9YZ+jEMfKBYIZqZN3LCxE7B/kryI9eaoy
BmRGY7n/eVIib7VQfYgFj7wAKo3d45lE0lE6pTxScTHgwGqm/ObyKCAuECOeqtdc
R8FgbdLW10UyURJPBpWBxSZpCHOMaRs/Fld3JpSKjXkeP1vuLk4L";
    // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
    public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzVfroSuD1KMmitOOQ0R6
tTsh/AMQuul2eooRRXwk+/aw50+wMuOsjpM6+zBH468pI0HOq3LXMscGAmp9WYXY
on7Naa6kvuw3zkmi9nLD220FAI8vO5ol+pLMz/hFauohSL6gS0z1oeTpXQicKLLc
MIwgUwtcGCq3i3v4dSbXaBi5thGVMHDWNsn9mtC2pY6fe9MVJcLhCk5z9d2sl+OD
WoWm/XbebopjENI6NiHnfWKeRXNeOa/MOymNzBvHZA8ZvMbekBoyb2/b2SpuTdW/
9t9k5KL3Ryws04brSowov9x54ocS9zCJ4qVmikE0yInl1OBUgrx19yPn1R6poL+B
9QIDAQAB";  // 签名方式
    public static string sign_type = "RSA2";

    // 编码格式
    public static string charset = "UTF-8";
}
public class PayData
{
    public string TradeNo;
    public DateTime CreateDate;
    public float Amount;
    public int UserId;
    public int Status = 0;
    public string PayType;

    private static Dictionary<string, PayData> AliPayDatas = new Dictionary<string, PayData>();

    public static void Add(string TradeNo, PayData TradeItem)
    {
        lock (AliPayDatas)
        {
            var key = string.Format("{0}_{1}", TradeItem.PayType, TradeNo);
            AliPayDatas.Add(key, TradeItem);
        }
    }
    public static PayData Find(string TradeNo,string payType)
    {
        lock (AliPayDatas)
        {
            var key = string.Format("{0}_{1}", payType, TradeNo);
            if (AliPayDatas.ContainsKey(key))
                return AliPayDatas[key];
            return null;
        }
    }
//     public static AliPayData TryRemove(string TradeNo)
//     {
//         lock (AliPayDatas)
//         {
//             if (AliPayDatas.ContainsKey(TradeNo))
//             {
//                 AliPayData _AliPayData = AliPayDatas[TradeNo];
//                 AliPayDatas.Remove(TradeNo);
//                 return _AliPayData;
//             }
//             return null;
//         }
//     }
}