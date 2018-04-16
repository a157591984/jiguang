namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Text;

    public class CommonHandel
    {
        public string GetClassNameByID(string ClassID)
        {
            return DbHelperSQL.GetSingle("select UserGroup_Name from UserGroup where UserGroup_Id='" + ClassID + "'").ToString();
        }

        public DataSet GetGradeAllClass(string GradeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct(ClassId),ClassName,ug.UserGroupOrder from VW_UserOnClassGradeSchool ucg \r\ninner join UserGroup ug on ug.UserGroup_Id=ucg.ClassId\r\nwhere GradeId='{0}' and IType='class' order by ug.UserGroupOrder", GradeID);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetHomeWorkInfo(string StrWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select hw.HomeWork_Id,hw.HomeWork_Name,vw.ClassName,vw.GradeName\r\n,ISNULL(fu.TrueName,fu.UserName) TeacherName\r\n,SubjectName=(select D_Name from dbo.Common_Dict where re.Subject=Common_Dict_ID)\r\n,HW_Score=(select SUM(TestQuestions_SumScore) from TestQuestions where ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id) \r\nfrom HomeWork hw\r\nleft join F_User fu on fu.UserId=hw.HomeWork_AssignTeacher\r\nleft join dbo.ResourceToResourceFolder re on re.ResourceToResourceFolder_Id=hw.ResourceToResourceFolder_Id\r\nleft join VW_ClassGradeSchool vw on vw.ClassId=hw.UserGroup_Id");
            if (StrWhere.Trim() != "")
            {
                builder.Append(" where " + StrWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageForCommonHandel(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by StatsClassHW_ScoreID desc");
            }
            builder.Append(")AS Row,T.ResourceToResourceFolder_Id,T.HomeWork_Name,T.BeginTime HomeWorkCreateTime,T.CreateTime,T.HomeWork_Status,T.HomeWork_Id,sc.SubjectID,sc.StatsClassHW_ScoreID,sc.HighestScore,sc.LowestScore,sc.Mode,sc.AVGScore,sc.Median,T.HomeWork_AssignTeacher TeacherID,T.UserGroup_Id ClassId,ug.UserGroup_Name ClassName from homework T \r\nleft join UserGroup ug on ug.UserGroup_Id=T.UserGroup_Id\r\nleft join StatsClassHW_Score sc  on T.HomeWork_Id=sc.HomeWork_ID ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCountForCommonHandel(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM");
            builder.Append("(select T.ResourceToResourceFolder_Id,T.HomeWork_Name,T.BeginTime HomeWorkCreateTime,T.CreateTime,T.HomeWork_Status,T.HomeWork_Id,re.Subject SubjectID,sc.StatsClassHW_ScoreID,sc.HighestScore,sc.LowestScore,sc.Mode,sc.AVGScore,sc.Median,T.HomeWork_AssignTeacher TeacherID,T.UserGroup_Id ClassId,ug.UserGroup_Name from homework T \r\ninner join dbo.ResourceToResourceFolder re on re.ResourceToResourceFolder_Id=T.ResourceToResourceFolder_Id \r\nleft join UserGroup ug on ug.UserGroup_Id=T.UserGroup_Id\r\nleft join StatsClassHW_Score sc  on T.HomeWork_Id=sc.HomeWork_ID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.Append(") d");
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public string GetStudentNameByID(string StudentId)
        {
            return DbHelperSQL.GetSingle("select Isnull(TrueName,UserName) from F_User where UserId='" + StudentId + "'").ToString();
        }

        public DataSet GetTeacherAllClass(string TeacherId, string StrWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct vw.ClassId,vw.ClassName,ug.UserGroupOrder from VW_UserOnClassGradeSchool vw\r\ninner join UserGroup ug on ug.UserGroup_Id=vw.ClassId\r\nwhere ClassMemberShipEnum in('{0}','{1}')\r\nand UserId='{2}'" + StrWhere + " order by ug.UserGroupOrder", MembershipEnum.headmaster, MembershipEnum.teacher, TeacherId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTeacherAllClassByRTRFolder_Id(string TeacherId, string ResourceToResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct vw.ClassId,vw.ClassName,ug.UserGroupOrder\r\n,AnalysisDataCount= case when (select COUNT(1) from StatsClassHW_Score where ClassID=vw.ClassId and TeacherId='{2}' and ResourceToResourceFolder_Id='{3}')=0 then 0 else 1 end\r\nfrom VW_UserOnClassGradeSchool vw\r\ninner join UserGroup ug on ug.UserGroup_Id=vw.ClassId\r\nwhere ClassMemberShipEnum in('{0}','{1}')\r\nand UserId='{2}' order by ug.UserGroupOrder", new object[] { MembershipEnum.headmaster, MembershipEnum.teacher, TeacherId, ResourceToResourceFolder_Id });
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTeacherAllClassBySubject(string TeacherId, string Subject)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct vw.ClassId,vw.ClassName,ug.UserGroupOrder\r\n,AnalysisDataCount= case when (select COUNT(1) from StatsClassHW_Score where ClassID=vw.ClassId and TeacherId='{2}' and SubjectID='{3}')=0 then 0 else 1 end\r\nfrom VW_UserOnClassGradeSchool vw\r\ninner join UserGroup ug on ug.UserGroup_Id=vw.ClassId\r\nwhere ClassMemberShipEnum in('{0}','{1}')\r\nand UserId='{2}'  order by ug.UserGroupOrder", new object[] { MembershipEnum.headmaster, MembershipEnum.teacher, TeacherId, Subject });
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetTeacherAllClassForCommon(string TeacherId, string StrWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct vw.ClassId,vw.ClassName,ug.UserGroupOrder from VW_UserOnClassGradeSchool vw\r\ninner join UserGroup ug on ug.UserGroup_Id=vw.ClassId\r\nwhere ClassMemberShipEnum in('{0}','{1}')\r\nand UserId='{2}'" + StrWhere + " order by ug.UserGroupOrder,ClassName", MembershipEnum.headmaster, MembershipEnum.teacher, TeacherId);
            return DbHelperSQL.Query(builder.ToString());
        }
    }
}

