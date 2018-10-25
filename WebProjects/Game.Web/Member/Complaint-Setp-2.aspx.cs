using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Member
{
    public partial class Complaint_Setp_2 : UCPageBase
    {
        protected string account = string.Empty;
        protected string number = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            number = GameRequest.GetQueryString("number");
            account = GameRequest.GetQueryString("account");

            lblAlertIcon.CssClass = "ui-result-pic-1";
            lblAlertInfo.CssClass = "ui-result-success";
            lblAlertInfo.Text = string.Format("恭喜您{0}，申述成功。您的申诉流水号：{1}。", account, number);
        }
    }
}