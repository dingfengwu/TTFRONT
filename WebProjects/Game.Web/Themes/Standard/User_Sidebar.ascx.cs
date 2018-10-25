using Game.Entity.Accounts;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Themes.Standard
{
    public partial class User_Sidebar : System.Web.UI.UserControl
    {
        public int MemberID = 0;
        protected int agentID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserTicketInfo userTicket = Fetch.GetUserCookie();
            if (userTicket != null)
            {
                UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userTicket.UserID);
                agentID = userInfo.AgentID;
            }
        }
    }
}