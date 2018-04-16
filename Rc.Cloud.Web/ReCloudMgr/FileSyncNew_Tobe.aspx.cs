using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncNew_Tobe : Rc.Cloud.Web.Common.InitPage
    {
        protected string strFileSyncUrlData = string.Empty;
        protected string strFileSyncUrlPlan = string.Empty;
        protected string strFileSyncUrlTestPaper = string.Empty;
        protected string strFileSyncUrlFileSchool = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10300200";

            if (!IsPostBack)
            {
                strFileSyncUrlData = "SyncData_Tobe.aspx?SysUser_ID=" + loginUser.SysUser_ID;
                strFileSyncUrlPlan = Rc.Common.ConfigHelper.GetConfigString("SynTeachingPlanWebSiteUrl") + "ReCloudMgr/SyncPlanFile_Tobe.aspx?SysUser_ID=" + loginUser.SysUser_ID;
                strFileSyncUrlTestPaper = Rc.Common.ConfigHelper.GetConfigString("SynTestWebSiteUrl") + "ReCloudMgr/SyncTestPaperFile_Tobe.aspx?SysUser_ID=" + loginUser.SysUser_ID;
                strFileSyncUrlFileSchool = "ReCloudMgr/SyncFileSchool_Tobe.aspx?SysUser_ID=" + loginUser.SysUser_ID;
            }
        }

        [WebMethod]
        public static string GetSchoolUrl(string schoolId)
        {
            string temp = string.Empty;
            try
            {
                Model_ConfigSchool modelCS = new Model_ConfigSchool();
                BLL_ConfigSchool bllCS = new BLL_ConfigSchool();
                modelCS = bllCS.GetModelBySchoolId(schoolId.Filter());
                if (modelCS == null)
                {
                    temp = JsonConvert.SerializeObject(new
                    {
                        err = "学校配置信息不存在"
                    });
                }
                else
                {
                    if (string.IsNullOrEmpty(modelCS.D_PublicValue))
                    {
                        temp = JsonConvert.SerializeObject(new
                        {
                            err = "请配置学校外网地址"
                        });
                    }
                    else
                    {
                        temp = JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            SchoolIP_Local = modelCS.D_Value,
                            SchoolIP = modelCS.D_PublicValue
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                temp = JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
            return temp;
        }
    }
}