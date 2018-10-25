using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;
using Game.Entity.NativeWeb;

namespace Game.Web.Member
{
    public partial class Complaint_Setp_3 : UCPageBase
    {
        protected string number = string.Empty;                 //编号 邮件找回编号或申诉编号
        protected string sign = string.Empty;                   //签名
        protected string errorMsg = string.Empty;               //错误信息
        protected string pageName = string.Empty;               //页标签名

        protected void Page_Load(object sender, EventArgs e)
        {
            SwitchStep(1);

            sign = Utils.GameRequest.GetQueryString("sign");
            number = Utils.GameRequest.GetQueryString("param");
            LossReport accountsLossReport = FacadeManage.aideNativeWebFacade.GetLossReport(number);
            if (accountsLossReport == null)
            {
                Response.Redirect("AccountAppeals.aspx");
                Response.End();
            }

            //申诉状态
            if (Convert.ToInt32(accountsLossReport.ProcessStatus) == 3)
            {
                RenderAlertInfo2(true, "申诉链接已被处理，不能重复操作");
                return;
            }

            //签名验证
            string reportkey = AppConfig.ReportForgetPasswordKey;
            string confirmReportSign = Utility.MD5(number + accountsLossReport.UserID + accountsLossReport.ReportDate.ToString() + accountsLossReport.Random + reportkey);
            if (sign != confirmReportSign)
            {
                RenderAlertInfo2(true, "该申诉链接无效，签名错误");
                return;
            }

            //有效期验证
            if (DateTime.Now > accountsLossReport.OverDate)
            {
                RenderAlertInfo2(true, "该申诉链接已经过期，链接有效期为24个小时");
                return;
            }
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("重置密码" + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}