using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;
using System.Text;
using System.Web.Services;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class HisKlgAnalysis : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;//用户id
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

                throw;
            }
        }


        /// <summary>
        /// 获得知识点列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetKnowledge(string SubjectID, string ClassID, string key, string KPScoreAvgRate, string DateType, string DateData, string TeacherID, int PageSize, int PageIndex)
        {
            try
            {
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                key = key.Filter();
                KPScoreAvgRate = KPScoreAvgRate.Filter();
                DateData = DateData.Filter();
                HttpContext.Current.Session["StatsClassSubject"] = SubjectID;
                string strWhere = "TeacherID ='" + TeacherID + "' ";
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " and  SubjectID = '" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(ClassID))
                {
                    strWhere += " and  ClassID = '" + ClassID + "'";
                }
                if (!string.IsNullOrEmpty(key))
                {
                    strWhere += " and  KPName like '%" + key.TrimEnd() + "%'";
                }
                if (!string.IsNullOrEmpty(KPScoreAvgRate))
                {
                    strWhere += " and  KPScoreAvgRate < " + KPScoreAvgRate + "";
                }
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " and  DateType = '" + DateType + "'";
                }
                if (!string.IsNullOrEmpty(DateData))
                {
                    strWhere += " and  DateData = '" + DateData + "'";
                }
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                //strWhere += StatsCommonHandle.GetStrWhereBySelfClassForTeacherData(SubjectID);

                DataTable dt = new DataTable();
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                int inum = 1;
                //if (string.IsNullOrEmpty(ClassID))//全部班级 暂时不从老师表取数据
                //{
                //    BLL_StatsTeacherHW_KPInData bll = new BLL_StatsTeacherHW_KPInData();
                //    dt = bll.GetListByPage(strWhere, "StatsTeacherHW_KPInDataID", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                //    intRecordCount = bll.GetRecordCount(strWhere);
                //    string temp = string.Empty;
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        listReturn.Add(new
                //        {
                //            SubjectID = dt.Rows[i]["SubjectID"].ToString(),
                //            SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                //            KPName = dt.Rows[i]["KPName"].ToString(),
                //            KPScoreAvgRate = (!string.IsNullOrEmpty(dt.Rows[i]["KPScoreAvgRate"].ToString())) ? dt.Rows[i]["KPScoreAvgRate"].ToString() + "%" : "-",
                //            DateData = dt.Rows[i]["DateData"].ToString(),
                //            DateType = dt.Rows[i]["DateType"].ToString()
                //        });
                //        inum++;
                //    }
                //}
                //else
                //{
                BLL_StatsClassHW_KPInData bll = new BLL_StatsClassHW_KPInData();
                dt = bll.GetListByPage(strWhere, "ClassName,DateData desc", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                intRecordCount = bll.GetRecordCount(strWhere);
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        SubjectID = dt.Rows[i]["SubjectID"].ToString(),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        ClassID = dt.Rows[i]["ClassID"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        KPName = dt.Rows[i]["KPName"].ToString(),
                        KPNameEncode = Rc.Common.DBUtility.DESEncrypt.Encrypt(dt.Rows[i]["KPName"].ToString()),
                        KPScoreAvgRate = (!string.IsNullOrEmpty(dt.Rows[i]["KPScoreAvgRate"].ToString())) ? dt.Rows[i]["KPScoreAvgRate"].ToString().clearLastZero() + "%" : "-",
                        DateData = dt.Rows[i]["DateData"].ToString(),
                        DateType = dt.Rows[i]["DateType"].ToString(),
                        TeacherId = dt.Rows[i]["TeacherId"].ToString(),
                        IsTeacherData = loginUser.UserId == TeacherID ? "" : "1"
                    });
                    inum++;
                }
                //}
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