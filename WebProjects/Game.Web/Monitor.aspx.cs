using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web
{
    public partial class Monitor : UCPageBase
    {
        public SinglePage singlePage = new SinglePage();
        protected string contents = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            singlePage = FacadeManage.aideNativeWebFacade.GetSinglePage(AppConfig.SinglePageKey.Monitor.ToString());
            contents = Game.Utils.Utility.HtmlDecode(singlePage.Contents);
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("家长监控工程 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(string.IsNullOrEmpty(singlePage.KeyWords) ? ApplicationSettings.Get("keywords") : singlePage.KeyWords);
            AddMetaDescription(string.IsNullOrEmpty(singlePage.Description) ? ApplicationSettings.Get("description") : singlePage.Description);
        }
    }
}