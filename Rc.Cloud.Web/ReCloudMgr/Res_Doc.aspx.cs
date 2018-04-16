using Rc.Cloud.Model;
using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.BLL;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Res_Doc : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Type = string.Empty;//资源类型（class类型文件、testPaper类型文件、ScienceWord类型文件）
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string _t = string.Empty;
        protected string s = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "20300100";
            if (!IsPostBack)
            {

            }
        }
        [WebMethod]
        public static string GetStatRes_Doc(int PageIndex, int PageSize)
        {
            try
            {
                string strWhere = string.Empty;
                BLL_StatRes_Doc bll = new BLL_StatRes_Doc();
                strWhere = "and SDocType<>'f22bd0fd-73b6-4c73-9df0-8b3cb8489816' and SProductionCount>0";
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                GH_PagerInfo<List<Model_StatRes_Doc>> pageInfo = bll.GetAllList(strWhere, "SYear desc,SDocType,SProductionCount desc", PageIndex, PageSize);
                List<Model_StatRes_Doc> list = pageInfo.PageData;
                List<object> listReturn = new List<object>();
                int inum = 1;
                foreach (var item in list)
                {
                    listReturn.Add(new
                    {
                        Num = inum + PageSize * (PageIndex - 1),
                        SYear = item.SYear,
                        SDocTypeName = item.SDocTypeName,
                        SProductionCount = item.SProductionCount,
                        SDownloadCount = item.SDownloadCount,
                        SSaleCount = item.SSaleCount,
                        textbookversion = "<a href='javascript:void(0);'onclick=\"Show('StatRes_Doc_Attr.aspx?SAttrType=textbookversion&SYear=" + item.SYear + "&SDocType=" + item.SDocType + "')\">查看</a>",
                        subject = "<a href='javascript:void(0);'onclick=\"Show('StatRes_Doc_Attr.aspx?SAttrType=subject&SYear=" + item.SYear + "&SDocType=" + item.SDocType + "')\">查看</a>",
                        gradeterm = "<a href='javascript:void(0);'onclick=\"Show('StatRes_Doc_Attr.aspx?SAttrType=gradeterm&SYear=" + item.SYear + "&SDocType=" + item.SDocType + "')\">查看</a>",
                        area = "<a href='javascript:void(0);'onclick=\"Show('StatRes_Doc_Attr.aspx?SAttrType=area&SYear=" + item.SYear + "&SDocType=" + item.SDocType + "')\">查看</a>"
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

        protected void btnStatisticsData_Click(object sender, EventArgs e)
        {
            try
            {
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql("EXEC P_GenerateStatisticsData");
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("统计生产数据错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                   , ex.TargetSite.Name.ToString(), ex.Message));
            }
        }

    }
}