using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using System.Web.Services;
using Rc.Model.Resources;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.teacher
{
    public partial class SelectPersonCharge : Rc.Cloud.Web.Common.FInitData
    {
        public string GradeId = string.Empty;
        public string SubjectId = string.Empty;
        public string ResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeId = Request.QueryString["GradeId"].Filter();
            SubjectId = Request.QueryString["SubjectId"].Filter();
            ResourceFolder_Id = Request.QueryString["ResourceFolder_Id"];
            if (!IsPostBack)
            {
                ltlGrade.Text = GetGrade();
                ltlSubject.Text = GetSubject();
            }
        }

        /// <summary>
        /// 获取年级
        /// </summary>
        /// <returns></returns>
        public string GetGrade()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                string sql = @"select  distinct t.GradeId,t.GradeName from [dbo].[VW_UserOnClassGradeSchool] t  
inner join VW_UserOnClassGradeSchool t1 on t1.SchoolId=t.SchoolId and t1.userid='" + FloginUser.UserId + @"'
where t.GradeId<>'' and t.GradeId='" + GradeId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (!string.IsNullOrEmpty(GradeId) && GradeId != "-1")
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string classActive = (GradeId == dt.Rows[i]["GradeId"].ToString().Trim()) ? "active" : "";
                            strHtml.AppendFormat("<li>");
                            strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{2}\" \">{1}</a>"
                                , dt.Rows[i]["GradeId"]
                                , dt.Rows[i]["GradeName"]
                                , classActive);
                            strHtml.AppendFormat("</li>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string classActive = (i == 0) ? "active" : "";
                        strHtml.AppendFormat("<li>");
                        strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{2}\" \">{1}</a>"
                            , dt.Rows[i]["GradeId"]
                            , dt.Rows[i]["GradeName"]
                            , classActive);
                        strHtml.AppendFormat("</li>");
                    }
                }
            }
            catch (Exception ex)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 获取学科
        /// </summary>
        /// <returns></returns>
        public string GetSubject()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {

                string sql = @"select  distinct dc.Common_Dict_Id,dc.D_Name,dc.D_Order from f_user u
inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=u.Subject where  Subject<>'' and Subject='" + SubjectId + "' order by dc.D_Order";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(SubjectId) && SubjectId != "-1")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string classActive = (SubjectId == dt.Rows[i]["Common_Dict_Id"].ToString()) ? "active" : "";
                            strHtml.AppendFormat("<li>");
                            strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{2}\" \">{1}</a>"
                                , dt.Rows[i]["Common_Dict_Id"]
                                , dt.Rows[i]["D_Name"]
                                , classActive);
                            strHtml.AppendFormat("</li>");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string classActive = (i == 0) ? "active" : "";
                            strHtml.AppendFormat("<li>");
                            strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{2}\" \">{1}</a>"
                                , dt.Rows[i]["Common_Dict_Id"]
                                , dt.Rows[i]["D_Name"]
                                , classActive);
                            strHtml.AppendFormat("</li>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="SubjectID"></param>
        /// <param name="GradeID"></param>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetList(string KeyName, string SubjectID, string GradeID, string ResourceFolder_Id)
        {
            try
            {
                KeyName = KeyName.Filter();
                SubjectID = SubjectID.Filter();
                GradeID = GradeID.Filter();
                ResourceFolder_Id = ResourceFolder_Id.Filter();
                HttpContext.Current.Session["StatsClassSubject"] = SubjectID;
                string strWhere = " ";
                if (!string.IsNullOrEmpty(KeyName))
                {
                    strWhere += " and  (UserName like '%" + KeyName + "%' or TrueName like '%" + KeyName + "%')";
                }
                if (!string.IsNullOrEmpty(SubjectID))
                {
                    strWhere += " and Common_Dict_ID = '" + SubjectID + "'";
                }
                if (!string.IsNullOrEmpty(GradeID))
                {
                    strWhere += " and GradeId = '" + GradeID + "'";
                }
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                DataTable dt = new DataTable();
                BLL_PrpeLesson bll = new BLL_PrpeLesson();
                string sql = string.Format(@"select distinct vw.UserId,vw.UserName,vw.TrueName,vw.GradeName,cd.D_Name,cd.Common_Dict_ID from [dbo].[VW_UserOnClassGradeSchool] vw
left join F_User u on u.UserId=vw.UserId
left join [dbo].[Common_Dict] cd on cd.Common_Dict_Id=u.Subject
where vw.schoolId in (select distinct schoolId from [VW_UserOnClassGradeSchool] where userId='{1}') and vw.UserId<>'{1}'" + strWhere, ResourceFolder_Id, loginUser.UserId);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                string sqlP = @"select * from PrpeLesson_Person where ResourceFolder_Id='" + ResourceFolder_Id + "'";
                DataTable dtP = Rc.Common.DBUtility.DbHelperSQL.Query(sqlP).Tables[0];
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] dr = dtP.Select("ChargePerson='" + dt.Rows[i]["UserId"].ToString() + "'");
                    listReturn.Add(new
                    {
                        UserId = dt.Rows[i]["UserId"].ToString(),
                        GradeName = dt.Rows[i]["GradeName"].ToString(),
                        Subject = dt.Rows[i]["D_Name"].ToString(),
                        UserName = dt.Rows[i]["UserName"].ToString(),
                        TrueName = dt.Rows[i]["TrueName"].ToString(),
                        PrpeLesson_Person_Id = dr.Length,

                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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
        /// <summary>
        /// 添加负责人
        /// </summary>
        /// <param name="userlist"></param>
        /// <param name="relist"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Empowers(string TeacherList, string ResourceFolder_Id)
        {
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                BLL_PrpeLesson_Person bll = new BLL_PrpeLesson_Person();
                List<Model_PrpeLesson_Person> listModel = new List<Model_PrpeLesson_Person>();
                ResourceFolder_Id = ResourceFolder_Id.TrimEnd(',').Filter();
                TeacherList = TeacherList.TrimEnd(',').Filter();
                string _dSql = @"delete PrpeLesson_Person where ResourceFolder_Id = '" + ResourceFolder_Id + @"' 
                and ChargePerson in ('" + TeacherList.Replace(",", "','") + "')";
                foreach (string item in TeacherList.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {

                        Model_PrpeLesson_Person model = new Model_PrpeLesson_Person();
                        model.PrpeLesson_Person_Id = Guid.NewGuid().ToString();
                        model.ResourceFolder_Id = ResourceFolder_Id;
                        model.ChargePerson = item;
                        model.CreateUser = loginUser.UserId;
                        model.CreateTime = DateTime.Now;
                        listModel.Add(model);
                    }
                }
                if (listModel.Count > 0)
                {
                    //Rc.Common.DBUtility.DbHelperSQL.Query(_dSql);
                    if (bll.AddPerson(listModel))
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return "2";
            }
        }
        /// <summary>
        /// 移除负责人
        /// </summary>
        /// <param name="userlist"></param>
        /// <param name="relist"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteEmpowers(string TeacherList, string ResourceFolder_Id)
        {
            try
            {
                string _dSql = @"delete PrpeLesson_Person where ResourceFolder_Id = '" + ResourceFolder_Id.Filter() + "' and ChargePerson in ('" + TeacherList.Replace(",", "','") + "')";
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(_dSql) > 0)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch (Exception ex)
            {

                return "";
            }
        }
    }
}