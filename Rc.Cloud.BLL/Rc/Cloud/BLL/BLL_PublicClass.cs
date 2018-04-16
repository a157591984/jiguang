namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class BLL_PublicClass
    {
        private DAL_PublicClass dal = new DAL_PublicClass();

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

        public List<MODEL_V_Common_Dict> GetCommon_Dict_List(string strWhere, string D_Type)
        {
            return this.DataTableToList(this.dal.GetCommon_Dict_List(strWhere, D_Type).Tables[0]);
        }

        public List<MODEL_V_Common_Dict> GetCommonMultiselect(string IsPy, string strWhere, string D_Type, string Hospital, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetCommonMultiselect(IsPy, strWhere, D_Type, Hospital, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<MODEL_V_Common_Dict> GetIntelligentAssociationList(string D_Type, string select, string strWhere, int PageIndex, int PageSize, out int rCount, out int pCount)
        {
            DataSet set = this.dal.GetIntelligentAssociationList(D_Type, select, strWhere, PageIndex, PageSize, out rCount, out pCount);
            return this.DataTableToList(set.Tables[0]);
        }
    }
}

