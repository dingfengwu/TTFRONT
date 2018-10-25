using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;
using Game.Entity.NativeWeb;

namespace Game.Web.Service
{
    public partial class Course : UCPageBase
    {
        public SinglePage singlePage = new SinglePage();
        protected string contents = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            singlePage = FacadeManage.aideNativeWebFacade.GetSinglePage(AppConfig.SinglePageKey.NewbieHelp.ToString());
            contents = Game.Utils.Utility.HtmlDecode(singlePage.Contents);
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("新手教程 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(string.IsNullOrEmpty(singlePage.KeyWords) ? ApplicationSettings.Get("keywords") : singlePage.KeyWords);
            AddMetaDescription(string.IsNullOrEmpty(singlePage.Description) ? ApplicationSettings.Get("description") : singlePage.Description);
        }
    }
}