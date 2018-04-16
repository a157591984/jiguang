using Newtonsoft.Json;
using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using Rc.Common;

namespace Rc.Cloud.Web.Sys
{
    public partial class ClientTabList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90500500";
            if (!IsPostBack)
            {
                ddlTabType.Items.Add(new ListItem("Tab类型", ""));
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.ClientTabTypeEnum)))
                {
                    ddlTabType.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.ClientTabTypeEnum>(item.ToString()), item.ToString()));
                }
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        [WebMethod]
        public static string GetDataList(string TabName, string TabType, int PageIndex, int PageSize)
        {
            try
            {
                TabName = TabName.Filter();
                TabType = TabType.Filter();
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(TabName))
                {
                    strWhere += string.Format(" and (TabName like '%{0}%' or Tabindex like '%{0}%') ", TabName);
                }
                if (!string.IsNullOrEmpty(TabType))
                {
                    strWhere += string.Format(" and TabType='{0}' ", TabType);
                }
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                BLL_ClientTab bll = new BLL_ClientTab();
                DataTable dt = bll.GetListByPage(strWhere, "TabType,CreateTime", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                intRecordCount = bll.GetRecordCount(strWhere);
                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        Tabindex = item["Tabindex"].ToString(),
                        TabName = item["TabName"].ToString(),
                        TabType = EnumService.GetDescription<Rc.Model.Resources.ClientTabTypeEnum>(item["TabType"].ToString()),
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
        public static string DelData(string Tabindex)
        {
            try
            {
                if (new BLL_ClientTab().Delete(Tabindex.Filter()))
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