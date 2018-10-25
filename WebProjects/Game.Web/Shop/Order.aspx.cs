using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Facade;
using System.Text;
using Game.Kernel;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;

namespace Game.Web.Shop
{
    public partial class Order : UCPageBase
    {
        protected int ProcessingCount = 0;              //处理中订单数
        protected int typeID = 0;

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
                BindOrderList();
            }
        }

        //绑定订单
        protected void BindOrderList()
        {
            //查询条件
            typeID = GameRequest.GetQueryInt("TypeID", 0);
            string where = string.Format(" WHERE UserID={0}", Fetch.GetUserCookie().UserID);
            switch (IntParam)
            {
                case 1:
                    where += " AND OrderStatus=0 ";
                    break;
                case 2:
                    where += " AND( OrderStatus=1 OR OrderStatus=2)";
                    break;
                case 3:
                    where += " AND( OrderStatus=3 OR OrderStatus=4 OR OrderStatus=5 OR OrderStatus=6)";
                    break;
                default:
                    break;
            }

            //绑定数据
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetOrderList(PageIndex, anpPage.PageSize, where, " ORDER BY BuyDate DESC");
            anpPage.RecordCount = pagerSet.RecordCount;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptData.DataSource = pagerSet.PageSet;
                rptData.DataBind();
                litNoData.Visible = false;
            }
            else
            {
                litNoData.Visible = true;
            }

            //处理中订单数
            ProcessingCount = FacadeManage.aideNativeWebFacade.GetProcessingOrderCount(Fetch.GetUserCookie().UserID);
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("我的订单 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}