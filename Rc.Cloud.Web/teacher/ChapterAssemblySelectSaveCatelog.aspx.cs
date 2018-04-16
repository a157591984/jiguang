using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Common.Config;
using System.Data;
using System.Text;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.teacher
{
    public partial class ChapterAssemblySelectSaveCatelog : Rc.Cloud.Web.Common.FInitData
    {
        StringBuilder stbHtml = new StringBuilder();
        protected string userId = string.Empty;
        protected string Identifier_Id = string.Empty;
        protected string ReName = string.Empty;
        protected string Title = string.Empty;
        protected string ComplexityTexts = string.Empty;
        protected string RtrfType = string.Empty;
        protected string strTestpaperViewWebSiteUrl = string.Empty;
        protected string ugid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ugid = Request["ugid"].Filter();
            strTestpaperViewWebSiteUrl = pfunction.GetResourceHost("TestWebSiteUrl");
            Identifier_Id = Request["Identifier_Id"].Filter();
            ReName = Request["ReName"].Filter();
            Title = Request["Title"].Filter();
            ComplexityTexts = Request["ComplexityText"].Filter();
            RtrfType = Request["RtrfType"].Filter();
            userId = FloginUser.UserId;
            if (!IsPostBack)
            {
                InitResourceFolder();
                loadData();
            }
        }

        /// <summary>
        /// 初始化老师章节组卷文件夹
        /// </summary>
        private void InitResourceFolder()
        {
            string Resource_Type = Resource_TypeConst.章节组卷;
            string Resource_Class = Resource_ClassConst.自有资源;
            string ResourceFolder_Owner = userId;
            BLL_ResourceFolder bll = new BLL_ResourceFolder();
            bll.InitTchChapterAssemblyRF(Resource_Type, Resource_Class, ResourceFolder_Owner);
        }

        private void loadData()
        {
            string strWhere = string.Format(" ResourceFolder_Owner='{0}' and Resource_Type='{1}' order by ResourceFolder_Order "
                , userId, Resource_TypeConst.章节组卷);
            DataTable dt = new DataTable();
            dt = new BLL_ResourceFolder().GetList(strWhere).Tables[0];
            ltlRF.Text = InitRFTree("0", dt).ToString();
        }

        /// <summary>
        /// 递归加载目录树
        /// </summary>
        protected StringBuilder InitRFTree(string parentId, DataTable dt)
        {
            string strWhere = string.Empty;
            strWhere = " 1=1 ";
            DataRow[] dr = dt.Select("ResourceFolder_ParentId='" + parentId + "'");

            if (dr.Length > 0)
            {

                foreach (DataRow item in dr)
                {
                    if (parentId == "0") stbHtml.Append("<ul>");
                    stbHtml.Append("<li>");
                    stbHtml.AppendFormat("<div class=\"mtree_link mtree-link-hook\" data-id=\"{0}\" title=\"{1}\">"
                        , item["ResourceFolder_Id"], item["ResourceFolder_Name"].ToString().ReplaceForFilter());
                    stbHtml.Append("<div class=\"mtree_indent mtree-indent-hook\"></div>");
                    stbHtml.Append("<div class=\"mtree_btn mtree-btn-hook\"></div>");
                    stbHtml.AppendFormat("<div class=\"mtree_name mtree-name-hook\">{0}</div>", item["ResourceFolder_Name"].ToString().ReplaceForFilter());
                    stbHtml.Append("<div class=\"mtree_opera mtree-opera-hook\">");
                    stbHtml.Append("<div class=\"mtree_add mtree-add-hook\"><i class=\"material-icons\">&#xE145;</i></div>");
                    //根目录不允许编辑、删除
                    if (parentId != "0") stbHtml.Append("<div class=\"mtree_edit mtree-edit-hook\"><i class=\"material-icons\">&#xE150;</i></div>");
                    if (parentId != "0") stbHtml.Append("<div class=\"mtree_del mtree-del-hook\"><i class=\"material-icons\">&#xE872;</i></div>");
                    stbHtml.Append("</div>");
                    stbHtml.Append("</div>");
                    //递归加载子级目录
                    //if (dt.Select("ResourceFolder_ParentId='" + item["ResourceFolder_Id"].ToString() + "'").Length > 0)
                    //{
                    stbHtml.Append("<ul>");
                    InitRFTree(item["ResourceFolder_Id"].ToString(), dt);
                    stbHtml.Append("</ul>");
                    //}
                    stbHtml.Append("</li>");
                    if (parentId == "0") stbHtml.Append("</ul>");
                }

            }
            return stbHtml;
        }

        [WebMethod]
        public static string ModifyFolder(string userId, string name, string id, string pid)
        {
            try
            {
                userId = userId.Filter();
                name = name.Filter();
                id = id.Filter();
                pid = pid.Filter();
                BLL_ResourceFolder bll = new BLL_ResourceFolder();
                Model_ResourceFolder model = new Model_ResourceFolder();
                bool flag = false;
                if (string.IsNullOrEmpty(id))
                {
                    #region 添加
                    //文件夹名称已存在
                    if (bll.GetRecordCount("ResourceFolder_ParentId='" + pid + "' and ResourceFolder_Name='" + name + "' ") > 0)
                    {
                        return "2";
                    }

                    Model_ResourceFolder modelParent = bll.GetModel(pid);
                    model.ResourceFolder_Id = Guid.NewGuid().ToString();
                    model.ResourceFolder_ParentId = pid;
                    model.ResourceFolder_Name = name.Filter();
                    model.ResourceFolder_Level = modelParent.ResourceFolder_Level + 1;
                    model.Resource_Type = modelParent.Resource_Type;
                    model.Resource_Class = modelParent.Resource_Class;
                    model.ResourceFolder_Order = 1;
                    model.ResourceFolder_Owner = userId;
                    model.CreateFUser = userId;
                    model.CreateTime = DateTime.Now;
                    model.ResourceFolder_isLast = "0";
                    if (bll.Add(model))
                    {
                        flag = true;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    //文件夹名称已存在
                    if (bll.GetRecordCount("ResourceFolder_Id!='" + id + "' and ResourceFolder_ParentId='" + pid + "' and ResourceFolder_Name='" + name + "' ") > 0)
                    {
                        return "2";
                    }

                    model = bll.GetModel(id);
                    model.ResourceFolder_Name = name.Filter();
                    if (bll.Update(model))
                    {
                        flag = true;
                    }
                    #endregion
                }
                if (flag)
                {
                    return model.ResourceFolder_Id;
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
        public static string DelFolder(string id)
        {
            try
            {
                id = id.Filter();
                BLL_ResourceFolder bll = new BLL_ResourceFolder();
                if (bll.GetRecordCount("ResourceFolder_ParentId='" + id + "'") > 0)
                {
                    return "2";//存在子级目录
                }
                if (new BLL_ResourceToResourceFolder().GetRecordCount("ResourceFolder_Id='" + id + "'") > 0)
                {
                    return "3";//目录中存在资源文件
                }
                if (bll.Delete(id))
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