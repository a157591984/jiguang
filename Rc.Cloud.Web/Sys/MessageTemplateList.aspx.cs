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
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Sys
{
    public partial class MessageTemplateList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPath = Request.Url.ToString();
            Module_Id = "90100500";
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        [WebMethod]
        public static string GetDataList(string UserName, int PageIndex, int PageSize)
        {
            try
            {
                UserName = UserName.Filter();
                string strWhere = string.Empty;
                strWhere = "1=1";
                if (!string.IsNullOrEmpty(UserName))
                {
                    strWhere += " AND UserName like '%" + UserName + "%'";
                }
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                BLL_SendMessageTemplate bll = new BLL_SendMessageTemplate();
                DataTable dt = bll.GetListByPage(strWhere, "SType", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                intRecordCount = bll.GetRecordCount(strWhere);
                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        SendSMSTemplateId = item["SendSMSTemplateId"].ToString(),
                        STypeName = Rc.Common.EnumService.GetDescription<SMSPAYTemplateEnum>(item["SType"].ToString()),
                        UserName = item["UserName"].ToString(),
                        PassWord = item["PassWord"].ToString(),
                        MsgUrl = item["MsgUrl"].ToString(),
                        IsStart = item["IsStart"].ToString() == "1" ? "启用" : "未启用",
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
        /// <summary>
        /// 删除
        /// </summary>
        [WebMethod]
        public static string DelData(string SendSMSTemplateId)
        {
            try
            {
                SendSMSTemplateId = SendSMSTemplateId.Filter();
                if (new BLL_SendMessageTemplate().Delete(SendSMSTemplateId))
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