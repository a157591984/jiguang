namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_StatRes_Doc_Attr
    {
        public bool Add(Model_StatRes_Doc_Attr model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into StatRes_Doc_Attr(");
            builder.Append("ID,SYear,SDocType,SDocTypeName,SAttrType,SData_ID,SData_Name,SProductionCount,SDownloadCount,SSaleCount,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ID,@SYear,@SDocType,@SDocTypeName,@SAttrType,@SData_ID,@SData_Name,@SProductionCount,@SDownloadCount,@SSaleCount,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Char, 0x24), new SqlParameter("@SYear", SqlDbType.Int, 4), new SqlParameter("@SDocType", SqlDbType.Char, 0x24), new SqlParameter("@SDocTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@SAttrType", SqlDbType.VarChar, 30), new SqlParameter("@SData_ID", SqlDbType.Char, 0x24), new SqlParameter("@SData_Name", SqlDbType.VarChar, 250), new SqlParameter("@SProductionCount", SqlDbType.Int, 4), new SqlParameter("@SDownloadCount", SqlDbType.Int, 4), new SqlParameter("@SSaleCount", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = model.SYear;
            cmdParms[2].Value = model.SDocType;
            cmdParms[3].Value = model.SDocTypeName;
            cmdParms[4].Value = model.SAttrType;
            cmdParms[5].Value = model.SData_ID;
            cmdParms[6].Value = model.SData_Name;
            cmdParms[7].Value = model.SProductionCount;
            cmdParms[8].Value = model.SDownloadCount;
            cmdParms[9].Value = model.SSaleCount;
            cmdParms[10].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_StatRes_Doc_Attr DataRowToModel(DataRow row)
        {
            Model_StatRes_Doc_Attr attr = new Model_StatRes_Doc_Attr();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    attr.ID = row["ID"].ToString();
                }
                if ((row["SYear"] != null) && (row["SYear"].ToString() != ""))
                {
                    attr.SYear = new int?(int.Parse(row["SYear"].ToString()));
                }
                if (row["SDocType"] != null)
                {
                    attr.SDocType = row["SDocType"].ToString();
                }
                if (row["SDocTypeName"] != null)
                {
                    attr.SDocTypeName = row["SDocTypeName"].ToString();
                }
                if (row["SAttrType"] != null)
                {
                    attr.SAttrType = row["SAttrType"].ToString();
                }
                if (row["SData_ID"] != null)
                {
                    attr.SData_ID = row["SData_ID"].ToString();
                }
                if (row["SData_Name"] != null)
                {
                    attr.SData_Name = row["SData_Name"].ToString();
                }
                if ((row["SProductionCount"] != null) && (row["SProductionCount"].ToString() != ""))
                {
                    attr.SProductionCount = new int?(int.Parse(row["SProductionCount"].ToString()));
                }
                if ((row["SDownloadCount"] != null) && (row["SDownloadCount"].ToString() != ""))
                {
                    attr.SDownloadCount = new int?(int.Parse(row["SDownloadCount"].ToString()));
                }
                if ((row["SSaleCount"] != null) && (row["SSaleCount"].ToString() != ""))
                {
                    attr.SSaleCount = new int?(int.Parse(row["SSaleCount"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    attr.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return attr;
        }

        public bool Delete(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatRes_Doc_Attr ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from StatRes_Doc_Attr ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from StatRes_Doc_Attr");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public GH_PagerInfo<List<Model_StatRes_Doc_Attr>> GetAllList(string Where, string Sort, int pageIndex, int pageSize)
        {
            new StringBuilder();
            string strWhere = "  1=1 " + Where;
            int recordCount = this.GetRecordCount(strWhere);
            int startIndex = ((pageIndex - 1) * pageSize) + 1;
            int endIndex = pageIndex * pageSize;
            DataTable table = this.GetListByPage(strWhere, Sort, startIndex, endIndex).Tables[0];
            List<Model_StatRes_Doc_Attr> list = new List<Model_StatRes_Doc_Attr>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(this.DataRowToModel(row));
            }
            return new GH_PagerInfo<List<Model_StatRes_Doc_Attr>> { PageSize = pageSize, CurrentPage = pageIndex, RecordCount = recordCount, PageData = list };
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,SYear,SDocType,SDocTypeName,SAttrType,SData_ID,SData_Name,SProductionCount,SDownloadCount,SSaleCount,CreateTime ");
            builder.Append(" FROM StatRes_Doc_Attr ");
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
            builder.Append(" ID,SYear,SDocType,SDocTypeName,SAttrType,SData_ID,SData_Name,SProductionCount,SDownloadCount,SSaleCount,CreateTime ");
            builder.Append(" FROM StatRes_Doc_Attr ");
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from StatRes_Doc_Attr T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageForSearch(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by ID desc");
            }
            builder.Append(")AS Row, T.*  from (select srda.*,rf.ResourceFolder_Name from StatRes_Doc_Attr srda\r\nleft join ResourceFolder RF on rf.ResourceFolder_Level='5' \r\nand rf.ParticularYear=srda.SYear\r\nand rf.Resource_Type=srda.SDocType\r\nand rf.Resource_Version=srda.SData_ID) T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_StatRes_Doc_Attr GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,SYear,SDocType,SDocTypeName,SAttrType,SData_ID,SData_Name,SProductionCount,SDownloadCount,SSaleCount,CreateTime from StatRes_Doc_Attr ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ID;
            new Model_StatRes_Doc_Attr();
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
            builder.Append("select count(1) FROM StatRes_Doc_Attr ");
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

        public int GetRecordCountForSearch(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM (select srda.*,rf.ResourceFolder_Name from StatRes_Doc_Attr srda\r\nleft join ResourceFolder RF on rf.ResourceFolder_Level='5' \r\nand rf.ParticularYear=srda.SYear\r\nand rf.Resource_Type=srda.SDocType\r\nand rf.Resource_Version=srda.SData_ID) t ");
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

        public bool Update(Model_StatRes_Doc_Attr model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update StatRes_Doc_Attr set ");
            builder.Append("SYear=@SYear,");
            builder.Append("SDocType=@SDocType,");
            builder.Append("SDocTypeName=@SDocTypeName,");
            builder.Append("SAttrType=@SAttrType,");
            builder.Append("SData_ID=@SData_ID,");
            builder.Append("SData_Name=@SData_Name,");
            builder.Append("SProductionCount=@SProductionCount,");
            builder.Append("SDownloadCount=@SDownloadCount,");
            builder.Append("SSaleCount=@SSaleCount,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ID=@ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SYear", SqlDbType.Int, 4), new SqlParameter("@SDocType", SqlDbType.Char, 0x24), new SqlParameter("@SDocTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@SAttrType", SqlDbType.VarChar, 30), new SqlParameter("@SData_ID", SqlDbType.Char, 0x24), new SqlParameter("@SData_Name", SqlDbType.VarChar, 250), new SqlParameter("@SProductionCount", SqlDbType.Int, 4), new SqlParameter("@SDownloadCount", SqlDbType.Int, 4), new SqlParameter("@SSaleCount", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SYear;
            cmdParms[1].Value = model.SDocType;
            cmdParms[2].Value = model.SDocTypeName;
            cmdParms[3].Value = model.SAttrType;
            cmdParms[4].Value = model.SData_ID;
            cmdParms[5].Value = model.SData_Name;
            cmdParms[6].Value = model.SProductionCount;
            cmdParms[7].Value = model.SDownloadCount;
            cmdParms[8].Value = model.SSaleCount;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

