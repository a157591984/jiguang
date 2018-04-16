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
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SxxmbEmpower : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Type = string.Empty;//资源类型（class类型文件、testPaper类型文件、ScienceWord类型文件）
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string _t = string.Empty;
        protected string s = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30100300";

            strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
            //strResource_Type = ddlResource_Type.SelectedValue;
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
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "教材版本");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "年级学期");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "学科");

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
        public static string GetTwo_WayChecklist(string DocName, string strGradeTerm, string strSubject, string strResource_Version, int PageIndex, int PageSize)
        {
            try
            {
                DocName = DocName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                Rc.Cloud.Model.Model_Struct_Func UserFun;
                UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, (loginUser == null ? "''" : clsUtility.ReDoStr(loginUser.SysRole_IDs, ',')), "30100300");
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

                strWhere += " and A.Two_WayChecklistType='0'";
                strSqlCount = @"select count(1) from Two_WayChecklist A  where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.*,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName
from Two_WayChecklist A  
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
                        CreateTime = pfunction.ConvertToLongDateTime(dtTwo.Rows[i]["CreateTime"].ToString())
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
        public static string OperateResourceAuth(string AttrEnum, string ReId, string AttrValue)
        {
            string temp = "0";
            try
            {
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                BLL_BookAttrbute bll = new BLL_BookAttrbute();
                Model_BookAttrbute model = new Model_BookAttrbute();
                List<Model_BookAttrbute> list = bll.GetModelList("ResourceFolder_Id='" + ReId + "' and AttrEnum='" + AttrEnum + "' ");
                bool result = false;
                if (list.Count != 0)
                {
                    model = list[0];
                    model.AttrValue = AttrValue;
                    model.CreateTime = DateTime.Now;
                    result = bll.Update(model);
                }
                else
                {
                    model.BookAttrId = Guid.NewGuid().ToString();
                    model.ResourceFolder_Id = ReId;
                    model.AttrEnum = AttrEnum;
                    model.AttrValue = AttrValue;
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;
                    result = bll.Add(model);
                }
                if (result)
                {
                    temp = "1";
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }

    }
}