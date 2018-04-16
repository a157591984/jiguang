using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.BLL;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class NoticeBoardList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90100600";
        }
        [WebMethod]
        public static string GetList(string key, int PageIndex, int PageSize)
        {
            try
            {
                key = key.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = "1=1 ";

                if (!string.IsNullOrEmpty(key))
                {
                    strWhere += " and notice_title like '%" + key + "%'";
                }
                BLL_NoticeBoard bll = new BLL_NoticeBoard();
                dt = bll.GetListByPage(strWhere, "create_time desc", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        notice_id = dt.Rows[i]["notice_id"].ToString(),
                        notice_title = dt.Rows[i]["notice_title"].ToString(),
                        notice_content = pfunction.GetSubstring(dt.Rows[i]["notice_content"].ToString(), 200, true),
                        create_time = pfunction.ConvertToLongDateTime(dt.Rows[i]["create_time"].ToString(), "yyyy-MM-dd HH:mm:ss"),
                        start_time = pfunction.ConvertToLongDateTime(dt.Rows[i]["start_time"].ToString(), "yyyy-MM-dd HH:mm"),
                        end_time = pfunction.ConvertToLongDateTime(dt.Rows[i]["end_time"].ToString(), "yyyy-MM-dd HH:mm"),
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
        [WebMethod]
        public static string delete(string notice_id)
        {
            try
            {
                BLL_NoticeBoard bll = new BLL_NoticeBoard();
                if (bll.Delete(notice_id))
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

                return "";
            }
        }
    }


}