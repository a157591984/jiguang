namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_SendMessageTemplate
    {
        public bool Add(Model_SendMessageTemplate model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SendMessageTemplate(");
            builder.Append("SendSMSTemplateId,SType,UserName,PassWord,Content,IsStart,MsgUrl,Method,CTime,CUser,UserId,Mobile)");
            builder.Append(" values (");
            builder.Append("@SendSMSTemplateId,@SType,@UserName,@PassWord,@Content,@IsStart,@MsgUrl,@Method,@CTime,@CUser,@UserId,@Mobile)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSTemplateId", SqlDbType.Char, 0x24), new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@PassWord", SqlDbType.NVarChar, 200), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@IsStart", SqlDbType.Int, 4), new SqlParameter("@MsgUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Method", SqlDbType.NVarChar, 200), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@CUser", SqlDbType.Char, 0x24), new SqlParameter("@UserId", SqlDbType.VarChar, 50), new SqlParameter("@Mobile", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = model.SendSMSTemplateId;
            cmdParms[1].Value = model.SType;
            cmdParms[2].Value = model.UserName;
            cmdParms[3].Value = model.PassWord;
            cmdParms[4].Value = model.Content;
            cmdParms[5].Value = model.IsStart;
            cmdParms[6].Value = model.MsgUrl;
            cmdParms[7].Value = model.Method;
            cmdParms[8].Value = model.CTime;
            cmdParms[9].Value = model.CUser;
            cmdParms[10].Value = model.UserId;
            cmdParms[11].Value = model.Mobile;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Model_SendMessageTemplate DataRowToModel(DataRow row)
        {
            Model_SendMessageTemplate template = new Model_SendMessageTemplate();
            if (row != null)
            {
                if (row["SendSMSTemplateId"] != null)
                {
                    template.SendSMSTemplateId = row["SendSMSTemplateId"].ToString();
                }
                if (row["SType"] != null)
                {
                    template.SType = row["SType"].ToString();
                }
                if (row["UserName"] != null)
                {
                    template.UserName = row["UserName"].ToString();
                }
                if (row["PassWord"] != null)
                {
                    template.PassWord = row["PassWord"].ToString();
                }
                if (row["Content"] != null)
                {
                    template.Content = row["Content"].ToString();
                }
                if ((row["IsStart"] != null) && (row["IsStart"].ToString() != ""))
                {
                    template.IsStart = new int?(int.Parse(row["IsStart"].ToString()));
                }
                if (row["MsgUrl"] != null)
                {
                    template.MsgUrl = row["MsgUrl"].ToString();
                }
                if (row["Method"] != null)
                {
                    template.Method = row["Method"].ToString();
                }
                if ((row["CTime"] != null) && (row["CTime"].ToString() != ""))
                {
                    template.CTime = new DateTime?(DateTime.Parse(row["CTime"].ToString()));
                }
                if (row["CUser"] != null)
                {
                    template.CUser = row["CUser"].ToString();
                }
                if (row["UserId"] != null)
                {
                    template.UserId = row["UserId"].ToString();
                }
                if (row["Mobile"] != null)
                {
                    template.Mobile = row["Mobile"].ToString();
                }
            }
            return template;
        }

        public bool Delete(string SendSMSTemplateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SendMessageTemplate ");
            builder.Append(" where SendSMSTemplateId=@SendSMSTemplateId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSTemplateId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SendSMSTemplateId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SendSMSTemplateIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SendMessageTemplate ");
            builder.Append(" where SendSMSTemplateId in (" + SendSMSTemplateIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string SendSMSTemplateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SendMessageTemplate");
            builder.Append(" where SendSMSTemplateId=@SendSMSTemplateId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSTemplateId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SendSMSTemplateId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SendSMSTemplateId,SType,UserName,PassWord,Content,IsStart,MsgUrl,Method,CTime,CUser,UserId,Mobile ");
            builder.Append(" FROM SendMessageTemplate ");
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
            builder.Append(" SendSMSTemplateId,SType,UserName,PassWord,Content,IsStart,MsgUrl,Method,CTime,CUser,UserId,Mobile ");
            builder.Append(" FROM SendMessageTemplate ");
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
                builder.Append("order by T.SendSMSTemplateId desc");
            }
            builder.Append(")AS Row, T.*  from SendMessageTemplate T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_SendMessageTemplate GetModel(string SendSMSTemplateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SendSMSTemplateId,SType,UserName,PassWord,Content,IsStart,MsgUrl,Method,CTime,CUser,UserId,Mobile from SendMessageTemplate ");
            builder.Append(" where SendSMSTemplateId=@SendSMSTemplateId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SendSMSTemplateId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = SendSMSTemplateId;
            new Model_SendMessageTemplate();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Model_SendMessageTemplate GetModelBySType(string SType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SendSMSTemplateId,SType,UserName,PassWord,Content,IsStart,MsgUrl,Method,CTime,CUser,UserId,Mobile from SendMessageTemplate ");
            builder.Append(" where IsStart='1' and SType=@SType ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SType", SqlDbType.VarChar, 50) };
            cmdParms[0].Value = SType;
            new Model_SendMessageTemplate();
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
            builder.Append("select count(1) FROM SendMessageTemplate ");
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

        public bool Update(Model_SendMessageTemplate model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SendMessageTemplate set ");
            builder.Append("SType=@SType,");
            builder.Append("UserName=@UserName,");
            builder.Append("PassWord=@PassWord,");
            builder.Append("Content=@Content,");
            builder.Append("IsStart=@IsStart,");
            builder.Append("MsgUrl=@MsgUrl,");
            builder.Append("Method=@Method,");
            builder.Append("CTime=@CTime,");
            builder.Append("CUser=@CUser,");
            builder.Append("UserId=@UserId,");
            builder.Append("Mobile=@Mobile");
            builder.Append(" where SendSMSTemplateId=@SendSMSTemplateId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SType", SqlDbType.VarChar, 50), new SqlParameter("@UserName", SqlDbType.NVarChar, 200), new SqlParameter("@PassWord", SqlDbType.NVarChar, 200), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@IsStart", SqlDbType.Int, 4), new SqlParameter("@MsgUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Method", SqlDbType.NVarChar, 200), new SqlParameter("@CTime", SqlDbType.DateTime), new SqlParameter("@CUser", SqlDbType.Char, 0x24), new SqlParameter("@UserId", SqlDbType.VarChar, 50), new SqlParameter("@Mobile", SqlDbType.VarChar, 50), new SqlParameter("@SendSMSTemplateId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.SType;
            cmdParms[1].Value = model.UserName;
            cmdParms[2].Value = model.PassWord;
            cmdParms[3].Value = model.Content;
            cmdParms[4].Value = model.IsStart;
            cmdParms[5].Value = model.MsgUrl;
            cmdParms[6].Value = model.Method;
            cmdParms[7].Value = model.CTime;
            cmdParms[8].Value = model.CUser;
            cmdParms[9].Value = model.UserId;
            cmdParms[10].Value = model.Mobile;
            cmdParms[11].Value = model.SendSMSTemplateId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

