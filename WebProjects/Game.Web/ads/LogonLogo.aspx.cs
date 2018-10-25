using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Entity.NativeWeb;

namespace Game.Web.ads
{
    public partial class LogonLogo : System.Web.UI.Page
    {
        protected string resourceURL = string.Empty;
        protected string targetURL = string.Empty;

        protected void Page_Load( object sender, EventArgs e )
        {
            Ads ads = FacadeManage.aideNativeWebFacade.GetAds( (int)AppConfig.AdsGameType.大厅登录框广告位 );
            if( ads != null )
            {
                resourceURL = Fetch.GetUploadFileUrl( ads.ResourceURL );
                targetURL = ads.LinkURL;
            }
        }
    }
}
