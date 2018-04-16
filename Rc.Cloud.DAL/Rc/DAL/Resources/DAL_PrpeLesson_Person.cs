namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_PrpeLesson_Person
    {
        public bool Add(Model_PrpeLesson_Person model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PrpeLesson_Person(");
            builder.Append("PrpeLesson_Person_Id,ResourceFolder_Id,ChargePerson,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@PrpeLesson_Person_Id,@ResourceFolder_Id,@ChargePerson,@CreateTime,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ChargePerson", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.PrpeLesson_Person_Id;
            cmdParms[1].Value = model.ResourceFolder_Id;
            cmdParms[2].Value = model.ChargePerson;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddPerson(List<Model_PrpeLesson_Person> list)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            int num = 0;
            StringBuilder builder = new StringBuilder();
            foreach (Model_PrpeLesson_Person person in list)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into PrpeLesson_Person(");
                builder.Append("PrpeLesson_Person_Id,ResourceFolder_Id,ChargePerson,CreateTime,CreateUser)");
                builder.Append(" values (");
                builder.Append("@PrpeLesson_Person_Id,@ResourceFolder_Id,@ChargePerson,@CreateTime,@CreateUser)");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ChargePerson", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray[0].Value = person.PrpeLesson_Person_Id;
                parameterArray[1].Value = person.ResourceFolder_Id;
                parameterArray[2].Value = person.ChargePerson;
                parameterArray[3].Value = person.CreateTime;
                parameterArray[4].Value = person.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_PrpeLesson_Person DataRowToModel(DataRow row)
        {
            Model_PrpeLesson_Person person = new Model_PrpeLesson_Person();
            if (row != null)
            {
                if (row["PrpeLesson_Person_Id"] != null)
                {
                    person.PrpeLesson_Person_Id = row["PrpeLesson_Person_Id"].ToString();
                }
                if (row["ResourceFolder_Id"] != null)
                {
                    person.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["ChargePerson"] != null)
                {
                    person.ChargePerson = row["ChargePerson"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    person.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    person.CreateUser = row["CreateUser"].ToString();
                }
            }
            return person;
        }

        public bool Delete(string PrpeLesson_Person_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PrpeLesson_Person ");
            builder.Append(" where PrpeLesson_Person_Id=@PrpeLesson_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = PrpeLesson_Person_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string PrpeLesson_Person_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PrpeLesson_Person ");
            builder.Append(" where PrpeLesson_Person_Id in (" + PrpeLesson_Person_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string PrpeLesson_Person_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from PrpeLesson_Person");
            builder.Append(" where PrpeLesson_Person_Id=@PrpeLesson_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = PrpeLesson_Person_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PrpeLesson_Person_Id,ResourceFolder_Id,ChargePerson,CreateTime,CreateUser ");
            builder.Append(" FROM PrpeLesson_Person ");
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
            builder.Append(" PrpeLesson_Person_Id,ResourceFolder_Id,ChargePerson,CreateTime,CreateUser ");
            builder.Append(" FROM PrpeLesson_Person ");
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
                builder.Append("order by T.PrpeLesson_Person_Id desc");
            }
            builder.Append(")AS Row, T.*  from PrpeLesson_Person T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_PrpeLesson_Person GetModel(string PrpeLesson_Person_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PrpeLesson_Person_Id,ResourceFolder_Id,ChargePerson,CreateTime,CreateUser from PrpeLesson_Person ");
            builder.Append(" where PrpeLesson_Person_Id=@PrpeLesson_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = PrpeLesson_Person_Id;
            new Model_PrpeLesson_Person();
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
            builder.Append("select count(1) FROM PrpeLesson_Person ");
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

        public bool Update(Model_PrpeLesson_Person model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PrpeLesson_Person set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("ChargePerson=@ChargePerson,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where PrpeLesson_Person_Id=@PrpeLesson_Person_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ChargePerson", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@PrpeLesson_Person_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.ChargePerson;
            cmdParms[2].Value = model.CreateTime;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.PrpeLesson_Person_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

