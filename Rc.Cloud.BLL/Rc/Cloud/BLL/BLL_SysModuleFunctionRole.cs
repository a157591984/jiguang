namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysModuleFunctionRole
    {
        private readonly DAL_SysModuleFunctionRole dal = new DAL_SysModuleFunctionRole();

        public bool AddSysModuleFunctionRole(Model_SysModuleFunctionRole model)
        {
            return this.dal.AddSysModuleFunctionRole(model);
        }

        public List<Model_SysModuleFunctionRole> DataTableToList(DataTable dt)
        {
            List<Model_SysModuleFunctionRole> list = new List<Model_SysModuleFunctionRole>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysModuleFunctionRole item = new Model_SysModuleFunctionRole();
                    if ((dt.Rows[i]["SysRole_ID"] != null) && (dt.Rows[i]["SysRole_ID"].ToString() != ""))
                    {
                        item.SysRole_ID = dt.Rows[i]["SysRole_ID"].ToString();
                    }
                    if ((dt.Rows[i]["MODULEID"] != null) && (dt.Rows[i]["MODULEID"].ToString() != ""))
                    {
                        item.MODULEID = dt.Rows[i]["MODULEID"].ToString();
                    }
                    if ((dt.Rows[i]["FUNCTIONID"] != null) && (dt.Rows[i]["FUNCTIONID"].ToString() != ""))
                    {
                        item.FUNCTIONID = dt.Rows[i]["FUNCTIONID"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public DataSet GetAllSysModuleFunctionRoleList()
        {
            return this.GetSysModuleFunctionRoleList("");
        }

        public DataSet GetRoleModuleFunctionBySysCode(string SysRole_ID, string ModuleFirst, string sysCode)
        {
            return this.dal.GetRoleModuleFunctionBySysCode(SysRole_ID, ModuleFirst, sysCode);
        }

        public DataSet GetSysModuleFunctionList(string SysRole_ID, string ModuleFirst)
        {
            return this.dal.GetSysModuleFunctionList(SysRole_ID, ModuleFirst);
        }

        public int GetSysModuleFunctionRoleCount(string strWhere)
        {
            return this.dal.GetSysModuleFunctionRoleCount(strWhere);
        }

        public DataSet GetSysModuleFunctionRoleList(string strWhere)
        {
            return this.dal.GetSysModuleFunctionRoleList(strWhere);
        }

        public DataSet GetSysModuleFunctionRoleList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetSysModuleFunctionRoleList(Top, strWhere, filedOrder);
        }

        public DataSet GetSysModuleFunctionRoleListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSysModuleFunctionRoleListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_SysModuleFunctionRole GetSysModuleFunctionRoleModel(string SysRole_ID, string MODULEID, string FUNCTIONID)
        {
            return this.dal.GetSysModuleFunctionRoleModel(SysRole_ID, MODULEID, FUNCTIONID);
        }

        public List<Model_SysModuleFunctionRole> GetSysModuleFunctionRoleModelList(string strWhere)
        {
            DataSet sysModuleFunctionRoleList = this.dal.GetSysModuleFunctionRoleList(strWhere);
            return this.DataTableToList(sysModuleFunctionRoleList.Tables[0]);
        }

        public int SetRoleModuleFunction(string checkeds, string SysRole_ID, string strModuleIdLike)
        {
            return this.dal.SetRoleModuleFunction(checkeds, SysRole_ID, strModuleIdLike);
        }

        public int SetRoleModuleFunctionBySysCode(string checkeds, string strSysRole_ID, string strModuleIdLike, string sysCode)
        {
            return this.dal.SetRoleModuleFunctionBySysCode(checkeds, strSysRole_ID, strModuleIdLike, sysCode);
        }
    }
}

