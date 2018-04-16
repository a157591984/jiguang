using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Web.teacher
{
    public partial class UploadFile : Rc.Cloud.Web.Common.FInitData
    {
        //文件夹
        protected string strResourceFolder_Id = string.Empty;

        //教材版本
        protected string strResource_Version = string.Empty;
        //教案类型
        protected string strLessonPlan_Type = string.Empty;
        //年级学期
        protected string strGradeTerm = string.Empty;
        //学科
        protected string strSubject = string.Empty;

        //资源类型资源类型（教案、作业、试卷）
        protected string strResource_Type = string.Empty;
        //资源类别（云资源，自有资源）
        protected string strResource_Class = string.Empty;

        //创建人
        protected string strCreateUser = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request["fid"]))
            {

                strResourceFolder_Id = Request["fid"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["R_Version"]))
            {

                strResource_Version = Request["R_Version"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["LP_Type"]))
            {

                strLessonPlan_Type = Request["LP_Type"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["GT"]))
            {

                strGradeTerm = Request["GT"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["subj"]))
            {

                strSubject = Request["subj"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["R_Type"]))
            {

                strResource_Type = Request["R_Type"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["R_Class"]))
            {

                strResource_Class = Request["R_Class"].ToString().Filter();
            }
            if (!String.IsNullOrEmpty(Request["uid"]))
            {

                strCreateUser = Request["uid"].ToString().Filter();
            }
            strUrlPara = string.Format("?fid={0}&R_Version={1}&LP_Type={2}&GT={3}&subj={4}&R_Type={5}&R_Class={6}&uid={7}"
                , strResourceFolder_Id
                , strResource_Version
                , strLessonPlan_Type
                , strGradeTerm
                , strSubject
                , strResource_Type
                , strResource_Class
                , strCreateUser);


        }
    }
}