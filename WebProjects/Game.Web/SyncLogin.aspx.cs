using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Kernel;

namespace Game.Web
{
    public partial class SyncLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = GameRequest.GetQueryInt("userid", 0);                //用户标识
            string signature = GameRequest.GetQueryString("signature");       //签名
            string time = GameRequest.GetQueryString("time");                 //过期时间
            string url = GameRequest.GetQueryString("url");                   //跳转地址

            //验证UserID
            if (userId == 0)
            {
                Response.Redirect(url);
                return;
            }

            //判断是否已登录
            UserTicketInfo userTiketInfo = Fetch.GetUserCookie();
            if (userTiketInfo != null && userTiketInfo.UserID == userId)
            {
                Response.Redirect(url);
                return;
            }

            //查询用户
            Message msg = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(userId, 0, "");
            if (!msg.Success)
            {
                Response.Redirect(url);
                return;
            }
            UserInfo userInfo = msg.EntityList[0] as UserInfo;
            if (userInfo == null)
            {
                Response.Redirect(url);
                return;
            }

            //验证是否有动态密码
            if (string.IsNullOrEmpty(userInfo.DynamicPass.Trim()))
            {
                Response.Redirect(url);
                return;
            }

            //验证签名
            //签名加密方法 MD5(UserID+动态密码+Time+Key);
            string md5Str = userId.ToString() + userInfo.DynamicPass + time.ToString() + AppConfig.SyncLoginKey;
            string webSignature = Utility.MD5(md5Str);
            if (signature != webSignature)
            {
                FileManager.WriteFile(Server.MapPath("/Log/1.txt"), string.Format("md5Str={0}&webSignature={1}&signature={2}&UserID={3}\n", md5Str, webSignature, signature, userId), true);
                Response.Redirect(url);
                return;
            }

            //验证链接有效期
            DateTime dtOut = userInfo.DynamicPassTime.AddMilliseconds(Convert.ToDouble(time) + Convert.ToDouble(AppConfig.SyncUrlTimeOut));
            if (dtOut < DateTime.Now)
            {
                Response.Redirect(url);
                return;
            }

            //同步登录
            Fetch.SetUserCookie(userInfo.ToUserTicketInfo());
            Response.Redirect(url);
        }
    }
}