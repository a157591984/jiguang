namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_BookAttr_School
    {
        public bool Add(Model_BookAttr_School model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into BookAttr_School(");
            builder.Append("BookAttr_School_Id,SchoolId,AttrEnum,AttrValue,Remark,CreateUser,CreateTime)");
            builder.Append(" values (");
            builder.Append("@BookAttr_School_Id,@SchoolId,@AttrEnum,@AttrValue,@Remark,@CreateUser,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttr_School_Id", SqlDbType.Char, 0x24), new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@AttrEnum", SqlDbType.VarChar, 50), new SqlParameter("@AttrValue", SqlDbType.VarChar, 10), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.BookAttr_School_Id;
            cmdParms[1].Value = model.SchoolId;
            cmdParms[2].Value = model.AttrEnum;
            cmdParms[3].Value = model.AttrValue;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreateUser;
            cmdParms[6].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_BookAttr_School DataRowToModel(DataRow row)
        {
            Model_BookAttr_School school = new Model_BookAttr_School();
            if (row != null)
            {
                if (row["BookAttr_School_Id"] != null)
                {
                    school.BookAttr_School_Id = row["BookAttr_School_Id"].ToString();
                }
                if (row["SchoolId"] != null)
                {
                    school.SchoolId = row["SchoolId"].ToString();
                }
                if (row["AttrEnum"] != null)
                {
                    school.AttrEnum = row["AttrEnum"].ToString();
                }
                if (row["AttrValue"] != null)
                {
                    school.AttrValue = row["AttrValue"].ToString();
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

        public bool Delete(string BookAttr_School_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookAttr_School ");
            builder.Append(" where BookAttr_School_Id=@BookAttr_School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttr_School_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookAttr_School_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BookAttr_School_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from BookAttr_School ");
            builder.Append(" where BookAttr_School_Id in (" + BookAttr_School_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string BookAttr_School_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from BookAttr_School");
            builder.Append(" where BookAttr_School_Id=@BookAttr_School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttr_School_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookAttr_School_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BookAttr_School_Id,SchoolId,AttrEnum,AttrValue,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM BookAttr_School ");
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
            builder.Append(" BookAttr_School_Id,SchoolId,AttrEnum,AttrValue,Remark,CreateUser,CreateTime ");
            builder.Append(" FROM BookAttr_School ");
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
                builder.Append("order by T.BookAttr_School_Id desc");
            }
            builder.Append(")AS Row, T.*  from BookAttr_School T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByUserId(string UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select distinct t.BookAttr_School_Id,t.SchoolId,t.AttrEnum,t.AttrValue,t.Remark,t.CreateUser,t.CreateTime ");
            builder.Append(" FROM BookAttr_School t ");
            builder.Append(" inner join VW_UserOnClassGradeSchool vw on vw.SchoolId=t.SchoolId ");
            builder.AppendFormat(" where vw.UserId='{0}' ", UserId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_BookAttr_School GetModel(string BookAttr_School_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BookAttr_School_Id,SchoolId,AttrEnum,AttrValue,Remark,CreateUser,CreateTime from BookAttr_School ");
            builder.Append(" where BookAttr_School_Id=@BookAttr_School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BookAttr_School_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = BookAttr_School_Id;
            new Model_BookAttr_School();
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
            builder.Append("select count(1) FROM BookAttr_School ");
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

        public bool Update(Model_BookAttr_School model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update BookAttr_School set ");
            builder.Append("SchoolId=@SchoolId,");
            builder.Append("AttrEnum=@AttrEnum,");
            builder.Append("AttrValue=@AttrValue,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateUser=@CreateUser,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where BookAttr_School_Id=@BookAttr_School_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SchoolId", SqlDbType.VarChar, 50), new SqlParameter("@AttrEnum", SqlDbType.VarChar, 50), new SqlParameter("@AttrValue", SqlDbType.VarChar, 10), new SqlParameter("@Remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@BookAttr_School_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SchoolId;
            cmdParms[1].Value = model.AttrEnum;
            cmdParms[2].Value = model.AttrValue;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateUser;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.BookAttr_School_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

