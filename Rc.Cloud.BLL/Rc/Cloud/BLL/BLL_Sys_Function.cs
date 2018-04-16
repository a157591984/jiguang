namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_Sys_Function
    {
        private readonly DAL_Sys_Function dal = new DAL_Sys_Function();

        public bool AddSys_Function(Model_Sys_Function model)
        {
            return this.dal.AddSys_Function(model);
        }

        public List<Model_Sys_Function> DataTableToSys_FunctionModelList(DataTable dt)
        {
            List<Model_Sys_Function> list = new List<Model_Sys_Function>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Sys_Function item = new Model_Sys_Function();
                    if ((dt.Rows[i]["FUNCTIONID"] != null) && (dt.Rows[i]["FUNCTIONID"].ToString() != ""))
                    {
                        item.FUNCTIONID = dt.Rows[i]["FUNCTIONID"].ToString();
                    }
                    if ((dt.Rows[i]["FUNCTIONName"] != null) && (dt.Rows[i]["FUNCTIONName"].ToString() != ""))
                    {
                        item.FUNCTIONName = dt.Rows[i]["FUNCTIONName"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool DeleteSys_FunctionByID(string FUNCTIONID)
        {
            return this.dal.DeleteSys_FunctionByID(FUNCTIONID);
        }

        public bool DeleteSys_FunctionListByID(string FUNCTIONIDlist)
        {
            return this.dal.DeleteSys_FunctionListByID(FUNCTIONIDlist);
        }

        public DataSet GetAllSys_FunctionList()
        {
            return this.GetSys_FunctionList(" 1=1 order by convert(int,FunctionId) ");
        }

        public int GetSys_FunctionCount(string strWhere)
        {
            return this.dal.GetSys_FunctionCount(strWhere);
        }

        public DataSet GetSys_FunctionList(string strWhere)
        {
            return this.dal.GetSys_FunctionList(strWhere);
        }

        public DataSet GetSys_FunctionList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetSys_FunctionList(Top, strWhere, filedOrder);
        }

        public DataSet GetSys_FunctionListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSys_FunctionListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_Sys_Function GetSys_FunctionModel(string FUNCTIONID)
        {
            return this.dal.GetSys_FunctionModel(FUNCTIONID);
        }

        public List<Model_Sys_Function> GetSys_FunctionModelList(string strWhere)
        {
            DataSet set = this.dal.GetSys_FunctionList(strWhere);
            return this.DataTableToSys_FunctionModelList(set.Tables[0]);
        }

        public DataSet GetSysModuleFunctionByModuleID(string ModuleId)
        {
            return this.dal.GetSysModuleFunctionByModuleID(ModuleId);
        }

        public bool UpdateSys_FunctionByID(Model_Sys_Function model)
        {
            return this.dal.UpdateSys_FunctionByID(model);
        }
    }
}

