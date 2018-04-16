using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;
using System.Data;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class SchoolIFList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90200500";
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string Key, string Name, int PageIndex, int PageSize)
        {
            try
            {
                BLL_TPSchoolIF bll = new BLL_TPSchoolIF();
                Key = Key.Filter();
                Name = Name.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(Key))
                {
                    strWhere += string.Format(" and SchoolIF_Code like '%{0}%' ", Key);
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    strWhere += string.Format(" and (SchoolIF_Name like '%{0}%' or SchoolId like '%{0}%' or SchoolName like '%{0}%') ", Name);
                }

                string orderBy = "SchoolIF_Code";
                dt = bll.GetListByPageSchool(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCountSchool(strWhere);

                foreach (DataRow item in dt.Rows)
                {
                    string strOperate = string.Empty;
                    if (item["SchoolIF_Code"].ToString() == ThirdPartyEnum.ahjzvs.ToString())
                    {
                        strOperate = string.Format("<a href=\"javascript:;\" onclick=\"HandleUserData('{0}','{1}')\" >初始化用户</a>"
                            , item["SchoolIF_Code"].ToString(), item["SchoolId"].ToString());
                        if (new BLL_FileSyncExecRecord().GetRecordCount("FileSyncExecRecord_Status='0' and FileSyncExecRecord_Type='初始化用户数据" + item["SchoolIF_Code"] + "'") > 0)
                        {
                            strOperate = "<a href=\"javascript:;\">正在初始化用户</a>";
                        }

                    }
                    listReturn.Add(new
                    {
                        SchoolIF_Id = item["SchoolIF_Id"].ToString(),
                        SchoolIF_Code = item["SchoolIF_Code"].ToString(),
                        SchoolIF_Name = item["SchoolIF_Name"].ToString(),
                        SchoolId = item["SchoolId"].ToString(),
                        SchoolName = item["SchoolName"].ToString(),
                        Remark = item["Remark"].ToString(),
                        Operate = strOperate
                    });
                }

                if (dt.Rows.Count > 0)
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
                    err = ex.Message.ToString()
                });
            }
        }

        [WebMethod]
        public static string DeleteData(string SchoolIF_Id)
        {
            try
            {
                if (new BLL_TPSchoolIF().Delete(SchoolIF_Id))
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

        [WebMethod]
        public static string HandleUserData(string SchoolCode, string SchoolId)
        {
            try
            {
                string strSql = string.Format(@"insert into TPIFUser
select NEWID(),'{0}',UserName,'',GETDATE() from (
select distinct UserName from VW_UserOnClassGradeSchool vw where vw.SchoolId='{1}'
and not exists(select 1 from TPIFUser where School='{0}' and UserName=vw.UserName )
) T", SchoolCode.Filter(), SchoolId.Filter());
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
                return "1";

            }
            catch (Exception)
            {
                return "0";
            }
        }

    }
}