using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Game.Utils;
using Qr.Net.Imaging;
using System.Drawing;
using System.IO;
using System.Data;
using Game.Facade;
using System.Text;

namespace Game.Web.WS
{
    /// <summary>
    /// NativeWeb 的摘要说明
    /// </summary>
    public class NativeWeb : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetQueryString("action").ToLower();
            switch(action)
            {
                case "getqrcodeimage":
                    GetQRCodeImage(context);
                    break;

                case "getclientip":
                    GetClientIP(context);
                    break;

                case "getnoticelist":
                    GetNoticeList(context);
                    break;

                case "getmobilenotice":
                    GetMobileNotice(context);
                    break;

                case "getawardorder":
                    GetAwardOrder(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取二维码图片
        /// </summary>
        private void GetQRCodeImage(HttpContext context)
        {
            string url = GameRequest.GetQueryString("url");
            if(string.IsNullOrEmpty(url))
            {
                return;
            }

            QrImage qrcode = new QrImage();

            // 编码方式
            qrcode.Mode = "byte";

            // 二维码版本
            qrcode.Version = -1;

            // 图片尺寸
            qrcode.Size = 100;

            // 补白尺寸
            qrcode.Padding = 10;

            // 二维码纠错等级
            qrcode.Level = "Q";

            // 二维码前景和背景色
            qrcode.Background = Color.White;
            qrcode.Foreground = Color.Black;

            // ico图标
            try
            {
                FileStream fs = new FileStream(TextUtility.GetFullPath("/favicon.ico"), FileMode.Open);
                Icon ico = new Icon(fs, 256, 256);
                qrcode.Logo = ico.ToBitmap();
                fs.Close();
            }
            catch
            { }

            // 创建图片
            try
            {
                Bitmap encodeImage = qrcode.CreateImage(url);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                encodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                context.Response.ClearContent();
                context.Response.ContentType = "image/png";
                context.Response.BinaryWrite(ms.ToArray());
                encodeImage.Dispose();
            }
            catch
            { }
        }

        /// <summary>
        /// 输出客户端IP
        /// </summary>
        /// <param name="context"></param>
        private void GetClientIP(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GameRequest.GetUserIP());
        }

        /// <summary>
        /// 输出大厅公告
        /// </summary>
        /// <param name="context"></param>
        private void GetNoticeList(HttpContext context)
        {
            IList<Entity.NativeWeb.News> list = FacadeManage.aideNativeWebFacade.GetTopNewsList(0, 0, 0, 5);
            StringBuilder sb = new StringBuilder();
            if (list != null) 
            {
                foreach (Entity.NativeWeb.News news in list)
                {
                    sb.AppendFormat("<a href='/News/NewsView.aspx?param={0}' target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;", news.NewsID, news.Subject.ToString());
                }
            }
            
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 移动版获取公告
        /// </summary>
        /// <param name="context"></param>
        private void GetMobileNotice(HttpContext context)
        {
            int kindId = GameRequest.GetQueryInt("kindid", 0);
            if(kindId == 0)
                return;
        }

        /// <summary>
        /// 获取兑换奖品列表
        /// </summary>
        /// <param name="context"></param>
        private void GetAwardOrder(HttpContext context)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<ul>");
            AjaxJsonValid ajaxJson = new AjaxJsonValid();
            try
            {
                DataSet ds = FacadeManage.aideNativeWebFacade.GetTopOrder(10);
                if(ds != null)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        html.Append("<li class=\"f12\">");
                        html.AppendFormat("<p>恭喜玩家：{0}</p>", FacadeManage.aideAccountsFacade.GetNickNameByUserID(Convert.ToInt32(dr["UserID"])));
                        html.AppendFormat("<p>成功兑换：{0}</p>", dr["AwardName"]);
                        html.AppendFormat("<p>兑换时间：{0}</p>", dr["BuyDate"]);
                        html.Append("</li>");
                    }
                }

                ajaxJson.SetValidDataValue(true);
                html.Append("</ul>");
                ajaxJson.AddDataItem("html", html.ToString());
                ajaxJson.AddDataItem("count", ds.Tables[0].Rows.Count);
                context.Response.Write(ajaxJson.SerializeToJson());
            }
            catch(Exception ex)
            {
                ajaxJson.msg = ex.ToString();
                context.Response.Write(ajaxJson.SerializeToJson());
            }
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