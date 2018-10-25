using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Facade;
using Game.Utils;
using Game.Entity.Treasure;
using Game.Kernel;
using System.Data;
using System.Web.Script.Serialization;

namespace Game.Web.AppPay
{
    public partial class ProductList : System.Web.UI.Page
    {
        #region Fields

        TreasureFacade treasureFacade = new TreasureFacade();

        #endregion

        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Response.Write(PayApp());
                Response.End();
            }
        }
        #endregion

        #region 公共方法

        private string PayApp()
        {
            DataSet ds = treasureFacade.GetAppList();
            IList<GlobalAppInfo> list = Game.Utils.DataHelper.ConvertDataTableToObjects<GlobalAppInfo>(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string rValue = jss.Serialize(list);
            return rValue;
        }

        #endregion
    }
}
