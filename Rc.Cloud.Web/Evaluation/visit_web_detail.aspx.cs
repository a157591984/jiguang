using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.BLL.Resources;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Evaluation
{
    public partial class visit_web_detail : Rc.Cloud.Web.Common.FInitData
    {
        public string TeacherId = string.Empty;
        public string ClassId = string.Empty;
        public string DateType = string.Empty;
        public string DateData = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TeacherId = Request.QueryString["TeacherId"].Filter();
            ClassId = Request.QueryString["ClassId"].Filter();
            DateType = Request.QueryString["DateType"].Filter();
            DateData = Request.QueryString["DateData"].Filter();

        }

        [WebMethod]
        public static string GetList(string TeacherId, string ClassId, string DateType, string DateData, int PageSize, int PageIndex)
        {
            try
            {
                TeacherId = TeacherId.Filter();
                ClassId = ClassId.Filter();
                DateType = DateType.Filter();
                DateData = DateData.Filter();

                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(TeacherId))
                {
                    strWhere += " AND t.user_id = '" + TeacherId + "'";
                }
                if (!string.IsNullOrEmpty(ClassId))
                {
                    strWhere += " AND t.class_id = '" + ClassId + "'";
                }
                if (!string.IsNullOrEmpty(DateType))
                {
                    switch (DateType)
                    {
                        case "day":
                            strWhere += " and CONVERT(varchar(10),t.open_time,120)='" + DateData + "'";
                            break;
                        case "week":
                            strWhere += " and dbo.f_GetDatePartWeek(CONVERT(varchar(10),t.open_time,120))='" + DateData + "'";
                            break;
                        case "month":
                            strWhere += " and CONVERT(varchar(7),t.open_time,120)='" + DateData + "'";
                            break;
                    }

                }
                DataTable dt = new DataTable();
                BLL_visit_web_s bll = new BLL_visit_web_s();
                dt = bll.GetListByPageNew(strWhere, " t.open_time desc", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCountNew(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        num = ((PageIndex - 1) * PageSize) + inum,
                        ClassName = dt.Rows[i]["ClassName"].ToString(),
                        TeacherName = dt.Rows[i]["TeacherName"].ToString(),
                        HomeWork_Name = dt.Rows[i]["HomeWork_Name"].ToString(),
                        open_time = pfunction.ConvertToLongDateTime(dt.Rows[i]["open_time"].ToString(), "MM-dd HH:mm:ss"),
                        close_time = pfunction.ConvertToLongDateTime(dt.Rows[i]["close_time"].ToString(), "MM-dd HH:mm:ss"),
                        VistiDuration_Count = pfunction.ConvertSecond(dt.Rows[i]["TimeCount"].ToString()),
                        typeName = (dt.Rows[i]["resource_class"].ToString() == Rc.Common.Config.Resource_ClassConst.云资源) ? "云资源" : "自有资源"

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