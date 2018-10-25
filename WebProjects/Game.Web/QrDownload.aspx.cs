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
    public partial class QrDownload : System.Web.UI.Page
    {
        protected string downloadURL = string.Empty;
        protected string gameIcoURL = string.Empty;
        protected string gameName = string.Empty;
        protected string gameSize = string.Empty;
        protected string gameDate = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int KindID = GameRequest.GetQueryInt("KindID", -1);
                downloadURL = string.Format("http://" + Request.Url.Authority.ToString() + "/DownLoadMB.aspx?KindID={0}", KindID);
                
                //获取游戏信息
                GameRulesInfo Info = FacadeManage.aideNativeWebFacade.GetGameHelp(KindID);
                if (Info != null)
                {
                    gameIcoURL = Info.ThumbnailUrl;
                    gameName = Info.KindName + " 手机版" + Info.MobileVersion;
                    gameSize = Info.MobileSize;
                    gameDate = Info.MobileDate;
                }
            }
        }
    }
}