namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_UserBuyResources
    {
        public bool Add(Model_UserBuyResources model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into UserBuyResources(");
            builder.Append("UserBuyResources_ID,UserId,Book_id,BookPrice,BuyType,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@UserBuyResources_ID,@UserId,@Book_id,@BookPrice,@BuyType,@CreateTime,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@Book_id", SqlDbType.Char, 0x24), new SqlParameter("@BookPrice", SqlDbType.Decimal, 5), new SqlParameter("@BuyType", SqlDbType.VarChar, 20), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserBuyResources_ID;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.Book_id;
            cmdParms[3].Value = model.BookPrice;
            cmdParms[4].Value = model.BuyType;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddUserBuyResources(List<Model_UserBuyResources> list)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            int num = 0;
            StringBuilder builder = new StringBuilder();
            foreach (Model_UserBuyResources resources in list)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0}; ", num);
                builder.Append("insert into UserBuyResources(");
                builder.Append("UserBuyResources_ID,UserId,Book_id,BookPrice,BuyType,CreateTime,CreateUser)");
                builder.Append(" values (");
                builder.Append("@UserBuyResources_ID,@UserId,@Book_id,@BookPrice,@BuyType,@CreateTime,@CreateUser)");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@Book_id", SqlDbType.Char, 0x24), new SqlParameter("@BookPrice", SqlDbType.Decimal, 5), new SqlParameter("@BuyType", SqlDbType.VarChar, 20), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray[0].Value = resources.UserBuyResources_ID;
                parameterArray[1].Value = resources.UserId;
                parameterArray[2].Value = resources.Book_id;
                parameterArray[3].Value = resources.BookPrice;
                parameterArray[4].Value = resources.BuyType;
                parameterArray[5].Value = resources.CreateTime;
                parameterArray[6].Value = resources.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_UserBuyResources DataRowToModel(DataRow row)
        {
            Model_UserBuyResources resources = new Model_UserBuyResources();
            if (row != null)
            {
                if (row["UserBuyResources_ID"] != null)
                {
                    resources.UserBuyResources_ID = row["UserBuyResources_ID"].ToString();
                }
                if (row["UserId"] != null)
                {
                    resources.UserId = row["UserId"].ToString();
                }
                if (row["Book_id"] != null)
                {
                    resources.Book_id = row["Book_id"].ToString();
                }
                if ((row["BookPrice"] != null) && (row["BookPrice"].ToString() != ""))
                {
                    resources.BookPrice = new decimal?(decimal.Parse(row["BookPrice"].ToString()));
                }
                if (row["BuyType"] != null)
                {
                    resources.BuyType = row["BuyType"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    resources.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    resources.CreateUser = row["CreateUser"].ToString();
                }
            }
            return resources;
        }

        public bool Delete(string UserBuyResources_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserBuyResources ");
            builder.Append(" where UserBuyResources_ID=@UserBuyResources_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserBuyResources_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string UserBuyResources_IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserBuyResources ");
            builder.Append(" where UserBuyResources_ID in (" + UserBuyResources_IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string UserBuyResources_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from UserBuyResources");
            builder.Append(" where UserBuyResources_ID=@UserBuyResources_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserBuyResources_ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserBuyResources_ID,UserId,Book_id,BookPrice,BuyType,CreateTime,CreateUser ");
            builder.Append(" FROM UserBuyResources ");
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
            builder.Append(" UserBuyResources_ID,UserId,Book_id,BookPrice,BuyType,CreateTime,CreateUser ");
            builder.Append(" FROM UserBuyResources ");
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
                builder.Append("order by T.UserBuyResources_ID desc");
            }
            builder.Append(")AS Row, T.*  from UserBuyResources T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_UserBuyResources GetModel(string UserBuyResources_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserBuyResources_ID,UserId,Book_id,BookPrice,BuyType,CreateTime,CreateUser from UserBuyResources ");
            builder.Append(" where UserBuyResources_ID=@UserBuyResources_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserBuyResources_ID;
            new Model_UserBuyResources();
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
            builder.Append("select count(1) FROM UserBuyResources ");
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

        public bool Update(Model_UserBuyResources model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserBuyResources set ");
            builder.Append("UserId=@UserId,");
            builder.Append("Book_id=@Book_id,");
            builder.Append("BookPrice=@BookPrice,");
            builder.Append("BuyType=@BuyType,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where UserBuyResources_ID=@UserBuyResources_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@Book_id", SqlDbType.Char, 0x24), new SqlParameter("@BookPrice", SqlDbType.Decimal, 5), new SqlParameter("@BuyType", SqlDbType.VarChar, 20), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.Book_id;
            cmdParms[2].Value = model.BookPrice;
            cmdParms[3].Value = model.BuyType;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.UserBuyResources_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

