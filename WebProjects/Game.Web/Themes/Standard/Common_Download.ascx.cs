using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;

namespace Game.Web.Themes.Standard
{
    public partial class Common_Download : System.Web.UI.UserControl
    {
        protected string fullDownloadURL = string.Empty;
        protected string janeDownloadURL = string.Empty;
        protected string androidDownloadURL = string.Empty;
        protected string iosDownloadURL = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGameInfo();
            }
        }

        /// <summary>
        /// 绑定大厅信息
        /// </summary>
        private void BindGameInfo()
        {
            ConfigInfo ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameFullPackageConfig.ToString());
            if (ci != null)
            {
                fullDownloadURL = ci.Field1;
            }

            ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameJanePackageConfig.ToString());
            if (ci != null)
            {
                janeDownloadURL = ci.Field1;
            }

            ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameAndroidConfig.ToString());
            if (ci != null)
            {
                androidDownloadURL = ci.Field1;
            }

            ci = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameIosConfig.ToString());
            if (ci != null)
            {
                iosDownloadURL = ci.Field1;
            }
        }
    }
}