using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.GameWebApplication
{
    /// <summary>
    /// ClientAppVersion 的摘要说明
    /// </summary>
    public class ClientAppVersion : IHttpHandler
    {
        public class AppVersion
        {
            public int code = 1;
            public string msg = "";
            public string version = "1.1";
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            AppVersion newAppVersion = new AppVersion();
            newAppVersion.code = 0;
            newAppVersion.version = "1.1";
            context.Response.Write(LitJson.JsonMapper.ToJson(newAppVersion));

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