using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using System.Data;

namespace Game.Web.Rank
{
    public partial class GradeRank : UCPageBase
    {
        public DataSet dsGradeConfig = new DataSet();   // 等级配置
        public int currentRankId = 1;                   // 当前排名

        protected void Page_Load(object sender, EventArgs e)
        {
            dsGradeConfig = FacadeManage.aidePlatformFacade.GetGrowLevelConfigList();
            BindRankData();
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("等级排行 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindRankData()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetExperienceRank(100);
            if (ds.Tables[0].Rows.Count > 0)
            {                
                rptRank.DataSource = ds;
                rptRank.DataBind();
            }
        }

        public int GetGradeConfig(int Experience)
        {
            DataView dv = (from n in dsGradeConfig.Tables[0].AsEnumerable()
                           where n.Field<int>("Experience") < Experience
                           orderby n.Field<int>("Experience") descending
                           select n).AsDataView();
            if (dv.Count > 0)
                return Convert.ToInt32(dv[0]["LevelID"]);
            else
                return 1;
        }
    }
}