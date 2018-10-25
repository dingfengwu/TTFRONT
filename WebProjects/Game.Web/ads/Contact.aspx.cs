using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.ads
{
    public partial class Contact : System.Web.UI.Page
    {
        protected string phone = string.Empty;
        protected string fax = string.Empty;
        protected string email = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigInfo model = FacadeManage.aideNativeWebFacade.GetConfigInfo(AppConfig.SiteConfigKey.ContactConfig.ToString());
            if (model != null)
            {
                phone = model.Field1;
                fax = model.Field2;
                email = model.Field3;
            }
        }
    }
}