namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using Rc.Common.StrUtility;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_Formulations_Dict
    {
        private readonly DAL_Formulations_Dict dal = new DAL_Formulations_Dict();

        public bool Add(Model_Formulations_Dict model)
        {
            if (this.dal.Add(model))
            {
                this.ClearCacheFormulations_Dict();
                return true;
            }
            return false;
        }

        public void ClearCacheFormulations_Dict()
        {
            string cacheName = "Formulations_Dict";
            CacheClass.DeleteCache(cacheName);
        }

        public List<Model_Formulations_Dict> DataTableToList(DataTable dt)
        {
            List<Model_Formulations_Dict> list = new List<Model_Formulations_Dict>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_Formulations_Dict item = new Model_Formulations_Dict();
                    if ((dt.Rows[i]["Formulations_Dict_ID"] != null) && (dt.Rows[i]["Formulations_Dict_ID"].ToString() != ""))
                    {
                        item.Formulations_Dict_ID = dt.Rows[i]["Formulations_Dict_ID"].ToString();
                    }
                    if ((dt.Rows[i]["D_Name"] != null) && (dt.Rows[i]["D_Name"].ToString() != ""))
                    {
                        item.D_Name = dt.Rows[i]["D_Name"].ToString();
                    }
                    if ((dt.Rows[i]["D_PartentID"] != null) && (dt.Rows[i]["D_PartentID"].ToString() != ""))
                    {
                        item.D_PartentID = dt.Rows[i]["D_PartentID"].ToString();
                    }
                    if ((dt.Rows[i]["D_Value"] != null) && (dt.Rows[i]["D_Value"].ToString() != ""))
                    {
                        item.D_Value = new int?(int.Parse(dt.Rows[i]["D_Value"].ToString()));
                    }
                    if ((dt.Rows[i]["D_Code"] != null) && (dt.Rows[i]["D_Code"].ToString() != ""))
                    {
                        item.D_Code = dt.Rows[i]["D_Code"].ToString();
                    }
                    if ((dt.Rows[i]["D_Level"] != null) && (dt.Rows[i]["D_Level"].ToString() != ""))
                    {
                        item.D_Level = new int?(int.Parse(dt.Rows[i]["D_Level"].ToString()));
                    }
                    if ((dt.Rows[i]["D_Order"] != null) && (dt.Rows[i]["D_Order"].ToString() != ""))
                    {
                        item.D_Order = new int?(int.Parse(dt.Rows[i]["D_Order"].ToString()));
                    }
                    if ((dt.Rows[i]["D_CreateTime"] != null) && (dt.Rows[i]["D_CreateTime"].ToString() != ""))
                    {
                        item.D_CreateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["D_CreateTime"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(string Formulations_Dict_ID)
        {
            if (this.dal.Delete(Formulations_Dict_ID))
            {
                this.ClearCacheFormulations_Dict();
                return true;
            }
            return false;
        }

        public bool DeleteList(string Formulations_Dict_IDlist)
        {
            if (this.dal.DeleteList(Formulations_Dict_IDlist))
            {
                this.ClearCacheFormulations_Dict();
                return true;
            }
            return false;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Model_Formulations_Dict> GetFormulations_Dict_List(string strWhere)
        {
            return this.DataTableToList(this.dal.GetFormulations_Dict_List(strWhere).Tables[0]);
        }

        public List<Model_Formulations_Dict> GetFormulations_DictModelList(int recordNum, string orderColumn, string orderType, string strCondition, params object[] param)
        {
            return this.dal.GetFormulations_DictModelList(recordNum, orderColumn, orderType, strCondition, param);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListPaged(string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetListPaged(D_Name, PageIndex, PageSize, out rCount, out pCount);
        }

        public DataSet GetListPaged(string content, int PageIndex, int PageSize, out int rCount, out int pCount, int type)
        {
            return this.dal.GetListPaged(content, PageIndex, PageSize, out rCount, out pCount, type);
        }

        public Model_Formulations_Dict GetModel(string Formulations_Dict_ID)
        {
            return this.dal.GetModel(Formulations_Dict_ID);
        }

        public List<Model_Formulations_Dict> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Model_Formulations_Dict> GetModelListByPaged(string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetListByPaged(D_Name, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Model_Formulations_Dict> GetModelListPaged(string D_Name, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetListPaged(D_Name, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_Formulations_Dict model)
        {
            if (this.dal.Update(model))
            {
                this.ClearCacheFormulations_Dict();
                return true;
            }
            return false;
        }

        public int updateTran(List<string> sqlList)
        {
            return this.dal.updateTran(sqlList);
        }

        public List<Model_Formulations_Dict> GetCacheFormulations_Dict
        {
            get
            {
                if (CacheClass.GetIfEnabledCache())
                {
                    string str = "Formulations_Dict";
                    object cache = CacheClass.GetCache(str);
                    if (cache == null)
                    {
                        try
                        {
                            cache = this.GetFormulations_Dict_List("");
                            if (cache != null)
                            {
                                CacheClass.AddCache(str, cache);
                            }
                        }
                        catch
                        {
                        }
                    }
                    return (List<Model_Formulations_Dict>) cache;
                }
                object obj3 = this.GetFormulations_Dict_List("");
                string cacheName = "Formulations_Dict";
                CacheClass.DeleteCache(cacheName);
                return (List<Model_Formulations_Dict>) obj3;
            }
        }
    }
}

