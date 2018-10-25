using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Themes.Standard
{
    public partial class Common_Header : System.Web.UI.UserControl
    {
        public int PageID = 0;
        public string title = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}