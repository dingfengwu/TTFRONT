using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Entity.NativeWeb;
using Game.Utils;

namespace Game.Web.Mobile
{
    public partial class Index : System.Web.UI.Page
    {
        // 客户端类型
        protected int terminalType = 0;

        protected string tel = string.Empty;
        protected string qq = string.Empty;

        protected string title = string.Empty;

        protected string resourceURL = string.Empty;
        protected string targetURL = string.Empty;

        protected string platformIntro = string.Empty;
        protected string platformDownloadUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            terminalType = Fetch.GetTerminalType(Page.Request);
            ConfigInfo ciAndroid = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameAndroidConfig.ToString());
            ConfigInfo ciIos = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameIosConfig.ToString());
            if (ciAndroid.Field1 != "" || ciIos.Field1 != "")
            {
                plPlatform.Visible = true;
                if (terminalType == 1)
                {
                    platformDownloadUrl = ciAndroid.Field1;
                }
                else
                {
                    platformDownloadUrl = ciIos.Field1;
                }
            }

            BindMoblieGame();

            ConfigInfo config = FacadeManage.aideNativeWebFacade.GetConfigInfo("ContactConfig");
            if(config != null)
            {
                qq = config.Field1;
                tel = config.Field3;
            }

            title = ApplicationSettings.Get("title");

            Ads ads = FacadeManage.aideNativeWebFacade.GetAds((int)AppConfig.AdsGameType.移动版网站广告位);
            if(ads != null)
            {
                resourceURL = Fetch.GetUploadFileUrl(ads.ResourceURL);
                targetURL = ads.LinkURL;
            }
        }

        /// <summary>
        /// 绑定手机游戏
        /// </summary>
        private void BindMoblieGame()
        {
            rptMoblieGame.DataSource = FacadeManage.aideNativeWebFacade.GetMoblieGame();
            rptMoblieGame.DataBind();
        }
    }
}