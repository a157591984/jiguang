using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using Rc.Cloud.Model;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class CloudResStuEmpower : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Module_Id = "10200500";

            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetUserList(string StuName, string TchName, string ClassId, string GradeId, string School, int PageSize, int PageIndex)
        {
            try
            {
                StuName = StuName.Trim().Filter();
                TchName = TchName.Trim().Filter();
                ClassId = ClassId.Trim().Filter();
                GradeId = GradeId.Trim().Filter();
                School = School.Trim().Filter();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                string strWhere1 = string.Empty;
                strWhere += " u.UserIdentity='S' ";
                if (!string.IsNullOrEmpty(ClassId))
                {
                    strWhere1 += string.Format(@" inner join [dbo].[VW_UserOnClassGradeSchool] vwC on vwC.userId=u.userid  and (vwC.ClassId like '%{0}%' or vwC.ClassName like '%{0}%')", ClassId);
                }
                if (!string.IsNullOrEmpty(GradeId))
                {
                    strWhere1 += string.Format(@" inner join [dbo].[VW_UserOnClassGradeSchool] vwG on vwG.userId=u.userid  and (vwG.GradeId like '%{0}%' or vwG.GradeName like '%{0}%')", GradeId);
                }
                if (!string.IsNullOrEmpty(School))
                {
                    strWhere1 += string.Format(@" inner join [dbo].[VW_UserOnClassGradeSchool] vwS on vwS.userId=u.userid  and (vwS.SchoolId like '%{0}%' or vwS.SchoolName like '%{0}%')", School);
                }
                if (!string.IsNullOrEmpty(StuName))
                {
                    strWhere += string.Format(" and u.userName like '%{0}%' ", StuName);
                }
                if (!string.IsNullOrEmpty(TchName))
                {
                    strWhere += string.Format(@" and u.userid in(select ugm.User_ID from UserGroup_Member ugm where ugm.MembershipEnum='{0}' and UserGroup_Id in(
select UserGroup_Id from UserGroup_Member ugm2
inner join F_User u on u.UserId=ugm2.User_ID
where ugm2.User_ApplicationStatus='passed' and ugm2.UserStatus='0' and ugm2.MembershipEnum in('{1}','{2}') and (u.UserName like '%{3}%' or u.TrueName like '%{3}%') ) ) "
                        , MembershipEnum.student
                        , MembershipEnum.headmaster
                        , MembershipEnum.teacher
                        , TchName);
                }
                strSqlCount = @" select count (1) FROM F_User U " + strWhere1 + "where " + strWhere;
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY a.TrueName) row,* from ( SELECT distinct u.UserId,u.UserName,u.TrueName  
FROM F_User U " + strWhere1 + " where " + strWhere + " ) a ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        UserName = dt.Rows[i]["userName"].ToString(),
                        UserId = dt.Rows[i]["userid"].ToString(),
                        ClassName = GetStudentClass(dt.Rows[i]["userid"].ToString(), ""),
                        TeacherName = GetStudentTeacher(dt.Rows[i]["userid"].ToString())
                    });
                }

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
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
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        /// <summary>
        /// 学生所属老师
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        private static string GetStudentTeacher(string studentid)
        {
            string strTemp = string.Empty;
            string strSql = string.Empty;

            strSql = string.Format(@"select u.UserId,u.UserName
from UserGroup ug
inner join UserGroup_Member ugm 
on ug.UserGroup_Id=ugm.UserGroup_Id
inner join F_User u on u.UserId=ug.User_ID
where ugm.User_ID='{0}'", studentid);

            DataTable dt = new DataTable();
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strTemp += string.Format("{0}；", dt.Rows[i]["UserName"].ToString());
            }
            return strTemp;
        }
        /// <summary>
        /// 学生所在班
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        private static string GetStudentClass(string studentid, string teacherid)
        {
            string strTemp = string.Empty;
            string strSql = string.Empty;

            strSql = string.Format(@"select ug.UserGroup_Id,ug.UserGroup_Name
from UserGroup ug
inner join UserGroup_Member ugm 
on ug.UserGroup_Id=ugm.UserGroup_Id
where 1=1");
            if (teacherid != "")
            {
                strSql += string.Format(" and  ug.User_ID='{0}'", teacherid);
            }
            strSql += string.Format(" and ugm.User_ID='{0}'", studentid);
            DataTable dt = new DataTable();
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strTemp += string.Format("{0}({1})；", dt.Rows[i]["UserGroup_Name"].ToString(), dt.Rows[i]["UserGroup_Id"].ToString());
            }
            return strTemp;
        }


    }
}