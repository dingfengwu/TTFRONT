using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Mobile
{
    public partial class Platform : System.Web.UI.Page
    {
        protected int terminalType = 0;
        protected string platformIntro = string.Empty;
        protected string platformDownloadUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            terminalType = Fetch.GetTerminalType(Page.Request);
            ConfigInfo ciAndroid = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameAndroidConfig.ToString());
            ConfigInfo ciIos = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.GameIosConfig.ToString());
            if (ciAndroid.Field1 != "" || ciIos.Field1 != "")
            {
                if (terminalType == 1)
                {
                    platformDownloadUrl = ciAndroid.Field1;
                }
                else
                {
                    platformDownloadUrl = ciIos.Field1;
                }
            }
            platformIntro = "";

            BindMoblieGame();
        }

        /// <summary>
        /// 绑定手机游戏
        /// </summary>
        private void BindMoblieGame()
        {
            DataSet ds = FacadeManage.aideNativeWebFacade.GetMoblieGame();
            if(ds.Tables[0].Rows.Count > 0)
            {
                rptMoblieGame.DataSource = ds.Tables[0];
                rptMoblieGame.DataBind();
            }
            else
            {
                rptMoblieGame.Visible = false;
                plNotData.Visible = true;
            }
        }
    }
}