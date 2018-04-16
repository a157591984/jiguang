namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ExeccuteDataAnalysis
    {
        public bool Add(Model_ExeccuteDataAnalysis model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ExeccuteDataAnalysis(");
            builder.Append("ExeccuteDataAnalysisID,ExeccuteLenght,Remark,ExeccuteTime,ExeccuteUserId,ExeccuteUserName)");
            builder.Append(" values (");
            builder.Append("@ExeccuteDataAnalysisID,@ExeccuteLenght,@Remark,@ExeccuteTime,@ExeccuteUserId,@ExeccuteUserName)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExeccuteDataAnalysisID", SqlDbType.Char, 0x24), new SqlParameter("@ExeccuteLenght", SqlDbType.NVarChar, 20), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@ExeccuteTime", SqlDbType.DateTime), new SqlParameter("@ExeccuteUserId", SqlDbType.Char, 0x24), new SqlParameter("@ExeccuteUserName", SqlDbType.NVarChar, 20) };
            cmdParms[0].Value = model.ExeccuteDataAnalysisID;
            cmdParms[1].Value = model.ExeccuteLenght;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.ExeccuteTime;
            cmdParms[4].Value = model.ExeccuteUserId;
            cmdParms[5].Value = model.ExeccuteUserName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ExeccuteDataAnalysis DataRowToModel(DataRow row)
        {
            Model_ExeccuteDataAnalysis analysis = new Model_ExeccuteDataAnalysis();
            if (row != null)
            {
                if (row["ExeccuteDataAnalysisID"] != null)
                {
                    analysis.ExeccuteDataAnalysisID = row["ExeccuteDataAnalysisID"].ToString();
                }
                if (row["ExeccuteLenght"] != null)
                {
                    analysis.ExeccuteLenght = row["ExeccuteLenght"].ToString();
                }
                if (row["Remark"] != null)
                {
                    analysis.Remark = row["Remark"].ToString();
                }
                if ((row["ExeccuteTime"] != null) && (row["ExeccuteTime"].ToString() != ""))
                {
                    analysis.ExeccuteTime = new DateTime?(DateTime.Parse(row["ExeccuteTime"].ToString()));
                }
                if (row["ExeccuteUserId"] != null)
                {
                    analysis.ExeccuteUserId = row["ExeccuteUserId"].ToString();
                }
                if (row["ExeccuteUserName"] != null)
                {
                    analysis.ExeccuteUserName = row["ExeccuteUserName"].ToString();
                }
            }
            return analysis;
        }

        public bool Delete(string ExeccuteDataAnalysisID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ExeccuteDataAnalysis ");
            builder.Append(" where ExeccuteDataAnalysisID=@ExeccuteDataAnalysisID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExeccuteDataAnalysisID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ExeccuteDataAnalysisID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ExeccuteDataAnalysisIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ExeccuteDataAnalysis ");
            builder.Append(" where ExeccuteDataAnalysisID in (" + ExeccuteDataAnalysisIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ExeccuteDataAnalysisID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ExeccuteDataAnalysis");
            builder.Append(" where ExeccuteDataAnalysisID=@ExeccuteDataAnalysisID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExeccuteDataAnalysisID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ExeccuteDataAnalysisID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ExeccuteDataAnalysisID,ExeccuteLenght,Remark,ExeccuteTime,ExeccuteUserId,ExeccuteUserName ");
            builder.Append(" FROM ExeccuteDataAnalysis ");
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
            builder.Append(" ExeccuteDataAnalysisID,ExeccuteLenght,Remark,ExeccuteTime,ExeccuteUserId,ExeccuteUserName ");
            builder.Append(" FROM ExeccuteDataAnalysis ");
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
                builder.Append("order by T.ExeccuteDataAnalysisID desc");
            }
            builder.Append(")AS Row, T.*  from ExeccuteDataAnalysis T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ExeccuteDataAnalysis GetModel(string ExeccuteDataAnalysisID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ExeccuteDataAnalysisID,ExeccuteLenght,Remark,ExeccuteTime,ExeccuteUserId,ExeccuteUserName from ExeccuteDataAnalysis ");
            builder.Append(" where ExeccuteDataAnalysisID=@ExeccuteDataAnalysisID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExeccuteDataAnalysisID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ExeccuteDataAnalysisID;
            new Model_ExeccuteDataAnalysis();
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
            builder.Append("select count(1) FROM ExeccuteDataAnalysis ");
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

        public GH_PagerInfo<List<Model_ExeccuteDataAnalysis>> SearhExeccuteDataAnalysis(string Where, string Sort, int pageIndex, int pageSize)
        {
            new StringBuilder();
            string strWhere = " 1=1 " + Where;
            int recordCount = this.GetRecordCount(strWhere);
            int startIndex = ((pageIndex - 1) * pageSize) + 1;
            int endIndex = pageIndex * pageSize;
            DataTable table = this.GetListByPage(strWhere, Sort, startIndex, endIndex).Tables[0];
            List<Model_ExeccuteDataAnalysis> list = new List<Model_ExeccuteDataAnalysis>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(this.DataRowToModel(row));
            }
            return new GH_PagerInfo<List<Model_ExeccuteDataAnalysis>> { PageSize = pageSize, CurrentPage = pageIndex, RecordCount = recordCount, PageData = list };
        }

        public bool Update(Model_ExeccuteDataAnalysis model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ExeccuteDataAnalysis set ");
            builder.Append("ExeccuteLenght=@ExeccuteLenght,");
            builder.Append("Remark=@Remark,");
            builder.Append("ExeccuteTime=@ExeccuteTime,");
            builder.Append("ExeccuteUserId=@ExeccuteUserId,");
            builder.Append("ExeccuteUserName=@ExeccuteUserName");
            builder.Append(" where ExeccuteDataAnalysisID=@ExeccuteDataAnalysisID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExeccuteLenght", SqlDbType.NVarChar, 20), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@ExeccuteTime", SqlDbType.DateTime), new SqlParameter("@ExeccuteUserId", SqlDbType.Char, 0x24), new SqlParameter("@ExeccuteUserName", SqlDbType.NVarChar, 20), new SqlParameter("@ExeccuteDataAnalysisID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ExeccuteLenght;
            cmdParms[1].Value = model.Remark;
            cmdParms[2].Value = model.ExeccuteTime;
            cmdParms[3].Value = model.ExeccuteUserId;
            cmdParms[4].Value = model.ExeccuteUserName;
            cmdParms[5].Value = model.ExeccuteDataAnalysisID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

