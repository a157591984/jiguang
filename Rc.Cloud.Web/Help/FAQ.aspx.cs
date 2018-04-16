using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.Help
{
    public partial class FAQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetHelpList(string key)
        {
            try
            {
                key = key.Filter();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = "1=1 ";

                if (!string.IsNullOrEmpty(key))
                {
                    strWhere += " and help_title like '%" + key + "%'";
                }
                strWhere += " order by create_time desc";
                BLL_HelpCenter bll = new BLL_HelpCenter();
                dt = bll.GetList(strWhere).Tables[0];
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        help_id = dt.Rows[i]["help_id"].ToString(),
                        help_title = dt.Rows[i]["help_title"].ToString(),
                        help_content = dt.Rows[i]["help_content"].ToString(),
                        create_time = pfunction.ConvertToLongDateTime(dt.Rows[i]["create_time"].ToString(), "yyyy-MM-dd HH:mm:ss"),
                    });
                }

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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