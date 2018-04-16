using Newtonsoft.Json;
using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Sys
{
    public partial class FUserShow1 : Rc.Cloud.Web.Common.InitPage
    {
        protected string UserId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = Request["UserId"].Filter();
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// 注册用户列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetFUserList(string UserId, int PageIndex, int PageSize)
        {
            try
            {
                UserId = UserId.Filter();
                string strWhere = string.Empty;
                strWhere = " where 1=1";
                if (!string.IsNullOrEmpty(UserId))
                {
                    strWhere += " AND UserId ='" + UserId + "'";
                }
                int intRecordCount = 0;
                List<object> listReturn = new List<object>();
                BLL_F_User bll_f_user = new BLL_F_User();

                string sql = "select ClassId,ClassName,GradeId,GradeName,SchoolId,SchoolName from [dbo].[VW_UserOnClassGradeSchool] " + strWhere;
                string sqlCount = "select count(*) from VW_UserOnClassGradeSchool " + strWhere;
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                intRecordCount = (int)Rc.Common.DBUtility.DbHelperSQL.GetSingle(sqlCount);
                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        ClassId = string.IsNullOrEmpty(item["ClassId"].ToString()) ? "-" : item["ClassId"].ToString(),
                        ClassName = string.IsNullOrEmpty(item["ClassName"].ToString()) ? "-" : item["ClassName"].ToString(),
                        GradeId = string.IsNullOrEmpty(item["GradeId"].ToString()) ? "-" : item["GradeId"].ToString(),
                        GradeName = string.IsNullOrEmpty(item["GradeName"].ToString()) ? "-" : item["GradeName"].ToString(),
                        SchoolId = item["SchoolId"].ToString(),
                        SchoolName = item["SchoolName"].ToString()
                    });
                }
                if (dt.Rows.Count > 0)
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
                    err = "error"
                });
            }
        }
    }
}