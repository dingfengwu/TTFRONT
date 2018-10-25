using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.GameWebApplication
{
    /// <summary>
    /// Summary description for ReadAnnouncement
    /// </summary>
    public class ReadAnnouncement : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var userId = context.Request["userId"];
            var newsId = context.Request["newsId"];
            FacadeManage.aideNativeWebFacade.UpdateNewsState(Convert.ToInt32(userId), Convert.ToInt32(newsId));

            var ret = new RetureCode();
            ret.msg = "";
            ret.code = 0;

            context.Response.Write(LitJson.JsonMapper.ToJson(ret));
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