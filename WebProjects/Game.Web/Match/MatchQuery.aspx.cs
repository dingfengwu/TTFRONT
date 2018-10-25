using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Entity.GameMatch;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;
using System.Data;

namespace Game.Web.Match
{
    public partial class MatchQuery : UCPageBase
    {
        public string matchID = "";

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
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-01");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindData();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("比赛成绩 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定比赛记录
        /// </summary>
        private void BindData()
        {
            StringBuilder sWhere = new StringBuilder();
            sWhere.AppendFormat(" WHERE UserID={0}", Fetch.GetUserCookie().UserID, matchID);
            sWhere.AppendFormat(" AND MatchStartTime>='{0}' AND MatchStartTime<='{1}'", Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00"), Convert.ToDateTime(CtrlHelper.GetTextAndFilter(txtEndDate) + " 23:59:59"));

            int sPageIndex = anpPage.CurrentPageIndex;
            int sPageSize = anpPage.PageSize;
            string orderQuery = "ORDER BY RecordDate DESC";
            PagerSet ps = FacadeManage.aideGameMatchFacade.GetList(StreamMatchHistory.Tablename, sPageIndex, sPageSize, orderQuery, sWhere.ToString());
            if (ps.PageSet.Tables[0].Rows.Count > 0)
            {
                //查询所有比赛
                DataTable dtMatchPublic = FacadeManage.aideGameMatchFacade.GetMatchPublicList().Tables[0];
                DataTable dtRecord = ps.PageSet.Tables[0];
                dtRecord.Columns.Add("MatchName");
                for (int i = 0; i < dtRecord.Rows.Count; i++)
                {
                    DataRow[] rows = dtMatchPublic.Select(string.Format("MatchID={0}", dtRecord.Rows[i]["MatchID"]));
                    if (rows.Length > 0)
                    {
                        dtRecord.Rows[i]["MatchName"] = rows[0]["MatchName"];
                    }
                    else
                    {
                        dtRecord.Rows[i]["MatchName"] = "";
                    }
                }
                rptRank.DataSource = dtRecord;
                rptRank.DataBind();
                anpPage.RecordCount = ps.RecordCount;
                litNoData.Visible = false;
            }
            else
            {
                litNoData.Visible = true;
            }

            if (ps.RecordCount < sPageSize)
            {
                anpPage.Visible = false;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void anpPage_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            anpPage.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }
    }
}