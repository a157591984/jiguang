namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StudentToParent
    {
        public bool Add(Model_StudentToParent model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StudentToParent(");
            builder.Append("StudentToParent_ID,Student_ID,Parent_ID,CreateTime)");
            builder.Append(" values (");
            builder.Append("@StudentToParent_ID,@Student_ID,@Parent_ID,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StudentToParent_ID", SqlDbType.Char, 0x24), new SqlParameter("@Student_ID", SqlDbType.Char, 0x24), new SqlParameter("@Parent_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.StudentToParent_ID;
            cmdParms[1].Value = model.Student_ID;
            cmdParms[2].Value = model.Parent_ID;
            cmdParms[3].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StudentToParent DataRowToModel(DataRow row)
        {
            Model_StudentToParent parent = new Model_StudentToParent();
            if (row != null)
            {
                if (row["StudentToParent_ID"] != null)
                {
                    parent.StudentToParent_ID = row["StudentToParent_ID"].ToString();
                }
                if (row["Student_ID"] != null)
                {
                    parent.Student_ID = row["Student_ID"].ToString();
                }
                if (row["Parent_ID"] != null)
                {
                    parent.Parent_ID = row["Parent_ID"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    parent.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return parent;
        }

        public Model_StudentToParent DataRowToModel_AddBaby(DataRow row)
        {
            Model_StudentToParent parent = new Model_StudentToParent();
            if (row != null)
            {
                if (row["StudentToParent_ID"] != null)
                {
                    parent.StudentToParent_ID = row["StudentToParent_ID"].ToString();
                }
                if (row["Student_ID"] != null)
                {
                    parent.Student_ID = row["Student_ID"].ToString();
                }
                if (row["Parent_ID"] != null)
                {
                    parent.Parent_ID = row["Parent_ID"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    parent.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["UserName"] != null) && (row["UserName"].ToString() != ""))
                {
                    parent.UserName = row["UserName"].ToString();
                }
                if ((row["Email"] != null) && (row["Email"].ToString() != ""))
                {
                    parent.Email = row["Email"].ToString();
                }
            }
            return parent;
        }

        public bool Delete(string StudentToParent_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StudentToParent ");
            builder.Append(" where StudentToParent_ID=@StudentToParent_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StudentToParent_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StudentToParent_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string StudentToParent_IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StudentToParent ");
            builder.Append(" where StudentToParent_ID in (" + StudentToParent_IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string StudentToParent_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StudentToParent");
            builder.Append(" where StudentToParent_ID=@StudentToParent_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StudentToParent_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StudentToParent_ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public GH_PagerInfo<List<Model_StudentToParent>> GetAllList(string Where, string Sort, int pageIndex, int pageSize)
        {
            new StringBuilder();
            string strWhere = " and 1=1 " + Where;
            int recordCount = this.GetRecordCount(strWhere);
            int startIndex = ((pageIndex - 1) * pageSize) + 1;
            int endIndex = pageIndex * pageSize;
            DataTable table = this.GetListByPages(strWhere, Sort, startIndex, endIndex).Tables[0];
            List<Model_StudentToParent> list = new List<Model_StudentToParent>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(this.DataRowToModel_AddBaby(row));
            }
            return new GH_PagerInfo<List<Model_StudentToParent>> { PageSize = pageSize, CurrentPage = pageIndex, RecordCount = recordCount, PageData = list };
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select StudentToParent_ID,Student_ID,Parent_ID,CreateTime ");
            builder.Append(" FROM StudentToParent ");
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
            builder.Append(" StudentToParent_ID,Student_ID,Parent_ID,CreateTime ");
            builder.Append(" FROM StudentToParent ");
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
                builder.Append("order by T.StudentToParent_ID desc");
            }
            builder.Append(")AS Row, U.UserName,U.Email  from StudentToParent T, dbo.F_User U ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE T.Student_ID=U.UserId " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPages(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.StudentToParent_ID desc");
            }
            builder.Append(")AS Row,T.*,U.UserName,U.Email  from StudentToParent T, dbo.F_User U ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE  T.Student_ID=U.UserId " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StudentToParent GetModel(string StudentToParent_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 StudentToParent_ID,Student_ID,Parent_ID,CreateTime from StudentToParent ");
            builder.Append(" where StudentToParent_ID=@StudentToParent_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StudentToParent_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = StudentToParent_ID;
            new Model_StudentToParent();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetParentToStuList_APP(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select t.*,u.UserName,u.TrueName ");
            builder.Append(" FROM StudentToParent t INNER JOIN F_User u ON t.Student_ID = u.UserId");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM StudentToParent ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_StudentToParent model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StudentToParent set ");
            builder.Append("Student_ID=@Student_ID,");
            builder.Append("Parent_ID=@Parent_ID,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where StudentToParent_ID=@StudentToParent_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Student_ID", SqlDbType.Char, 0x24), new SqlParameter("@Parent_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@StudentToParent_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Student_ID;
            cmdParms[1].Value = model.Parent_ID;
            cmdParms[2].Value = model.CreateTime;
            cmdParms[3].Value = model.StudentToParent_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

