namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_Execution
    {
        private readonly DAL_V_Common_Dict dal = new DAL_V_Common_Dict();

        public List<MODEL_V_Common_Dict> DataTableToList(DataTable dt)
        {
            List<MODEL_V_Common_Dict> list = new List<MODEL_V_Common_Dict>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    MODEL_V_Common_Dict item = new MODEL_V_Common_Dict();
                    if ((dt.Rows[i]["Common_Dict_ID"] != null) && (dt.Rows[i]["Common_Dict_ID"].ToString() != ""))
                    {
                        item.Common_Dict_ID = dt.Rows[i]["Common_Dict_ID"].ToString();
                    }
                    if ((dt.Rows[i]["D_Name"] != null) && (dt.Rows[i]["D_Name"].ToString() != ""))
                    {
                        item.D_Name = dt.Rows[i]["D_Name"].ToString();
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
                    if ((dt.Rows[i]["D_Type"] != null) && (dt.Rows[i]["D_Type"].ToString() != ""))
                    {
                        item.D_Type = dt.Rows[i]["D_Type"].ToString();
                    }
                    if ((dt.Rows[i]["D_Remark"] != null) && (dt.Rows[i]["D_Remark"].ToString() != ""))
                    {
                        item.D_Remark = dt.Rows[i]["D_Remark"].ToString();
                    }
                    if ((dt.Rows[i]["D_CreateUser"] != null) && (dt.Rows[i]["D_CreateUser"].ToString() != ""))
                    {
                        item.D_CreateUser = dt.Rows[i]["D_CreateUser"].ToString();
                    }
                    if ((dt.Rows[i]["D_CreateTime"] != null) && (dt.Rows[i]["D_CreateTime"].ToString() != ""))
                    {
                        item.D_CreateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["D_CreateTime"].ToString()));
                    }
                    if ((dt.Rows[i]["D_ModifyUser"] != null) && (dt.Rows[i]["D_ModifyUser"].ToString() != ""))
                    {
                        item.D_ModifyUser = dt.Rows[i]["D_ModifyUser"].ToString();
                    }
                    if ((dt.Rows[i]["D_ModifyTime"] != null) && (dt.Rows[i]["D_ModifyTime"].ToString() != ""))
                    {
                        item.D_ModifyTime = new DateTime?(DateTime.Parse(dt.Rows[i]["D_ModifyTime"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<MODEL_V_Common_Dict> GetCommon_Dict_List(string strWhere)
        {
            return this.DataTableToList(this.dal.GetCommon_Dict_List(strWhere).Tables[0]);
        }

        public List<MODEL_V_Common_Dict> GetCommon_Dict_List(string strWhere, string D_Type)
        {
            return this.DataTableToList(this.dal.GetCommon_Dict_List(strWhere, D_Type).Tables[0]);
        }

        public List<MODEL_V_Common_Dict> GetCommon_Dict_ListPaged(string D_Name, string D_Type, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetCommon_Dict_ListPaged(D_Name, D_Type, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<MODEL_V_Common_Dict> GetCommon_Dict_ListPaged(string D_Name, string D_Type, string Hospital, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetCommon_Dict_ListPaged(D_Name, D_Type, Hospital, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetCommon_Dict_Table_List(string strWhere)
        {
            return this.dal.GetCommon_Dict_List(strWhere);
        }

        public DataSet GetCommon_DictByType(string strType)
        {
            return this.dal.GetCommon_DictByType(strType);
        }

        public DataSet GetCommon_target_List()
        {
            return this.dal.GetCommon_target_List();
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

        public MODEL_V_Common_Dict GetModel(string Common_Dict_ID)
        {
            return this.dal.GetModel(Common_Dict_ID);
        }

        public List<MODEL_V_Common_Dict> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }
    }
}

