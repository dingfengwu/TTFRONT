using Game.Facade;
using Game.Web.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.GameWebApplication
{
    /// <summary>
    /// Summary description for Announcement
    /// </summary>
    public class Announcement : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userId = context.Request["userId"];

            var data = new NewsJson();
            var notices = FacadeManage.aideNativeWebFacade.GetMobileNotcieList(1, 1);
            if(notices.PageSet.Tables[0].Rows.Count > 0)
            {
                data.Id = Convert.ToInt32(notices.PageSet.Tables[0].Rows[0]["NewsId"]);
                data.Subject = notices.PageSet.Tables[0].Rows[0]["Subject"].ToString();
                data.Body= notices.PageSet.Tables[0].Rows[0]["Body"].ToString();

                var readSta = FacadeManage.aideNativeWebFacade.GetNewsState(Convert.ToInt32(notices.PageSet.Tables[0].Rows[0]["NewsId"]), Convert.ToInt32(userId));
                if (readSta.Id > 0)
                {
                    data.IsReaded = true;
                }
            }

            context.Response.Write(LitJson.JsonMapper.ToJson(data));
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