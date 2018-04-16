namespace Rc.Common.DBUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class DbHelperSQL_Operate
    {
        public static string connectionString = PubConstant.ConnectionString_Operate;

        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection) {
                CommandType = CommandType.StoredProcedure
            };
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        public static bool ColumnExists(string tableName, string columnName)
        {
            object single = GetSingle("select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'");
            if (single == null)
            {
                return false;
            }
            return (Convert.ToInt32(single) > 0);
        }

        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlDataReader reader2;
            strSQL = FiltrationDangerChar(strSQL);
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                connection.Close();
                reader2 = reader;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlDataReader reader2;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                conn.Close();
                reader2 = reader;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public static int ExecuteSql(string SQLString)
        {
            int num2;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    int num = command.ExecuteNonQuery();
                    connection.Close();
                    num2 = num;
                }
                catch (SqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, string content)
        {
            int num2;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                SqlParameter parameter = new SqlParameter("@content", SqlDbType.NText) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            int num2;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    num2 = num;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            int num2;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    command.CommandTimeout = Times;
                    int num = command.ExecuteNonQuery();
                    connection.Close();
                    num2 = num;
                }
                catch (SqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return num2;
        }

        public static object ExecuteSqlGet(string SQLString, string content)
        {
            object obj3;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                SqlParameter parameter = new SqlParameter("@content", SqlDbType.NText) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return obj3;
        }

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int num2;
            strSQL = FiltrationDangerChar(strSQL);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(strSQL, connection);
                SqlParameter parameter = new SqlParameter("@fs", SqlDbType.Image) {
                    Value = fs
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteSqlTran(Dictionary<string, SqlParameter[]> dictionary)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    int num = 0;
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        foreach (KeyValuePair<string, SqlParameter[]> pair in dictionary)
                        {
                            string cmdText = pair.Key.ToString();
                            SqlParameter[] cmdParms = pair.Value;
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            num += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                        num2 = num;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (CommandInfo info in cmdList)
                        {
                            string commandText = info.CommandText;
                            SqlParameter[] parameters = (SqlParameter[]) info.Parameters;
                            PrepareCommand(cmd, connection, transaction, commandText, parameters);
                            if ((info.EffentNextType == EffentNextType.WhenHaveContine) || (info.EffentNextType == EffentNextType.WhenNoHaveContine))
                            {
                                if (info.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                                object obj2 = cmd.ExecuteScalar();
                                bool flag = false;
                                if ((obj2 == null) && (obj2 == DBNull.Value))
                                {
                                    flag = false;
                                }
                                flag = Convert.ToInt32(obj2) > 0;
                                if ((info.EffentNextType == EffentNextType.WhenHaveContine) && !flag)
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                                if ((info.EffentNextType == EffentNextType.WhenNoHaveContine) && flag)
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                            }
                            else
                            {
                                int num2 = cmd.ExecuteNonQuery();
                                num += num2;
                                if ((info.EffentNextType == EffentNextType.ExcuteEffectRows) && (num2 == 0))
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                                cmd.Parameters.Clear();
                            }
                        }
                        transaction.Commit();
                        num3 = num;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return num3;
        }

        public static int ExecuteSqlTran(List<string> SQLStringList)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection
                };
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    int num = 0;
                    for (int i = 0; i < SQLStringList.Count; i++)
                    {
                        string strSql = SQLStringList[i];
                        if (strSql.Trim().Length > 1)
                        {
                            strSql = FiltrationDangerChar(strSql);
                            command.CommandTimeout = 0x708;
                            command.CommandText = strSql;
                            num += command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    num3 = num;
                }
                catch
                {
                    transaction.Rollback();
                    num3 = 0;
                }
            }
            return num3;
        }

        public static int ExecuteSqlTran(string[] SQLStringArr)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection
                };
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    int num = 0;
                    for (int i = 0; i < SQLStringArr.Length; i++)
                    {
                        string strSql = SQLStringArr[i];
                        if (strSql.Trim().Length > 1)
                        {
                            strSql = FiltrationDangerChar(strSql);
                            command.CommandTimeout = 0x708;
                            command.CommandText = strSql;
                            num += command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    num3 = num;
                }
                catch
                {
                    transaction.Rollback();
                    num3 = 0;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num3;
        }

        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[]) entry.Value;
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (CommandInfo info in SQLStringList)
                        {
                            string commandText = info.CommandText;
                            SqlParameter[] parameters = (SqlParameter[]) info.Parameters;
                            foreach (SqlParameter parameter in parameters)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, commandText, parameters);
                            cmd.ExecuteNonQuery();
                            foreach (SqlParameter parameter2 in parameters)
                            {
                                if (parameter2.Direction == ParameterDirection.Output)
                                {
                                    num = Convert.ToInt32(parameter2.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[]) entry.Value;
                            foreach (SqlParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            foreach (SqlParameter parameter2 in cmdParms)
                            {
                                if (parameter2.Direction == ParameterDirection.Output)
                                {
                                    num = Convert.ToInt32(parameter2.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool Exists(string strSql)
        {
            int num;
            object single = GetSingle(strSql);
            if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(single.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            int num;
            object single = GetSingle(strSql, cmdParms);
            if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(single.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public static string FiltrationDangerChar(string strSql)
        {
            string str = string.Empty;
            if (strSql != "")
            {
                str = strSql.Replace("--", "－－");
            }
            return str;
        }

        public static int GetMaxID(string FieldName, string TableName)
        {
            object single = GetSingle("select max(" + FieldName + ")+1 from " + TableName);
            if (single == null)
            {
                return 1;
            }
            return int.Parse(single.ToString());
        }

        public static int GetMaxID(string FieldName, string TableName, string condition)
        {
            object single = GetSingle("select isnull(max(" + FieldName + "),0)+1 from " + TableName + condition);
            if (single == null)
            {
                return 1;
            }
            return int.Parse(single.ToString());
        }

        public static DataSet GetRecordByPage(string pSql, int pNum, int pSize, out int rCount, out int pCount)
        {
            return GetRecordByPage(pSql, pNum, pSize, "", out rCount, out pCount);
        }

        public static DataSet GetRecordByPage(string pSql, int pNum, int pSize, string sort, out int rCount, out int pCount)
        {
            rCount = 0;
            pCount = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@pSql", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@pNum", SqlDbType.Int), new SqlParameter("@pSize", SqlDbType.Int), new SqlParameter("@sort", SqlDbType.NVarChar), new SqlParameter("@rowNumName", SqlDbType.NVarChar, 100), new SqlParameter("@rCount", SqlDbType.Int), new SqlParameter("@pCount", SqlDbType.Int) };
            parameters[0].Value = pSql;
            parameters[1].Value = pNum;
            parameters[2].Value = pSize;
            parameters[3].Value = sort;
            parameters[4].Value = "r_n";
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;
            return RunProcedure("UP_GetRecordByPage", parameters, "ds", out rCount, out pCount);
        }

        public static DataSet GetRecordByPage(string pSql, int pNum, int pSize, string sort, string rowNumName, out int rCount, out int pCount)
        {
            rCount = 0;
            pCount = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@pSql", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@pNum", SqlDbType.Int), new SqlParameter("@pSize", SqlDbType.Int), new SqlParameter("@sort", SqlDbType.NVarChar), new SqlParameter("@rowNumName", SqlDbType.NVarChar, 100), new SqlParameter("@rCount", SqlDbType.Int), new SqlParameter("@pCount", SqlDbType.Int) };
            parameters[0].Value = pSql;
            parameters[1].Value = pNum;
            parameters[2].Value = pSize;
            parameters[3].Value = sort;
            parameters[4].Value = rowNumName;
            parameters[5].Direction = ParameterDirection.Output;
            parameters[6].Direction = ParameterDirection.Output;
            return RunProcedure("UP_GetRecordByPage", parameters, "ds", out rCount, out pCount);
        }

        public static object GetSingle(string SQLString)
        {
            object obj3;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return "";
                    }
                    obj3 = objA;
                }
                catch (SqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle(string SQLString, int Times)
        {
            object obj3;
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    command.CommandTimeout = Times;
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (SqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object objA = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
            return obj3;
        }

        public static string GetValueByTableAndFiled(string tableName, string filedName, string Where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select " + filedName + " from " + tableName);
            builder.Append(" where 1=1 " + Where);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return single.ToString();
            }
            return "";
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static DataSet Query(string SQLString)
        {
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new SqlDataAdapter(SQLString, connection).Fill(dataSet, "ds");
                }
                catch (SqlException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public static DataSet Query(string SQLString, int Times)
        {
            SQLString = FiltrationDangerChar(SQLString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new SqlDataAdapter(SQLString, connection) { SelectCommand = { CommandTimeout = Times } }.Fill(dataSet, "ds");
                }
                catch (SqlException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            DataSet set2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        adapter.Fill(dataSet, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    set2 = dataSet;
                }
            }
            return set2;
        }

        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            connection.Close();
            return reader;
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                new SqlDataAdapter { SelectCommand = BuildQueryCommand(connection, storedProcName, parameters) }.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                return (int) command.Parameters["ReturnValue"].Value;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = Times;
                adapter.Fill(dataSet, tableName);
                adapter.Dispose();
                connection.Close();
                return dataSet;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, out int rCount, out int pCount)
        {
            rCount = 0;
            pCount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet, tableName);
                rCount = (int) command.Parameters["@rCount"].Value;
                pCount = (int) command.Parameters["@pCount"].Value;
                connection.Close();
                return dataSet;
            }
        }

        public static bool TabExists(string TableName)
        {
            int num;
            object single = GetSingle("select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1");
            if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(single.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }
    }
}

