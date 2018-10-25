using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Treasure;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;

namespace Game.Web.Member
{
    public partial class InsureOut : UCPageBase
    {
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
                GameScoreInfo scoreInfo = FacadeManage.aideTreasureFacade.GetTreasureInfo2(Fetch.GetUserCookie().UserID);
                if (scoreInfo != null)
                {
                    this.lblInsureScore.Text = scoreInfo.InsureScore.ToString();
                    this.lblScore.Text = scoreInfo.Score.ToString();
                    this.txtScore.Text = scoreInfo.InsureScore.ToString();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Game.Utils.Validate.IsPositiveInt64(txtScore.Text.Trim()))
            {
                Show("请输入正确的金额");
                return;
            }
            Int64 score = Convert.ToInt64(txtScore.Text.Trim());
            if (score <= 0)
            {
                Show("取出金额必须大于0");
                return;
            }

            string note = CtrlHelper.GetTextAndFilter(txtNote);
            StringBuilder sqlQuery = new StringBuilder();
            Message umsg = FacadeManage.aideTreasureFacade.InsureOut(Fetch.GetUserCookie().UserID, TextEncrypt.EncryptPassword(CtrlHelper.GetTextAndFilter(txtInsurePass)), score, GameRequest.GetUserIP(), note);
            if (umsg.Success)
            {
                ShowAndRedirect("取款成功!", "/Member/InsureOut.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}