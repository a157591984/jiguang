namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SysProduct
    {
        private readonly DAL_SysProduct dal = new DAL_SysProduct();

        public bool Add(Model_SysProduct model)
        {
            return this.dal.Add(model);
        }

        public bool AddExists(Model_SysProduct model)
        {
            return this.dal.AddExists(model);
        }

        public List<Model_SysProduct> DataTableToList(DataTable dt)
        {
            List<Model_SysProduct> list = new List<Model_SysProduct>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysProduct item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string SysCode)
        {
            return this.dal.Delete(SysCode);
        }

        public bool DeleteList(string SysCodelist)
        {
            return this.dal.DeleteList(SysCodelist);
        }

        public bool EditExists(Model_SysProduct model)
        {
            return this.dal.EideExists(model);
        }

        public bool Exists(string SysCode)
        {
            return this.dal.Exists(SysCode);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetCodeList(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetSysCodeList(strWhere, PageIndex, PageSize, out rCount, out pCount);
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

        public Model_SysProduct GetModel(string SysCode)
        {
            return this.dal.GetModel(SysCode);
        }

        public List<Model_SysProduct> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_SysProduct model)
        {
            return this.dal.Update(model);
        }
    }
}

