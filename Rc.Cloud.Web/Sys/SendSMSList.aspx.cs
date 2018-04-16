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
    public partial class SendSMSList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90402000";
            if (!IsPostBack)
            {
                
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        [WebMethod]
        public static string GetDataList(string Mobile, int PageIndex, int PageSize)
        {
            try
            {
                Mobile = Mobile.Filter();
                string strWhere = string.Empty;
                strWhere = "1=1";
                if (!string.IsNullOrEmpty(Mobile))
                {
                    strWhere += " AND Mobile like '%" + Mobile + "%'";
                }
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                BLL_SendSMSRecord bll = new BLL_SendSMSRecord();
                DataTable dt = bll.GetListByPage(strWhere, "CTime DESC", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                intRecordCount = bll.GetRecordCount(strWhere);
                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        Mobile = item["Mobile"].ToString(),
                        Content = item["Content"].ToString(),
                        Status = item["Status"].ToString(),
                        CTime = pfunction.ConvertToLongDateTime(item["CTime"].ToString())
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

    }
}