namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysModuleFunctionUser
    {
        private readonly DAL_SysModuleFunctionUser dal = new DAL_SysModuleFunctionUser();

        public bool AddSysModuleFunctionUser(Model_SysModuleFunctionUser model)
        {
            return this.dal.AddSysModuleFunctionUser(model);
        }

        public List<Model_SysModuleFunctionUser> DataTableToList(DataTable dt)
        {
            List<Model_SysModuleFunctionUser> list = new List<Model_SysModuleFunctionUser>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysModuleFunctionUser item = new Model_SysModuleFunctionUser();
                    if ((dt.Rows[i]["User_ID"] != null) && (dt.Rows[i]["User_ID"].ToString() != ""))
                    {
                        item.User_ID = dt.Rows[i]["User_ID"].ToString();
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

        public DataSet GetAllSysModuleFunctionUserList()
        {
            return this.GetSysModuleFunctionUserList("");
        }

        public List<Model_SysModuleFunctionUser> GetModelList(string strWhere)
        {
            DataSet sysModuleFunctionUserList = this.dal.GetSysModuleFunctionUserList(strWhere);
            return this.DataTableToList(sysModuleFunctionUserList.Tables[0]);
        }

        public DataSet GetSysModuleFunctionList(string DoctorInfoId, string ModuleFirst)
        {
            return this.dal.GetSysModuleFunctionList(DoctorInfoId, ModuleFirst);
        }

        public int GetSysModuleFunctionUserCount(string strWhere)
        {
            return this.dal.GetSysModuleFunctionUserCount(strWhere);
        }

        public DataSet GetSysModuleFunctionUserList(string strWhere)
        {
            return this.dal.GetSysModuleFunctionUserList(strWhere);
        }

        public DataSet GetSysModuleFunctionUserList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetSysModuleFunctionUserList(Top, strWhere, filedOrder);
        }

        public DataSet GetSysModuleFunctionUserListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSysModuleFunctionUserListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_SysModuleFunctionUser GetSysModuleFunctionUserModel(string User_ID, string MODULEID, string FUNCTIONID)
        {
            return this.dal.GetSysModuleFunctionUserModel(User_ID, MODULEID, FUNCTIONID);
        }

        public DataSet GetUserModuleFunctionBySysCode(string DoctorInfoId, string ModuleFirst, string sysCode)
        {
            return this.dal.GetUserModuleFunctionBySysCode(DoctorInfoId, ModuleFirst, sysCode);
        }

        public int SetUserModuleFunction(string checkeds, string strDoctorId, string strModuleIdLike)
        {
            return this.dal.SetUserModuleFunction(checkeds, strDoctorId, strModuleIdLike);
        }

        public int SetUserModuleFunctionBySysCode(string checkeds, string strDoctorId, string strModuleIdLike, string sysCode)
        {
            return this.dal.SetUserModuleFunctionBySysCode(checkeds, strDoctorId, strModuleIdLike, sysCode);
        }
    }
}

