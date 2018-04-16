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

namespace Rc.Cloud.Web.Principal
{
    public partial class HisKlgAnalysis : Rc.Cloud.Web.Common.FInitData
    {
        protected string UserId = string.Empty;//用户id
        public string GradeId = string.Empty;
        public string GradeName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = FloginUser.UserId;
            GradeId = Request.QueryString["GradeId"].Filter();
            GradeName = Request.QueryString["GradeName"].Filter();
            if (!IsPostBack)
            {
                ltlGradeName.Text = Server.UrlDecode(GradeName);
                GetClass();
                GetSubjec();
                //GetGrade();
                //GetSubject();
            }
        }


        ///// <summary>
        ///// 获得当前用户所教年级
        ///// </summary>
        //protected void GetGrade()
        //{
        //    string classActive = string.Empty;
        //    StringBuilder strGrade = new StringBuilder();
        //    BLL_UserGroup bll_user_group = new BLL_UserGroup();
        //    DataTable dt = bll_user_group.GetList("UserGroup_AttrEnum='Grade'").Tables[0];
        //    strGrade.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        strGrade.AppendFormat("<li><a href='##' ajax-value='{0}'>{1}</a></li>",
        //            item["UserGroup_id"],
        //            item["UserGroup_Name"]
        //            );
        //    }
        //    ltlGrade.Text = strGrade.ToString();
        //}

        /// <summary>
        /// 学科
        /// </summary>
        public void GetSubjec()
        {
            ltlSubject.Text = StatsCommonHandle.GetTeacherSubjectData();

        }
        ///// <summary>
        ///// 获得当前用户所教学科
        ///// </summary>
        //protected void GetSubject()
        //{
        //    string classActive = string.Empty;
        //    StringBuilder subjectStr = new StringBuilder();
        //    BLL_Common_Dict bll_common_dict = new BLL_Common_Dict();
        //    DataTable dt = bll_common_dict.GetList("D_Type = '7' ORDER BY D_CreateTime").Tables[0];
        //    subjectStr.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        subjectStr.AppendFormat("<li><a href='##' ajax-value='{0}'>{1}</a></li>",
        //            item["Common_Dict_ID"],
        //            item["D_Name"]
        //            );
        //    }
        //    ltlSubject.Text = subjectStr.ToString();
        //}


        /// <summary>
        /// 获得当前用户所教班级
        /// </summary>
        protected void GetClass()
        {
            ltlClass.Text = StatsCommonHandle.GetGradeAllClass(GradeId);
        }


        /// <summary>
        /// 获得知识点列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetKnowledge(string Gradeid, string SubjectID, string ClassID, string key, string KPScoreAvgRate, string DateType, string DateData, int PageSize, int PageIndex)
        {
            try
            {
                Gradeid = Gradeid.Filter();
                SubjectID = SubjectID.Filter();
                ClassID = ClassID.Filter();
                key = key.Filter();
                KPScoreAvgRate = KPScoreAvgRate.Filter();
                DateData = DateData.Filter();
                HttpContext.Current.Session["StatsGradeSubject"] = SubjectID;
                string strWhere = "1=1";
                if (!string.IsNullOrEmpty(Gradeid))
                {
                    strWhere += " and  Gradeid = '" + Gradeid + "'";
                }
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
                    strWhere += " and  KPName like '%" + key + "%'";
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
                DataTable dt = new DataTable();
                BLL_StatsClassHW_KPInData bll = new BLL_StatsClassHW_KPInData();
                dt = bll.GetListByPage(strWhere, "StatsClassHW_KPInDataID ASC", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        GradeID = dt.Rows[i]["Gradeid"],
                        GradeName = dt.Rows[i]["GradeName"].ToString(),
                        SubjectID = dt.Rows[i]["SubjectID"].ToString(),
                        SubjectName = dt.Rows[i]["SubjectName"].ToString(),
                        ClassID = dt.Rows[i]["ClassID"].ToString(),
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        KPName = dt.Rows[i]["KPName"].ToString(),
                        KPNameEncode = Rc.Common.DBUtility.DESEncrypt.Encrypt(dt.Rows[i]["KPName"].ToString()),
                        KPScoreAvgRate = (!string.IsNullOrEmpty(dt.Rows[i]["KPScoreAvgRate"].ToString())) ? dt.Rows[i]["KPScoreAvgRate"].ToString().clearLastZero() + "%" : "-",
                        DateData = dt.Rows[i]["DateData"].ToString(),
                        DateType = dt.Rows[i]["DateType"].ToString()
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