using Rc.Cloud.Model;
using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.BLL;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class BookAuditList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Type = string.Empty;//资源类型（class类型文件、testPaper类型文件、ScienceWord类型文件）
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string _t = string.Empty;
        protected string s = string.Empty;
        protected string userId = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10100200";
            userId = loginUser.SysUser_ID;
            strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
            strResource_Type = ddlResource_Type.SelectedValue;
            strGradeTerm = ddlGradeTerm.SelectedValue;
            strSubject = ddlSubject.SelectedValue;
            strResource_Version = ddlResource_Version.SelectedValue;

            _t = Request.QueryString["t"];
            if (_t == "2")
            {
                Module_Id = "10100500";
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "请选择");
                //文档类型
                strWhere = string.Format(" D_Type='1' AND Common_Dict_ID!='{0}' order by d_order", Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Type, dt, "D_Name", "Common_Dict_ID", "请选择");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "请选择");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "请选择");

            }
        }
        /// <summary>
        /// 获取书本目录
        /// </summary>
        /// <param name="DocName"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strResource_Class"></param>
        /// <param name="strGradeTerm"></param>
        /// <param name="strSubject"></param>
        /// <param name="strResource_Version"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCloudBooks(string DocName, string strResource_Type, string strResource_Class, string strGradeTerm, string strSubject, string strResource_Version, string AuditState, string ATime, int PageIndex, int PageSize)
        {
            try
            {
                DocName = DocName.Filter();
                ATime = ATime.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                Rc.Cloud.Model.Model_Struct_Func UserFun;
                UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, (loginUser == null ? "''" : clsUtility.ReDoStr(loginUser.SysRole_IDs, ',')), "10100500");
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(DocName)) strWhere += " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";
                if (strResource_Class != "-1") strWhere += " and Resource_Class = '" + strResource_Class.Filter() + "' ";
                if (strResource_Type != "-1") strWhere += " and Resource_Type = '" + strResource_Type.Filter() + "' ";
                if (strGradeTerm != "-1") strWhere += " and GradeTerm = '" + strGradeTerm.Filter() + "' ";
                if (strSubject != "-1") strWhere += " and Subject = '" + strSubject.Filter() + "' ";
                if (strResource_Version != "-1") strWhere += " and Resource_Version = '" + strResource_Version.Filter() + "' ";
                if (AuditState != "-1")
                {
                    if (AuditState == "3")
                    {
                        strWhere += " and AuditState is null ";
                    }
                    else
                    {
                        strWhere += " and AuditState='" + AuditState + "' ";
                    }
                }
                if (!string.IsNullOrEmpty(ATime)) strWhere += " and convert(nvarchar(10),t.CreateTime,23)='" + ATime + "' ";
                strWhere += " AND ResourceFolder_Level=5";//管理员维护的书籍目录
                strSqlCount = @"select count(1) from ResourceFolder A  
left JOIN  BookAudit t on t.ResourceFolder_Id=A.ResourceFolder_Id  where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY t.AuditState,t.CreateTime DESC) row,A.*,t.AuditState,t.CreateTime AuditTime,UserName=isnull(su.SysUser_Name,su.SysUser_LoginName)  
,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName,t4.D_Name as Resource_TypeName
from ResourceFolder A  
left JOIN  BookAudit t on t.ResourceFolder_Id=A.ResourceFolder_Id
left join  SysUser su on su.SysUser_ID=t.CreateUser
left join Common_Dict t1 on t1.Common_Dict_Id=A.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=A.Subject
left join Common_Dict t3 on t3.Common_Dict_Id=A.Resource_Version
left join Common_Dict t4 on t4.Common_Dict_Id=A.Resource_Type
where 1=1 " + strWhere + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter();
                    //docName = pfunction.GetDocFileName(docName);
                    string strOperate = string.Empty;
                    if (UserFun.Delete)
                    {
                        strOperate = string.Format("<a href=\"javascript:;\" onclick=\"DelData('{0}');\">删除</a>&nbsp;"
                            , dtRes.Rows[i]["ResourceFolder_Id"].ToString()
                            );
                    }
                    if (UserFun.Edit)
                    {
                        strOperate += string.Format("<a href=\"javascript:;\" onclick=\"EditData('{0}','0');\">编辑</a>"
                            , dtRes.Rows[i]["ResourceFolder_Id"].ToString()
                            );
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        GradeTerm = dtRes.Rows[i]["GradeTermName"].ToString(),
                        Subject = dtRes.Rows[i]["SubjectName"].ToString(),
                        Resource_Version = dtRes.Rows[i]["Resource_VersionName"].ToString(),
                        Resource_Type = dtRes.Rows[i]["Resource_TypeName"].ToString(),
                        docName = docName,
                        docNameSub = pfunction.GetSubstring(docName, 30, true),
                        AuditTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["AuditTime"].ToString()),
                        AuditStateStr = dtRes.Rows[i]["AuditState"].ToString() == "" ? "<span style='color:red'>未审核</span>" : dtRes.Rows[i]["AuditState"].ToString() == "1" ? "<span style='color:green'>已审核</span>" : "<span style='color:blue'>审核未通过</span>",
                        UserName = dtRes.Rows[i]["UserName"].ToString(),
                        AuditState = dtRes.Rows[i]["AuditState"].ToString(),
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }
        [WebMethod]
        public static string BookAudit(string reid, string rename, string type)
        {
            try
            {
                Model_VSysUserRole loginUser = HttpContext.Current.Session["LoginUser"] as Model_VSysUserRole;
                reid = reid.Filter();
                type = type.Filter();
                Model_BookAudit model = new Model_BookAudit();
                BLL_BookAudit bll = new BLL_BookAudit();
                Model_SyncData modelSD = new Model_SyncData();
                model = bll.GetModel(reid);
                if (model == null)
                {
                    model = new Model_BookAudit();
                    
                    if (type == "1")
                    {
                        model.AuditState = "1";
                        model.ResourceFolder_Id = reid;
                        model.Book_Name = rename;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                        #region 审核通过，记录同步数据
                        modelSD.SyncDataId = Guid.NewGuid().ToString();
                        modelSD.TableName = "BookAudit";
                        modelSD.DataId = reid;
                        modelSD.OperateType = "bookaudit";
                        modelSD.CreateTime = DateTime.Now;
                        modelSD.SyncStatus = "0";
                        #endregion
                    }
                    else
                    {
                        model.AuditState = "0";
                        model.ResourceFolder_Id = reid;
                        model.Book_Name = rename;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                    }
                    if (bll.Add(model))
                    {
                        if (modelSD.SyncDataId != null) new BLL_SyncData().Add(modelSD);
                        return "1";
                    }
                    else
                    {
                        return "2";
                    }
                }
                else
                {
                    if (type == "1")
                    {
                        model = bll.GetModel(reid);
                        model.AuditState = "1";
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                        #region 审核通过，记录同步数据
                        modelSD.SyncDataId = Guid.NewGuid().ToString();
                        modelSD.TableName = "BookAudit";
                        modelSD.DataId = reid;
                        modelSD.OperateType = "bookaudit";
                        modelSD.CreateTime = DateTime.Now;
                        modelSD.SyncStatus = "0";
                        #endregion
                    }
                    else
                    {
                        model = bll.GetModel(reid);
                        model.AuditState = "0";
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                    }
                    if (bll.Update(model))
                    {
                        if (modelSD.SyncDataId != null) new BLL_SyncData().Add(modelSD);
                        return "1";
                    }
                    else
                    {
                        return "2";
                    }
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
        public static string DelData(string dataId, string userId)
        {
            try
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
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS(dataId, "", string.Format("删除文件夹|操作人{0}|文件夹Id{1}|路径{2}", userId, dataId, "BookAuditList.aspx"));
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(dataId, "", string.Format("删除文件资源|操作人{0}|文件夹Id{1}|路径{2}", userId, dataId, "BookAuditList.aspx"));
                return "0";
            }
        }

    }
}