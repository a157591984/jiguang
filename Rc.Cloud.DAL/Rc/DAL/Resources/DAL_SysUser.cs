namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysUser
    {
        public bool Add(Model_SysUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysUser(");
            builder.Append("SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser)");
            builder.Append(" values (");
            builder.Append("@SysUser_ID,@SysUser_Name,@SysUser_LoginName,@SysUser_PassWord,@SysUser_Tel,@SysDepartment_ID,@SysUser_Enable,@SysUser_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_Name", SqlDbType.NVarChar, 0x80), new SqlParameter("@SysUser_LoginName", SqlDbType.NVarChar, 20), new SqlParameter("@SysUser_PassWord", SqlDbType.NVarChar, 200), new SqlParameter("@SysUser_Tel", SqlDbType.NVarChar, 20), new SqlParameter("@SysDepartment_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysUser_Remark", SqlDbType.NVarChar, 0x800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysUser_ID;
            cmdParms[1].Value = model.SysUser_Name;
            cmdParms[2].Value = model.SysUser_LoginName;
            cmdParms[3].Value = model.SysUser_PassWord;
            cmdParms[4].Value = model.SysUser_Tel;
            cmdParms[5].Value = model.SysDepartment_ID;
            cmdParms[6].Value = model.SysUser_Enable;
            cmdParms[7].Value = model.SysUser_Remark;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.CreateUser;
            cmdParms[10].Value = model.UpdateTime;
            cmdParms[11].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysUser DataRowToModel(DataRow row)
        {
            Model_SysUser user = new Model_SysUser();
            if (row != null)
            {
                if (row["SysUser_ID"] != null)
                {
                    user.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["SysUser_Name"] != null)
                {
                    user.SysUser_Name = row["SysUser_Name"].ToString();
                }
                if (row["SysUser_LoginName"] != null)
                {
                    user.SysUser_LoginName = row["SysUser_LoginName"].ToString();
                }
                if (row["SysUser_PassWord"] != null)
                {
                    user.SysUser_PassWord = row["SysUser_PassWord"].ToString();
                }
                if (row["SysUser_Tel"] != null)
                {
                    user.SysUser_Tel = row["SysUser_Tel"].ToString();
                }
                if (row["SysDepartment_ID"] != null)
                {
                    user.SysDepartment_ID = row["SysDepartment_ID"].ToString();
                }
                if ((row["SysUser_Enable"] != null) && (row["SysUser_Enable"].ToString() != ""))
                {
                    if ((row["SysUser_Enable"].ToString() == "1") || (row["SysUser_Enable"].ToString().ToLower() == "true"))
                    {
                        user.SysUser_Enable = true;
                    }
                    else
                    {
                        user.SysUser_Enable = false;
                    }
                }
                if (row["SysUser_Remark"] != null)
                {
                    user.SysUser_Remark = row["SysUser_Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    user.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    user.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["UpdateTime"] != null) && (row["UpdateTime"].ToString() != ""))
                {
                    user.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                }
                if (row["UpdateUser"] != null)
                {
                    user.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return user;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUser ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysUser ");
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
            builder.Append(" SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" FROM SysUser ");
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
                builder.Append("order by T.SysUser_ID desc");
            }
            builder.Append(")AS Row, T.*  from SysUser T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysUser GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysUser_ID,SysUser_Name,SysUser_LoginName,SysUser_PassWord,SysUser_Tel,SysDepartment_ID,SysUser_Enable,SysUser_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser from SysUser ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Model_SysUser();
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
            builder.Append("select count(1) FROM SysUser ");
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

        public bool Update(Model_SysUser model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysUser set ");
            builder.Append("SysUser_ID=@SysUser_ID,");
            builder.Append("SysUser_Name=@SysUser_Name,");
            builder.Append("SysUser_LoginName=@SysUser_LoginName,");
            builder.Append("SysUser_PassWord=@SysUser_PassWord,");
            builder.Append("SysUser_Tel=@SysUser_Tel,");
            builder.Append("SysDepartment_ID=@SysDepartment_ID,");
            builder.Append("SysUser_Enable=@SysUser_Enable,");
            builder.Append("SysUser_Remark=@SysUser_Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("UpdateUser=@UpdateUser");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_Name", SqlDbType.NVarChar, 0x80), new SqlParameter("@SysUser_LoginName", SqlDbType.NVarChar, 20), new SqlParameter("@SysUser_PassWord", SqlDbType.NVarChar, 200), new SqlParameter("@SysUser_Tel", SqlDbType.NVarChar, 20), new SqlParameter("@SysDepartment_ID", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_Enable", SqlDbType.Bit, 1), new SqlParameter("@SysUser_Remark", SqlDbType.NVarChar, 0x800), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@UpdateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysUser_ID;
            cmdParms[1].Value = model.SysUser_Name;
            cmdParms[2].Value = model.SysUser_LoginName;
            cmdParms[3].Value = model.SysUser_PassWord;
            cmdParms[4].Value = model.SysUser_Tel;
            cmdParms[5].Value = model.SysDepartment_ID;
            cmdParms[6].Value = model.SysUser_Enable;
            cmdParms[7].Value = model.SysUser_Remark;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.CreateUser;
            cmdParms[10].Value = model.UpdateTime;
            cmdParms[11].Value = model.UpdateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

