namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Two_WayChecklistToTestpaper
    {
        public bool Add(Model_Two_WayChecklistToTestpaper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklistToTestpaper(");
            builder.Append("Two_WayChecklistToTestpaper_Id,Two_WayChecklist_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklistToTestpaper_Id,@Two_WayChecklist_Id,@ResourceToResourceFolder_Id,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTestpaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Two_WayChecklistToTestpaper_Id;
            cmdParms[1].Value = model.Two_WayChecklist_Id;
            cmdParms[2].Value = model.ResourceToResourceFolder_Id;
            cmdParms[3].Value = model.CreateUser;
            cmdParms[4].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int AddRelationPaper(Model_Two_WayChecklistToTestpaper model, List<Model_Two_WayChecklistDetailToTestQuestions> listModel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into Two_WayChecklistToTestpaper(");
            builder.Append("Two_WayChecklistToTestpaper_Id,Two_WayChecklist_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@Two_WayChecklistToTestpaper_Id,@Two_WayChecklist_Id,@ResourceToResourceFolder_Id,@CreateUser,@CreateTime)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTestpaper_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            parameterArray[0].Value = model.Two_WayChecklistToTestpaper_Id;
            parameterArray[1].Value = model.Two_WayChecklist_Id;
            parameterArray[2].Value = model.ResourceToResourceFolder_Id;
            parameterArray[3].Value = model.CreateUser;
            parameterArray[4].Value = model.CreateTime;
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_Two_WayChecklistDetailToTestQuestions questions in listModel)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into Two_WayChecklistDetailToTestQuestions(");
                builder.Append("Two_WayChecklistDetailToTestQuestions_Id,Two_WayChecklist_Id,Two_WayChecklistDetail_Id,ResourceToResourceFolder_Id,TestQuestions_Id,CreateUser,CreateTime)");
                builder.Append(" values (");
                builder.Append("@Two_WayChecklistDetailToTestQuestions_Id,@Two_WayChecklist_Id,@Two_WayChecklistDetail_Id,@ResourceToResourceFolder_Id,@TestQuestions_Id,@CreateUser,@CreateTime)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Two_WayChecklistDetailToTestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@Two_WayChecklistDetail_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray2[0].Value = questions.Two_WayChecklistDetailToTestQuestions_Id;
                parameterArray2[1].Value = questions.Two_WayChecklist_Id;
                parameterArray2[2].Value = questions.Two_WayChecklistDetail_Id;
                parameterArray2[3].Value = questions.ResourceToResourceFolder_Id;
                parameterArray2[4].Value = questions.TestQuestions_Id;
                parameterArray2[5].Value = questions.CreateUser;
                parameterArray2[6].Value = questions.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            int num2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (num2 > 0)
            {
                return num2;
            }
            return 0;
        }

        public Model_Two_WayChecklistToTestpaper DataRowToModel(DataRow row)
        {
            Model_Two_WayChecklistToTestpaper testpaper = new Model_Two_WayChecklistToTestpaper();
            if (row != null)
            {
                if (row["Two_WayChecklistToTestpaper_Id"] != null)
                {
                    testpaper.Two_WayChecklistToTestpaper_Id = row["Two_WayChecklistToTestpaper_Id"].ToString();
                }
                if (row["Two_WayChecklist_Id"] != null)
                {
                    testpaper.Two_WayChecklist_Id = row["Two_WayChecklist_Id"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    testpaper.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["CreateUser"] != null)
                {
                    testpaper.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    testpaper.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return testpaper;
        }

        public bool Delete(string Two_WayChecklistToTestpaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistToTestpaper ");
            builder.Append(" where Two_WayChecklistToTestpaper_Id=@Two_WayChecklistToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistToTestpaper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Two_WayChecklistToTestpaper_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Two_WayChecklistToTestpaper ");
            builder.Append(" where Two_WayChecklistToTestpaper_Id in (" + Two_WayChecklistToTestpaper_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string Two_WayChecklistToTestpaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Two_WayChecklistToTestpaper");
            builder.Append(" where Two_WayChecklistToTestpaper_Id=@Two_WayChecklistToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistToTestpaper_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Two_WayChecklistToTestpaper_Id,Two_WayChecklist_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistToTestpaper ");
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
            builder.Append(" Two_WayChecklistToTestpaper_Id,Two_WayChecklist_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime ");
            builder.Append(" FROM Two_WayChecklistToTestpaper ");
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
                builder.Append("order by T.Two_WayChecklistToTestpaper_Id desc");
            }
            builder.Append(")AS Row, T.*  from Two_WayChecklistToTestpaper T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Two_WayChecklistToTestpaper GetModel(string Two_WayChecklistToTestpaper_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Two_WayChecklistToTestpaper_Id,Two_WayChecklist_Id,ResourceToResourceFolder_Id,CreateUser,CreateTime from Two_WayChecklistToTestpaper ");
            builder.Append(" where Two_WayChecklistToTestpaper_Id=@Two_WayChecklistToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklistToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Two_WayChecklistToTestpaper_Id;
            new Model_Two_WayChecklistToTestpaper();
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
            builder.Append("select count(1) FROM Two_WayChecklistToTestpaper ");
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

        public int GetRecordCount_Operate(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Two_WayChecklistToTestpaper ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL_Operate.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_Two_WayChecklistToTestpaper model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Two_WayChecklistToTestpaper set ");
            builder.Append("Two_WayChecklist_Id=@Two_WayChecklist_Id,");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where Two_WayChecklistToTestpaper_Id=@Two_WayChecklistToTestpaper_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Two_WayChecklist_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Two_WayChecklistToTestpaper_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Two_WayChecklist_Id;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.CreateUser;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.Two_WayChecklistToTestpaper_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

