namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_SyncFileToSchool
    {
        public bool Add(Model_SyncFileToSchool model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SyncFileToSchool(");
            builder.Append("SyncFileToSchool_Id,SchoolId,ResourceToResourceFolder_Id,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@SyncFileToSchool_Id,@SchoolId,@ResourceToResourceFolder_Id,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchool_Id", SqlDbType.Char, 0x24), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.SyncFileToSchool_Id;
            cmdParms[1].Value = model.SchoolId;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SyncFileToSchool DataRowToModel(DataRow row)
        {
            Model_SyncFileToSchool school = new Model_SyncFileToSchool();
            if (row != null)
            {
                if (row["SyncFileToSchool_Id"] != null)
                {
                    school.SyncFileToSchool_Id = row["SyncFileToSchool_Id"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    school.SchoolId = row["SchoolId"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    school.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["Remark"] != null)
                {
                    school.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    school.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    school.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return school;
        }

        public bool Delete(string SyncFileToSchool_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncFileToSchool ");
            builder.Append(" where SyncFileToSchool_Id=@SyncFileToSchool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchool_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SyncFileToSchool_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SyncFileToSchool ");
            builder.Append(" where SyncFileToSchool_Id in (" + SyncFileToSchool_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SyncFileToSchool_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SyncFileToSchool");
            builder.Append(" where SyncFileToSchool_Id=@SyncFileToSchool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchool_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SyncFileToSchool_Id,SchoolId,ResourceToResourceFolder_Id,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM SyncFileToSchool ");
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
            builder.Append(" SyncFileToSchool_Id,SchoolId,ResourceToResourceFolder_Id,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM SyncFileToSchool ");
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
                builder.Append("order by T.SyncFileToSchool_Id desc");
            }
            builder.Append(")AS Row, T.*  from SyncFileToSchool T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(int pType, string schoolId, string bookId, int PageIndex, int PageSize, out int rCount)
        {
            rCount = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@pType", SqlDbType.Int), new SqlParameter("@pSchoolId", SqlDbType.NVarChar, 200), new SqlParameter("@pBookId", SqlDbType.NVarChar, 200), new SqlParameter("@pNum", SqlDbType.Int), new SqlParameter("@pSize", SqlDbType.Int), new SqlParameter("@rCount", SqlDbType.Int) };
            parameters[0].Value = pType;
            parameters[1].Value = schoolId;
            parameters[2].Value = bookId;
            parameters[3].Value = PageIndex;
            parameters[4].Value = PageSize;
            parameters[5].Direction = ParameterDirection.Output;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage_SyncFileSchool_Tobe", parameters, "ds", out rCount);
        }

        public DataSet GetListByPage(string schoolId, string bookId, string isNeed, int PageIndex, int PageSize, out int rCount)
        {
            rCount = 0;
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM ( ");
            builder.AppendFormat(" select ROW_NUMBER() OVER (order by ResourceFolder_Order,ResourceToResourceFolder_Order)AS Row\r\n,rtrf.Resource_Type,rtrf.ResourceToResourceFolder_Id,rtrf.Resource_Name,rtrf.ResourceToResourceFolder_Order,rtrf.CreateTime \r\n,rf.ResourceFolder_Name,rf.ResourceFolder_Order,t.SyncFileToSchoolDataDetail2_Id\r\nfrom ResourceToResourceFolder rtrf\r\ninner join ResourceFolder rf on rf.ResourceFolder_Id=rtrf.ResourceFolder_Id\r\nleft join SyncFileToSchoolDataDetail2 t on t.SchoolId='{0}' and t.ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id \r\nwhere 1=1 ", schoolId);
            if (!string.IsNullOrEmpty(bookId))
            {
                builder.AppendFormat(" and rtrf.Book_ID='{0}' ", bookId);
            }
            if (isNeed == "1")
            {
                builder.Append(" and t.SyncFileToSchoolDataDetail2_Id is not null ");
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", ((PageIndex - 1) * PageSize) + 1, PageIndex * PageSize);
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat(" select count(1)\r\nfrom ResourceToResourceFolder rtrf\r\ninner join ResourceFolder rf on rf.ResourceFolder_Id=rtrf.ResourceFolder_Id\r\nleft join SyncFileToSchoolDataDetail2 t on t.SchoolId='{0}' and t.ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id \r\nwhere 1=1 ", schoolId);
            if (!string.IsNullOrEmpty(bookId))
            {
                builder2.AppendFormat(" and rtrf.Book_ID='{0}' ", bookId);
            }
            if (isNeed == "1")
            {
                builder2.Append(" and t.SyncFileToSchoolDataDetail2_Id is not null ");
            }
            rCount = int.Parse(DbHelperSQL.GetSingle(builder2.ToString()).ToString());
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SyncFileToSchool GetModel(string SyncFileToSchool_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SyncFileToSchool_Id,SchoolId,ResourceToResourceFolder_Id,Remark,CreateUser,CreateTime from SyncFileToSchool ");
            builder.Append(" where SyncFileToSchool_Id=@SyncFileToSchool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SyncFileToSchool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SyncFileToSchool_Id;
            new Model_SyncFileToSchool();
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
            builder.Append("select count(1) FROM SyncFileToSchool ");
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

        public bool Update(Model_SyncFileToSchool model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SyncFileToSchool set ");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where SyncFileToSchool_Id=@SyncFileToSchool_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@SyncFileToSchool_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SchoolId;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.Remark;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.SyncFileToSchool_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

