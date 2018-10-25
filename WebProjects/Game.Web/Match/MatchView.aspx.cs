using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;
using System.Data.Common;
using Game.Entity.GameMatch;
using System.Data;

namespace Game.Web.Match
{
    public partial class MatchView : UCPageBase
    {
        public string content = string.Empty;
        public int matchNo = GameRequest.GetQueryInt("num", 0);
        public int type = GameRequest.GetQueryInt("type", 0);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (MatchInfoModel != null)
                    content = Game.Utils.Utility.HtmlDecode(MatchInfoModel.MatchContent);

                // 奖励配置
                BindRewardData();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            if (MatchInfoModel != null)
            {
                AddMetaTitle(MatchInfoModel.MatchName + " - " + ApplicationSettings.Get("title"));
            }
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定奖励配置
        /// </summary>
        protected void BindRewardData()
        {
            DataSet ds = FacadeManage.aideGameMatchFacade.GetMatchRewardList(IntParam);
            rptReward.DataSource = ds;
            rptReward.DataBind();
        }

        /// <summary>
        /// 比赛展示实体
        /// </summary>
        protected MatchInfo MatchInfoModel
        {
            get
            {
                return FacadeManage.aideGameMatchFacade.GetMatchInfo(IntParam);
            }
        }
    }
}