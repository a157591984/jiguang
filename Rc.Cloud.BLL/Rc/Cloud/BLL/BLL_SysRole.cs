namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_SysRole
    {
        private readonly DAL_SysRole dal = new DAL_SysRole();

        public bool AddSysRole(Model_SysRole model)
        {
            return this.dal.AddSysRole(model);
        }

        public List<Model_SysRole> DataTableToList(DataTable dt)
        {
            List<Model_SysRole> list = new List<Model_SysRole>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Model_SysRole item = new Model_SysRole();
                    if ((dt.Rows[i]["SysRole_ID"] != null) && (dt.Rows[i]["SysRole_ID"].ToString() != ""))
                    {
                        item.SysRole_ID = dt.Rows[i]["SysRole_ID"].ToString();
                    }
                    if ((dt.Rows[i]["SysRole_Name"] != null) && (dt.Rows[i]["SysRole_Name"].ToString() != ""))
                    {
                        item.SysRole_Name = dt.Rows[i]["SysRole_Name"].ToString();
                    }
                    if ((dt.Rows[i]["SysRole_Order"] != null) && (dt.Rows[i]["SysRole_Order"].ToString() != ""))
                    {
                        item.SysRole_Order = new int?(int.Parse(dt.Rows[i]["SysRole_Order"].ToString()));
                    }
                    if ((dt.Rows[i]["SysRole_Enable"] != null) && (dt.Rows[i]["SysRole_Enable"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["SysRole_Enable"].ToString() == "1") || (dt.Rows[i]["SysRole_Enable"].ToString().ToLower() == "true"))
                        {
                            item.SysRole_Enable = true;
                        }
                        else
                        {
                            item.SysRole_Enable = false;
                        }
                    }
                    if ((dt.Rows[i]["SysRole_Remark"] != null) && (dt.Rows[i]["SysRole_Remark"].ToString() != ""))
                    {
                        item.SysRole_Remark = dt.Rows[i]["SysRole_Remark"].ToString();
                    }
                    if ((dt.Rows[i]["CreateTime"] != null) && (dt.Rows[i]["CreateTime"].ToString() != ""))
                    {
                        item.CreateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateTime"].ToString()));
                    }
                    if ((dt.Rows[i]["CreateUser"] != null) && (dt.Rows[i]["CreateUser"].ToString() != ""))
                    {
                        item.CreateUser = dt.Rows[i]["CreateUser"].ToString();
                    }
                    if ((dt.Rows[i]["UpdateTime"] != null) && (dt.Rows[i]["UpdateTime"].ToString() != ""))
                    {
                        item.UpdateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["UpdateTime"].ToString()));
                    }
                    if ((dt.Rows[i]["UpdateUser"] != null) && (dt.Rows[i]["UpdateUser"].ToString() != ""))
                    {
                        item.UpdateUser = dt.Rows[i]["UpdateUser"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool ExistsSysRole(Model_SysRole mcd, string type)
        {
            return this.dal.ExistsSysRole(mcd, type);
        }

        public DataSet GetSysRoleList()
        {
            return this.dal.GetSysRoleList();
        }

        public DataSet GetSysRoleList(string strWhere)
        {
            return this.dal.GetSysRoleList(strWhere);
        }

        public DataSet GetSysRoleList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetSysRoleList(Top, strWhere, filedOrder);
        }

        public Model_SysRole GetSysRoleModel(string SysRole_ID)
        {
            return this.dal.GetSysRoleModel(SysRole_ID);
        }

        public List<Model_SysRole> GetSysRoleModelList(Model_SysRoleParameter parameter, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetSysRoleList(parameter, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }

        public bool UpdateSysRoleByID(Model_SysRole model)
        {
            return this.dal.UpdateSysRoleByID(model);
        }
    }
}

