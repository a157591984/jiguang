using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Homework
{
    public partial class AutoCompleteDemo : System.Web.UI.Page
    {
        protected string strpAutoCompleteConditionIn = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strpAutoCompleteConditionIn = "D_Name|1136764095@qq.com";
        }
    }
}