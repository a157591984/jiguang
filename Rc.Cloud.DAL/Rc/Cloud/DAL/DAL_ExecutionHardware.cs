namespace Rc.Cloud.DAL
{
    using Rc.Cloud.Model;
    using Rc.Common.DBUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DAL_ExecutionHardware
    {
        public DAL_ExecutionHardware()
        {
            this.CurrDB = DatabaseSQLHelperFactory.CreateDatabase();
        }

        public DAL_ExecutionHardware(DatabaseSQLHelper db)
        {
            this.CurrDB = db;
        }

        public int Add(Model_ExecutionHardware model)
        {
            return this.Add(null, model);
        }

        internal int Add(DbTransaction tran, Model_ExecutionHardware model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" INSERT INTO ");
            builder.Append(" ExecutionHardware( ");
            builder.Append(" ExecutionHardware_ID,CustomerInfo_ID,Supplier_ID,ExecutionHardware_Type,ExecutionHardware_Code,ExecutionHardware_ApplyMan,ExecutionHardware_ApplyDepartment,ExecutionHardware_Name,ExecutionHardware_Count,ExecutionHardware_Version,ExecutionHardware_ProcurementTime,ExecutionHardware_ProcurementCosts,ExecutionHardware_ProcurementStaff,ExecutionHardware_BearTheCostSide,ExecutionHardware_Brand,ExecutionHardware_Status,ExecutionHardware_ReceiveTime,ExecutionHardware_InstallTime,ExecutionHardware_Remark,CreateTime,CreateUser,UpdateTime,UpdateUser ");
            builder.Append(" ) ");
            builder.Append(" values( ");
            builder.Append(" @ExecutionHardware_ID,@CustomerInfo_ID,@Supplier_ID,@ExecutionHardware_Type,@ExecutionHardware_Code,@ExecutionHardware_ApplyMan,@ExecutionHardware_ApplyDepartment,@ExecutionHardware_Name,@ExecutionHardware_Count,@ExecutionHardware_Version,@ExecutionHardware_ProcurementTime,@ExecutionHardware_ProcurementCosts,@ExecutionHardware_ProcurementStaff,@ExecutionHardware_BearTheCostSide,@ExecutionHardware_Brand,@ExecutionHardware_Status,@ExecutionHardware_ReceiveTime,@ExecutionHardware_InstallTime,@ExecutionHardware_Remark,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser ");
            builder.Append(" ) ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { 
                model.ExecutionHardware_ID, model.CustomerInfo_ID, model.Supplier_ID, model.ExecutionHardware_Type, model.ExecutionHardware_Code, model.ExecutionHardware_ApplyMan, model.ExecutionHardware_ApplyDepartment, model.ExecutionHardware_Name, model.ExecutionHardware_Count, model.ExecutionHardware_Version, model.ExecutionHardware_ProcurementTime, model.ExecutionHardware_ProcurementCosts, model.ExecutionHardware_ProcurementStaff, model.ExecutionHardware_BearTheCostSide, model.ExecutionHardware_Brand, model.ExecutionHardware_Status, 
                model.ExecutionHardware_ReceiveTime, model.ExecutionHardware_InstallTime, model.ExecutionHardware_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser
             });
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DeleteByCondition(null, strCondition, param);
        }

        internal int DeleteByCondition(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM ");
            builder.Append(" ExecutionHardware ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, param);
        }

        public int DeleteByPK(string executionhardware_id)
        {
            return this.DeleteByPK(null, executionhardware_id);
        }

        internal int DeleteByPK(DbTransaction tran, string executionhardware_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" DELETE FROM");
            builder.Append(" ExecutionHardware ");
            builder.Append(" WHERE ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { executionhardware_id });
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
            builder.Append(" ExecutionHardware ");
            if (!string.IsNullOrEmpty(conditionStr))
            {
                builder.Append(" WHERE ");
                builder.Append(conditionStr);
            }
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, paramValues)) > 0);
        }

        public bool ExistsByLogic(string executionhardware_id)
        {
            return this.ExistsByLogic(null, executionhardware_id);
        }

        internal bool ExistsByLogic(DbTransaction tran, string executionhardware_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" ExecutionHardware ");
            builder.Append(" WHERE ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID  ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { executionhardware_id })) > 0);
        }

        public bool ExistsByPK(string executionhardware_id)
        {
            return this.ExistsByPK(null, executionhardware_id);
        }

        internal bool ExistsByPK(DbTransaction tran, string executionhardware_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" COUNT(1) ");
            builder.Append(" FROM ");
            builder.Append(" ExecutionHardware ");
            builder.Append(" WHERE ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID ");
            return (Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, new object[] { executionhardware_id })) > 0);
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
            builder.Append(" ExecutionHardware ");
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

        public int GetExecutionHardwareCount(string strCondition, params object[] param)
        {
            return this.GetExecutionHardwareCount(null, strCondition, param);
        }

        internal int GetExecutionHardwareCount(DbTransaction tran, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" Count(1) ");
            builder.Append(" FROM ");
            builder.Append(" ExecutionHardware ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                builder.Append(" WHERE ");
                builder.Append(strCondition);
            }
            return Convert.ToInt32(this.CurrDB.ExecuteScalar(builder.ToString(), tran, param));
        }

        public DataSet GetListByID(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *,CustomerInfo.CustomerInfo_NameCN,Common_Dict.D_Name  from ExecutionHardware inner join CustomerInfo on ExecutionHardware.CustomerInfo_ID=CustomerInfo.CustomerInfo_ID                       \r\n                     left join Common_Dict on Common_Dict.Common_Dict_ID=ExecutionHardware. ExecutionHardware_Type where 1=1 ");
            builder.Append(" and ExecutionHardware_ID='" + id + "'");
            return this.CurrDB.ExecuteDataSet(builder.ToString(), null, new object[0]);
        }

        public DataSet GetListPaged(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT row_number() over(order by ExecutionHardware_ProcurementTime DESC ) AS r_n,\r\n                               ExecutionHardware_ID,ExecutionHardware_Name,Common_Dict.D_Name,ExecutionHardware_Count,ExecutionHardware_InstallTime,ExecutionHardware_ProcurementTime\r\n                            from ExecutionHardware left join Common_Dict on  Common_Dict.Common_Dict_ID=ExecutionHardware.ExecutionHardware_Type                    \r\n                          where 1=1 ");
            builder.Append(" and ExecutionHardware.CustomerInfo_ID='" + strWhere + "'");
            return sys.GetRecordByPage(builder.ToString(), PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetMaxCode(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            string str = DateTime.Now.ToString("yyyy-MM-dd");
            builder.Append("select MAX(");
            builder.Append(tableName + "_Code) as code from ");
            builder.Append(tableName + " where ");
            builder.Append(tableName + "_Code like '");
            builder.Append(str + "%'");
            return this.CurrDB.ExecuteDataSet(builder.ToString(), null, new object[0]);
        }

        public Model_ExecutionHardware GetModel_ExecutionHardwareByLogic(string executionhardware_id)
        {
            return this.GetModel_ExecutionHardwareByLogic(null, executionhardware_id);
        }

        internal Model_ExecutionHardware GetModel_ExecutionHardwareByLogic(DbTransaction tran, string executionhardware_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" ExecutionHardware ");
            builder.Append(" WHERE ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID  ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { executionhardware_id });
            Model_ExecutionHardware hardware = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                hardware = new Model_ExecutionHardware();
                if (row["ExecutionHardware_ID"] != null)
                {
                    hardware.ExecutionHardware_ID = row["ExecutionHardware_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    hardware.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                if (row["Execution_ID"] != null)
                {
                    hardware.Execution_ID = row["Execution_ID"].ToString();
                }
                if (row["Supplier_ID"] != null)
                {
                    hardware.Supplier_ID = row["Supplier_ID"].ToString();
                }
                if (row["ExecutionHardware_Type"] != null)
                {
                    hardware.ExecutionHardware_Type = row["ExecutionHardware_Type"].ToString();
                }
                if (row["ExecutionHardware_Code"] != null)
                {
                    hardware.ExecutionHardware_Code = row["ExecutionHardware_Code"].ToString();
                }
                if (row["ExecutionHardware_ApplyMan"] != null)
                {
                    hardware.ExecutionHardware_ApplyMan = row["ExecutionHardware_ApplyMan"].ToString();
                }
                if (row["ExecutionHardware_ApplyDepartment"] != null)
                {
                    hardware.ExecutionHardware_ApplyDepartment = row["ExecutionHardware_ApplyDepartment"].ToString();
                }
                if (row["ExecutionHardware_Name"] != null)
                {
                    hardware.ExecutionHardware_Name = row["ExecutionHardware_Name"].ToString();
                }
                if (row["ExecutionHardware_Count"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_Count"].ToString()))
                    {
                        hardware.ExecutionHardware_Count = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_Count = new int?(int.Parse(row["ExecutionHardware_Count"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Version"] != null)
                {
                    hardware.ExecutionHardware_Version = row["ExecutionHardware_Version"].ToString();
                }
                if (row["ExecutionHardware_ProcurementTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementTime"].ToString()))
                    {
                        hardware.ExecutionHardware_ProcurementTime = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_ProcurementTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ProcurementTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementCosts"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementCosts"].ToString()))
                    {
                        hardware.ExecutionHardware_ProcurementCosts = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_ProcurementCosts = new decimal?(decimal.Parse(row["ExecutionHardware_ProcurementCosts"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementStaff"] != null)
                {
                    hardware.ExecutionHardware_ProcurementStaff = row["ExecutionHardware_ProcurementStaff"].ToString();
                }
                if (row["ExecutionHardware_BearTheCostSide"] != null)
                {
                    hardware.ExecutionHardware_BearTheCostSide = row["ExecutionHardware_BearTheCostSide"].ToString();
                }
                if (row["ExecutionHardware_Status"] != null)
                {
                    hardware.ExecutionHardware_Status = row["ExecutionHardware_Status"].ToString();
                }
                if (row["ExecutionHardware_ReceiveTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ReceiveTime"].ToString()))
                    {
                        hardware.ExecutionHardware_ReceiveTime = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_ReceiveTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ReceiveTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_InstallTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_InstallTime"].ToString()))
                    {
                        hardware.ExecutionHardware_InstallTime = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_InstallTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_InstallTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Remark"] != null)
                {
                    hardware.ExecutionHardware_Remark = row["ExecutionHardware_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        hardware.CreateTime = null;
                    }
                    else
                    {
                        hardware.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    hardware.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        hardware.UpdateTime = null;
                    }
                    else
                    {
                        hardware.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] != null)
                {
                    hardware.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return hardware;
        }

        public Model_ExecutionHardware GetModel_ExecutionHardwareByPK(string executionhardware_id)
        {
            return this.GetModel_ExecutionHardwareByPK(null, executionhardware_id);
        }

        internal Model_ExecutionHardware GetModel_ExecutionHardwareByPK(DbTransaction tran, string executionhardware_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            builder.Append(" TOP 1 * ");
            builder.Append(" FROM ");
            builder.Append(" ExecutionHardware ");
            builder.Append(" WHERE ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID ");
            DataSet set = this.CurrDB.ExecuteDataSet(builder.ToString(), tran, new object[] { executionhardware_id });
            Model_ExecutionHardware hardware = null;
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                hardware = new Model_ExecutionHardware();
                if (row["ExecutionHardware_ID"] != null)
                {
                    hardware.ExecutionHardware_ID = row["ExecutionHardware_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    hardware.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                if (row["Supplier_ID"] != null)
                {
                    hardware.Supplier_ID = row["Supplier_ID"].ToString();
                }
                if (row["ExecutionHardware_Type"] != null)
                {
                    hardware.ExecutionHardware_Type = row["ExecutionHardware_Type"].ToString();
                }
                if (row["ExecutionHardware_Code"] != null)
                {
                    hardware.ExecutionHardware_Code = row["ExecutionHardware_Code"].ToString();
                }
                if (row["ExecutionHardware_ApplyMan"] != null)
                {
                    hardware.ExecutionHardware_ApplyMan = row["ExecutionHardware_ApplyMan"].ToString();
                }
                if (row["ExecutionHardware_ApplyDepartment"] != null)
                {
                    hardware.ExecutionHardware_ApplyDepartment = row["ExecutionHardware_ApplyDepartment"].ToString();
                }
                if (row["ExecutionHardware_Name"] != null)
                {
                    hardware.ExecutionHardware_Name = row["ExecutionHardware_Name"].ToString();
                }
                if (row["ExecutionHardware_Count"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_Count"].ToString()))
                    {
                        hardware.ExecutionHardware_Count = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_Count = new int?(int.Parse(row["ExecutionHardware_Count"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Version"] != null)
                {
                    hardware.ExecutionHardware_Version = row["ExecutionHardware_Version"].ToString();
                }
                if (row["ExecutionHardware_ProcurementTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementTime"].ToString()))
                    {
                        hardware.ExecutionHardware_ProcurementTime = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_ProcurementTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ProcurementTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementCosts"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementCosts"].ToString()))
                    {
                        hardware.ExecutionHardware_ProcurementCosts = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_ProcurementCosts = new decimal?(decimal.Parse(row["ExecutionHardware_ProcurementCosts"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementStaff"] != null)
                {
                    hardware.ExecutionHardware_ProcurementStaff = row["ExecutionHardware_ProcurementStaff"].ToString();
                }
                if (row["ExecutionHardware_BearTheCostSide"] != null)
                {
                    hardware.ExecutionHardware_BearTheCostSide = row["ExecutionHardware_BearTheCostSide"].ToString();
                }
                if (row["ExecutionHardware_Status"] != null)
                {
                    hardware.ExecutionHardware_Status = row["ExecutionHardware_Status"].ToString();
                }
                if (row["ExecutionHardware_ReceiveTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ReceiveTime"].ToString()))
                    {
                        hardware.ExecutionHardware_ReceiveTime = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_ReceiveTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ReceiveTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_InstallTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_InstallTime"].ToString()))
                    {
                        hardware.ExecutionHardware_InstallTime = null;
                    }
                    else
                    {
                        hardware.ExecutionHardware_InstallTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_InstallTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Remark"] != null)
                {
                    hardware.ExecutionHardware_Remark = row["ExecutionHardware_Remark"].ToString();
                }
                if (row["CreateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["CreateTime"].ToString()))
                    {
                        hardware.CreateTime = null;
                    }
                    else
                    {
                        hardware.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                    }
                }
                if (row["CreateUser"] != null)
                {
                    hardware.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["UpdateTime"].ToString()))
                    {
                        hardware.UpdateTime = null;
                    }
                    else
                    {
                        hardware.UpdateTime = new DateTime?(DateTime.Parse(row["UpdateTime"].ToString()));
                    }
                }
                if (row["UpdateUser"] != null)
                {
                    hardware.UpdateUser = row["UpdateUser"].ToString();
                }
            }
            return hardware;
        }

        public List<Model_ExecutionHardware> GetModel_ExecutionHardwareList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_ExecutionHardwareList(null, recordNum, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_ExecutionHardware> GetModel_ExecutionHardwareList(DbTransaction tran, int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ");
            if (recordNum > 0)
            {
                builder.Append(" TOP " + recordNum);
            }
            builder.Append(" * ");
            builder.Append(" FROM ");
            builder.Append(" ExecutionHardware ");
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
            List<Model_ExecutionHardware> list = new List<Model_ExecutionHardware>();
            Model_ExecutionHardware item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_ExecutionHardware();
                if (row["ExecutionHardware_ID"] != null)
                {
                    item.ExecutionHardware_ID = row["ExecutionHardware_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    item.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                if (row["Execution_ID"] != null)
                {
                    item.Execution_ID = row["Execution_ID"].ToString();
                }
                if (row["Supplier_ID"] != null)
                {
                    item.Supplier_ID = row["Supplier_ID"].ToString();
                }
                if (row["ExecutionHardware_Type"] != null)
                {
                    item.ExecutionHardware_Type = row["ExecutionHardware_Type"].ToString();
                }
                if (row["ExecutionHardware_Code"] != null)
                {
                    item.ExecutionHardware_Code = row["ExecutionHardware_Code"].ToString();
                }
                if (row["ExecutionHardware_ApplyMan"] != null)
                {
                    item.ExecutionHardware_ApplyMan = row["ExecutionHardware_ApplyMan"].ToString();
                }
                if (row["ExecutionHardware_ApplyDepartment"] != null)
                {
                    item.ExecutionHardware_ApplyDepartment = row["ExecutionHardware_ApplyDepartment"].ToString();
                }
                if (row["ExecutionHardware_Name"] != null)
                {
                    item.ExecutionHardware_Name = row["ExecutionHardware_Name"].ToString();
                }
                if (row["ExecutionHardware_Count"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_Count"].ToString()))
                    {
                        item.ExecutionHardware_Count = null;
                    }
                    else
                    {
                        item.ExecutionHardware_Count = new int?(int.Parse(row["ExecutionHardware_Count"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Version"] != null)
                {
                    item.ExecutionHardware_Version = row["ExecutionHardware_Version"].ToString();
                }
                if (row["ExecutionHardware_ProcurementTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementTime"].ToString()))
                    {
                        item.ExecutionHardware_ProcurementTime = null;
                    }
                    else
                    {
                        item.ExecutionHardware_ProcurementTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ProcurementTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementCosts"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementCosts"].ToString()))
                    {
                        item.ExecutionHardware_ProcurementCosts = null;
                    }
                    else
                    {
                        item.ExecutionHardware_ProcurementCosts = new decimal?(decimal.Parse(row["ExecutionHardware_ProcurementCosts"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementStaff"] != null)
                {
                    item.ExecutionHardware_ProcurementStaff = row["ExecutionHardware_ProcurementStaff"].ToString();
                }
                if (row["ExecutionHardware_BearTheCostSide"] != null)
                {
                    item.ExecutionHardware_BearTheCostSide = row["ExecutionHardware_BearTheCostSide"].ToString();
                }
                if (row["ExecutionHardware_Status"] != null)
                {
                    item.ExecutionHardware_Status = row["ExecutionHardware_Status"].ToString();
                }
                if (row["ExecutionHardware_ReceiveTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ReceiveTime"].ToString()))
                    {
                        item.ExecutionHardware_ReceiveTime = null;
                    }
                    else
                    {
                        item.ExecutionHardware_ReceiveTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ReceiveTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_InstallTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_InstallTime"].ToString()))
                    {
                        item.ExecutionHardware_InstallTime = null;
                    }
                    else
                    {
                        item.ExecutionHardware_InstallTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_InstallTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Remark"] != null)
                {
                    item.ExecutionHardware_Remark = row["ExecutionHardware_Remark"].ToString();
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
                if (row["UpdateUser"] != null)
                {
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public List<Model_ExecutionHardware> GetModel_ExecutionHardwareListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.GetModel_ExecutionHardwareListByPage(null, pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        internal List<Model_ExecutionHardware> GetModel_ExecutionHardwareListByPage(DbTransaction tran, int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
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
            builder.Append(string.Format(" SELECT (ROW_NUMBER() OVER(ORDER BY {0} {1})) as rownum,* FROM ExecutionHardware", orderColumn, orderType));
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
            List<Model_ExecutionHardware> list = new List<Model_ExecutionHardware>();
            Model_ExecutionHardware item = null;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                item = new Model_ExecutionHardware();
                if (row["ExecutionHardware_ID"] != null)
                {
                    item.ExecutionHardware_ID = row["ExecutionHardware_ID"].ToString();
                }
                if (row["CustomerInfo_ID"] != null)
                {
                    item.CustomerInfo_ID = row["CustomerInfo_ID"].ToString();
                }
                if (row["Execution_ID"] != null)
                {
                    item.Execution_ID = row["Execution_ID"].ToString();
                }
                if (row["Supplier_ID"] != null)
                {
                    item.Supplier_ID = row["Supplier_ID"].ToString();
                }
                if (row["ExecutionHardware_Type"] != null)
                {
                    item.ExecutionHardware_Type = row["ExecutionHardware_Type"].ToString();
                }
                if (row["ExecutionHardware_Code"] != null)
                {
                    item.ExecutionHardware_Code = row["ExecutionHardware_Code"].ToString();
                }
                if (row["ExecutionHardware_ApplyMan"] != null)
                {
                    item.ExecutionHardware_ApplyMan = row["ExecutionHardware_ApplyMan"].ToString();
                }
                if (row["ExecutionHardware_ApplyDepartment"] != null)
                {
                    item.ExecutionHardware_ApplyDepartment = row["ExecutionHardware_ApplyDepartment"].ToString();
                }
                if (row["ExecutionHardware_Name"] != null)
                {
                    item.ExecutionHardware_Name = row["ExecutionHardware_Name"].ToString();
                }
                if (row["ExecutionHardware_Count"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_Count"].ToString()))
                    {
                        item.ExecutionHardware_Count = null;
                    }
                    else
                    {
                        item.ExecutionHardware_Count = new int?(int.Parse(row["ExecutionHardware_Count"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Version"] != null)
                {
                    item.ExecutionHardware_Version = row["ExecutionHardware_Version"].ToString();
                }
                if (row["ExecutionHardware_ProcurementTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementTime"].ToString()))
                    {
                        item.ExecutionHardware_ProcurementTime = null;
                    }
                    else
                    {
                        item.ExecutionHardware_ProcurementTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ProcurementTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementCosts"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ProcurementCosts"].ToString()))
                    {
                        item.ExecutionHardware_ProcurementCosts = null;
                    }
                    else
                    {
                        item.ExecutionHardware_ProcurementCosts = new decimal?(decimal.Parse(row["ExecutionHardware_ProcurementCosts"].ToString()));
                    }
                }
                if (row["ExecutionHardware_ProcurementStaff"] != null)
                {
                    item.ExecutionHardware_ProcurementStaff = row["ExecutionHardware_ProcurementStaff"].ToString();
                }
                if (row["ExecutionHardware_BearTheCostSide"] != null)
                {
                    item.ExecutionHardware_BearTheCostSide = row["ExecutionHardware_BearTheCostSide"].ToString();
                }
                if (row["ExecutionHardware_Status"] != null)
                {
                    item.ExecutionHardware_Status = row["ExecutionHardware_Status"].ToString();
                }
                if (row["ExecutionHardware_ReceiveTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_ReceiveTime"].ToString()))
                    {
                        item.ExecutionHardware_ReceiveTime = null;
                    }
                    else
                    {
                        item.ExecutionHardware_ReceiveTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_ReceiveTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_InstallTime"] != null)
                {
                    if (string.IsNullOrWhiteSpace(row["ExecutionHardware_InstallTime"].ToString()))
                    {
                        item.ExecutionHardware_InstallTime = null;
                    }
                    else
                    {
                        item.ExecutionHardware_InstallTime = new DateTime?(DateTime.Parse(row["ExecutionHardware_InstallTime"].ToString()));
                    }
                }
                if (row["ExecutionHardware_Remark"] != null)
                {
                    item.ExecutionHardware_Remark = row["ExecutionHardware_Remark"].ToString();
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
                if (row["UpdateUser"] != null)
                {
                    item.UpdateUser = row["UpdateUser"].ToString();
                }
                list.Add(item);
            }
            return list;
        }

        public int Update(Model_ExecutionHardware model)
        {
            return this.Update(null, model);
        }

        internal int Update(DbTransaction tran, Model_ExecutionHardware model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" ExecutionHardware ");
            builder.Append(" SET ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID,CustomerInfo_ID=@CustomerInfo_ID,Supplier_ID=@Supplier_ID,ExecutionHardware_Type=@ExecutionHardware_Type,ExecutionHardware_Code=@ExecutionHardware_Code,ExecutionHardware_ApplyMan=@ExecutionHardware_ApplyMan,ExecutionHardware_ApplyDepartment=@ExecutionHardware_ApplyDepartment,ExecutionHardware_Name=@ExecutionHardware_Name,ExecutionHardware_Count=@ExecutionHardware_Count,ExecutionHardware_Version=@ExecutionHardware_Version,ExecutionHardware_ProcurementTime=@ExecutionHardware_ProcurementTime,ExecutionHardware_ProcurementCosts=@ExecutionHardware_ProcurementCosts,ExecutionHardware_ProcurementStaff=@ExecutionHardware_ProcurementStaff,ExecutionHardware_BearTheCostSide=@ExecutionHardware_BearTheCostSide,ExecutionHardware_Brand=@ExecutionHardware_Brand,ExecutionHardware_Status=@ExecutionHardware_Status,ExecutionHardware_ReceiveTime=@ExecutionHardware_ReceiveTime,ExecutionHardware_InstallTime=@ExecutionHardware_InstallTime,ExecutionHardware_Remark=@ExecutionHardware_Remark,CreateTime=@CreateTime,CreateUser=@CreateUser,UpdateTime=@UpdateTime,UpdateUser=@UpdateUser ");
            builder.Append(" WHERE ");
            builder.Append(" ExecutionHardware_ID=@ExecutionHardware_ID ");
            return this.CurrDB.ExecuteNonQuery(builder.ToString(), tran, new object[] { 
                model.ExecutionHardware_ID, model.CustomerInfo_ID, model.Supplier_ID, model.ExecutionHardware_Type, model.ExecutionHardware_Code, model.ExecutionHardware_ApplyMan, model.ExecutionHardware_ApplyDepartment, model.ExecutionHardware_Name, model.ExecutionHardware_Count, model.ExecutionHardware_Version, model.ExecutionHardware_ProcurementTime, model.ExecutionHardware_ProcurementCosts, model.ExecutionHardware_ProcurementStaff, model.ExecutionHardware_BearTheCostSide, model.ExecutionHardware_Brand, model.ExecutionHardware_Status, 
                model.ExecutionHardware_ReceiveTime, model.ExecutionHardware_InstallTime, model.ExecutionHardware_Remark, model.CreateTime, model.CreateUser, model.UpdateTime, model.UpdateUser
             });
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.Update(null, strUpdateColumns, strCondition, param);
        }

        internal int Update(DbTransaction tran, string strUpdateColumns, string strCondition, params object[] param)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(" ExecutionHardware ");
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

