using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;

namespace Game.Web.Spread
{
    public partial class Index : UCPageBase
    {
        protected int gameTime = 1800;                  //一次性赠送需要游戏时长
        protected int presentGold = 200000;             //一次性赠送金币
        protected decimal balanceRate = 0.1M;           //推广分成
        protected int registerGold = 5000;              //推广赠送金币
        protected string spreadUrl = string.Empty;      //推广链接
        protected decimal fillGrantRate = 0.1M;         //充值赠送比例

        protected void Page_Load(object sender, EventArgs e)
        {
            Themes.Standard.Common_Header sHeader = (Themes.Standard.Common_Header)this.FindControl("sHeader");
            sHeader.title = "推广系统";
            DatasBind();

            // 获取推广链接
            if (Fetch.IsUserOnline())
            {
                spanLogon.Visible = false;
                spanSpread.Visible = true;
                string domain = Request.Url.Authority.ToString();

                // 域名以.分组第二个元素值
                string element = domain.Split('.')[1];

                // 域名所有后缀
                Array list = AppConfig.domainSuffixList.Split('|');

                if (Array.IndexOf(list, element) != -1)
                    // 顶级域名但不带www
                    spreadUrl = "http://" + Fetch.GetUserCookie().GameID + "." + Request.Url.Authority;
                else
                    spreadUrl = "http://" + Fetch.GetUserCookie().GameID + "." + Request.Url.Authority.Substring(Request.Url.Authority.IndexOf('.') + 1);
            }
            else
            {
                spanLogon.Visible = true;
                spanSpread.Visible = false;
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("推广首页 - " + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DatasBind()
        {
            GlobalSpreadInfo model = new GlobalSpreadInfo();
            model = FacadeManage.aideTreasureFacade.GetGlobalSpreadInfo();
            if (model != null)
            {
                gameTime = model.PlayTimeCount / 60;
                presentGold = model.PlayTimeGrantScore;
                balanceRate = model.BalanceRate;
                registerGold = model.RegisterGrantScore;
                fillGrantRate = model.FillGrantRate;
            }
        }
    }
}