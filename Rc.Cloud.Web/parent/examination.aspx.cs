using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.Model;
using System.Data;
using System.Text;


namespace Rc.Cloud.Web.parent
{
    public partial class examination : System.Web.UI.Page
    {
        protected string struserid = "";
        protected string km_html = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            struserid = Request.QueryString["struserid"];
            if (!IsPostBack)
            {
                isit_Load();
            }   
        }

        private void isit_Load()
        {
            string strWhere = string.Empty;
            StringBuilder html = new StringBuilder();
            DataTable dt = new DataTable();
            strWhere = " D_Type='7' order by d_order";
            dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
            int num=0;
            foreach (DataRow row in dt.Rows)
            {
                num++;
                html.AppendFormat("<li><div class='name'><a href='javascript:void(0)' id='datetype" + num + "' value='" + row["Common_Dict_ID"] + "'>" + row["d_name"] + "</a></div></li>");
            }
            km_html = html.ToString();
        }
        /// <summary>
        /// 获取统计列表
        /// </summary>
        /// <param name="datetype"></param>
        /// <param name="struserid"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStatTest_TestRListPage(string ssubject, string struserid,int PageSize, int PageIndex)
        {
            try
            {
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(ssubject)) strWhere = " and ssubject='" + ssubject + "' ";
                strWhere += " and UserId='" + struserid + "' ";

                strSqlCount = @"select count(1) from StatTest_TestR A  where 1=1  " + strWhere;
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY SRank desc) row,A.* from StatTest_TestR A  where 1=1 "
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
                        SSubjectName = dtRes.Rows[i]["SSubjectName"].ToString(),
                        SRank = dtRes.Rows[i]["SRank"].ToString(),
                        HomeWork_Name = dtRes.Rows[i]["HomeWork_Name"].ToString(),
                        HomeWorkDate = dtRes.Rows[i]["HomeWorkDate"].ToString(),
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