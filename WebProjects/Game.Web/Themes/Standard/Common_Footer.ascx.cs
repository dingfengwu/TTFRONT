using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Themes.Standard
{
    public partial class Common_Footer : System.Web.UI.UserControl
    {
        protected string contents = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigInfo model = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.SiteConfig.ToString());
            if (model != null)
            {
                contents = Utility.HtmlDecode(model.Field8);
            }
        }
    }
}