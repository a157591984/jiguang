namespace Rc.Common.DBUtility
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public class sys
    {
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
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage", parameters, "ds", out rCount, out pCount);
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
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage", parameters, "ds", out rCount, out pCount);
        }
    }
}

