using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using Game.Entity.NativeWeb;

namespace Game.Web.Shop
{
    public partial class OrderInfo : UCPageBase
    {
        protected string compellation = string.Empty;       //收货人姓名
        protected int orderStatus = 0;                      //订单状态
        protected string mobilePhone = string.Empty;        //手机号码
        protected string qq = string.Empty;                 //QQ号码
        protected int province = -1;                        //省份
        protected int city = -1;                            //城市
        protected int area = -1;                            //地区
        protected string dwellingPlace = string.Empty;      //详细地址
        protected string postalCode = string.Empty;         //邮编
        protected int counts = 1;                           //购买数量
        protected int totalAmount = 0;                      //花费金额
        protected DateTime buyDate = DateTime.Now;          //购买时间 
        protected int awardID = 0;                          //商品标识

        protected string awardName = string.Empty;          //商品名称
        protected string smallImage = string.Empty;         //展示大图
        protected int price = 0;                            //奖牌价格


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IntParam == 0)
            {
                return;
            }

            //订单信息
            AwardOrder awardOrder = FacadeManage.aideNativeWebFacade.GetAwardOrder(IntParam, Fetch.GetUserCookie().UserID);
            if (awardOrder == null)
            {
                return;
            }
            compellation = awardOrder.Compellation;
            orderStatus = awardOrder.OrderStatus;
            mobilePhone = awardOrder.MobilePhone;
            qq = awardOrder.QQ;
            province = awardOrder.Province;
            city = awardOrder.City;
            area = awardOrder.Area;
            dwellingPlace = awardOrder.DwellingPlace;
            postalCode = awardOrder.PostalCode;
            counts = awardOrder.AwardCount;
            totalAmount = awardOrder.TotalAmount;
            buyDate = awardOrder.BuyDate;
            awardID = awardOrder.AwardID;

            //商品信息

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(awardOrder.AwardID);
            if (awardInfo != null)
            {
                awardName = awardInfo.AwardName;
                smallImage = awardInfo.SmallImage;
                price = awardInfo.Price;

                //收货人信息
                int needInfo = awardInfo.NeedInfo;
                int qqValue = (int)AppConfig.AwardNeedInfoType.QQ号码;
                int nameValue = (int)AppConfig.AwardNeedInfoType.真实姓名;
                int phoneValue = (int)AppConfig.AwardNeedInfoType.手机号码;
                int addressValue = (int)AppConfig.AwardNeedInfoType.收货地址及邮编;

                if ((needInfo & addressValue) == addressValue)
                {
                    plAddress.Visible = true;
                }
                if ((needInfo & nameValue) == nameValue)
                {
                    liName.Visible = true;
                }
                if ((needInfo & phoneValue) == phoneValue)
                {
                    liPhone.Visible = true;
                }
                if ((needInfo & qqValue) == qqValue)
                {
                    liQQ.Visible = true;
                }
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("订单详情 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}