namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_F_User
    {
        private readonly DAL_F_User dal = new DAL_F_User();

        public bool Add(Model_F_User model)
        {
            return this.dal.Add(model);
        }

        public int APPReg(Model_F_User model, Model_f_user_token modelToken)
        {
            return this.dal.APPReg(model, modelToken);
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

        public bool DeleteList(string UserIdlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(UserIdlist, 0));
        }

        public bool DelFUser(string UserId)
        {
            return this.dal.DelFUser(UserId);
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

        public DataSet GetListPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListPage(strWhere, orderby, startIndex, endIndex);
        }

        public Model_F_User GetModel(string UserId)
        {
            return this.dal.GetModel(UserId);
        }

        public Model_F_User GetModel(string UserName, string Password)
        {
            return this.dal.GetModel(UserName, Password);
        }

        public Model_F_User GetModelA(string UserId, string Password)
        {
            return this.dal.GetModelA(UserId, Password);
        }

        public Model_F_User GetModelByCache(string UserId)
        {
            string cacheKey = "Model_F_UserModel-" + UserId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(UserId);
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
            return (Model_F_User) cache;
        }

        public Model_F_User GetModelByUserIdToken(string UserId, string Token, string productType)
        {
            return this.dal.GetModelByUserIdToken(UserId, Token, productType);
        }

        public Model_F_User GetModelByUserName(string UserName)
        {
            return this.dal.GetModelByUserName(UserName);
        }

        public List<Model_F_User> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public Model_F_User GetModelWhere(string strWhere)
        {
            return this.dal.GetModelstrWhere(strWhere);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountA(string strWhere)
        {
            return this.dal.GetRecordCountA(strWhere);
        }

        public bool resettingPwd(Model_F_User model, Model_Msg modelMsg)
        {
            return this.dal.resettingPwd(model, modelMsg);
        }

        public bool Update(Model_F_User model)
        {
            return this.dal.Update(model);
        }
    }
}

