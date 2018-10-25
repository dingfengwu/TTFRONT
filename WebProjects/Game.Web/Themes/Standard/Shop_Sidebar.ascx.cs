using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Facade;
using System.Data;

namespace Game.Web.Themes.Standard
{
    public partial class Shop_Sidebar : System.Web.UI.UserControl
    {
        protected int typeID = 0;       //分类标识
        public int ShopPageID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            typeID = GameRequest.GetQueryInt("param", 0);

            //绑定商品顶级分类列表
            DataSet ds = FacadeManage.aideNativeWebFacade.GetShopTypeListByParentId(0);
            if (ds.Tables[0].Rows.Count > 0)
            {               
                rptTopType.DataSource = ds;
                rptTopType.DataBind();
            }
        }
    }
}