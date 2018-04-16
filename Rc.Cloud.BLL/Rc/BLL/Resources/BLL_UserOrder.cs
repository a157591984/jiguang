namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_UserOrder
    {
        private readonly DAL_UserOrder dal = new DAL_UserOrder();

        public bool Add(Model_UserOrder model)
        {
            return this.dal.Add(model);
        }

        public List<Model_UserOrder> DataTableToList(DataTable dt)
        {
            List<Model_UserOrder> list = new List<Model_UserOrder>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_UserOrder item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string UserOrder_Id)
        {
            return this.dal.Delete(UserOrder_Id);
        }

        public bool DeleteList(string UserOrder_Idlist)
        {
            return this.dal.DeleteList(UserOrder_Idlist);
        }

        public bool Exists(string UserOrder_Id)
        {
            return this.dal.Exists(UserOrder_Id);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
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

        public DataSet GetListByPageALLOrderList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageALLOrderList(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListByPageOrderList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageOrderList(strWhere, orderby, startIndex, endIndex);
        }

        public Model_UserOrder GetModel(string UserOrder_Id)
        {
            return this.dal.GetModel(UserOrder_Id);
        }

        public Model_UserOrder GetModelByCache(string UserOrder_Id)
        {
            string cacheKey = "UserOrderModel-" + UserOrder_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserOrder_Id);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Model_UserOrder) cache;
        }

        public Model_UserOrder GetModelByOrderNo(string UserOrder_No)
        {
            return this.dal.GetModelByOrderNo(UserOrder_No);
        }

        public List<Model_UserOrder> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountALL(string strWhere)
        {
            return this.dal.GetRecordCountALL(strWhere);
        }

        public bool Update(Model_UserOrder model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateAndAddUserBuyResources(Model_UserOrder model, Model_UserBuyResources buyModel)
        {
            return this.dal.UpdateAndAddUserBuyResources(model, buyModel);
        }
    }
}

