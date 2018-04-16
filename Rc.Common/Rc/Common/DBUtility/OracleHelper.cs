namespace Rc.Common.DBUtility
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OracleClient;

    public class OracleHelper
    {
        private static OracleConnection _con = null;
        public static string constr = PubConstant.ConnectionString;

        public OracleHelper()
        {
        }

        public OracleHelper(string Linkstr)
        {
            constr = PubConstant.GetConnectionString(Linkstr);
        }

        public OracleDataReader ExecuteReader(string strSQL)
        {
            OracleDataReader reader2;
            OracleConnection connection = new OracleConnection(constr);
            OracleCommand command = new OracleCommand(strSQL, connection);
            try
            {
                connection.Open();
                reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (OracleException exception)
            {
                throw new Exception(exception.Message);
            }
            return reader2;
        }

        public int ExecuteSql(string SQLString)
        {
            int num2;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        num2 = command.ExecuteNonQuery();
                    }
                    catch (OracleException exception)
                    {
                        connection.Close();
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return num2;
        }

        public int ExecuteSql(string SQLString, string content)
        {
            int num2;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                OracleCommand command = new OracleCommand(SQLString, connection);
                OracleParameter parameter = new OracleParameter("@content", OracleType.NVarChar) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (OracleException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int num2;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                OracleCommand command = new OracleCommand(strSQL, connection);
                OracleParameter parameter = new OracleParameter("@fs", OracleType.LongRaw) {
                    Value = fs
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (OracleException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            bool flag = false;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                connection.Open();
                OracleCommand command = new OracleCommand {
                    Connection = connection
                };
                OracleTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    try
                    {
                        for (int i = 0; i < SQLStringList.Count; i++)
                        {
                            string str = SQLStringList[i].ToString();
                            if (str.Trim().Length > 1)
                            {
                                command.CommandText = str;
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        flag = true;
                    }
                    catch (OracleException exception)
                    {
                        flag = false;
                        transaction.Rollback();
                        throw new Exception(exception.Message);
                    }
                    return flag;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return flag;
        }

        public bool Exists(string SQLString)
        {
            bool flag;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object objA = command.ExecuteScalar();
                        if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                        {
                            return false;
                        }
                        flag = true;
                    }
                    catch (OracleException exception)
                    {
                        connection.Close();
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return flag;
        }

        public bool Exists(string strSql, params OracleParameter[] cmdParms)
        {
            int num;
            object single = this.GetSingle(strSql, cmdParms);
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

        public int GetMaxID(string FieldName, string TableName)
        {
            string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
            object single = this.GetSingle(sQLString);
            if (single == null)
            {
                return 1;
            }
            return int.Parse(single.ToString());
        }

        public object GetSingle(string SQLString)
        {
            object obj3;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand(SQLString, connection))
                {
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
                    catch (OracleException exception)
                    {
                        connection.Close();
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return obj3;
        }

        public object GetSingle(string SQLString, params OracleParameter[] cmdParms)
        {
            object obj3;
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    try
                    {
                        this.PrepareCommand(command, connection, null, SQLString, cmdParms);
                        object objA = command.ExecuteScalar();
                        command.Parameters.Clear();
                        if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                        {
                            return null;
                        }
                        obj3 = objA;
                    }
                    catch (OracleException exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return obj3;
        }

        private void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
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
                foreach (OracleParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public DataSet Query(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(constr))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new OracleDataAdapter(SQLString, connection).Fill(dataSet, "ds");
                }
                catch (OracleException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close();
                }
                return dataSet;
            }
        }

        public static OracleConnection Con
        {
            get
            {
                if (_con == null)
                {
                    _con = new OracleConnection();
                }
                if (_con.ConnectionString == "")
                {
                    _con.ConnectionString = constr;
                }
                return _con;
            }
            set
            {
                _con = value;
            }
        }
    }
}

