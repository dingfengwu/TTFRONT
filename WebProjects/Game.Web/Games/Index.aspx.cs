using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Games
{
    public partial class Index : UCPageBase
    {
        protected string fullDownloadURL = string.Empty;
        protected string janeDownloadURL = string.Empty;
        protected string androidDownloadURL = string.Empty;
        protected string iosDownloadURL = string.Empty;

        protected int isShowMoblieDownload = 0;
        protected string domain = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                domain = "http://" + Request.Url.Authority.ToString();
                isShowMoblieDownload = AppConfig.IsShowMoblieDownload;
                BindGameInfo();
                BindGameList();
                BindMoblieGame();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("游戏列表 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
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

        /// <summary>
        /// 绑定推荐游戏
        /// </summary>
        private void BindGameList()
        {
            rptGame.DataSource = FacadeManage.aideNativeWebFacade.GetGameHelps(30);
            rptGame.DataBind();
        }

        /// <summary>
        /// 绑定手机游戏
        /// </summary>
        private void BindMoblieGame()
        {
            if (isShowMoblieDownload == 1)
            {
                rptMoblieGame.DataSource = FacadeManage.aideNativeWebFacade.GetMoblieGame();
                rptMoblieGame.DataBind();
            }
        }
    }
}