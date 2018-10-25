using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Mobile.Shop
{
    public partial class Buy : UCPageBase
    {
        #region 继承属性

        protected override bool IsAuthenticatedUser
        {
            get
            {
                return true;
            }
        }

        #endregion

        protected string imgUrl = string.Empty;
        protected int price = 0;
        public string name = string.Empty;
        public int inventory = 0;
        public int userMedals = 0;

        protected string realName = string.Empty;           //会员真实姓名
        protected string phone = string.Empty;              //电话号码
        protected int province = -1;                        //身份
        protected int city = -1;                            //城市    
        protected int area = -1;                            //地区
        protected string address = string.Empty;            //详细地址

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IntParam == 0)
                Response.Redirect("/Mobile/Shop404.html");

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(IntParam);
            if(awardInfo == null)
                Response.Redirect("/Mobile/Shop404.html");

            imgUrl = Fetch.GetUploadFileUrl(awardInfo.SmallImage);
            price = awardInfo.Price;
            name = awardInfo.AwardName;
            inventory = awardInfo.Inventory;

            Message umsg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
            UserInfo user = umsg.EntityList[0] as UserInfo;
            userMedals = user.UserMedal;
            realName = user.Compellation;

            IndividualDatum individualDatum = FacadeManage.aideAccountsFacade.GetUserContactInfoByUserID(Fetch.GetUserCookie().UserID);
            phone = individualDatum.MobilePhone;
            address = individualDatum.DwellingPlace;
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