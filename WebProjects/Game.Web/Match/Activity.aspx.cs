using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Kernel;
using Game.Facade;
using Game.Entity.NativeWeb;
using System.Data;

namespace Game.Web.Match
{
    public partial class Activity : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }                
        }

        private void BindData()
        {
            DataSet ds = FacadeManage.aideNativeWebFacade.GetList("Activity", 1, 1000, " ORDER BY SortID ASC,InputDate DESC", "").PageSet;
            rptData.DataSource = ds;
            rptData.DataBind();

            if (ds.Tables[0].Rows.Count < anpPage.PageSize)
            {
                anpPage.Visible = false;
            }
        }
    }
}