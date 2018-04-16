namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_HomeworkToTQ
    {
        public bool Add(Model_HomeworkToTQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HomeworkToTQ(");
            builder.Append("HomeworkToTQ_Id,HomeWork_Id,ResourceToResourceFolder_Id,TestQuestions_Id,topicNumber,UserGroup_Id,rtrfId_Old,Sort,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@HomeworkToTQ_Id,@HomeWork_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@topicNumber,@UserGroup_Id,@rtrfId_Old,@Sort,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeworkToTQ_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24), new SqlParameter("@Sort", SqlDbType.Int, 4), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.HomeworkToTQ_Id;
            cmdParms[1].Value = model.HomeWork_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.TestQuestions_Id;
            cmdParms[4].Value = model.topicNumber;
            cmdParms[5].Value = model.UserGroup_Id;
            cmdParms[6].Value = model.rtrfId_Old;
            cmdParms[7].Value = model.Sort;
            cmdParms[8].Value = model.CreateUser;
            cmdParms[9].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddMultiData(List<Model_HomeworkToTQ> listMHTTQ)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (Model_HomeworkToTQ otq in listMHTTQ)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0} ;", num);
                builder.Append("insert into HomeworkToTQ(");
                builder.Append("HomeworkToTQ_Id,HomeWork_Id,ResourceToResourceFolder_Id,TestQuestions_Id,topicNumber,UserGroup_Id,rtrfId_Old,Sort,CreateUser,CreateTime)");
                builder.Append(" values (");
                builder.Append("@HomeworkToTQ_Id,@HomeWork_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@topicNumber,@UserGroup_Id,@rtrfId_Old,@Sort,@CreateUser,@CreateTime)");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@HomeworkToTQ_Id", SqlDbType.Char, 0x24), new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24), new SqlParameter("@Sort", SqlDbType.Int, 4), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray[0].Value = otq.HomeworkToTQ_Id;
                parameterArray[1].Value = otq.HomeWork_Id;
                parameterArray[2].Value = otq.ResourceToResourceFolder_Id;
                parameterArray[3].Value = otq.TestQuestions_Id;
                parameterArray[4].Value = otq.topicNumber;
                parameterArray[5].Value = otq.UserGroup_Id;
                parameterArray[6].Value = otq.rtrfId_Old;
                parameterArray[7].Value = otq.Sort;
                parameterArray[8].Value = otq.CreateUser;
                parameterArray[9].Value = otq.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_HomeworkToTQ DataRowToModel(DataRow row)
        {
            Model_HomeworkToTQ otq = new Model_HomeworkToTQ();
            if (row != null)
            {
                if (row["HomeworkToTQ_Id"] != null)
                {
                    otq.HomeworkToTQ_Id = row["HomeworkToTQ_Id"].ToString();
                }
                if (row["HomeWork_Id"] != null)
                {
                    otq.HomeWork_Id = row["HomeWork_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    otq.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["TestQuestions_Id"] != null)
                {
                    otq.TestQuestions_Id = row["TestQuestions_Id"].ToString();
                }
                if (row["topicNumber"] != null)
                {
                    otq.topicNumber = row["topicNumber"].ToString();
                }
                if (row["UserGroup_Id"] != null)
                {
                    otq.UserGroup_Id = row["UserGroup_Id"].ToString();
                }
                if (row["rtrfId_Old"] != null)
                {
                    otq.rtrfId_Old = row["rtrfId_Old"].ToString();
                }
                if ((row["Sort"] != null) && (row["Sort"].ToString() != ""))
                {
                    otq.Sort = new int?(int.Parse(row["Sort"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    otq.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    otq.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return otq;
        }

        public bool Delete(string HomeworkToTQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HomeworkToTQ ");
            builder.Append(" where HomeworkToTQ_Id=@HomeworkToTQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeworkToTQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeworkToTQ_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string HomeworkToTQ_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from HomeworkToTQ ");
            builder.Append(" where HomeworkToTQ_Id in (" + HomeworkToTQ_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string HomeworkToTQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from HomeworkToTQ");
            builder.Append(" where HomeworkToTQ_Id=@HomeworkToTQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeworkToTQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeworkToTQ_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select HomeworkToTQ_Id,HomeWork_Id,ResourceToResourceFolder_Id,TestQuestions_Id,topicNumber,UserGroup_Id,rtrfId_Old,Sort,CreateUser,CreateTime ");
            builder.Append(" FROM HomeworkToTQ ");
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
            builder.Append(" HomeworkToTQ_Id,HomeWork_Id,ResourceToResourceFolder_Id,TestQuestions_Id,topicNumber,UserGroup_Id,rtrfId_Old,Sort,CreateUser,CreateTime ");
            builder.Append(" FROM HomeworkToTQ ");
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
                builder.Append("order by T.HomeworkToTQ_Id desc");
            }
            builder.Append(")AS Row, T.*  from HomeworkToTQ T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_HomeworkToTQ GetModel(string HomeworkToTQ_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 HomeworkToTQ_Id,HomeWork_Id,ResourceToResourceFolder_Id,TestQuestions_Id,topicNumber,UserGroup_Id,rtrfId_Old,Sort,CreateUser,CreateTime from HomeworkToTQ ");
            builder.Append(" where HomeworkToTQ_Id=@HomeworkToTQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeworkToTQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = HomeworkToTQ_Id;
            new Model_HomeworkToTQ();
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
            builder.Append("select count(1) FROM HomeworkToTQ ");
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

        public bool Update(Model_HomeworkToTQ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HomeworkToTQ set ");
            builder.Append("HomeWork_Id=@HomeWork_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("TestQuestions_Id=@TestQuestions_Id,");
            builder.Append("topicNumber=@topicNumber,");
            builder.Append("UserGroup_Id=@UserGroup_Id,");
            builder.Append("rtrfId_Old=@rtrfId_Old,");
            builder.Append("Sort=@Sort,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where HomeworkToTQ_Id=@HomeworkToTQ_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@HomeWork_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@UserGroup_Id", SqlDbType.VarChar, 10), new SqlParameter("@rtrfId_Old", SqlDbType.Char, 0x24), new SqlParameter("@Sort", SqlDbType.Int, 4), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@HomeworkToTQ_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.HomeWork_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.TestQuestions_Id;
            cmdParms[3].Value = model.topicNumber;
            cmdParms[4].Value = model.UserGroup_Id;
            cmdParms[5].Value = model.rtrfId_Old;
            cmdParms[6].Value = model.Sort;
            cmdParms[7].Value = model.CreateUser;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.HomeworkToTQ_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

