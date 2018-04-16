using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Common
{
    public static class StatsCommonHandle
    {
        /// <summary>
        /// 获取当前登录老师可展示学科（班主任、代课老师，代课老师显示自有学科，班主任显示负责班级所有学科）2016-04-22
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherSubject()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                string classActive = string.Empty;
                //                string strSql = string.Format(@"select distinct SubjectID,SubjectName,TeacherID from StatsClassHW_Score
                //where ClassId in(select UserGroup_Id from UserGroup_Member where UserStatus='0' and MemberShipEnum='{2}' and User_Id='{0}')
                //or (SubjectID='{1}' and TeacherID='{0}')"
                //                    , loginUser.UserId
                //                    , loginUser.Subject
                //                    , MembershipEnum.headmaster);
                string strSql = string.Format(@"select distinct hw.SubjectId,cd.D_Name as SubjectName from HomeWork hw
left join Common_Dict cd on cd.Common_Dict_Id=hw.SubjectId
where hw.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where UserStatus='0' and MemberShipEnum='{2}' and User_Id='{0}')
or (hw.SubjectId='{1}' and hw.HomeWork_AssignTeacher='{0}')"
                    , loginUser.UserId
                    , loginUser.Subject
                    , MembershipEnum.headmaster);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                //BLL_Common_Dict bll_common_dict = new BLL_Common_Dict();
                //bll_common_dict.GetList("Common_Dict_ID = '" + FloginUser.Subject + "' ORDER BY D_CreateTime").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (HttpContext.Current.Session["StatsClassSubject"] == null || HttpContext.Current.Session["StatsClassSubject"].ToString() == "")
                    {
                        HttpContext.Current.Session["StatsClassSubject"] = dt.Rows[i]["SubjectID"];
                    }
                    classActive = (HttpContext.Current.Session["StatsClassSubject"].ToString() == dt.Rows[i]["SubjectID"].ToString()) ? "active" : "";
                    //classActive = (i == 0) ? "active" : "";
                    strHtml.AppendFormat("<li>");
                    strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{2}\" \">{1}</a>"
                        , dt.Rows[i]["SubjectID"]
                        , dt.Rows[i]["SubjectName"]
                        , classActive);
                    strHtml.AppendFormat("</li>");
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 获取当前登录老师（班主任、代课老师）2016-04-22
        /// </summary>
        /// <returns></returns>
        public static string GetAllTeacher()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                string classActive = string.Empty;
                string strSql = string.Format(@"select distinct hw.HomeWork_AssignTeacher as TeacherId ,isnull(fu.TrueName,fu.UserName) as TeacherName from HomeWork hw
left join f_user fu on fu.userId=hw.HomeWork_AssignTeacher
where hw.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where UserStatus='0' and MemberShipEnum='{2}' and User_Id='{0}')
or (hw.SubjectId='{1}' and hw.HomeWork_AssignTeacher='{0}')"
                    , loginUser.UserId
                    , loginUser.Subject
                    , MembershipEnum.headmaster);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    classActive = i == 0 ? "active" : "";
                    strHtml.AppendFormat("<li>");
                    strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{2}\" \">{1}</a>"
                        , dt.Rows[i]["TeacherId"]
                        , dt.Rows[i]["TeacherName"]
                        , classActive);
                    strHtml.AppendFormat("</li>");
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }

        /// <summary>
        /// 获取当前登录老师可展示学科（班主任、代课老师，代课老师显示自有学科，班主任显示负责班级所有学科）2016-05-13  讲义用
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherSubjectForCommon()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                string classActive = string.Empty;
                string strSql = string.Format(@"select distinct  Subject,cd.D_Name,hw.HomeWork_AssignTeacher from HomeWork  hw
left join ResourceToResourceFolder re on re.ResourceToResourceFolder_Id= hw.ResourceToResourceFolder_Id
left join dbo.Common_Dict cd on cd.Common_Dict_ID=re.Subject
where UserGroup_Id in(select UserGroup_Id from UserGroup_Member where UserStatus='0' and MemberShipEnum='{2}' and User_Id='{0}') or (Subject='{1}' and HomeWork_AssignTeacher='{0}')"
                    , loginUser.UserId
                    , loginUser.Subject
                    , MembershipEnum.headmaster);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                //BLL_Common_Dict bll_common_dict = new BLL_Common_Dict();
                //bll_common_dict.GetList("Common_Dict_ID = '" + FloginUser.Subject + "' ORDER BY D_CreateTime").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    classActive = (i == 0) ? "active" : "";
                    strHtml.AppendFormat("<li>");
                    strHtml.AppendFormat("<div class=\"name {0}\">", classActive);
                    strHtml.AppendFormat("<i class=\"tree_btn fa\"></i>");
                    strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" ajax-TeacherId=\"{1}\">{2}</a>"
                        , dt.Rows[i]["Subject"]
                        , dt.Rows[i]["HomeWork_AssignTeacher"]
                        , dt.Rows[i]["D_Name"]);
                    strHtml.AppendFormat("</div>");
                    strHtml.AppendFormat("</li>");
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }

        /// <summary>
        /// 获取当前登录老师可展示学科（布置作业的所有学科）2016-08-15  讲评TS
        /// 17-08-22 调整
        /// </summary>
        public static string GetTeacherSubjectForComment(string tchId)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                string classActive = string.Empty;
                string strSql = string.Format(@"select distinct hw.SubjectId,cd.D_Name,hw.HomeWork_AssignTeacher from HomeWork  hw
inner join dbo.Common_Dict cd on cd.Common_Dict_ID=hw.SubjectId
where HomeWork_AssignTeacher='{0}' ", tchId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    classActive = (i == 0) ? "active" : "";
                    strHtml.AppendFormat("<li>");
                    strHtml.AppendFormat("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"{3}\" ajax-TeacherId=\"{1}\">{2}</a>"
                        , dt.Rows[i]["SubjectId"]
                        , dt.Rows[i]["HomeWork_AssignTeacher"]
                        , dt.Rows[i]["D_Name"]
                        , classActive);
                    strHtml.AppendFormat("</li>");
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 当前登录老师获取数据的时候加上自有班级条件（班主任、代课老师） 2016-04-22
        /// </summary>
        public static string GetStrWhereBySelfClassForTeacherData(string SubjectId)
        {
            string strWhere = string.Empty;
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                if (SubjectId != loginUser.Subject)
                {
                    //                    strWhere = @" and ClassId in(select ClassId from (
                    //select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from StatsClassHW_Score where ClassID=vw.ClassId)
                    //from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum='" + MembershipEnum.headmaster + "' and UserId='" + loginUser.UserId + "') t where AnalysisDataCount>0) ";
                    strWhere = @" and ClassId in(
select distinct vw.ClassId
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum='" + MembershipEnum.headmaster + "' and UserId='" + loginUser.UserId + "' ) ";
                }
                else
                {
                    strWhere = @" and ClassId in(
select distinct vw.ClassId
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum in('" + MembershipEnum.headmaster + "','" + MembershipEnum.teacher + "') and UserId='" + loginUser.UserId + "' ) ";
                }
            }
            catch (Exception)
            {

            }
            return strWhere;
        }

        /// <summary>
        ///  当前登录老师获取数据的时候加上自有班级条件（班主任、代课老师） 2016-05-12 讲评
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public static string GetStrWhereBySelfClassForComment(string SubjectId)
        {
            string strWhere = string.Empty;
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                //                if (SubjectId != loginUser.Subject)
                //                {
                //                    strWhere = @" and T.UserGroup_Id in(select ClassId from (
                //select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from HomeWork where UserGroup_Id=vw.ClassId)
                //from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum='" + MembershipEnum.headmaster + "' and UserId='" + loginUser.UserId + "') t where AnalysisDataCount>0) ";
                //                }
                //                else
                //                {
                strWhere = @" and T.UserGroup_Id in(select ClassId from (
select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from HomeWork where UserGroup_Id=vw.ClassId)
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum in('" + MembershipEnum.headmaster + "','" + MembershipEnum.teacher + "') and UserId='" + loginUser.UserId + "') t where AnalysisDataCount>0) ";
                //}
            }
            catch (Exception)
            {

            }
            return strWhere;
        }

        /// <summary>
        /// 获取老师有效班级（班主任、代课老师）2016-04-22
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherClassByTeacherId(string TeacherId, string ResourceToResourceFolder_Id)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                TeacherId = TeacherId.Filter();
                DataTable dt = new CommonHandel().GetTeacherAllClassByRTRFolder_Id(TeacherId, ResourceToResourceFolder_Id).Tables[0];
                strHtml.Append("<li><a href='javascript:;' class='active' ajax-value=''>全部</a></li>");
                foreach (DataRow item in dt.Rows)
                {
                    strHtml.AppendFormat("<li><a href='javascript:;' ajax-value='{0}' >{1}</a></li>"
                        , item["ClassId"].ToString()
                        , item["ClassName"].ToString());
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 获取老师有效班级（班主任、代课老师）2016-04-22
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherClassBySubject(string TeacherId, string Subject)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                TeacherId = TeacherId.Filter();
                DataTable dt = new CommonHandel().GetTeacherAllClassBySubject(TeacherId, Subject).Tables[0];
                DataRow[] dr = dt.Select("AnalysisDataCount=1");
                strHtml.Append("<li><a href='javascript:;' class='active' ajax-value=''>全部</a></li>");
                foreach (DataRow item in dr)
                {

                    strHtml.AppendFormat("<li><a href='javascript:;' ajax-value='{0}'>{1}</a></li>"
                        , item["ClassId"].ToString()
                        , item["ClassName"].ToString());

                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 获取老师有效班级（班主任、代课老师）2016-08-12 讲义
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherClassByTeacherIdForCommon(string TeacherId, string StrWhere)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                TeacherId = TeacherId.Filter();
                DataTable dt = new CommonHandel().GetTeacherAllClassForCommon(TeacherId, StrWhere).Tables[0];
                strHtml.Append("<li><a href='javascript:;' class='active' ajax-value=''>全部</a></li>");
                foreach (DataRow item in dt.Rows)
                {

                    strHtml.AppendFormat("<li><a href='javascript:;' ajax-value='{0}'>{1}</a></li>"
                        , item["ClassId"].ToString()
                        , item["ClassName"].ToString());

                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 获取老师有效班级(统计二级页面)xpy 2016-04-22
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherClass(string TeacherId, string ClassID, string ClassName, string ResourceToResourceFolder_Id)
        {
            StringBuilder classStr = new StringBuilder();
            //if (string.IsNullOrEmpty(ClassID))
            //{
            ClassID = "-1";
            string classActive = string.Empty;
            DataTable dt = new CommonHandel().GetTeacherAllClassByRTRFolder_Id(TeacherId, ResourceToResourceFolder_Id).Tables[0];
            DataRow[] dr = dt.Select("AnalysisDataCount=1");
            if (HttpContext.Current.Session["StatsClassClassId"] == null || HttpContext.Current.Session["StatsClassClassId"].ToString() == "" || HttpContext.Current.Session["StatsClassClassId"].ToString() == "-1")
            {

                classStr.Append("<li><a href='javascript:;' tt='1' cs='-1' class='active' ajax-value=''>全部</a></li>");
                foreach (DataRow item in dr)
                {
                    string strClassId = item["ClassId"].ToString().Trim();
                    classStr.AppendFormat("<li><a href='javascript:;' tt='1' cs='{0}' >{1}</a></li>"
                        , strClassId
                        , item["ClassName"].ToString());
                }
            }
            else
            {
                classStr.Append("<li><a href='javascript:;' tt='1' cs='-1'  ajax-value=''>全部</a></li>");
                foreach (DataRow item in dr)
                {
                    string strClassId = item["ClassId"].ToString().Trim();
                    classActive = (HttpContext.Current.Session["StatsClassClassId"].ToString() == strClassId) ? "active" : "";
                    classStr.AppendFormat("<li><a href='javascript:;' tt='1' cs='{0}' class='{2}' >{1}</a></li>"
                        , strClassId
                        , item["ClassName"].ToString()
                        , classActive);
                }
            }

            return classStr.ToString();
            //}
            //else
            //{
            //    classStr.AppendFormat("<li><a href='javascript:;' tt='1' cs='{0}' class=\"{2}\">{1}</a></li>"
            //                           , ClassID
            //                           , ClassName
            //                           , "active");
            //    return classStr.ToString();
            //}
        }

        /// <summary>
        /// 获取当前登录人年级数据（班主任和代课老师除外）2016-04-23
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTeacherGradeData()
        {
            DataTable dtReturn = new DataTable();
            StringBuilder strHtml = new StringBuilder();
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                string strSql = string.Empty;
                strSql = string.Format(@"select * from (
select vw.UserId,UserName,ug.UserGroupOrder as GradeOrder,GradeId as GroupId,GradeName as GroupName,GradeMemberShipEnum as GroupMemberShipEnum,GradeGradeType as GroupGradeType from VW_UserOnClassGradeSchool vw 
left join [dbo].[UserGroup] ug on ug.UserGroup_Id=vw.GradeId 
where IType='grade' and GradeMemberShipEnum!='' 

union all
select UserId,UserName,NULL as GradeOrder,SchoolId,SchoolName,SchoolMemberShipEnum,NULL GroupGradeType from VW_UserOnClassGradeSchool where IType='school' and SchoolMemberShipEnum!=''
) t where UserId='{0}'", loginUser.UserId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                StringBuilder strSubjectHtml = new StringBuilder();
                foreach (DataRow itemRow in dt.Rows)
                {
                    if (itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.principal.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.vice_principal.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.Dean.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.TeachingLeader.ToString())
                    {
                        //strSubjectHtml.AppendFormat("select distinct Gradeid,GradeName,ug.GradeType as GroupGradeType from StatsGradeHW_Score hw left join UserGroup ug on ug.UserGroup_Id=hw.Gradeid where SchoolId='{0}' union all "
                        //    , itemRow["GroupId"]);
                        strSubjectHtml.AppendFormat("select distinct GradeOrder,Gradeid,GradeName,GradeGradeType as GroupGradeType from dbo.VW_ClassGradeSchool where SchoolId='{0}' union all ", itemRow["GroupId"]);
                    }
                    else if (itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.gradedirector.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.GroupLeader.ToString())
                    {
                        strSubjectHtml.AppendFormat("select '{0}'as GradeOrder,'{1}' as Gradeid,'{2}' as GradeName, '{3}' as GroupGradeType union all ", itemRow["GradeOrder"], itemRow["GroupId"], itemRow["GroupName"], itemRow["GroupGradeType"]);
                    }
                }
                strSql = string.Empty;
                if (strSubjectHtml.ToString().Length > 10) strSql = string.Format("select distinct * from ({0}) t", strSubjectHtml.ToString().Substring(0, strSubjectHtml.ToString().Length - 10));

                if (!string.IsNullOrEmpty(strSql))
                {
                    strSql = string.Format(@"select *,dict.D_Name as GroupGradeTypeName
,ClassCount=(select count(1) from UserGroup_Member where MembershipEnum='classrc' and UserGroup_Id=t.GradeId and UserStatus=0 )
,StudentCount=(select count(1) from VW_UserOnClassGradeSchool where ClassMemberShipEnum='student' and GradeId=t.GradeId )
from ({0}) t 
left join Common_Dict dict on dict.Common_Dict_Id=t.GroupGradeType order by GradeOrder
", strSql);

                    dtReturn = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                }

            }
            catch (Exception)
            {

            }
            return dtReturn;
        }
        /// <summary>
        /// 获取当前登录人学科数据（班主任和代课老师除外）2016-04-23
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherSubjectData()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                string strSql = string.Empty;
                strSql = string.Format(@"select * from (
select UserId,UserName,GradeId as GroupId,GradeName as GroupName,GradeMemberShipEnum as GroupMemberShipEnum from VW_UserOnClassGradeSchool where IType='grade' and GradeMemberShipEnum!=''
union all
select UserId,UserName,SchoolId,SchoolName,SchoolMemberShipEnum from VW_UserOnClassGradeSchool where IType='school' and SchoolMemberShipEnum!=''
) t where UserId='{0}'", loginUser.UserId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                StringBuilder strSubjectHtml = new StringBuilder();
                foreach (DataRow itemRow in dt.Rows)
                {
                    if (itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.principal.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.vice_principal.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.Dean.ToString())
                    {
                        //strSubjectHtml.AppendFormat("select distinct SubjectId,SubjectName from StatsGradeHW_Score where SchoolId='{0}' union all ", itemRow["GroupId"]);
                        strSubjectHtml.AppendFormat(@"select distinct hw.SubjectId,cd.D_Name as SubjectName from HomeWork hw 
inner join Common_Dict cd on cd.Common_Dict_ID=hw.SubjectId
where hw.UserGroup_Id in(select ClassId from VW_ClassGradeSchool where SchoolId='{0}')  union all ", itemRow["GroupId"]);
                    }
                    else if (itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.gradedirector.ToString())
                    {
                        //strSubjectHtml.AppendFormat("select distinct SubjectId,SubjectName from StatsGradeHW_Score where GradeId='{0}' union all ", itemRow["GroupId"]);
                        strSubjectHtml.AppendFormat(@"select distinct hw.SubjectId,cd.D_Name as SubjectName from HomeWork hw 
inner join Common_Dict cd on cd.Common_Dict_ID=hw.SubjectId
where hw.UserGroup_Id in(select ClassId from VW_ClassGradeSchool where GradeId='{0}')  union all ", itemRow["GroupId"]);
                    }
                    else if (itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.TeachingLeader.ToString() || itemRow["GroupMemberShipEnum"].ToString() == MembershipEnum.GroupLeader.ToString())
                    {
                        strSubjectHtml.AppendFormat("select '{0}' as SubjectId,D_Name as SubjectName from Common_Dict where Common_Dict_id='{0}' union all ", loginUser.Subject);
                    }
                }

                strSql = string.Format("select distinct * from ({0}) t", strSubjectHtml.ToString().Substring(0, strSubjectHtml.ToString().Length - 10));
                string classActive = string.Empty;
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (HttpContext.Current.Session["StatsGradeSubject"] == null || HttpContext.Current.Session["StatsGradeSubject"].ToString() == "")
                    {
                        HttpContext.Current.Session["StatsGradeSubject"] = dt.Rows[i]["SubjectID"];
                    }
                    classActive = (HttpContext.Current.Session["StatsGradeSubject"].ToString() == dt.Rows[i]["SubjectID"].ToString()) ? "active" : "";
                    strHtml.AppendFormat("<li>");
                    strHtml.AppendFormat("<div class=\"mtree_link mtree-link-hook {0}\" ajax-value=\"{1}\">"
                        , classActive
                        , dt.Rows[i]["SubjectID"]);
                    strHtml.AppendFormat("<div class='mtree_indent mtree-indent-hook'></div>");
                    strHtml.AppendFormat("<div class='mtree_btn mtree-btn-hook'></div>");
                    strHtml.AppendFormat("<div class='mtree_name mtree-name-hook'>{0}</div>"
                        , dt.Rows[i]["SubjectName"]);
                    strHtml.AppendFormat("</div>");
                    strHtml.AppendFormat("</li>");
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 获取年级下的所有班级 2016-4-23 xpy
        /// </summary>
        /// <param name="GradeID"></param>
        /// <returns></returns>
        public static string GetGradeAllClass(string GradeID)
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                GradeID = GradeID.Filter();
                DataTable dt = new CommonHandel().GetGradeAllClass(GradeID).Tables[0];
                strHtml.Append("<li><a href='javascript:;' class='active' ajax-value=''>全部</a></li>");
                foreach (DataRow item in dt.Rows)
                {
                    strHtml.AppendFormat("<li><a href='javascript:;' ajax-value='{0}' >{1}</a></li>"
                        , item["ClassId"].ToString()
                        , item["ClassName"].ToString());
                }
            }
            catch (Exception)
            {
                strHtml = new StringBuilder();
            }
            return strHtml.ToString();
        }

    }
}