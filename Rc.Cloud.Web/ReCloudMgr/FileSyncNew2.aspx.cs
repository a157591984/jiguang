using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncNew2 : Rc.Cloud.Web.Common.InitPage
    {
        protected string strFileSyncUrlTchPlanFileSchool = string.Empty;
        protected string strFileSyncUrlTchTestpaperFileSchool = string.Empty;
        protected string strFileSyncUrlHWFileSchool = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10300100";

            if (!IsPostBack)
            {
                string strSql = string.Format("select * from ConfigSchool ");
                DataTable dtServer = Rc.Common.DBUtility.DbHelperSQL_Operate.Query(strSql).Tables[0];
                #region 来源服务器
                ddlSource.Items.Clear();
                ddlSource.Items.Add(new ListItem("来源服务器", ""));
                foreach (DataRow item in dtServer.Rows)
                {
                    if (!string.IsNullOrEmpty(item["D_PublicValue"].ToString()))
                    {
                        ddlSource.Items.Add(new ListItem(item["School_Name"].ToString(), item["School_ID"].ToString() + "^" + item["D_PublicValue"].ToString()));
                    }
                }
                ddlSource.Items.Add(new ListItem("运营主服务器", "^" + Rc.Common.ConfigHelper.GetConfigString("SynStudentAnswerWebSiteUrl")));
                ddlSource.Items.Add(new ListItem("运营教案服务器", "^" + Rc.Common.ConfigHelper.GetConfigString("SynTeachingPlanWebSiteUrl")));
                ddlSource.Items.Add(new ListItem("运营习题集服务器", "^" + Rc.Common.ConfigHelper.GetConfigString("SynTestWebSiteUrl")));
                #endregion
                #region 目标服务器
                ddlTarget.Items.Clear();
                ddlTarget.Items.Add(new ListItem("目标服务器", ""));
                foreach (DataRow item in dtServer.Rows)
                {
                    if (!string.IsNullOrEmpty(item["D_PublicValue"].ToString()))
                    {
                        ddlTarget.Items.Add(new ListItem(item["School_Name"].ToString(), item["School_ID"].ToString() + "^" + item["D_PublicValue"].ToString()));
                    }
                }
                ddlTarget.Items.Add(new ListItem("运营主服务器", "^" + Rc.Common.ConfigHelper.GetConfigString("SynStudentAnswerWebSiteUrl")));
                ddlTarget.Items.Add(new ListItem("运营教案服务器", "^" + Rc.Common.ConfigHelper.GetConfigString("SynTeachingPlanWebSiteUrl")));
                ddlTarget.Items.Add(new ListItem("运营习题集服务器", "^" + Rc.Common.ConfigHelper.GetConfigString("SynTestWebSiteUrl")));
                #endregion

                strFileSyncUrlTchPlanFileSchool = "ReCloudMgr/DownloadTchPlanFiles.aspx?SysUser_ID=" + loginUser.SysUser_ID;
                strFileSyncUrlTchTestpaperFileSchool = "ReCloudMgr/DownloadTchTestpaperFiles.aspx?SysUser_ID=" + loginUser.SysUser_ID;
                strFileSyncUrlHWFileSchool = "ReCloudMgr/DownloadHWFiles.aspx?SysUser_ID=" + loginUser.SysUser_ID;

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