namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_BookAttrbute
    {
        public bool Add(Model_BookAttrbute model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into BookAttrbute(");
            builder.Append("BookAttrId,ResourceFolder_Id,AttrEnum,AttrValue,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@BookAttrId,@ResourceFolder_Id,@AttrEnum,@AttrValue,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttrId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@AttrEnum", SqlDbType.VarChar, 50), new SqlParameter("@AttrValue", SqlDbType.VarChar, 10), new SqlParameter("@CreateUser", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.BookAttrId;
            cmdParms[1].Value = model.ResourceFolder_Id;
            cmdParms[2].Value = model.AttrEnum;
            cmdParms[3].Value = model.AttrValue;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_BookAttrbute DataRowToModel(DataRow row)
        {
            Model_BookAttrbute attrbute = new Model_BookAttrbute();
            if (row != null)
            {
                if (row["BookAttrId"] != null)
                {
                    attrbute.BookAttrId = row["BookAttrId"].ToString();
                }
                if (row["ResourceFolder_Id"] != null)
                {
                    attrbute.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["AttrEnum"] != null)
                {
                    attrbute.AttrEnum = row["AttrEnum"].ToString();
                }
                if (row["AttrValue"] != null)
                {
                    attrbute.AttrValue = row["AttrValue"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    attrbute.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    attrbute.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return attrbute;
        }

        public bool Delete(string BookAttrId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookAttrbute ");
            builder.Append(" where BookAttrId=@BookAttrId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttrId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookAttrId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BookAttrIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookAttrbute ");
            builder.Append(" where BookAttrId in (" + BookAttrIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string BookAttrId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from BookAttrbute");
            builder.Append(" where BookAttrId=@BookAttrId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttrId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookAttrId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BookAttrId,ResourceFolder_Id,AttrEnum,AttrValue,CreateUser,CreateTime ");
            builder.Append(" FROM BookAttrbute ");
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
            builder.Append(" BookAttrId,ResourceFolder_Id,AttrEnum,AttrValue,CreateUser,CreateTime ");
            builder.Append(" FROM BookAttrbute ");
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
                builder.Append("order by T.BookAttrId desc");
            }
            builder.Append(")AS Row, T.*  from BookAttrbute T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_BookAttrbute GetModel(string BookAttrId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BookAttrId,ResourceFolder_Id,AttrEnum,AttrValue,CreateUser,CreateTime from BookAttrbute ");
            builder.Append(" where BookAttrId=@BookAttrId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttrId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookAttrId;
            new Model_BookAttrbute();
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
            builder.Append("select count(1) FROM BookAttrbute ");
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

        public bool Update(Model_BookAttrbute model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update BookAttrbute set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("AttrEnum=@AttrEnum,");
            builder.Append("AttrValue=@AttrValue,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where BookAttrId=@BookAttrId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@AttrEnum", SqlDbType.VarChar, 50), new SqlParameter("@AttrValue", SqlDbType.VarChar, 10), new SqlParameter("@CreateUser", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@BookAttrId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.AttrEnum;
            cmdParms[2].Value = model.AttrValue;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.BookAttrId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

