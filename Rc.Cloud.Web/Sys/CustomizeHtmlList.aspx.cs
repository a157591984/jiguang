using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using System.Data;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Sys
{
    public partial class CustomizeHtmlList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90100700";
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        [WebMethod]
        public static string GetDataList(int PageIndex, int PageSize)
        {
            try
            {
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                BLL_CustomizeHtml bll = new BLL_CustomizeHtml();
                DataTable dt = bll.GetListByPage("", "CreateTime desc", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                intRecordCount = bll.GetRecordCount("");
                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        CustomizeHtml_Id = item["CustomizeHtml_Id"].ToString(),
                        HtmlType = item["HtmlType"].ToString(),
                        HtmlContent = pfunction.GetSubstring(pfunction.DeleteHTML(item["HtmlContent"].ToString()), 200, true),
                        CreateTime = pfunction.ConvertToLongDateTime(item["CreateTime"].ToString())
                    });
                }
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = intRecordCount,
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
                    err = "error"
                });
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        [WebMethod]
        public static string DelData(string CustomizeHtml_Id)
        {
            try
            {
                if (new BLL_CustomizeHtml().Delete(CustomizeHtml_Id.Filter()))
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