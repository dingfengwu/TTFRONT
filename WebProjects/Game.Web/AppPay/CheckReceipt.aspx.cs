using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Game.Utils;
using System.Net;
using System.IO;
using Game.Facade;
using Game.Entity.Treasure;
using Game.Kernel;
using System.Data;

namespace Game.Web.AppPay
{
    public partial class CheckReceipt : System.Web.UI.Page
    {
        #region Fields

        string receiptData = "";
        int userID = GameRequest.GetQueryInt( "UserID" , 0 );
        string orderID = GameRequest.GetQueryString( "OrderID" );
        int payAmount = GameRequest.GetQueryInt( "PayAmount" , 0 );
        #endregion

        #region 窗口事件

        protected void Page_Load( object sender , EventArgs e )
        {
            if( !IsPostBack )
            {
                StreamReader sr = new StreamReader( Request.InputStream );
                receiptData = sr.ReadToEnd();

                //苹果返回数据
                string rValue = AppInfo( receiptData );
                AppReceiptInfo receipt = AppReceiptInfo.DeserializeObject( rValue );

                //订单数据
                ShareDetialInfo detailInfo = new ShareDetialInfo();
                detailInfo.UserID = userID;
                detailInfo.OrderID = orderID;
                detailInfo.PayAmount = payAmount;
                detailInfo.ShareID = 100;

                //处理数据
                TreasureFacade treasureFacade = new TreasureFacade();
                treasureFacade.WriteReturnAppDetail( detailInfo , receipt );

                if( receipt.Status == 0 )
                {
                    //写充值记录
                    try
                    {
                        Message msg = treasureFacade.FilliedApp(detailInfo, receipt.Receipt.product_id);
                        if( msg.Success )
                        {
                            Response.Write( "0" );
                        }
                        else
                        {
                            Response.Write( msg.Content );
                        }
                    }
                    catch( Exception ex )
                    {
                        Response.Write( ex.Message );
                    }
                }
                else
                {
                    Response.Write( "失败" );
                }
            }
        }
        #endregion

        #region 处理方法

        public string AppInfo( string receiptData )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( "{\"receipt-data\":\"" + receiptData + "\"}" );

            string rValue = Send_( ApplicationSettings.Get( "appUrl" ) , sb.ToString() );
            return rValue;
        }

        public string Send_( string url , string message )
        {
            //把sXmlMessage发送到指定的DsmpUrl地址上
            Encoding encode = System.Text.Encoding.GetEncoding( "utf-8" );
            byte[] arrB = encode.GetBytes( message );
            HttpWebRequest myReq = ( HttpWebRequest )WebRequest.Create( url );
            myReq.Method = "POST";
            myReq.ContentType = "application/x-www-form-urlencoded";
            myReq.ContentLength = arrB.Length;
            Stream outStream = myReq.GetRequestStream();
            outStream.Write( arrB , 0 , arrB.Length );
            outStream.Close();


            //接收HTTP做出的响应
            HttpWebResponse myResp = ( HttpWebResponse )myReq.GetResponse();
            Stream ReceiveStream = myResp.GetResponseStream();
            StreamReader readStream = new StreamReader( ReceiveStream , encode );
            string str = readStream.ReadToEnd();
            readStream.Close();
            myResp.Close();
            return str;
        }
        #endregion
    }
}
