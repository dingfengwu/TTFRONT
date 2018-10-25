using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web
{
    public partial class DownSimply : System.Web.UI.Page
    {
        protected string janeDownloadURL = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigInfo ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameJanePackageConfig.ToString());
            if (ci != null)
            {
                janeDownloadURL = ci.Field1;
            }

            //检查代理域名
            string agentDomain = Request.Url.Authority;
            AccountsAgent agent = FacadeManage.aideAccountsFacade.GetAccountAgentByDomain(agentDomain);
            if (agent.AgentID == 0)
            {
                Response.Redirect(janeDownloadURL);
            }
            else
            {
                string filePath = Server.MapPath(janeDownloadURL);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    //下载
                    string fileName = file.Name;
                    byte[] info = ReadFileReturnBytes(filePath);
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + agent.AgentID + "_" + fileName.Split('.')[0] + "." + fileName.Split('.')[1]);
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    Response.BinaryWrite(info);
                }
                else
                {
                    Response.Redirect(janeDownloadURL);
                }
            }
        }

        public static byte[] ReadFileReturnBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader bReader = new BinaryReader(fStream);
            byte[] buffer = bReader.ReadBytes((int)fStream.Length);

            fStream.Flush();
            fStream.Close();
            bReader.Close();

            return buffer;
        }
    }
}