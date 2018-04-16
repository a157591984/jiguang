namespace Rc.Common.DBUtility
{
    using System;
    using System.Configuration;

    public sealed class DatabaseSQLHelperFactory
    {
        public static DatabaseSQLHelper CreateDatabase()
        {
            return new DatabaseSQLHelper();
        }

        public static DatabaseSQLHelper CreateDatabase(string strConfigName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[strConfigName];
            if (settings != null)
            {
                return new DatabaseSQLHelper(settings.ConnectionString);
            }
            return null;
        }
    }
}

