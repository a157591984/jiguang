namespace Rc.Common.DBUtility
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.OracleClient;

    public static class OracleDBHelper
    {
        private static OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, OracleParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(storedProcName, connection) {
                CommandType = CommandType.StoredProcedure
            };
            foreach (OracleParameter parameter in parameters)
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

        public static int ExecuteNonQuery(string sql, params OracleParameter[] sps)
        {
            int num2;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.Parameters.AddRange(sps);
                    int num = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    command.Dispose();
                    num2 = num;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteNonQueryWithTransaction(List<string> sqlList)
        {
            int num2;
            int num = 0;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    OracleTransaction transaction = connection.BeginTransaction();
                    OracleCommand command = new OracleCommand();
                    try
                    {
                        command.Transaction = transaction;
                        foreach (string str in sqlList)
                        {
                            command.Connection = connection;
                            command.CommandText = str;
                            num = command.ExecuteNonQuery();
                        }
                        command.Transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        num = 0;
                        command.Transaction.Rollback();
                        throw exception;
                    }
                    command.Parameters.Clear();
                    command.Dispose();
                    num2 = num;
                }
                catch (Exception exception2)
                {
                    throw exception2;
                }
                finally
                {
                    connection.Close();
                }
            }
            return num2;
        }

        public static OracleDataReader ExecuteProcDataReader(string storedProcName, OracleParameter[] parameters)
        {
            OracleDataReader reader;
            OracleConnection connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
                OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return reader;
        }

        public static DataTable ExecuteProcDataTable(string storedProcName, OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    new OracleDataAdapter { SelectCommand = BuildQueryCommand(connection, storedProcName, parameters) }.Fill(dataTable);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
                return dataTable;
            }
        }

        public static DataTable ExecuteQuery(string sql, params OracleParameter[] sps)
        {
            DataTable table2;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    OracleDataAdapter adapter = new OracleDataAdapter(sql, connection) {
                        SelectCommand = { CommandTimeout = 300 }
                    };
                    adapter.SelectCommand.Parameters.AddRange(sps);
                    adapter.Fill(dataTable);
                    adapter.Dispose();
                    table2 = dataTable;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return table2;
        }

        public static OracleDataReader ExecuteReader(string sql, params OracleParameter[] sps)
        {
            OracleDataReader reader2;
            OracleConnection connection = new OracleConnection(connectionString);
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                OracleCommand command = new OracleCommand(sql, connection);
                command.Parameters.AddRange(sps);
                OracleDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                reader2 = reader;
            }
            catch (Exception exception)
            {
                connection.Close();
                throw exception;
            }
            return reader2;
        }

        public static object ExecuteScalar(string sql, params OracleParameter[] sps)
        {
            object obj3;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    OracleCommand command = new OracleCommand(sql, connection) {
                        CommandTimeout = 300
                    };
                    if (sps != null)
                    {
                        command.Parameters.AddRange(sps);
                    }
                    object obj2 = command.ExecuteScalar();
                    command.Parameters.Clear();
                    command.Dispose();
                    obj3 = obj2;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return obj3;
        }

        public static void ExecuteStoreProcedure(string procedure, params OracleParameter[] sps)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    OracleCommand command = new OracleCommand(procedure, connection) {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddRange(sps);
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool Exists(string strOracle, params OracleParameter[] cmdParms)
        {
            int num;
            object objA = ExecuteScalar(strOracle, cmdParms);
            if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(objA.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public static OracleConnection GetConnection()
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                return connection;
            }
        }

        public static int GetMaxID(string FieldName, string TableName)
        {
            object obj2 = ExecuteScalar("select max(TO_NUMBER(" + FieldName + ")) from " + TableName, new OracleParameter[0]);
            if ((obj2 != null) && !string.IsNullOrEmpty(obj2.ToString()))
            {
                return int.Parse(obj2.ToString());
            }
            return 1;
        }

        public static string connectionString
        {
            get
            {
                string str2;
                if ((ConfigurationManager.ConnectionStrings["OracleHISConn"] == null) && (ConfigurationManager.ConnectionStrings["OracleHISConn"].ToString() == ""))
                {
                    throw new Exception("在webconfig配置文件中缺少配置项：vCyberConnectionString。或者配置不正确！");
                }
                string connectionString = ConfigurationManager.ConnectionStrings["OracleHISConn"].ToString();
                OracleConnection connection = new OracleConnection(connectionString);
                try
                {
                    connection.Open();
                    str2 = connectionString;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
                return str2;
            }
        }
    }
}

