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
    public partial class SxxmbAuditList : Rc.Cloud.Web.Common.InitPage
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
            Module_Id = "30100200";
            userId = loginUser.SysUser_ID;
            strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
            strGradeTerm = ddlGradeTerm.SelectedValue;
            strSubject = ddlSubject.SelectedValue;
            strResource_Version = ddlResource_Version.SelectedValue;
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "请选择");

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
        /// 双向细目表
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
        public static string GetTwo_WayChecklist(string DocName, string strGradeTerm, string strSubject, string strResource_Version, string AuditState, int PageIndex, int PageSize)
        {
            try
            {
                DocName = DocName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                Rc.Cloud.Model.Model_Struct_Func UserFun;
                UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, (loginUser == null ? "''" : clsUtility.ReDoStr(loginUser.SysRole_IDs, ',')), "30100200");
                #region 资源信息
                DataTable dtTwo = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(DocName)) strWhere += " and Two_WayChecklist_Name like '%" + DocName.Filter() + "%' ";
                if (strGradeTerm != "-1") strWhere += " and GradeTerm = '" + strGradeTerm.Filter() + "' ";
                if (strSubject != "-1") strWhere += " and Subject = '" + strSubject.Filter() + "' ";
                if (strResource_Version != "-1") strWhere += " and Resource_Version = '" + strResource_Version.Filter() + "' ";
                if (AuditState != "-1")
                {
                    if (AuditState == "3")
                    {
                        strWhere += " and Status is null ";
                    }
                    else
                    {
                        strWhere += " and Status='" + AuditState + "' ";
                    }
                }
                strWhere += " and A.Two_WayChecklist_Id in(select Two_WayChecklist_Id  from Two_WayChecklistToTestpaper)";
                strSqlCount = @"select count(1) from Two_WayChecklist A  
left JOIN  Two_WayChecklistAudit t on t.Two_WayChecklist_Id=A.Two_WayChecklist_Id  where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY t.Status,t.CreateTime DESC) row,A.*,t.Status,t.CreateTime AuditTime,UserName=isnull(su.SysUser_Name,su.SysUser_LoginName)  
,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName
from Two_WayChecklist A  
left JOIN  Two_WayChecklistAudit t on t.Two_WayChecklist_Id=A.Two_WayChecklist_Id
left join  SysUser su on su.SysUser_ID=t.CreateUser
left join Common_Dict t1 on t1.Common_Dict_Id=A.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=A.Subject
left join Common_Dict t3 on t3.Common_Dict_Id=A.Resource_Version
where 1=1 " + strWhere + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtTwo = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtTwo.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtTwo.Rows[i]["Two_WayChecklist_Name"].ToString();
                    //docName = pfunction.GetDocFileName(docName);
                    string strOperate = string.Empty;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtTwo.Rows[i]["Two_WayChecklist_Id"].ToString(),
                        GradeTerm = dtTwo.Rows[i]["GradeTermName"].ToString(),
                        Subject = dtTwo.Rows[i]["SubjectName"].ToString(),
                        Resource_Version = dtTwo.Rows[i]["Resource_VersionName"].ToString(),
                        docName = docName,
                        docNameSub = pfunction.GetSubstring(docName, 30, true),
                        AuditTime = pfunction.ConvertToLongDateTime(dtTwo.Rows[i]["AuditTime"].ToString()),
                        AuditStateStr = dtTwo.Rows[i]["Status"].ToString() == "" ? "<span style='color:red'>未审核</span>" : dtTwo.Rows[i]["Status"].ToString() == "1" ? "<span style='color:green'>已审核</span>" : "<span style='color:blue'>审核未通过</span>",
                        UserName = dtTwo.Rows[i]["UserName"].ToString(),
                        Status = dtTwo.Rows[i]["Status"].ToString(),
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
        public static string SxxmbAudit(string reid, string rename, string type)
        {
            try
            {
                Model_VSysUserRole loginUser = HttpContext.Current.Session["LoginUser"] as Model_VSysUserRole;
                reid = reid.Filter();
                type = type.Filter();
                Model_Two_WayChecklistAudit model = new Model_Two_WayChecklistAudit();
                BLL_Two_WayChecklistAudit bll = new BLL_Two_WayChecklistAudit();
                model = bll.GetModel(reid);
                if (model == null)
                {
                    model = new Model_Two_WayChecklistAudit();
                    if (type == "1")
                    {
                        model.Status = 1;
                        model.Two_WayChecklist_Id = reid;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                    }
                    else
                    {
                        model.Status = 0;
                        model.Two_WayChecklist_Id = reid;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                    }
                    if (bll.Add(model))
                    {
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
                        model.Status = 1;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                    }
                    else
                    {
                        model = bll.GetModel(reid);
                        model.Status = 0;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                    }
                    if (bll.Update(model))
                    {
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