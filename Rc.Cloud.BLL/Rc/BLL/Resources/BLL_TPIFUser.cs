namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_TPIFUser
    {
        private readonly DAL_TPIFUser dal = new DAL_TPIFUser();

        public bool Add(Model_TPIFUser model)
        {
            return this.dal.Add(model);
        }

        public List<Model_TPIFUser> DataTableToList(DataTable dt)
        {
            List<Model_TPIFUser> list = new List<Model_TPIFUser>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_TPIFUser item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ThirdPartyIFUser_Id)
        {
            return this.dal.Delete(ThirdPartyIFUser_Id);
        }

        public bool DeleteList(string ThirdPartyIFUser_Idlist)
        {
            return this.dal.DeleteList(ThirdPartyIFUser_Idlist);
        }

        public bool Exists(string ThirdPartyIFUser_Id)
        {
            return this.dal.Exists(ThirdPartyIFUser_Id);
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

        public Model_TPIFUser GetModel(string ThirdPartyIFUser_Id)
        {
            return this.dal.GetModel(ThirdPartyIFUser_Id);
        }

        public Model_TPIFUser GetModelByCache(string ThirdPartyIFUser_Id)
        {
            string cacheKey = "Model_TPIFUserModel-" + ThirdPartyIFUser_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ThirdPartyIFUser_Id);
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
            return (Model_TPIFUser) cache;
        }

        public Model_TPIFUser GetModelBySchoolUserName(string School, string UserName)
        {
            return this.dal.GetModelBySchoolUserName(School, UserName);
        }

        public Model_TPIFUser GetModelByUserName(string UserName)
        {
            return this.dal.GetModelByUserName(UserName);
        }

        public List<Model_TPIFUser> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_TPIFUser model)
        {
            return this.dal.Update(model);
        }
    }
}

