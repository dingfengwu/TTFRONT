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
    public partial class Detail : UCPageBase
    {
        protected string awardName = string.Empty;          //商品名称
        protected string bigImage = string.Empty;           //展示大图
        protected string description = string.Empty;        //商品描述
        protected int inventory = 0;                        //库存数量
        protected int price = 0;                            //奖牌价格
        protected Int16 status = 0;                         //商品状态 0:正常 1:下架 
        protected string pageName = string.Empty;           //页名称

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
                bigImage = Fetch.GetUploadFileUrl(awardInfo.BigImage);
                description = Utility.HtmlDecode(awardInfo.Description);
                inventory = awardInfo.Inventory;
                price = awardInfo.Price;
                status = awardInfo.Nullity;

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
            AddMetaTitle(awardName + " - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}