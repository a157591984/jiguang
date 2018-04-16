namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SysUserTask
    {
        public bool Add(Model_SysUserTask model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SysUserTask(");
            builder.Append("SysUserTask_id,SysUser_ID,TaskType,TaskID,TaskName,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@SysUserTask_id,@SysUser_ID,@TaskType,@TaskID,@TaskName,@CreateTime,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUserTask_id", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@TaskType", SqlDbType.VarChar, 20), new SqlParameter("@TaskID", SqlDbType.VarChar, 0x24), new SqlParameter("@TaskName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime, 3), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysUserTask_id;
            cmdParms[1].Value = model.SysUser_ID;
            cmdParms[2].Value = model.TaskType;
            cmdParms[3].Value = model.TaskID;
            cmdParms[4].Value = model.TaskName;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SysUserTask DataRowToModel(DataRow row)
        {
            Model_SysUserTask task = new Model_SysUserTask();
            if (row != null)
            {
                if (row["SysUserTask_id"] != null)
                {
                    task.SysUserTask_id = row["SysUserTask_id"].ToString();
                }
                if (row["SysUser_ID"] != null)
                {
                    task.SysUser_ID = row["SysUser_ID"].ToString();
                }
                if (row["TaskType"] != null)
                {
                    task.TaskType = row["TaskType"].ToString();
                }
                if (row["TaskID"] != null)
                {
                    task.TaskID = row["TaskID"].ToString();
                }
                if (row["TaskName"] != null)
                {
                    task.TaskName = row["TaskName"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    task.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    task.CreateUser = row["CreateUser"].ToString();
                }
            }
            return task;
        }

        public bool Delete(string SysUserTask_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUserTask ");
            builder.Append(" where SysUserTask_id=@SysUserTask_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUserTask_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysUserTask_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SysUserTask_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUserTask ");
            builder.Append(" where SysUserTask_id in (" + SysUserTask_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SysUserTask_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SysUserTask");
            builder.Append(" where SysUserTask_id=@SysUserTask_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUserTask_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysUserTask_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SysUserTask_id,SysUser_ID,TaskType,TaskID,TaskName,CreateTime,CreateUser ");
            builder.Append(" FROM SysUserTask ");
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
            builder.Append(" SysUserTask_id,SysUser_ID,TaskType,TaskID,TaskName,CreateTime,CreateUser ");
            builder.Append(" FROM SysUserTask ");
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
                builder.Append("order by T.SysUserTask_id desc");
            }
            builder.Append(")AS Row, T.*  from SysUserTask T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SysUserTask GetModel(string SysUserTask_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SysUserTask_id,SysUser_ID,TaskType,TaskID,TaskName,CreateTime,CreateUser from SysUserTask ");
            builder.Append(" where SysUserTask_id=@SysUserTask_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUserTask_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SysUserTask_id;
            new Model_SysUserTask();
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
            builder.Append("select count(1) FROM SysUserTask ");
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

        public int TaskAllocation(List<Model_SysUserTask> list, string SysUser_ID, string CreateUser)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SysUserTask where SysUser_ID=@SysUser_ID ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = SysUser_ID;
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_SysUserTask task in list)
            {
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into SysUserTask(");
                builder.Append("SysUserTask_id,SysUser_ID,TaskType,TaskID,TaskName,CreateTime,CreateUser)");
                builder.Append(" values (");
                builder.Append("@SysUserTask_id,@SysUser_ID,@TaskType,@TaskID,@TaskName,@CreateTime,@CreateUser)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@SysUserTask_id", SqlDbType.Char, 0x24), new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@TaskType", SqlDbType.VarChar, 20), new SqlParameter("@TaskID", SqlDbType.VarChar, 0x24), new SqlParameter("@TaskName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.Date, 3), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray2[0].Value = task.SysUserTask_id;
                parameterArray2[1].Value = task.SysUser_ID;
                parameterArray2[2].Value = task.TaskType;
                parameterArray2[3].Value = task.TaskID;
                parameterArray2[4].Value = task.TaskName;
                parameterArray2[5].Value = task.CreateTime;
                parameterArray2[6].Value = task.CreateUser;
                num++;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            builder = new StringBuilder();
            builder.Append("exec P_GenerateDirectoryForUser @SysUser_ID,@CreateUser ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray3[0].Value = SysUser_ID;
            parameterArray3[1].Value = CreateUser;
            dictionary.Add(builder.ToString(), parameterArray3);
            int num2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (num2 > 0)
            {
                return num2;
            }
            return 0;
        }

        public bool Update(Model_SysUserTask model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SysUserTask set ");
            builder.Append("SysUser_ID=@SysUser_ID,");
            builder.Append("TaskType=@TaskType,");
            builder.Append("TaskID=@TaskID,");
            builder.Append("TaskName=@TaskName,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where SysUserTask_id=@SysUserTask_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SysUser_ID", SqlDbType.Char, 0x24), new SqlParameter("@TaskType", SqlDbType.VarChar, 20), new SqlParameter("@TaskID", SqlDbType.VarChar, 0x24), new SqlParameter("@TaskName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime, 3), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@SysUserTask_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SysUser_ID;
            cmdParms[1].Value = model.TaskType;
            cmdParms[2].Value = model.TaskID;
            cmdParms[3].Value = model.TaskName;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.SysUserTask_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

