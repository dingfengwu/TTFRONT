using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using Game.Utils;
using System.Drawing.Drawing2D;
using Game.Facade;
using System.Net;
using System.IO;

namespace Game.Web.WS
{
    /// <summary>
    /// QRCode 的摘要说明
    /// </summary>
    public class QRCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            GetQRCode(context);
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="context"></param>
        private void GetQRCode(HttpContext context)
        {
            string encodeData = GameRequest.GetQueryString("qt");
            string icoURL = GameRequest.GetQueryString("qm");
            int width = GameRequest.GetQueryInt("qs", 0);
            if (encodeData != string.Empty)
            {
                calQrcode(encodeData, icoURL, width, context);
            }
        }

        /// <summary>
        /// 按照指定的大小绘制二维码
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private void calQrcode(string sData, string icoURL, int size, HttpContext context)
        {
            //二维码版本,大小获取
            Color qrCodeBackgroundColor = Color.White;
            Color qrCodeForegroundColor = Color.Black;
            int length = System.Text.Encoding.UTF8.GetBytes(sData).Length;

            //生成二维码数据
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//使用M纠错级别
            qrCodeEncoder.QRCodeVersion = 0;
            var encodedData = qrCodeEncoder.Encode(sData, System.Text.Encoding.UTF8);

            //绘制图片
            int x = 0, y = 0;
            int w = 0, h = 0;
            // 二维码矩阵单边数据点数目
            int count = encodedData.Length;
            // 获取单个数据点边长
            double sideLength = Convert.ToDouble(size) / count;
            // 初始化背景色画笔
            SolidBrush backcolor = new SolidBrush(qrCodeBackgroundColor);
            // 初始化前景色画笔
            SolidBrush forecolor = new SolidBrush(qrCodeForegroundColor);
            // 定义画布
            Bitmap image = new Bitmap(size, size);
            // 获取GDI+绘图图画
            Graphics graph = Graphics.FromImage(image);
            // 先填充背景色
            graph.FillRectangle(backcolor, 0, 0, size, size);

            // 变量数据矩阵生成二维码
            for (int row = 0; row < count; row++)
            {
                for (int col = 0; col < count; col++)
                {
                    // 计算数据点矩阵起始坐标和宽高
                    x = Convert.ToInt32(Math.Round(col * sideLength));
                    y = Convert.ToInt32(Math.Round(row * sideLength));
                    w = Convert.ToInt32(Math.Ceiling((col + 1) * sideLength) - Math.Floor(col * sideLength));
                    h = Convert.ToInt32(Math.Ceiling((row + 1) * sideLength) - Math.Floor(row * sideLength));

                    // 绘制数据矩阵
                    graph.FillRectangle(encodedData[col][row] ? forecolor : backcolor, x, y, w, h);
                }
            }

            //添加LOGO
            string path = context.Server.MapPath("/favicon.ico");            
            Bitmap logoImage = null;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                logoImage = new Bitmap(path);
            }
            if (icoURL != "")
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(Game.Facade.Fetch.GetUploadFileUrl(icoURL));
                try
                {
                    HttpWebResponse webReponse = (HttpWebResponse)webRequest.GetResponse();
                    if (webReponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = webReponse.GetResponseStream())
                        {
                            Image img = Image.FromStream(stream);
                            logoImage = new Bitmap(img);
                            img.Dispose();
                        }
                    }
                }
                catch { }
            }
            if (logoImage != null)
            {
                image = CoverImage(image, logoImage, graph);
                logoImage.Dispose();            
            }         
            //输出
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            context.Response.ClearContent();
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(ms.ToArray());
            image.Dispose();
        }

        /// <summary>
        /// 层叠图片
        /// </summary>
        /// <param name="original">原始图片(目前只支持正方形)</param>
        /// <param name="image">层叠图片(目前只支持正方形)</param>
        /// <returns>处理以后的图片</returns>
        private Bitmap CoverImage(Bitmap original, Bitmap image, Graphics graph = null)
        {
            //缩放附加图片
            int sideSLen = original.Width;
            int sideTLen = sideSLen / 4;
            image = ResizeImage(image, sideTLen, sideTLen);

            // 获取GDI+绘图图画
            graph = graph == null ? Graphics.FromImage(original) : graph;

            // 将附加图片绘制到原始图中央
            graph.DrawImage(image, (original.Width - sideTLen) / 2, (original.Height - sideTLen) / 2, sideTLen, sideTLen);

            // 释放GDI+绘图图画内存
            graph.Dispose();

            // 返回处理结果
            return original;
        }

        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的图片</returns>
        private Bitmap ResizeImage(Bitmap original, int width, int height)
        {
            try
            {
                Bitmap image = new Bitmap(width, height);
                Graphics graph = Graphics.FromImage(image);
                // 插值算法的质量
                graph.CompositingQuality = CompositingQuality.HighQuality;
                graph.SmoothingMode = SmoothingMode.HighQuality;
                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graph.DrawImage(original, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
                graph.Dispose();
                return image;
            }
            catch
            {
                return null;
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