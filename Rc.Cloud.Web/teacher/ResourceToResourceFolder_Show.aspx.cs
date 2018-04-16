using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.DBUtility;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Cloud.Web.Common;
using Rc.Common;
using Rc.Common.Config;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.teacher
{
    public partial class ResourceToResourceFolder_Show : Rc.Cloud.Web.Common.FInitData
    {
        public string ResourceFolder_Id = string.Empty;
        public string resTitle = string.Empty;
        public string type = string.Empty;
        BLL_PrpeLesson bll = new BLL_PrpeLesson();
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceFolder_Id = Request.QueryString["ResourceFolder_Id"].Filter();
            resTitle = Request.QueryString["resTitle"].Filter();
            type = "1";//Request.QueryString["type"];
            if (!IsPostBack)
            {
                this.ltlBookName.Text = resTitle;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetResourceList(string ResourceFolder_Id, string resTitle, int PageSize, int PageIndex)
        {
            try
            {
                ResourceFolder_Id = ResourceFolder_Id.Filter();

                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                strWhere += " and ResourceFolder_ParentId='" + ResourceFolder_Id + "' ";
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
                    if (dtRes.Rows[i]["File_Suffix"].ToString() == "class")
                    {
                        strIType = "1";
                    }
                    else if (dtRes.Rows[i]["File_Suffix"].ToString() == "testPaper")
                    {
                        strIType = "2";
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        ResourceFolder_NameUrl = HttpContext.Current.Server.UrlEncode(resTitle + "-" + dtRes.Rows[i]["ResourceFolder_Name"].ToString()),
                        docType = dtRes.Rows[i]["File_Suffix"].ToString(),
                        RType = dtRes.Rows[i]["RType"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IType = strIType,
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