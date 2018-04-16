namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_CustomizeHtml
    {
        public bool Add(Model_CustomizeHtml model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CustomizeHtml(");
            builder.Append("CustomizeHtml_Id,HtmlType,HtmlContent,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@CustomizeHtml_Id,@HtmlType,@HtmlContent,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CustomizeHtml_Id", SqlDbType.Char, 0x24), new SqlParameter("@HtmlType", SqlDbType.VarChar, 50), new SqlParameter("@HtmlContent", SqlDbType.NVarChar, -1), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.CustomizeHtml_Id;
            cmdParms[1].Value = model.HtmlType;
            cmdParms[2].Value = model.HtmlContent;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_CustomizeHtml DataRowToModel(DataRow row)
        {
            Model_CustomizeHtml html = new Model_CustomizeHtml();
            if (row != null)
            {
                if (row["CustomizeHtml_Id"] != null)
                {
                    html.CustomizeHtml_Id = row["CustomizeHtml_Id"].ToString();
                }
                if (row["HtmlType"] != null)
                {
                    html.HtmlType = row["HtmlType"].ToString();
                }
                if (row["HtmlContent"] != null)
                {
                    html.HtmlContent = row["HtmlContent"].ToString();
                }
                if (row["Remark"] != null)
                {
                    html.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    html.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    html.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return html;
        }

        public bool Delete(string CustomizeHtml_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CustomizeHtml ");
            builder.Append(" where CustomizeHtml_Id=@CustomizeHtml_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CustomizeHtml_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = CustomizeHtml_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string CustomizeHtml_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CustomizeHtml ");
            builder.Append(" where CustomizeHtml_Id in (" + CustomizeHtml_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string CustomizeHtml_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CustomizeHtml");
            builder.Append(" where CustomizeHtml_Id=@CustomizeHtml_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CustomizeHtml_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = CustomizeHtml_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public string GetHtmlContentByHtmlType(string HtmlType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 HtmlContent from CustomizeHtml ");
            builder.Append(" where HtmlType=@HtmlType ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HtmlType", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = HtmlType;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return "";
            }
            return single.ToString();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select CustomizeHtml_Id,HtmlType,HtmlContent,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM CustomizeHtml ");
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
            builder.Append(" CustomizeHtml_Id,HtmlType,HtmlContent,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM CustomizeHtml ");
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
                builder.Append("order by T.CustomizeHtml_Id desc");
            }
            builder.Append(")AS Row, T.*  from CustomizeHtml T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_CustomizeHtml GetModel(string CustomizeHtml_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CustomizeHtml_Id,HtmlType,HtmlContent,Remark,CreateUser,CreateTime from CustomizeHtml ");
            builder.Append(" where CustomizeHtml_Id=@CustomizeHtml_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CustomizeHtml_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = CustomizeHtml_Id;
            new Model_CustomizeHtml();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_CustomizeHtml GetModelByHtmlType(string HtmlType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CustomizeHtml_Id,HtmlType,HtmlContent,Remark,CreateUser,CreateTime from CustomizeHtml ");
            builder.Append(" where HtmlType=@HtmlType ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HtmlType", SqlDbType.VarChar, 100) };
            cmdParms[0].Value = HtmlType;
            new Model_CustomizeHtml();
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
            builder.Append("select count(1) FROM CustomizeHtml ");
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

        public bool Update(Model_CustomizeHtml model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CustomizeHtml set ");
            builder.Append("HtmlType=@HtmlType,");
            builder.Append("HtmlContent=@HtmlContent,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where CustomizeHtml_Id=@CustomizeHtml_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HtmlType", SqlDbType.VarChar, 50), new SqlParameter("@HtmlContent", SqlDbType.NVarChar, -1), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CustomizeHtml_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HtmlType;
            cmdParms[1].Value = model.HtmlContent;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.CustomizeHtml_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

