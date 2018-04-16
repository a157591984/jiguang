namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_BookArea
    {
        public bool Add(Model_BookArea model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into BookArea(");
            builder.Append("BookArea_ID,ResourceFolder_Id,Book_Name,Province_ID,City_ID,County_ID,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@BookArea_ID,@ResourceFolder_Id,@Book_Name,@Province_ID,@City_ID,@County_ID,@CreateTime,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookArea_ID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@Province_ID", SqlDbType.Char, 0x24), new SqlParameter("@City_ID", SqlDbType.Char, 0x24), new SqlParameter("@County_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.BookArea_ID;
            cmdParms[1].Value = model.ResourceFolder_Id;
            cmdParms[2].Value = model.Book_Name;
            cmdParms[3].Value = model.Province_ID;
            cmdParms[4].Value = model.City_ID;
            cmdParms[5].Value = model.County_ID;
            cmdParms[6].Value = model.CreateTime;
            cmdParms[7].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int AddModelList(string ResourceFolder_Id, List<Model_BookArea> list)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookArea where ResourceFolder_Id=@ResourceFolder_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", ResourceFolder_Id) };
            dictionary.Add(builder.ToString(), parameterArray);
            int num = 0;
            foreach (Model_BookArea area in list)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into BookArea(");
                builder.Append("BookArea_ID,ResourceFolder_Id,Book_Name,Province_ID,City_ID,County_ID,CreateTime,CreateUser)");
                builder.Append(" values (");
                builder.Append("@BookArea_ID,@ResourceFolder_Id,@Book_Name,@Province_ID,@City_ID,@County_ID,@CreateTime,@CreateUser)");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@BookArea_ID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@Province_ID", SqlDbType.Char, 0x24), new SqlParameter("@City_ID", SqlDbType.Char, 0x24), new SqlParameter("@County_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
                parameterArray2[0].Value = area.BookArea_ID;
                parameterArray2[1].Value = area.ResourceFolder_Id;
                parameterArray2[2].Value = area.Book_Name;
                parameterArray2[3].Value = area.Province_ID;
                parameterArray2[4].Value = area.City_ID;
                parameterArray2[5].Value = area.County_ID;
                parameterArray2[6].Value = area.CreateTime;
                parameterArray2[7].Value = area.CreateUser;
                dictionary.Add(builder.ToString(), parameterArray2);
            }
            object obj2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public Model_BookArea DataRowToModel(DataRow row)
        {
            Model_BookArea area = new Model_BookArea();
            if (row != null)
            {
                if (row["BookArea_ID"] != null)
                {
                    area.BookArea_ID = row["BookArea_ID"].ToString();
                }
                if (row["ResourceFolder_Id"] != null)
                {
                    area.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["Book_Name"] != null)
                {
                    area.Book_Name = row["Book_Name"].ToString();
                }
                if (row["Province_ID"] != null)
                {
                    area.Province_ID = row["Province_ID"].ToString();
                }
                if (row["City_ID"] != null)
                {
                    area.City_ID = row["City_ID"].ToString();
                }
                if (row["County_ID"] != null)
                {
                    area.County_ID = row["County_ID"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    area.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    area.CreateUser = row["CreateUser"].ToString();
                }
            }
            return area;
        }

        public bool Delete(string BookArea_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookArea ");
            builder.Append(" where BookArea_ID=@BookArea_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookArea_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookArea_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BookArea_IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookArea ");
            builder.Append(" where BookArea_ID in (" + BookArea_IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string BookArea_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from BookArea");
            builder.Append(" where BookArea_ID=@BookArea_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookArea_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookArea_ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BookArea_ID,ResourceFolder_Id,Book_Name,Province_ID,City_ID,County_ID,CreateTime,CreateUser ");
            builder.Append(" FROM BookArea ");
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
            builder.Append(" BookArea_ID,ResourceFolder_Id,Book_Name,Province_ID,City_ID,County_ID,CreateTime,CreateUser ");
            builder.Append(" FROM BookArea ");
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
                builder.Append("order by T.BookArea_ID desc");
            }
            builder.Append(")AS Row, T.*  from BookArea T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_BookArea GetModel(string BookArea_ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BookArea_ID,ResourceFolder_Id,Book_Name,Province_ID,City_ID,County_ID,CreateTime,CreateUser from BookArea ");
            builder.Append(" where BookArea_ID=@BookArea_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookArea_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookArea_ID;
            new Model_BookArea();
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
            builder.Append("select count(1) FROM BookArea ");
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

        public bool Update(Model_BookArea model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update BookArea set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("Book_Name=@Book_Name,");
            builder.Append("Province_ID=@Province_ID,");
            builder.Append("City_ID=@City_ID,");
            builder.Append("County_ID=@County_ID,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where BookArea_ID=@BookArea_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Book_Name", SqlDbType.NVarChar, 100), new SqlParameter("@Province_ID", SqlDbType.Char, 0x24), new SqlParameter("@City_ID", SqlDbType.Char, 0x24), new SqlParameter("@County_ID", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@BookArea_ID", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceFolder_Id;
            cmdParms[1].Value = model.Book_Name;
            cmdParms[2].Value = model.Province_ID;
            cmdParms[3].Value = model.City_ID;
            cmdParms[4].Value = model.County_ID;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.CreateUser;
            cmdParms[7].Value = model.BookArea_ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

