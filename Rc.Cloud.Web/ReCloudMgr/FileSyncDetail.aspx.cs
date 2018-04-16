using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using System.Web.Services;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncDetail : System.Web.UI.Page
    {
        protected string strFileSyncExecRecord_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strFileSyncExecRecord_id = Request.QueryString["FileSyncExecRecord_id"].ToString().Filter();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string strFileSyncExecRecord_id, string strDetail_Status, int PageSize, int PageIndex)
        {
            try
            {
                strDetail_Status = strDetail_Status.Filter();

                strFileSyncExecRecord_id = strFileSyncExecRecord_id.Filter();
              
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;

                if (strDetail_Status == "0") strWhere += " and f.Detail_Status='0' ";
                if (strDetail_Status == "1") strWhere += " and f.Detail_Status='1' ";
                if (strDetail_Status == "2") strWhere += " and f.Detail_Status='2' ";

                strWhere += string.Format(" and f.FileSyncExecRecord_id='{0}' ", strFileSyncExecRecord_id);
                strSqlCount = @"select count(*) from FileSyncExecRecordDetail f where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY f.detail_timestart desc) row,rf.ResourceFolder_Name ,f.* from FileSyncExecRecordDetail f 
left join ResourceFolder rf on f.Book_Id=rf.ResourceFolder_Id where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    string strDetail_StatusTemp = string.Empty;
                    if (dtRes.Rows[i]["Detail_Status"].ToString()=="1")
                    {
                        strDetail_StatusTemp = "成功";
                    }
                    else if (dtRes.Rows[i]["Detail_Status"].ToString() == "2")
                    {
                        strDetail_StatusTemp = "失败";
                    }
                    else
                    {
                        strDetail_StatusTemp = "进行中";
                    }
                    inum++;
                    string strIType = string.Empty;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        ResourceFolder_Name = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        FileSyncExecRecordDetail_id = dtRes.Rows[i]["FileSyncExecRecordDetail_id"].ToString(),
                        FileSyncExecRecord_id = dtRes.Rows[i]["FileSyncExecRecord_id"].ToString(),
                        Book_Id = dtRes.Rows[i]["Book_Id"].ToString(),
                        ResourceToResourceFolder_Id = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        Detail_Status = strDetail_StatusTemp,
                        FileUrl = dtRes.Rows[i]["FileUrl"].ToString(),
                        Detail_TimeStart = dtRes.Rows[i]["Detail_TimeStart"].ToString(),
                        Detail_TimeEnd = dtRes.Rows[i]["Detail_TimeEnd"].ToString(),
                        Detail_Long = pfunction.ExecDateDiff(dtRes.Rows[i]["Detail_TimeStart"].ToString(), dtRes.Rows[i]["Detail_TimeEnd"].ToString()),
                        Detail_Remark = dtRes.Rows[i]["Detail_Remark"].ToString()
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

    }
}