using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
namespace WebApplication1
{
    /// <summary>
    /// AppleInappItem 的摘要说明
    /// </summary>
    public class AppleInappItem : IHttpHandler
    {
        public class AppleReturnData
        {
            public int status = 0;
            public string puenvironment = "Sandbox";
            public Creceipt receipt;
            public class Creceipt
            {
                public string receipt_type = "ProductionSandbox";
                public int adam_id = 0;
                public int app_item_id = 0;
                public string bundle_id = "com.c2dxluagm.RoyalHall";
                public string application_version = "1.0";
                public int download_id = 0;
                public int version_external_identifier = 0;
                public string receipt_creation_date = "2018-05-03 09:50:00 Etc/GMT";
                public string receipt_creation_date_ms = "1525341000000";
                public string receipt_creation_date_pst = "2018-05-03 02:50:00 America/Los_Angeles";
                public string request_date = "2018-05-04 09:40:54 Etc/GMT";
                public string request_date_ms = "1525426854639";
                public string request_date_pst = "2018-05-04 02:40:54 America/Los_Angeles";
                public string original_purchase_date = "2013-08-01 07:00:00 Etc/GMT";
                public string original_purchase_date_ms = "1375340400000";
                public string original_purchase_date_pst = "2013-08-01 00:00:00 America/Los_Angeles";
                public string original_application_version = "1.0";
                public List<CIn_App> in_app = new List<CIn_App>();
                public class CIn_App
                {
                    public string quantity = "1";
                    public string product_id = "com.tt.purchase.3";
                    public string transaction_id = "1000000395542830";
                    public string original_transaction_id = "1000000395542830";
                    public string purchase_date = "2018-05-03 09:49:59 Etc/GMT";
                    public string purchase_date_ms = "1525340999000";
                    public string purchase_date_pst = "2018-05-03 02:49:59 America/Los_Angeles";
                    public string original_purchase_date = "2018-05-03 09:49:59 Etc/GMT";
                    public string original_purchase_date_ms = "1525340999000";
                    public string original_purchase_date_pst = "2018-05-03 02:49:59 America/Los_Angeles";
                    public string is_trial_period = "false";
                }
            }
        }
        HttpContext httpContext;
        public void ProcessRequest(HttpContext context)
        {
            httpContext = context;
            Page_Load(context);
        }
        protected void Page_Load(HttpContext context)
        {
            RetureCode newCode = new RetureCode();
            try
            {
                Stream sm = context.Request.InputStream;
                StreamReader inputData = new StreamReader(sm);
                string DataString = inputData.ReadToEnd();
//                 Debug.Log("AppleInapp", DataString);
                ReceiptDataJson _ReceiptDataJson = LitJson.JsonMapper.ToObject<ReceiptDataJson>(DataString);
                newCode.data.userId = _ReceiptDataJson.UserId;
                float xx = 0;
                float.TryParse(_ReceiptDataJson.AppVersion, out xx);
                string msg = "";
                if (PostInApp((int)(xx * 1000) > (int)((3.18f) * 1000), _ReceiptDataJson.UserId, _ReceiptDataJson.receiptData, out newCode.data.sorce, out msg))
                {
                    newCode.code = 0;
                }
                else
                    newCode.code = 1;
                newCode.msg = msg;
            }
            catch(Exception exp)
            {
                newCode.code = 2;
                newCode.msg = exp.Message.ToString() + "\n" + exp.StackTrace.ToString();
                Debug.LogException(exp);
            }
            WebApplication1.Phone.SendStringToClient(httpContext, LitJson.JsonMapper.ToJson(newCode));
        }
        public static bool PostInApp(bool bTest, int UserId, string json, out int _ReturnSoce, out string msg)
        {
            _ReturnSoce = 0;
            msg = "";
            string returnmessage = "";
            try
            {
                LitJson.JsonData newData = new LitJson.JsonData();
                newData["receipt-data"] = json;
                json = newData.ToJson();
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                System.Net.WebRequest request = null;
                if(bTest)
                     request = System.Net.HttpWebRequest.Create("https://sandbox.itunes.apple.com/verifyReceipt");
                else
                    request = System.Net.HttpWebRequest.Create("https://buy.itunes.apple.com/verifyReceipt");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = postBytes.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                    stream.Flush();
                }
                var sendresponse = request.GetResponse();
                string sendresponsetext = "";
                using (var streamReader = new StreamReader(sendresponse.GetResponseStream()))
                {
                    sendresponsetext = streamReader.ReadToEnd().Trim();
                }
                returnmessage = sendresponsetext;
                if (returnmessage.Length < 50)
                {
                    msg = "验证失败";
                    return false;
                }
                AppleReturnData _AppleReturnData = LitJson.JsonMapper.ToObject<AppleReturnData>(sendresponsetext);
//                 if (_AppleReturnData.receipt.bundle_id != "com.c2dxluagm.RoyalHall")
//                 {
//                     msg = "bundle_id 不正确";
//                     return false;
//                 }
                string[] productString = _AppleReturnData.receipt.in_app[0].product_id.Split('.');
                if (productString.Length <= 2)
                {
                    msg = "验证失败";
                    return false;
                }
                int Price = 0;
                int.TryParse(productString[productString.Length - 1], out Price);
                Price = Price * 100;
                bool rlt = AddSorce(UserId, Price, _AppleReturnData, out _ReturnSoce);
                if (!rlt)
                    msg = "已经验证过了";

                JsonEMail newEmail = new JsonEMail();
                newEmail.dwUserID = UserId;
                newEmail.nStatus = 0;
                newEmail.szTitle = "支付成功";
                newEmail.szMessage = "IOS内购交易：支付成功[" + (Price / 100).ToString() + ".00]";
                newEmail.szSender = "系统";
                newEmail.szSendTime = DateTime.Now.ToString();
                EmailAdd.AddEmail(newEmail);

                return rlt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return false;
        }
        public static bool AddSorce(int UserId, int Sroce, AppleReturnData _AppleReturnData, out int _ReturnSoce)
        {
            _ReturnSoce = 0;
            string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBTreasure"];
//             string MyConn = "server=103.105.58.140;uid=testdb;pwd=123abc;database=RYTreasureDB;Trusted_Connection=no";
            SqlConnection MyConnection = new SqlConnection(MyConn);
            try
            {
                MyConnection.Open();
                {
                    string selStr = "select transaction_id from ReturnAppDetailInfo where transaction_id=" + _AppleReturnData.receipt.in_app[0].transaction_id;
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (_Reader.HasRows)
                    {
                        _Reader.Close();
                        return false;
                    }
                    _Reader.Close();
                }
                {
                    string col = @"
UserID,
OrderID,
PayAmount,
Status,
quantity,
product_id,
transaction_id,
purchase_date,
original_transaction_id,
original_purchase_date,
app_item_id,
version_external_identifier,
bid,
bvrs";

                    CsString _Value = new CsString("");
                    _Value.Fill(UserId);
                    _Value.Fill(_AppleReturnData.receipt.download_id);
                    _Value.Fill(Sroce);
                    _Value.Fill("0");
                    _Value.Fill(1);
                    _Value.Fill(_AppleReturnData.receipt.in_app[0].product_id);
                    _Value.Fill(_AppleReturnData.receipt.in_app[0].transaction_id);
                    _Value.Fill(_AppleReturnData.receipt.in_app[0].purchase_date);
                    _Value.Fill(_AppleReturnData.receipt.in_app[0].original_transaction_id);
                    _Value.Fill(_AppleReturnData.receipt.in_app[0].original_purchase_date);
                    _Value.Fill(_AppleReturnData.receipt.app_item_id);
                    _Value.Fill(_AppleReturnData.receipt.version_external_identifier);
                    _Value.Fill(_AppleReturnData.receipt.bundle_id);
                    _Value.FillEx(123);
                    CsString str = CsString.InstertSql("ReturnAppDetailInfo", col, _Value.ToString());

                    SqlCommand MyCommand = new SqlCommand(str.ToString(), MyConnection);
                    MyCommand.ExecuteNonQuery();
                }
                {
                    string MyUpdate = "Update GameScoreInfo set Score=Score+" + Sroce.ToString() +
                        " where UserID=" + UserId;
                    SqlCommand MyCommand = new SqlCommand(MyUpdate, MyConnection);
                    MyCommand.ExecuteNonQuery();
                }
                {
                    string selStr = "select Score from GameScoreInfo where UserID=" + UserId;
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (_Reader.Read())
                    {
                        _ReturnSoce = (int)_Reader.GetInt64(0);
                        _Reader.Close();
                        return true;
                    }
                    _Reader.Close();
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
        public class ReceiptDataJson
        {
            public int UserId = 0;
            public string AppVersion; //0 正式, 1 测试
            public string receiptData = "";
        }
        public class RetureCode
        {
            public int code = 1;
            public string msg = "";
            public class UserData
            {
                public int userId;
                public int sorce;
            }
            public UserData data = new UserData();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}