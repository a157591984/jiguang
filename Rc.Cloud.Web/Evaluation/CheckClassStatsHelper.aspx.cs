using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class CheckClassStatsHelper : Rc.Cloud.Web.Common.FInitData
    {
        protected string HomeWork_ID = string.Empty;
        protected string HomeWork_Name = string.Empty;
        string ResourceToResourceFolder_Id = string.Empty;
        string ClassID = string.Empty;
        string TeacherID = string.Empty;
        string ClassName = string.Empty;
        string IsTeacherData = string.Empty;
        protected string link = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            HomeWork_Name = Request.QueryString["HomeWork_Name"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

            ClassID = Request.QueryString["ClassID"].Filter();
            TeacherID = Request.QueryString["TeacherID"].Filter();
            ClassName = Request.QueryString["ClassName"].Filter();
            IsTeacherData = Request.QueryString["IsTeacherData"].Filter();
            if (IsTeacherData == "1")
            {
                link = string.Format("AssessmentProfile.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}&ClassID={2}&TeacherID={3}&ClassName={4}&ClassCode={2}"
                                , ResourceToResourceFolder_Id
                                , HomeWork_ID
                                , ClassID
                                , TeacherID
                                , ClassName);
            }
            else
            {
                link = string.Format("AssessmentProfile.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}&ClassCode={2}"
                , ResourceToResourceFolder_Id
                , HomeWork_ID
                , ClassID);
            }
        }

        [WebMethod]
        public static string CheckStatsHelper(string hwId)
        {
            try
            {
                hwId = hwId.Filter();
                int inum = new BLL_StatsHelper().GetRecordCount("Exec_Status='0' and SType='1' and Homework_Id='" + hwId + "' ");
                if (inum != 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "检查作业是否要重新统计失败：" + ex.Message.ToString());
                return "2";
            }
        }

    }
}