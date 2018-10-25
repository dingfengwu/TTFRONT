using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Mobile
{
    public partial class Feedback : UCPageBase
    {
        protected string contents = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigInfo model = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.ContactConfig.ToString());
                if (model != null)
                {
                    contents = model.Field1;
                }
            }

            if (Page.IsPostBack)
            {
                PostData();
            }
        }

        #region 继承属性

        protected override bool IsAuthenticatedUser
        {
            get
            {
                return true;
            }
        }

        #endregion

        /// <summary>
        /// 提交数据
        /// </summary>
        private void PostData()
        {
            string content = GameRequest.GetFormString("content");
            HttpPostedFile imgFile = null;
            if (Request.Files.Count != 0)
            {
                imgFile = Request.Files[0];
            }
            string fileUrl = "";
            if (imgFile != null && imgFile.ContentLength != 0)
            {
                try
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(imgFile.InputStream);
                    image.Dispose();
                }
                catch
                {
                    ShowAndRedirect("目前只支持图片格式文件,对您使用不便感到非常抱歉。", "/Mobile/Feedback.aspx");
                    return;
                }

                //文件上传
                string savePath = "/Upload/Feedback/";
                string dirPath = Server.MapPath(savePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = imgFile.FileName;
                string fileExt = Path.GetExtension(fileName).ToLower();
                //保存图片
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                string filePath = dirPath + newFileName;
                fileUrl = savePath + newFileName;
                imgFile.SaveAs(filePath);
            }

            if (content == "")
            {
                ShowAndRedirect("反馈意见不能为空。", "/Mobile/Feedback.aspx");
                return;
            }
            if (content.Length < 10)
            {
                ShowAndRedirect("反馈意见最少输入10个字符。", "/Mobile/Feedback.aspx");
                return;
            }
            
            //提交留言
            string accounts = "";
            if (Fetch.GetUserCookie() != null)
            {
                accounts = Fetch.GetUserCookie().Accounts;
            }

            if (fileUrl != "")
            {
                content = content + string.Format("<image src=\"{0}\"></image>", fileUrl);
            }

            GameFeedbackInfo info = new GameFeedbackInfo();
            info.FeedbackContent = Utility.HtmlEncode(content);
            info.FeedbackTitle = "";
            info.ClientIP = GameRequest.GetUserIP();

            Message msg = FacadeManage.aideNativeWebFacade.PublishFeedback(info, accounts);
            if (msg.Success)
            {
                ShowAndRedirect("感谢您的问题反馈，我们将尽快给予回复，敬请留意！", "/Mobile/Feedback.aspx");
            }
            else
            {
                Show(msg.Content);
            }
        }
    }
}