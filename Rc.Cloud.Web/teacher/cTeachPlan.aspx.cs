using Rc.BLL.Resources;
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
using Rc.Common.Config;
using Rc.Model.Resources;
namespace Homework.teacher
{
    public partial class cloudTeachPlan : Rc.Cloud.Web.Common.FInitData
    {
        StringBuilder strHtml = new StringBuilder();


        protected string strResource_Class = string.Empty;//资源类别 (云资源\自有资源)
        protected void Page_Load(object sender, EventArgs e)
        {
            //固定为教案
            // strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
            if (!IsPostBack)
            {
                string strResource_Type = string.Empty;//资源类型 （class类型文件、testPaper类型文件、ScienceWord类型文件、按属性生成的目录）
                //string strResource_Class = string.Empty;//资源类别 (云资源\自有资源)
                string strGradeTermActive = string.Empty;//年级学期


                string strSubject = FloginUser.Subject;
                string strResource_Version = FloginUser.Resource_Version;

                strResource_Class = Resource_ClassConst.云资源;
                strResource_Type = Resource_TypeConst.class类型文件;
                if (!String.IsNullOrEmpty(Request["Resource_Class"]))
                {
                    strResource_Class = Request["Resource_Class"].ToString();//资源类别 (云资源\自有资源)
                }
                if (!String.IsNullOrEmpty(Request["Resource_Type"]))//资源类型 （class类型文件、testPaper类型文件、ScienceWord类型文件、按属性生成的目录）
                {
                    strResource_Type = Request["Resource_Type"].ToString();
                }
                if (!String.IsNullOrEmpty(Request["GradeTerm"]))//年级学期
                {
                    strGradeTermActive = Request["GradeTerm"].ToString();
                }
                DataTable dtUser_GradeTerm = GetUser_GradeTerm();

                if (strGradeTermActive == "")//第一次进来默认一个年级学期
                {
                    strGradeTermActive = getGradeTermOneID(dtUser_GradeTerm);
                }
                litSubNaiv.Text = GetSubNaiv(strResource_Class, strResource_Type, "").ToString();
                litTree.Text = GetLeftTree(strResource_Class, strResource_Type, strSubject, strResource_Version, strGradeTermActive, dtUser_GradeTerm).ToString();
                // GetSubNaiv();
                //litTree.Text = GetResourceAttribute().ToString();
            }
        }
        /// <summary>
        /// 得到左侧目录树
        /// </summary>
        /// <param name="strResource_Class"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strSubject"></param>
        /// <param name="strResource_Version"></param>
        /// <param name="strGradeTermActive"></param>
        /// <param name="dtUser_GradeTerm"></param>
        /// <returns></returns>
        private StringBuilder GetLeftTree(string strResource_Class
            , string strResource_Type
            , string strSubject
            , string strResource_Version
            , string strGradeTermActive
            , DataTable dtUser_GradeTerm)
        {
            StringBuilder strSubNaivHtml = new StringBuilder();
            string GradeTermOne = string.Empty;
            DataTable dtGetResourceFolder = new DataTable();


            //左边栏   TAB
            strSubNaivHtml.Append(GetLeftTreeTab(strResource_Class, strResource_Type, strGradeTermActive).ToString());
            if (strResource_Class == Resource_ClassConst.云资源)
            {
                //左边栏   年级学期
                // strSubNaivHtml.Append(GetUser_GradeTermHtml(strResource_Class, strResource_Type, dtUser_GradeTerm, strGradeTermActive).ToString());
                //左边栏   教材版本，学科
                //strSubNaivHtml.Append(GetResourceVersionSubject());
            }
            else if (strResource_Class == Resource_ClassConst.自有资源)
            {

            }

            dtGetResourceFolder = GetResourceFolder(strResource_Class, strResource_Type, strSubject, strResource_Version, strGradeTermActive);
            //书籍目录
            strSubNaivHtml.Append(GetResourceFolderHtml(dtGetResourceFolder, strResource_Class));
            return strSubNaivHtml;
        }
        /// <summary>
        /// 得到第一个年级学期的名称
        /// </summary>
        /// <param name="dtUser_GradeTerm"></param>
        /// <returns></returns>
        private string getGradeTermOneID(DataTable dtUser_GradeTerm)
        {
            string strTemp = string.Empty;
            if (dtUser_GradeTerm.Rows.Count > 0)
            {
                strTemp = dtUser_GradeTerm.Rows[0]["GradeTerm_ID"].ToString();
            }
            return strTemp;
        }

        /// <summary>
        /// 教材版本，学科
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetResourceVersionSubject()
        {
            StringBuilder strTempHtml = new StringBuilder();

            string strResource_VersionName//教材版本
                = Rc.Common.DBUtility.DbHelperSQL.GetSingle(string.Format("SELECT MAX(D_Name) AS D_NAME from Common_Dict WHERE Common_Dict_ID ='{0}'"
                , FloginUser.Resource_Version)).ToString();
            string strSubjectName//学科
                = Rc.Common.DBUtility.DbHelperSQL.GetSingle(string.Format("SELECT MAX(D_Name) AS D_NAME from Common_Dict WHERE Common_Dict_ID ='{0}'"
                , FloginUser.Subject)).ToString();

            strTempHtml.AppendFormat("<dt>{0} {1}</dt>", strResource_VersionName, strSubjectName);
            return strTempHtml;
        }
        /// <summary> 
        /// 得到老师的年级学期 table
        /// </summary>
        /// <returns></returns>
        private DataTable GetUser_GradeTerm()
        {
            //老师所属的年级学期
            string strSql = string.Empty;
            DataTable dtUser_GradeTerm = new DataTable();
            strSql = string.Format(@"SELECT A.GradeTerm_ID,B.D_Name FROM F_User_GradeTerm A
INNER JOIN Common_Dict B ON A.GradeTerm_ID=B.Common_Dict_ID 
WHERE A.UserId='{0}'", FloginUser.UserId);
            dtUser_GradeTerm = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            return dtUser_GradeTerm;
        }
        /// <summary>
        /// 得到老师的年级学期 HTML
        /// </summary>
        /// <param name="dtUser_GradeTerm"></param>
        /// <param name="GradeTermActive"></param>
        /// <param name="GradeTermOne"></param>
        /// <returns></returns>
        private StringBuilder GetUser_GradeTermHtml(string strResource_Class, string strResource_Type, DataTable dtUser_GradeTerm, string GradeTermActive)
        {
            StringBuilder strTempHtml = new StringBuilder();
            StringBuilder strTempHtmlTemp = new StringBuilder();
            string GradeTermActiveName = string.Empty;//当前学期名称
            if (dtUser_GradeTerm.Rows.Count > 1)//两个学期以上显示
            {
                strTempHtmlTemp.Append("<i class='fa fa-angle-down'></i>");
                strTempHtmlTemp.Append("<div>");
                for (int i = 0; i < dtUser_GradeTerm.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        GradeTermActiveName = dtUser_GradeTerm.Rows[i]["D_Name"].ToString();
                    }
                    strTempHtmlTemp.AppendFormat("<p><a href='cTeachPlan.aspx?{0}' {1}>{2}</a></p>"
                        , GetUrl(strResource_Class, strResource_Type, dtUser_GradeTerm.Rows[i]["GradeTerm_ID"].ToString())
                         , dtUser_GradeTerm.Rows[i]["GradeTerm_ID"].ToString() == GradeTermActive ? "class='active'" : ""
                         , dtUser_GradeTerm.Rows[i]["D_Name"]);

                }
                strTempHtmlTemp.Append("</div>");
            }
            DataRow[] dv = dtUser_GradeTerm.Select(string.Format(" GradeTerm_ID='{0}'", GradeTermActive));
            if (dv.Count() != 0)
            {
                GradeTermActiveName = dv != null ? dv[0]["D_Name"].ToString() : "";
            }
            strTempHtml.Append("<dt class='select'>");
            strTempHtml.AppendFormat("<span>{0}<span>"
                , GradeTermActiveName);

            strTempHtml.Append(strTempHtmlTemp);

            strTempHtml.Append("</dt>");
            return strTempHtml;
        }

        /// <summary>
        /// 得到资源目录 table
        /// </summary>
        /// <param name="strResource_Class"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strSubject"></param>
        /// <param name="strResource_Version"></param>
        /// <param name="strGradeTerm"></param>
        /// <returns></returns>
        private DataTable GetResourceFolder(string strResource_Class, string strResource_Type, string strSubject, string strResource_Version, string strGradeTermActive)
        {
            string strWhere = string.Empty;
            strWhere = string.Format(@"  1=1
            AND Resource_Class='{0}'
            AND Resource_Type='{1}'
            ", strResource_Class, strResource_Type);
            if (strResource_Class == Resource_ClassConst.自有资源)
            {
                strWhere += string.Format(" AND CreateFUser ='{0}'", FloginUser.UserId);
            }
            else if (strResource_Class == Resource_ClassConst.云资源)
            {
                //if (strGradeTermActive!="")
                //{
                //    strWhere += string.Format(" AND GradeTerm ='{0}'", strGradeTermActive);
                //}

                // strWhere += string.Format(" AND Subject ='{0}'", strSubject);
                //strWhere += string.Format(" AND Resource_Version ='{0}'", strResource_Version);
                strWhere += string.Format(" AND Book_ID IN(SELECT Book_ID FROM UserBuyResources WHERE ResourceFolder.Book_ID=UserBuyResources.Book_id AND UserBuyResources.UserId='{0}' )"
                   , FloginUser.UserId);
            }
            //else if (strResource_Class == Resource_ClassConst.共享资源)
            //{
            //    strWhere = " Resource_Type='" + strResource_Type + "' and  ResourceFolder_Id in(select distinct (ResourceFolder_Id) from ResourceShare where ShareObjectId=(select SchoolId from dbo.VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "'))";
            //}
            strWhere += " order by ResourceFolder_Order,ResourceFolder_Name ";
            DataTable dtResourceFolder = new Rc.BLL.Resources.BLL_ResourceFolder().GetList(strWhere).Tables[0];
            return dtResourceFolder;
            //DataTable dtUser_GradeTerm = GetUser_GradeTerm();
            //return GetSubNaivNew(strResource_Type, dtUser_GradeTerm, strGradeTermActive);
        }
        /// <summary>
        /// 得到资源目录 html
        /// </summary>
        /// <param name="dtGetResourceFolder"></param>
        /// <param name="strResource_Class"></param>
        /// <returns></returns>
        private StringBuilder GetResourceFolderHtml(DataTable dtGetResourceFolder, string strResource_Class)
        {
            strHtml = new StringBuilder();
            StringBuilder strFolderHtml = new StringBuilder();
            DataView dvw = new DataView();

            dvw.Table = dtGetResourceFolder;
            //dvw.RowFilter = string.Format(" Resource_Version ='{0}'", ResourceVersion_ID);
            strFolderHtml.Append("<div class='mtree mtree-hook'>");
            if (dtGetResourceFolder.Rows.Count == 0)
            {
                strFolderHtml.Append("<div class=\"text-center\">暂无数据</div>");
            }
            else
            {
                strFolderHtml.Append(InitNavigationTree("0", strResource_Class, dvw));
            }

            strFolderHtml.Append("</div>");
            return strFolderHtml;
        }
        /// <summary>
        /// 左边栏 TAB
        /// </summary>
        /// <param name="strResource_Type"></param>
        /// <returns></returns>
        private StringBuilder GetLeftTreeTab(string strResourceClass, string strResource_Type, string strGradeTermActive)
        {
            StringBuilder strTempHtml = new StringBuilder();
            strTempHtml.AppendFormat("<ul class=\"resource_type\">");
            strTempHtml.AppendFormat("<li title='class文档类型' {0}> <a href='cTeachPlan.aspx?{1}'>Class</a></li>"
              , strResource_Type == Rc.Common.Config.Resource_TypeConst.class类型文件 ? "class='active'" : ""
              , GetUrl(strResourceClass, Rc.Common.Config.Resource_TypeConst.class类型文件, strGradeTermActive)
              );
            strTempHtml.AppendFormat("<li title='Sinceword文档类型'{0}><a href='cTeachPlan.aspx?{1}'>ScienceWord</a></li>"
                , strResource_Type == Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件 ? "class='active'" : ""
                , GetUrl(strResourceClass, Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件, strGradeTermActive)
                );

            strTempHtml.AppendFormat("</ul>");

            return strTempHtml;
        }
        /// <summary>
        /// 得到二级菜单
        /// </summary>
        protected StringBuilder GetSubNaiv(string strResource_Class, string strResource_Type, string strGradeTermActive)
        {
            StringBuilder strNaiv = new StringBuilder();
            strNaiv.AppendFormat(@"<li><a href='cTeachPlan.aspx?{1}' {2}>云教案</a></li>
                <li><a href='cTeachPlan.aspx?{3}' {4}>自有教案</a></li>"
                , string.IsNullOrEmpty(FloginUser.TrueName) ? FloginUser.UserName : FloginUser.TrueName
                , GetUrl(Resource_ClassConst.云资源, strResource_Type, strGradeTermActive)
                , strResource_Class == Resource_ClassConst.云资源 ? "class='active'" : ""
                , GetUrl(Resource_ClassConst.自有资源, strResource_Type, strGradeTermActive)
                , strResource_Class == Resource_ClassConst.自有资源 ? "class='active'" : ""
                );
            return strNaiv;
        }
        /// <summary>
        /// 得到跳转的URL
        /// </summary>
        /// <param name="strResource_Class"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strGradeTermActive"></param>
        /// <returns></returns>
        private string GetUrl(string strResource_Class, string strResource_Type, string strGradeTermActive)
        {
            string strTemp = string.Empty;
            strTemp += string.Format("Resource_Class={0}&Resource_Type={1}&GradeTerm={2}"
                , strResource_Class, strResource_Type, strGradeTermActive);
            return strTemp;
        }
        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        /// <param name="TreeIDCurrent"></param>
        /// <param name="TreeLevelCurrent"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected StringBuilder InitNavigationTree(string TreeIDCurrent, string strResource_Class, DataView dvw)
        {
            string strWhere = string.Empty;
            strWhere = " 1=1 ";
            if (TreeIDCurrent == "0")
            {
                if (strResource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                {
                    dvw.RowFilter = string.Format("  {0} and ResourceFolder_Level ='5'", strWhere);
                }
                else if (strResource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                {
                    dvw.RowFilter = string.Format("  {0} and ResourceFolder_ParentId ='0'", strWhere);
                }

            }
            else
            {
                dvw.RowFilter = string.Format(" {0} and  ResourceFolder_ParentId = '{1}' ", strWhere, TreeIDCurrent);
            }
            if (dvw.Count > 0)
            {
                int subProcess = 0;
                int subMax = dvw.Count;
                string liClass = string.Empty;
                foreach (DataRowView drv in dvw)
                {
                    subProcess++;
                    if (subProcess == 1)
                    {
                        int level = 0;
                        int.TryParse(drv["ResourceFolder_Level"].ToString(), out level);
                        if (strResource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            strHtml.AppendFormat(" <ul data-level='{0}'>", level - 5 + 1);
                        }
                        else
                        {
                            strHtml.AppendFormat(" <ul data-level='{0}'>", level + 1);
                        }
                    }
                    strHtml.Append("<li>");

                    //strHtml.Append("<div class=\"name\">");
                    //strHtml.AppendFormat(" <i class='tree_btn fa'></i>");
                    //strHtml.AppendFormat(" <a href='##'  onclick=\"ShowSubDocument('{0}')\" >{1}</a>"
                    //    , drv["ResourceFolder_Id"].ToString()
                    //    , drv["ResourceFolder_Name"].ToString().ReplaceForFilter());
                    //strHtml.Append(" </div>");

                    strHtml.AppendFormat("<div class='mtree_link mtree-link-hook' data-id='{0}'>"
                        , drv["ResourceFolder_Id"].ToString());
                    strHtml.Append("<div class='mtree_indent mtree-indent-hook'></div>");
                    strHtml.Append("<div class='mtree_btn mtree-btn-hook'></div>");
                    strHtml.AppendFormat("<div class='mtree_name mtree-name-hook'>{0}</div>"
                        , drv["ResourceFolder_Name"].ToString().ReplaceForFilter());
                    strHtml.Append("</div>");

                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), strResource_Class, dvw);

                    strHtml.Append("</li>");
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</ul>");
                    }

                }
            }
            return strHtml;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="ResourceFolder_Id">文件夹标识</param>
        /// <param name="DocName">文档名称的查询条件</param>
        /// <param name="ShowFolderIn">显示某个文件夹中的文件 ：1搜索 0当前目录</param>
        /// <param name="strResource_Class">资源类别（标识）</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCloudResource(string ResourceFolder_Id, string DocName, string ShowFolderIn, string strResource_Class, int PageSize, int PageIndex)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
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
                if (!string.IsNullOrEmpty(DocName)) strWhere = " and File_Name like '%" + DocName.Filter() + "%' ";
                //strWhere += string.Format(" and LessonPlan_Type='{0}' and Resource_Class = '{1}'", strLessonPlan_Type.Filter(), strResource_Class.Filter());
                strWhere += string.Format(" and Resource_Class = '{0}'", strResource_Class.Filter());
                if (Resource_ClassConst.云资源 == strResource_Class)
                {
                    strWhere += string.Format(" AND Book_ID IN(SELECT Book_ID FROM UserBuyResources WHERE A.Book_ID=UserBuyResources.Book_id AND UserBuyResources.UserId='{0}' )"
                    , FloginUser.UserId);
                }

                if (ShowFolderIn != "1")
                    strWhere += " and ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' ";
                else
                    strWhere += " and 1<>1 ";
                //strWhere += string.Format(" and Resource_Class='{0}' and Resource_Type='{1}'", strResource_Class, strResource_Type);
                strSqlCount = @"select count(*) from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id  where 1=1 " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.ResourceToResourceFolder_Order,A.Resource_Name) row,A.*,B.Resource_ContentLength from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id  where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                string operateStr = string.Empty;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    Rc.Interface.BookAttrModel bkAttrModel = GetBookAttrValue(dtRes.Rows[i]["Book_ID"].ToString());
                    inum++;
                    string docName = dtRes.Rows[i]["File_Name"].ToString().ReplaceForFilter();
                    //docName = pfunction.GetDocFileName(docName);
                    string resUrl = "Upload/Resource/" + dtRes.Rows[i]["Resource_Url"].ToString().Replace("\\", "/");
                    if (!string.IsNullOrEmpty(resUrl) && resUrl.IndexOf(".") > 0)
                    {
                        resUrl = resUrl.Substring(0, resUrl.LastIndexOf(".")) + ".htm";
                    }
                    if (dtRes.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {

                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        docType = dtRes.Rows[i]["File_Suffix"].ToString(),
                        // docTypeAll = dtRes.Rows[i]["File_Suffix"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
                        docUrl = pfunction.GetResourceHost("TeachingPlanViewWebSiteUrl") + resUrl,
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IsDownload = bkAttrModel.IsSave,
                        Resource_Class = dtRes.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.自有资源 ? true : false,
                        IsShare = dtRes.Rows[i]["Resource_shared"].ToString()
                        //Opearate = operateStr
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

        /// <summary>
        /// 获取资源属性（是否可打印，存盘）
        /// </summary>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        public static Rc.Interface.BookAttrModel GetBookAttrValue(string ResourceFolder_Id)
        {
            Rc.Interface.BookAttrModel model = new Rc.Interface.BookAttrModel();
            model.IsPrint = false;
            model.IsSave = false;
            try
            {
                List<Model_BookAttrbute> listModel = new BLL_BookAttrbute().GetModelList("ResourceFolder_Id='" + ResourceFolder_Id + "'");
                foreach (var item in listModel)
                {
                    if (item.AttrEnum == BookAttrEnum.Print.ToString() && item.AttrValue == "1")
                    {
                        model.IsPrint = true;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Save.ToString() && item.AttrValue == "1")
                    {
                        model.IsSave = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return model;
        }
        /// <summary>
        /// 分享资源
        /// </summary>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string share(string ResourceToResourceFolder_Id)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
            try
            {
                string strsql = "select distinct(SchoolId) from dbo.VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "' and SchoolId<>''";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strsql).Tables[0];

                Model_ResourceShare rs = new Model_ResourceShare();
                rs.ResourceShareId = Guid.NewGuid().ToString();
                rs.ResourceToResourceFolder_Id = ResourceToResourceFolder_Id;
                rs.ShareObjectId = dt.Rows.Count > 0 ? dt.Rows[0]["SchoolId"].ToString() : "";
                rs.CreateUserId = FloginUser.UserId;
                rs.CreateTime = DateTime.Now;

                Model_ResourceToResourceFolder rtr = new Model_ResourceToResourceFolder();
                BLL_ResourceToResourceFolder rtrbll = new BLL_ResourceToResourceFolder();
                rtr = rtrbll.GetModel(ResourceToResourceFolder_Id);
                rtr.Resource_shared = "1";
                BLL_ResourceShare bllrs = new BLL_ResourceShare();
                if (bllrs.ShareResource(rs, rtr))
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                return "";
            }
        }
        /// <summary>
        /// 取消分享资源
        /// </summary>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string cancel(string ResourceToResourceFolder_Id)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
            try
            {
                Model_ResourceToResourceFolder rtr = new Model_ResourceToResourceFolder();
                BLL_ResourceToResourceFolder rtrbll = new BLL_ResourceToResourceFolder();
                rtr = rtrbll.GetModel(ResourceToResourceFolder_Id);
                rtr.Resource_shared = "";
                BLL_ResourceShare bllrs = new BLL_ResourceShare();
                if (bllrs.CancelShareResource(ResourceToResourceFolder_Id, rtr))
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}