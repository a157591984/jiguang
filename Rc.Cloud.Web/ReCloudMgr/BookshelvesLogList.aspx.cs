using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Model;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class BookshelvesLogList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10100400";

            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 图书上下架日志
        /// </summary>
        [WebMethod]
        public static string GetBookshelvesLog(string BookName, string UserName, string STime, string LogType, int PageIndex, int PageSize)
        {
            try
            {
                BookName = BookName.Filter();
                UserName = UserName.Filter();
                LogType = LogType.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(BookName)) strWhere += " and bk.Book_Name like '%" + BookName + "%' ";
                if (!string.IsNullOrEmpty(UserName)) strWhere += " and su.SysUser_LoginName like '%" + UserName + "%' ";
                if (!string.IsNullOrEmpty(LogType)) strWhere += " and bl.LogTypeEnum='" + LogType + "' ";
                if (!string.IsNullOrEmpty(STime)) strWhere += " and convert(nvarchar(10),bl.CreateTime,23)='" + STime + "' ";

                strSqlCount = @"select count(1) from BookshelvesLog bl
left join Bookshelves bk on bk.ResourceFolder_Id=bl.BookId left join SysUser su on su.SysUser_ID=bl.CreateUser where 1=1 " + strWhere + " ";

                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY bl.CreateTime DESC) row,bl.BookShelvesLog_Id,bl.BookId,bl.LogTypeEnum,bl.CreateUser,bl.CreateTime ,bk.Book_Name,su.SysUser_LoginName
,RF.ParticularYear,rf.Resource_Type,dic.D_Name as Resource_TypeName
from BookshelvesLog bl
left join Bookshelves bk on bk.ResourceFolder_Id=bl.BookId
left join SysUser su on su.SysUser_ID=bl.CreateUser
left join ResourceFolder RF ON RF.Book_Id=bl.BookId and RF.ResourceFolder_Level=5
left join Common_Dict dic on dic.Common_Dict_Id=RF.Resource_Type
where 1=1 " + strWhere + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = ((PageIndex - 1) * PageSize + inum),
                        BookShelvesLog_Id = dtRes.Rows[i]["BookShelvesLog_Id"].ToString(),
                        ParticularYear = dtRes.Rows[i]["ParticularYear"].ToString(),
                        Resource_TypeName = dtRes.Rows[i]["Resource_TypeName"].ToString(),
                        Book_Name = dtRes.Rows[i]["Book_Name"].ToString(),
                        UserName = dtRes.Rows[i]["SysUser_LoginName"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        LogTypeEnumName = (dtRes.Rows[i]["LogTypeEnum"].ToString() == "1" ? "上架" : "下架")
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