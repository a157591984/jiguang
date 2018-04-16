namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using Rc.Common.StrUtility;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Xml;

    public class DAL_V_Common_Dict
    {
        private string DType_GetSql(string D_Type)
        {
            XmlDocument document = new XmlDocument();
            document.Load(HttpContext.Current.Server.MapPath("/XmlFile/UserControlConfig.xml"));
            XmlNodeList childNodes = document.DocumentElement.SelectNodes("/UserControlConfig")[0].ChildNodes;
            string str = "";
            foreach (XmlNode node in childNodes)
            {
                if (node.Attributes["ConfigName"].Value.Trim() == D_Type)
                {
                    str = node.Attributes["SqlString"].Value.Decrypt();
                }
            }
            if (str.Trim() == "")
            {
                str = childNodes[0].Attributes["SqlString"].Value.Decrypt();
            }
            return str;
        }

        public DataSet GetCommon_Dict_List(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM V_Common_Dict ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by D_Order,D_Name ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetCommon_Dict_List(string strWhere, string D_Type)
        {
            string str = this.DType_GetSql(D_Type);
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

        public DataSet GetCommon_Dict_ListPaged(string D_Name, string D_Type, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string pSql = string.Empty;
            pSql = "select  row_number() over(order by D_Order,D_Name) AS r_n, * from V_Common_Dict where 1=1 ";
            if (D_Name != "")
            {
                pSql = pSql + string.Format(" and (D_Name like '%{0}%' or D_Code like '{0}%')", D_Name);
            }
            if (D_Type != "")
            {
                pSql = pSql + string.Format(" and D_Type = '{0}' ", D_Type);
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetCommon_Dict_ListPaged(string D_Name, string D_Type, string Hospital, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            string str = this.DType_GetSql(D_Type);
            string pSql = string.Empty;
            pSql = "select  row_number() over(order by D_Order,D_Name) AS r_n, * from ";
            pSql = (pSql + " (" + str + ") as FF ") + "where 1=1 ";
            if (D_Name != "")
            {
                pSql = pSql + string.Format(" and (D_Name like '%{0}%' or D_Code like '{0}%')", D_Name);
            }
            if (!string.IsNullOrEmpty(Hospital))
            {
                if (D_Type.Trim() == "V001")
                {
                    pSql = pSql + " AND Common_Dict_ID IN (select DoctorInfo_ID FROM DoctorInfo where DoctorInfo.HospitalDepartment_ID in(Select HospitalDepartment_ID from HospitalDepartment where HospitalInfo_ID='" + Hospital + "'))";
                }
                else
                {
                    pSql = pSql + " and D_Code ='" + Hospital + "'";
                }
            }
            return sys.GetRecordByPage(pSql, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetCommon_DictByType(string strType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Common_Dict_ID,D_Value,D_Name ");
            builder.Append(" FROM V_Common_Dict ");
            builder.Append(" where D_Type=" + strType);
            builder.Append(" order by D_Order,D_Name ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetCommon_target_List()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Common_Dict_ID, D_Name ");
            builder.Append(" FROM V_Common_Dict ");
            builder.Append(" Where D_Type = 6 ");
            builder.Append(" order by D_Order desc,D_Name asc");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Common_Dict_ID,D_Name,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime ");
            builder.Append(" FROM V_Common_Dict ");
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
            builder.Append(" Common_Dict_ID,D_Name,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime ");
            builder.Append(" FROM V_Common_Dict ");
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
                builder.Append("order by T.Common_Dict_ID desc");
            }
            builder.Append(")AS Row, T.*  from V_Common_Dict T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public MODEL_V_Common_Dict GetModel(string Common_Dict_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Common_Dict_ID,D_Name,D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime from V_Common_Dict ");
            builder.Append(" where Common_Dict_ID=@Common_Dict_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Common_Dict_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Common_Dict_ID;
            MODEL_V_Common_Dict dict = new MODEL_V_Common_Dict();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["Common_Dict_ID"] != null) && (set.Tables[0].Rows[0]["Common_Dict_ID"].ToString() != ""))
            {
                dict.Common_Dict_ID = set.Tables[0].Rows[0]["Common_Dict_ID"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Name"] != null) && (set.Tables[0].Rows[0]["D_Name"].ToString() != ""))
            {
                dict.D_Name = set.Tables[0].Rows[0]["D_Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Value"] != null) && (set.Tables[0].Rows[0]["D_Value"].ToString() != ""))
            {
                dict.D_Value = new int?(int.Parse(set.Tables[0].Rows[0]["D_Value"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_Code"] != null) && (set.Tables[0].Rows[0]["D_Code"].ToString() != ""))
            {
                dict.D_Code = set.Tables[0].Rows[0]["D_Code"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Level"] != null) && (set.Tables[0].Rows[0]["D_Level"].ToString() != ""))
            {
                dict.D_Level = new int?(int.Parse(set.Tables[0].Rows[0]["D_Level"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_Order"] != null) && (set.Tables[0].Rows[0]["D_Order"].ToString() != ""))
            {
                dict.D_Order = new int?(int.Parse(set.Tables[0].Rows[0]["D_Order"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_Type"] != null) && (set.Tables[0].Rows[0]["D_Type"].ToString() != ""))
            {
                dict.D_Type = set.Tables[0].Rows[0]["D_Type"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_Remark"] != null) && (set.Tables[0].Rows[0]["D_Remark"].ToString() != ""))
            {
                dict.D_Remark = set.Tables[0].Rows[0]["D_Remark"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_CreateUser"] != null) && (set.Tables[0].Rows[0]["D_CreateUser"].ToString() != ""))
            {
                dict.D_CreateUser = set.Tables[0].Rows[0]["D_CreateUser"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_CreateTime"] != null) && (set.Tables[0].Rows[0]["D_CreateTime"].ToString() != ""))
            {
                dict.D_CreateTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["D_CreateTime"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["D_ModifyUser"] != null) && (set.Tables[0].Rows[0]["D_ModifyUser"].ToString() != ""))
            {
                dict.D_ModifyUser = set.Tables[0].Rows[0]["D_ModifyUser"].ToString();
            }
            if ((set.Tables[0].Rows[0]["D_ModifyTime"] != null) && (set.Tables[0].Rows[0]["D_ModifyTime"].ToString() != ""))
            {
                dict.D_ModifyTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["D_ModifyTime"].ToString()));
            }
            return dict;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM V_Common_Dict ");
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
    }
}

