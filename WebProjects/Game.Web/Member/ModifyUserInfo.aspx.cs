using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using Game.Kernel;

namespace Game.Web.Member
{
    public partial class ModifyUserInfo : UCPageBase
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
                UserInfo user = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "").EntityList[0] as UserInfo;
                this.lblAccounts.Text = user.Accounts;
                lblGameID.Text = user.GameID.ToString();
                ddlGender.SelectedValue = user.Gender.ToString();
                txtUnderWrite.Text = user.UnderWrite;
                txtCompellation.Text = user.Compellation;

                IndividualDatum contact = FacadeManage.aideAccountsFacade.GetUserContactInfoByUserID(Fetch.GetUserCookie().UserID);
                txtAddress.Text = contact.DwellingPlace;
                txtEmail.Text = contact.EMail;
                txtMobilePhone.Text = contact.MobilePhone;
                txtQQ.Text = contact.QQ;
                txtSeatPhone.Text = contact.SeatPhone;
                txtUserNote.Text = contact.UserNote;
            }
        }

        /// <summary>
        /// 更新用户资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            AccountsInfo info = new AccountsInfo();
            info.Gender = Convert.ToByte(ddlGender.SelectedValue);
            info.UnderWrite = CtrlHelper.GetTextAndFilter(txtUnderWrite);

            IndividualDatum contact = new IndividualDatum();
            contact.MobilePhone = TextFilter.FilterScript(txtMobilePhone.Text.Trim());
            contact.SeatPhone = TextFilter.FilterScript(txtSeatPhone.Text.Trim());
            contact.QQ = TextFilter.FilterScript(txtQQ.Text.Trim());
            contact.EMail = TextFilter.FilterScript(txtEmail.Text.Trim());
            contact.DwellingPlace = CtrlHelper.GetTextAndFilter(txtAddress);
            contact.UserNote = CtrlHelper.GetTextAndFilter(txtUserNote);

            contact.UserID = Fetch.GetUserCookie().UserID;

            Message umsg = FacadeManage.aideAccountsFacade.ModifyUserIndividual(contact, info);
            if (umsg.Success)
            {
                ShowAndRedirect("个人资料修改成功!", "/Member/ModifyUserInfo.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}