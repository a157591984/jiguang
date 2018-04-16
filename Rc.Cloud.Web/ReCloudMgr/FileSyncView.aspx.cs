using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncView : System.Web.UI.Page
    {
        protected string resTitle = string.Empty;
        protected string resourceFolderId = string.Empty;
        protected string FileSyncExecRecord_Type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            FileSyncExecRecord_Type = Request.QueryString["FileSyncExecRecord_Type"].Filter();

            resourceFolderId = Request.QueryString["resourceFolderId"].Filter();
            resTitle = Request.QueryString["resTitle"].Filter();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetResourceList(string ResourceFolder_Id, string DocName, int PageSize, int PageIndex)
        {
            try
            {
                ResourceFolder_Id = ResourceFolder_Id.Filter();
                DocName = DocName.Filter();

                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                strWhere += " and ResourceFolder_ParentId='" + ResourceFolder_Id + "' ";
                if (!string.IsNullOrEmpty(DocName)) strWhere = " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";

                strSqlCount = @"select count(*) from VW_ResourceAndResourceFolder A where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.RType,A.ResourceFolder_Order,A.ResourceFolder_Name) row,A.* from VW_ResourceAndResourceFolder A where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string strIType = string.Empty;
                    if (dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.class类型文件 || dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.class类型微课件 || dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.ScienceWord类型文件)
                    {
                        strIType = "1";
                    }
                    else if (dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.testPaper类型文件)
                    {
                        strIType = "2";
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        docType = dtRes.Rows[i]["File_Suffix"].ToString(),
                        RType = dtRes.Rows[i]["RType"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IType = strIType
                    });
                }
                #endregion

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