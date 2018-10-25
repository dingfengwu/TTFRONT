using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.GameWebApplication
{
    /// <summary>
    /// PayWxList 的摘要说明
    /// </summary>
    public class PayWxList : IHttpHandler
    {
        public class CWxCell
        {
            public string WeiXinName;
            public string WeiXinID;
            public CWxCell(string _WeiXinName, string _WeiXinID)
            {
                WeiXinName = _WeiXinName;
                WeiXinID = _WeiXinID;
            }
        }
        public class CWxMgr
        {
            public int code = 0;
            public List<CWxCell> Wxs = new List<CWxCell>();
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            CWxMgr newCWxMgr = new CWxMgr();
            newCWxMgr.code = 0;
            CWxCell newCWxCell1 = new CWxCell("123", "wx456");
            CWxCell newCWxCell2 = new CWxCell("12345", "wx45996");
            newCWxMgr.Wxs.Add(newCWxCell1);
            newCWxMgr.Wxs.Add(newCWxCell2);
            context.Response.Write(LitJson.JsonMapper.ToJson(newCWxMgr));
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