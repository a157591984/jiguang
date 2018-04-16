namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class DAL_CustomVisitAttachment
    {
        public DAL_CustomVisitAttachment()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_CustomVisitAttachment(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_CustomVisitAttachment model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_CustomVisitAttachment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" CustomVisitAttachment( ");
            builder.Append(" CustomVisitAttachment_ID,CustomVisit_ID,CustomVisitAttachment_URL,CustomVisitAttachment_SourceName,CustomVisitAttachment_ServerName,CreateTime,CreateUser,UpdateUser,UpdateTime ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @CustomVisitAttachment_ID,@CustomVisit_ID,@CustomVisitAttachment_URL,@CustomVisitAttachment_SourceName,@CustomVisitAttachment_ServerName,@CreateTime,@CreateUser,@UpdateUser,@UpdateTime ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.CustomVisitAttachment_ID, model.CustomVisit_ID, model.CustomVisitAttachment_URL, model.CustomVisitAttachment_SourceName, model.CustomVisitAttachment_ServerName, model.CreateTime, model.CreateUser, model.UpdateUser, model.UpdateTime });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" CustomVisitAttachment ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string customvisitattachment_id)
        {
            return this.DeleteByPK(null, customvisitattachment_id);
        }

        internal int DeleteByPK(DbTransaction tran, string customvisitattachment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" WHERE ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { customvisitattachment_id });
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.ExistsByCondition(null, conditionStr, paramValues);
        }

        internal bool ExistsByCondition(DbTransaction tran, string conditionStr, params object[] paramValues)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByLogic(string customvisitattachment_id)
        {
            return this.ExistsByLogic(null, customvisitattachment_id);
        }

        internal bool ExistsByLogic(DbTransaction tran, string customvisitattachment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" WHERE ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID  ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { customvisitattachment_id })) > 0);
        }

        public bool ExistsByPK(string customvisitattachment_id)
        {
            return this.ExistsByPK(null, customvisitattachment_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string customvisitattachment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" WHERE ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { customvisitattachment_id })) > 0);
        }

        public int GetCustomVisitAttachmentCount(string strCondition, params object[] param)
        {
            return this.GetCustomVisitAttachmentCount(null, strCondition, param);
        }

        internal int GetCustomVisitAttachmentCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public Model_CustomVisitAttachment GetCustomVisitAttachmentModelByLogic(string customvisitattachment_id)
        {
            return this.GetCustomVisitAttachmentModelByLogic(null, customvisitattachment_id);
        }

        internal Model_CustomVisitAttachment GetCustomVisitAttachmentModelByLogic(DbTransaction tran, string customvisitattachment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" WHERE ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID  ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { customvisitattachment_id });
            Model_CustomVisitAttachment attachment = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                attachment = new Model_CustomVisitAttachment();
                if (row["CustomVisitAttachment_ID"] != null)
                {
                    attachment.CustomVisitAttachment_ID = row["CustomVisitAttachment_ID"].ToString();
                }
                if (row["CustomVisit_ID"] != null)
                {
                    attachment.CustomVisit_ID = row["CustomVisit_ID"].ToString();
                }
                if (row["CustomVisitAttachment_URL"] != null)
                {
                    attachment.CustomVisitAttachment_URL = row["CustomVisitAttachment_URL"].ToString();
                }
                if (row["CustomVisitAttachment_SourceName"] != null)
                {
                    attachment.CustomVisitAttachment_SourceName = row["CustomVisitAttachment_SourceName"].ToString();
                }
                if (row["CustomVisitAttachment_ServerName"] != null)
                {
                    attachment.CustomVisitAttachment_ServerName = row["CustomVisitAttachment_ServerName"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        attachment.CreateTime = null;
                    }
                    else
                    {
                        attachment.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    attachment.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    attachment.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] == null)
                {
                    return attachment;
                }
                if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                {
                    attachment.UpdateTime = null;
                    return attachment;
                }
                attachment.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
            }
            return attachment;
        }

        public Model_CustomVisitAttachment GetCustomVisitAttachmentModelByPK(string customvisitattachment_id)
        {
            return this.GetCustomVisitAttachmentModelByPK(null, customvisitattachment_id);
        }

        internal Model_CustomVisitAttachment GetCustomVisitAttachmentModelByPK(DbTransaction tran, string customvisitattachment_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" WHERE ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { customvisitattachment_id });
            Model_CustomVisitAttachment attachment = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                attachment = new Model_CustomVisitAttachment();
                if (row["CustomVisitAttachment_ID"] != null)
                {
                    attachment.CustomVisitAttachment_ID = row["CustomVisitAttachment_ID"].ToString();
                }
                if (row["CustomVisit_ID"] != null)
                {
                    attachment.CustomVisit_ID = row["CustomVisit_ID"].ToString();
                }
                if (row["CustomVisitAttachment_URL"] != null)
                {
                    attachment.CustomVisitAttachment_URL = row["CustomVisitAttachment_URL"].ToString();
                }
                if (row["CustomVisitAttachment_SourceName"] != null)
                {
                    attachment.CustomVisitAttachment_SourceName = row["CustomVisitAttachment_SourceName"].ToString();
                }
                if (row["CustomVisitAttachment_ServerName"] != null)
                {
                    attachment.CustomVisitAttachment_ServerName = row["CustomVisitAttachment_ServerName"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        attachment.CreateTime = null;
                    }
                    else
                    {
                        attachment.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    attachment.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    attachment.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] == null)
                {
                    return attachment;
                }
                if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                {
                    attachment.UpdateTime = null;
                    return attachment;
                }
                attachment.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
            }
            return attachment;
        }

        public List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetCustomVisitAttachmentModelList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            if (!string.IsNullOrEmpty(orderColumn))
            {
                builder.Append(" ORDER BY ");
                builder.Append(orderColumn);
                if (!string.IsNullOrEmpty(orderType))
                {
                    builder.Append(" " + orderType);
                }
            }
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, param);
            List<Model_CustomVisitAttachment> list = new List<Model_CustomVisitAttachment>();
            Model_CustomVisitAttachment item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_CustomVisitAttachment();
                if (row["CustomVisitAttachment_ID"] != null)
                {
                    item.CustomVisitAttachment_ID = row["CustomVisitAttachment_ID"].ToString();
                }
                if (row["CustomVisit_ID"] != null)
                {
                    item.CustomVisit_ID = row["CustomVisit_ID"].ToString();
                }
                if (row["CustomVisitAttachment_URL"] != null)
                {
                    item.CustomVisitAttachment_URL = row["CustomVisitAttachment_URL"].ToString();
                }
                if (row["CustomVisitAttachment_SourceName"] != null)
                {
                    item.CustomVisitAttachment_SourceName = row["CustomVisitAttachment_SourceName"].ToString();
                }
                if (row["CustomVisitAttachment_ServerName"] != null)
                {
                    item.CustomVisitAttachment_ServerName = row["CustomVisitAttachment_ServerName"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        item.CreateTime = null;
                    }
                    else
                    {
                        item.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    item.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        item.UpdateTime = null;
                    }
                    else
                    {
                        item.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetCustomVisitAttachmentModelListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_CustomVisitAttachment> GetCustomVisitAttachmentModelListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            if ((pageSize <= 0) || (pageIndex <= 0))
            {
                throw new Exception("分页参数错误，必须大于零");
            }
            if (string.IsNullOrEmpty(orderColumn))
            {
                throw new Exception("排序字段必须填写");
            }
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM (");
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM CustomVisitAttachment", orderColumn, orderType));
            if (!string.IsNullOrWhiteSpace(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            builder.Append(" ) t ");
            builder.Append(" WHERE rownum between ");
            builder.Append(string.Format(" {0} ", num));
            builder.Append(" AND ");
            builder.Append(string.Format(" {0} ", num2));
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, param);
            List<Model_CustomVisitAttachment> list = new List<Model_CustomVisitAttachment>();
            Model_CustomVisitAttachment item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_CustomVisitAttachment();
                if (row["CustomVisitAttachment_ID"] != null)
                {
                    item.CustomVisitAttachment_ID = row["CustomVisitAttachment_ID"].ToString();
                }
                if (row["CustomVisit_ID"] != null)
                {
                    item.CustomVisit_ID = row["CustomVisit_ID"].ToString();
                }
                if (row["CustomVisitAttachment_URL"] != null)
                {
                    item.CustomVisitAttachment_URL = row["CustomVisitAttachment_URL"].ToString();
                }
                if (row["CustomVisitAttachment_SourceName"] != null)
                {
                    item.CustomVisitAttachment_SourceName = row["CustomVisitAttachment_SourceName"].ToString();
                }
                if (row["CustomVisitAttachment_ServerName"] != null)
                {
                    item.CustomVisitAttachment_ServerName = row["CustomVisitAttachment_ServerName"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        item.CreateTime = null;
                    }
                    else
                    {
                        item.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    item.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateUser"] != null)
                {
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        item.UpdateTime = null;
                    }
                    else
                    {
                        item.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetDataSet(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal DataSet GetDataSet(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" CustomVisitAttachment ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            if (!string.IsNullOrEmpty(orderColumn))
            {
                builder.Append(" ORDER BY ");
                builder.Append(orderColumn);
                if (!string.IsNullOrEmpty(orderType))
                {
                    builder.Append(" " + orderType);
                }
            }
            return this.CurrDB.ExecuteDataSet(builder.ToString(), tran, param);
        }

        public int Update(Model_CustomVisitAttachment model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_CustomVisitAttachment model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" SET ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID,CustomVisit_ID=@CustomVisit_ID,CustomVisitAttachment_URL=@CustomVisitAttachment_URL,CustomVisitAttachment_SourceName=@CustomVisitAttachment_SourceName,CustomVisitAttachment_ServerName=@CustomVisitAttachment_ServerName,CreateTime=@CreateTime,CreateUser=@CreateUser,UpdateUser=@UpdateUser,UpdateTime=@UpdateTime ");
            builder.Append(" WHERE ");
            builder.Append(" CustomVisitAttachment_ID=@CustomVisitAttachment_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { model.CustomVisitAttachment_ID, model.CustomVisit_ID, model.CustomVisitAttachment_URL, model.CustomVisitAttachment_SourceName, model.CustomVisitAttachment_ServerName, model.CreateTime, model.CreateUser, model.UpdateUser, model.UpdateTime });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" CustomVisitAttachment ");
            builder.Append(" SET ");
            builder.Append(strUpdateColumns);
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        private DatabaseSQLHelper CurrDB { get; set; }
    }
}

