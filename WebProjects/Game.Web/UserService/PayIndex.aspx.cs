using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using System.Text;

namespace Game.Web.UserService
{
    public partial class PayIndex : UCPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindIssueData();
            }
        }

        private void BindIssueData()
        {
            IList<GameIssueInfo> issue = FacadeManage.aideNativeWebFacade.GetTopIssueList(4, 2);
            if (issue != null)
            {
                this.rptIssueList.DataSource = issue;
                this.rptIssueList.DataBind();
            }
        }

        /// <summary>
        /// 获取问题内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected string GetIssueContent(string content)
        {
            string rValue = "";
            if (!string.IsNullOrEmpty(content))
            {
                if (content.Length > 30)
                {
                    rValue = TextUtility.CutLeft(Utility.HtmlDecode(content), 30) + "...";
                }
                else
                {
                    rValue = Utility.HtmlDecode(content);
                }
            }
            return rValue;
        }
    }
}