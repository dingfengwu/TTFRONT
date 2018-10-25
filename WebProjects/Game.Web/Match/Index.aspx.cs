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

namespace Game.Web.Match
{
    public partial class Index : UCPageBase
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMatchData();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("比赛中心 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定比赛信息
        /// </summary>
        private void BindMatchData()
        {
            int sPageSize = anpPage.PageSize;
            PagerSet pagerSet = FacadeManage.aideGameMatchFacade.GetMatchList(PageIndex, sPageSize);
            this.rptMatchList.DataSource = pagerSet.PageSet;
            this.rptMatchList.DataBind();
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.RecordCount < sPageSize)
            {
                anpPage.Visible = false;
            }
        }

        /// <summary>
        /// 获取比赛状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetMatchStatus(byte status)
        {
            string rValue = "";
            switch (status)
            {
                case 0:
                    rValue = "match_state_1.png";
                    break;
                case 2:
                    rValue = "match_state_2.png";
                    break;
                case 8:
                    rValue = "match_state_3.png";
                    break;
                default:
                    rValue = "match_state_1.png";
                    break;
            }
            return rValue;
        }

        /// <summary>
        /// 获取比赛图片路径
        /// </summary>
        /// <param name="matchImage"></param>
        /// <returns></returns>
        protected string GetMatchImageUrl(string matchImage)
        {
            if (matchImage == "")
            {
                return "/images/match/default.png";
            }
            else
            {
                return Game.Facade.Fetch.GetUploadFileUrl(matchImage);
            }
        }
    }
}