using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.Web.SessionState;

using Game.Facade;
using Game.Utils;
using Game.Entity.Accounts;
using System.Drawing.Drawing2D;

namespace Game.Web.Ashx
{
    /// <summary>
    /// 获取自定义头像
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    public class UserFace : IHttpHandler , IRequiresSessionState
    {
        public void ProcessRequest( HttpContext context )
        {
            // 自定义头像
            int customID = GameRequest.GetInt( "customid" , 0 );
            if( customID == 0 )
            {
                return;
            }

            AccountsFace faceModel = FacadeManage.aideAccountsFacade.GetAccountsFace( customID );
            if( faceModel == null )
            {
                return;
            }
            else
            {
                byte[] faceByte = (byte[])faceModel.CustomFace;

                // 新建画布
                int width = 48;
                int height = 48;
                Bitmap bitmap = new Bitmap(width, height);

                // 循环像素
                int site = 4;
                for (int y = 0; y < 48; y++)
                {
                    for (int x = 0; x < 48; x++)
                    {
                        byte b = faceByte[site - 4];
                        byte g = faceByte[site - 3];
                        byte r = faceByte[site - 2];
                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                        site = site + 4;
                    }
                }

                //放大图片
                Bitmap bitmap2 = ResizeImage(bitmap, 96, 96);

                // 输出图片
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bitmap2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                context.Response.ClearContent();
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(ms.ToArray());
                bitmap.Dispose();
            }
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