using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.Common.Config;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class ReList : Rc.Cloud.Web.Common.InitPage
    {
        protected string resTitle = string.Empty;
        protected string resourceFolderId = string.Empty;
        protected string type = string.Empty;
        protected string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = loginUser.SysUser_ID;
            type = Request.QueryString["type"];
            if (!string.IsNullOrEmpty(type))
            {
                Module_Id = "10100200";//图书审核模块
            }
            else
            { Module_Id = "10100300"; }

            resourceFolderId = Request.QueryString["resourceFolderId"].Filter();
            resTitle = Request.QueryString["resTitle"].Filter();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetResourceList(string ResourceFolder_Id, string DocName, int PageSize, int PageIndex, string Module_Id)
        {
            try
            {
                Rc.Cloud.Model.Model_Struct_Func UserFun;
                Rc.Cloud.Model.Model_VSysUserRole loginModel = HttpContext.Current.Session["LoginUser"] as Rc.Cloud.Model.Model_VSysUserRole;
                UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginModel.SysUser_ID, (loginModel == null ? "''" : clsUtility.ReDoStr(loginModel.SysRole_IDs, ',')), Module_Id);

                ResourceFolder_Id = ResourceFolder_Id.Filter();
                DocName = DocName.Filter();

                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                strWhere += " and ResourceFolder_ParentId='" + ResourceFolder_Id + "' ";
                if (!string.IsNullOrEmpty(DocName)) strWhere = " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";

                strSqlCount = @"select count(*) from VW_ResourceAndResourceFolder A where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.RType,A.ResourceFolder_Order,A.ResourceFolder_Name) row,A.* from VW_ResourceAndResourceFolder A where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string strIType = string.Empty;
                    if (dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.class类型文件 || dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.class类型微课件 || dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.ScienceWord类型文件)
                    {
                        strIType = "1";
                    }
                    else if (dtRes.Rows[i]["Resource_Type"].ToString() == Resource_TypeConst.testPaper类型文件)
                    {
                        strIType = "2";
                    }
                    string strOperate = string.Empty;
                    if (Module_Id == "10100500")
                    {
                        if (UserFun.Delete)
                        {
                            strOperate = string.Format("<a href=\"javascript:;\" onclick=\"DelData('{0}','{1}');\">删除</a>&nbsp;"
                                                        , dtRes.Rows[i]["ResourceFolder_Id"].ToString()
                                                        , dtRes.Rows[i]["RType"].ToString());
                        }
                        if (UserFun.Edit)
                        {
                            strOperate += string.Format("<a href=\"javascript:;\" onclick=\"EditData('{0}','{1}');\">编辑</a>"
                                                        , dtRes.Rows[i]["ResourceFolder_Id"].ToString()
                                                        , dtRes.Rows[i]["RType"].ToString());
                        }
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        docType = dtRes.Rows[i]["File_Suffix"].ToString(),
                        RType = dtRes.Rows[i]["RType"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IType = strIType,
                        Operate = strOperate
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        [WebMethod]
        public static string DelData(string dataId, string dataType, string userId)
        {
            try
            {
                if (dataType == "0")//文件夹
                {
                    if (new BLL_ResourceFolder().GetRecordCount("ResourceFolder_ParentId='" + dataId + "'") > 0
                        || new BLL_ResourceToResourceFolder().GetRecordCount("ResourceFolder_Id='" + dataId + "'") > 0)
                    {
                        return "2";
                    }
                    else
                    {
                        if (new BLL_ResourceFolder().Delete(dataId))
                        {
                            Rc.Common.DBUtility.DbHelperSQL_Operate.ExecuteSql("delete from ResourceFolder where ResourceFolder_Id='" + dataId + "' ");
                            Rc.Common.SystemLog.SystemLog.AddLogFromBS(dataId, "", string.Format("删除文件夹|操作人{0}|文件夹Id{1}|路径{2}", userId, dataId, "ReList.aspx"));
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
                else//文件
                {
                    Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("EXEC dbo.P_DelResourceData '" + dataId + "'", 7200);
                    Rc.Common.DBUtility.DbHelperSQL_Operate.ExecuteSqlByTime("EXEC dbo.P_DelResourceData '" + dataId + "'", 7200);
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(dataId, "", string.Format("删除文件资源|操作人{0}|文件夹Id{1}|路径{2}", userId, dataId, "ReList.aspx"));
                    return "1";
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(dataId, "", string.Format("删除文件资源|操作人{0}|文件夹Id{1}|路径{2}", userId, dataId, "ReList.aspx"));
                return "0";
            }
        }

    }
}