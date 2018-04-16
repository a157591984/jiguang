namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_DictionarySQlMaintenance
    {
        public bool Add(Model_DictionarySQlMaintenance model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DictionarySQlMaintenance(");
            builder.Append("DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime)");
            builder.Append(" values (");
            builder.Append("@DictionarySQlMaintenance_ID,@DictionarySQlMaintenance_Mark,@DictionarySQlMaintenance_Name,@DictionarySQlMaintenance_Explanation,@DictionarySQlMaintenance_SQL,@DictionarySQlMaintenance_CretateUser,@DictionarySQlMaintenance_CreateTime,@DictionarySQlMaintenance_UpdateUser,@DictionarySQlMaintenance_UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictionarySQlMaintenance_ID", SqlDbType.Char, 0x24), new SqlParameter("@DictionarySQlMaintenance_Mark", SqlDbType.NVarChar, 100), new SqlParameter("@DictionarySQlMaintenance_Name", SqlDbType.NVarChar, 100), new SqlParameter("@DictionarySQlMaintenance_Explanation", SqlDbType.NVarChar, 400), new SqlParameter("@DictionarySQlMaintenance_SQL", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@DictionarySQlMaintenance_CretateUser", SqlDbType.Char, 0x24), new SqlParameter("@DictionarySQlMaintenance_CreateTime", SqlDbType.DateTime), new SqlParameter("@DictionarySQlMaintenance_UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@DictionarySQlMaintenance_UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.DictionarySQlMaintenance_ID;
            cmdParms[1].Value = model.DictionarySQlMaintenance_Mark;
            cmdParms[2].Value = model.DictionarySQlMaintenance_Name;
            cmdParms[3].Value = model.DictionarySQlMaintenance_Explanation;
            cmdParms[4].Value = model.DictionarySQlMaintenance_SQL;
            cmdParms[5].Value = model.DictionarySQlMaintenance_CretateUser;
            cmdParms[6].Value = model.DictionarySQlMaintenance_CreateTime;
            cmdParms[7].Value = model.DictionarySQlMaintenance_UpdateUser;
            cmdParms[8].Value = model.DictionarySQlMaintenance_UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_DictionarySQlMaintenance DataRowToModel(DataRow row)
        {
            Model_DictionarySQlMaintenance maintenance = new Model_DictionarySQlMaintenance();
            if (row != null)
            {
                if (row["DictionarySQlMaintenance_ID"] != null)
                {
                    maintenance.DictionarySQlMaintenance_ID = row["DictionarySQlMaintenance_ID"].ToString();
                }
                if (row["DictionarySQlMaintenance_Mark"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Mark = row["DictionarySQlMaintenance_Mark"].ToString();
                }
                if (row["DictionarySQlMaintenance_Name"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Name = row["DictionarySQlMaintenance_Name"].ToString();
                }
                if (row["DictionarySQlMaintenance_Explanation"] != null)
                {
                    maintenance.DictionarySQlMaintenance_Explanation = row["DictionarySQlMaintenance_Explanation"].ToString();
                }
                if (row["DictionarySQlMaintenance_SQL"] != null)
                {
                    maintenance.DictionarySQlMaintenance_SQL = row["DictionarySQlMaintenance_SQL"].ToString();
                }
                if (row["DictionarySQlMaintenance_CretateUser"] != null)
                {
                    maintenance.DictionarySQlMaintenance_CretateUser = row["DictionarySQlMaintenance_CretateUser"].ToString();
                }
                if ((row["DictionarySQlMaintenance_CreateTime"] != null) && (row["DictionarySQlMaintenance_CreateTime"].ToString() != ""))
                {
                    maintenance.DictionarySQlMaintenance_CreateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_CreateTime"].ToString()));
                }
                if (row["DictionarySQlMaintenance_UpdateUser"] != null)
                {
                    maintenance.DictionarySQlMaintenance_UpdateUser = row["DictionarySQlMaintenance_UpdateUser"].ToString();
                }
                if ((row["DictionarySQlMaintenance_UpdateTime"] != null) && (row["DictionarySQlMaintenance_UpdateTime"].ToString() != ""))
                {
                    maintenance.DictionarySQlMaintenance_UpdateTime = new DateTime?(DateTime.Parse(row["DictionarySQlMaintenance_UpdateTime"].ToString()));
                }
            }
            return maintenance;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from DictionarySQlMaintenance ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime ");
            builder.Append(" FROM DictionarySQlMaintenance ");
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
            builder.Append(" DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime ");
            builder.Append(" FROM DictionarySQlMaintenance ");
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
            builder.Append(")AS Row, T.*  from DictionarySQlMaintenance T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_DictionarySQlMaintenance GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 DictionarySQlMaintenance_ID,DictionarySQlMaintenance_Mark,DictionarySQlMaintenance_Name,DictionarySQlMaintenance_Explanation,DictionarySQlMaintenance_SQL,DictionarySQlMaintenance_CretateUser,DictionarySQlMaintenance_CreateTime,DictionarySQlMaintenance_UpdateUser,DictionarySQlMaintenance_UpdateTime from DictionarySQlMaintenance ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_DictionarySQlMaintenance();
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
            builder.Append("select count(1) FROM DictionarySQlMaintenance ");
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

        public bool Update(Model_DictionarySQlMaintenance model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DictionarySQlMaintenance set ");
            builder.Append("DictionarySQlMaintenance_ID=@DictionarySQlMaintenance_ID,");
            builder.Append("DictionarySQlMaintenance_Mark=@DictionarySQlMaintenance_Mark,");
            builder.Append("DictionarySQlMaintenance_Name=@DictionarySQlMaintenance_Name,");
            builder.Append("DictionarySQlMaintenance_Explanation=@DictionarySQlMaintenance_Explanation,");
            builder.Append("DictionarySQlMaintenance_SQL=@DictionarySQlMaintenance_SQL,");
            builder.Append("DictionarySQlMaintenance_CretateUser=@DictionarySQlMaintenance_CretateUser,");
            builder.Append("DictionarySQlMaintenance_CreateTime=@DictionarySQlMaintenance_CreateTime,");
            builder.Append("DictionarySQlMaintenance_UpdateUser=@DictionarySQlMaintenance_UpdateUser,");
            builder.Append("DictionarySQlMaintenance_UpdateTime=@DictionarySQlMaintenance_UpdateTime");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictionarySQlMaintenance_ID", SqlDbType.Char, 0x24), new SqlParameter("@DictionarySQlMaintenance_Mark", SqlDbType.NVarChar, 100), new SqlParameter("@DictionarySQlMaintenance_Name", SqlDbType.NVarChar, 100), new SqlParameter("@DictionarySQlMaintenance_Explanation", SqlDbType.NVarChar, 400), new SqlParameter("@DictionarySQlMaintenance_SQL", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@DictionarySQlMaintenance_CretateUser", SqlDbType.Char, 0x24), new SqlParameter("@DictionarySQlMaintenance_CreateTime", SqlDbType.DateTime), new SqlParameter("@DictionarySQlMaintenance_UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@DictionarySQlMaintenance_UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.DictionarySQlMaintenance_ID;
            cmdParms[1].Value = model.DictionarySQlMaintenance_Mark;
            cmdParms[2].Value = model.DictionarySQlMaintenance_Name;
            cmdParms[3].Value = model.DictionarySQlMaintenance_Explanation;
            cmdParms[4].Value = model.DictionarySQlMaintenance_SQL;
            cmdParms[5].Value = model.DictionarySQlMaintenance_CretateUser;
            cmdParms[6].Value = model.DictionarySQlMaintenance_CreateTime;
            cmdParms[7].Value = model.DictionarySQlMaintenance_UpdateUser;
            cmdParms[8].Value = model.DictionarySQlMaintenance_UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

