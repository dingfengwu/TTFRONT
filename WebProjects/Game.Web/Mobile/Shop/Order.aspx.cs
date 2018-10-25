using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Mobile.Shop
{
    public partial class Order : UCPageBase
    {
        protected string imgUrl = string.Empty;
        protected int price = 0;
        public string name = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IntParam == 0)
                Response.Redirect("/Mobile/Shop404.html");

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(IntParam);
            if (awardInfo == null)
                Response.Redirect("/Mobile/Shop404.html");

            imgUrl = Fetch.GetUploadFileUrl(awardInfo.SmallImage);
            price = awardInfo.Price;
            name = awardInfo.AwardName;
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("商城 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}