namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SysDepartment
    {
        private DAL_SysDepartment dal = new DAL_SysDepartment();
        private readonly DAL_SysDepartment DAL = new DAL_SysDepartment();

        public int Add(Model_SysDepartment model)
        {
            return this.DAL.Add(model);
        }

        public int DeleteByCondition(string strCondition, params object[] param)
        {
            return this.DAL.DeleteByCondition(strCondition, param);
        }

        public int DeleteByPK(string sysdepartment_id)
        {
            return this.DAL.DeleteByPK(sysdepartment_id);
        }

        public bool ExistsByCondition(string conditionStr, params object[] paramValues)
        {
            return this.DAL.ExistsByCondition(conditionStr, paramValues);
        }

        public bool ExistsByLogic(string sysdepartment_id)
        {
            return this.DAL.ExistsByLogic(sysdepartment_id);
        }

        public bool ExistsByPK(string sysdepartment_id)
        {
            return this.DAL.ExistsByPK(sysdepartment_id);
        }

        public DataSet GetDataList()
        {
            return this.dal.GetDataList();
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetDataSet(int recordNum, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysDepartmentCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetDataSet(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetListPaged(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetListPaged(strWhere, PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_SysDepartment GetModel_SysDepartmentByLogic(string sysdepartment_id)
        {
            return this.DAL.GetModel_SysDepartmentByLogic(sysdepartment_id);
        }

        public Model_SysDepartment GetModel_SysDepartmentByPK(string sysdepartment_id)
        {
            return this.DAL.GetModel_SysDepartmentByPK(sysdepartment_id);
        }

        public List<Model_SysDepartment> GetModel_SysDepartmentList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SysDepartmentList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysDepartment> GetModel_SysDepartmentListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.DAL.GetModel_SysDepartmentListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public List<Model_SysDepartment> GetModel_SysDepartmentListByPage(int pageSize, int pageIndex, string orderColumn, string orderType, out int recordCount, string strCondition, params object[] param)
        {
            recordCount = this.GetSysDepartmentCount(strCondition, param);
            if (recordCount == 0)
            {
                return null;
            }
            return this.DAL.GetModel_SysDepartmentListByPage(pageSize, pageIndex, orderColumn, orderType, strCondition, param);
        }

        public int GetSysDepartmentCount(string strCondition, params object[] param)
        {
            return this.DAL.GetSysDepartmentCount(strCondition, param);
        }

        public bool IsOrNotExists(Model_SysDepartment model, int i)
        {
            return this.dal.IsOrNotExists(model, i);
        }

        public int Update(Model_SysDepartment model)
        {
            return this.DAL.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.DAL.Update(strUpdateColumns, strCondition, param);
        }
    }
}

