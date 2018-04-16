namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ConfigSchool
    {
        public bool Add(Model_ConfigSchool model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ConfigSchool(");
            builder.Append("ConfigEnum,School_ID,School_Name,D_Name,D_Value,D_Type,D_Remark,D_Order,D_CreateTime,D_CreateUser,D_UpdateTime,D_UpdateUser,D_PublicValue,SchoolIP,Mobile)");
            builder.Append(" values (");
            builder.Append("@ConfigEnum,@School_ID,@School_Name,@D_Name,@D_Value,@D_Type,@D_Remark,@D_Order,@D_CreateTime,@D_CreateUser,@D_UpdateTime,@D_UpdateUser,@D_PublicValue,@SchoolIP,@Mobile)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConfigEnum", SqlDbType.VarChar, 20), new SqlParameter("@School_ID", SqlDbType.VarChar, 50), new SqlParameter("@School_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_Name", SqlDbType.NVarChar, 100), new SqlParameter("@D_Value", SqlDbType.VarChar, 300), new SqlParameter("@D_Type", SqlDbType.VarChar, 50), new SqlParameter("@D_Remark", SqlDbType.NVarChar, 500), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@D_CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_UpdateTime", SqlDbType.DateTime), new SqlParameter("@D_UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_PublicValue", SqlDbType.VarChar, 300), new SqlParameter("@SchoolIP", SqlDbType.VarChar, 300), new SqlParameter("@Mobile", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.ConfigEnum;
            cmdParms[1].Value = model.School_ID;
            cmdParms[2].Value = model.School_Name;
            cmdParms[3].Value = model.D_Name;
            cmdParms[4].Value = model.D_Value;
            cmdParms[5].Value = model.D_Type;
            cmdParms[6].Value = model.D_Remark;
            cmdParms[7].Value = model.D_Order;
            cmdParms[8].Value = model.D_CreateTime;
            cmdParms[9].Value = model.D_CreateUser;
            cmdParms[10].Value = model.D_UpdateTime;
            cmdParms[11].Value = model.D_UpdateUser;
            cmdParms[12].Value = model.D_PublicValue;
            cmdParms[13].Value = model.SchoolIP;
            cmdParms[14].Value = model.Mobile;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ConfigSchool DataRowToModel(DataRow row)
        {
            Model_ConfigSchool school = new Model_ConfigSchool();
            if (row != null)
            {
                if (row["ConfigEnum"] != null)
                {
                    school.ConfigEnum = row["ConfigEnum"].ToString();
                }
                if (row["School_ID"] != null)
                {
                    school.School_ID = row["School_ID"].ToString();
                }
                if (row["School_Name"] != null)
                {
                    school.School_Name = row["School_Name"].ToString();
                }
                if (row["D_Name"] != null)
                {
                    school.D_Name = row["D_Name"].ToString();
                }
                if (row["D_Value"] != null)
                {
                    school.D_Value = row["D_Value"].ToString();
                }
                if (row["D_Type"] != null)
                {
                    school.D_Type = row["D_Type"].ToString();
                }
                if (row["D_Remark"] != null)
                {
                    school.D_Remark = row["D_Remark"].ToString();
                }
                if ((row["D_Order"] != null) && (row["D_Order"].ToString() != ""))
                {
                    school.D_Order = new int?(int.Parse(row["D_Order"].ToString()));
                }
                if ((row["D_CreateTime"] != null) && (row["D_CreateTime"].ToString() != ""))
                {
                    school.D_CreateTime = new DateTime?(DateTime.Parse(row["D_CreateTime"].ToString()));
                }
                if (row["D_CreateUser"] != null)
                {
                    school.D_CreateUser = row["D_CreateUser"].ToString();
                }
                if ((row["D_UpdateTime"] != null) && (row["D_UpdateTime"].ToString() != ""))
                {
                    school.D_UpdateTime = new DateTime?(DateTime.Parse(row["D_UpdateTime"].ToString()));
                }
                if (row["D_UpdateUser"] != null)
                {
                    school.D_UpdateUser = row["D_UpdateUser"].ToString();
                }
                if (row["D_PublicValue"] != null)
                {
                    school.D_PublicValue = row["D_PublicValue"].ToString();
                }
                if (row["SchoolIP"] != null)
                {
                    school.SchoolIP = row["SchoolIP"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    school.Mobile = row["Mobile"].ToString();
                }
            }
            return school;
        }

        public bool Delete(string ConfigEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ConfigSchool ");
            builder.Append(" where ConfigEnum=@ConfigEnum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConfigEnum", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = ConfigEnum;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ConfigEnumlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ConfigSchool ");
            builder.Append(" where ConfigEnum in (" + ConfigEnumlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ConfigEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ConfigSchool");
            builder.Append(" where ConfigEnum=@ConfigEnum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConfigEnum", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = ConfigEnum;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ConfigEnum,School_ID,School_Name,D_Name,D_Value,D_Type,D_Remark,D_Order,D_CreateTime,D_CreateUser,D_UpdateTime,D_UpdateUser,D_PublicValue,SchoolIP,Mobile ");
            builder.Append(" FROM ConfigSchool ");
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
            builder.Append(" ConfigEnum,School_ID,School_Name,D_Name,D_Value,D_Type,D_Remark,D_Order,D_CreateTime,D_CreateUser,D_UpdateTime,D_UpdateUser,D_PublicValue,SchoolIP,Mobile ");
            builder.Append(" FROM ConfigSchool ");
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
                builder.Append("order by T.ConfigEnum desc");
            }
            builder.Append(")AS Row, T.*  from ConfigSchool T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ConfigSchool GetModel(string ConfigEnum)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ConfigEnum,School_ID,School_Name,D_Name,D_Value,D_Type,D_Remark,D_Order,D_CreateTime,D_CreateUser,D_UpdateTime,D_UpdateUser,D_PublicValue,SchoolIP,Mobile from ConfigSchool ");
            builder.Append(" where ConfigEnum=@ConfigEnum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ConfigEnum", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = ConfigEnum;
            new Model_ConfigSchool();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_ConfigSchool GetModelBySchoolId(string SchoolId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from ConfigSchool ");
            builder.Append(" where School_ID=@School_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_ID", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = SchoolId;
            new Model_ConfigSchool();
            DataSet set = DbHelperSQL_Operate.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_ConfigSchool GetModelBySchoolIdNew(string SchoolId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from ConfigSchool ");
            builder.Append(" where School_ID=@School_ID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_ID", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = SchoolId;
            new Model_ConfigSchool();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetOperateList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ConfigEnum,School_ID,School_Name,D_Name,D_Value,D_Type,D_Remark,D_Order,D_CreateTime,D_CreateUser,D_UpdateTime,D_UpdateUser,D_PublicValue,SchoolIP,Mobile from ConfigSchool ");
            builder.Append(" FROM ConfigSchool ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL_Operate.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM ConfigSchool ");
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

        public DataSet GetSchoolInfoList(string userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct SchoolId,SchoolName from VW_UserOnClassGradeSchool \r\nwhere SchoolId is not null and SchoolId!='' and UserId='{0}';", userId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetSchoolPublicUrl(string userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select distinct t.D_Value as apiUrlList,t.D_PublicValue as publicUrl from ConfigSchool t\r\ninner join VW_UserOnClassGradeSchool t2 on  t2.UserId='{0}' and t2.SchoolId=t.School_ID; ", userId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Model_ConfigSchool model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ConfigSchool set ");
            builder.Append("School_ID=@School_ID,");
            builder.Append("School_Name=@School_Name,");
            builder.Append("D_Name=@D_Name,");
            builder.Append("D_Value=@D_Value,");
            builder.Append("D_Type=@D_Type,");
            builder.Append("D_Remark=@D_Remark,");
            builder.Append("D_Order=@D_Order,");
            builder.Append("D_CreateTime=@D_CreateTime,");
            builder.Append("D_CreateUser=@D_CreateUser,");
            builder.Append("D_UpdateTime=@D_UpdateTime,");
            builder.Append("D_UpdateUser=@D_UpdateUser,");
            builder.Append("D_PublicValue=@D_PublicValue,");
            builder.Append("SchoolIP=@SchoolIP,");
            builder.Append("Mobile=@Mobile");
            builder.Append(" where ConfigEnum=@ConfigEnum ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@School_ID", SqlDbType.VarChar, 50), new SqlParameter("@School_Name", SqlDbType.NVarChar, 250), new SqlParameter("@D_Name", SqlDbType.NVarChar, 100), new SqlParameter("@D_Value", SqlDbType.VarChar, 300), new SqlParameter("@D_Type", SqlDbType.VarChar, 50), new SqlParameter("@D_Remark", SqlDbType.NVarChar, 500), new SqlParameter("@D_Order", SqlDbType.Int, 4), new SqlParameter("@D_CreateTime", SqlDbType.DateTime), new SqlParameter("@D_CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_UpdateTime", SqlDbType.DateTime), new SqlParameter("@D_UpdateUser", SqlDbType.Char, 0x24), new SqlParameter("@D_PublicValue", SqlDbType.VarChar, 300), new SqlParameter("@SchoolIP", SqlDbType.VarChar, 300), new SqlParameter("@Mobile", SqlDbType.VarChar, 50), new SqlParameter("@ConfigEnum", SqlDbType.VarChar, 20) };
            cmdParms[0].Value = model.School_ID;
            cmdParms[1].Value = model.School_Name;
            cmdParms[2].Value = model.D_Name;
            cmdParms[3].Value = model.D_Value;
            cmdParms[4].Value = model.D_Type;
            cmdParms[5].Value = model.D_Remark;
            cmdParms[6].Value = model.D_Order;
            cmdParms[7].Value = model.D_CreateTime;
            cmdParms[8].Value = model.D_CreateUser;
            cmdParms[9].Value = model.D_UpdateTime;
            cmdParms[10].Value = model.D_UpdateUser;
            cmdParms[11].Value = model.D_PublicValue;
            cmdParms[12].Value = model.SchoolIP;
            cmdParms[13].Value = model.Mobile;
            cmdParms[14].Value = model.ConfigEnum;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

