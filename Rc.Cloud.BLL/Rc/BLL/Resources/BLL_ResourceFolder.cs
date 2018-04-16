namespace Rc.BLL.Resources
{
    using Rc.Common;
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLL_ResourceFolder
    {
        private readonly DAL_ResourceFolder dal = new DAL_ResourceFolder();

        public bool Add(Model_ResourceFolder model)
        {
            return this.dal.Add(model);
        }

        public List<Model_ResourceFolder> DataTableToList(DataTable dt)
        {
            List<Model_ResourceFolder> list = new List<Model_ResourceFolder>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_ResourceFolder item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ResourceFolder_Id)
        {
            return this.dal.Delete(ResourceFolder_Id);
        }

        public bool DeleteList(string ResourceFolder_Idlist)
        {
            return this.dal.DeleteList(PageValidate.SafeLongFilter(ResourceFolder_Idlist, 0));
        }

        public bool Exists(string ResourceFolder_Id)
        {
            return this.dal.Exists(ResourceFolder_Id);
        }

        public bool Exists(string ResourceFolder_ParentId, string ResourceFolder_Name)
        {
            return this.dal.Exists(ResourceFolder_ParentId, ResourceFolder_Name);
        }

        public bool Exists(string ResourceFolder_ParentId, string ResourceFolder_Name, string ResourceFolder_Id)
        {
            return this.dal.Exists(ResourceFolder_ParentId, ResourceFolder_Name, ResourceFolder_Id);
        }

        public bool Exists(string ResourceFolder_ParentId, string ResourceFolder_Name, string CreateFUser, string Resource_Type)
        {
            return this.dal.Exists(ResourceFolder_ParentId, ResourceFolder_Name, CreateFUser, Resource_Type);
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

        public Model_ResourceFolder GetModel(string ResourceFolder_Id)
        {
            return this.dal.GetModel(ResourceFolder_Id);
        }

        public Model_ResourceFolder GetModelA(string ResourceFolder_Id)
        {
            return this.dal.GetModelA(ResourceFolder_Id);
        }

        public Model_ResourceFolder GetModelByCache(string ResourceFolder_Id)
        {
            string cacheKey = "Model_ResourceFolderModel-" + ResourceFolder_Id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ResourceFolder_Id);
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
            return (Model_ResourceFolder) cache;
        }

        public List<Model_ResourceFolder> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSysFolderTree(string CreateFUser, string Resource_Class, string Resource_Type)
        {
            return this.dal.GetSysFolderTree(CreateFUser, Resource_Class, Resource_Type);
        }

        public DataSet GetTeacherCLPFolderTree(string ResourceFolder_Owner, string Resource_Type)
        {
            return this.dal.GetTeacherCLPFolderTree(ResourceFolder_Owner, Resource_Type);
        }

        public DataSet GetTeacherFolderTree(string ResourceFolder_Owner, string Resource_Class, string Resource_Type)
        {
            return this.dal.GetTeacherFolderTree(ResourceFolder_Owner, Resource_Class, Resource_Type);
        }

        public bool InitTchChapterAssemblyRF(string Resource_Type, string Resource_Class, string ResourceFolder_Owner)
        {
            return this.dal.InitTchChapterAssemblyRF(Resource_Type, Resource_Class, ResourceFolder_Owner);
        }

        public bool Update(Model_ResourceFolder model)
        {
            return this.dal.Update(model);
        }
    }
}

