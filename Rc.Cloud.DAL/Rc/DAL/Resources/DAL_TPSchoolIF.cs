namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_TPSchoolIF
    {
        public bool Add(Model_TPSchoolIF model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into TPSchoolIF(");
            builder.Append("SchoolIF_Id,SchoolIF_Name,SchoolIF_Code,SchoolId,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@SchoolIF_Id,@SchoolIF_Name,@SchoolIF_Code,@SchoolId,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolIF_Id", SqlDbType.Char, 0x24), new SqlParameter("@SchoolIF_Name", SqlDbType.VarChar, 200), new SqlParameter("@SchoolIF_Code", SqlDbType.VarChar, 50), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SchoolIF_Id;
            cmdParms[1].Value = model.SchoolIF_Name;
            cmdParms[2].Value = model.SchoolIF_Code;
            cmdParms[3].Value = model.SchoolId;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_TPSchoolIF DataRowToModel(DataRow row)
        {
            Model_TPSchoolIF lif = new Model_TPSchoolIF();
            if (row != null)
            {
                if (row["SchoolIF_Id"] != null)
                {
                    lif.SchoolIF_Id = row["SchoolIF_Id"].ToString();
                }
                if (row["SchoolIF_Name"] != null)
                {
                    lif.SchoolIF_Name = row["SchoolIF_Name"].ToString();
                }
                if (row["SchoolIF_Code"] != null)
                {
                    lif.SchoolIF_Code = row["SchoolIF_Code"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    lif.SchoolId = row["SchoolId"].ToString();
                }
                if (row["Remark"] != null)
                {
                    lif.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    lif.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    lif.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return lif;
        }

        public bool Delete(string SchoolIF_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TPSchoolIF ");
            builder.Append(" where SchoolIF_Id=@SchoolIF_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolIF_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SchoolIF_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SchoolIF_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from TPSchoolIF ");
            builder.Append(" where SchoolIF_Id in (" + SchoolIF_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SchoolIF_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from TPSchoolIF");
            builder.Append(" where SchoolIF_Id=@SchoolIF_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolIF_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SchoolIF_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SchoolIF_Id,SchoolIF_Name,SchoolIF_Code,SchoolId,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM TPSchoolIF ");
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
            builder.Append(" SchoolIF_Id,SchoolIF_Name,SchoolIF_Code,SchoolId,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM TPSchoolIF ");
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
                builder.Append("order by T.SchoolIF_Id desc");
            }
            builder.Append(")AS Row, T.*  from TPSchoolIF T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageSchool(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.SchoolIF_Id desc");
            }
            builder.Append(")AS Row, T.*  from (\r\nselect a.*,ug.UserGroup_Name as SchoolName from TPSchoolIF a\r\nleft join UserGroup ug on ug.UserGroup_Id=a.SchoolId\r\n                ) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_TPSchoolIF GetModel(string SchoolIF_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SchoolIF_Id,SchoolIF_Name,SchoolIF_Code,SchoolId,Remark,CreateUser,CreateTime from TPSchoolIF ");
            builder.Append(" where SchoolIF_Id=@SchoolIF_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolIF_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SchoolIF_Id;
            new Model_TPSchoolIF();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_TPSchoolIF GetModelBySchoolIF_Code(string SchoolIF_Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SchoolIF_Id,SchoolIF_Name,SchoolIF_Code,SchoolId,Remark,CreateUser,CreateTime from TPSchoolIF ");
            builder.Append(" where SchoolIF_Code=@SchoolIF_Code ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolIF_Code", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = SchoolIF_Code;
            new Model_TPSchoolIF();
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
            builder.Append("select count(1) FROM TPSchoolIF ");
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

        public int GetRecordCountSchool(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM ( ");
            builder.Append("select a.*,ug.UserGroup_Name as SchoolName from TPSchoolIF a\r\nleft join UserGroup ug on ug.UserGroup_Id=a.SchoolId");
            builder.Append(" ) T ");
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

        public bool Update(Model_TPSchoolIF model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update TPSchoolIF set ");
            builder.Append("SchoolIF_Name=@SchoolIF_Name,");
            builder.Append("SchoolIF_Code=@SchoolIF_Code,");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where SchoolIF_Id=@SchoolIF_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolIF_Name", SqlDbType.VarChar, 200), new SqlParameter("@SchoolIF_Code", SqlDbType.VarChar, 50), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolIF_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SchoolIF_Name;
            cmdParms[1].Value = model.SchoolIF_Code;
            cmdParms[2].Value = model.SchoolId;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.SchoolIF_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

