using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using System.Data;
using System.Text;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Cloud.Model;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class DictRelation_List : Rc.Cloud.Web.Common.InitPage
    {
        static DataTable dtDic = new DataTable();
        Common_DictBLL BLL = new Common_DictBLL();
        protected string ReturnUrl = string.Empty;
        protected string SysName = Rc.Common.ConfigHelper.GetConfigString("SysName");
        Rc.Cloud.Model.Model_Struct_Func UserFun;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90300300";
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            btnAdd.Visible = UserFun.Add;
            if (!IsPostBack)
            {

            }
        }
        [WebMethod]
        public static string GetList(int PageIndex, int PageSize)
        {
            try
            {
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string StrWhere = " where 1=1 ";
                strSqlCount = @"select count(*) from [DictRelation] " + StrWhere + " ";
                strSql = @"  select * from (
 select ROW_NUMBER() over(ORDER BY cd1.D_Order) row,a.*,cd1.D_Name Name1,cd2.D_Name as Name2,cd1.D_Order,CountDict=(select count(*) from DictRelation_Detail where DictRelation_Id=a.DictRelation_Id) from [DictRelation] a
 left join Common_Dict cd1 on cd1.Common_Dict_ID=a.HeadDict_Id
 left join Common_Dict cd2 on cd2.Common_Dict_ID=a.SonDict_Id "
                    + StrWhere + "  ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        SonName = dt.Rows[i]["Name2"].ToString(),
                        HeadName = dt.Rows[i]["Name1"].ToString(),
                        DictRelation_Id = dt.Rows[i]["DictRelation_Id"].ToString(),
                        HeadDict_Id = dt.Rows[i]["HeadDict_Id"].ToString(),
                        SonDict_Id = dt.Rows[i]["SonDict_Id"].ToString(),
                        CountDict = dt.Rows[i]["CountDict"].ToString(),
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
        [WebMethod]
        public static string DeleteItem(string DictRelation_Id)
        {
            try
            {
                string sql = "delete from [DictRelation] where DictRelation_Id='" + DictRelation_Id.Filter() + "'";
                int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                if (i > 0)
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