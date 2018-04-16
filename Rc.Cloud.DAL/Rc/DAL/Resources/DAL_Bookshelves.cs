namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Bookshelves
    {
        public bool Add(Model_Bookshelves model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Bookshelves(");
            builder.Append("ResourceFolder_Id,Book_Name,BookImg_Url,BookPrice,BookShelvesState,PutUpTime,PutDownTime,CreateUser,CreateTime,BookBrief)");
            builder.Append(" values (");
            builder.Append("@ResourceFolder_Id,@Book_Name,@BookImg_Url,@BookPrice,@BookShelvesState,@PutUpTime,@PutDownTime,@CreateUser,@CreateTime,@BookBrief)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@BookImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@BookPrice", SqlDbType.Decimal, 5), new SqlParameter("@BookShelvesState", SqlDbType.Char, 1), new SqlParameter("@PutUpTime", SqlDbType.DateTime), new SqlParameter("@PutDownTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@BookBrief", SqlDbType.Text) };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.Book_Name;
            cmdParms[2].Value = model.BookImg_Url;
            cmdParms[3].Value = model.BookPrice;
            cmdParms[4].Value = model.BookShelvesState;
            cmdParms[5].Value = model.PutUpTime;
            cmdParms[6].Value = model.PutDownTime;
            cmdParms[7].Value = model.CreateUser;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.BookBrief;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_Bookshelves DataRowToModel(DataRow row)
        {
            Model_Bookshelves bookshelves = new Model_Bookshelves();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    bookshelves.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["Book_Name"] != null)
                {
                    bookshelves.Book_Name = row["Book_Name"].ToString();
                }
                if (row["BookImg_Url"] != null)
                {
                    bookshelves.BookImg_Url = row["BookImg_Url"].ToString();
                }
                if ((row["BookPrice"] != null) && (row["BookPrice"].ToString() != ""))
                {
                    bookshelves.BookPrice = new decimal?(decimal.Parse(row["BookPrice"].ToString()));
                }
                if (row["BookShelvesState"] != null)
                {
                    bookshelves.BookShelvesState = row["BookShelvesState"].ToString();
                }
                if ((row["PutUpTime"] != null) && (row["PutUpTime"].ToString() != ""))
                {
                    bookshelves.PutUpTime = new DateTime?(DateTime.Parse(row["PutUpTime"].ToString()));
                }
                if ((row["PutDownTime"] != null) && (row["PutDownTime"].ToString() != ""))
                {
                    bookshelves.PutDownTime = new DateTime?(DateTime.Parse(row["PutDownTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    bookshelves.CreateUser = row["CreateUser"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    bookshelves.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["BookBrief"] != null)
                {
                    bookshelves.BookBrief = row["BookBrief"].ToString();
                }
            }
            return bookshelves;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Bookshelves ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Bookshelves ");
            builder.Append(" where ResourceFolder_Id in (" + ResourceFolder_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Bookshelves");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceFolder_Id,Book_Name,BookImg_Url,BookPrice,BookShelvesState,PutUpTime,PutDownTime,CreateUser,CreateTime,BookBrief ");
            builder.Append(" FROM Bookshelves ");
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
            builder.Append(" ResourceFolder_Id,Book_Name,BookImg_Url,BookPrice,BookShelvesState,PutUpTime,PutDownTime,CreateUser,CreateTime,BookBrief ");
            builder.Append(" FROM Bookshelves ");
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
                builder.Append("order by T.ResourceFolder_Id desc");
            }
            builder.Append(")AS Row, T.*  from Bookshelves T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Bookshelves GetModel(string ResourceFolder_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceFolder_Id,Book_Name,BookImg_Url,BookPrice,BookShelvesState,PutUpTime,PutDownTime,CreateUser,CreateTime,BookBrief from Bookshelves ");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceFolder_Id;
            new Model_Bookshelves();
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
            builder.Append("select count(1) FROM Bookshelves ");
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

        public bool Update(Model_Bookshelves model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Bookshelves set ");
            builder.Append("Book_Name=@Book_Name,");
            builder.Append("BookImg_Url=@BookImg_Url,");
            builder.Append("BookPrice=@BookPrice,");
            builder.Append("BookShelvesState=@BookShelvesState,");
            builder.Append("PutUpTime=@PutUpTime,");
            builder.Append("PutDownTime=@PutDownTime,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("BookBrief=@BookBrief");
            builder.Append(" where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@BookImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@BookPrice", SqlDbType.Decimal, 5), new SqlParameter("@BookShelvesState", SqlDbType.Char, 1), new SqlParameter("@PutUpTime", SqlDbType.DateTime), new SqlParameter("@PutDownTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@BookBrief", SqlDbType.Text), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Book_Name;
            cmdParms[1].Value = model.BookImg_Url;
            cmdParms[2].Value = model.BookPrice;
            cmdParms[3].Value = model.BookShelvesState;
            cmdParms[4].Value = model.PutUpTime;
            cmdParms[5].Value = model.PutDownTime;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.CreateTime;
            cmdParms[8].Value = model.BookBrief;
            cmdParms[9].Value = model.ResourceFolder_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

