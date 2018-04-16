namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_F_User
    {
        private readonly DAL_F_User dal = new DAL_F_User();

        public bool Add(Model_F_User model)
        {
            return this.dal.Add(model);
        }

        public List<Model_F_User> DataTableToList(DataTable dt)
        {
            List<Model_F_User> list = new List<Model_F_User>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_F_User item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string UserId)
        {
            return this.dal.Delete(UserId);
        }

        public bool Exists(string UserId)
        {
            return this.dal.Exists(UserId);
        }

        public bool Exists(Model_F_User f_user, string type)
        {
            return this.dal.Exists(f_user, type);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetDataList()
        {
            return this.dal.GetDataList();
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

        public DataSet GetListPaged(string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            return this.dal.GetListPaged(strWhere, PageIndex, PageSize, out rCount, out pCount);
        }

        public Model_F_User GetModel(string UserId)
        {
            return this.dal.GetModel(UserId);
        }

        public Model_F_User GetModel(string UserId, string Password)
        {
            return this.dal.GetModel(UserId, Password);
        }

        public List<Model_F_User> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_F_User model)
        {
            return this.dal.Update(model);
        }
    }
}

