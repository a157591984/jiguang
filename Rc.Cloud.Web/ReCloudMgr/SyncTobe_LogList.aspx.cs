using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using System.IO;
using Rc.Common.DBUtility;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SyncTobe_LogList : System.Web.UI.Page
    {
        protected string FileSyncExecRecord_Type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            FileSyncExecRecord_Type = Request["FileSyncExecRecord_Type"].Filter();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string strFileSyncExecRecord_Type, string strFileSyncExecRecord_Status, int PageSize, int PageIndex)
        {
            try
            {

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;

                if (strFileSyncExecRecord_Status == "0") strWhere += " and f.Detail_Status='0' ";
                if (strFileSyncExecRecord_Status == "1") strWhere += " and f.Detail_Status='1' ";
                if (strFileSyncExecRecord_Status == "2") strWhere += " and f.Detail_Status='2' ";

                strWhere += " and f.FileSyncExecRecord_Type='" + strFileSyncExecRecord_Type.Filter() + "' ";
                strSqlCount = @"select count(*) from FileSyncExecRecord f where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY f.FileSyncExecRecord_TimeStart desc) row,f.*,u.SysUser_Name from FileSyncExecRecord f
 left join SysUser u on f.createUser=u.SysUser_ID where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string strDetail_StatusTemp = string.Empty;
                    if (dtRes.Rows[i]["FileSyncExecRecord_Status"].ToString() == "1")
                    {
                        strDetail_StatusTemp = "成功";
                    }
                    else if (dtRes.Rows[i]["FileSyncExecRecord_Status"].ToString() == "2")
                    {
                        strDetail_StatusTemp = "失败";
                    }
                    else
                    {
                        strDetail_StatusTemp = "进行中";
                    }
                    double execHours = 0;
                    DateTime d1 = new DateTime();
                    if (!string.IsNullOrEmpty(dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString()))
                    {
                        d1 = Convert.ToDateTime(dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString());
                    }
                    DateTime d2 = DateTime.Now;
                    TimeSpan d3 = d2 - d1;
                    execHours = d3.TotalHours;
                    string strIType = string.Empty;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        FileSyncExecRecord_id = dtRes.Rows[i]["FileSyncExecRecord_id"].ToString(),
                        FileSyncExecRecord_Type = dtRes.Rows[i]["FileSyncExecRecord_Type"].ToString(),
                        FileSyncExecRecord_TimeStart = dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString(),
                        FileSyncExecRecord_TimeEnd = dtRes.Rows[i]["FileSyncExecRecord_TimeEnd"].ToString(),
                        FileSyncExecRecord_Long = pfunction.ExecDateDiff(dtRes.Rows[i]["FileSyncExecRecord_TimeStart"].ToString(), dtRes.Rows[i]["FileSyncExecRecord_TimeEnd"].ToString()),
                        FileSyncExecRecord_Status = strDetail_StatusTemp,
                        FileSyncExecRecord_Condition = dtRes.Rows[i]["FileSyncExecRecord_Condition"].ToString(),
                        createUser = dtRes.Rows[i]["createUser"].ToString(),
                        FileSyncExecRecord_Remark = dtRes.Rows[i]["FileSyncExecRecord_Remark"].ToString(),
                        SysUser_Name = dtRes.Rows[i]["SysUser_Name"].ToString(),
                        ExecHours = execHours
                    });
                }

                if (inum > 0)
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"// ex.Message.ToString()
                });
            }
        }

    }
}