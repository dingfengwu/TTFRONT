using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web
{
    public partial class Notice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindNotice();
        }

        private void BindNotice()
        {
            this.rptNotice.DataSource = FacadeManage.aideNativeWebFacade.GetTopNewsList(0, 0, 0, 10);
            this.rptNotice.DataBind();
        }
    }
}