using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using Game.Entity.NativeWeb;
using System.Data;

namespace Game.Web.Mobile
{
    public partial class Info : System.Web.UI.Page
    {
        // 客户端类型
        public int terminalType = 0;

        // 规则实体
        public GameRulesInfo model;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = GameRequest.GetQueryInt("id", 0);
            if(id == 0)
                Response.Redirect("/404.html");

            model = FacadeManage.aideNativeWebFacade.GetGameHelp(id);
            if(model == null)
                Response.Redirect("/404.html");

            model.HelpIntro = Game.Utils.Utility.HtmlDecode(model.HelpIntro);

            terminalType = Fetch.GetTerminalType(Page.Request);

            BindMoblieGame(id);
        }

        /// <summary>
        /// 绑定手机游戏
        /// </summary>
        private void BindMoblieGame(int id)
        {
            DataSet ds = FacadeManage.aideNativeWebFacade.GetMoblieGame();
            if(ds.Tables[0].Rows.Count > 0)
            {
                IList<GameRulesInfo> listGameRule = Game.Utils.DataHelper.ConvertDataTableToObjects<GameRulesInfo>(ds.Tables[0]);
                if(listGameRule.Count > 0)
                {
                    IList<GameRulesInfo> listOtherGameRule = listGameRule.Where(e => e.KindID != id).ToList();
                    if(listOtherGameRule.Count > 0)
                    {
                        rptMoblieGame.DataSource = listOtherGameRule;
                        rptMoblieGame.DataBind();
                        return;
                    }
                }
                rptMoblieGame.Visible = false;
                plNotData.Visible = true;
            }
        }
    }
}