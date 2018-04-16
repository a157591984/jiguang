namespace Rc.Common.DBUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public sealed class DatabaseSQLHelper
    {
        public DatabaseSQLHelper()
        {
            this.dbHelperSQLP = new DbHelperSQLP();
        }

        public DatabaseSQLHelper(string connString)
        {
            this.dbHelperSQLP = new DbHelperSQLP(connString);
        }

        public void AddInParameter(DbCommand dbCommand, string parmName, DbType dbType, object value)
        {
            SqlParameter parameter = new SqlParameter(parmName, dbType);
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }
            dbCommand.Parameters.Add(parameter);
        }

        public void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            SqlParameter parameter = new SqlParameter(name, dbType) {
                Direction = direction,
                SourceVersion = sourceVersion
            };
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }
            command.Parameters.Add(parameter);
        }

        public DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
            }
            SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand) dbCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteDataSet(string sqlString, DbTransaction tran = null, params object[] paramValues)
        {
            if (string.IsNullOrEmpty(sqlString))
            {
                throw new ArgumentException("未传入SQL语句");
            }
            SqlParameter[] parametersSqlText = this.GetParametersSqlText(sqlString, paramValues);
            return this.dbHelperSQLP.Query(sqlString, parametersSqlText);
        }

        public int ExecuteNonQuery(DbCommand dbCommand)
        {
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
            }
            using (dbCommand.Connection)
            {
                dbCommand.Connection.Open();
                return dbCommand.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string sqlString, DbTransaction tran = null, params object[] paramValues)
        {
            if (string.IsNullOrEmpty(sqlString))
            {
                throw new ArgumentException("未传入SQL语句");
            }
            SqlParameter[] parametersSqlText = this.GetParametersSqlText(sqlString, paramValues);
            return this.dbHelperSQLP.ExecuteSql(sqlString, parametersSqlText);
        }

        public int ExecuteNonQueryByPro(string storedProcedureName, params object[] parameterValues)
        {
            DbCommand command = new SqlCommand(storedProcedureName) {
                CommandType = CommandType.StoredProcedure
            };
            object[] objArray = parameterValues;
            for (int i = 0; i < objArray.Length; i++)
            {
                object obj1 = objArray[i];
                command.Parameters.Add(parameterValues);
            }
            return command.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader(DbCommand dbCommand)
        {
            IDataReader reader;
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
            }
            try
            {
                dbCommand.Connection.Open();
                reader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                dbCommand.Connection.Close();
                throw exception;
            }
            return reader;
        }

        public IDataReader ExecuteReader(string strSql)
        {
            IDataReader reader;
            DbCommand command = new SqlCommand(strSql, new SqlConnection(this.ConnectionString));
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                command.Connection.Close();
                throw exception;
            }
            return reader;
        }

        public IDataReader ExecuteReader(CommandType commandType, string strSql)
        {
            IDataReader reader;
            DbCommand command = new SqlCommand(strSql, new SqlConnection(this.ConnectionString)) {
                CommandType = commandType
            };
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                command.Connection.Close();
                throw exception;
            }
            return reader;
        }

        public IDataReader ExecuteReader(DbCommand dbCommand, DbTransaction tran, params object[] pamrs)
        {
            IDataReader reader;
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
            }
            foreach (object obj2 in pamrs)
            {
                dbCommand.Parameters.Add(obj2);
            }
            try
            {
                dbCommand.Connection.Open();
                reader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                dbCommand.Connection.Close();
                throw exception;
            }
            return reader;
        }

        public IDataReader ExecuteReader(string strSql, DbTransaction tran, params object[] pamrs)
        {
            SqlParameter[] parametersSqlText = this.GetParametersSqlText(strSql, pamrs);
            return this.dbHelperSQLP.ExecuteReader(strSql, parametersSqlText);
        }

        public object ExecuteScalar(DbCommand dbCommand)
        {
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
            }
            using (dbCommand.Connection)
            {
                dbCommand.Connection.Open();
                return dbCommand.ExecuteScalar();
            }
        }

        public object ExecuteScalar(string strSql)
        {
            DbCommand command = new SqlCommand(strSql, new SqlConnection(this.ConnectionString));
            using (command.Connection)
            {
                command.Connection.Open();
                return command.ExecuteScalar();
            }
        }

        public object ExecuteScalar(string sqlString, DbTransaction tran = null, params object[] paramValues)
        {
            if (string.IsNullOrEmpty(sqlString))
            {
                throw new ArgumentException("未传入SQL语句");
            }
            SqlParameter[] parametersSqlText = this.GetParametersSqlText(sqlString, paramValues);
            return this.dbHelperSQLP.GetSingle(sqlString, parametersSqlText);
        }

        internal SqlParameter[] GetParametersSqlText(string sqlString, params object[] parameterValues)
        {
            if (!this.SameNumParamAndValues(sqlString, parameterValues))
            {
                throw new InvalidOperationException("条件参数与传入参数数量不一致");
            }
            List<SqlParameter> list = new List<SqlParameter>();
            List<string> list2 = this.getSqlParam(sqlString);
            if (((list2 != null) && (list2.Count == parameterValues.Length)) && (list2.Count > 0))
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    SqlParameter item = new SqlParameter {
                        IsNullable = true,
                        Value = (parameterValues[i] == null) ? DBNull.Value : parameterValues[i],
                        ParameterName = list2[i]
                    };
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        private List<string> getSqlParam(string sql)
        {
            if ((sql == null) || (sql == ""))
            {
                return null;
            }
            string pattern = "@([a-zA-Z_0-9])+";
            List<string> list = new List<string>();
            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            IEnumerator enumerator = regex.Matches(sql).GetEnumerator() ;
           
                while (enumerator.MoveNext())
                {
                    Predicate<string> match = null;
                    Match m = (Match) enumerator.Current;
                    if (match == null)
                    {
                        match = s => s.ToLower() == m.Value.ToLower();
                    }
                    if (!list.Exists(match))
                    {
                        list.Add(m.Value);
                    }
                }
           
            return list;
        }

        public DbCommand GetSqlStringCommand(string strSql)
        {
            return new SqlCommand(strSql) { CommandType = CommandType.Text, Connection = new SqlConnection(this.ConnectionString) };
        }

        public DbCommand GetStoredProcCommand(string strSql)
        {
            return new SqlCommand(strSql) { CommandType = CommandType.StoredProcedure, Connection = new SqlConnection(this.ConnectionString) };
        }

        private bool SameNumParamAndValues(string sqlString, object[] parameterValues)
        {
            Regex regex = new Regex("@([a-zA-Z0-9_])+");
            List<string> list = new List<string>();
            IEnumerator enumerator = regex.Matches(sqlString).GetEnumerator();
            
                while (enumerator.MoveNext())
                {
                    Predicate<string> match = null;
                    Match m = (Match) enumerator.Current;
                    if (match == null)
                    {
                        match = p => p == m.Value;
                    }
                    if (!list.Exists(match))
                    {
                        list.Add(m.Value);
                    }
                }
          
            if ((list.Count != 0) && (list.Count != parameterValues.Length))
            {
                
                return false;
            }
            return true;
        }

        public string ConnectionString
        {
            get
            {
                return this.dbHelperSQLP.connectionString;
            }
        }

        public DbHelperSQLP dbHelperSQLP { get; private set; }
    }
}

