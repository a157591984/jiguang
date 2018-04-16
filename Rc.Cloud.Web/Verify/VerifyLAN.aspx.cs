using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.Verify
{
    public partial class VerifyLAN : System.Web.UI.Page
    {
        protected string UserId = string.Empty;
        protected string LanUrl = string.Empty;
        protected string ExtranetUrl = Rc.Common.ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl");
        protected string flag = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //UserId = Request.QueryString["UserId"];
            //if (string.IsNullOrEmpty(UserId))
            //{
            //    if (Session["FLoginUser"] != null)
            //    {
            //        Model_F_User modelUser = Session["FLoginUser"] as Model_F_User;
            //        UserId = modelUser.UserId;
            //    }
            //}
            //object userLanUrlCookie = Rc.Common.StrUtility.CookieClass.GetCookie("LanUrl_" + UserId);
            //if (!string.IsNullOrEmpty(UserId))
            //{
            //    List<Model_ConfigSchool> listModelCS = new List<Model_ConfigSchool>();
            //    string strWhere = string.Format("D_Type='{0}' and School_ID in(select SchoolId from VW_UserOnClassGradeSchool where userid='{1}' and SchoolId is not null)"
            //        , ConfigSchoolTypeEnum.TeachingplanResourceHost.ToString(), UserId);
            //    listModelCS = new BLL_ConfigSchool().GetModelList(strWhere);
            //    if (listModelCS.Count > 0)
            //    {
            //        Model_ConfigSchool model = listModelCS[0];
            //        LanUrl = model.D_Value;
            //    }
            //    if (!string.IsNullOrEmpty(LanUrl) && LanUrl.Substring(LanUrl.Length - 1) != "/") LanUrl += "/";
            //}
            //if (userLanUrlCookie == null) Rc.Common.StrUtility.CookieClass.SetCookie("LanUrl_" + UserId, ExtranetUrl);
            //if (Rc.Common.StrUtility.CookieClass.GetCookie("LanUrlStatus_" + UserId) == null)
            //{
            //    Rc.Common.StrUtility.CookieClass.SetCookie("LanUrlStatus_" + UserId, "a");
            //    flag = "1";
            //}

        }
    }
}