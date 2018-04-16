namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_DatabaseCopyLibrary
    {
        private DAL_DatabaseCopyLibrary dal = new DAL_DatabaseCopyLibrary();

        public int Add(Model_DatabaseCopyLibrary model)
        {
            return this.dal.Add(model);
        }

        public int DeleteByPK(string customervisit_id)
        {
            return this.dal.DeleteByPK(customervisit_id);
        }

        public DataSet GetListByID(string id)
        {
            return this.dal.GetListByID(id);
        }

        public DataSet GetListPaged(Model_DatabaseCopyLibraryParam model, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetListPaged(model, PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_DatabaseCopyLibrary GetModel(string id)
        {
            return this.dal.GetModel(id);
        }

        public Model_DatabaseCopyLibrary GetModel_DatabaseCopyLibraryByPK(string id)
        {
            return this.dal.GetModel_DatabaseCopyLibraryByPK(id);
        }

        public int Update(Model_DatabaseCopyLibrary model)
        {
            return this.dal.Update(model);
        }

        public int Update(string strUpdateColumns, string strCondition, params object[] param)
        {
            return this.dal.Update(strUpdateColumns, strCondition, param);
        }
    }
}

