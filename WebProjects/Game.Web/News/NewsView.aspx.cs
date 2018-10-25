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

namespace Game.Web.News
{
    public partial class NewsView : UCPageBase
    {
        public string source = "";
        public string type = "";
        public string issueDate = "";
        public string title = "";
        public string content = "";

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Entity.NativeWeb.News news = FacadeManage.aideNativeWebFacade.GetNewsByNewsID(IntParam, 0);
                if (news != null)
                {
                    source = news.IsLinks == 1 ? news.LinkUrl : ApplicationSettings.Get("title");
                    type = news.ClassID == 1 ? "新闻" : "公告";
                    issueDate = news.IssueDate.ToString("yyyy-MM-dd HH:mm:ss");
                    title = news.Subject;
                    content = Utility.HtmlDecode(news.Body);

                    Entity.NativeWeb.News news1 = FacadeManage.aideNativeWebFacade.GetNewsByNewsID(IntParam, 1);
                    if (news1 != null)
                    {
                        this.next1.Title = "上一篇新闻：" + news1.Subject;
                        this.next1.HRef = "NewsView.aspx?param=" + news1.NewsID;
                    }
                    else
                    {
                        this.next1.Title = "已经是第一篇了！";
                        this.next1.Disabled = true;
                        this.next1.Attributes.Add("class", "ui-news-text-prev ui-no-page");
                    }

                    Entity.NativeWeb.News news2 = FacadeManage.aideNativeWebFacade.GetNewsByNewsID(IntParam, 2);
                    if (news2 != null)
                    {
                        this.last1.Title = "下一篇新闻：" + news2.Subject;
                        this.last1.HRef = "NewsView.aspx?param=" + news2.NewsID;

                    }
                    else
                    {
                        this.last1.Title = "已经是最后一篇了！";
                        this.last1.Disabled = true;
                        this.last1.Attributes.Add("class", "ui-news-text-next ui-no-page");

                    }
                }
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            Entity.NativeWeb.News news = FacadeManage.aideNativeWebFacade.GetNewsByNewsID(IntParam, 0);
            if (news != null)
            {
                AddMetaTitle(news.Subject + " - " + ApplicationSettings.Get("title"));
            }
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}