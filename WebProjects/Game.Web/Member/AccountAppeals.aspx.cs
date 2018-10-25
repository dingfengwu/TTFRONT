using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using System.Text;
using Game.Utils;

namespace Game.Web.Member
{
    public partial class AccountAppeals : UCPageBase
    {
        protected string questionList = string.Empty;       //帐号保护问题OptionList

        protected void Page_Load(object sender, EventArgs e)
        {
            BindProtectQuestion();
        }

        //绑定密保问题
        private void BindProtectQuestion()
        {
            List<string> list = Fetch.ProtectionQuestiongs;
            StringBuilder sbList = new StringBuilder();
            sbList.Append("<option value=\"0\">==请选择密保问题==</option>");
            foreach (string question in list)
            {
                sbList.AppendFormat("<option value=\"{0}\">{0}</option>", question);
            }
            questionList = sbList.ToString();
        }

        /// <summary>
        /// 增加页面标题
        /// </summary>
        protected override void AddHeaderTitle()
        {
            AddMetaTitle("帐号申诉" + ApplicationSettings.Get("title"));
            AddMetaKeywords(ApplicationSettings.Get("keywords"));
            AddMetaDescription(ApplicationSettings.Get("description"));
        }
    }
}