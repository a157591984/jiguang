namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_visit_client_s
    {
        public bool Add(Model_visit_client_s model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into visit_client_s(");
            builder.Append("visit_client_id,user_id,resource_data_id,product_type,tab_id,open_time,close_time,operate_type)");
            builder.Append(" values (");
            builder.Append("@visit_client_id,@user_id,@resource_data_id,@product_type,@tab_id,@open_time,@close_time,@operate_type)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_client_id", SqlDbType.Char, 0x24), new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@resource_data_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50), new SqlParameter("@tab_id", SqlDbType.VarChar, 100), new SqlParameter("@open_time", SqlDbType.DateTime), new SqlParameter("@close_time", SqlDbType.DateTime), new SqlParameter("@operate_type", SqlDbType.VarChar, 10) };
            cmdParms[0].Value = model.visit_client_id;
            cmdParms[1].Value = model.user_id;
            cmdParms[2].Value = model.resource_data_id;
            cmdParms[3].Value = model.product_type;
            cmdParms[4].Value = model.tab_id;
            cmdParms[5].Value = model.open_time;
            cmdParms[6].Value = model.close_time;
            cmdParms[7].Value = model.operate_type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_visit_client_s DataRowToModel(DataRow row)
        {
            Model_visit_client_s _s = new Model_visit_client_s();
            if (row != null)
            {
                if (row["visit_client_id"] != null)
                {
                    _s.visit_client_id = row["visit_client_id"].ToString();
                }
                if (row["user_id"] != null)
                {
                    _s.user_id = row["user_id"].ToString();
                }
                if (row["resource_data_id"] != null)
                {
                    _s.resource_data_id = row["resource_data_id"].ToString();
                }
                if (row["product_type"] != null)
                {
                    _s.product_type = row["product_type"].ToString();
                }
                if (row["tab_id"] != null)
                {
                    _s.tab_id = row["tab_id"].ToString();
                }
                if ((row["open_time"] != null) && (row["open_time"].ToString() != ""))
                {
                    _s.open_time = new DateTime?(DateTime.Parse(row["open_time"].ToString()));
                }
                if ((row["close_time"] != null) && (row["close_time"].ToString() != ""))
                {
                    _s.close_time = new DateTime?(DateTime.Parse(row["close_time"].ToString()));
                }
                if (row["operate_type"] != null)
                {
                    _s.operate_type = row["operate_type"].ToString();
                }
            }
            return _s;
        }

        public bool Delete(string visit_client_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from visit_client_s ");
            builder.Append(" where visit_client_id=@visit_client_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_client_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = visit_client_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string visit_client_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from visit_client_s ");
            builder.Append(" where visit_client_id in (" + visit_client_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string visit_client_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from visit_client_s");
            builder.Append(" where visit_client_id=@visit_client_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_client_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = visit_client_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select visit_client_id,user_id,resource_data_id,product_type,tab_id,open_time,close_time,operate_type ");
            builder.Append(" FROM visit_client_s ");
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
            builder.Append(" visit_client_id,user_id,resource_data_id,product_type,tab_id,open_time,close_time,operate_type ");
            builder.Append(" FROM visit_client_s ");
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
                builder.Append("order by T.visit_client_id desc");
            }
            builder.Append(")AS Row, T.*  from visit_client_s T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageDetail(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.visit_client_id desc");
            }
            builder.Append(")AS Row, t.*,t2.Resource_Name,t2.Resource_Class\r\n,(case when t3.TrueName='' then t3.UserName when t3.TrueName is null then t3.UserName else t3.UserName end) as TeacherName  \r\nfrom visit_client_s t\r\ninner join ResourceToResourceFolder t2 on t2.ResourceToResourceFolder_Id=t.resource_data_id\r\nleft join F_User t3 on t3.UserId=t.user_id");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by DateData desc");
            }
            builder.Append(")AS Row,v.DateType,v.DateData,v.TeacherId,v.TeacherName,u.TrueName,v.VisitCount_All,v.VisitCount_Cloud,v.VisitCount_Own,\r\nv.VisitFile_All,v.VisitFile_Cloud,v.VisitFile_Own,v.CreateOwnCount_All,v.CreateOwnCount_Plan,v.CreateOwnCount_TestPaper\r\nfrom  StatsVisitClient v \r\n inner join f_user u on u.userid=v.TeacherId ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(") TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_visit_client_s GetModel(string visit_client_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 visit_client_id,user_id,resource_data_id,product_type,tab_id,open_time,close_time,operate_type from visit_client_s ");
            builder.Append(" where visit_client_id=@visit_client_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@visit_client_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = visit_client_id;
            new Model_visit_client_s();
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
            builder.Append("select count(1) FROM visit_client_s ");
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

        public int GetRecordCountDetail(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from visit_client_s t\r\ninner join ResourceToResourceFolder t2 on t2.ResourceToResourceFolder_Id=t.resource_data_id\r\nleft join F_User t3 on t3.UserId=t.user_id ");
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

        public int GetRecordCountNew(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from  StatsVisitClient ");
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

        public bool Update(Model_visit_client_s model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update visit_client_s set ");
            builder.Append("user_id=@user_id,");
            builder.Append("resource_data_id=@resource_data_id,");
            builder.Append("product_type=@product_type,");
            builder.Append("tab_id=@tab_id,");
            builder.Append("open_time=@open_time,");
            builder.Append("close_time=@close_time,");
            builder.Append("operate_type=@operate_type");
            builder.Append(" where visit_client_id=@visit_client_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@user_id", SqlDbType.Char, 0x24), new SqlParameter("@resource_data_id", SqlDbType.Char, 0x24), new SqlParameter("@product_type", SqlDbType.VarChar, 50), new SqlParameter("@tab_id", SqlDbType.VarChar, 100), new SqlParameter("@open_time", SqlDbType.DateTime), new SqlParameter("@close_time", SqlDbType.DateTime), new SqlParameter("@operate_type", SqlDbType.VarChar, 10), new SqlParameter("@visit_client_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.user_id;
            cmdParms[1].Value = model.resource_data_id;
            cmdParms[2].Value = model.product_type;
            cmdParms[3].Value = model.tab_id;
            cmdParms[4].Value = model.open_time;
            cmdParms[5].Value = model.close_time;
            cmdParms[6].Value = model.operate_type;
            cmdParms[7].Value = model.visit_client_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

