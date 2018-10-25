using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
namespace WebApplication1
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
    public partial class AppleInapp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stream sm = Request.GetBufferedInputStream();
            StreamReader inputData = new StreamReader(sm);
            string DataString = inputData.ReadToEnd();
            Debug.Log("AppleInapp", DataString);
            ReceiptDataJson _ReceiptDataJson = LitJson.JsonMapper.ToObject<ReceiptDataJson>(DataString);
            RetureCode newCode = new RetureCode();
            newCode.data.userId = _ReceiptDataJson.UserId;
            if (PostInApp(_ReceiptDataJson.UserId, _ReceiptDataJson.receiptData, out newCode.data.sorce))
            {
                Debug.Log("AppleInapp", "", true);
                newCode.code = 0;
            }
            else
            {
                Debug.Log("AppleInapp", "", false);
                newCode.code = 1;
            }
            WebApplication.PhoneLogin.SendStringToClient(this, LitJson.JsonMapper.ToJson(newCode));
        }
        public static bool AddScore(int score, int userId,string tradeNo)
        {
            try
            {
                string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBTreasure"];
                SqlConnection MyConnection = new SqlConnection(MyConn);
                MyConnection.Open();

                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append("EXEC NET_PM_AddScoreByLoading ")
                    .AppendFormat("'{0}',", userId)
                    .AppendFormat("'{0}',", score)
                    .AppendFormat("'{0}'", tradeNo);
                SqlCommand MyCommand = new SqlCommand(sqlBuilder.ToString(), MyConnection);
                bool rl = MyCommand.ExecuteNonQuery()>0;
                Debug.Log("sql", sqlBuilder.ToString(), rl);
                MyConnection.Close();
                return rl;
            }
            catch (Exception ex)
            {          
                Debug.LogException(ex);
                return false;
            }
            finally
            {

            }
        }

        public static bool AddScoreByBinding(int score, int userId)
        {
            try
            {
                string MyConn = System.Configuration.ConfigurationManager.AppSettings["DBTreasure"];
                SqlConnection MyConnection = new SqlConnection(MyConn);
                MyConnection.Open();

                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append("EXEC NET_PM_AddScoreByBindingPhone ")
                    .AppendFormat("'{0}',", userId)
                    .AppendFormat("'{0}'", score);
                SqlCommand MyCommand = new SqlCommand(sqlBuilder.ToString(), MyConnection);
                bool rl = MyCommand.ExecuteNonQuery() > 0;
                Debug.Log("sql", sqlBuilder.ToString(), rl);
                MyConnection.Close();
                return rl;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
            finally
            {

            }
        }

        public static bool PostInApp(int UserId, string json, out int _ReturnSoce)
        {
            _ReturnSoce = 0;
            string returnmessage = "";
            try
            {
                LitJson.JsonData newData = new LitJson.JsonData();
                newData["receipt-data"] = json;
                json = newData.ToJson();
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                var request = System.Net.HttpWebRequest.Create("https://sandbox.itunes.apple.com/verifyReceipt");
//                 var request = System.Net.HttpWebRequest.Create("https://buy.itunes.apple.com/verifyReceipt");
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
                    Debug.Log("Error returnmessage.Length < 50", sendresponsetext, false);
                    return false;
                }
                AppleReturnData _AppleReturnData = LitJson.JsonMapper.ToObject<AppleReturnData>(sendresponsetext);
//                 if (_AppleReturnData.receipt.bundle_id != "com.c2dxluagm.RoyalHall")
//                 {
//                     Debug.Log("AppleReturnData.receipt.bundle_id != com.c2dxluagm.RoyalHall", sendresponsetext, false);
//                     return false;
//                 }
                string[] productString = _AppleReturnData.receipt.in_app[0].product_id.Split('.');
                if (productString.Length <= 2)
                {
                    Debug.Log("productString.Length <= 2", sendresponsetext, false);
                    return false;
                }
                int Price = 0;
                int.TryParse(productString[productString.Length - 1], out Price);
                Price = Price * 100;
                AddSorce(UserId, Price, _AppleReturnData, out _ReturnSoce);
                //
                JsonEMail newEmail = new JsonEMail();
                newEmail.dwUserID = UserId;
                newEmail.nStatus = 0;
                newEmail.szTitle = "支付成功";
                newEmail.szMessage = "IOS内购交易：支付成功[" + Price.ToString() + "]";
                newEmail.szSender = "系统";
                newEmail.szSendTime = DateTime.Now.ToString();
                EmailAdd.AddEmail(newEmail);
                return true;
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

//             string MyConn = "Data Source=.; Initial Catalog=RYTreasureDB; User ID=sa_games; Password=sa_gamesluatest; Pooling=true";
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
                        Debug.LogError("Sql", selStr);
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
                    bool rl = MyCommand.ExecuteNonQuery()>0;
                    Debug.Log("Sql", MyUpdate, rl);

                }
                {
                    string selStr = "select Score from GameScoreInfo where UserID=" + UserId;
                    SqlCommand MyCommand = new SqlCommand(selStr, MyConnection);
                    SqlDataReader _Reader = MyCommand.ExecuteReader();
                    if (_Reader.Read())
                    {
                        _ReturnSoce = (int)_Reader.GetInt64(0);
                        _Reader.Close();
                        return false;
                    }
                    _Reader.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                if (MyConnection!=null)
                    MyConnection.Close();
            }
            return false;
        }
        public class ReceiptDataJson
        {
            public int UserId = 0;
            public string receiptData = "";
        }
        public class RetureCode
        {
            public int code = 1;
            public class UserData
            {
                public int userId;
                public int sorce;
            }
            public UserData data = new UserData();
        }
    }
}