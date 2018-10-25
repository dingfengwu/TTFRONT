using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Kernel;
using Game.Facade;
using Game.Entity.NativeWeb;
using System.Data;


namespace Game.Web.Match
{
    public partial class ActivityInfo : UCPageBase
    {
        public string title = string.Empty;
        public string content = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Game.Entity.NativeWeb.Activity model = FacadeManage.aideNativeWebFacade.GetActivity(IntParam);
            if (model != null)
            {
                title = model.Title;
                content = Utility.HtmlDecode(model.Describe);
            }
        }
    }
}