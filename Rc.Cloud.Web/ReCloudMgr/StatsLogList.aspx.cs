using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.BLL;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class StatsLogList : Rc.Cloud.Web.Common.InitPage
    {
        public string TimeLenght = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10255000";
            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetDataList(string DataType, string DataName, string LogStatus, int PageIndex, int PageSize)
        {
            try
            {
                DataType = DataType.Filter();
                DataName = DataName.Filter();
                LogStatus = LogStatus.Filter();
                string strWhere = " 1=1 ";
                BLL_StatsLog bll = new BLL_StatsLog();

                if (!string.IsNullOrEmpty(DataType))
                {
                    strWhere += " and DataType='" + DataType + "' ";
                }
                if (!string.IsNullOrEmpty(DataName))
                {
                    strWhere += " and DataName like '%" + DataName + "%' ";
                }
                if (!string.IsNullOrEmpty(LogStatus))
                {
                    strWhere += " and LogStatus='" + LogStatus + "' ";
                }

                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                DataTable dt = new DataTable();
                dt = bll.GetListByPage(strWhere, "CTime desc", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        Num = inum + PageSize * (PageIndex - 1),
                        StatsLogId = dt.Rows[i]["StatsLogId"].ToString(),
                        DataId = dt.Rows[i]["DataId"].ToString(),
                        DataName = dt.Rows[i]["DataName"].ToString(),
                        DataType = dt.Rows[i]["DataType"].ToString(),
                        DataTypeName = dt.Rows[i]["DataType"].ToString() == "1" ? "按日期" : "按试卷",
                        LogStatus = dt.Rows[i]["LogStatus"].ToString() == "1" ? "成功" : "失败",
                        CTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["CTime"].ToString(),"yyyy-MM-dd HH:mm:ss")
                    });
                    inum++;
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
        public static string ReExecuteStatsData(string StatsLogId)
        {
            try
            {
                BLL_StatsLog bll = new BLL_StatsLog();
                Model_StatsLog model = bll.GetModel(StatsLogId);
                model.CTime = DateTime.Now;
                bll.ExecuteStatsAddLog(model);
                return "1";
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("10255000", "重新执行失败：" + ex.Message.ToString());
                return "0";
            }
        }

    }
}