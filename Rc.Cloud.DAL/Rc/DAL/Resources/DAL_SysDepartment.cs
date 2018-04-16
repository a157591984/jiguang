namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysDepartment
    {
        public bool Add(Model_SysDepartment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysDepartment(");
            builder.Append("SysDepartment_ID,SysDepartment_Name,SysDepartment_ParentID,SysDepartment_Tel,SysUser_ID,SysDepartment_Enable,SysDepartment_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            builder.Append(" values (");
            builder.Append("@SysDepartment_ID,@SysDepartment_Name,@SysDepartment_ParentID,@SysDepartment_Tel,@SysUser_ID,@SysDepartment_Enable,@SysDepartment_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysDepartment_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysDepartment_Name", SqlDbType.NVarChar, 0x80), new SqlParameter("@SysDepartment_ParentID", SqlDbType.Char, 0x24), new SqlParameter("@SysDepartment_Tel", SqlDbType.NVarChar, 20), new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysDepartment_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysDepartment_Remark", SqlDbType.NVarChar, 0x800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysDepartment_ID;
            cmdParms[1].Value = model.SysDepartment_Name;
            cmdParms[2].Value = model.SysDepartment_ParentID;
            cmdParms[3].Value = model.SysDepartment_Tel;
            cmdParms[4].Value = model.SysUser_ID;
            cmdParms[5].Value = model.SysDepartment_Enable;
            cmdParms[6].Value = model.SysDepartment_Remark;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.UpdateTime;
            cmdParms[10].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysDepartment DataRowToModel(DataRow row)
        {
            Model_SysDepartment department = new Model_SysDepartment();
            if (row != null)
            {
                if (row["SysDepartment_ID"] != null)
                {
                    department.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if (row["SysDepartment_Name"] != null)
                {
                    department.SysDepartment_Name = row["SysDepartment_Name"].ToString();
                }
                if (row["SysDepartment_ParentID"] != null)
                {
                    department.SysDepartment_ParentID = row["SysDepartment_ParentID"].ToString();
                }
                if (row["SysDepartment_Tel"] != null)
                {
                    department.SysDepartment_Tel = row["SysDepartment_Tel"].ToString();
                }
                if (row["SysUser_ID"] != null)
                {
                    department.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if ((row["SysDepartment_Enable"] != null) && (row["SysDepartment_Enable"].ToString() != ""))
                {
                    if ((row["SysDepartment_Enable"].ToString() == "1") || (row["SysDepartment_Enable"].ToString().ToLower() == "true"))
                    {
                        department.SysDepartment_Enable = true;
                    }
                    else
                    {
                        department.SysDepartment_Enable = false;
                    }
                }
                if (row["SysDepartment_Remark"] != null)
                {
                    department.SysDepartment_Remark = row["SysDepartment_Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    department.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    department.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    department.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    department.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return department;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysDepartment ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysDepartment_ID,SysDepartment_Name,SysDepartment_ParentID,SysDepartment_Tel,SysUser_ID,SysDepartment_Enable,SysDepartment_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysDepartment ");
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
            builder.Append(" SysDepartment_ID,SysDepartment_Name,SysDepartment_ParentID,SysDepartment_Tel,SysUser_ID,SysDepartment_Enable,SysDepartment_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysDepartment ");
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
                builder.Append("order by T.SysDepartment_ID desc");
            }
            builder.Append(")AS Row, T.*  from SysDepartment T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysDepartment GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysDepartment_ID,SysDepartment_Name,SysDepartment_ParentID,SysDepartment_Tel,SysUser_ID,SysDepartment_Enable,SysDepartment_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser from SysDepartment ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysDepartment();
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
            builder.Append("select count(1) FROM SysDepartment ");
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

        public bool Update(Model_SysDepartment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysDepartment set ");
            builder.Append("SysDepartment_ID=@SysDepartment_ID,");
            builder.Append("SysDepartment_Name=@SysDepartment_Name,");
            builder.Append("SysDepartment_ParentID=@SysDepartment_ParentID,");
            builder.Append("SysDepartment_Tel=@SysDepartment_Tel,");
            builder.Append("SysUser_ID=@SysUser_ID,");
            builder.Append("SysDepartment_Enable=@SysDepartment_Enable,");
            builder.Append("SysDepartment_Remark=@SysDepartment_Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("UpdateUser=@UpdateUser");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysDepartment_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysDepartment_Name", SqlDbType.NVarChar, 0x80), new SqlParameter("@SysDepartment_ParentID", SqlDbType.Char, 0x24), new SqlParameter("@SysDepartment_Tel", SqlDbType.NVarChar, 20), new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysDepartment_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysDepartment_Remark", SqlDbType.NVarChar, 0x800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysDepartment_ID;
            cmdParms[1].Value = model.SysDepartment_Name;
            cmdParms[2].Value = model.SysDepartment_ParentID;
            cmdParms[3].Value = model.SysDepartment_Tel;
            cmdParms[4].Value = model.SysUser_ID;
            cmdParms[5].Value = model.SysDepartment_Enable;
            cmdParms[6].Value = model.SysDepartment_Remark;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.UpdateTime;
            cmdParms[10].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

