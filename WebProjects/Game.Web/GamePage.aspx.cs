using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Facade;
using Game.Utils;


namespace Game.Web
{
    public partial class GamePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pageID = GameRequest.GetInt("PageID", 0);
            string sql = "Select ResponseUrl From GamePageItem(nolock) Where PageID=" + pageID;
            object obj = FacadeManage.aidePlatformFacade.GetObjectBySql(sql);
            if (obj != null)
            {
                string url = obj.ToString();
                Response.Redirect(url);
            }
        }
    }
}