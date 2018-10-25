using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Mobile.Agent
{
    public partial class AgentInfo : UCPageBase
    {
        #region Fields

        //代理信息
        protected string accounts = string.Empty;
        protected string agentID = string.Empty;
        protected string compellation = string.Empty;
        protected string domain = string.Empty;
        protected string agentType = string.Empty;
        protected string agentScale = string.Empty;
        protected string payBackScore = string.Empty;
        protected string payBackScale = string.Empty;
        protected string mobilePhone = string.Empty;
        protected string eMail = string.Empty;
        protected string dwellingPlace = string.Empty;
        protected string collectDate = string.Empty;
        #endregion

        #region 继承属性

        protected override bool IsAuthenticatedUser
        {
            get
            {
                return true;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //代理信息
                AccountsAgent agent = FacadeManage.aideAccountsFacade.GetAccountAgentByUserID(Fetch.GetUserCookie().UserID);
                if (agent.AgentID != 0)
                {
                    agentID = agent.AgentID.ToString();
                    compellation = agent.Compellation;
                    domain = agent.Domain;
                    agentType = agent.AgentType == 1 ? "充值分成" : "税收分成";
                    agentScale = Convert.ToInt32(agent.AgentScale * 1000).ToString() + "‰";
                    payBackScore = agent.PayBackScore.ToString();
                    payBackScale = Convert.ToInt32(agent.PayBackScale * 1000).ToString() + "‰";
                    mobilePhone = agent.MobilePhone;
                    eMail = agent.EMail;
                    dwellingPlace = agent.DwellingPlace;
                    collectDate = agent.CollectDate.ToString("yyyy-MM-dd");
                }                
            }
        }
    }
}