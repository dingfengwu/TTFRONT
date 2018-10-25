using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;

namespace Game.Web.Shop
{
    public partial class Buy : UCPageBase
    {
        protected string awardName = string.Empty;          //商品名称
        protected string smallImage = string.Empty;         //展示大图
        protected int price = 0;                            //奖牌价格

        protected string realName = string.Empty;           //会员真实姓名
        protected string phone = string.Empty;              //电话号码
        protected int province = -1;                        //身份
        protected int city = -1;                            //城市    
        protected int area = -1;                            //地区
        protected string address = string.Empty;            //详细地址
        protected int userMedal = 0;                        //奖牌数量
        protected string pageName = string.Empty;           //面包屑名称

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
            if (IntParam == 0)
            {
                return;
            }

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(IntParam);
            if (awardInfo != null)
            {
                awardName = awardInfo.AwardName;
                smallImage = Fetch.GetUploadFileUrl(awardInfo.BigImage);
                price = awardInfo.Price;

                //商品购买需要填写的资料
                int needInfo = awardInfo.NeedInfo;
                int qqValue = (int)AppConfig.AwardNeedInfoType.QQ号码;
                int nameValue = (int)AppConfig.AwardNeedInfoType.真实姓名;
                int phoneValue = (int)AppConfig.AwardNeedInfoType.手机号码;
                int addressValue = (int)AppConfig.AwardNeedInfoType.收货地址及邮编;

                if ((needInfo & addressValue) == addressValue)
                {
                    divAddress.Visible = true;
                }
                if ((needInfo & nameValue) == nameValue)
                {
                    divRealName.Visible = true;
                }
                if ((needInfo & phoneValue) == phoneValue)
                {
                    divPhone.Visible = true;
                }
                if ((needInfo & qqValue) == qqValue)
                {
                    divQQ.Visible = true;
                }

                //绑定会员信息
                UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "").EntityList[0] as UserInfo;
                realName = userInfo.Compellation;
                userMedal = userInfo.UserMedal;
                IndividualDatum individualDatum = FacadeManage.aideAccountsFacade.GetUserContactInfoByUserID(Fetch.GetUserCookie().UserID);
                phone = individualDatum.MobilePhone;
                address = individualDatum.DwellingPlace;

                //面包屑
                AwardType awardType = FacadeManage.aideNativeWebFacade.GetAwardType(awardInfo.TypeID);
                if (awardType != null)
                {
                    pageName = awardType.TypeName;
                    if (awardType.ParentID != 0)
                    {
                        awardType = FacadeManage.aideNativeWebFacade.GetAwardType(awardType.ParentID);
                        pageName = awardType.TypeName + " -> " + pageName;
                    }
                }
                pageName += " -> " + awardInfo.AwardName;
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("提交订单 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}