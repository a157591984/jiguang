namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ResourceShare
    {
        public bool Add(Model_ResourceShare model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ResourceShare(");
            builder.Append("ResourceShareId,ResourceToResourceFolder_Id,ResourceShareType,ShareObjectId,Remark,CreateTime,CreateUserId)");
            builder.Append(" values (");
            builder.Append("@ResourceShareId,@ResourceToResourceFolder_Id,@ResourceShareType,@ShareObjectId,@Remark,@CreateTime,@CreateUserId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceShareId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceShareType", SqlDbType.NVarChar, 30), new SqlParameter("@ShareObjectId", SqlDbType.VarChar, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUserId", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = model.ResourceShareId;
            cmdParms[1].Value = model.ResourceToResourceFolder_Id;
            cmdParms[2].Value = model.ResourceShareType;
            cmdParms[3].Value = model.ShareObjectId;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreateTime;
            cmdParms[6].Value = model.CreateUserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool CancelShareResource(string ResourceToResourceFolder_Id, Model_ResourceToResourceFolder rtrmodel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("delete from ResourceShare ");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.VarChar, 0x24) };
            parameterArray[0].Value = ResourceToResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("update ResourceToResourceFolder set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("Resource_Id=@Resource_Id,");
            builder.Append("File_Name=@File_Name,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("Resource_Class=@Resource_Class,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("File_Owner=@File_Owner,");
            builder.Append("CreateFUser=@CreateFUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("File_Suffix=@File_Suffix,");
            builder.Append("LessonPlan_Type=@LessonPlan_Type,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Domain=@Resource_Domain,");
            builder.Append("Resource_Url=@Resource_Url,");
            builder.Append("Resource_shared=@Resource_shared,");
            builder.Append("Book_ID=@Book_ID,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("ResourceToResourceFolder_Order=@ResourceToResourceFolder_Order");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            parameterArray2[0].Value = rtrmodel.ResourceFolder_Id;
            parameterArray2[1].Value = rtrmodel.Resource_Id;
            parameterArray2[2].Value = rtrmodel.File_Name;
            parameterArray2[3].Value = rtrmodel.Resource_Type;
            parameterArray2[4].Value = rtrmodel.Resource_Name;
            parameterArray2[5].Value = rtrmodel.Resource_Class;
            parameterArray2[6].Value = rtrmodel.Resource_Version;
            parameterArray2[7].Value = rtrmodel.File_Owner;
            parameterArray2[8].Value = rtrmodel.CreateFUser;
            parameterArray2[9].Value = rtrmodel.CreateTime;
            parameterArray2[10].Value = rtrmodel.UpdateTime;
            parameterArray2[11].Value = rtrmodel.File_Suffix;
            parameterArray2[12].Value = rtrmodel.LessonPlan_Type;
            parameterArray2[13].Value = rtrmodel.GradeTerm;
            parameterArray2[14].Value = rtrmodel.Subject;
            parameterArray2[15].Value = rtrmodel.Resource_Domain;
            parameterArray2[0x10].Value = rtrmodel.Resource_Url;
            parameterArray2[0x11].Value = rtrmodel.Resource_shared;
            parameterArray2[0x12].Value = rtrmodel.Book_ID;
            parameterArray2[0x13].Value = rtrmodel.ParticularYear;
            parameterArray2[20].Value = rtrmodel.ResourceToResourceFolder_Order;
            parameterArray2[0x15].Value = rtrmodel.ResourceToResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_ResourceShare DataRowToModel(DataRow row)
        {
            Model_ResourceShare share = new Model_ResourceShare();
            if (row != null)
            {
                if (row["ResourceShareId"] != null)
                {
                    share.ResourceShareId = row["ResourceShareId"].ToString();
                }
                if (row["ResourceToResourceFolder_Id"] != null)
                {
                    share.ResourceToResourceFolder_Id = row["ResourceToResourceFolder_Id"].ToString();
                }
                if (row["ResourceShareType"] != null)
                {
                    share.ResourceShareType = row["ResourceShareType"].ToString();
                }
                if (row["ShareObjectId"] != null)
                {
                    share.ShareObjectId = row["ShareObjectId"].ToString();
                }
                if (row["Remark"] != null)
                {
                    share.Remark = row["Remark"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    share.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUserId"] != null)
                {
                    share.CreateUserId = row["CreateUserId"].ToString();
                }
            }
            return share;
        }

        public bool Delete(string ResourceShareId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceShare ");
            builder.Append(" where ResourceShareId=@ResourceShareId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceShareId", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = ResourceShareId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceShareIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceShare ");
            builder.Append(" where ResourceShareId in (" + ResourceShareIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceShareId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceShare");
            builder.Append(" where ResourceShareId=@ResourceShareId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceShareId", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = ResourceShareId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceShareId,ResourceToResourceFolder_Id,ResourceShareType,ShareObjectId,Remark,CreateTime,CreateUserId ");
            builder.Append(" FROM ResourceShare ");
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
            builder.Append(" ResourceShareId,ResourceToResourceFolder_Id,ResourceShareType,ShareObjectId,Remark,CreateTime,CreateUserId ");
            builder.Append(" FROM ResourceShare ");
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
                builder.Append("order by T.ResourceShareId desc");
            }
            builder.Append(")AS Row, T.*  from ResourceShare T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ResourceShare GetModel(string ResourceShareId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceShareId,ResourceToResourceFolder_Id,ResourceShareType,ShareObjectId,Remark,CreateTime,CreateUserId from ResourceShare ");
            builder.Append(" where ResourceShareId=@ResourceShareId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceShareId", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = ResourceShareId;
            new Model_ResourceShare();
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
            builder.Append("select count(1) FROM ResourceShare ");
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

        public bool ShareResource(Model_ResourceShare model, Model_ResourceToResourceFolder rtrmodel)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into ResourceShare(");
            builder.Append("ResourceShareId,ResourceToResourceFolder_Id,ResourceShareType,ShareObjectId,Remark,CreateTime,CreateUserId)");
            builder.Append(" values (");
            builder.Append("@ResourceShareId,@ResourceToResourceFolder_Id,@ResourceShareType,@ShareObjectId,@Remark,@CreateTime,@CreateUserId)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@ResourceShareId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceShareType", SqlDbType.NVarChar, 30), new SqlParameter("@ShareObjectId", SqlDbType.VarChar, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUserId", SqlDbType.VarChar, 0x24) };
            parameterArray[0].Value = model.ResourceShareId;
            parameterArray[1].Value = model.ResourceToResourceFolder_Id;
            parameterArray[2].Value = model.ResourceShareType;
            parameterArray[3].Value = model.ShareObjectId;
            parameterArray[4].Value = model.Remark;
            parameterArray[5].Value = model.CreateTime;
            parameterArray[6].Value = model.CreateUserId;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("update ResourceToResourceFolder set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("Resource_Id=@Resource_Id,");
            builder.Append("File_Name=@File_Name,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("Resource_Class=@Resource_Class,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("File_Owner=@File_Owner,");
            builder.Append("CreateFUser=@CreateFUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("File_Suffix=@File_Suffix,");
            builder.Append("LessonPlan_Type=@LessonPlan_Type,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Domain=@Resource_Domain,");
            builder.Append("Resource_Url=@Resource_Url,");
            builder.Append("Resource_shared=@Resource_shared,");
            builder.Append("Book_ID=@Book_ID,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("ResourceToResourceFolder_Order=@ResourceToResourceFolder_Order");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Order", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            parameterArray2[0].Value = rtrmodel.ResourceFolder_Id;
            parameterArray2[1].Value = rtrmodel.Resource_Id;
            parameterArray2[2].Value = rtrmodel.File_Name;
            parameterArray2[3].Value = rtrmodel.Resource_Type;
            parameterArray2[4].Value = rtrmodel.Resource_Name;
            parameterArray2[5].Value = rtrmodel.Resource_Class;
            parameterArray2[6].Value = rtrmodel.Resource_Version;
            parameterArray2[7].Value = rtrmodel.File_Owner;
            parameterArray2[8].Value = rtrmodel.CreateFUser;
            parameterArray2[9].Value = rtrmodel.CreateTime;
            parameterArray2[10].Value = rtrmodel.UpdateTime;
            parameterArray2[11].Value = rtrmodel.File_Suffix;
            parameterArray2[12].Value = rtrmodel.LessonPlan_Type;
            parameterArray2[13].Value = rtrmodel.GradeTerm;
            parameterArray2[14].Value = rtrmodel.Subject;
            parameterArray2[15].Value = rtrmodel.Resource_Domain;
            parameterArray2[0x10].Value = rtrmodel.Resource_Url;
            parameterArray2[0x11].Value = rtrmodel.Resource_shared;
            parameterArray2[0x12].Value = rtrmodel.Book_ID;
            parameterArray2[0x13].Value = rtrmodel.ParticularYear;
            parameterArray2[20].Value = rtrmodel.ResourceToResourceFolder_Order;
            parameterArray2[0x15].Value = rtrmodel.ResourceToResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public bool Update(Model_ResourceShare model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ResourceShare set ");
            builder.Append("ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id,");
            builder.Append("ResourceShareType=@ResourceShareType,");
            builder.Append("ShareObjectId=@ShareObjectId,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUserId=@CreateUserId");
            builder.Append(" where ResourceShareId=@ResourceShareId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceShareType", SqlDbType.NVarChar, 30), new SqlParameter("@ShareObjectId", SqlDbType.VarChar, 0x24), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUserId", SqlDbType.VarChar, 0x24), new SqlParameter("@ResourceShareId", SqlDbType.VarChar, 0x24) };
            cmdParms[0].Value = model.ResourceToResourceFolder_Id;
            cmdParms[1].Value = model.ResourceShareType;
            cmdParms[2].Value = model.ShareObjectId;
            cmdParms[3].Value = model.Remark;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.CreateUserId;
            cmdParms[6].Value = model.ResourceShareId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

