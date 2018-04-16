using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Text;
using Rc.Common.Config;
using System.Data;
using System.Web.Services;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.student
{
    public partial class microClass : Rc.Cloud.Web.Common.FInitData
    {
        StringBuilder strHtml = new StringBuilder();

        protected string strResource_Class = Resource_ClassConst.云资源;//资源类别 (云资源\自有资源)
        protected void Page_Load(object sender, EventArgs e)
        {
            string strResource_Type = Rc.Common.Config.Resource_TypeConst.class类型微课件;
            if (!IsPostBack)
            {
                litTree.Text = GetLeftTree(strResource_Class, strResource_Type).ToString();

            }
        }
        /// <summary>
        /// 得到左侧目录树
        /// </summary>
        private StringBuilder GetLeftTree(string strResource_Class, string strResource_Type)
        {
            StringBuilder strSubNaivHtml = new StringBuilder();
            string GradeTermOne = string.Empty;
            DataTable dtGetResourceFolder = GetResourceFolder(strResource_Class, strResource_Type);
            //书籍目录
            strSubNaivHtml.Append(GetResourceFolderHtml(dtGetResourceFolder, strResource_Class));
            return strSubNaivHtml;
        }

        /// <summary>
        /// 得到资源目录 table
        /// </summary>
        private DataTable GetResourceFolder(string strResource_Class, string strResource_Type)
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
                strWhere += string.Format(" AND Book_ID IN(SELECT Book_ID FROM UserBuyResources WHERE ResourceFolder.Book_ID=UserBuyResources.Book_id AND UserBuyResources.UserId='{0}' )"
                   , FloginUser.UserId);
            }
            strWhere += " order by ResourceFolder_Order,ResourceFolder_Name ";
            DataTable dtResourceFolder = new Rc.BLL.Resources.BLL_ResourceFolder().GetList(strWhere).Tables[0];
            return dtResourceFolder;
        }
        /// <summary>
        /// 得到资源目录 html
        /// </summary>
        private StringBuilder GetResourceFolderHtml(DataTable dtGetResourceFolder, string strResource_Class)
        {
            StringBuilder strFolderHtml = new StringBuilder();
            DataView dvw = new DataView();

            dvw.Table = dtGetResourceFolder;
            strFolderHtml.Append("<div class='tree sidebar_menu'>");
            if (dtGetResourceFolder.Rows.Count == 0)
            {
                strFolderHtml.Append("<div class=\"text-center\">对不起，没有找到资源</div>");
            }
            else
            {
                string strBuyRe = string.Format(@" and Common_Dict_Id in( select [Subject] from dbo.ResourceFolder
where book_id in(select Book_Id from dbo.UserBuyResources where UserId='{0}') ) ", FloginUser.UserId);
                List<Model_Common_Dict> listDict = new BLL_Common_Dict().GetModelList("D_Type='7' " + strBuyRe + " order by D_Name ");
                foreach (var item in listDict)
                {
                    strFolderHtml.Append("<ul data-level='0'>");
                    strFolderHtml.Append("<li>");
                    strFolderHtml.AppendFormat("<div class=\"name\"> <i class='tree_btn fa'></i> <a href='##' onclick=\"ShowSubDocument('{0}','1')\">{1}</a></div>", item.Common_Dict_ID, item.D_Name);
                    strFolderHtml.Append(InitNavigationTree("0", strResource_Class, item.Common_Dict_ID, dvw));
                    strFolderHtml.Append("</li>");
                    strFolderHtml.Append("</ul>");
                }
            }

            strFolderHtml.Append("</div>");
            return strFolderHtml;
        }

        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        protected StringBuilder InitNavigationTree(string TreeIDCurrent, string strResource_Class, string strSubject, DataView dvw)
        {
            string strWhere = string.Empty;
            strWhere = " Subject='" + strSubject + "' ";
            if (TreeIDCurrent == "0")
            {
                strHtml = new StringBuilder();
                dvw.RowFilter = string.Format("  {0} and ResourceFolder_Level ='5'", strWhere);
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
                        int level = 1;
                        int.TryParse(drv["ResourceFolder_Level"].ToString(), out  level);
                        strHtml.AppendFormat(" <ul data-level='{0}'>", level - 4);
                        strHtml.Append("<li>");
                    }
                    strHtml.Append("<div class=\"name\">");
                    strHtml.AppendFormat(" <i class='tree_btn fa'></i>");
                    strHtml.AppendFormat(" <a href='##'  onclick=\"ShowSubDocument('{0}','2')\">{1}</a>"
                        , drv["ResourceFolder_Id"].ToString()
                        , drv["ResourceFolder_Name"].ToString()
                        );
                    strHtml.Append(" </div>");
                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), strResource_Class, strSubject, dvw);
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</li>");
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
        public static string GetCloudResource(string ResourceFolder_Id, string DocName, string IType, int PageIndex, int PageSize)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
            try
            {
                ResourceFolder_Id = ResourceFolder_Id.Filter();
                DocName = DocName.Filter();
                IType = IType.Filter();

                int pageSize = 10;
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = " Resource_Type='" + Resource_TypeConst.class类型微课件 + "' ";
                if (!string.IsNullOrEmpty(DocName)) strWhere = " and File_Name like '%" + DocName.Filter() + "%' ";
                strWhere += string.Format(" and Resource_Class = '{0}'", Resource_ClassConst.云资源);
                strWhere += string.Format(" AND Book_ID IN(SELECT Book_ID FROM UserBuyResources WHERE A.Book_ID=UserBuyResources.Book_id AND UserBuyResources.UserId='{0}' )", FloginUser.UserId);


                if (IType == "1")
                {
                    strWhere += " and Subject='" + ResourceFolder_Id + "' ";
                }
                else
                {
                    strWhere += " and ResourceFolder_Id='" + ResourceFolder_Id + "' ";
                }

                strSqlCount = @"select count(*) from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id  where " + strWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.ResourceToResourceFolder_Order,A.Resource_Name) row,A.*,B.Resource_ContentLength from ResourceToResourceFolder A
INNER JOIN Resource B ON A.Resource_Id=B.Resource_Id  where "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * pageSize + 1) + " and " + (PageIndex * pageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    Rc.Cloud.Web.AuthApi.index.BookAttrModel bkAttrModel = GetBookAttrValue(dtRes.Rows[i]["Book_ID"].ToString());
                    inum++;
                    string docName = dtRes.Rows[i]["File_Name"].ToString();
                    string resUrl = "Upload/Resource/" + dtRes.Rows[i]["Resource_Url"].ToString().Replace("\\", "/");
                    if (!string.IsNullOrEmpty(resUrl) && resUrl.IndexOf(".") > 0)
                    {
                        resUrl = resUrl.Substring(0, resUrl.LastIndexOf(".")) + ".htm";
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceToResourceFolder_Id"].ToString(),
                        docName = docName,
                        docType = dtRes.Rows[i]["File_Suffix"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
                        docUrl = pfunction.GetResourceHost("TeachingPlanViewWebSiteUrl") + resUrl,
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IsDownload = bkAttrModel.IsSave
                    });
                }
                #endregion

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = pageSize,
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
        public static Rc.Cloud.Web.AuthApi.index.BookAttrModel GetBookAttrValue(string ResourceFolder_Id)
        {
            Rc.Cloud.Web.AuthApi.index.BookAttrModel model = new Rc.Cloud.Web.AuthApi.index.BookAttrModel();
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
    }
}