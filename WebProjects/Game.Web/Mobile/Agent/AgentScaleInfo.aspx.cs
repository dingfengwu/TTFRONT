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

namespace Game.Web.Mobile.Agent
{
    public partial class AgentScaleInfo : UCPageBase
    {
        #region Fields

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
                ShowAndRedirect("结算成功!", "/Mobile/Agent/AgentScaleInfo.aspx");
            }
            else
            {
                ShowAndRedirect(umsg.Content, "/Mobile/Agent/AgentScaleInfo.aspx");
            }
        }
    }
}