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
    public partial class visit_web : Rc.Cloud.Web.Common.FInitData
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

                GetClass();
            }
        }

        /// <summary>
        /// 获得当前用户所教班级
        /// </summary>
        protected void GetClass()
        {
            ltlClass.Text = StatsCommonHandle.GetGradeAllClass(GradeId);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        [WebMethod]
        public static string GetList(string GradeID, string ClassID, string DateType, string TeacherName, int PageSize, int PageIndex)
        {




            try
            {
                GradeID = GradeID.Filter();
                ClassID = ClassID.Filter();
                DateType = DateType.Filter();
                TeacherName = TeacherName.Filter();

                string strWhere = " ClassId !='' ";
                if (!string.IsNullOrEmpty(GradeID))
                {
                    strWhere += " AND GradeId = '" + GradeID + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " AND ClassId = '" + ClassID + "'";
                }
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " AND DateType = '" + DateType + "'";
                }
                if (!string.IsNullOrEmpty(TeacherName))
                {
                    strWhere += " and TeacherName like '%" + TeacherName + "%' ";
                }

                DataTable dt = new DataTable();
                BLL_StatsVisitWeb bll = new BLL_StatsVisitWeb();
                dt = bll.GetListByPage(strWhere, " DateData desc,VisitCount_All desc,TeacherName", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        DateType = dt.Rows[i]["DateType"].ToString(),
                        DateData = dt.Rows[i]["DateData"].ToString(),
                        ClassId = dt.Rows[i]["ClassId"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        TeacherId = dt.Rows[i]["TeacherId"].ToString(),
                        TeacherName = dt.Rows[i]["TeacherName"].ToString(),
                        VisitCount_All = dt.Rows[i]["VisitCount_All"].ToString(),
                        VisitCount_Cloud = dt.Rows[i]["VisitCount_Cloud"].ToString(),
                        VisitCount_Own = dt.Rows[i]["VisitCount_Own"].ToString(),
                        VisitFile_All = dt.Rows[i]["VisitFile_All"].ToString(),
                        VisitFile_Cloud = dt.Rows[i]["VisitFile_Cloud"].ToString(),
                        VisitFile_Own = dt.Rows[i]["VisitFile_Own"].ToString(),
                        VistiDuration_Count = pfunction.ConvertSecond(dt.Rows[i]["VistiDuration_Count"].ToString()),
                        VistiDuration_Avg = pfunction.ConvertSecond(dt.Rows[i]["VistiDuration_Avg"].ToString())

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

    }
}