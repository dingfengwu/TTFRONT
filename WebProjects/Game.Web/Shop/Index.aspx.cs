using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using System.Text.RegularExpressions;
using Game.Kernel;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using System.Text;

namespace Game.Web.Shop
{
    public partial class Index : UCPageBase
    {
        protected string orderDefault = string.Empty;       // 默认排序链接
        protected string orderCountLink = string.Empty;     // 人气排序链接
        protected string orderPriceLink = string.Empty;     // 价格排序链接
        protected string orderTimeLink = string.Empty;      // 时间排序链接
        protected int orderField = 0;                       // 排序字段类型  
        protected string orderMethod = "down";              // 排序方法 升序或降序
        protected string pageInfo = string.Empty;           // 页信息
        protected string pageName = "全部商品";           // 页名称

        protected void Page_Load(object sender, EventArgs e)
        {
            orderField = GameRequest.GetQueryInt("orderby", 0);
            orderMethod = GameRequest.GetQueryString("method");
            SetOrderByLink();
            BindShop();
        }

        /// <summary>
        /// 获取排序语句
        /// </summary>
        /// <returns></returns>
        protected string GetOrderBy()
        {
            string orderBy = "ORDER BY SortID DESC";

            //参数验证
            if (orderField <= 0 || orderField > 3 || (orderMethod != "down" && orderMethod != "up"))
            {
                return orderBy;
            }

            //构建排序
            orderBy = " ORDER BY";
            switch (orderField)
            {
                case 1:
                    orderBy += " BuyCount";
                    break;
                case 2:
                    orderBy += " Price";
                    break;
                case 3:
                    orderBy += " CollectDate";
                    break;
            }
            if (orderMethod == "down")
            {
                orderBy += " DESC";
            }
            else
            {
                orderBy += " ASC";
            }
            return orderBy;
        }

        /// <summary>
        /// 设置排序链接
        /// </summary>
        protected void SetOrderByLink()
        {
            //处理URL地址
            string url = TextFilter.FilterHtml(TextFilter.FilterScript(GameRequest.GetUrl()));
            Regex reg = new Regex(@"(&|\?)?orderby=[0-9]{1}&method=(down|up){1}");
            url = reg.Replace(url, "");

            orderDefault = url;
            int indexOf = url.IndexOf("?");
            if (indexOf == -1)
            {
                url += "?";
            }
            else
            {
                url += "&";
            }

            //初始化排序链接
            orderCountLink = url + "orderby=1&method=down";
            orderPriceLink = url + "orderby=2&method=down";
            orderTimeLink = url + "orderby=3&method=down";

            //参数验证
            if (orderField <= 0 || orderField > 3 || (orderMethod != "down" && orderMethod != "up"))
            {
                return;
            }

            //根据参数改变排序链接
            string newLink = string.Empty;
            if (orderMethod == "down")
            {
                newLink = string.Format("{0}orderby={1}&method=up", url, orderField);
            }
            else
            {
                newLink = string.Format("{0}orderby={1}&method=down", url, orderField);
            }
            switch (orderField)
            {
                case 1:
                    orderCountLink = newLink;
                    break;
                case 2:
                    orderPriceLink = newLink;
                    break;
                case 3:
                    orderTimeLink = newLink;
                    break;
            }
        }

        /// <summary>
        /// 绑定商品
        /// </summary>
        protected void BindShop()
        {
            //查询条件
            string where = " WHERE Nullity=0";
            if (IntParam != 0)
            {
                where += string.Format(" AND ( TypeID IN (SELECT TypeID FROM AwardType WHERE ParentID={0}) OR TypeID={0} ) ", IntParam);
                AwardType awardType = FacadeManage.aideNativeWebFacade.GetAwardType(IntParam);
                if (awardType != null)
                {
                    pageName = awardType.TypeName;
                    if (awardType.ParentID != 0)
                    {
                        awardType = FacadeManage.aideNativeWebFacade.GetAwardType(awardType.ParentID);
                        pageName = awardType.TypeName + " -> " + pageName;
                    }
                }
            }

            //登录用户查能兑换商品
            int range = GameRequest.GetQueryInt("range", 0);
            if (range == 1)
            {
                if (!Fetch.IsUserOnline())
                {
                    string url = string.Format("/login.aspx?url={0}", Utility.UrlEncode(Utils.GameRequest.GetUrl()));
                    Response.Redirect(url);
                    Response.End();
                }
                else
                {
                    Message msg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "");
                    UserInfo user = msg.EntityList[0] as UserInfo;
                    where = string.Format(" WHERE Price<={1} AND Nullity=0", IntParam, user.UserMedal);
                    pageName = "我能兑换的商品";
                }
            }

            //排序方法
            string orderBy = GetOrderBy();

            //绑定数据

            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(AwardInfo.Tablename, PageIndex, anpPage.PageSize, orderBy, where);
            anpPage.RecordCount = pagerSet.RecordCount;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptShop.DataSource = pagerSet.PageSet;
                rptShop.DataBind();
            }            
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("游戏商城 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}