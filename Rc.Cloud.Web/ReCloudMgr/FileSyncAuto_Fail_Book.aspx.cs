using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncAuto_Fail_Book : Rc.Cloud.Web.Common.InitPage
    {
        public string FileSyncExecRecord_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            FileSyncExecRecord_id = Request.QueryString["FileSyncExecRecord_id"].Filter();
            Module_Id = "10300200";
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string FileSyncExecRecord_id, int PageSize, int PageIndex)
        {
            try
            {
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                strWhere += " and FileSyncExecRecord_id='" + FileSyncExecRecord_id.Filter() + "'";
                strSqlCount = @"select COUNT(distinct(book_id)) from FileSyncExecRecordDetail  where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY ResourceFolder_Name desc) row,d.* from (select  distinct(f.Book_Id),rf.ResourceFolder_Name,f.FileSyncExecRecord_id from dbo.FileSyncExecRecordDetail f
left join ResourceFolder rf on rf.ResourceFolder_Id= f.Book_Id) d  where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        FileSyncExecRecord_id = dtRes.Rows[i]["FileSyncExecRecord_id"].ToString(),
                        ResourceFolder_Name = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        Book_Id = dtRes.Rows[i]["Book_Id"],
                        ResourceFolder_NameE = HttpContext.Current.Server.UrlEncode(dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter())
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