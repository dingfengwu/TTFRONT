using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web
{
    public partial class Index : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserLongout();
                BindAds();
                BindNewsData();
                BindGame();
                BindScoreRank();
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("首页 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定广告
        /// </summary>
        protected void BindAds()
        {
            rptAds.DataSource = FacadeManage.aideNativeWebFacade.GetWebHomeAdsList(100);
            rptAds.DataBind();
        }

        /// <summary>
        /// 绑定新闻列表
        /// </summary>
        private void BindNewsData()
        {
            this.rptNews.DataSource = FacadeManage.aideNativeWebFacade.GetTopNewsList(0, 0, 0, 5);
            this.rptNews.DataBind();
        }

        /// <summary>
        /// 绑定游戏
        /// </summary>
        private void BindGame()
        {
            this.rptGame.DataSource = FacadeManage.aideNativeWebFacade.GetGameHelps(10);
            this.rptGame.DataBind();
        }

        /// <summary>
        /// 绑定财富排行
        /// </summary>
        private void BindScoreRank()
        {
            StringBuilder builderTopView = new StringBuilder();
            StringBuilder builderOtherView = new StringBuilder();
            DataSet ds = FacadeManage.aideTreasureFacade.GetScoreRanking(10);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i < 3)
                    {
                        //前三
                        builderTopView.AppendFormat("<li><p class=\"ui-rank-face\"><img src=\"{0}\"><i class=\"ui-rank-{1}\"></i></p><p>{2}</p><span>{3}</span></li>", FacadeManage.aideAccountsFacade.GetUserFaceUrl(Convert.ToInt32(dt.Rows[i]["UserID"])), i + 1, TextUtility.CutLeft(dt.Rows[i]["NickName"].ToString(),5), GetChinaString(Convert.ToInt64(dt.Rows[i]["Score"])));
                    }
                    else
                    {
                        //其他
                        builderOtherView.AppendFormat("<li class=\"fn-clear\"><span>{2}</span><strong>{0}</strong><p>{1}</p></li>", i + 1, TextUtility.CutLeft(dt.Rows[i]["NickName"].ToString(), 16), dt.Rows[i]["Score"].ToString());
                    }
                }
                litRank.Text = "<ul class=\"ui-rank-topthree fn-clear\">" + builderTopView.ToString() + "</ul>" + "<ul class=\"ui-rank-list\">" + builderOtherView.ToString() + "</ul>";
            }
            else
            {
                litRank.Text = "";
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void UserLongout()
        {
            string logout = GameRequest.GetQueryString("exit");
            if (logout == "true")
            {
                Fetch.DeleteUserCookie();
                Response.Redirect("/Index.aspx");
            }
        }
    }
}