namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatsSchoolMember
    {
        public bool Add(Model_StatsSchoolMember model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatsSchoolMember(");
            builder.Append("StatsSchoolMember_Id,DateType,DateData,SchoolId,SchoolName,MemberType,MemberCount,CreateTime,NMemberCount)");
            builder.Append(" values (");
            builder.Append("@StatsSchoolMember_Id,@DateType,@DateData,@SchoolId,@SchoolName,@MemberType,@MemberCount,@CreateTime,@NMemberCount)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolMember_Id", SqlDbType.Char, 0x24), new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@SchoolId", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 200), new SqlParameter("@MemberType", SqlDbType.VarChar, 10), new SqlParameter("@MemberCount", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@NMemberCount", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.StatsSchoolMember_Id;
            cmdParms[1].Value = model.DateType;
            cmdParms[2].Value = model.DateData;
            cmdParms[3].Value = model.SchoolId;
            cmdParms[4].Value = model.SchoolName;
            cmdParms[5].Value = model.MemberType;
            cmdParms[6].Value = model.MemberCount;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.NMemberCount;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatsSchoolMember DataRowToModel(DataRow row)
        {
            Model_StatsSchoolMember member = new Model_StatsSchoolMember();
            if (row != null)
            {
                if (row["StatsSchoolMember_Id"] != null)
                {
                    member.StatsSchoolMember_Id = row["StatsSchoolMember_Id"].ToString();
                }
                if (row["DateType"] != null)
                {
                    member.DateType = row["DateType"].ToString();
                }
                if (row["DateData"] != null)
                {
                    member.DateData = row["DateData"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    member.SchoolId = row["SchoolId"].ToString();
                }
                if (row["SchoolName"] != null)
                {
                    member.SchoolName = row["SchoolName"].ToString();
                }
                if (row["MemberType"] != null)
                {
                    member.MemberType = row["MemberType"].ToString();
                }
                if ((row["MemberCount"] != null) && (row["MemberCount"].ToString() != ""))
                {
                    member.MemberCount = new int?(int.Parse(row["MemberCount"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    member.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["NMemberCount"] != null) && (row["NMemberCount"].ToString() != ""))
                {
                    member.NMemberCount = new int?(int.Parse(row["NMemberCount"].ToString()));
                }
            }
            return member;
        }

        public bool Delete(string StatsSchoolMember_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsSchoolMember ");
            builder.Append(" where StatsSchoolMember_Id=@StatsSchoolMember_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolMember_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsSchoolMember_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StatsSchoolMember_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatsSchoolMember ");
            builder.Append(" where StatsSchoolMember_Id in (" + StatsSchoolMember_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StatsSchoolMember_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatsSchoolMember");
            builder.Append(" where StatsSchoolMember_Id=@StatsSchoolMember_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolMember_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsSchoolMember_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StatsSchoolMember_Id,DateType,DateData,SchoolId,SchoolName,MemberType,MemberCount,CreateTime,NMemberCount ");
            builder.Append(" FROM StatsSchoolMember ");
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
            builder.Append(" StatsSchoolMember_Id,DateType,DateData,SchoolId,SchoolName,MemberType,MemberCount,CreateTime,NMemberCount ");
            builder.Append(" FROM StatsSchoolMember ");
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
                builder.Append("order by T.StatsSchoolMember_Id desc");
            }
            builder.Append(")AS Row, T.*  from StatsSchoolMember T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatsSchoolMember GetModel(string StatsSchoolMember_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StatsSchoolMember_Id,DateType,DateData,SchoolId,SchoolName,MemberType,MemberCount,CreateTime,NMemberCount from StatsSchoolMember ");
            builder.Append(" where StatsSchoolMember_Id=@StatsSchoolMember_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StatsSchoolMember_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StatsSchoolMember_Id;
            new Model_StatsSchoolMember();
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
            builder.Append("select count(1) FROM StatsSchoolMember ");
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

        public bool Update(Model_StatsSchoolMember model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatsSchoolMember set ");
            builder.Append("DateType=@DateType,");
            builder.Append("DateData=@DateData,");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("SchoolName=@SchoolName,");
            builder.Append("MemberType=@MemberType,");
            builder.Append("MemberCount=@MemberCount,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("NMemberCount=@NMemberCount");
            builder.Append(" where StatsSchoolMember_Id=@StatsSchoolMember_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DateType", SqlDbType.VarChar, 10), new SqlParameter("@DateData", SqlDbType.VarChar, 20), new SqlParameter("@SchoolId", SqlDbType.VarChar, 10), new SqlParameter("@SchoolName", SqlDbType.NVarChar, 200), new SqlParameter("@MemberType", SqlDbType.VarChar, 10), new SqlParameter("@MemberCount", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@NMemberCount", SqlDbType.Int, 4), new SqlParameter("@StatsSchoolMember_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.DateType;
            cmdParms[1].Value = model.DateData;
            cmdParms[2].Value = model.SchoolId;
            cmdParms[3].Value = model.SchoolName;
            cmdParms[4].Value = model.MemberType;
            cmdParms[5].Value = model.MemberCount;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.NMemberCount;
            cmdParms[8].Value = model.StatsSchoolMember_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

