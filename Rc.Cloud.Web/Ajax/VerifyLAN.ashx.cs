using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Ajax
{
    /// <summary>
    /// VerifyLAN 的摘要说明
    /// </summary>
    public class VerifyLAN : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string LanUrl = string.Empty;
            string ExtranetUrl = Rc.Common.ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl");
            string strUserId = string.Empty;
            try
            {
                strUserId = context.Request.QueryString["UserId"].Filter();

                List<Model_ConfigSchool> listModelCS = new List<Model_ConfigSchool>();
                string strWhere = string.Format("D_Type='{0}' and School_ID in(select SchoolId from VW_UserOnClassGradeSchool where userid='{1}' and SchoolId is not null)"
                    , ConfigSchoolTypeEnum.TeachingplanResourceHost.ToString(), strUserId);
                listModelCS = new BLL_ConfigSchool().GetModelList(strWhere);
                if (listModelCS.Count > 0)
                {
                    Model_ConfigSchool model = listModelCS[0];
                    //服务器端验证‘学校局域网是否可访问’有问题，暂时停用，直接不验证
                    //Rc.Cloud.Web.Common.pfunction.VerifyLANIsAccess(strUserId, model.D_Value, ExtranetUrl);
                    Rc.Common.DataCache.SetCache(strUserId, model.D_Value);
                }
            }
            catch (Exception ex)
            {

            }
            context.Response.Write("1");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}