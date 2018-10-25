using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;


namespace Game.Web.Member
{
    public partial class InsureTransfer : UCPageBase
    {
        protected double MinTradeScore = 0;   //最低转账金额

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
                }
            }

            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("select {0} from {1} where {2}='{3}'", SystemStatusInfo._StatusValue, SystemStatusInfo.Tablename, SystemStatusInfo._StatusName, "TransferPrerequisite");
            MinTradeScore = Convert.ToInt32(FacadeManage.aideAccountsFacade.GetObjectBySql(sqlQuery.ToString()));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int userId = 0;

            if (this.radType1.Checked)
            {
                userId = FacadeManage.aideAccountsFacade.GetUserIDByNickName(txtUser.Text.Trim());
                if (userId == 0)
                {
                    Show("您输入的用户昵称错误，请重新输入！");
                    this.txtUser.Text = "";
                    this.txtUser.Focus();
                    return;
                }
            }
            else
            {
                Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(0, Utility.StrToInt(txtUser.Text.Trim(), 0), "");
                if (!umsg.Success)
                {
                    Show("您输入的游戏ID号码错误，请重新输入！");
                    this.txtUser.Text = "";
                    this.txtUser.Focus();
                    return;
                }

                UserInfo user = umsg.EntityList[0] as UserInfo;
                userId = user.UserID;
            }

            if (!Game.Utils.Validate.IsPositiveInt64(txtScore.Text.Trim()))
            {
                Show("请输入正确的金额");
                return;
            }
            Int64 score = Convert.ToInt64(txtScore.Text.Trim());
            string note = CtrlHelper.GetTextAndFilter(txtNote);
            if (score <= 0)
            {
                Show("转账金额必须大于0");
                return;
            }

            Message msg = FacadeManage.aideTreasureFacade.InsureTransfer(Fetch.GetUserCookie().UserID, TextEncrypt.EncryptPassword(CtrlHelper.GetTextAndFilter(txtInsurePass)), userId, score, GameRequest.GetUserIP(), note);
            if (msg.Success)
            {
                ShowAndRedirect(msg.Content, "/Member/InsureTransfer.aspx");
            }
            else
            {
                Show(msg.Content);
            }
        }
    }
}