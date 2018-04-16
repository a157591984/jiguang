namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_S_KnowledgePointAttrExtend
    {
        public bool Add(Model_S_KnowledgePointAttrExtend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into S_KnowledgePointAttrExtend(");
            builder.Append("S_KnowledgePointAttrExtend_Id,S_KnowledgePointBasic_Id,S_KnowledgePointAttrEnum,S_KnowledgePointAttrName,S_KnowledgePointAttrValue,MaxValue,MinValue,CreateTime)");
            builder.Append(" values (");
            builder.Append("@S_KnowledgePointAttrExtend_Id,@S_KnowledgePointBasic_Id,@S_KnowledgePointAttrEnum,@S_KnowledgePointAttrName,@S_KnowledgePointAttrValue,@MaxValue,@MinValue,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointAttrExtend_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointAttrEnum", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointAttrName", SqlDbType.NVarChar, 100), new SqlParameter("@S_KnowledgePointAttrValue", SqlDbType.Decimal, 9), new SqlParameter("@MaxValue", SqlDbType.Decimal, 9), new SqlParameter("@MinValue", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.S_KnowledgePointAttrExtend_Id;
            cmdParms[1].Value = model.S_KnowledgePointBasic_Id;
            cmdParms[2].Value = model.S_KnowledgePointAttrEnum;
            cmdParms[3].Value = model.S_KnowledgePointAttrName;
            cmdParms[4].Value = model.S_KnowledgePointAttrValue;
            cmdParms[5].Value = model.MaxValue;
            cmdParms[6].Value = model.MinValue;
            cmdParms[7].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_S_KnowledgePointAttrExtend DataRowToModel(DataRow row)
        {
            Model_S_KnowledgePointAttrExtend extend = new Model_S_KnowledgePointAttrExtend();
            if (row != null)
            {
                if (row["S_KnowledgePointAttrExtend_Id"] != null)
                {
                    extend.S_KnowledgePointAttrExtend_Id = row["S_KnowledgePointAttrExtend_Id"].ToString();
                }
                if (row["S_KnowledgePointBasic_Id"] != null)
                {
                    extend.S_KnowledgePointBasic_Id = row["S_KnowledgePointBasic_Id"].ToString();
                }
                if (row["S_KnowledgePointAttrEnum"] != null)
                {
                    extend.S_KnowledgePointAttrEnum = row["S_KnowledgePointAttrEnum"].ToString();
                }
                if (row["S_KnowledgePointAttrName"] != null)
                {
                    extend.S_KnowledgePointAttrName = row["S_KnowledgePointAttrName"].ToString();
                }
                if ((row["S_KnowledgePointAttrValue"] != null) && (row["S_KnowledgePointAttrValue"].ToString() != ""))
                {
                    extend.S_KnowledgePointAttrValue = new decimal?(decimal.Parse(row["S_KnowledgePointAttrValue"].ToString()));
                }
                if ((row["MaxValue"] != null) && (row["MaxValue"].ToString() != ""))
                {
                    extend.MaxValue = new decimal?(decimal.Parse(row["MaxValue"].ToString()));
                }
                if ((row["MinValue"] != null) && (row["MinValue"].ToString() != ""))
                {
                    extend.MinValue = new decimal?(decimal.Parse(row["MinValue"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    extend.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return extend;
        }

        public bool Delete(string S_KnowledgePointAttrExtend_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_KnowledgePointAttrExtend ");
            builder.Append(" where S_KnowledgePointAttrExtend_Id=@S_KnowledgePointAttrExtend_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointAttrExtend_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePointAttrExtend_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string S_KnowledgePointAttrExtend_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from S_KnowledgePointAttrExtend ");
            builder.Append(" where S_KnowledgePointAttrExtend_Id in (" + S_KnowledgePointAttrExtend_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string S_KnowledgePointAttrExtend_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from S_KnowledgePointAttrExtend");
            builder.Append(" where S_KnowledgePointAttrExtend_Id=@S_KnowledgePointAttrExtend_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointAttrExtend_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePointAttrExtend_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select S_KnowledgePointAttrExtend_Id,S_KnowledgePointBasic_Id,S_KnowledgePointAttrEnum,S_KnowledgePointAttrName,S_KnowledgePointAttrValue,MaxValue,MinValue,CreateTime ");
            builder.Append(" FROM S_KnowledgePointAttrExtend ");
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
            builder.Append(" S_KnowledgePointAttrExtend_Id,S_KnowledgePointBasic_Id,S_KnowledgePointAttrEnum,S_KnowledgePointAttrName,S_KnowledgePointAttrValue,MaxValue,MinValue,CreateTime ");
            builder.Append(" FROM S_KnowledgePointAttrExtend ");
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
                builder.Append("order by T.S_KnowledgePointAttrExtend_Id desc");
            }
            builder.Append(")AS Row, T.*  from S_KnowledgePointAttrExtend T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_S_KnowledgePointAttrExtend GetModel(string S_KnowledgePointAttrExtend_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 S_KnowledgePointAttrExtend_Id,S_KnowledgePointBasic_Id,S_KnowledgePointAttrEnum,S_KnowledgePointAttrName,S_KnowledgePointAttrValue,MaxValue,MinValue,CreateTime from S_KnowledgePointAttrExtend ");
            builder.Append(" where S_KnowledgePointAttrExtend_Id=@S_KnowledgePointAttrExtend_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointAttrExtend_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = S_KnowledgePointAttrExtend_Id;
            new Model_S_KnowledgePointAttrExtend();
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
            builder.Append("select count(1) FROM S_KnowledgePointAttrExtend ");
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

        public bool Update(Model_S_KnowledgePointAttrExtend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update S_KnowledgePointAttrExtend set ");
            builder.Append("S_KnowledgePointBasic_Id=@S_KnowledgePointBasic_Id,");
            builder.Append("S_KnowledgePointAttrEnum=@S_KnowledgePointAttrEnum,");
            builder.Append("S_KnowledgePointAttrName=@S_KnowledgePointAttrName,");
            builder.Append("S_KnowledgePointAttrValue=@S_KnowledgePointAttrValue,");
            builder.Append("MaxValue=@MaxValue,");
            builder.Append("MinValue=@MinValue,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where S_KnowledgePointAttrExtend_Id=@S_KnowledgePointAttrExtend_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@S_KnowledgePointBasic_Id", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointAttrEnum", SqlDbType.Char, 0x24), new SqlParameter("@S_KnowledgePointAttrName", SqlDbType.NVarChar, 100), new SqlParameter("@S_KnowledgePointAttrValue", SqlDbType.Decimal, 9), new SqlParameter("@MaxValue", SqlDbType.Decimal, 9), new SqlParameter("@MinValue", SqlDbType.Decimal, 9), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@S_KnowledgePointAttrExtend_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.S_KnowledgePointBasic_Id;
            cmdParms[1].Value = model.S_KnowledgePointAttrEnum;
            cmdParms[2].Value = model.S_KnowledgePointAttrName;
            cmdParms[3].Value = model.S_KnowledgePointAttrValue;
            cmdParms[4].Value = model.MaxValue;
            cmdParms[5].Value = model.MinValue;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.S_KnowledgePointAttrExtend_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

