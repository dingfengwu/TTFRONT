using Game.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web
{
    public partial class UserRank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRankData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindRankData()
        {
            DataSet ds = FacadeManage.aideTreasureFacade.GetScoreRanking(10);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptRank.DataSource = ds;
                rptRank.DataBind();
            }
        }
    }
}