namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_DictRelation_Detail
    {
        public bool Add(Model_DictRelation_Detail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DictRelation_Detail(");
            builder.Append("DictRelation_Detail_Id,DictRelation_Id,Dict_Id,Parent_Id,Dict_Type,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@DictRelation_Detail_Id,@DictRelation_Id,@Dict_Id,@Parent_Id,@Dict_Type,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Detail_Id", SqlDbType.Char, 0x24), new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24), new SqlParameter("@Dict_Id", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@Dict_Type", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.DictRelation_Detail_Id;
            cmdParms[1].Value = model.DictRelation_Id;
            cmdParms[2].Value = model.Dict_Id;
            cmdParms[3].Value = model.Parent_Id;
            cmdParms[4].Value = model.Dict_Type;
            cmdParms[5].Value = model.Remark;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Add(string DictRelation_Id, string SecondCode, List<Model_DictRelation_Detail> lsit)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("delete from DictRelation_Detail ");
            builder.Append(" where DictRelation_Id=@DictRelation_Id and Parent_Id=@Parent_Id");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = DictRelation_Id;
            parameterArray[1].Value = SecondCode;
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_DictRelation_Detail detail in lsit)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into DictRelation_Detail(");
                builder.Append("DictRelation_Detail_Id,DictRelation_Id,Dict_Id,Parent_Id,Dict_Type,Remark,CreateUser,CreateTime)");
                builder.Append(" values (");
                builder.Append("@DictRelation_Detail_Id,@DictRelation_Id,@Dict_Id,@Parent_Id,@Dict_Type,@Remark,@CreateUser,@CreateTime)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@DictRelation_Detail_Id", SqlDbType.Char, 0x24), new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24), new SqlParameter("@Dict_Id", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@Dict_Type", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray2[0].Value = detail.DictRelation_Detail_Id;
                parameterArray2[1].Value = detail.DictRelation_Id;
                parameterArray2[2].Value = detail.Dict_Id;
                parameterArray2[3].Value = detail.Parent_Id;
                parameterArray2[4].Value = detail.Dict_Type;
                parameterArray2[5].Value = detail.Remark;
                parameterArray2[6].Value = detail.CreateUser;
                parameterArray2[7].Value = detail.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_DictRelation_Detail DataRowToModel(DataRow row)
        {
            Model_DictRelation_Detail detail = new Model_DictRelation_Detail();
            if (row != null)
            {
                if (row["DictRelation_Detail_Id"] != null)
                {
                    detail.DictRelation_Detail_Id = row["DictRelation_Detail_Id"].ToString();
                }
                if (row["DictRelation_Id"] != null)
                {
                    detail.DictRelation_Id = row["DictRelation_Id"].ToString();
                }
                if (row["Dict_Id"] != null)
                {
                    detail.Dict_Id = row["Dict_Id"].ToString();
                }
                if (row["Parent_Id"] != null)
                {
                    detail.Parent_Id = row["Parent_Id"].ToString();
                }
                if (row["Dict_Type"] != null)
                {
                    detail.Dict_Type = row["Dict_Type"].ToString();
                }
                if (row["Remark"] != null)
                {
                    detail.Remark = row["Remark"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    detail.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    detail.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return detail;
        }

        public bool Delete(string DictRelation_Detail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from DictRelation_Detail ");
            builder.Append(" where DictRelation_Detail_Id=@DictRelation_Detail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Detail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = DictRelation_Detail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string DictRelation_Detail_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from DictRelation_Detail ");
            builder.Append(" where DictRelation_Detail_Id in (" + DictRelation_Detail_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string DictRelation_Detail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from DictRelation_Detail");
            builder.Append(" where DictRelation_Detail_Id=@DictRelation_Detail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Detail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = DictRelation_Detail_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DictRelation_Detail_Id,DictRelation_Id,Dict_Id,Parent_Id,Dict_Type,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM DictRelation_Detail ");
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
            builder.Append(" DictRelation_Detail_Id,DictRelation_Id,Dict_Id,Parent_Id,Dict_Type,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM DictRelation_Detail ");
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
                builder.Append("order by T.DictRelation_Detail_Id desc");
            }
            builder.Append(")AS Row, T.*  from DictRelation_Detail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_DictRelation_Detail GetModel(string DictRelation_Detail_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 DictRelation_Detail_Id,DictRelation_Id,Dict_Id,Parent_Id,Dict_Type,Remark,CreateUser,CreateTime from DictRelation_Detail ");
            builder.Append(" where DictRelation_Detail_Id=@DictRelation_Detail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Detail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = DictRelation_Detail_Id;
            new Model_DictRelation_Detail();
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
            builder.Append("select count(1) FROM DictRelation_Detail ");
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

        public bool Update(Model_DictRelation_Detail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update DictRelation_Detail set ");
            builder.Append("DictRelation_Id=@DictRelation_Id,");
            builder.Append("Dict_Id=@Dict_Id,");
            builder.Append("Parent_Id=@Parent_Id,");
            builder.Append("Dict_Type=@Dict_Type,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where DictRelation_Detail_Id=@DictRelation_Detail_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DictRelation_Id", SqlDbType.Char, 0x24), new SqlParameter("@Dict_Id", SqlDbType.Char, 0x24), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@Dict_Type", SqlDbType.Char, 0x24), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@DictRelation_Detail_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.DictRelation_Id;
            cmdParms[1].Value = model.Dict_Id;
            cmdParms[2].Value = model.Parent_Id;
            cmdParms[3].Value = model.Dict_Type;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.DictRelation_Detail_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

