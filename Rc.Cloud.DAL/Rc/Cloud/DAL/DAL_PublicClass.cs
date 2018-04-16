namespace Rc.Cloud.DAL
{
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_PublicClass
    {
        private string DType_GetSql(string D_Type)
        {
            string str = string.Empty;
            DataTable table = DbHelperSQL.Query("select * from DictionarySQlMaintenance d where DictionarySQlMaintenance_Mark='" + D_Type + "'").Tables[0];
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["DictionarySQlMaintenance_SQL"].ToString().Decrypt();
            }
            return str;
        }

        public DataSet GetCommon_Dict_List(string strWhere, string D_Type)
        {
            string str = this.DType_GetSql(D_Type).Replace("{0}", "");
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM ");
            builder.Append(" (" + str + ") as FF ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by D_Order,D_Name ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetCommonMultiselect(string IsPy, string D_Name, string D_Type, string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string format = this.DType_GetSql(D_Type);
            StringBuilder builder = new StringBuilder();
            if (strWhere != "")
            {
                builder.AppendFormat(format, strWhere);
            }
            else
            {
                builder.Append(format.Replace("{0}", ""));
            }
            string pSql = string.Empty;
            pSql = "select  row_number() over(order by D_Order,D_Name) AS r_n, * from ";
            pSql = (pSql + " (" + builder.ToString() + ") as FF ") + "where 1=1 ";
            if (D_Name != "")
            {
                pSql = pSql + string.Format(" and (D_Name like '%{0}%' or D_Code like '{0}%'", D_Name);
                if (IsPy == "1")
                {
                    pSql = pSql + string.Format(" or dbo.f_GetPy(d_name) like '%{0}%'", D_Name);
                }
                pSql = pSql + ")";
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetIntelligentAssociationList(string D_Type, string select, string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string str = this.DType_GetSql(D_Type);
            StringBuilder builder = new StringBuilder();
            builder.Append(str.Replace("{0}", ""));
            string pSql = string.Empty;
            pSql = (("select " + select + " from ") + " (" + builder.ToString() + ") as FF ") + "where 1=1 ";
            if (strWhere != "")
            {
                pSql = pSql + strWhere;
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }
    }
}

