namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysModuleFunction
    {
        private readonly DAL_SysModuleFunction dal = new DAL_SysModuleFunction();

        public bool AddSysModuleFunction(Model_SysModuleFunction model)
        {
            return this.dal.AddSysModuleFunction(model);
        }

        public List<Model_SysModuleFunction> DataTableToList(DataTable dt)
        {
            List<Model_SysModuleFunction> list = new List<Model_SysModuleFunction>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysModuleFunction item = new Model_SysModuleFunction();
                    if ((dt.Rows[i]["ModuleID"] != null) && (dt.Rows[i]["ModuleID"].ToString() != ""))
                    {
                        item.ModuleID = dt.Rows[i]["ModuleID"].ToString();
                    }
                    if ((dt.Rows[i]["FunctionId"] != null) && (dt.Rows[i]["FunctionId"].ToString() != ""))
                    {
                        item.FunctionId = dt.Rows[i]["FunctionId"].ToString();
                    }
                    if ((dt.Rows[i]["IsDefault"] != null) && (dt.Rows[i]["IsDefault"].ToString() != ""))
                    {
                        item.IsDefault = new int?(int.Parse(dt.Rows[i]["IsDefault"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool DeleteSysModuleFunction(string ModuleID, string FunctionId)
        {
            return this.dal.DeleteSysModuleFunction(ModuleID, FunctionId);
        }

        public DataSet GetAllSysModuleFunctionList()
        {
            return this.GetSysModuleFunctionList("");
        }

        public int GetSysModuleFunctionCount(string strWhere)
        {
            return this.dal.GetSysModuleFunctionCount(strWhere);
        }

        public DataSet GetSysModuleFunctionList(string strWhere)
        {
            return this.dal.GetSysModuleFunctionList(strWhere);
        }

        public DataSet GetSysModuleFunctionList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetSysModuleFunctionList(Top, strWhere, filedOrder);
        }

        public DataSet GetSysModuleFunctionListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSysModuleFunctionListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_SysModuleFunction GetSysModuleFunctionModel(string ModuleID, string FunctionId)
        {
            return this.dal.GetSysModuleFunctionModel(ModuleID, FunctionId);
        }

        public List<Model_SysModuleFunction> GetSysModuleFunctionModelList(string strWhere)
        {
            DataSet sysModuleFunctionList = this.dal.GetSysModuleFunctionList(strWhere);
            return this.DataTableToList(sysModuleFunctionList.Tables[0]);
        }

        public bool SetModuleFunction(string ModuleId, string FunctionIds)
        {
            return this.dal.SetModuleFunction(ModuleId, FunctionIds);
        }

        public bool SetModuleFunctionByModuleIdAndSysCode(string ModuleId, string FunctionIds, string sysCode)
        {
            return this.dal.SetModuleFunctionByModuleIdAndSysCode(ModuleId, FunctionIds, sysCode);
        }

        public bool UpdateSysModuleFunction(Model_SysModuleFunction model)
        {
            return this.dal.UpdateSysModuleFunction(model);
        }
    }
}

