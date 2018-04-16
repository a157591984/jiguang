using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System.Data;
using Rc.BLL.Resources;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Principal
{
    public partial class visit_client : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;
        public string GradeId = string.Empty;
        public string GradeName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            GradeName = Request.QueryString["GradeName"].Filter();
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
                ltlGradeName.Text = Server.UrlDecode(GradeName);

                //GetClass();
            }
        }

        /// <summary>
        /// 获得当前用户所教班级
        /// </summary>
        //protected void GetClass()
        //{
        //    ltlClass.Text = StatsCommonHandle.GetGradeAllClass(GradeId);
        //}

        /// <summary>
        /// 获得列表
        /// </summary>
        [WebMethod]
        public static string GetList(string GradeID, string DateType, string TeacherName, int PageSize, int PageIndex)
        {
            try
            {
                GradeID = GradeID.Filter();
                //ClassID = ClassID.Filter();
                DateType = DateType.Filter();
                TeacherName = TeacherName.Filter();

                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(GradeID))
                {
                    strWhere += " and TeacherId in ( select userid from VW_UserOnClassGradeSchool where GradeId='" + GradeID + "')";
                }
                //if (!string.IsNullOrEmpty(ClassID))
                //{
                //    strWhere += " AND ClassId = '" + ClassID + "'";
                //}
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " AND DateType = '" + DateType + "'";
                }
                if (!string.IsNullOrEmpty(TeacherName))
                {
                    strWhere += " and TeacherName like '%" + TeacherName + "%' ";
                }

                DataTable dt = new DataTable();
                BLL_visit_client_s bll = new BLL_visit_client_s();
                dt = bll.GetListByPageNew(strWhere, " DateData desc,VisitCount_All desc,TeacherName", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCountNew(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        DateType = dt.Rows[i]["DateType"].ToString(),
                        DateData = dt.Rows[i]["DateData"].ToString(),
                        TeacherId = dt.Rows[i]["TeacherId"].ToString(),
                        TeacherName = dt.Rows[i]["TeacherName"].ToString(),
                        TrueName = dt.Rows[i]["TrueName"].ToString(),
                        VisitCount_All = dt.Rows[i]["VisitCount_All"].ToString(),
                        VisitCount_Cloud = dt.Rows[i]["VisitCount_Cloud"].ToString(),
                        VisitCount_Own = dt.Rows[i]["VisitCount_Own"].ToString(),
                        VisitFile_All = dt.Rows[i]["VisitFile_All"].ToString(),
                        VisitFile_Cloud = dt.Rows[i]["VisitFile_Cloud"].ToString(),
                        VisitFile_Own = dt.Rows[i]["VisitFile_Own"].ToString(),
                        TeacherClass = GetTeacherClass(GradeID, dt.Rows[i]["TeacherId"].ToString()),
                        CreateOwnCount_All = dt.Rows[i]["CreateOwnCount_All"].ToString(),
                        CreateOwnCount_Plan = dt.Rows[i]["CreateOwnCount_Plan"].ToString(),
                        CreateOwnCount_TestPaper = dt.Rows[i]["CreateOwnCount_TestPaper"].ToString()
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = intRecordCount,
                        list = listReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }
        [WebMethod]
        public static string GetClassName(string GradeId, string TeacherId)
        {
            try
            {
                string Table = "<table class=\"table table-bordered text-center\"> <thead><tr><td>班级ID</td><td>班级名称</td></tr></thead><tbody>{0}</tbody></table>";
                string Temp = string.Empty;
                string sql = "select distinct(ClassId),ClassName from VW_UserOnClassGradeSchool where UserId='" + TeacherId.Filter() + "' and  GradeId='" + GradeId.Filter() + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Temp += "<tr><td>" + dt.Rows[i]["ClassId"].ToString() + "</td><td>" + dt.Rows[i]["ClassName"].ToString() + "</td></tr>";
                    }
                }
                return Temp = string.Format(Table, Temp);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetTeacherClass(string GradeId, string TeacherId)
        {
            try
            {
                string sql = "select COUNT(*)  from VW_UserOnClassGradeSchool where UserId='" + TeacherId + "' and GradeId='" + GradeId + "' and  ClassId<>''";
                object j = Rc.Common.DBUtility.DbHelperSQL.GetSingle(sql);
                return j.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}