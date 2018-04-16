namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using Rc.Common.StrUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_SysModule
    {
        private readonly DAL_SysModule dal = new DAL_SysModule();

        public bool AddSysModule(Model_SysModule model)
        {
            return this.dal.AddSysModule(model);
        }

        public List<Model_SysModule> DataTableToList(DataTable dt)
        {
            List<Model_SysModule> list = new List<Model_SysModule>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysModule item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool DeleteSysModuleByID(string module_ID)
        {
            return this.dal.DeleteSysModuleByID(module_ID);
        }

        public bool DeleteSysModuleBySyscodeAndModuleID(string MODULEID)
        {
            return this.dal.DeleteSysModuleBySyscodeAndModuleID(MODULEID);
        }

        public bool DeleteSysModuleListByModuleID(string MODULEIDlist)
        {
            return this.dal.DeleteSysModuleListByModuleID(MODULEIDlist);
        }

        public bool ExistsSysModule(Model_SysModule model, string type)
        {
            return this.dal.ExistsSysModule(model, type);
        }

        public bool ExistsSysModuleByID(string MODULEID)
        {
            return this.dal.ExistsSysModuleByID(MODULEID);
        }

        public DataSet GetAllSysModuleList()
        {
            return this.GetSysModuleList("");
        }

        public DataTable GetListByCache()
        {
            string cacheName = "MODEL_SysModuleList";
            object cache = CacheClass.GetCache(cacheName);
            cache = null;
            if (cache == null)
            {
                try
                {
                    cache = this.GetSysModuleList("").Tables[0];
                    if (cache != null)
                    {
                        int num = 1;
                        CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                    }
                }
                catch
                {
                }
            }
            return (DataTable) cache;
        }

        public string GetModule_PathBySetMap(string Module_Id, string m_Type)
        {
            string[] strArray = this.GetSetMapByCache(Module_Id, m_Type).Split(new char[] { '_' });
            string str2 = "";
            int num = 0;
            foreach (string str3 in strArray)
            {
                if (!(str3 == "根目录") && !(str3.ToUpper() == "ROOT"))
                {
                    object obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, " <div class=\"div_right_title_001\" id='div_right_title_", num, "'>" }) + str3 + "</div>";
                    num++;
                    if (num != strArray.Length)
                    {
                        str2 = str2 + "<div class=\"div_right_title_002\"></div>";
                    }
                }
            }
            return str2;
        }

        public string GetModule_PathBySetMapBySysCode(string Module_Id, string m_Type)
        {
            string[] strArray = this.GetSetMapByCacheBySysCode(Module_Id, m_Type).Split(new char[] { '_' });
            string str2 = "";
            str2 = str2 + "<ol class='breadcrumb'>";
            foreach (string str3 in strArray)
            {
                if (!(str3 == "根目录") && !(str3.ToUpper() == "ROOT"))
                {
                    str2 = str2 + string.Format("<li><a href='javascript:;'>{0}</a></li>", str3);
                }
            }
            return (str2 + "</ol>");
        }

        public DataSet GetModuleListBySysCode(string strWhere)
        {
            return this.dal.GetModuleListBySysCode(strWhere);
        }

        public DataSet GetModuleListBySysCode_Power(string User_ID, string SysRole_IDS)
        {
            return this.dal.GetModuleListBySysCode_Power(User_ID, SysRole_IDS);
        }

        public DataTable GetOwenModuleListByCache(string DoctorInfo_ID, string SysRole_IDS)
        {
            string cacheName = "Owen_TopTree_" + DoctorInfo_ID;
            object cache = CacheClass.GetCache(cacheName);
            if (cache == null)
            {
                try
                {
                    cache = this.GetSysModuleList_Power(DoctorInfo_ID, SysRole_IDS).Tables[0];
                    if (cache != null)
                    {
                        int num = 1;
                        CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                    }
                }
                catch
                {
                }
            }
            return (DataTable) cache;
        }

        public DataTable GetOwenModuleListByCacheBySysCode(string User_ID, string SysRole_IDS)
        {
            string cacheName = "Owen_TopTree_" + clsUtility.GetSysCode() + "_" + User_ID;
            object cache = CacheClass.GetCache(cacheName);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModuleListBySysCode_Power(User_ID, SysRole_IDS).Tables[0];
                    if (cache != null)
                    {
                        int num = 1;
                        CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                    }
                }
                catch
                {
                }
            }
            return (DataTable) cache;
        }

        public DataSet GetPM_Detect_ConfigList()
        {
            return this.dal.GetPM_Detect_ConfigList();
        }

        public string GetSetMap(string ModuleId, string type)
        {
            return this.dal.GetSetMap(ModuleId, type);
        }

        public string GetSetMapByCache(string ModuleId, string type)
        {
            string cacheName = "GetSetMap" + ModuleId + type;
            object cache = CacheClass.GetCache(cacheName);
            if (cache == null)
            {
                try
                {
                    cache = this.GetSetMap(ModuleId, type);
                    if (cache != null)
                    {
                        int num = 1;
                        CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public string GetSetMapByCacheBySysCode(string ModuleId, string type)
        {
            string cacheName = "GetSetMap_" + clsUtility.GetSysCode() + "_" + ModuleId + type;
            object cache = CacheClass.GetCache(cacheName);
            if (cache == null)
            {
                try
                {
                    cache = this.GetSetMapBySysCode(ModuleId, type);
                    if (cache != null)
                    {
                        int num = 1;
                        CacheClass.AddCache(cacheName, DateTime.Now.AddMinutes((double) num), cache);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public string GetSetMapBySysCode(string ModuleId, string type)
        {
            return this.dal.GetSetMapBySysCode(ModuleId, type);
        }

        public DataSet GetSysCodeList()
        {
            return this.dal.GetSysCodeList();
        }

        public int GetSysModuleCount(string strWhere)
        {
            return this.dal.GetSysModuleCount(strWhere);
        }

        public DataSet GetSysModuleForFirstBySysCode(string sysCode)
        {
            return this.dal.GetSysModuleForFirstBySysCode(sysCode);
        }

        public DataSet GetSysModuleForFirstLevel()
        {
            return this.dal.GetSysModuleForFirstLevel();
        }

        public DataSet GetSysModuleList(string strWhere)
        {
            return this.dal.GetSysModuleList(strWhere);
        }

        public DataSet GetSysModuleList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetSysModuleList(Top, strWhere, filedOrder);
        }

        public DataSet GetSysModuleList_Power(string DoctorInfo_ID, string SysRole_IDS)
        {
            return this.dal.GetSysModuleList_Power(DoctorInfo_ID, SysRole_IDS);
        }

        public DataSet GetSysModuleListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetSysModuleListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_SysModule GetSysModuleModelByID(string MODULEID)
        {
            return this.dal.GetSysModuleModelByID(MODULEID);
        }

        public Model_SysModule GetSysModuleModelBySyscodeAndModuleID(string moduleid)
        {
            return this.dal.GetSysModuleModelBySyscodeAndModuleID(moduleid);
        }

        public List<Model_SysModule> GetSysModuleModelList(string strWhere)
        {
            DataSet sysModuleList = this.dal.GetSysModuleList(strWhere);
            return this.DataTableToList(sysModuleList.Tables[0]);
        }

        public string GetURL(string code, string module)
        {
            return this.dal.GetURL(code, module);
        }

        public bool UpdateSysModuleBySyscodeAndModuleID(Model_SysModule model)
        {
            return this.dal.UpdateSysModuleBySyscodeAndModuleID(model);
        }
    }
}

