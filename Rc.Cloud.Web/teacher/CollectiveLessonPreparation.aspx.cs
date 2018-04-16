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
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Common.Config;
namespace Rc.Cloud.Web.teacher
{
    public partial class CollectiveLessonPreparation : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
        private string GetGrade()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                string Strwhere = string.Empty;
                string sql = string.Empty;
                switch (FloginUser.UserPost)
                {
                    #region 校长，副校长，教务主任只显示本学校的
                    case UserPost.校长:
                        Strwhere = @" and SchoolId in (select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                        sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                        break;
                    case UserPost.副校长:
                        Strwhere = @" and SchoolId in (select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                        sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                        break;
                    case UserPost.教务主任:
                        Strwhere = @" and SchoolId in (select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                        sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                        break;
                    #endregion
                    #region 年级组长，教研组长,备课组长只显示本年级
                    case UserPost.教研组长:
                        Strwhere = @" and GradeId in (select distinct GradeId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                        sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                        break;
                    case UserPost.年级组长:
                        Strwhere = @" and GradeId in (select distinct GradeId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                        sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                        break;
                    case UserPost.备课组长:
                        Strwhere = @" and GradeId in (select distinct GradeId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                        sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                        break;
                    #endregion
                    #region 普通老师显示参与备课的年级
                    case UserPost.普通老师:
                        Strwhere = @" where  ChargePerson='" + FloginUser.UserId + "'";
                        sql = @"select distinct t2.Grade as GradeId,t3. UserGroup_Name as GradeName from [dbo].PrpeLesson_Person t1
inner join [dbo].[PrpeLesson] t2 on t2.ResourceFolder_Id=t1.ResourceFolder_Id
inner join UserGroup t3 on t3.UserGroup_Id=t2.Grade" + Strwhere;
                        break;
                    #endregion
                }

                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
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
        private string GetSubject()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                string Strwhere = string.Empty;
                string sql = string.Empty;
                switch (FloginUser.UserPost)
                {
                    case UserPost.普通老师:
                        Strwhere = @" where  ChargePerson='" + FloginUser.UserId + "'";
                        sql = @"select distinct dc.Common_Dict_Id,dc.D_Name  from [dbo].PrpeLesson_Person t1
inner join [dbo].[PrpeLesson] t2 on t2.ResourceFolder_Id=t1.ResourceFolder_Id
inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=t2.Subject" + Strwhere;
                        break;
                    case UserPost.备课组长:
                        Strwhere = @" where  u.Userid='" + FloginUser.UserId + "'";
                        sql = @"select dc.Common_Dict_Id,dc.D_Name from f_user u
inner join [Common_Dict] dc on dc.Common_Dict_Id=u.Subject" + Strwhere;
                        break;
                    default:
                        sql = @"select  distinct dc.Common_Dict_Id,dc.D_Name,dc.D_Order from f_user u
                inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=u.Subject where  Subject<>'' order by dc.D_Order ";
                        break;
                }
                //                string sql = string.Format(@"select  distinct dc.Common_Dict_Id,dc.D_Name,dc.D_Order from f_user u
                //inner join VW_UserOnClassGradeSchool vw on vw.UserId=u.UserId and vw.SchoolId in(select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='{0}')
                //inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=u.Subject where u.Subject<>'' order by dc.D_Order ", FloginUser.UserId.Filter());
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
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
            catch (Exception ex)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }


        [WebMethod]
        public static string GetList(string KeyName, string SubjectID, string GradeID, int PageIndex, int PageSize)
        {
            try
            {
                KeyName = KeyName.Filter();
                SubjectID = SubjectID.Filter();
                GradeID = GradeID.Filter();
                string PpstWhere = string.Empty;
                HttpContext.Current.Session["StatsClassSubject"] = SubjectID;
                string strWhere = " where 1=1 ";
                if (!string.IsNullOrEmpty(KeyName))
                {
                    strWhere += " and  ResourceFolder_Name like '%" + KeyName + "%'";
                }

                strWhere += " and pl.Subject = '" + SubjectID + "'";

                strWhere += " and pl.Grade = '" + GradeID + "'";

                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                if (loginUser.UserPost == UserPost.普通老师)
                {
                    PpstWhere = "inner join PrpeLesson_Person pp on pp.ResourceFolder_Id=pl.ResourceFolder_Id and pp.ChargePerson='" + loginUser.UserId + @"'";
                }
                DataTable dt = new DataTable();
                BLL_PrpeLesson bll = new BLL_PrpeLesson();
                string sql = @" select * from (select ROW_NUMBER() over(ORDER BY pl.CreateTime DESC) row,  pl.*,u.UserName,u.TrueName,dc.D_Name as SubjectName,ug.UserGroup_Name as GradeName,rf.ResourceFolder_Name 
  from PrpeLesson pl " + PpstWhere + @"
inner join ResourceFolder rf on rf.[ResourceFolder_Id]=pl.ResourceFolder_Id
left join Common_Dict dc on dc.Common_Dict_Id=pl.Subject 
left join UserGroup ug on  ug.UserGroup_Id=pl.Grade
left join f_user u on  u.UserId=pl.CreateUser " + strWhere + ")t  where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                string sqlFiles = @" select * from ResourceToResourceFolder rtrf where File_Suffix<>'' and Resource_Type='" + Resource_TypeConst.集体备课文件 + "'";
                DataTable dtFiles = Rc.Common.DBUtility.DbHelperSQL.Query(sqlFiles).Tables[0];
                string countsql = @"select count(*) from PrpeLesson pl " + PpstWhere + @"
inner join ResourceFolder rf on rf.[ResourceFolder_Id]=pl.ResourceFolder_Id
left join Common_Dict dc on dc.Common_Dict_Id=pl.Subject 
left join UserGroup ug on  ug.UserGroup_Id=pl.Grade" + strWhere;
                int intRecordCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(countsql));
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int PersonCount = new BLL_PrpeLesson_Person().GetRecordCount("ResourceFolder_Id='" + dt.Rows[i]["ResourceFolder_Id"] + "'");
                    DataRow[] dr = dtFiles.Select("Book_Id='" + dt.Rows[i]["ResourceFolder_Id"] + "'");
                    int FilesCount = dr.Length;
                    listReturn.Add(new
                    {
                        ResourceFolder_Id = dt.Rows[i]["ResourceFolder_Id"].ToString(),
                        ResourceFolder_Name = dt.Rows[i]["ResourceFolder_Name"].ToString(),
                        GradeName = dt.Rows[i]["GradeName"].ToString(),
                        Subject = dt.Rows[i]["SubjectName"].ToString(),
                        PersonCount = PersonCount,
                        FilesCount = FilesCount,
                        StartTime = pfunction.ToShortDate(dt.Rows[i]["StartTime"].ToString()),
                        EndTime = pfunction.ToShortDate(dt.Rows[i]["EndTime"].ToString()),
                        CreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        UserName = !string.IsNullOrEmpty(dt.Rows[i]["TrueName"].ToString()) ? dt.Rows[i]["TrueName"].ToString() : dt.Rows[i]["UserName"].ToString(),
                        flag = dt.Rows[i]["CreateUser"].ToString() == loginUser.UserId ? "true" : "false",
                        stage = dt.Rows[i]["stage"].ToString()
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
        public static string Delete(string ResourceFolder_Id)
        {
            try
            {
                if (new BLL_ResourceToResourceFolder().GetRecordCount("Book_Id='" + ResourceFolder_Id.Filter() + "'") > 0)
                {
                    return "2";
                }
                else
                {
                    string sql = string.Format(@"delete from PrpeLesson_Person where ResourceFolder_Id='{0}';
                                                   delete from PrpeLesson where ResourceFolder_Id='{0}';
                                                   delete from ResourceFolder where ResourceFolder_Id='{0}';", ResourceFolder_Id.Filter());
                    if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 更改备课状态
        /// </summary>
        /// <param name="reId"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        [WebMethod]
        public static string OperateResourceAuth(string ReId, string stage)
        {
            try
            {
                if (!string.IsNullOrEmpty(ReId))
                {
                    Model_PrpeLesson model = new BLL_PrpeLesson().GetModel(ReId.Filter());
                    if (model != null)
                    {
                        model.Stage = stage.Filter();
                        if (new BLL_PrpeLesson().Update(model))
                        {
                            return "1";
                        }
                        else
                        { return ""; }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}