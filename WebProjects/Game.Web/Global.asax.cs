using Game.Utils;
using Game.Utils.Cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Game.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Context.Server.GetLastError();
            if (ex != null)
            {
                // 判断距离上一次写入的日志时间 小于500毫秒则不写入
                string key = "LastWritErrorLogTime";
                object obj = WHCache.Default.Get<AspNetCache>(key);
                if (obj != null)
                {
                    if ((DateTime.Now - Convert.ToDateTime(obj)).TotalMilliseconds < 500)
                        return;
                }

                // 日志内容
                StringBuilder log = new StringBuilder();
                log.Append(DateTime.Now);
                log.Append(" 源：" + Request.Url.ToString());
                log.Append(" IP：" + GameRequest.GetUserIP());
                log.Append(" 描述：" + ex.Message + "\r\n");

                // 检查目录
                string directory = Server.MapPath("/Log/");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 检查当前日志文件大小 超过30M则创建一个新的日志文件
                string fullPath = directory + "ErrorLog" + DateTime.Now.ToString("yyyy-MM") + ".log";
                int i = 0;
                fullPath = FileManager.GetCurrentLogName(directory, fullPath, ref i);
                if (File.Exists(fullPath))
                {
                    FileInfo fi = new FileInfo(fullPath);
                    if (fi.Length > 30 * 1024 * 1024)
                        fullPath = directory + "ErrorLog" + DateTime.Now.ToString("yyyy-MM") + "[" + i + "].log";
                }

                // 写入日志
                File.AppendAllText(fullPath, log.ToString(), System.Text.Encoding.Unicode);
                WHCache.Default.Save<AspNetCache>(key, DateTime.Now, 1);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}