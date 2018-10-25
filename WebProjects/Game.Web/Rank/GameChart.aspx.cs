using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;
using System.Data;

namespace Game.Web.Rank
{
    public partial class GameChart : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRankData();
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("玩家财富排行 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindRankData()
        {
            DataSet ds = FacadeManage.aideTreasureFacade.GetScoreRanking(100);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptRank.DataSource = ds;
                rptRank.DataBind();
            }
        }
    }
}