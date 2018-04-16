using Newtonsoft.Json;
using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.teacher
{
    public partial class teachingPlan : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetResourceFolderListPage(int PageIndex, int PageSize)
        {
            try
            {

                StringBuilder strHtml = new StringBuilder();
                Rc.Model.Resources.Model_F_User loginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                strSqlCount = @"SELECT count(1) from ResourceFolder t 
                                left join Bookshelves t1 on t.ResourceFolder_ID=t1.ResourceFolder_ID 
                                where t.Resource_Type in('" + Resource_TypeConst.class类型文件 + "','" + Resource_TypeConst.ScienceWord类型文件 + "') and t.ResourceFolder_ID in(SELECT book_id from UserBuyResources where userid='" + loginUser.UserId + "') ";

                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY t.CreateTime DESC) row,t1.BookImg_Url,t.*,t1.bookShelvesstate,t1.BookPrice,t1.Book_Name  from ResourceFolder t 
                                left join Bookshelves t1 on t.ResourceFolder_ID=t1.ResourceFolder_ID 
                                where t.Resource_Type in('" + Resource_TypeConst.class类型文件 + "','" + Resource_TypeConst.ScienceWord类型文件 + "') and  t.ResourceFolder_ID in(SELECT book_id from UserBuyResources where userid='" + loginUser.UserId + "') "
                    + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        BookImg_Url = dtRes.Rows[i]["BookImg_Url"].ToString(),
                        Book_Name = dtRes.Rows[i]["Book_Name"].ToString(),
                        BookPrice = dtRes.Rows[i]["BookPrice"].ToString(),
                        ResourceFolder_ID = dtRes.Rows[i]["ResourceFolder_ID"].ToString(),
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
                    err = "error"//ex.Message.ToString()
                });
            }
        }
    }
}