namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_UserOrder
    {
        public bool Add(Model_UserOrder model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into UserOrder(");
            builder.Append("UserOrder_Id,UserId,UserOrder_No,UserOrder_Time,UserOrder_Paytool,UsreOrder_Buyeremail,UserOrder_Remark,UserOrder_Type,UserOrder_Amount,UserOrder_Status,UserOrder_FinishTime,Book_Id,Book_Name,Book_Price,BookImg_Url,trade_no,trade_status)");
            builder.Append(" values (");
            builder.Append("@UserOrder_Id,@UserId,@UserOrder_No,@UserOrder_Time,@UserOrder_Paytool,@UsreOrder_Buyeremail,@UserOrder_Remark,@UserOrder_Type,@UserOrder_Amount,@UserOrder_Status,@UserOrder_FinishTime,@Book_Id,@Book_Name,@Book_Price,@BookImg_Url,@trade_no,@trade_status)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserOrder_Id", SqlDbType.Char, 0x24), new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@UserOrder_No", SqlDbType.VarChar, 50), new SqlParameter("@UserOrder_Time", SqlDbType.DateTime), new SqlParameter("@UserOrder_Paytool", SqlDbType.VarChar, 50), new SqlParameter("@UsreOrder_Buyeremail", SqlDbType.VarChar, 100), new SqlParameter("@UserOrder_Remark", SqlDbType.VarChar, 500), new SqlParameter("@UserOrder_Type", SqlDbType.VarChar, 10), new SqlParameter("@UserOrder_Amount", SqlDbType.Decimal, 9), new SqlParameter("@UserOrder_Status", SqlDbType.Int, 4), new SqlParameter("@UserOrder_FinishTime", SqlDbType.DateTime), new SqlParameter("@Book_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@Book_Price", SqlDbType.Decimal, 9), new SqlParameter("@BookImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@trade_no", SqlDbType.NVarChar, 100), 
                new SqlParameter("@trade_status", SqlDbType.NVarChar, 50)
             };
            cmdParms[0].Value = model.UserOrder_Id;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.UserOrder_No;
            cmdParms[3].Value = model.UserOrder_Time;
            cmdParms[4].Value = model.UserOrder_Paytool;
            cmdParms[5].Value = model.UsreOrder_Buyeremail;
            cmdParms[6].Value = model.UserOrder_Remark;
            cmdParms[7].Value = model.UserOrder_Type;
            cmdParms[8].Value = model.UserOrder_Amount;
            cmdParms[9].Value = model.UserOrder_Status;
            cmdParms[10].Value = model.UserOrder_FinishTime;
            cmdParms[11].Value = model.Book_Id;
            cmdParms[12].Value = model.Book_Name;
            cmdParms[13].Value = model.Book_Price;
            cmdParms[14].Value = model.BookImg_Url;
            cmdParms[15].Value = model.trade_no;
            cmdParms[0x10].Value = model.trade_status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_UserOrder DataRowToModel(DataRow row)
        {
            Model_UserOrder order = new Model_UserOrder();
            if (row != null)
            {
                if (row["UserOrder_Id"] != null)
                {
                    order.UserOrder_Id = row["UserOrder_Id"].ToString();
                }
                if (row["UserId"] != null)
                {
                    order.UserId = row["UserId"].ToString();
                }
                if (row["UserOrder_No"] != null)
                {
                    order.UserOrder_No = row["UserOrder_No"].ToString();
                }
                if ((row["UserOrder_Time"] != null) && (row["UserOrder_Time"].ToString() != ""))
                {
                    order.UserOrder_Time = DateTime.Parse(row["UserOrder_Time"].ToString());
                }
                if (row["UserOrder_Paytool"] != null)
                {
                    order.UserOrder_Paytool = row["UserOrder_Paytool"].ToString();
                }
                if (row["UsreOrder_Buyeremail"] != null)
                {
                    order.UsreOrder_Buyeremail = row["UsreOrder_Buyeremail"].ToString();
                }
                if (row["UserOrder_Remark"] != null)
                {
                    order.UserOrder_Remark = row["UserOrder_Remark"].ToString();
                }
                if (row["UserOrder_Type"] != null)
                {
                    order.UserOrder_Type = row["UserOrder_Type"].ToString();
                }
                if ((row["UserOrder_Amount"] != null) && (row["UserOrder_Amount"].ToString() != ""))
                {
                    order.UserOrder_Amount = decimal.Parse(row["UserOrder_Amount"].ToString());
                }
                if ((row["UserOrder_Status"] != null) && (row["UserOrder_Status"].ToString() != ""))
                {
                    order.UserOrder_Status = int.Parse(row["UserOrder_Status"].ToString());
                }
                if ((row["UserOrder_FinishTime"] != null) && (row["UserOrder_FinishTime"].ToString() != ""))
                {
                    order.UserOrder_FinishTime = DateTime.Parse(row["UserOrder_FinishTime"].ToString());
                }
                if (row["Book_Id"] != null)
                {
                    order.Book_Id = row["Book_Id"].ToString();
                }
                if (row["Book_Name"] != null)
                {
                    order.Book_Name = row["Book_Name"].ToString();
                }
                if ((row["Book_Price"] != null) && (row["Book_Price"].ToString() != ""))
                {
                    order.Book_Price = decimal.Parse(row["Book_Price"].ToString());
                }
                if (row["BookImg_Url"] != null)
                {
                    order.BookImg_Url = row["BookImg_Url"].ToString();
                }
                if (row["trade_no"] != null)
                {
                    order.trade_no = row["trade_no"].ToString();
                }
                if (row["trade_status"] != null)
                {
                    order.trade_status = row["trade_status"].ToString();
                }
            }
            return order;
        }

        public bool Delete(string UserOrder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserOrder ");
            builder.Append(" where UserOrder_Id=@UserOrder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserOrder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserOrder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string UserOrder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from UserOrder ");
            builder.Append(" where UserOrder_Id in (" + UserOrder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string UserOrder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from UserOrder");
            builder.Append(" where UserOrder_Id=@UserOrder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserOrder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserOrder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserOrder_Id,UserId,UserOrder_No,UserOrder_Time,UserOrder_Paytool,UsreOrder_Buyeremail,UserOrder_Remark,UserOrder_Type,UserOrder_Amount,UserOrder_Status,UserOrder_FinishTime,Book_Id,Book_Name,Book_Price,BookImg_Url,trade_no,trade_status ");
            builder.Append(" FROM UserOrder ");
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
            builder.Append(" UserOrder_Id,UserId,UserOrder_No,UserOrder_Time,UserOrder_Paytool,UsreOrder_Buyeremail,UserOrder_Remark,UserOrder_Type,UserOrder_Amount,UserOrder_Status,UserOrder_FinishTime,Book_Id,Book_Name,Book_Price,BookImg_Url,trade_no,trade_status ");
            builder.Append(" FROM UserOrder ");
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
                builder.Append("order by T.UserOrder_Id desc");
            }
            builder.Append(")AS Row, T.*  from UserOrder T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageALLOrderList(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserOrder_Time desc");
            }
            builder.Append(")AS Row,a.* from(\r\nselect o.UserOrder_Id,o.UserId,o.UserOrder_No,o.UserOrder_FinishTime,UserOrder_Amount,o.UserOrder_Status,o.UserOrder_Type\r\n ,o.Book_Id,o.Book_Name,o.Book_Price,CommentCount=(select COUNT(*) from UserOrder_Comment where order_num=o.UserOrder_No),ro.Resource_Type,NULL as BuyType from dbo.UserOrder o\r\n inner join ResourceFolder ro on ro.ResourceFolder_Id=o.Book_Id\r\n union \r\n select r.UserBuyResources_ID,r.UserId,NULL,r.CreateTime ,0,NULL,r.BuyType,r.Book_id,rf.Book_Name,rf.BookPrice,0,ro.Resource_Type,r.BuyType  from UserBuyResources r\r\n left join Bookshelves rf on rf.ResourceFolder_Id=r.Book_id\r\n inner join ResourceFolder ro on ro.ResourceFolder_Id=r.Book_id\r\n where r.BuyType in('NBSQ','FREE')\r\n ) a");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(") TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageOrderList(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by UserOrder_Time desc");
            }
            builder.Append(")AS Row, o.UserOrder_Id,o.UserId,o.UserOrder_No,o.UserOrder_FinishTime,UserOrder_Amount,o.UserOrder_Status\r\n ,o.Book_Id,o.Book_Name,o.Book_Price,CommentCount=(select COUNT(*) from UserOrder_Comment where order_num=o.UserOrder_No) from dbo.UserOrder o ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(") TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_UserOrder GetModel(string UserOrder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserOrder_Id,UserId,UserOrder_No,UserOrder_Time,UserOrder_Paytool,UsreOrder_Buyeremail,UserOrder_Remark,UserOrder_Type,UserOrder_Amount,UserOrder_Status,UserOrder_FinishTime,Book_Id,Book_Name,Book_Price,BookImg_Url,trade_no,trade_status from UserOrder ");
            builder.Append(" where UserOrder_Id=@UserOrder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserOrder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = UserOrder_Id;
            new Model_UserOrder();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_UserOrder GetModelByOrderNo(string UserOrder_No)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UserOrder_Id,UserId,UserOrder_No,UserOrder_Time,UserOrder_Paytool,UsreOrder_Buyeremail,UserOrder_Remark,UserOrder_Type,UserOrder_Amount,UserOrder_Status,UserOrder_FinishTime,Book_Id,Book_Name,Book_Price,BookImg_Url,trade_no,trade_status from UserOrder ");
            builder.Append(" where UserOrder_No=@UserOrder_No ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserOrder_No", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = UserOrder_No;
            new Model_UserOrder();
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
            builder.Append("select count(1) FROM UserOrder ");
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

        public int GetRecordCountALL(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) FROM  (\r\nselect o.UserOrder_Id,o.UserId,o.UserOrder_No,o.UserOrder_FinishTime,UserOrder_Amount,o.UserOrder_Status,o.UserOrder_Type\r\n ,o.Book_Id,o.Book_Name,o.Book_Price,CommentCount=(select COUNT(*) from UserOrder_Comment where order_num=o.UserOrder_No),ro.Resource_Type from dbo.UserOrder o\r\n inner join ResourceFolder ro on ro.ResourceFolder_Id=o.Book_Id\r\n union \r\n select r.UserBuyResources_ID,r.UserId,NULL,r.CreateTime ,0,NULL,r.BuyType,r.Book_id,rf.Book_Name,rf.BookPrice,0,ro.Resource_Type  from UserBuyResources r\r\n left join Bookshelves rf on rf.ResourceFolder_Id=r.Book_id\r\n inner join ResourceFolder ro on ro.ResourceFolder_Id=r.Book_id\r\n where r.BuyType='NBSQ'\r\n ) a");
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

        public bool Update(Model_UserOrder model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update UserOrder set ");
            builder.Append("UserId=@UserId,");
            builder.Append("UserOrder_No=@UserOrder_No,");
            builder.Append("UserOrder_Time=@UserOrder_Time,");
            builder.Append("UserOrder_Paytool=@UserOrder_Paytool,");
            builder.Append("UsreOrder_Buyeremail=@UsreOrder_Buyeremail,");
            builder.Append("UserOrder_Remark=@UserOrder_Remark,");
            builder.Append("UserOrder_Type=@UserOrder_Type,");
            builder.Append("UserOrder_Amount=@UserOrder_Amount,");
            builder.Append("UserOrder_Status=@UserOrder_Status,");
            builder.Append("UserOrder_FinishTime=@UserOrder_FinishTime,");
            builder.Append("Book_Id=@Book_Id,");
            builder.Append("Book_Name=@Book_Name,");
            builder.Append("Book_Price=@Book_Price,");
            builder.Append("BookImg_Url=@BookImg_Url,");
            builder.Append("trade_no=@trade_no,");
            builder.Append("trade_status=@trade_status");
            builder.Append(" where UserOrder_Id=@UserOrder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@UserOrder_No", SqlDbType.VarChar, 50), new SqlParameter("@UserOrder_Time", SqlDbType.DateTime), new SqlParameter("@UserOrder_Paytool", SqlDbType.VarChar, 50), new SqlParameter("@UsreOrder_Buyeremail", SqlDbType.VarChar, 100), new SqlParameter("@UserOrder_Remark", SqlDbType.VarChar, 500), new SqlParameter("@UserOrder_Type", SqlDbType.VarChar, 10), new SqlParameter("@UserOrder_Amount", SqlDbType.Decimal, 9), new SqlParameter("@UserOrder_Status", SqlDbType.Int, 4), new SqlParameter("@UserOrder_FinishTime", SqlDbType.DateTime), new SqlParameter("@Book_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@Book_Price", SqlDbType.Decimal, 9), new SqlParameter("@BookImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@trade_no", SqlDbType.NVarChar, 100), new SqlParameter("@trade_status", SqlDbType.NVarChar, 50), 
                new SqlParameter("@UserOrder_Id", SqlDbType.Char, 0x24)
             };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.UserOrder_No;
            cmdParms[2].Value = model.UserOrder_Time;
            cmdParms[3].Value = model.UserOrder_Paytool;
            cmdParms[4].Value = model.UsreOrder_Buyeremail;
            cmdParms[5].Value = model.UserOrder_Remark;
            cmdParms[6].Value = model.UserOrder_Type;
            cmdParms[7].Value = model.UserOrder_Amount;
            cmdParms[8].Value = model.UserOrder_Status;
            cmdParms[9].Value = model.UserOrder_FinishTime;
            cmdParms[10].Value = model.Book_Id;
            cmdParms[11].Value = model.Book_Name;
            cmdParms[12].Value = model.Book_Price;
            cmdParms[13].Value = model.BookImg_Url;
            cmdParms[14].Value = model.trade_no;
            cmdParms[15].Value = model.trade_status;
            cmdParms[0x10].Value = model.UserOrder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateAndAddUserBuyResources(Model_UserOrder modelUO, Model_UserBuyResources buyModel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("update UserOrder set ");
            builder.Append("UserOrder_Paytool=@UserOrder_Paytool,");
            builder.Append("UsreOrder_Buyeremail=@UsreOrder_Buyeremail,");
            builder.Append("UserOrder_Status=@UserOrder_Status,");
            builder.Append("UserOrder_FinishTime=@UserOrder_FinishTime,");
            builder.Append("trade_no=@trade_no,");
            builder.Append("trade_status=@trade_status");
            builder.Append(" where UserOrder_Id=@UserOrder_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@UserOrder_Paytool", SqlDbType.VarChar, 50), new SqlParameter("@UsreOrder_Buyeremail", SqlDbType.VarChar, 100), new SqlParameter("@UserOrder_Status", SqlDbType.Int, 4), new SqlParameter("@UserOrder_FinishTime", SqlDbType.DateTime), new SqlParameter("@trade_no", SqlDbType.NVarChar, 100), new SqlParameter("@trade_status", SqlDbType.NVarChar, 50), new SqlParameter("@UserOrder_Id", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = modelUO.UserOrder_Paytool;
            parameterArray[1].Value = modelUO.UsreOrder_Buyeremail;
            parameterArray[2].Value = modelUO.UserOrder_Status;
            parameterArray[3].Value = modelUO.UserOrder_FinishTime;
            parameterArray[4].Value = modelUO.trade_no;
            parameterArray[5].Value = modelUO.trade_status;
            parameterArray[6].Value = modelUO.UserOrder_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into UserBuyResources(");
            builder.Append("UserBuyResources_ID,UserId,Book_id,BookPrice,BuyType,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@UserBuyResources_ID,@UserId,@Book_id,@BookPrice,@BuyType,@CreateTime,@CreateUser)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@UserBuyResources_ID", SqlDbType.Char, 0x24), new SqlParameter("@UserId", SqlDbType.Char, 0x24), new SqlParameter("@Book_id", SqlDbType.Char, 0x24), new SqlParameter("@BookPrice", SqlDbType.Decimal, 5), new SqlParameter("@BuyType", SqlDbType.VarChar, 20), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            parameterArray2[0].Value = buyModel.UserBuyResources_ID;
            parameterArray2[1].Value = buyModel.UserId;
            parameterArray2[2].Value = buyModel.Book_id;
            parameterArray2[3].Value = buyModel.BookPrice;
            parameterArray2[4].Value = buyModel.BuyType;
            parameterArray2[5].Value = buyModel.CreateTime;
            parameterArray2[6].Value = buyModel.CreateUser;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }
    }
}

