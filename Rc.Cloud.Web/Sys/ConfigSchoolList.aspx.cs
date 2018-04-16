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
    public partial class ConfigSchoolList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90200200";
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string Key, string Name, int PageIndex, int PageSize)
        {
            try
            {
                BLL_ConfigSchool bll = new BLL_ConfigSchool();
                Key = Key.Filter();
                Name = Name.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(Key))
                {
                    strWhere += " and ConfigEnum='" + Key + "' ";
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    strWhere += string.Format(" and (D_Name like '%{0}%' or School_Name like '%{0}%') ", Name);
                }

                string orderBy = "D_Order";
                dt = bll.GetListByPage(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        ConfigEnum = dt.Rows[i]["ConfigEnum"].ToString(),
                        School_Id = dt.Rows[i]["School_Id"].ToString(),
                        School_Name = dt.Rows[i]["School_Name"].ToString(),
                        D_Name = dt.Rows[i]["D_Name"].ToString(),
                        D_Value = dt.Rows[i]["D_Value"].ToString(),
                        D_PublicValue = dt.Rows[i]["D_PublicValue"].ToString(),
                        D_Type = dt.Rows[i]["D_Type"].ToString(),
                        D_TypeName = Rc.Common.EnumService.GetDescription<ConfigSchoolTypeEnum>(dt.Rows[i]["D_Type"].ToString()),
                        D_Order = dt.Rows[i]["D_Order"].ToString(),
                        D_Remark = dt.Rows[i]["D_Remark"].ToString(),
                        SchoolIP = dt.Rows[i]["SchoolIP"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["D_CreateTime"].ToString())
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
        public static string DeleteData(string ConfigEnum)
        {
            try
            {
                if (new BLL_ConfigSchool().Delete(ConfigEnum))
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