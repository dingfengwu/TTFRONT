using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Member
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
        //分成信息
        protected string childCount = string.Empty;
        protected string agentRevenue = string.Empty;
        protected string agentPay = string.Empty;
        protected string agentPayBack = string.Empty;
        protected string agentIn = string.Empty;
        protected string agentOut = string.Empty;
        protected string agentRemain = string.Empty;
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
                //判断是否为代理
                UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserBaseInfoByUserID(userTicket.UserID);
                if (userInfo.AgentID == 0)
                {
                    Response.Redirect("/Member/Index.aspx");
                }

                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
                if (umsg.Success)
                {
                    UserInfo ui = umsg.EntityList[0] as UserInfo;
                    accounts = ui.Accounts;
                }

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
                //分成信息
                childCount = FacadeManage.aideAccountsFacade.GetAgentChildCount(Fetch.GetUserCookie().UserID).ToString();
                DataSet ds = FacadeManage.aideTreasureFacade.GetAgentFinance(Fetch.GetUserCookie().UserID);
                agentRevenue = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentRevenue"]).ToString();
                agentPay = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentPay"]).ToString();
                agentPayBack = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentPayBack"]).ToString();
                agentIn = (Convert.ToInt64(ds.Tables[0].Rows[0]["AgentRevenue"]) + Convert.ToInt64(ds.Tables[0].Rows[0]["AgentPay"]) + Convert.ToInt64(ds.Tables[0].Rows[0]["AgentPayBack"])).ToString();
                agentOut = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentOut"]).ToString();
                agentRemain = (Convert.ToInt64(agentIn) - Convert.ToInt64(agentOut)).ToString();

                txtScore.Text = agentRemain.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Message umsg = FacadeManage.aideTreasureFacade.GetAgentBalance(Utility.StrToInt(this.txtScore.Text.Trim(), 0), Fetch.GetUserCookie().UserID, GameRequest.GetUserIP());
            if (umsg.Success)
            {
                ShowAndRedirect("结算成功!", "/Member/AgentInfo.aspx");
            }
            else
            {
                ShowAndRedirect(umsg.Content, "/Member/AgentInfo.aspx");
            }
        }
    }
}