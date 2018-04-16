namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_HW_TestPaper
    {
        public bool Add(Model_HW_TestPaper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HW_TestPaper(");
            builder.Append("HW_TestPaper_Id,TestPaper_Path,TestPaper_Status,CreateTime)");
            builder.Append(" values (");
            builder.Append("@HW_TestPaper_Id,@TestPaper_Path,@TestPaper_Status,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HW_TestPaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestPaper_Path", SqlDbType.VarChar, 500), new SqlParameter("@TestPaper_Status", SqlDbType.Char, 1), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.HW_TestPaper_Id;
            cmdParms[1].Value = model.TestPaper_Path;
            cmdParms[2].Value = model.TestPaper_Status;
            cmdParms[3].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_HW_TestPaper DataRowToModel(DataRow row)
        {
            Model_HW_TestPaper paper = new Model_HW_TestPaper();
            if (row != null)
            {
                if (row["HW_TestPaper_Id"] != null)
                {
                    paper.HW_TestPaper_Id = row["HW_TestPaper_Id"].ToString();
                }
                if (row["TestPaper_Path"] != null)
                {
                    paper.TestPaper_Path = row["TestPaper_Path"].ToString();
                }
                if (row["TestPaper_Status"] != null)
                {
                    paper.TestPaper_Status = row["TestPaper_Status"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    paper.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return paper;
        }

        public bool Delete(string HW_TestPaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HW_TestPaper ");
            builder.Append(" where HW_TestPaper_Id=@HW_TestPaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HW_TestPaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HW_TestPaper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string HW_TestPaper_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HW_TestPaper ");
            builder.Append(" where HW_TestPaper_Id in (" + HW_TestPaper_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string HW_TestPaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from HW_TestPaper");
            builder.Append(" where HW_TestPaper_Id=@HW_TestPaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HW_TestPaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HW_TestPaper_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select HW_TestPaper_Id,TestPaper_Path,TestPaper_Status,CreateTime ");
            builder.Append(" FROM HW_TestPaper ");
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
            builder.Append(" HW_TestPaper_Id,TestPaper_Path,TestPaper_Status,CreateTime ");
            builder.Append(" FROM HW_TestPaper ");
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
                builder.Append("order by T.HW_TestPaper_Id desc");
            }
            builder.Append(")AS Row, T.*  from HW_TestPaper T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_HW_TestPaper GetModel(string HW_TestPaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 HW_TestPaper_Id,TestPaper_Path,TestPaper_Status,CreateTime from HW_TestPaper ");
            builder.Append(" where HW_TestPaper_Id=@HW_TestPaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HW_TestPaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HW_TestPaper_Id;
            new Model_HW_TestPaper();
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
            builder.Append("select count(1) FROM HW_TestPaper ");
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

        public bool Update(Model_HW_TestPaper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HW_TestPaper set ");
            builder.Append("TestPaper_Path=@TestPaper_Path,");
            builder.Append("TestPaper_Status=@TestPaper_Status,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where HW_TestPaper_Id=@HW_TestPaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TestPaper_Path", SqlDbType.VarChar, 500), new SqlParameter("@TestPaper_Status", SqlDbType.Char, 1), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@HW_TestPaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.TestPaper_Path;
            cmdParms[1].Value = model.TestPaper_Status;
            cmdParms[2].Value = model.CreateTime;
            cmdParms[3].Value = model.HW_TestPaper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

