namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_AudioVideoIntro
    {
        private readonly DAL_AudioVideoIntro dal = new DAL_AudioVideoIntro();

        public bool Add(Model_AudioVideoIntro model)
        {
            return this.dal.Add(model);
        }

        public List<Model_AudioVideoIntro> DataTableToList(DataTable dt)
        {
            List<Model_AudioVideoIntro> list = new List<Model_AudioVideoIntro>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_AudioVideoIntro item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string AudioVideoIntroId)
        {
            return this.dal.Delete(AudioVideoIntroId);
        }

        public bool DeleteList(string AudioVideoIntroIdlist)
        {
            return this.dal.DeleteList(AudioVideoIntroIdlist);
        }

        public bool Exists(string AudioVideoIntroId)
        {
            return this.dal.Exists(AudioVideoIntroId);
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

        public Model_AudioVideoIntro GetModel(string AudioVideoIntroId)
        {
            return this.dal.GetModel(AudioVideoIntroId);
        }

        public Model_AudioVideoIntro GetModelByCache(string AudioVideoIntroId)
        {
            string cacheKey = "Model_AudioVideoIntroModel-" + AudioVideoIntroId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AudioVideoIntroId);
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
            return (Model_AudioVideoIntro) cache;
        }

        public List<Model_AudioVideoIntro> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Model_AudioVideoIntro model)
        {
            return this.dal.Update(model);
        }
    }
}

