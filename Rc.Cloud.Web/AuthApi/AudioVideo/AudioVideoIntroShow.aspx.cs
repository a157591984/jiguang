using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Common;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.AuthApi
{
    public partial class AudioVideoIntroShow : System.Web.UI.Page
    {
        protected string AudioVideoUrl = string.Empty;
        public string AudioVideoBookId = string.Empty;
        BLL_AudioVideoIntro bll = new BLL_AudioVideoIntro();
        Model_AudioVideoIntro model = new Model_AudioVideoIntro();
        protected void Page_Load(object sender, EventArgs e)
        { 
            AudioVideoUrl = Request.QueryString["AudioVideoUrl"];
            if (!IsPostBack)
            {
                ltlShow.Text = "<video src=\"" + AudioVideoUrl + "\" controls=\"controls\" autoplay=\"autoplay\" style=\"width: 540px;\">";
            }
        }


    }
}