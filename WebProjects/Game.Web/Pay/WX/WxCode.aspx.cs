using Game.Facade;
using Game.Utils;
using Qr.Net.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Pay.WX
{
    public partial class WxCode : UCPageBase
    {
        public string imagecode = string.Empty;
        public string orderid = string.Empty;
        public string amountcode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string return_code = Request.Form["return_code"];
            string return_msg = Request.Form["return_msg"];
            string code_url = Request.Form["code_url"];
            string orderID = Request.Form["orderID"];
            string amount = Request.Form["amount"];


            if (return_code == "SUCCESS" && code_url != null && code_url != "")
            {
                //初始化二维码生成工具
                QrImage qrcode = new QrImage();
                // 编码方式
                qrcode.Mode = "byte";

                // 二维码版本
                qrcode.Version = -1;

                // 图片尺寸
                qrcode.Size = 200;

                // 补白尺寸
                qrcode.Padding = 10;

                // 二维码纠错等级
                qrcode.Level = "Q";

                // 二维码前景和背景色
                qrcode.Background = Color.White;
                qrcode.Foreground = Color.Black;

                Bitmap encodeImage = qrcode.CreateImage(code_url);
                MemoryStream ms = new MemoryStream();
                encodeImage.Save(ms, ImageFormat.Png);
                byte[] img = ms.GetBuffer();
                imagecode = Convert.ToBase64String(img, 0, img.Length);
                amountcode = amount;
                orderid = orderID;
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("微信扫码 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}