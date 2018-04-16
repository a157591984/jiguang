namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_HWScoreLevelDict
    {
        public bool Add(Model_HWScoreLevelDict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HWScoreLevelDict(");
            builder.Append("HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,Remark,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@HWScoreLevelDictID,@HWScoreLevelName,@HWScoreLevelRateLeft,@HWScoreLevelRateRight,@AddedValue,@Remark,@CreateTime,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24), new SqlParameter("@HWScoreLevelName", SqlDbType.NVarChar, 50), new SqlParameter("@HWScoreLevelRateLeft", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelRateRight", SqlDbType.Decimal, 5), new SqlParameter("@AddedValue", SqlDbType.Decimal, 5), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HWScoreLevelDictID;
            cmdParms[1].Value = model.HWScoreLevelName;
            cmdParms[2].Value = model.HWScoreLevelRateLeft;
            cmdParms[3].Value = model.HWScoreLevelRateRight;
            cmdParms[4].Value = model.AddedValue;
            cmdParms[5].Value = model.Remark;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_HWScoreLevelDict DataRowToModel(DataRow row)
        {
            Model_HWScoreLevelDict dict = new Model_HWScoreLevelDict();
            if (row != null)
            {
                if (row["HWScoreLevelDictID"] != null)
                {
                    dict.HWScoreLevelDictID = row["HWScoreLevelDictID"].ToString();
                }
                if (row["HWScoreLevelName"] != null)
                {
                    dict.HWScoreLevelName = row["HWScoreLevelName"].ToString();
                }
                if ((row["HWScoreLevelRateLeft"] != null) && (row["HWScoreLevelRateLeft"].ToString() != ""))
                {
                    dict.HWScoreLevelRateLeft = new decimal?(decimal.Parse(row["HWScoreLevelRateLeft"].ToString()));
                }
                if ((row["HWScoreLevelRateRight"] != null) && (row["HWScoreLevelRateRight"].ToString() != ""))
                {
                    dict.HWScoreLevelRateRight = new decimal?(decimal.Parse(row["HWScoreLevelRateRight"].ToString()));
                }
                if ((row["AddedValue"] != null) && (row["AddedValue"].ToString() != ""))
                {
                    dict.AddedValue = new decimal?(decimal.Parse(row["AddedValue"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    dict.Remark = row["Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    dict.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    dict.CreateUser = row["CreateUser"].ToString();
                }
            }
            return dict;
        }

        public bool Delete(string HWScoreLevelDictID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HWScoreLevelDict ");
            builder.Append(" where HWScoreLevelDictID=@HWScoreLevelDictID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HWScoreLevelDictID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string HWScoreLevelDictIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HWScoreLevelDict ");
            builder.Append(" where HWScoreLevelDictID in (" + HWScoreLevelDictIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string HWScoreLevelDictID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from HWScoreLevelDict");
            builder.Append(" where HWScoreLevelDictID=@HWScoreLevelDictID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HWScoreLevelDictID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,Remark,CreateTime,CreateUser ");
            builder.Append(" FROM HWScoreLevelDict ");
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
            builder.Append(" HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,Remark,CreateTime,CreateUser ");
            builder.Append(" FROM HWScoreLevelDict ");
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
                builder.Append("order by T.HWScoreLevelDictID desc");
            }
            builder.Append(")AS Row, T.*  from HWScoreLevelDict T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_HWScoreLevelDict GetModel(string HWScoreLevelDictID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 HWScoreLevelDictID,HWScoreLevelName,HWScoreLevelRateLeft,HWScoreLevelRateRight,AddedValue,Remark,CreateTime,CreateUser from HWScoreLevelDict ");
            builder.Append(" where HWScoreLevelDictID=@HWScoreLevelDictID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HWScoreLevelDictID;
            new Model_HWScoreLevelDict();
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
            builder.Append("select count(1) FROM HWScoreLevelDict ");
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

        public bool Update(Model_HWScoreLevelDict model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HWScoreLevelDict set ");
            builder.Append("HWScoreLevelName=@HWScoreLevelName,");
            builder.Append("HWScoreLevelRateLeft=@HWScoreLevelRateLeft,");
            builder.Append("HWScoreLevelRateRight=@HWScoreLevelRateRight,");
            builder.Append("AddedValue=@AddedValue,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where HWScoreLevelDictID=@HWScoreLevelDictID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HWScoreLevelName", SqlDbType.NVarChar, 50), new SqlParameter("@HWScoreLevelRateLeft", SqlDbType.Decimal, 5), new SqlParameter("@HWScoreLevelRateRight", SqlDbType.Decimal, 5), new SqlParameter("@AddedValue", SqlDbType.Decimal, 5), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@HWScoreLevelDictID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HWScoreLevelName;
            cmdParms[1].Value = model.HWScoreLevelRateLeft;
            cmdParms[2].Value = model.HWScoreLevelRateRight;
            cmdParms[3].Value = model.AddedValue;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.HWScoreLevelDictID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

