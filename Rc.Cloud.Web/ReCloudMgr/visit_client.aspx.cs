using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class visit_client : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "20200100";
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        [WebMethod]
        public static string GetList(string DateType, string TeacherName, int PageSize, int PageIndex)
        {
            try
            {
                DateType = DateType.Filter();
                TeacherName = TeacherName.Filter();

                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " AND DateType = '" + DateType + "'";
                }
                if (!string.IsNullOrEmpty(TeacherName))
                {
                    strWhere += " and TeacherName like '%" + TeacherName + "%' ";
                }

                DataTable dt = new DataTable();
                BLL_StatsVisitClient bll = new BLL_StatsVisitClient();
                dt = bll.GetListByPageNew(strWhere, " DateData desc,VisitCount_All desc,TeacherName", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        DateType = dt.Rows[i]["DateType"].ToString(),
                        DateData = dt.Rows[i]["DateData"].ToString(),
                        TeacherId = dt.Rows[i]["TeacherId"].ToString(),
                        TeacherName = dt.Rows[i]["TeacherName"].ToString(),
                        TrueName = dt.Rows[i]["TrueName"].ToString(),
                        VisitCount_All = dt.Rows[i]["VisitCount_All"].ToString(),
                        VisitCount_Cloud = dt.Rows[i]["VisitCount_Cloud"].ToString(),
                        VisitCount_Own = dt.Rows[i]["VisitCount_Own"].ToString(),
                        VisitFile_All = dt.Rows[i]["VisitFile_All"].ToString(),
                        VisitFile_Cloud = dt.Rows[i]["VisitFile_Cloud"].ToString(),
                        VisitFile_Own = dt.Rows[i]["VisitFile_Own"].ToString(),
                        CreateOwnCount_All = dt.Rows[i]["CreateOwnCount_All"].ToString(),
                        CreateOwnCount_Plan = dt.Rows[i]["CreateOwnCount_Plan"].ToString(),
                        CreateOwnCount_TestPaper = dt.Rows[i]["CreateOwnCount_TestPaper"].ToString()
                    });
                    inum++;
                }
                if (inum > 1)
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
                    err = ex.Message.ToString()
                });
            }
        }

    }
}