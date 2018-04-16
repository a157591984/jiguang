using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;
using System.Data;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class SchoolSMSList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90200400";
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string Name, int PageIndex, int PageSize)
        {
            try
            {
                BLL_SchoolSMS bll = new BLL_SchoolSMS();
                Name = Name.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(Name)) strWhere += " and (School_Id like '%" + Name + "%' or School_Name like '%" + Name + "%') ";

                string orderBy = "School_Id";
                dt = bll.GetListByPage(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        School_Id = dt.Rows[i]["School_Id"].ToString(),
                        School_Name = dt.Rows[i]["School_Name"].ToString(),
                        SMSCount = dt.Rows[i]["SMSCount"].ToString(),
                        Remark = dt.Rows[i]["Remark"].ToString()
                    });
                }

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
                        list = listReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

        [WebMethod]
        public static string DeleteData(string School_Id)
        {
            try
            {
                string sql = string.Format("delete from SchoolSMS where School_Id='{0}';delete from SchoolSMS_Person where School_Id='{0}'", School_Id.Filter());
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}