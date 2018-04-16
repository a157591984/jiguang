namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_UserGroup_Member
    {
        public bool Add(Model_UserGroup_Member model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into UserGroup_Member(");
            builder.Append("UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser)");
            builder.Append(" values (");
            builder.Append("@UserGroup_Member_Id,@UserGroup_Id,@User_ID,@User_ApplicationStatus,@User_ApplicationTime,@User_ApplicationReason,@User_ApplicationPassTime,@UserStatus,@MembershipEnum,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@User_ApplicationStatus", SqlDbType.VarChar, 50), new SqlParameter("@User_ApplicationTime", SqlDbType.DateTime), new SqlParameter("@User_ApplicationReason", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@User_ApplicationPassTime", SqlDbType.DateTime), new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@MembershipEnum", SqlDbType.VarChar, 20), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserGroup_Member_Id;
            cmdParms[1].Value = model.UserGroup_Id;
            cmdParms[2].Value = model.User_ID;
            cmdParms[3].Value = model.User_ApplicationStatus;
            cmdParms[4].Value = model.User_ApplicationTime;
            cmdParms[5].Value = model.User_ApplicationReason;
            cmdParms[6].Value = model.User_ApplicationPassTime;
            cmdParms[7].Value = model.UserStatus;
            cmdParms[8].Value = model.MembershipEnum;
            cmdParms[9].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AgreeMemberJoinGroup(List<Model_UserGroup_Member> listMember, List<Model_Msg> listMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (Model_UserGroup_Member member in listMember)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("update UserGroup_Member set ");
                builder.Append("UserGroup_Id=@UserGroup_Id,");
                builder.Append("User_ID=@User_ID,");
                builder.Append("User_ApplicationStatus=@User_ApplicationStatus,");
                builder.Append("User_ApplicationTime=@User_ApplicationTime,");
                builder.Append("User_ApplicationReason=@User_ApplicationReason,");
                builder.Append("User_ApplicationPassTime=@User_ApplicationPassTime,");
                builder.Append("UserStatus=@UserStatus,");
                builder.Append("MembershipEnum=@MembershipEnum,");
                builder.Append("CreateUser=@CreateUser");
                builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@User_ApplicationStatus", SqlDbType.VarChar, 50), new SqlParameter("@User_ApplicationTime", SqlDbType.DateTime), new SqlParameter("@User_ApplicationReason", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@User_ApplicationPassTime", SqlDbType.DateTime), new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@MembershipEnum", SqlDbType.VarChar, 20), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
                parameterArray[0].Value = member.UserGroup_Id;
                parameterArray[1].Value = member.User_ID;
                parameterArray[2].Value = member.User_ApplicationStatus;
                parameterArray[3].Value = member.User_ApplicationTime;
                parameterArray[4].Value = member.User_ApplicationReason;
                parameterArray[5].Value = member.User_ApplicationPassTime;
                parameterArray[6].Value = member.UserStatus;
                parameterArray[7].Value = member.MembershipEnum;
                parameterArray[8].Value = member.CreateUser;
                parameterArray[9].Value = member.UserGroup_Member_Id;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            foreach (Model_Msg msg in listMsg)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into Msg(");
                builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
                builder.Append(" values (");
                builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray2[0].Value = msg.MsgId;
                parameterArray2[1].Value = msg.MsgEnum;
                parameterArray2[2].Value = msg.MsgTypeEnum;
                parameterArray2[3].Value = msg.ResourceDataId;
                parameterArray2[4].Value = msg.MsgTitle;
                parameterArray2[5].Value = msg.MsgContent;
                parameterArray2[6].Value = msg.MsgStatus;
                parameterArray2[7].Value = msg.MsgSender;
                parameterArray2[8].Value = msg.MsgAccepter;
                parameterArray2[9].Value = msg.CreateTime;
                parameterArray2[10].Value = msg.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public int ClearUserGroup(string UserGroup_Id)
        {
            return DbHelperSQL.ExecuteSql(string.Format("delete from UserGroup_Member where User_Id='{0}';\r\ndelete UserGroup where UserGroup_Id='{0}'; ", UserGroup_Id));
        }

        public int ClearUserGroupMember(string UserGroup_Id)
        {
            return DbHelperSQL.ExecuteSql(string.Format("delete from F_User where UserId in(select User_Id from UserGroup_Member where UserGroup_Id='{0}' and User_Id in(select User_Id from UserGroup_Member group by User_Id having COUNT(*)=1));\r\ndelete UserGroup_Member where UserGroup_Id='{0}'; ", UserGroup_Id));
        }

        public Model_UserGroup_Member DataRowToModel(DataRow row)
        {
            Model_UserGroup_Member member = new Model_UserGroup_Member();
            if (row != null)
            {
                if (row["UserGroup_Member_Id"] != null)
                {
                    member.UserGroup_Member_Id = row["UserGroup_Member_Id"].ToString();
                }
                if (row["UserGroup_Id"] != null)
                {
                    member.UserGroup_Id = row["UserGroup_Id"].ToString();
                }
                if (row["User_ID"] != null)
                {
                    member.User_ID = row["User_ID"].ToString();
                }
                if (row["User_ApplicationStatus"] != null)
                {
                    member.User_ApplicationStatus = row["User_ApplicationStatus"].ToString();
                }
                if ((row["User_ApplicationTime"] != null) && (row["User_ApplicationTime"].ToString() != ""))
                {
                    member.User_ApplicationTime = new DateTime?(DateTime.Parse(row["User_ApplicationTime"].ToString()));
                }
                if (row["User_ApplicationReason"] != null)
                {
                    member.User_ApplicationReason = row["User_ApplicationReason"].ToString();
                }
                if ((row["User_ApplicationPassTime"] != null) && (row["User_ApplicationPassTime"].ToString() != ""))
                {
                    member.User_ApplicationPassTime = new DateTime?(DateTime.Parse(row["User_ApplicationPassTime"].ToString()));
                }
                if ((row["UserStatus"] != null) && (row["UserStatus"].ToString() != ""))
                {
                    member.UserStatus = new int?(int.Parse(row["UserStatus"].ToString()));
                }
                if (row["MembershipEnum"] != null)
                {
                    member.MembershipEnum = row["MembershipEnum"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    member.CreateUser = row["CreateUser"].ToString();
                }
            }
            return member;
        }

        public bool Delete(string UserGroup_Member_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserGroup_Member ");
            builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserGroup_Member_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(string UserGroup_Member_Id, Model_Msg modelMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("delete from UserGroup_Member ");
            builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = UserGroup_Member_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into Msg(");
            builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = modelMsg.MsgId;
            parameterArray2[1].Value = modelMsg.MsgEnum;
            parameterArray2[2].Value = modelMsg.MsgTypeEnum;
            parameterArray2[3].Value = modelMsg.ResourceDataId;
            parameterArray2[4].Value = modelMsg.MsgTitle;
            parameterArray2[5].Value = modelMsg.MsgContent;
            parameterArray2[6].Value = modelMsg.MsgStatus;
            parameterArray2[7].Value = modelMsg.MsgSender;
            parameterArray2[8].Value = modelMsg.MsgAccepter;
            parameterArray2[9].Value = modelMsg.CreateTime;
            parameterArray2[10].Value = modelMsg.CreateUser;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool DeleteList(string UserGroup_Member_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserGroup_Member ");
            builder.Append(" where UserGroup_Member_Id in (" + UserGroup_Member_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string UserGroup_Member_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from UserGroup_Member");
            builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserGroup_Member_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetAllClassList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT RTRIM(UGM.User_Id) AS UserGroup_Id, UGM.UserGroup_Id AS ParentID,UG.UserGroup_Name ");
            builder.Append(" FROM UserGroup_Member AS UGM LEFT JOIN UserGroup AS UG ON UGM.User_Id = UG.UserGroup_Id ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassMemberList(string strWhere, string orderby)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append("select UGM.*,FU.TrueName,FU.UserName,FU.Email,dic.D_Name as SubjectName from UserGroup_Member ugm\r\n            left join F_User FU on UGM.User_ID=FU.UserId left join Common_Dict dic on dic.Common_Dict_ID=FU.Subject ) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by UserGroup_Member_Id desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassMemberListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserGroup_Member_Id desc");
            }
            builder.Append(")AS Row, T.*  from (\r\n            select UGM.*,FU.TrueName,FU.UserName,FU.Email,dic.D_Name as SubjectName\r\n            ,StudentCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id=UGM.UserGroup_Id and MembershipEnum='" + MembershipEnum.student + "' )\r\n,ug.UserGroup_Name as ClassName\r\n            from dbo.UserGroup_Member UGM \r\n            left join F_User FU on UGM.User_ID=FU.UserId left join Common_Dict dic on dic.Common_Dict_ID=FU.Subject \r\nleft join UserGroup ug on ugm.UserGroup_Id=ug.UserGroup_Id) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassMemberListEX(string strWhere, string HomeWork_Id, string orderby)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "SELECT * from (\r\n            select UGM.*,FU.TrueName,FU.UserName,FU.Email,dic.D_Name as SubjectName,shw.HomeWork_Id\r\n            ,StudentCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id=UGM.UserGroup_Id and MembershipEnum='", MembershipEnum.student, "' )\r\n,ug.UserGroup_Name as ClassName\r\n            from dbo.UserGroup_Member UGM \r\n            left join F_User FU on UGM.User_ID=FU.UserId left join Common_Dict dic on dic.Common_Dict_ID=FU.Subject \r\nleft join UserGroup ug on ugm.UserGroup_Id=ug.UserGroup_Id\r\nleft join Student_HomeWork shw on shw.Student_Id=UGM.User_ID and HomeWork_Id='", HomeWork_Id, "') T " }));
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by UserGroup_Member_Id desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetClassStuList_APP(string classId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UGM.*,FU.TrueName,FU.UserName,ug.UserGroup_Name as ClassName\r\nfrom dbo.UserGroup_Member UGM \r\nleft join F_User FU on UGM.User_ID=FU.UserId left join Common_Dict dic on dic.Common_Dict_ID=FU.Subject \r\nleft join UserGroup ug on ugm.UserGroup_Id=ug.UserGroup_Id ");
            builder.AppendFormat(" WHERE User_ApplicationStatus='passed' and UserStatus='0' and UGM.UserGroup_Id='{0}' and MembershipEnum='{1}' order by TrueName ", classId, MembershipEnum.student);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetCreateAddClassList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT UG.UserGroup_Id,UG.UserGroup_Name ");
            builder.Append(" FROM UserGroup_Member AS UGM LEFT JOIN UserGroup AS UG ON UGM.UserGroup_Id = UG.UserGroup_Id ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetGradeMemberListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserGroup_Member_Id desc");
            }
            builder.Append(string.Concat(new object[] { ")AS Row, T.*  from (select UGM.*\r\n            ,TeacherCount=(select count(1) from (select distinct User_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id=UGM.User_Id and MembershipEnum in('", MembershipEnum.headmaster, "','", MembershipEnum.teacher, "'))t )\r\n            ,StudentCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id=UGM.User_Id and MembershipEnum='", MembershipEnum.student, "' )\r\n            ,UG.UserGroup_Name,NULL as UserName,1 as IType,'班级' as PostName,UG.UserGroupOrder\r\n            from dbo.UserGroup_Member UGM\r\n            left join UserGroup UG on UG.UserGroup_Id=UGM.User_Id  where UGM.MembershipEnum='", MembershipEnum.classrc, "'\r\nunion all\r\nselect UGM.*,0,0,fu.TrueName,fu.UserName,2 as IType,dic.D_Name as PostName,1 as UserGroupOrder\r\nfrom dbo.UserGroup_Member UGM\r\nleft join F_User fu on UGM.User_Id=fu.UserId \r\nleft join Common_Dict dic on dic.Common_Dict_Id=fu.UserPost\r\nwhere UGM.MembershipEnum in('", MembershipEnum.gradedirector, "','", MembershipEnum.GroupLeader, "')) T " }));
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetGradeMemberListByPageForSys(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserGroup_Member_Id desc");
            }
            builder.AppendFormat(")AS Row, T.*  from (\r\nselect UGM.*\r\n,UG.UserGroup_Name as ClassName,'' as UserName,'班级' as PostName\r\n,UG.UserGroup_BriefIntroduction,UG.CreateTime as ClassCreateTime\r\n,ClassUser=(select top 1 UserName from F_User where UserId=(select top 1 User_Id from UserGroup_Member where MembershipEnum='{0}' and UserGroup_Id=ug.UserGroup_Id))\r\n,MemberCount=(select COUNT(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserGroup_Id=ugm.User_ID )\r\n,UGP.UserGroup_Name as ParentUserGroup_Name\r\nfrom dbo.UserGroup_Member UGM\r\nleft join UserGroup UG on UG.UserGroup_Id=UGM.User_Id \r\nleft join UserGroup UGP on UGP.UserGroup_Id=ugm.UserGroup_Id\r\nwhere UGM.MembershipEnum='{1}' ", MembershipEnum.headmaster, MembershipEnum.classrc);
            builder.AppendFormat(" union all\r\nselect UGM.*\r\n,fu.TrueName,fu.UserName,dic.D_Name as PostName\r\n,'' as UserGroup_BriefIntroduction,null,'',0\r\n,UGP.UserGroup_Name as ParentUserGroup_Name\r\nfrom dbo.UserGroup_Member UGM\r\nleft join F_User fu on UGM.User_Id=fu.UserId \r\nleft join Common_Dict dic on dic.Common_Dict_Id=fu.UserPost\r\nleft join UserGroup UGP on UGP.UserGroup_Id=ugm.UserGroup_Id\r\nwhere UGM.MembershipEnum in('{0}','{1}')) T ", MembershipEnum.gradedirector, MembershipEnum.GroupLeader);
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetGradeMemberRecordCountForSys(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "select count(1) FROM (\r\nselect UGM.*\r\n,UG.UserGroup_Name as ClassName\r\nfrom dbo.UserGroup_Member UGM\r\nleft join UserGroup UG on UG.UserGroup_Id=UGM.User_Id where UGM.MembershipEnum='", MembershipEnum.classrc, "'\r\nunion all\r\nselect UGM.*\r\n,fu.UserName as ClassName\r\nfrom dbo.UserGroup_Member UGM\r\nleft join F_User fu on UGM.User_Id=fu.UserId \r\nwhere UGM.MembershipEnum in('", MembershipEnum.gradedirector, "','", MembershipEnum.GroupLeader, "') ) t " }));
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

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser ");
            builder.Append(" FROM UserGroup_Member ");
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
            builder.Append(" UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser ");
            builder.Append(" FROM UserGroup_Member ");
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
                builder.Append("order by T.UserGroup_Member_Id desc");
            }
            builder.Append(")AS Row, T.*  from UserGroup_Member T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_UserGroup_Member GetModel(string UserGroup_Member_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser from UserGroup_Member ");
            builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserGroup_Member_Id;
            new Model_UserGroup_Member();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_UserGroup_Member GetModel(string UserGroup_Id, string User_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser from UserGroup_Member ");
            builder.Append(" where UserGroup_Id=@UserGroup_Id and User_ID=@User_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.Char, 0x24), new SqlParameter("@User_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserGroup_Id;
            cmdParms[1].Value = User_ID;
            new Model_UserGroup_Member();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM UserGroup_Member ");
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

        public DataSet GetSchoolMemberListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserGroup_Member_Id desc");
            }
            builder.AppendFormat(")AS Row, T.*  from (select UGM.*\r\n            ,ClassCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id=UGM.User_Id and MembershipEnum='{0}' ) \r\n            ,TeacherCount=(select count(1) from (select distinct User_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id in(select User_Id from UserGroup_Member where UserGroup_Member.UserGroup_Id=UGM.User_Id and MembershipEnum='{0}' and User_ApplicationStatus='passed' and UserStatus=0 ) and MembershipEnum in('{1}','{2}'))t )\r\n            ,StudentCount=(select count(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Member.UserGroup_Id in(select User_Id from UserGroup_Member where UserGroup_Member.UserGroup_Id=UGM.User_Id and MembershipEnum='{0}' and User_ApplicationStatus='passed' and UserStatus=0 ) and MembershipEnum='{3}' )\r\n            ,UG.UserGroup_Name,NULL as UserName,1 as IType,'年级' as PostName,UG.UserGroupOrder\r\n            from dbo.UserGroup_Member UGM\r\n            left join UserGroup UG on UG.UserGroup_Id=UGM.User_Id where UGM.MembershipEnum='{4}' ", new object[] { MembershipEnum.classrc, MembershipEnum.headmaster, MembershipEnum.teacher, MembershipEnum.student, MembershipEnum.grade });
            builder.AppendFormat(" union all\r\nselect UGM.*,0,0,0,fu.TrueName,fu.UserName,2 as IType,dic.D_Name as PostName,1 as UserGroupOrder\r\nfrom dbo.UserGroup_Member UGM\r\nleft join F_User fu on UGM.User_Id=fu.UserId \r\nleft join Common_Dict dic on dic.Common_Dict_Id=fu.UserPost\r\nwhere UGM.MembershipEnum in('{0}','{1}','{2}','{3}') ) T ", new object[] { MembershipEnum.principal, MembershipEnum.vice_principal, MembershipEnum.Dean, MembershipEnum.TeachingLeader });
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSchoolMemberListByPageForSys(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserGroup_Member_Id desc");
            }
            builder.AppendFormat(")AS Row, T.*  from (\r\nselect UGM.*\r\n,UG.UserGroup_Name as GradeName,'' as UserName,'年级' as PostName\r\n,UG.UserGroup_BriefIntroduction,UG.CreateTime as GradeCreateTime\r\n,GradeUser=(select top 1 UserName from F_User where UserId=(select top 1 User_Id from UserGroup_Member where MembershipEnum='{0}' and UserGroup_Id=ug.UserGroup_Id))\r\n,MemberCount=(select COUNT(1) from UserGroup_Member where User_ApplicationStatus='passed' and UserGroup_Id=ugm.User_ID )\r\n,UGP.UserGroup_Name as ParentUserGroup_Name\r\nfrom dbo.UserGroup_Member UGM\r\nleft join UserGroup UG on UG.UserGroup_Id=UGM.User_Id \r\nleft join UserGroup UGP on UGP.UserGroup_Id=ugm.UserGroup_Id\r\nwhere UGM.MembershipEnum='{1}' ", MembershipEnum.gradedirector, MembershipEnum.grade);
            builder.AppendFormat(" union all\r\nselect UGM.*\r\n,fu.TrueName,fu.UserName,dic.D_Name as PostName\r\n,'' as UserGroup_BriefIntroduction,null,'',0\r\n,UGP.UserGroup_Name as ParentUserGroup_Name\r\nfrom dbo.UserGroup_Member UGM\r\nleft join F_User fu on UGM.User_Id=fu.UserId \r\nleft join Common_Dict dic on dic.Common_Dict_Id=fu.UserPost\r\nleft join UserGroup UGP on UGP.UserGroup_Id=ugm.UserGroup_Id\r\nwhere UGM.MembershipEnum in('{0}','{1}','{2}','{3}')) T ", new object[] { MembershipEnum.principal, MembershipEnum.vice_principal, MembershipEnum.Dean, MembershipEnum.TeachingLeader });
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetSchoolMemberRecordCountForSys(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select count(1) FROM (\r\nselect UGM.*\r\n,UG.UserGroup_Name as GradeName\r\nfrom dbo.UserGroup_Member UGM\r\nleft join UserGroup UG on UG.UserGroup_Id=UGM.User_Id where UGM.MembershipEnum='{0}' ", MembershipEnum.grade);
            builder.AppendFormat("union all\r\nselect UGM.*\r\n,fu.UserName as GradeName\r\nfrom dbo.UserGroup_Member UGM\r\nleft join F_User fu on UGM.User_Id=fu.UserId \r\nwhere UGM.MembershipEnum in('{0}','{1}','{2}','{3}') ) t ", new object[] { MembershipEnum.principal, MembershipEnum.vice_principal, MembershipEnum.Dean, MembershipEnum.TeachingLeader });
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

        public int ImportClassMemberData(List<Model_F_User> listModelFU, List<Model_UserGroup_Member> listModelUGM, Model_UserGroup modelUG)
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
            foreach (Model_UserGroup_Member member in listModelUGM)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into UserGroup_Member(");
                builder.Append("UserGroup_Member_Id,UserGroup_Id,User_ID,User_ApplicationStatus,User_ApplicationTime,User_ApplicationReason,User_ApplicationPassTime,UserStatus,MembershipEnum,CreateUser)");
                builder.Append(" values (");
                builder.Append("@UserGroup_Member_Id,@UserGroup_Id,@User_ID,@User_ApplicationStatus,@User_ApplicationTime,@User_ApplicationReason,@User_ApplicationPassTime,@UserStatus,@MembershipEnum,@CreateUser)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@User_ApplicationStatus", SqlDbType.VarChar, 50), new SqlParameter("@User_ApplicationTime", SqlDbType.DateTime), new SqlParameter("@User_ApplicationReason", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@User_ApplicationPassTime", SqlDbType.DateTime), new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@MembershipEnum", SqlDbType.VarChar, 20), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray2[0].Value = member.UserGroup_Member_Id;
                parameterArray2[1].Value = member.UserGroup_Id;
                parameterArray2[2].Value = member.User_ID;
                parameterArray2[3].Value = member.User_ApplicationStatus;
                parameterArray2[4].Value = member.User_ApplicationTime;
                parameterArray2[5].Value = member.User_ApplicationReason;
                parameterArray2[6].Value = member.User_ApplicationPassTime;
                parameterArray2[7].Value = member.UserStatus;
                parameterArray2[8].Value = member.MembershipEnum;
                parameterArray2[9].Value = member.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            builder = new StringBuilder();
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
            builder.Append("UserGroup_AttrEnum=@UserGroup_AttrEnum");
            builder.Append(" where UserGroup_Id=@UserGroup_Id ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserGroup_ParentId", SqlDbType.VarChar, 0x24), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Name", SqlDbType.NVarChar, 250), new SqlParameter("@GradeType", SqlDbType.Char, 0x24), new SqlParameter("@StartSchoolYear", SqlDbType.Decimal, 5), new SqlParameter("@Grade", SqlDbType.Decimal, 5), new SqlParameter("@Class", SqlDbType.Decimal, 5), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@UserGroupp_Type", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_BriefIntroduction", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UserGroup_AttrEnum", SqlDbType.VarChar, 10), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10) };
            parameterArray3[0].Value = modelUG.UserGroup_ParentId;
            parameterArray3[1].Value = modelUG.User_ID;
            parameterArray3[2].Value = modelUG.UserGroup_Name;
            parameterArray3[3].Value = modelUG.GradeType;
            parameterArray3[4].Value = modelUG.StartSchoolYear;
            parameterArray3[5].Value = modelUG.Grade;
            parameterArray3[6].Value = modelUG.Class;
            parameterArray3[7].Value = modelUG.Subject;
            parameterArray3[8].Value = modelUG.UserGroupp_Type;
            parameterArray3[9].Value = modelUG.UserGroup_BriefIntroduction;
            parameterArray3[10].Value = modelUG.CreateTime;
            parameterArray3[11].Value = modelUG.UserGroup_AttrEnum;
            parameterArray3[12].Value = modelUG.UserGroup_Id;
            dictionary.Add(builder.ToString(), parameterArray3);
            return DbHelperSQL.ExecuteSqlTran(dictionary);
        }

        public bool QuitGradeSchool(string PUserGroup_Id, string UserGroup_Id, Model_Msg modelMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("delete from UserGroup_Member where UserGroup_Id=@PUserGroup_Id and User_Id=@UserGroup_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@PUserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 0x24) };
            parameterArray[0].Value = PUserGroup_Id;
            parameterArray[1].Value = UserGroup_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into Msg(");
            builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = modelMsg.MsgId;
            parameterArray2[1].Value = modelMsg.MsgEnum;
            parameterArray2[2].Value = modelMsg.MsgTypeEnum;
            parameterArray2[3].Value = modelMsg.ResourceDataId;
            parameterArray2[4].Value = modelMsg.MsgTitle;
            parameterArray2[5].Value = modelMsg.MsgContent;
            parameterArray2[6].Value = modelMsg.MsgStatus;
            parameterArray2[7].Value = modelMsg.MsgSender;
            parameterArray2[8].Value = modelMsg.MsgAccepter;
            parameterArray2[9].Value = modelMsg.CreateTime;
            parameterArray2[10].Value = modelMsg.CreateUser;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool RefuseMemberJoinGroup(string UserGroup_Member_Id, List<Model_Msg> listMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.AppendFormat("delete from UserGroup_Member where UserGroup_Member_Id in({0}) ", UserGroup_Member_Id);
            dictionary.Add(builder.ToString(), null);
            int num = 0;
            foreach (Model_Msg msg in listMsg)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into Msg(");
                builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
                builder.Append(" values (");
                builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray[0].Value = msg.MsgId;
                parameterArray[1].Value = msg.MsgEnum;
                parameterArray[2].Value = msg.MsgTypeEnum;
                parameterArray[3].Value = msg.ResourceDataId;
                parameterArray[4].Value = msg.MsgTitle;
                parameterArray[5].Value = msg.MsgContent;
                parameterArray[6].Value = msg.MsgStatus;
                parameterArray[7].Value = msg.MsgSender;
                parameterArray[8].Value = msg.MsgAccepter;
                parameterArray[9].Value = msg.CreateTime;
                parameterArray[10].Value = msg.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool setHeaderMaster(string sessionUserId, string userGroupMemberId, string userGroupId, string userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update UserGroup_Member set MembershipEnum='{0}' where UserGroup_Id='{1}' and User_ID='{2}';", MembershipEnum.teacher, userGroupId, sessionUserId);
            builder.AppendFormat("update UserGroup_Member set MembershipEnum='{0}' where UserGroup_Id='{1}' and User_ID='{2}';", MembershipEnum.headmaster, userGroupId, userId);
            builder.AppendFormat("update UserGroup set User_ID='{0}' where UserGroup_Id='{1}';", userId, userGroupId);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool TeacherRemoveStudent(Model_UserGroup_Member model, Model_Msg modelMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("update UserGroup_Member set UserStatus=@UserStatus ");
            builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = model.UserStatus;
            parameterArray[1].Value = model.UserGroup_Member_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into Msg(");
            builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = modelMsg.MsgId;
            parameterArray2[1].Value = modelMsg.MsgEnum;
            parameterArray2[2].Value = modelMsg.MsgTypeEnum;
            parameterArray2[3].Value = modelMsg.ResourceDataId;
            parameterArray2[4].Value = modelMsg.MsgTitle;
            parameterArray2[5].Value = modelMsg.MsgContent;
            parameterArray2[6].Value = modelMsg.MsgStatus;
            parameterArray2[7].Value = modelMsg.MsgSender;
            parameterArray2[8].Value = modelMsg.MsgAccepter;
            parameterArray2[9].Value = modelMsg.CreateTime;
            parameterArray2[10].Value = modelMsg.CreateUser;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool Update(Model_UserGroup_Member model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserGroup_Member set ");
            builder.Append("UserGroup_Id=@UserGroup_Id,");
            builder.Append("User_ID=@User_ID,");
            builder.Append("User_ApplicationStatus=@User_ApplicationStatus,");
            builder.Append("User_ApplicationTime=@User_ApplicationTime,");
            builder.Append("User_ApplicationReason=@User_ApplicationReason,");
            builder.Append("User_ApplicationPassTime=@User_ApplicationPassTime,");
            builder.Append("UserStatus=@UserStatus,");
            builder.Append("MembershipEnum=@MembershipEnum,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where UserGroup_Member_Id=@UserGroup_Member_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@User_ID", SqlDbType.Char, 0x24), new SqlParameter("@User_ApplicationStatus", SqlDbType.VarChar, 50), new SqlParameter("@User_ApplicationTime", SqlDbType.DateTime), new SqlParameter("@User_ApplicationReason", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@User_ApplicationPassTime", SqlDbType.DateTime), new SqlParameter("@UserStatus", SqlDbType.Int, 4), new SqlParameter("@MembershipEnum", SqlDbType.VarChar, 20), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UserGroup_Member_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserGroup_Id;
            cmdParms[1].Value = model.User_ID;
            cmdParms[2].Value = model.User_ApplicationStatus;
            cmdParms[3].Value = model.User_ApplicationTime;
            cmdParms[4].Value = model.User_ApplicationReason;
            cmdParms[5].Value = model.User_ApplicationPassTime;
            cmdParms[6].Value = model.UserStatus;
            cmdParms[7].Value = model.MembershipEnum;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.UserGroup_Member_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

