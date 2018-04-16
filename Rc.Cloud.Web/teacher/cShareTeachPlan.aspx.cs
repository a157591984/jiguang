using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Newtonsoft.Json;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.teacher
{
    public partial class cShareTeachPlan : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strResource_Class = Resource_ClassConst.共享资源;
                string strResource_Type = Resource_TypeConst.class类型文件;
                litSubNaiv.Text = GetSubNaiv(strResource_Class, strResource_Type, "").ToString();
            }
        }
        /// <summary>
        /// 得到二级菜单
        /// </summary>
        protected StringBuilder GetSubNaiv(string strResource_Class, string strResource_Type, string strGradeTermActive)
        {
            StringBuilder strNaiv = new StringBuilder();
            //string strUpload = string.Empty;

            strNaiv.AppendFormat(@"<li><a href='cTeachPlan.aspx?{1}' {2}>云教案</a></li>
                <li><a href='cTeachPlan.aspx?{3}' {4}>自有教案</a></li>
                <li><a href='cShareTeachPlan.aspx?{5}' {6}>共享教案</a></li>"
                , string.IsNullOrEmpty(FloginUser.TrueName) ? FloginUser.UserName : FloginUser.TrueName
                , GetUrl(Resource_ClassConst.云资源, strResource_Type, strGradeTermActive)
                , strResource_Class == Resource_ClassConst.云资源 ? "class='active'" : ""
                , GetUrl(Resource_ClassConst.自有资源, strResource_Type, strGradeTermActive)
                , strResource_Class == Resource_ClassConst.自有资源 ? "class='active'" : ""
                , GetUrl(Resource_ClassConst.共享资源, strResource_Type, strGradeTermActive)
                , strResource_Class == Resource_ClassConst.共享资源 ? "class='active'" : ""
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
        /// 加载数据
        /// </summary>
        /// <param name="ResourceFolder_Id">文件夹标识</param>
        /// <param name="DocName">文档名称的查询条件</param>
        /// <param name="ShowFolderIn">显示某个文件夹中的文件 ：1搜索 0当前目录</param>
        /// <param name="strResource_Class">资源类别（标识）</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCloudResource(string Resource_Type, int PageSize, int PageIndex)
        {
            Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
            try
            {
                Resource_Type = Resource_Type.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(Resource_Type) && Resource_Type != "1")
                {
                    strWhere += string.Format(" and rtr.Resource_Type = '{0}'", Resource_Type.Filter());
                }
                if (!string.IsNullOrEmpty(FloginUser.Subject) && FloginUser.Subject != "-1")
                {
                    strWhere += " and rtr.Subject='" + FloginUser.Subject + "'";
                }
                strWhere += " and rs.ResourceToResourceFolder_Id in(select distinct (ResourceToResourceFolder_Id) from ResourceShare where ShareObjectId in (select distinct(SchoolId) from dbo.VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "' and SchoolId<>''))";
                strSqlCount = @"select count(*) from(select rtr.ResourceFolder_Id,rtr.ResourceToResourceFolder_Id,rtr.Subject,rtr.Resource_Type,
rtr.Resource_Name,rtr.Resource_Url,rs.CreateTime,ISNULL(fu.TrueName,fu.UserName) TeacherName,r.Resource_ContentLength
,rtr.File_Suffix,rtr.Book_ID,rtr.File_Name from dbo.ResourceShare rs
left join ResourceToResourceFolder rtr on rtr.ResourceToResourceFolder_Id=rs.ResourceToResourceFolder_Id
left join ResourceFolder rf on rf.ResourceFolder_Id=rtr.ResourceFolder_Id
left join Resource  r on r.Resource_Id=rtr.Resource_Id
left join F_User fu on fu.UserId=rs.CreateUserId where " + strWhere + " ) a ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.Resource_Name) row,A.* from (select rtr.ResourceFolder_Id,rtr.ResourceToResourceFolder_Id,rtr.Subject,rtr.Resource_Type,
rtr.Resource_Name,rtr.Resource_Url,rs.CreateTime,ISNULL(fu.TrueName,fu.UserName) TeacherName,r.Resource_ContentLength
,rtr.File_Suffix,rtr.Book_ID,rtr.File_Name from dbo.ResourceShare rs
left join ResourceToResourceFolder rtr on rtr.ResourceToResourceFolder_Id=rs.ResourceToResourceFolder_Id
left join ResourceFolder rf on rf.ResourceFolder_Id=rtr.ResourceFolder_Id
left join Resource  r on r.Resource_Id=rtr.Resource_Id
left join F_User fu on fu.UserId=rs.CreateUserId  where "
                    + strWhere + " ) A ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    Rc.Cloud.Web.AuthApi.index.BookAttrModel bkAttrModel = GetBookAttrValue(dtRes.Rows[i]["Book_ID"].ToString());
                    inum++;
                    string docName = dtRes.Rows[i]["File_Name"].ToString();
                    //docName = pfunction.GetDocFileName(docName);
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
                        // docTypeAll = dtRes.Rows[i]["File_Suffix"].ToString(),
                        docSize = pfunction.ConvertDocSizeUnit(dtRes.Rows[i]["Resource_ContentLength"].ToString()),
                        docUrl = pfunction.GetResourceHost("TeachingPlanViewWebSiteUrl") + resUrl,
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        IsDownload = bkAttrModel.IsSave,
                        TeacherName = dtRes.Rows[i]["TeacherName"].ToString()
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