namespace Rc.Cloud.DAL
{
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Text;

    public class DAL_DictForHospital
    {
        public DataSet GetHospitalLevel(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM HospitalLevel_Dict ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by D_Order,D_Name ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetHospitalProperty(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Property_Dict ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by D_Order,D_Name ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetHospitalRegional(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Regional_Dict ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by D_Code ");
            return DbHelperSQL.Query(builder.ToString());
        }
    }
}

