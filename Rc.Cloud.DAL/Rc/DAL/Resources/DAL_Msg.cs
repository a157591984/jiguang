namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Msg
    {
        public bool Add(Model_Msg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Msg(");
            builder.Append("MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser)");
            builder.Append(" values (");
            builder.Append("@MsgId,@MsgEnum,@MsgTypeEnum,@ResourceDataId,@MsgTitle,@MsgContent,@MsgStatus,@MsgSender,@MsgAccepter,@CreateTime,@CreateUser)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24), new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.MsgId;
            cmdParms[1].Value = model.MsgEnum;
            cmdParms[2].Value = model.MsgTypeEnum;
            cmdParms[3].Value = model.ResourceDataId;
            cmdParms[4].Value = model.MsgTitle;
            cmdParms[5].Value = model.MsgContent;
            cmdParms[6].Value = model.MsgStatus;
            cmdParms[7].Value = model.MsgSender;
            cmdParms[8].Value = model.MsgAccepter;
            cmdParms[9].Value = model.CreateTime;
            cmdParms[10].Value = model.CreateUser;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool BatchMarkReadStatus(List<Model_Msg> listMsg)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (Model_Msg msg in listMsg)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("update Msg set ");
                builder.Append("MsgEnum=@MsgEnum,");
                builder.Append("MsgTypeEnum=@MsgTypeEnum,");
                builder.Append("ResourceDataId=@ResourceDataId,");
                builder.Append("MsgTitle=@MsgTitle,");
                builder.Append("MsgContent=@MsgContent,");
                builder.Append("MsgStatus=@MsgStatus,");
                builder.Append("MsgSender=@MsgSender,");
                builder.Append("MsgAccepter=@MsgAccepter,");
                builder.Append("CreateTime=@CreateTime,");
                builder.Append("CreateUser=@CreateUser");
                builder.Append(" where MsgId=@MsgId ");
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@MsgId", SqlDbType.Char, 0x24) };
                parameterArray[0].Value = msg.MsgEnum;
                parameterArray[1].Value = msg.MsgTypeEnum;
                parameterArray[2].Value = msg.ResourceDataId;
                parameterArray[3].Value = msg.MsgTitle;
                parameterArray[4].Value = msg.MsgContent;
                parameterArray[5].Value = msg.MsgStatus;
                parameterArray[6].Value = msg.MsgSender;
                parameterArray[7].Value = msg.MsgAccepter;
                parameterArray[8].Value = msg.CreateTime;
                parameterArray[9].Value = msg.CreateUser;
                parameterArray[10].Value = msg.MsgId;
                dictionary.Add(builder.ToString(), parameterArray);
            }
            return (DbHelperSQL.ExecuteSqlTran(dictionary) > 0);
        }

        public Model_Msg DataRowToModel(DataRow row)
        {
            Model_Msg msg = new Model_Msg();
            if (row != null)
            {
                if (row["MsgId"] != null)
                {
                    msg.MsgId = row["MsgId"].ToString();
                }
                if (row["MsgEnum"] != null)
                {
                    msg.MsgEnum = row["MsgEnum"].ToString();
                }
                if (row["MsgTypeEnum"] != null)
                {
                    msg.MsgTypeEnum = row["MsgTypeEnum"].ToString();
                }
                if (row["ResourceDataId"] != null)
                {
                    msg.ResourceDataId = row["ResourceDataId"].ToString();
                }
                if (row["MsgTitle"] != null)
                {
                    msg.MsgTitle = row["MsgTitle"].ToString();
                }
                if (row["MsgContent"] != null)
                {
                    msg.MsgContent = row["MsgContent"].ToString();
                }
                if (row["MsgStatus"] != null)
                {
                    msg.MsgStatus = row["MsgStatus"].ToString();
                }
                if (row["MsgSender"] != null)
                {
                    msg.MsgSender = row["MsgSender"].ToString();
                }
                if (row["MsgAccepter"] != null)
                {
                    msg.MsgAccepter = row["MsgAccepter"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    msg.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if (row["CreateUser"] != null)
                {
                    msg.CreateUser = row["CreateUser"].ToString();
                }
            }
            return msg;
        }

        public bool Delete(string MsgId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Msg ");
            builder.Append(" where MsgId=@MsgId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = MsgId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string MsgIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Msg ");
            builder.Append(" where MsgId in (" + MsgIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string MsgId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Msg");
            builder.Append(" where MsgId=@MsgId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = MsgId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser ");
            builder.Append(" FROM Msg ");
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
            builder.Append(" MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser ");
            builder.Append(" FROM Msg ");
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
                builder.Append("order by T.MsgId desc");
            }
            builder.Append(")AS Row, T.*  from Msg T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListForSearch(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.Msg_Id desc");
            }
            builder.Append(")AS Row, T.*,u.TrueName,u.UserName from Msg T \r\n            left join F_User u on T.MsgSender=u.UserId ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Msg GetModel(string MsgId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 MsgId,MsgEnum,MsgTypeEnum,ResourceDataId,MsgTitle,MsgContent,MsgStatus,MsgSender,MsgAccepter,CreateTime,CreateUser from Msg ");
            builder.Append(" where MsgId=@MsgId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MsgId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = MsgId;
            new Model_Msg();
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
            builder.Append("select count(1) FROM Msg ");
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

        public bool Update(Model_Msg model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Msg set ");
            builder.Append("MsgEnum=@MsgEnum,");
            builder.Append("MsgTypeEnum=@MsgTypeEnum,");
            builder.Append("ResourceDataId=@ResourceDataId,");
            builder.Append("MsgTitle=@MsgTitle,");
            builder.Append("MsgContent=@MsgContent,");
            builder.Append("MsgStatus=@MsgStatus,");
            builder.Append("MsgSender=@MsgSender,");
            builder.Append("MsgAccepter=@MsgAccepter,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("CreateUser=@CreateUser");
            builder.Append(" where MsgId=@MsgId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@MsgEnum", SqlDbType.VarChar, 20), new SqlParameter("@MsgTypeEnum", SqlDbType.VarChar, 20), new SqlParameter("@ResourceDataId", SqlDbType.Char, 0x24), new SqlParameter("@MsgTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MsgContent", SqlDbType.NVarChar, 500), new SqlParameter("@MsgStatus", SqlDbType.VarChar, 20), new SqlParameter("@MsgSender", SqlDbType.Char, 0x24), new SqlParameter("@MsgAccepter", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@MsgId", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.MsgEnum;
            cmdParms[1].Value = model.MsgTypeEnum;
            cmdParms[2].Value = model.ResourceDataId;
            cmdParms[3].Value = model.MsgTitle;
            cmdParms[4].Value = model.MsgContent;
            cmdParms[5].Value = model.MsgStatus;
            cmdParms[6].Value = model.MsgSender;
            cmdParms[7].Value = model.MsgAccepter;
            cmdParms[8].Value = model.CreateTime;
            cmdParms[9].Value = model.CreateUser;
            cmdParms[10].Value = model.MsgId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

