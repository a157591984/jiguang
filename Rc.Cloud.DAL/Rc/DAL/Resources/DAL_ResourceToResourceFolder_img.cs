namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_ResourceToResourceFolder_img
    {
        public bool Add(Model_ResourceToResourceFolder_img model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder_img(");
            builder.Append("ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_img_id,@ResourceToResourceFolder_id,@ResourceToResourceFolderImg_Url,@ResourceToResourceFolderImg_Order,@CreateTime)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolderImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@ResourceToResourceFolderImg_Order", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ResourceToResourceFolder_img_id;
            cmdParms[1].Value = model.ResourceToResourceFolder_id;
            cmdParms[2].Value = model.ResourceToResourceFolderImg_Url;
            cmdParms[3].Value = model.ResourceToResourceFolderImg_Order;
            cmdParms[4].Value = model.CreateTime;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_ResourceToResourceFolder_img DataRowToModel(DataRow row)
        {
            Model_ResourceToResourceFolder_img _img = new Model_ResourceToResourceFolder_img();
            if (row != null)
            {
                if (row["ResourceToResourceFolder_img_id"] != null)
                {
                    _img.ResourceToResourceFolder_img_id = row["ResourceToResourceFolder_img_id"].ToString();
                }
                if (row["ResourceToResourceFolder_id"] != null)
                {
                    _img.ResourceToResourceFolder_id = row["ResourceToResourceFolder_id"].ToString();
                }
                if (row["ResourceToResourceFolderImg_Url"] != null)
                {
                    _img.ResourceToResourceFolderImg_Url = row["ResourceToResourceFolderImg_Url"].ToString();
                }
                if ((row["ResourceToResourceFolderImg_Order"] != null) && (row["ResourceToResourceFolderImg_Order"].ToString() != ""))
                {
                    _img.ResourceToResourceFolderImg_Order = new int?(int.Parse(row["ResourceToResourceFolderImg_Order"].ToString()));
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    _img.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
            }
            return _img;
        }

        public bool Delete(string ResourceToResourceFolder_img_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder_img ");
            builder.Append(" where ResourceToResourceFolder_img_id=@ResourceToResourceFolder_img_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_img_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ResourceToResourceFolder_img_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder_img ");
            builder.Append(" where ResourceToResourceFolder_img_id in (" + ResourceToResourceFolder_img_idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string ResourceToResourceFolder_img_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from ResourceToResourceFolder_img");
            builder.Append(" where ResourceToResourceFolder_img_id=@ResourceToResourceFolder_img_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_img_id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime ");
            builder.Append(" FROM ResourceToResourceFolder_img ");
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
            builder.Append(" ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime ");
            builder.Append(" FROM ResourceToResourceFolder_img ");
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
                builder.Append("order by T.ResourceToResourceFolder_img_id desc");
            }
            builder.Append(")AS Row, T.*  from ResourceToResourceFolder_img T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_ResourceToResourceFolder_img GetModel(string ResourceToResourceFolder_img_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime from ResourceToResourceFolder_img ");
            builder.Append(" where ResourceToResourceFolder_img_id=@ResourceToResourceFolder_img_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_img_id;
            new Model_ResourceToResourceFolder_img();
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
            builder.Append("select count(1) FROM ResourceToResourceFolder_img ");
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

        public bool Update(Model_ResourceToResourceFolder_img model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ResourceToResourceFolder_img set ");
            builder.Append("ResourceToResourceFolder_id=@ResourceToResourceFolder_id,");
            builder.Append("ResourceToResourceFolderImg_Url=@ResourceToResourceFolderImg_Url,");
            builder.Append("ResourceToResourceFolderImg_Order=@ResourceToResourceFolderImg_Order,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ResourceToResourceFolder_img_id=@ResourceToResourceFolder_img_id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolderImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@ResourceToResourceFolderImg_Order", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.ResourceToResourceFolder_id;
            cmdParms[1].Value = model.ResourceToResourceFolderImg_Url;
            cmdParms[2].Value = model.ResourceToResourceFolderImg_Order;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.ResourceToResourceFolder_img_id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int UpdateRTRFAddListData(Model_ResourceToResourceFolder modelRTRF, List<Model_ResourceToResourceFolder_img> list, Model_BookProductionLog modelBPL)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
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
            builder.Append("ParticularYear=@ParticularYear");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            parameterArray[0].Value = modelRTRF.ResourceFolder_Id;
            parameterArray[1].Value = modelRTRF.Resource_Id;
            parameterArray[2].Value = modelRTRF.File_Name;
            parameterArray[3].Value = modelRTRF.Resource_Type;
            parameterArray[4].Value = modelRTRF.Resource_Name;
            parameterArray[5].Value = modelRTRF.Resource_Class;
            parameterArray[6].Value = modelRTRF.Resource_Version;
            parameterArray[7].Value = modelRTRF.File_Owner;
            parameterArray[8].Value = modelRTRF.CreateFUser;
            parameterArray[9].Value = modelRTRF.CreateTime;
            parameterArray[10].Value = modelRTRF.UpdateTime;
            parameterArray[11].Value = modelRTRF.File_Suffix;
            parameterArray[12].Value = modelRTRF.LessonPlan_Type;
            parameterArray[13].Value = modelRTRF.GradeTerm;
            parameterArray[14].Value = modelRTRF.Subject;
            parameterArray[15].Value = modelRTRF.Resource_Domain;
            parameterArray[0x10].Value = modelRTRF.Resource_Url;
            parameterArray[0x11].Value = modelRTRF.Resource_shared;
            parameterArray[0x12].Value = modelRTRF.Book_ID;
            parameterArray[0x13].Value = modelRTRF.ParticularYear;
            parameterArray[20].Value = modelRTRF.ResourceToResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("delete from ResourceToResourceFolder_img where ResourceToResourceFolder_id=@ResourceToResourceFolder_id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_id", modelRTRF.ResourceToResourceFolder_Id) };
            dictionary.Add(builder.ToString(), parameterArray2);
            int num = 0;
            foreach (Model_ResourceToResourceFolder_img _img in list)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into ResourceToResourceFolder_img(");
                builder.Append("ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime)");
                builder.Append(" values (");
                builder.Append("@ResourceToResourceFolder_img_id,@ResourceToResourceFolder_id,@ResourceToResourceFolderImg_Url,@ResourceToResourceFolderImg_Order,@CreateTime)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolderImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@ResourceToResourceFolderImg_Order", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray3[0].Value = _img.ResourceToResourceFolder_img_id;
                parameterArray3[1].Value = _img.ResourceToResourceFolder_id;
                parameterArray3[2].Value = _img.ResourceToResourceFolderImg_Url;
                parameterArray3[3].Value = _img.ResourceToResourceFolderImg_Order;
                parameterArray3[4].Value = _img.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            builder = new StringBuilder();
            builder.Append("insert into BookProductionLog(");
            builder.Append("BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark)");
            builder.Append(" values (");
            builder.Append("@BookProductionLog_Id,@BookId,@ResourceToResourceFolder_Id,@ParticularYear,@Resource_Type,@LogTypeEnum,@CreateUser,@CreateTime,@LogRemark)");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200) };
            parameterArray4[0].Value = modelBPL.BookProductionLog_Id;
            parameterArray4[1].Value = modelBPL.BookId;
            parameterArray4[2].Value = modelBPL.ResourceToResourceFolder_Id;
            parameterArray4[3].Value = modelBPL.ParticularYear;
            parameterArray4[4].Value = modelBPL.Resource_Type;
            parameterArray4[5].Value = modelBPL.LogTypeEnum;
            parameterArray4[6].Value = modelBPL.CreateUser;
            parameterArray4[7].Value = modelBPL.CreateTime;
            parameterArray4[8].Value = modelBPL.LogRemark;
            dictionary.Add(builder.ToString(), parameterArray4);
            object obj2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }
    }
}

