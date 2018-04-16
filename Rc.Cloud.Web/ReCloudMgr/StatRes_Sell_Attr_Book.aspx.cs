using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class StatRes_Sell_Attr_Book : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10251500";
            if (!IsPostBack)
            {

            }
        }
        [WebMethod]
        public static string GetStatRes_Doc_Attr(string SAttrType, string SDocType, string SYear, int PageIndex, int PageSize)
        {
            try
            {
                string strWhere = string.Empty;
                BLL_StatRes_Doc_Attr bll = new BLL_StatRes_Doc_Attr();
                strWhere = "and SSaleCount>0 and SAttrType='" + SAttrType + "' and SDocType='" + SDocType + "' and SYear=" + SYear;
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                GH_PagerInfo<List<Model_StatRes_Doc_Attr>> pageInfo = bll.GetAllList(strWhere, "SYear desc,SDocType,SSaleCount desc", PageIndex, PageSize);
                List<Model_StatRes_Doc_Attr> list = pageInfo.PageData;
                List<object> listReturn = new List<object>();
                int inum = 1;
                foreach (var item in list)
                {
                    listReturn.Add(new
                    {
                        Num = inum + PageSize * (PageIndex - 1),
                        SYear = item.SYear,
                        SDocTypeName = item.SDocTypeName,
                        SData_Name = item.SData_Name,
                        SProductionCount = item.SProductionCount,
                        SDownloadCount = item.SDownloadCount,
                        SSaleCount = item.SSaleCount
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = pageInfo.CurrentPage,
                        PageSize = pageInfo.PageSize,
                        TotalCount = pageInfo.RecordCount,
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
    }
}