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
    public partial class ModifyLogonPass : UCPageBase
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
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // 验证密码
            string oldPassword = txtOldPass.Text.Trim();
            string newPassword = txtNewPass.Text.Trim();
            if (string.IsNullOrEmpty(oldPassword))
            {
                Show("请输入原密码！");
                return;
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                Show("请输入新密码！");
                return;
            }
            if (oldPassword == newPassword)
            {
                Show("新密码不能与原密码一样！");
                return;
            }

            Message umsg = FacadeManage.aideAccountsFacade.ModifyLogonPasswd(Fetch.GetUserCookie().UserID, TextEncrypt.EncryptPassword(oldPassword), TextEncrypt.EncryptPassword(newPassword), GameRequest.GetUserIP());
            if (umsg.Success)
            {
                Fetch.DeleteUserCookie();
                ShowAndRedirect("登录密码修改成功，请重新登录", "/Login.aspx");
            }
            else
            {
                Show(umsg.Content);
            }
        }
    }
}