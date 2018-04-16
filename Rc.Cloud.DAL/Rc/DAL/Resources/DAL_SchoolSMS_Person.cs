namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SchoolSMS_Person
    {
        public bool Add(Model_SchoolSMS_Person model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SchoolSMS_Person(");
            builder.Append("SchoolSMS_Person_Id,School_Id,Name,PhoneNum,Job,Company,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@SchoolSMS_Person_Id,@School_Id,@Name,@PhoneNum,@Job,@Company,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolSMS_Person_Id", SqlDbType.Char, 0x24), new SqlParameter("@School_Id", SqlDbType.VarChar, 50), new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@PhoneNum", SqlDbType.NVarChar, 11), new SqlParameter("@Job", SqlDbType.NVarChar, 50), new SqlParameter("@Company", SqlDbType.NVarChar, 100), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SchoolSMS_Person_Id;
            cmdParms[1].Value = model.School_Id;
            cmdParms[2].Value = model.Name;
            cmdParms[3].Value = model.PhoneNum;
            cmdParms[4].Value = model.Job;
            cmdParms[5].Value = model.Company;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SchoolSMS_Person DataRowToModel(DataRow row)
        {
            Model_SchoolSMS_Person person = new Model_SchoolSMS_Person();
            if (row != null)
            {
                if (row["SchoolSMS_Person_Id"] != null)
                {
                    person.SchoolSMS_Person_Id = row["SchoolSMS_Person_Id"].ToString();
                }
                if (row["School_Id"] != null)
                {
                    person.School_Id = row["School_Id"].ToString();
                }
                if (row["Name"] != null)
                {
                    person.Name = row["Name"].ToString();
                }
                if (row["PhoneNum"] != null)
                {
                    person.PhoneNum = row["PhoneNum"].ToString();
                }
                if (row["Job"] != null)
                {
                    person.Job = row["Job"].ToString();
                }
                if (row["Company"] != null)
                {
                    person.Company = row["Company"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    person.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    person.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return person;
        }

        public bool Delete(string SchoolSMS_Person_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SchoolSMS_Person ");
            builder.Append(" where SchoolSMS_Person_Id=@SchoolSMS_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolSMS_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SchoolSMS_Person_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SchoolSMS_Person_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SchoolSMS_Person ");
            builder.Append(" where SchoolSMS_Person_Id in (" + SchoolSMS_Person_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SchoolSMS_Person_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SchoolSMS_Person");
            builder.Append(" where SchoolSMS_Person_Id=@SchoolSMS_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolSMS_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SchoolSMS_Person_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SchoolSMS_Person_Id,School_Id,Name,PhoneNum,Job,Company,CreateUser,CreateTime ");
            builder.Append(" FROM SchoolSMS_Person ");
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
            builder.Append(" SchoolSMS_Person_Id,School_Id,Name,PhoneNum,Job,Company,CreateUser,CreateTime ");
            builder.Append(" FROM SchoolSMS_Person ");
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
                builder.Append("order by T.SchoolSMS_Person_Id desc");
            }
            builder.Append(")AS Row, T.*  from SchoolSMS_Person T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SchoolSMS_Person GetModel(string SchoolSMS_Person_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SchoolSMS_Person_Id,School_Id,Name,PhoneNum,Job,Company,CreateUser,CreateTime from SchoolSMS_Person ");
            builder.Append(" where SchoolSMS_Person_Id=@SchoolSMS_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolSMS_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SchoolSMS_Person_Id;
            new Model_SchoolSMS_Person();
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
            builder.Append("select count(1) FROM SchoolSMS_Person ");
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

        public bool Update(Model_SchoolSMS_Person model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SchoolSMS_Person set ");
            builder.Append("School_Id=@School_Id,");
            builder.Append("Name=@Name,");
            builder.Append("PhoneNum=@PhoneNum,");
            builder.Append("Job=@Job,");
            builder.Append("Company=@Company,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where SchoolSMS_Person_Id=@SchoolSMS_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_Id", SqlDbType.VarChar, 50), new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@PhoneNum", SqlDbType.NVarChar, 11), new SqlParameter("@Job", SqlDbType.NVarChar, 50), new SqlParameter("@Company", SqlDbType.NVarChar, 100), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SchoolSMS_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.School_Id;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.PhoneNum;
            cmdParms[3].Value = model.Job;
            cmdParms[4].Value = model.Company;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.SchoolSMS_Person_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

