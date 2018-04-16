using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using System.Data;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Cloud.Model;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Res_BookArea : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        public string _t = string.Empty;
        protected string s = string.Empty;
        static Model_Struct_Func StaticUserFun;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "资源所属区域维护";
            Module_Id = "10400200";
            strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                //省
                dt = new Rc.BLL.Resources.BLL_Regional_Dict().GetList("D_PartentID=''  ORDER BY CONVERT(INT,D_Code)").Tables[0];
                pfunction.SetDdl(ddlProvince, dt, "D_Name", "Regional_Dict_Id", "省份");
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
                //年份
                pfunction.SetDdlYear(ddlParticularYear, DateTime.Now.Year - 5, DateTime.Now.Year + 2, true);
            }
            StaticUserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
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
        public static string GetCloudBooks(string DocName, string strResource_Type, string strResource_Class, string strGradeTerm, string strSubject, string strResource_Version, string strParticularYear, string Province, string City, int PageIndex, int PageSize)
        {
            try
            {
                DocName = DocName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(DocName)) strWhere = " and ResourceFolder_Name like '%" + DocName.Filter() + "%' ";
                if (strResource_Class != "-1") strWhere += " and Resource_Class = '" + strResource_Class.Filter() + "' ";
                if (strResource_Type != "-1") strWhere += " and Resource_Type = '" + strResource_Type.Filter() + "' ";
                if (strGradeTerm != "-1") strWhere += " and GradeTerm = '" + strGradeTerm.Filter() + "' ";
                if (strSubject != "-1") strWhere += " and Subject = '" + strSubject.Filter() + "' ";
                if (strResource_Version != "-1") strWhere += " and Resource_Version = '" + strResource_Version.Filter() + "' ";
                if (strParticularYear != "-1") strWhere += " and ParticularYear = '" + strParticularYear.Filter() + "' ";
                if (Province != "-1") strWhere += " and ResourceFolder_Id in(SELECT ResourceFolder_Id FROM BookArea where Province_ID='" + Province + "') ";
                if (City != "-1") strWhere += " and ResourceFolder_Id in(SELECT ResourceFolder_Id FROM BookArea where City_Id='" + City + "') ";
                //if (tp == "0") strWhere += " and ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' ";

                strWhere += " AND ResourceFolder_Level=5";//管理员维护的书籍目录
                strSqlCount = @"select count(*) from ResourceFolder A   where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.* 
,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName,t4.D_Name as Resource_TypeName
from ResourceFolder A 
left join Common_Dict t1 on t1.Common_Dict_Id=A.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=A.Subject
left join Common_Dict t3 on t3.Common_Dict_Id=A.Resource_Version
left join Common_Dict t4 on t4.Common_Dict_Id=A.Resource_Type
where 1=1 " + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;

                DataTable dtArea = Rc.Common.DBUtility.DbHelperSQL.Query(@"select t.ResourceFolder_Id,t2.D_Name as ProvinceName,t3.D_Name as CityName from BookArea t
left join Regional_Dict t2 on t2.Regional_Dict_ID=t.Province_ID
left join Regional_Dict t3 on t3.Regional_Dict_ID=t.City_ID ").Tables[0];

                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter();
                    docName = pfunction.GetDocFileName(docName);
                    string strOperate = string.Empty;
                    if (StaticUserFun.Add)
                    {
                        strOperate = string.Format("<a title=\"维护地域\"  href=\"javascript:;\" onclick=\"showPop('{0}','{1}')\" >维护地域</a>"
                            , dtRes.Rows[i]["ResourceFolder_Id"].ToString(), docName);
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        GradeTerm = dtRes.Rows[i]["GradeTermName"].ToString(),
                        Subject = dtRes.Rows[i]["SubjectName"].ToString(),
                        Resource_Version = dtRes.Rows[i]["Resource_VersionName"].ToString(),
                        Resource_Type = dtRes.Rows[i]["Resource_TypeName"].ToString(),
                        ParticularYear = dtRes.Rows[i]["ParticularYear"].ToString(),
                        docName = docName,
                        docNameSub = pfunction.GetSubstring(docName, 25, true),
                        AreaInfo = GetResAreaName(dtArea, dtRes.Rows[i]["ResourceFolder_Id"].ToString()),
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

        private static string GetResAreaName(DataTable dtArea, string ResourceFolder_Id)
        {
            string temp = string.Empty;
            try
            {
                DataRow[] drArea = dtArea.Select("ResourceFolder_Id='" + ResourceFolder_Id + "'");
                foreach (DataRow item in drArea)
                {
                    string regionalName = item["ProvinceName"].ToString() + item["CityName"].ToString();
                    if (!string.IsNullOrEmpty(regionalName))
                    {
                        temp += regionalName + ",";
                    }
                }
                temp = temp.TrimEnd(',');
            }
            catch (Exception)
            {

            }
            return temp;
        }

        /// <summary>
        /// 获取市区
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetAreaCityInfo(string pid)
        {
            string temp = string.Empty;
            temp = "<option value=\"-1\">市区</option>";
            try
            {
                List<Model_Regional_Dict> list = new List<Model_Regional_Dict>();
                list = new BLL_Regional_Dict().GetModelList("D_PartentID='" + pid.Filter() + "' ORDER BY CONVERT(INT,D_Code)");
                foreach (var item in list)
                {
                    temp += string.Format("<option value=\"{0}\">{1}</option>", item.Regional_Dict_ID, item.D_Name);
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }

    }
}