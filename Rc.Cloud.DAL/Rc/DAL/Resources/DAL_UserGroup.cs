namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_UserGroup
    {
        public bool Add(Model_UserGroup model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into UserGroup(");
            builder.Append("UserGroup_Id,UserGroup_ParentId,User_ID,UserGroup_Name,GradeType,StartSchoolYear,Grade,Class,Subject,UserGroupp_Type,UserGroup_BriefIntroduction,CreateTime,UserGroup_AttrEnum,UserGroupOrder)");
            builder.Append(" values (");
            builder.Append("@UserGroup_Id,@UserGroup_ParentId,@User_ID,@UserGroup_Name,@GradeType,@StartSchoolYear,@Grade,@Class,@Subject,@UserGroupp_Type,@UserGroup_BriefIntroduction,@CreateTime,@UserGroup_AttrEnum,@UserGroupOrder)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@UserGroup_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Name", SqlDbType.NVarChar, 250), new SqlParameter("@GradeType", SqlDbType.Char, 0x24), new SqlParameter("@StartSchoolYear", SqlDbType.Decimal, 5), new SqlParameter("@Grade", SqlDbType.Decimal, 5), new SqlParameter("@Class", SqlDbType.Decimal, 5), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserGroupp_Type", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_BriefIntroduction", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_AttrEnum", SqlDbType.VarChar, 10), new SqlParameter("@UserGroupOrder", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserGroup_Id;
            cmdParms[1].Value = model.UserGroup_ParentId;
            cmdParms[2].Value = model.User_ID;
            cmdParms[3].Value = model.UserGroup_Name;
            cmdParms[4].Value = model.GradeType;
            cmdParms[5].Value = model.StartSchoolYear;
            cmdParms[6].Value = model.Grade;
            cmdParms[7].Value = model.Class;
            cmdParms[8].Value = model.Subject;
            cmdParms[9].Value = model.UserGroupp_Type;
            cmdParms[10].Value = model.UserGroup_BriefIntroduction;
            cmdParms[11].Value = model.CreateTime;
            cmdParms[12].Value = model.UserGroup_AttrEnum;
            cmdParms[13].Value = model.UserGroupOrder;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddGroupUpClassPoolAddMember(Model_UserGroup model, Model_ClassPool modelClassPool, Model_UserGroup_Member modelUGM)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into UserGroup(");
            builder.Append("UserGroup_Id,UserGroup_ParentId,User_ID,UserGroup_Name,GradeType,StartSchoolYear,Grade,Class,Subject,UserGroupp_Type,UserGroup_BriefIntroduction,CreateTime,UserGroup_AttrEnum,UserGroupOrder)");
            builder.Append(" values (");
            builder.Append("@UserGroup_Id,@UserGroup_ParentId,@User_ID,@UserGroup_Name,@GradeType,@StartSchoolYear,@Grade,@Class,@Subject,@UserGroupp_Type,@UserGroup_BriefIntroduction,@CreateTime,@UserGroup_AttrEnum,@UserGroupOrder)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@UserGroup_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Name", SqlDbType.NVarChar, 250), new SqlParameter("@GradeType", SqlDbType.Char, 0x24), new SqlParameter("@StartSchoolYear", SqlDbType.Decimal, 5), new SqlParameter("@Grade", SqlDbType.Decimal, 5), new SqlParameter("@Class", SqlDbType.Decimal, 5), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserGroupp_Type", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_BriefIntroduction", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_AttrEnum", SqlDbType.VarChar, 10), new SqlParameter("@UserGroupOrder", SqlDbType.Int, 4) };
            parameterArray[0].Value = model.UserGroup_Id;
            parameterArray[1].Value = model.UserGroup_ParentId;
            parameterArray[2].Value = model.User_ID;
            parameterArray[3].Value = model.UserGroup_Name;
            parameterArray[4].Value = model.GradeType;
            parameterArray[5].Value = model.StartSchoolYear;
            parameterArray[6].Value = model.Grade;
            parameterArray[7].Value = model.Class;
            parameterArray[8].Value = model.Subject;
            parameterArray[9].Value = model.UserGroupp_Type;
            parameterArray[10].Value = model.UserGroup_BriefIntroduction;
            parameterArray[11].Value = model.CreateTime;
            parameterArray[12].Value = model.UserGroup_AttrEnum;
            parameterArray[13].Value = model.UserGroupOrder;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("update ClassPool set ");
            builder.Append("IsUsed=@IsUsed");
            builder.Append(" where ClassPool_Id=@ClassPool_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@IsUsed", SqlDbType.Int, 4), new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = modelClassPool.IsUsed;
            parameterArray2[1].Value = modelClassPool.ClassPool_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            if (modelUGM != null)
            {
                builder = new StringBuilder();
                builder.Append("insert into UserGroup_Member(");
                builder.Append("UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum)");
                builder.Append(" values (");
                builder.Append("@UserGroup_Member_Id,@UserGroup_Id,@User_ID,@User_ApplicationStatus,@User_ApplicationTime,@User_ApplicationReason,@User_ApplicationPassTime,@UserStatus,@MembershipEnum)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@User_ApplicationStatus", SqlDbType.VarChar, 50), new SqlParameter("@User_ApplicationTime", SqlDbType.DateTime), new SqlParameter("@User_ApplicationReason", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@User_ApplicationPassTime", SqlDbType.DateTime), new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@MembershipEnum", SqlDbType.VarChar, 20) };
                parameterArray3[0].Value = modelUGM.UserGroup_Member_Id;
                parameterArray3[1].Value = modelUGM.UserGroup_Id;
                parameterArray3[2].Value = modelUGM.User_ID;
                parameterArray3[3].Value = modelUGM.User_ApplicationStatus;
                parameterArray3[4].Value = modelUGM.User_ApplicationTime;
                parameterArray3[5].Value = modelUGM.User_ApplicationReason;
                parameterArray3[6].Value = modelUGM.User_ApplicationPassTime;
                parameterArray3[7].Value = modelUGM.UserStatus;
                parameterArray3[8].Value = modelUGM.MembershipEnum;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_UserGroup DataRowToModel(DataRow row)
        {
            Model_UserGroup group = new Model_UserGroup();
            if (row != null)
            {
                if (row["UserGroup_Id"] != null)
                {
                    group.UserGroup_Id = row["UserGroup_Id"].ToString();
                }
                if (row["UserGroup_ParentId"] != null)
                {
                    group.UserGroup_ParentId = row["UserGroup_ParentId"].ToString();
                }
                if (row["User_ID"] != null)
                {
                    group.User_ID = row["User_ID"].ToString();
                }
                if (row["UserGroup_Name"] != null)
                {
                    group.UserGroup_Name = row["UserGroup_Name"].ToString();
                }
                if (row["GradeType"] != null)
                {
                    group.GradeType = row["GradeType"].ToString();
                }
                if ((row["StartSchoolYear"] != null) && (row["StartSchoolYear"].ToString() != ""))
                {
                    group.StartSchoolYear = new decimal?(decimal.Parse(row["StartSchoolYear"].ToString()));
                }
                if ((row["Grade"] != null) && (row["Grade"].ToString() != ""))
                {
                    group.Grade = new decimal?(decimal.Parse(row["Grade"].ToString()));
                }
                if ((row["Class"] != null) && (row["Class"].ToString() != ""))
                {
                    group.Class = new decimal?(decimal.Parse(row["Class"].ToString()));
                }
                if (row["Subject"] != null)
                {
                    group.Subject = row["Subject"].ToString();
                }
                if (row["UserGroupp_Type"] != null)
                {
                    group.UserGroupp_Type = row["UserGroupp_Type"].ToString();
                }
                if (row["UserGroup_BriefIntroduction"] != null)
                {
                    group.UserGroup_BriefIntroduction = row["UserGroup_BriefIntroduction"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    group.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["UserGroup_AttrEnum"] != null)
                {
                    group.UserGroup_AttrEnum = row["UserGroup_AttrEnum"].ToString();
                }
                if ((row["UserGroupOrder"] != null) && (row["UserGroupOrder"].ToString() != ""))
                {
                    group.UserGroupOrder = new int?(int.Parse(row["UserGroupOrder"].ToString()));
                }
            }
            return group;
        }

        public bool Delete(string UserGroup_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserGroup ");
            builder.Append(" where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            cmdParms[0].Value = UserGroup_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string UserGroup_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserGroup ");
            builder.Append(" where UserGroup_Id in (" + UserGroup_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DelGroupUpClassPoolDelMember(string UserGroup_Id, string PUserGroup_Id, Model_ClassPool modelClassPool)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(PUserGroup_Id))
            {
                builder = new StringBuilder();
                builder.Append("delete from UserGroup_Member where UserGroup_Id=@PUserGroup_Id and User_Id=@UserGroup_Id ");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@PUserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 0x24) };
                parameterArray[0].Value = PUserGroup_Id;
                parameterArray[1].Value = UserGroup_Id;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            builder = new StringBuilder();
            builder.Append("delete from UserGroup_Member where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            parameterArray2[0].Value = UserGroup_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            builder = new StringBuilder();
            builder.Append("delete from UserGroup where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            parameterArray3[0].Value = UserGroup_Id;
            dictionary.Add(builder.ToString(), parameterArray3);
            builder = new StringBuilder();
            builder.Append("update ClassPool set ");
            builder.Append("IsUsed=@IsUsed");
            builder.Append(" where ClassPool_Id=@ClassPool_Id ");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@IsUsed", SqlDbType.Int, 4), new SqlParameter("@ClassPool_Id", SqlDbType.Char, 0x24) };
            parameterArray4[0].Value = modelClassPool.IsUsed;
            parameterArray4[1].Value = modelClassPool.ClassPool_Id;
            dictionary.Add(builder.ToString(), parameterArray4);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool Exists(string UserGroup_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from UserGroup");
            builder.Append(" where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            cmdParms[0].Value = UserGroup_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAllClassListByUserIdMembershipEnum(string userId, string UserGroup_AttrEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select UG.*,FU.UserName\r\n,MemberCount=(select COUNT(1) from UserGroup_Member where UserStatus=0 and User_ApplicationStatus='passed' and UserGroup_Id=UG.UserGroup_Id)\r\n,MsgCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='applied' and UserGroup_Id=UG.UserGroup_Id)\r\n,UGM.UserGroup_Id as PGroupId,UG2.UserGroup_Name as PGroupName\r\n,UGM.User_ApplicationStatus as PGroup_User_ApplicationStatus\r\n,UGM.UserStatus as PGroup_UserStatus\r\n,UGM2.User_ApplicationStatus as JoinApplicationStatus\r\n,UGM2.UserStatus as JoinStatus\r\nfrom UserGroup UG\r\nleft join F_User FU on UG.User_ID=FU.UserId \r\nleft join UserGroup_Member UGM on UGM.MembershipEnum='{0}' and UGM.User_ID=UG.UserGroup_Id \r\nleft join UserGroup UG2 on UGM.UserGroup_Id=UG2.UserGroup_Id\r\nleft join UserGroup_Member UGM2 on UGM2.MembershipEnum='{1}' and UGM2.User_ID='{2}' and UGM2.UserGroup_Id=UG.UserGroup_Id ", MembershipEnum.classrc, MembershipEnum.teacher, userId);
            if (userId.Trim() != "")
            {
                builder.AppendFormat(" where UG.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_Id='{0}' ) and UG.UserGroup_AttrEnum='{1}'  order by UG.UserGroupOrder,UG.UserGroup_Name ", userId, UserGroup_AttrEnum);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassListByUserIdMembershipEnum(string userId, string MembershipEnum, string UserGroup_AttrEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select UG.* from UserGroup UG", new object[0]);
            builder.AppendFormat(" where UG.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}' ) and UG.UserGroup_AttrEnum='{1}' order by UG.UserGroupOrder ", userId, UserGroup_AttrEnum);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetGradeListByUserIdMembershipEnum(string userId, string UserGroup_AttrEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select UG.*,FU.UserName\r\n,MemberCount=(select COUNT(1) from UserGroup_Member where UserStatus=0 and User_ApplicationStatus='passed' and UserGroup_Id=UG.UserGroup_Id)\r\n,MsgCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='applied' and UserGroup_Id=UG.UserGroup_Id)\r\n,UGM.UserGroup_Id as PGroupId,UGM.User_ApplicationStatus as PGroup_User_ApplicationStatus,UGM.UserStatus as PGroup_UserStatus,UG2.UserGroup_Name as PGroupName \r\n,UGM2.User_ApplicationStatus as JoinApplicationStatus,UGM2.UserStatus as JoinStatus\r\nfrom UserGroup UG\r\nleft join F_User FU on UG.User_ID=FU.UserId \r\nleft join UserGroup_Member UGM on UGM.MembershipEnum='{0}' and UGM.User_ID=UG.UserGroup_Id\r\nleft join UserGroup UG2 on UGM.UserGroup_Id=UG2.UserGroup_Id \r\nleft join UserGroup_Member UGM2 on UGM2.MembershipEnum in('{1}','{2}') and UGM2.User_ID='{3}' and UGM2.UserGroup_Id=UG.UserGroup_Id ", new object[] { MembershipEnum.grade, MembershipEnum.gradedirector, MembershipEnum.GroupLeader, userId });
            if (userId.Trim() != "")
            {
                builder.AppendFormat(" where (UG.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_Id='{0}' ) or UG.User_Id='{0}') and UG.UserGroup_AttrEnum='{1}' order by UG.UserGroupOrder,UG.UserGroup_Name ", userId, UserGroup_AttrEnum);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetGroupListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserGroup_Id desc");
            }
            builder.Append(")AS Row, T.*  from (select ug.*,fu.UserName,fu.TrueName\r\n,GroupMemberCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and UserGroup_Id=ug.UserGroup_Id )\r\n,ug2.UserGroup_Name as ParentUserGroup_Name\r\nfrom UserGroup ug\r\nleft join F_User fu on fu.UserId=ug.User_ID\r\nleft join UserGroup ug2 on ug2.UserGroup_Id=ug.UserGroup_ParentId\r\n) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserGroup_Id,UserGroup_ParentId,User_ID,UserGroup_Name,GradeType,StartSchoolYear,Grade,Class,Subject,UserGroupp_Type,UserGroup_BriefIntroduction,CreateTime,UserGroup_AttrEnum,UserGroupOrder ");
            builder.Append(" FROM UserGroup ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" UserGroup_Id,UserGroup_ParentId,User_ID,UserGroup_Name,GradeType,StartSchoolYear,Grade,Class,Subject,UserGroupp_Type,UserGroup_BriefIntroduction,CreateTime,UserGroup_AttrEnum,UserGroupOrder ");
            builder.Append(" FROM UserGroup ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.UserGroup_Id desc");
            }
            builder.Append(")AS Row, T.*  from UserGroup T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_UserGroup GetModel(string UserGroup_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserGroup_Id,UserGroup_ParentId,User_ID,UserGroup_Name,GradeType,StartSchoolYear,Grade,Class,Subject,UserGroupp_Type,UserGroup_BriefIntroduction,CreateTime,UserGroup_AttrEnum,UserGroupOrder from UserGroup ");
            builder.Append(" where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            cmdParms[0].Value = UserGroup_Id;
            new Model_UserGroup();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetPassGroupByUserIdUserGroupAttrEnum(string userId, string UserGroup_AttrEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UG.* from UserGroup UG ");
            builder.AppendFormat(" where (UG.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_Id='{0}' and User_ApplicationStatus='passed' and UserStatus='0' ) or UG.User_Id='{0}') and UG.UserGroup_AttrEnum='{1}' order by UG.UserGroupOrder ", userId, UserGroup_AttrEnum);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM UserGroup ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetSchoolListByUserIdMembershipEnum(string userId, string UserGroup_AttrEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select UG.*,FU.UserName\r\n,MemberCount=(select COUNT(1) from UserGroup_Member where UserStatus=0 and User_ApplicationStatus='passed' and UserGroup_Id=UG.UserGroup_Id)\r\n,MsgCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='applied' and UserGroup_Id=UG.UserGroup_Id)\r\n,UGM.User_ApplicationStatus as JoinApplicationStatus,UGM.UserStatus as JoinStatus\r\nfrom UserGroup UG\r\nleft join F_User FU on UG.User_ID=FU.UserId \r\nleft join UserGroup_Member UGM on UGM.MembershipEnum in('{0}','{1}','{2}','{3}') and UGM.User_ID='{4}' and UGM.UserGroup_Id=UG.UserGroup_Id ", new object[] { MembershipEnum.principal, MembershipEnum.vice_principal, MembershipEnum.Dean, MembershipEnum.TeachingLeader, userId });
            if (userId.Trim() != "")
            {
                builder.AppendFormat(" where (UG.UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_Id='{0}' ) or UG.User_Id='{0}') and UG.UserGroup_AttrEnum='{1}' order by UG.UserGroupOrder ", userId, UserGroup_AttrEnum);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int ImportGradeData(List<Model_F_User> listModelFU, List<Model_UserGroup> listModelUG, List<Model_ClassPool> listModelCP, List<Model_UserGroup_Member> listModelUGM)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (Model_F_User user in listModelFU)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into F_User(");
                builder.Append("UserId,UserName,Password,TrueName,UserIdentity,Birthday,Sex,Email,Mobile,Province,City,County,School,CreateTime,Stoken,StokenTime,Resource_Version,GradeTerm,Subject,UserPost)");
                builder.Append(" values (");
                builder.Append("@UserId,@UserName,@Password,@TrueName,@UserIdentity,@Birthday,@Sex,@Email,@Mobile,@Province,@City,@County,@School,@CreateTime,@Stoken,@StokenTime,@Resource_Version,@GradeTerm,@Subject,@UserPost)");
                SqlParameter[] parameterArray = new SqlParameter[] { 
                    new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@UserName", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.Char, 0x20), new SqlParameter("@TrueName", SqlDbType.NVarChar, 250), new SqlParameter("@UserIdentity", SqlDbType.Char, 1), new SqlParameter("@Birthday", SqlDbType.DateTime), new SqlParameter("@Sex", SqlDbType.Char, 1), new SqlParameter("@Email", SqlDbType.VarChar, 200), new SqlParameter("@Mobile", SqlDbType.VarChar, 20), new SqlParameter("@Province", SqlDbType.NVarChar, 250), new SqlParameter("@City", SqlDbType.NVarChar, 250), new SqlParameter("@County", SqlDbType.NVarChar, 250), new SqlParameter("@School", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Stoken", SqlDbType.NVarChar, 50), new SqlParameter("@StokenTime", SqlDbType.DateTime), 
                    new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserPost", SqlDbType.Char, 0x24)
                 };
                parameterArray[0].Value = user.UserId;
                parameterArray[1].Value = user.UserName;
                parameterArray[2].Value = user.Password;
                parameterArray[3].Value = user.TrueName;
                parameterArray[4].Value = user.UserIdentity;
                parameterArray[5].Value = user.Birthday;
                parameterArray[6].Value = user.Sex;
                parameterArray[7].Value = user.Email;
                parameterArray[8].Value = user.Mobile;
                parameterArray[9].Value = user.Province;
                parameterArray[10].Value = user.City;
                parameterArray[11].Value = user.County;
                parameterArray[12].Value = user.School;
                parameterArray[13].Value = user.CreateTime;
                parameterArray[14].Value = user.Stoken;
                parameterArray[15].Value = user.StokenTime;
                parameterArray[0x10].Value = user.Resource_Version;
                parameterArray[0x11].Value = user.GradeTerm;
                parameterArray[0x12].Value = user.Subject;
                parameterArray[0x13].Value = user.UserPost;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            foreach (Model_UserGroup group in listModelUG)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into UserGroup(");
                builder.Append("UserGroup_Id,UserGroup_ParentId,User_ID,UserGroup_Name,GradeType,StartSchoolYear,Grade,Class,Subject,UserGroupp_Type,UserGroup_BriefIntroduction,CreateTime,UserGroup_AttrEnum)");
                builder.Append(" values (");
                builder.Append("@UserGroup_Id,@UserGroup_ParentId,@User_ID,@UserGroup_Name,@GradeType,@StartSchoolYear,@Grade,@Class,@Subject,@UserGroupp_Type,@UserGroup_BriefIntroduction,@CreateTime,@UserGroup_AttrEnum)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@UserGroup_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Name", SqlDbType.NVarChar, 250), new SqlParameter("@GradeType", SqlDbType.Char, 0x24), new SqlParameter("@StartSchoolYear", SqlDbType.Decimal, 5), new SqlParameter("@Grade", SqlDbType.Decimal, 5), new SqlParameter("@Class", SqlDbType.Decimal, 5), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserGroupp_Type", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_BriefIntroduction", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_AttrEnum", SqlDbType.VarChar, 10) };
                parameterArray2[0].Value = group.UserGroup_Id;
                parameterArray2[1].Value = group.UserGroup_ParentId;
                parameterArray2[2].Value = group.User_ID;
                parameterArray2[3].Value = group.UserGroup_Name;
                parameterArray2[4].Value = group.GradeType;
                parameterArray2[5].Value = group.StartSchoolYear;
                parameterArray2[6].Value = group.Grade;
                parameterArray2[7].Value = group.Class;
                parameterArray2[8].Value = group.Subject;
                parameterArray2[9].Value = group.UserGroupp_Type;
                parameterArray2[10].Value = group.UserGroup_BriefIntroduction;
                parameterArray2[11].Value = group.CreateTime;
                parameterArray2[12].Value = group.UserGroup_AttrEnum;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            builder = new StringBuilder();
            builder.Append("update ClassPool set IsUsed='0' where IsUsed='1' and Class_Id not in(select UserGroup_Id from UserGroup)");
            dictionary.Add(builder.ToString(), null);
            foreach (Model_UserGroup_Member member in listModelUGM)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into UserGroup_Member(");
                builder.Append("UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser)");
                builder.Append(" values (");
                builder.Append("@UserGroup_Member_Id,@UserGroup_Id,@User_ID,@User_ApplicationStatus,@User_ApplicationTime,@User_ApplicationReason,@User_ApplicationPassTime,@UserStatus,@MembershipEnum,@CreateUser)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@User_ApplicationStatus", SqlDbType.VarChar, 50), new SqlParameter("@User_ApplicationTime", SqlDbType.DateTime), new SqlParameter("@User_ApplicationReason", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@User_ApplicationPassTime", SqlDbType.DateTime), new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@MembershipEnum", SqlDbType.VarChar, 20), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray3[0].Value = member.UserGroup_Member_Id;
                parameterArray3[1].Value = member.UserGroup_Id;
                parameterArray3[2].Value = member.User_ID;
                parameterArray3[3].Value = member.User_ApplicationStatus;
                parameterArray3[4].Value = member.User_ApplicationTime;
                parameterArray3[5].Value = member.User_ApplicationReason;
                parameterArray3[6].Value = member.User_ApplicationPassTime;
                parameterArray3[7].Value = member.UserStatus;
                parameterArray3[8].Value = member.MembershipEnum;
                parameterArray3[9].Value = member.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            return DbHelperSQL.ExecuteSqlTran(dictionary);
        }

        public bool Update(Model_UserGroup model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserGroup set ");
            builder.Append("UserGroup_ParentId=@UserGroup_ParentId,");
            builder.Append("User_ID=@User_ID,");
            builder.Append("UserGroup_Name=@UserGroup_Name,");
            builder.Append("GradeType=@GradeType,");
            builder.Append("StartSchoolYear=@StartSchoolYear,");
            builder.Append("Grade=@Grade,");
            builder.Append("Class=@Class,");
            builder.Append("Subject=@Subject,");
            builder.Append("UserGroupp_Type=@UserGroupp_Type,");
            builder.Append("UserGroup_BriefIntroduction=@UserGroup_BriefIntroduction,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UserGroup_AttrEnum=@UserGroup_AttrEnum,");
            builder.Append("UserGroupOrder=@UserGroupOrder");
            builder.Append(" where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Name", SqlDbType.NVarChar, 250), new SqlParameter("@GradeType", SqlDbType.Char, 0x24), new SqlParameter("@StartSchoolYear", SqlDbType.Decimal, 5), new SqlParameter("@Grade", SqlDbType.Decimal, 5), new SqlParameter("@Class", SqlDbType.Decimal, 5), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserGroupp_Type", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_BriefIntroduction", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_AttrEnum", SqlDbType.VarChar, 10), new SqlParameter("@UserGroupOrder", SqlDbType.Int, 4), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            cmdParms[0].Value = model.UserGroup_ParentId;
            cmdParms[1].Value = model.User_ID;
            cmdParms[2].Value = model.UserGroup_Name;
            cmdParms[3].Value = model.GradeType;
            cmdParms[4].Value = model.StartSchoolYear;
            cmdParms[5].Value = model.Grade;
            cmdParms[6].Value = model.Class;
            cmdParms[7].Value = model.Subject;
            cmdParms[8].Value = model.UserGroupp_Type;
            cmdParms[9].Value = model.UserGroup_BriefIntroduction;
            cmdParms[10].Value = model.CreateTime;
            cmdParms[11].Value = model.UserGroup_AttrEnum;
            cmdParms[12].Value = model.UserGroupOrder;
            cmdParms[13].Value = model.UserGroup_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

