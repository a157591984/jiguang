namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_DictRelation
    {
        public bool Add(Model_DictRelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DictRelation(");
            builder.Append("DictRelation_Id,HeadDict_Id,SonDict_Id,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@DictRelation_Id,@HeadDict_Id,@SonDict_Id,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24), new SqlParameter("@HeadDict_Id", SqlDbType.Char, 0x24), new SqlParameter("@SonDict_Id", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.DictRelation_Id;
            cmdParms[1].Value = model.HeadDict_Id;
            cmdParms[2].Value = model.SonDict_Id;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_DictRelation DataRowToModel(DataRow row)
        {
            Model_DictRelation relation = new Model_DictRelation();
            if (row != null)
            {
                if (row["DictRelation_Id"] != null)
                {
                    relation.DictRelation_Id = row["DictRelation_Id"].ToString();
                }
                if (row["HeadDict_Id"] != null)
                {
                    relation.HeadDict_Id = row["HeadDict_Id"].ToString();
                }
                if (row["SonDict_Id"] != null)
                {
                    relation.SonDict_Id = row["SonDict_Id"].ToString();
                }
                if (row["Remark"] != null)
                {
                    relation.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    relation.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    relation.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return relation;
        }

        public bool Delete(string DictRelation_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from DictRelation ");
            builder.Append(" where DictRelation_Id=@DictRelation_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = DictRelation_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string DictRelation_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from DictRelation ");
            builder.Append(" where DictRelation_Id in (" + DictRelation_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string DictRelation_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from DictRelation");
            builder.Append(" where DictRelation_Id=@DictRelation_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = DictRelation_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DictRelation_Id,HeadDict_Id,SonDict_Id,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM DictRelation ");
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
            builder.Append(" DictRelation_Id,HeadDict_Id,SonDict_Id,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM DictRelation ");
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
                builder.Append("order by T.DictRelation_Id desc");
            }
            builder.Append(")AS Row, T.*  from DictRelation T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_DictRelation GetModel(string DictRelation_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 DictRelation_Id,HeadDict_Id,SonDict_Id,Remark,CreateUser,CreateTime from DictRelation ");
            builder.Append(" where DictRelation_Id=@DictRelation_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = DictRelation_Id;
            new Model_DictRelation();
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
            builder.Append("select count(1) FROM DictRelation ");
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

        public bool Update(Model_DictRelation model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DictRelation set ");
            builder.Append("HeadDict_Id=@HeadDict_Id,");
            builder.Append("SonDict_Id=@SonDict_Id,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where DictRelation_Id=@DictRelation_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HeadDict_Id", SqlDbType.Char, 0x24), new SqlParameter("@SonDict_Id", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HeadDict_Id;
            cmdParms[1].Value = model.SonDict_Id;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.DictRelation_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

