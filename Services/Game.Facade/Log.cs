using Game.Utils;
using Game.Utils.Cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Facade
{
    public class Log
    {
        // 日志文件路径
        public const string LogFilePath = "/Log/Error/";

        // 写日志时间间隔，单位毫秒
        public const int TimeInterval = 20;

        public static void Write(string content)
        {
            // 判断距离上一次写入的日志时间
            string key = "LastWritErrorLogTime";
            object obj = WHCache.Default.Get<AspNetCache>(key);
            if (obj == null || (DateTime.Now - Convert.ToDateTime(obj)).TotalMilliseconds > TimeInterval)
            {
                // 日志内容
                StringBuilder log = new StringBuilder();
                log.Append(DateTime.Now);
                log.Append(" 源：" + GameRequest.GetRawUrl());
                log.Append(" IP：" + GameRequest.GetUserIP());
                log.Append(" 描述：" + content + "\r\n");

                // 检查目录
                string directory = Game.Utils.TextUtility.GetMapPath(LogFilePath);
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
    }
}
