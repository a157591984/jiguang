using Newtonsoft.Json;
using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class HWSubmitMark : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;
        public bool Isheadmaster = false;//判断是否为班主任
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            if (!IsPostBack)
            {
                string sql = @"select * from [dbo].[VW_UserOnClassGradeSchool] 
where ClassMemberShipEnum='headmaster' and userId='" + UserId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Isheadmaster = true;
                }
                GetSubject();
                GetAllTeacher();
            }
        }

        /// <summary>
        /// 学科
        /// </summary>
        protected void GetSubject()
        {
            ltlSubject.Text = StatsCommonHandle.GetTeacherSubject();
        }

        /// <summary>
        /// 老师（班主任和代课老师）
        /// </summary>
        protected void GetAllTeacher()
        {
            ltlTeacher.Text = StatsCommonHandle.GetAllTeacher();
        }
        [WebMethod]
        /// <summary>
        /// 班级
        /// </summary>
        public static string GetClass(string SubjectID, string TeacherID)
        {
            try
            {

                string TempDate = string.Empty;
                TempDate = StatsCommonHandle.GetTeacherClassBySubject(TeacherID.Filter(), SubjectID.Filter());
                return TempDate;
            }
            catch (Exception)
            {

                return "";
            }
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        [WebMethod]
        public static string GetList(string SubjectID, string ClassID, string DateType, string TeacherID, int PageSize, int PageIndex)
        {
            try
            {
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                DateType = DateType.Filter();
                HttpContext.Current.Session["StatsClassSubject"] = SubjectID;
                string strWhere = "TeacherID ='" + TeacherID + "'";
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " AND SubjectID = '" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " AND ClassID = '" + ClassID + "'";
                }
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " AND DateType = '" + DateType + "'";
                }
                //strWhere += StatsCommonHandle.GetStrWhereBySelfClassForTeacherData(SubjectID);

                DataTable dt = new DataTable();
                BLL_StatsClassHW_CorrectedInData bll = new BLL_StatsClassHW_CorrectedInData();
                dt = bll.GetListByPage(strWhere, " DateData DESC", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        ClassID = dt.Rows[i]["ClassID"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        SubjectID = dt.Rows[i]["SubjectID"].ToString(),
                        AssignedCount = dt.Rows[i]["AssignedCount"].ToString(),
                        CommittedCount = dt.Rows[i]["CommittedCount"].ToString(),
                        CorrectedCount = dt.Rows[i]["CorrectedCount"].ToString(),
                        CommittedCountRate = dt.Rows[i]["CommittedCountRate"].ToString().clearLastZero() + "%",
                        CorrectedCountRate = dt.Rows[i]["CorrectedCountRate"].ToString().clearLastZero() + "%",
                        DateData = dt.Rows[i]["DateData"].ToString(),
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