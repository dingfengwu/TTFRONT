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
    public partial class ModifyFace : UCPageBase
    {
        public string faceUrl = "";

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
                BindData();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int faceID = Convert.ToInt16(hfFaceID.Value);
            if (faceID == -1)
            {
                Show("请选择头像!");
                BindData();
                return;
            }
            Message umsg = FacadeManage.aideAccountsFacade.ModifyUserFace(Fetch.GetUserCookie().UserID, faceID);
            if (umsg.Success)
            {
                Show("头像修改成功!");
            }
            else
            {
                Show(umsg.Content);
            }
            BindData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
            if (umsg.Success)
            {
                UserInfo ui = umsg.EntityList[0] as UserInfo;
                hfFaceID.Value = "-1";
                faceUrl = FacadeManage.aideAccountsFacade.GetUserFaceUrl(ui.FaceID, ui.CustomID);
            }
        }
    }
}