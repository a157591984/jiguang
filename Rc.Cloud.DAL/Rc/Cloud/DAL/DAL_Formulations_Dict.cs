namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_Formulations_Dict
    {
        public bool Add(Model_Formulations_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Formulations_Dict(");
            builder.Append("Formulations_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime)");
            builder.Append(" values (");
            builder.Append("@Formulations_Dict_ID,@D_Name,@D_PartentID,@D_Value,@D_Code,@D_Level,@D_Order,@D_CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Formulations_Dict_ID", SqlDbType.Char, 0x24), new SqlParameter("@D_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_PartentID", SqlDbType.Char, 0x24), new SqlParameter("@D_Value", SqlDbType.Int, 4), new SqlParameter("@D_Code", SqlDbType.VarChar, 50), new SqlParameter("@D_Level", SqlDbType.Int, 4), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Formulations_Dict_ID;
            cmdParms[1].Value = model.D_Name;
            cmdParms[2].Value = model.D_PartentID;
            cmdParms[3].Value = model.D_Value;
            cmdParms[4].Value = model.D_Code;
            cmdParms[5].Value = model.D_Level;
            cmdParms[6].Value = model.D_Order;
            cmdParms[7].Value = model.D_CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(string Formulations_Dict_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Formulations_Dict ");
            builder.Append(" where Formulations_Dict_ID=@Formulations_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Formulations_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Formulations_Dict_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Formulations_Dict_IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Formulations_Dict ");
            builder.Append(" where Formulations_Dict_ID in (" + Formulations_Dict_IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetFormulations_Dict_List(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Formulations_Dict ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by D_Name ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public List<Model_Formulations_Dict> GetFormulations_DictModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetFormulations_DictModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_Formulations_Dict> GetFormulations_DictModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" Formulations_Dict ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            if (!string.IsNullOrEmpty(orderColumn))
            {
                builder.Append(" ORDER BY ");
                builder.Append(orderColumn);
                if (!string.IsNullOrEmpty(orderType))
                {
                    builder.Append(" " + orderType);
                }
            }
            DataSet set = DbHelperSQL.Query(builder.ToString());
            List<Model_Formulations_Dict> list = new List<Model_Formulations_Dict>();
            Model_Formulations_Dict item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_Formulations_Dict();
                if (row["Formulations_Dict_ID"] != null)
                {
                    item.Formulations_Dict_ID = row["Formulations_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    item.D_Name = row["D_Name"].ToString();
                }
                if (row["D_PartentID"] != null)
                {
                    item.D_PartentID = row["D_PartentID"].ToString();
                }
                if (row["D_Value"] != null)
                {
                    if (string.IsNullOrEmpty(row["D_Value"].ToString()))
                    {
                        item.D_Value = null;
                    }
                    else
                    {
                        item.D_Value = new int?(int.Parse(row["D_Value"].ToString()));
                    }
                }
                if (row["D_Code"] != null)
                {
                    item.D_Code = row["D_Code"].ToString();
                }
                if (row["D_Level"] != null)
                {
                    if (string.IsNullOrEmpty(row["D_Level"].ToString()))
                    {
                        item.D_Level = null;
                    }
                    else
                    {
                        item.D_Level = new int?(int.Parse(row["D_Level"].ToString()));
                    }
                }
                if (row["D_Order"] != null)
                {
                    if (string.IsNullOrEmpty(row["D_Order"].ToString()))
                    {
                        item.D_Order = null;
                    }
                    else
                    {
                        item.D_Order = new int?(int.Parse(row["D_Order"].ToString()));
                    }
                }
                if (row["D_CreateTime"] != null)
                {
                    if (string.IsNullOrEmpty(row["D_CreateTime"].ToString()))
                    {
                        item.D_CreateTime = null;
                    }
                    else
                    {
                        item.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Formulations_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime ");
            builder.Append(" FROM Formulations_Dict ");
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
            builder.Append(" Formulations_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime ");
            builder.Append(" FROM Formulations_Dict ");
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
                builder.Append("order by T.Formulations_Dict_ID desc");
            }
            builder.Append(")AS Row, T.*  from Formulations_Dict T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPaged(string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string pSql = string.Empty;
            pSql = "select  row_number() over(order by  D_Name) AS r_n, * from Formulations_Dict where 1=1 and D_PartentID is null ";
            if (D_Name != "")
            {
                pSql = pSql + " AND D_Name like'" + D_Name + "%'";
            }
            return DbHelperSQL.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetListPaged(string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string pSql = string.Empty;
            pSql = "select  row_number() over(order by  D_Name) AS r_n, * from Formulations_Dict where 1=1 ";
            if (D_Name != "")
            {
                pSql = pSql + " AND D_Name='" + D_Name + "'";
            }
            return DbHelperSQL.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetListPaged(string Content, int PageIndex, int PageSize, out int rCount, out int pCount, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  row_number() over(order by  D_Name) AS r_n, * from Formulations_Dict where 1=1 ");
            if (Content != "")
            {
                builder.Append(Content);
            }
            return DbHelperSQL.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_Formulations_Dict GetModel(string Formulations_Dict_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Formulations_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime from Formulations_Dict ");
            builder.Append(" where Formulations_Dict_ID=@Formulations_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Formulations_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Formulations_Dict_ID;
            Model_Formulations_Dict dict = new Model_Formulations_Dict();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["Formulations_Dict_ID"] != null) && (set.Tables[0].Rows[0]["Formulations_Dict_ID"].ToString() != ""))
            {
                dict.Formulations_Dict_ID = set.Tables[0].Rows[0]["Formulations_Dict_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Name"] != null) && (set.Tables[0].Rows[0]["D_Name"].ToString() != ""))
            {
                dict.D_Name = set.Tables[0].Rows[0]["D_Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_PartentID"] != null) && (set.Tables[0].Rows[0]["D_PartentID"].ToString() != ""))
            {
                dict.D_PartentID = set.Tables[0].Rows[0]["D_PartentID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Value"] != null) && (set.Tables[0].Rows[0]["D_Value"].ToString() != ""))
            {
                dict.D_Value = new int?(int.Parse(set.Tables[0].Rows[0]["D_Value"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_Code"] != null) && (set.Tables[0].Rows[0]["D_Code"].ToString() != ""))
            {
                dict.D_Code = set.Tables[0].Rows[0]["D_Code"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Level"] != null) && (set.Tables[0].Rows[0]["D_Level"].ToString() != ""))
            {
                dict.D_Level = new int?(int.Parse(set.Tables[0].Rows[0]["D_Level"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_Order"] != null) && (set.Tables[0].Rows[0]["D_Order"].ToString() != ""))
            {
                dict.D_Order = new int?(int.Parse(set.Tables[0].Rows[0]["D_Order"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_CreateTime"] != null) && (set.Tables[0].Rows[0]["D_CreateTime"].ToString() != ""))
            {
                dict.D_CreateTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["D_CreateTime"].ToString()));
            }
            return dict;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Formulations_Dict ");
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

        public bool Update(Model_Formulations_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Formulations_Dict set ");
            builder.Append("D_Name=@D_Name,");
            builder.Append("D_PartentID=@D_PartentID,");
            builder.Append("D_Value=@D_Value,");
            builder.Append("D_Code=@D_Code,");
            builder.Append("D_Level=@D_Level,");
            builder.Append("D_Order=@D_Order,");
            builder.Append("D_CreateTime=@D_CreateTime");
            builder.Append(" where Formulations_Dict_ID=@Formulations_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@D_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_PartentID", SqlDbType.Char, 0x24), new SqlParameter("@D_Value", SqlDbType.Int, 4), new SqlParameter("@D_Code", SqlDbType.VarChar, 50), new SqlParameter("@D_Level", SqlDbType.Int, 4), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@Formulations_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.D_Name;
            cmdParms[1].Value = model.D_PartentID;
            cmdParms[2].Value = model.D_Value;
            cmdParms[3].Value = model.D_Code;
            cmdParms[4].Value = model.D_Level;
            cmdParms[5].Value = model.D_Order;
            cmdParms[6].Value = model.D_CreateTime;
            cmdParms[7].Value = model.Formulations_Dict_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int updateTran(List<string> sqlList)
        {
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }
    }
}

