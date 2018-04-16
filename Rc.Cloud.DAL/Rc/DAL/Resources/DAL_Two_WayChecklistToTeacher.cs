namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Two_WayChecklistToTeacher
    {
        public bool Add(Model_Two_WayChecklistToTeacher model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklistToTeacher(");
            builder.Append("Two_WayChecklistToTeacher_Id,Two_WayChecklist_Id,Two_NewWayChecklist_Id,Teacher_Id,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklistToTeacher_Id,@Two_WayChecklist_Id,@Two_NewWayChecklist_Id,@Teacher_Id,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTeacher_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_NewWayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Teacher_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Two_WayChecklistToTeacher_Id;
            cmdParms[1].Value = model.Two_WayChecklist_Id;
            cmdParms[2].Value = model.Two_NewWayChecklist_Id;
            cmdParms[3].Value = model.Teacher_Id;
            cmdParms[4].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Two_WayChecklistToTeacher DataRowToModel(DataRow row)
        {
            Model_Two_WayChecklistToTeacher teacher = new Model_Two_WayChecklistToTeacher();
            if (row != null)
            {
                if (row["Two_WayChecklistToTeacher_Id"] != null)
                {
                    teacher.Two_WayChecklistToTeacher_Id = row["Two_WayChecklistToTeacher_Id"].ToString();
                }
                if (row["Two_WayChecklist_Id"] != null)
                {
                    teacher.Two_WayChecklist_Id = row["Two_WayChecklist_Id"].ToString();
                }
                if (row["Two_NewWayChecklist_Id"] != null)
                {
                    teacher.Two_NewWayChecklist_Id = row["Two_NewWayChecklist_Id"].ToString();
                }
                if (row["Teacher_Id"] != null)
                {
                    teacher.Teacher_Id = row["Teacher_Id"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    teacher.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return teacher;
        }

        public bool Delete(string Two_WayChecklistToTeacher_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistToTeacher ");
            builder.Append(" where Two_WayChecklistToTeacher_Id=@Two_WayChecklistToTeacher_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTeacher_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistToTeacher_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Two_WayChecklistToTeacher_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistToTeacher ");
            builder.Append(" where Two_WayChecklistToTeacher_Id in (" + Two_WayChecklistToTeacher_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Two_WayChecklistToTeacher_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Two_WayChecklistToTeacher");
            builder.Append(" where Two_WayChecklistToTeacher_Id=@Two_WayChecklistToTeacher_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTeacher_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistToTeacher_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Two_WayChecklistToTeacher_Id,Two_WayChecklist_Id,Two_NewWayChecklist_Id,Teacher_Id,CreateTime ");
            builder.Append(" FROM Two_WayChecklistToTeacher ");
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
            builder.Append(" Two_WayChecklistToTeacher_Id,Two_WayChecklist_Id,Two_NewWayChecklist_Id,Teacher_Id,CreateTime ");
            builder.Append(" FROM Two_WayChecklistToTeacher ");
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
                builder.Append("order by T.Two_WayChecklistToTeacher_Id desc");
            }
            builder.Append(")AS Row, T.*  from Two_WayChecklistToTeacher T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Two_WayChecklistToTeacher GetModel(string Two_WayChecklistToTeacher_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Two_WayChecklistToTeacher_Id,Two_WayChecklist_Id,Two_NewWayChecklist_Id,Teacher_Id,CreateTime from Two_WayChecklistToTeacher ");
            builder.Append(" where Two_WayChecklistToTeacher_Id=@Two_WayChecklistToTeacher_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTeacher_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistToTeacher_Id;
            new Model_Two_WayChecklistToTeacher();
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
            builder.Append("select count(1) FROM Two_WayChecklistToTeacher ");
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

        public bool Update(Model_Two_WayChecklistToTeacher model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Two_WayChecklistToTeacher set ");
            builder.Append("Two_WayChecklist_Id=@Two_WayChecklist_Id,");
            builder.Append("Two_NewWayChecklist_Id=@Two_NewWayChecklist_Id,");
            builder.Append("Teacher_Id=@Teacher_Id,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Two_WayChecklistToTeacher_Id=@Two_WayChecklistToTeacher_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_NewWayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Teacher_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Two_WayChecklistToTeacher_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Two_WayChecklist_Id;
            cmdParms[1].Value = model.Two_NewWayChecklist_Id;
            cmdParms[2].Value = model.Teacher_Id;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.Two_WayChecklistToTeacher_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

