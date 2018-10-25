using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using System.Data;
using Game.Kernel;
using Game.Entity.Accounts;
using Game.Utils;

namespace Game.Web.Mobile.Shop
{
    public partial class Goods : UCPageBase
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
            BindData();            
        }

        private void BindData()
        {
            DataSet ds = FacadeManage.aideNativeWebFacade.GetShopList(int.MaxValue);
            rptData.DataSource = ds;
            rptData.DataBind();
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("商城 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}