using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Common;
using System.Web.Services;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Sys
{
    public partial class AddSchoolSMS_Person : Rc.Cloud.Web.Common.InitPage
    {
        protected string School_Id = string.Empty;
        BLL_SchoolSMS bll = new BLL_SchoolSMS();
        Model_SchoolSMS model = new Model_SchoolSMS();
        protected void Page_Load(object sender, EventArgs e)
        {
            School_Id = Request.QueryString["School_Id"].ToString().Filter();

            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string School_Id, int PageIndex, int PageSize)
        {
            try
            {
                BLL_SchoolSMS_Person bll = new BLL_SchoolSMS_Person();
                School_Id = School_Id.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " School_Id='" + School_Id + "' ";
                string strOrderBy = "CreateTime desc";
                dt = bll.GetListByPage(strWhere, strOrderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        SchoolSMS_Person_Id = dt.Rows[i]["SchoolSMS_Person_Id"].ToString(),
                        Name = dt.Rows[i]["Name"].ToString(),
                        Job = dt.Rows[i]["Job"].ToString(),
                        PhoneNum = dt.Rows[i]["PhoneNum"].ToString(),
                        Company = dt.Rows[i]["Company"].ToString(),
                        School_Id = School_Id

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
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="School_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteData(string SchoolSMS_Person_Id)
        {
            try
            {
                string sql = string.Format(@"delete from SchoolSMS_Person where SchoolSMS_Person_Id='{0}'", SchoolSMS_Person_Id.Filter());
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}