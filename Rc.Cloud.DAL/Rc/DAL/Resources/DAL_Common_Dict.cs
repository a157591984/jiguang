namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Common_Dict
    {
        public bool Add(Model_Common_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Common_Dict(");
            builder.Append("Common_Dict_ID,D_Name,D_ParentID,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime)");
            builder.Append(" values (");
            builder.Append("@Common_Dict_ID,@D_Name,@D_ParentID,@D_Value,@D_Code,@D_Level,@D_Order,@D_Type,@D_Remark,@D_CreateUser,@D_CreateTime,@D_ModifyUser,@D_ModifyTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Common_Dict_ID", SqlDbType.Char, 0x24), new SqlParameter("@D_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_ParentID", SqlDbType.Char, 0x24), new SqlParameter("@D_Value", SqlDbType.Int, 4), new SqlParameter("@D_Code", SqlDbType.VarChar, 300), new SqlParameter("@D_Level", SqlDbType.Int, 4), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_Type", SqlDbType.Int, 4), new SqlParameter("@D_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@D_CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@D_ModifyUser", SqlDbType.Char, 0x24), new SqlParameter("@D_ModifyTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Common_Dict_ID;
            cmdParms[1].Value = model.D_Name;
            cmdParms[2].Value = model.D_ParentID;
            cmdParms[3].Value = model.D_Value;
            cmdParms[4].Value = model.D_Code;
            cmdParms[5].Value = model.D_Level;
            cmdParms[6].Value = model.D_Order;
            cmdParms[7].Value = model.D_Type;
            cmdParms[8].Value = model.D_Remark;
            cmdParms[9].Value = model.D_CreateUser;
            cmdParms[10].Value = model.D_CreateTime;
            cmdParms[11].Value = model.D_ModifyUser;
            cmdParms[12].Value = model.D_ModifyTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Common_Dict DataRowToModel(DataRow row)
        {
            Model_Common_Dict dict = new Model_Common_Dict();
            if (row != null)
            {
                if (row["Common_Dict_ID"] != null)
                {
                    dict.Common_Dict_ID = row["Common_Dict_ID"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    dict.D_Name = row["D_Name"].ToString();
                }
                if (row["D_ParentID"] != null)
                {
                    dict.D_ParentID = row["D_ParentID"].ToString();
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
                if ((row["D_Type"] != null) && (row["D_Type"].ToString() != ""))
                {
                    dict.D_Type = new int?(int.Parse(row["D_Type"].ToString()));
                }
                if (row["D_Remark"] != null)
                {
                    dict.D_Remark = row["D_Remark"].ToString();
                }
                if (row["D_CreateUser"] != null)
                {
                    dict.D_CreateUser = row["D_CreateUser"].ToString();
                }
                if ((row["D_CreateTime"] != null) && (row["D_CreateTime"].ToString() != ""))
                {
                    dict.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                }
                if (row["D_ModifyUser"] != null)
                {
                    dict.D_ModifyUser = row["D_ModifyUser"].ToString();
                }
                if ((row["D_ModifyTime"] != null) && (row["D_ModifyTime"].ToString() != ""))
                {
                    dict.D_ModifyTime = new DateTime?(DateTime.Parse(row["D_ModifyTime"].ToString()));
                }
            }
            return dict;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Common_Dict ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Common_Dict_ID,D_Name,D_ParentID,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime ");
            builder.Append(" FROM Common_Dict ");
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
            builder.Append(" Common_Dict_ID,D_Name,D_ParentID,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime ");
            builder.Append(" FROM Common_Dict ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByDType(string D_Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Common_Dict_ID,D_Name FROM Common_Dict ");
            builder.AppendFormat(" where D_Type='{0}' ", D_Type);
            builder.Append(" ORDER BY D_ORDER,D_NAME ");
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
                builder.Append("order by T. desc");
            }
            builder.Append(")AS Row, T.*  from Common_Dict T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Common_Dict GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Common_Dict_ID,D_Name,D_ParentID,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime from Common_Dict ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_Common_Dict();
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
            builder.Append("select count(1) FROM Common_Dict ");
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

        public bool Update(Model_Common_Dict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Common_Dict set ");
            builder.Append("Common_Dict_ID=@Common_Dict_ID,");
            builder.Append("D_Name=@D_Name,");
            builder.Append("D_ParentID=@D_ParentID,");
            builder.Append("D_Value=@D_Value,");
            builder.Append("D_Code=@D_Code,");
            builder.Append("D_Level=@D_Level,");
            builder.Append("D_Order=@D_Order,");
            builder.Append("D_Type=@D_Type,");
            builder.Append("D_Remark=@D_Remark,");
            builder.Append("D_CreateUser=@D_CreateUser,");
            builder.Append("D_CreateTime=@D_CreateTime,");
            builder.Append("D_ModifyUser=@D_ModifyUser,");
            builder.Append("D_ModifyTime=@D_ModifyTime");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Common_Dict_ID", SqlDbType.Char, 0x24), new SqlParameter("@D_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_ParentID", SqlDbType.Char, 0x24), new SqlParameter("@D_Value", SqlDbType.Int, 4), new SqlParameter("@D_Code", SqlDbType.VarChar, 300), new SqlParameter("@D_Level", SqlDbType.Int, 4), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_Type", SqlDbType.Int, 4), new SqlParameter("@D_Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@D_CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@D_ModifyUser", SqlDbType.Char, 0x24), new SqlParameter("@D_ModifyTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Common_Dict_ID;
            cmdParms[1].Value = model.D_Name;
            cmdParms[2].Value = model.D_ParentID;
            cmdParms[3].Value = model.D_Value;
            cmdParms[4].Value = model.D_Code;
            cmdParms[5].Value = model.D_Level;
            cmdParms[6].Value = model.D_Order;
            cmdParms[7].Value = model.D_Type;
            cmdParms[8].Value = model.D_Remark;
            cmdParms[9].Value = model.D_CreateUser;
            cmdParms[10].Value = model.D_CreateTime;
            cmdParms[11].Value = model.D_ModifyUser;
            cmdParms[12].Value = model.D_ModifyTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

