namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Regional_Dict
    {
        public bool Add(Model_Regional_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Regional_Dict(");
            builder.Append("Regional_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime,D_Type,D_CreateUser,D_Remark,D_UpdateUser,D_UpdateTime)");
            builder.Append(" values (");
            builder.Append("@Regional_Dict_ID,@D_Name,@D_PartentID,@D_Value,@D_Code,@D_Level,@D_Order,@D_CreateTime,@D_Type,@D_CreateUser,@D_Remark,@D_UpdateUser,@D_UpdateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Regional_Dict_ID", SqlDbType.Char, 0x24), new SqlParameter("@D_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_PartentID", SqlDbType.Char, 0x24), new SqlParameter("@D_Value", SqlDbType.Int, 4), new SqlParameter("@D_Code", SqlDbType.VarChar, 50), new SqlParameter("@D_Level", SqlDbType.Int, 4), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@D_Type", SqlDbType.Int, 4), new SqlParameter("@D_CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@D_UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_UpdateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Regional_Dict_ID;
            cmdParms[1].Value = model.D_Name;
            cmdParms[2].Value = model.D_PartentID;
            cmdParms[3].Value = model.D_Value;
            cmdParms[4].Value = model.D_Code;
            cmdParms[5].Value = model.D_Level;
            cmdParms[6].Value = model.D_Order;
            cmdParms[7].Value = model.D_CreateTime;
            cmdParms[8].Value = model.D_Type;
            cmdParms[9].Value = model.D_CreateUser;
            cmdParms[10].Value = model.D_Remark;
            cmdParms[11].Value = model.D_UpdateUser;
            cmdParms[12].Value = model.D_UpdateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Regional_Dict DataRowToModel(DataRow row)
        {
            Model_Regional_Dict dict = new Model_Regional_Dict();
            if (row != null)
            {
                if (row["Regional_Dict_ID"] != null)
                {
                    dict.Regional_Dict_ID = row["Regional_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    dict.D_Name = row["D_Name"].ToString();
                }
                if (row["D_PartentID"] != null)
                {
                    dict.D_PartentID = row["D_PartentID"].ToString();
                }
                if ((row["D_Value"] != null) && (row["D_Value"].ToString() != ""))
                {
                    dict.D_Value = new int?(int.Parse(row["D_Value"].ToString()));
                }
                if (row["D_Code"] != null)
                {
                    dict.D_Code = row["D_Code"].ToString();
                }
                if ((row["D_Level"] != null) && (row["D_Level"].ToString() != ""))
                {
                    dict.D_Level = new int?(int.Parse(row["D_Level"].ToString()));
                }
                if ((row["D_Order"] != null) && (row["D_Order"].ToString() != ""))
                {
                    dict.D_Order = new int?(int.Parse(row["D_Order"].ToString()));
                }
                if ((row["D_CreateTime"] != null) && (row["D_CreateTime"].ToString() != ""))
                {
                    dict.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                }
                if ((row["D_Type"] != null) && (row["D_Type"].ToString() != ""))
                {
                    dict.D_Type = new int?(int.Parse(row["D_Type"].ToString()));
                }
                if (row["D_CreateUser"] != null)
                {
                    dict.D_CreateUser = row["D_CreateUser"].ToString();
                }
                if (row["D_Remark"] != null)
                {
                    dict.D_Remark = row["D_Remark"].ToString();
                }
                if (row["D_UpdateUser"] != null)
                {
                    dict.D_UpdateUser = row["D_UpdateUser"].ToString();
                }
                if ((row["D_UpdateTime"] != null) && (row["D_UpdateTime"].ToString() != ""))
                {
                    dict.D_UpdateTime = new DateTime?(DateTime.Parse(row["D_UpdateTime"].ToString()));
                }
            }
            return dict;
        }

        public bool Delete(string Regional_Dict_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Regional_Dict ");
            builder.Append(" where Regional_Dict_ID=@Regional_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Regional_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Regional_Dict_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Regional_Dict_IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Regional_Dict ");
            builder.Append(" where Regional_Dict_ID in (" + Regional_Dict_IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Regional_Dict_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Regional_Dict");
            builder.Append(" where Regional_Dict_ID=@Regional_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Regional_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Regional_Dict_ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Regional_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime,D_Type,D_CreateUser,D_Remark,D_UpdateUser,D_UpdateTime ");
            builder.Append(" FROM Regional_Dict ");
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
            builder.Append(" Regional_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime,D_Type,D_CreateUser,D_Remark,D_UpdateUser,D_UpdateTime ");
            builder.Append(" FROM Regional_Dict ");
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
                builder.Append("order by T.Regional_Dict_ID desc");
            }
            builder.Append(")AS Row, T.*  from Regional_Dict T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Regional_Dict GetModel(string Regional_Dict_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Regional_Dict_ID,D_Name,D_PartentID,D_Value,D_Code,D_Level,D_Order,D_CreateTime,D_Type,D_CreateUser,D_Remark,D_UpdateUser,D_UpdateTime from Regional_Dict ");
            builder.Append(" where Regional_Dict_ID=@Regional_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Regional_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Regional_Dict_ID;
            new Model_Regional_Dict();
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
            builder.Append("select count(1) FROM Regional_Dict ");
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

        public bool Update(Model_Regional_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Regional_Dict set ");
            builder.Append("D_Name=@D_Name,");
            builder.Append("D_PartentID=@D_PartentID,");
            builder.Append("D_Value=@D_Value,");
            builder.Append("D_Code=@D_Code,");
            builder.Append("D_Level=@D_Level,");
            builder.Append("D_Order=@D_Order,");
            builder.Append("D_CreateTime=@D_CreateTime,");
            builder.Append("D_Type=@D_Type,");
            builder.Append("D_CreateUser=@D_CreateUser,");
            builder.Append("D_Remark=@D_Remark,");
            builder.Append("D_UpdateUser=@D_UpdateUser,");
            builder.Append("D_UpdateTime=@D_UpdateTime");
            builder.Append(" where Regional_Dict_ID=@Regional_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@D_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_PartentID", SqlDbType.Char, 0x24), new SqlParameter("@D_Value", SqlDbType.Int, 4), new SqlParameter("@D_Code", SqlDbType.VarChar, 50), new SqlParameter("@D_Level", SqlDbType.Int, 4), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@D_Type", SqlDbType.Int, 4), new SqlParameter("@D_CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@D_UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_UpdateTime", SqlDbType.DateTime), new SqlParameter("@Regional_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.D_Name;
            cmdParms[1].Value = model.D_PartentID;
            cmdParms[2].Value = model.D_Value;
            cmdParms[3].Value = model.D_Code;
            cmdParms[4].Value = model.D_Level;
            cmdParms[5].Value = model.D_Order;
            cmdParms[6].Value = model.D_CreateTime;
            cmdParms[7].Value = model.D_Type;
            cmdParms[8].Value = model.D_CreateUser;
            cmdParms[9].Value = model.D_Remark;
            cmdParms[10].Value = model.D_UpdateUser;
            cmdParms[11].Value = model.D_UpdateTime;
            cmdParms[12].Value = model.Regional_Dict_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

