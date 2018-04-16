using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Catalog : Rc.Cloud.Web.Common.InitPage
    {
        StringBuilder strHtml = new StringBuilder();
        protected string strResource_Type = string.Empty;
        protected string strResource_Class = string.Empty;
        protected string t = string.Empty;
        protected string s = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            Module_Id = "10101000";
            //
            if (!String.IsNullOrEmpty(Request["t"]))
            {
                t = Request["t"].ToString();
                if (Request["t"].ToString() == "1")
                {
                    Module_Id = "10101000";
                    strResource_Type = Rc.Common.Config.Resource_TypeConst.ScienceWord类型文件;
                }
                else if (Request["t"].ToString() == "2")
                {
                    Module_Id = "10102000";
                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                }
                else if (Request["t"].ToString() == "3")
                {
                    Module_Id = "10103000";
                    strResource_Type = Rc.Common.Config.Resource_TypeConst.testPaper类型文件;
                }
            }
            if (!String.IsNullOrEmpty(Request["s"]))
            {
                s = Request["s"].ToString();
                if (Request["s"].ToString() == "1")
                {
                    strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                }
                else if (Request["s"].ToString() == "2")
                {
                    strResource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                }
            }

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID");
                //教案类型
                strWhere = " D_Type='5' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlLessonPlan_Type, dt, "D_Name", "Common_Dict_ID");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID");


                litTree.Text = GetResourceAttribute().ToString();
            }
            checkExistUserCatalog();
        }
        /// <summary>
        /// 验证用户是否已创建目录
        /// </summary>
        private void checkExistUserCatalog()
        {
            string strWhere = string.Empty;
            strWhere += " 1=1 ";
            if (ddlResource_Version.SelectedValue != "-1" &&
                ddlLessonPlan_Type.SelectedValue != "-1" &&
                ddlGradeTerm.SelectedValue != "-1" &&
                ddlSubject.SelectedValue != "-1")
            {
                strWhere += string.Format(" and Resource_Version='{0}' and LessonPlan_Type='{1}' and GradeTerm='{2}' and Subject='{3}' "
                    , ddlResource_Version.SelectedValue.Filter()
                    , ddlLessonPlan_Type.SelectedValue.Filter()
                    , ddlGradeTerm.SelectedValue.Filter()
                    , ddlSubject.SelectedValue.Filter()
                    );
            }
            else
            {
                strWhere = " 1!=1";

            }
            strWhere += string.Format(" and Resource_Class='{0}' and Resource_Type='{1}'", strResource_Class, strResource_Type);
            string strSql = string.Empty;
            strSql = string.Format("select count(1) from ResourceFolder where {0}", strWhere);
            if (Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString()) == 0)
            {
                btnNewCreate.Style.Add("display", "block");
                btnUpdate.Style.Add("display", "none");
                btnSave.Style.Add("display", "none");
                btnSub.Style.Add("display", "none");
                btnDelete.Style.Add("display", "none");
            }
            else
            {
                // litTree.Text = GetTreeHtml();
                btnNewCreate.Style.Add("display", "none");
                btnUpdate.Style.Add("display", "block");
                btnSave.Style.Add("display", "block");
                btnSub.Style.Add("display", "block");
                btnDelete.Style.Add("display", "block");
            }
        }
        /// <summary>
        /// 资源属性
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetResourceAttribute()
        {
            StringBuilder sb = new StringBuilder();
            string strWhere = string.Empty;
            strWhere = string.Empty;
            if (ddlResource_Version.SelectedValue != "-1" &&
                ddlLessonPlan_Type.SelectedValue != "-1" &&
                ddlGradeTerm.SelectedValue != "-1" &&
                ddlSubject.SelectedValue != "-1")
            {
                table_content.Visible = true;
                tableConfirm.Visible = false;
                strWhere += string.Format(" 1=1 and Resource_Version='{0}' and LessonPlan_Type='{1}' and GradeTerm='{2}' and Subject='{3}' "
                    , ddlResource_Version.SelectedValue.Filter()
                    , ddlLessonPlan_Type.SelectedValue.Filter()
                    , ddlGradeTerm.SelectedValue.Filter()
                    , ddlSubject.SelectedValue.Filter()
                    );
                strWhere += "  order by ResourceFolder_Name ";
                DataTable dt = new Rc.BLL.Resources.BLL_ResourceFolder().GetList(strWhere).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sb.Append(GetFolderHtml(dt));
                }
                else
                {
                    sb.Append("<div> 此目录位置下尚未维护目录，您可以在右方区域维护目录。</div>");
                }

                strHtml = new StringBuilder();
            }
            else
            {
                //如果条件不全不可维护目录。
                table_content.Visible = false;
                tableConfirm.Visible = true;
            }

            return sb;
        }
        /// <summary>
        /// 资源目录
        /// </summary>
        /// <param name="ResourceVersion_ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private StringBuilder GetFolderHtml(DataTable dt)
        {
            strHtml = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            DataView dvw = new DataView();
            dvw.Table = dt;
            sb.Append("<div data-type='tree'>");
            sb.Append(InitNavigationTree("0", dvw));
            sb.Append("</div>");
            return sb;
        }
        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        /// <param name="TreeIDCurrent"></param>
        /// <param name="TreeLevelCurrent"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected StringBuilder InitNavigationTree(string TreeIDCurrent, DataView dvw)
        {
            string strWhere = string.Empty;
            strWhere = " 1=1 ";

            if (TreeIDCurrent == "0")
            {

                dvw.RowFilter = string.Format("  {0} and ResourceFolder_ParentId ='0' ", strWhere);
                // dvw.Sort = "ResourceFolder_Name";
            }
            else
            {
                dvw.RowFilter = string.Format(" {0} and  ResourceFolder_ParentId = '{1}' ", strWhere, TreeIDCurrent);
                // dvw.Sort = "ResourceFolder_Name";
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
                        int.TryParse(drv["ResourceFolder_Level"].ToString(), out  level);

                        strHtml.AppendFormat(" <ul class='left_tree_list' data-level='{0}'>", level + 1);
                        strHtml.Append("<li>");
                    }
                    if (drv["ResourceFolder_isLast"].ToString() != "1")
                    {
                        liClass = "fa fa-minus-square-o plus";
                    }
                    else
                    {
                        liClass = "fa plus";
                    }
                    strHtml.Append(" <div>");
                    strHtml.AppendFormat(" <i class='{0}'></i>", liClass);
                    strHtml.AppendFormat(" <a href='##'  onclick=\"ShowSubDocument('{0}','{1}','{2}','{3}','{4}','{5}')\" >{2}</a>"
                        , drv["ResourceFolder_Id"].ToString()
                            , drv["ResourceFolder_ParentId"].ToString()
                            , drv["ResourceFolder_Name"].ToString()
                            , drv["ResourceFolder_Level"].ToString()
                            , drv["Resource_Version"].ToString()
                            , drv["ResourceFolder_isLast"].ToString());
                    //strHtml.AppendFormat(" <a href='##' >{0}</a>", drv["ResourceFolder_Name"].ToString());
                    strHtml.Append(" </div>");

                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), dvw);
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
        /// 根据ID得到级别
        /// </summary>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        private int GetTreeLevel(string ResourceFolder_Id)
        {
            int i = 0;

            string strSql = string.Empty;
            strSql = string.Format(@"declare @s char(36)
set @s='{0}'
;with f as
(
select * from ResourceFolder where ResourceFolder_Id=@s
union all
select a.* from ResourceFolder a ,f where a.ResourceFolder_Id=f.ResourceFolder_ParentId
)
select COUNT(*) from f ", ResourceFolder_Id);
            return int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString());
        }
        protected void btsSearch_Click(object sender, EventArgs e)
        {
            litTree.Text = GetResourceAttribute().ToString();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SaveResourceFolder(2);
            litTree.Text = GetResourceAttribute().ToString();
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            SaveResourceFolder(1);
            litTree.Text = GetResourceAttribute().ToString();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (checkCatalogExistCatalog(hidResourceFolder_Id.Value.Filter()))
            {
                OperationFailed("存在下级目录,不允许删除。");
                //if (checkCatalogIsLast(hidResourceFolder_Id.Value.Filter())) btnSub.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
            if (checkCatalogExistDocument(hidResourceFolder_Id.Value.Filter()))
            {
                OperationFailed("目录下存在文件,不允许删除。");
                //if (checkCatalogIsLast(hidResourceFolder_Id.Value.Filter())) btnSub.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [dbo].[ResourceFolder] where ResourceFolder_Id='" + hidResourceFolder_Id.Value.Filter() + "' ");


            if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString()) > 0)
            {

                OperationSuccess("删除目录成功。");
            }
            else
            {

                OperationFailed("删除目录失败。");
            }
            litTree.Text = GetResourceAttribute().ToString();
        }
        bool checkCatalogExistCatalog(string ResourceFolder_Id)
        {
           
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(string.Format("select count(*) from ResourceFolder where ResourceFolder_ParentId='{0}'", ResourceFolder_Id)).ToString();
            if (int.Parse(str) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        bool checkCatalogExistDocument(string ResourceFolder_Id)
        {
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(string.Format("select  count(*) from ResourceToResourceFolder where ResourceFolder_Id='{0}'", ResourceFolder_Id)).ToString();
            if (int.Parse(str) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveResourceFolder(1);
           
            litTree.Text = GetResourceAttribute().ToString();
            
        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            SaveResourceFolder(1);
            litTree.Text = GetResourceAttribute().ToString();
            checkExistUserCatalog();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InsertOrUpdate"></param>
        /// <returns></returns>
        private void SaveResourceFolder(int InsertOrUpdate)
        {
            string ResourceFolder_Id = Guid.NewGuid().ToString();
            string ResourceFolder_ParentId = this.hidResourceFolder_ParentId.Value.Filter();
            string ResourceFolder_Name = this.txtResourceFolder_Name.Text.Filter();

            int ResourceFolder_Level = GetTreeLevel(ResourceFolder_ParentId);

            string Resource_Type = strResource_Type.Filter();
            string Resource_Class = strResource_Class.Filter();
            string Resource_Version = ddlResource_Version.SelectedValue.Filter();
            //string ResourceFolder_Remark = this.txtResourceFolder_Remark.Text;
            //int ResourceFolder_Order = int.Parse(this.txtResourceFolder_Order.Text);
            string ResourceFolder_Owner = loginUser.SysUser_ID;
            string CreateFUser = loginUser.SysUser_ID;
            string LessonPlan_Type = ddlLessonPlan_Type.SelectedValue.Filter();
            string GradeTerm = ddlGradeTerm.SelectedValue.Filter();
            string Subject = ddlSubject.SelectedValue.Filter();
            DateTime CreateTime = DateTime.Now;
            string ResourceFolder_isLast = this.ddlResourceFolder_isLast.SelectedValue.Filter();
            //如果插入
            if (InsertOrUpdate == 1)
            {
                ResourceFolder_Id = Guid.NewGuid().ToString();
            }
            else
            {
                ResourceFolder_Id = hidResourceFolder_Id.Value.Filter();
            }

            Rc.Model.Resources.Model_ResourceFolder model = new Rc.Model.Resources.Model_ResourceFolder();
            model.ResourceFolder_Id = ResourceFolder_Id;
            model.ResourceFolder_ParentId = ResourceFolder_ParentId;
            model.ResourceFolder_Name = ResourceFolder_Name;
            model.ResourceFolder_Level = ResourceFolder_Level;
            model.Resource_Type = Resource_Type;
            model.Resource_Class = Resource_Class;
            model.Resource_Version = Resource_Version;
            //model.ResourceFolder_Remark = ResourceFolder_Remark;
            //model.ResourceFolder_Order = ResourceFolder_Order;
            model.LessonPlan_Type = LessonPlan_Type;
            model.GradeTerm = GradeTerm;
            model.Subject = Subject;
            model.ResourceFolder_Owner = ResourceFolder_Owner;
            model.CreateFUser = CreateFUser;
            model.CreateTime = CreateTime;
            model.ResourceFolder_isLast = ResourceFolder_isLast;

            Rc.BLL.Resources.BLL_ResourceFolder bll = new Rc.BLL.Resources.BLL_ResourceFolder();
            bool b = true;
            if (InsertOrUpdate == 1)
            {
                b = bll.Add(model);
            }
            else if (InsertOrUpdate == 2)
            {
                b = bll.Update(model);
            }

            if (b)
            {
                OperationSuccess("操作成功。");
            }
            else
            {
                OperationFailed("操作失败。");
            }
            // litTree.Text = GetTreeHtml();
        }


    }
}