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
    public partial class DictRelationDetail_List : Rc.Cloud.Web.Common.InitPage
    {
        public string HeadDict_Id = string.Empty;
        public string SonDict_Id = string.Empty;
        public string DictRelation_Id = string.Empty;
        Common_DictBLL BLL = new Common_DictBLL();
        protected string ReturnUrl = string.Empty;
        protected string SysName = Rc.Common.ConfigHelper.GetConfigString("SysName");
        Rc.Cloud.Model.Model_Struct_Func UserFun;
        protected void Page_Load(object sender, EventArgs e)
        {
            DictRelation_Id = Request["DictRelation_Id"].Filter();
            HeadDict_Id = Request["HeadDict_Id"].Filter();
            SonDict_Id = Request["SonDict_Id"].Filter();
            Module_Id = "90300300";
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            ReturnUrl = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            btnAdd.Visible = UserFun.Add;

            if (!IsPostBack)
            {
                

            }
        }
        [WebMethod]
        public static string GetList(string DictRelation_Id, int PageIndex, int PageSize)
        {
            try
            {
                DictRelation_Id = DictRelation_Id.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string StrWhere = " where 1=1 ";

                if (!string.IsNullOrEmpty(DictRelation_Id))
                {
                    StrWhere += " and DictRelation_Id='" + DictRelation_Id + "'";
                }

                strSqlCount = @"select count(distinct(Parent_Id)) from [DictRelation_Detail] " + StrWhere + " ";
                strSql = @"  select * from (
 select ROW_NUMBER() over(ORDER BY cd2.D_Name,cd1.D_Order) row,Parent_Id,cd1.D_Name Name1,cd2.D_Name as Name2,a.Dict_Type,cd1.D_Order from [DictRelation_Detail] a
 left join Common_Dict cd1 on cd1.Common_Dict_ID=a.Parent_Id
 left join Common_Dict cd2 on cd2.D_Value=a.Dict_Type and  cd2.D_type='0' "
                    + StrWhere + "    group by Parent_Id,cd1.D_Name,cd2.D_Name,a.Dict_Type,cd1.D_Order ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        Dicttype = dt.Rows[i]["Dict_Type"].ToString(),
                        Dict_Type = dt.Rows[i]["Name2"].ToString(),
                        Dict_Name = dt.Rows[i]["Name1"].ToString(),
                        NameUrl = HttpContext.Current.Server.UrlEncode(dt.Rows[i]["Name1"].ToString()),
                        Dict_Data = GetDicName(dt.Rows[i]["Parent_Id"].ToString(), DictRelation_Id),
                        Parent_Id = dt.Rows[i]["Parent_Id"].ToString()
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
        public static string DeleteItem(string ParentId, string DictRelation_Id)
        {
            try
            {
                string sql = "delete from [DictRelation_Detail] where Parent_Id='" + ParentId.Filter() + "' and DictRelation_Id='" + DictRelation_Id + "'";
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
        public static string GetDicName(string ParentId, string DictRelation_Id)
        {
            try
            {
                string sql = @"select dic.D_Name,Parent_Id,dic.D_Order from [dbo].[DictRelation_Detail] dr
left join [dbo].[Common_Dict] dic on dic.Common_Dict_ID=dr.Dict_Id where Parent_Id='" + ParentId.Filter() + "' and DictRelation_Id='" + DictRelation_Id + "' order by dic.D_Order ";
                DataTable dtDic = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                string Str = string.Empty;
                if (dtDic.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDic.Rows.Count; i++)
                    {
                        Str += dtDic.Rows[i]["D_Name"].ToString() + ",";
                    }
                    return Str.TrimEnd(',');
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