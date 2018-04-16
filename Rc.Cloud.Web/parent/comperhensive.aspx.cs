using Newtonsoft.Json;
using Rc.Cloud.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.parent
{
    public partial class comperhensive : System.Web.UI.Page
    {
        protected string struserid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            struserid = Request.QueryString["struserid"];
            if (IsPostBack)
            {
                
            }
        }
        /// <summary>
        /// 获取统计列表
        /// </summary>
        /// <param name="datetype"></param>
        /// <param name="struserid"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatTest_ComprehensiveRListPage(string datetype, string struserid,int PageSize, int PageIndex)
        {
            try
            {
                PageIndex = Convert.ToInt32(PageIndex.ToString());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(datetype)) strWhere = " and SType='" + datetype + "' ";
                strWhere += " and UserId='" + struserid + "' ";

                strSqlCount = @"select count(1) from StatTest_ComprehensiveR A  where 1=1  " + strWhere;
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY SDate desc,SRank desc) row,A.* from StatTest_ComprehensiveR A  where 1=1 "
                    + strWhere + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        SDate = dtRes.Rows[i]["SDate"].ToString(),
                        SRank = dtRes.Rows[i]["SRank"].ToString(),
                        SAverageScore = dtRes.Rows[i]["SAverageScore"].ToString(),
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }
       
    }
}