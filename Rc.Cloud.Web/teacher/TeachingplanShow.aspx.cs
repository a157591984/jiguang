using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.teacher
{
    public partial class TeachingplanShow : Rc.Cloud.Web.Common.FInitData
    {
        public string ResourceFolder_ID = string.Empty;
        public string Type = string.Empty;
        public string BookPrice = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        public bool Vbtn = false;
        //public int level = 0;
        public int levels = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Type = Request.QueryString["Type"];
            ResourceFolder_ID = Request.QueryString["ResourceFolder_ID"];
            if (!IsPostBack)
            {
                LaodData();
            }

        }
        protected void LaodData()
        {
            BLL_UserBuyResources bllubr = new BLL_UserBuyResources();
            DataTable dtubr = bllubr.GetList(" Book_id='" + ResourceFolder_ID + "' and UserId='" + FloginUser.UserId + "'").Tables[0];
            if (dtubr.Rows.Count > 0)
            {
                Vbtn = true;
                btnBuy.Visible = false;
            }
            BLL_Bookshelves bll = new BLL_Bookshelves();
            DataTable dt = bll.GetList("ResourceFolder_Id='" + ResourceFolder_ID + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                #region 处理图片显示宽高
                int imgHeight = 0;
                int imgWidth = 0;
                string imgFilePath = Server.MapPath(dt.Rows[0]["BookImg_Url"].ToString());
                if (System.IO.File.Exists(imgFilePath))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(imgFilePath);
                    imgHeight = img.Height;
                    imgWidth = img.Width;
                    if (imgHeight / 300.0 > imgWidth / 220.0)
                    {
                        imgWidth = 0;
                        if (imgHeight > 300) imgHeight = 300;

                    }
                    else
                    {
                        imgHeight = 0;
                        if (imgWidth > 220) imgWidth = 220;
                    }
                    img.Dispose();
                }
                #endregion
                //ltlImg.Text = string.Format("<img src='{0}' {1} {2} onerror=\"this.src='../images/re_nopic.jpg'\" width='186' height='239' />"
                //    , dt.Rows[0]["BookImg_Url"].ToString()
                //    , imgHeight > 0 ? string.Format("height='{0}'", imgHeight) : ""
                //    , imgWidth > 0 ? string.Format("width='{0}'", imgWidth) : "");
                ltlImg.Text = string.Format("<img src='{0}' onerror=\"this.src='../images/re_nopic.jpg'\" width='175' height='240' />"
                    , dt.Rows[0]["BookImg_Url"].ToString());
                ltlMaskImg.Text = string.Format("<img class='blur-item-hook' src='{0}' />"
                    , dt.Rows[0]["BookImg_Url"].ToString());
                ltlBookName.Text = dt.Rows[0]["Book_Name"].ToString();
                btnBuy.Text = "购买 ￥" + dt.Rows[0]["BookPrice"].ToString();
                ltlBookBif.Text = dt.Rows[0]["BookBrief"].ToString();

            }
            string strsql = @"select ResourceFolder_Id,ResourceFolder_Name,Resource_Type,Resource_Class,File_Suffix,ResourceFolder_Level,ResourceFolder_ParentId from VW_ResourceAndResourceFolder where Book_ID='" + ResourceFolder_ID + "'  order by ResourceFolder_Order";

            DataTable dtre = Rc.Common.DBUtility.DbHelperSQL.Query(strsql).Tables[0];
            if (dtre.Rows.Count > 0)
            {

                DataView dvw = new DataView();
                dvw.Table = dtre;
                litTree.Text = InitNavigationTree("0", dtre.Rows[0]["Resource_Class"].ToString(), dvw, 1).ToString();
            }
            string sql = @" select c.comment_id,c.comment_content,create_time,comment_evaluate,ISNULL(u.TrueName,u.UserName)as Commenter,o.Book_Id  from userorder_comment c
 left join F_User u on u.UserId=c.user_id
 left join UserOrder o on o.UserOrder_No=c.order_num where  o.Book_Id='" + ResourceFolder_ID + "'";
            DataTable dtC = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
            rptcourse_comment.DataSource = dtC;
            rptcourse_comment.DataBind();
        }

        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        /// <param name="TreeIDCurrent"></param>
        /// <param name="TreeLevelCurrent"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected StringBuilder InitNavigationTree(string TreeIDCurrent, string strResource_Class, DataView dvw, int level)
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

                        //int.TryParse(drv["ResourceFolder_Level"].ToString(), out  level);
                        if (strResource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                        {
                            strHtml.AppendFormat(" <ul>", level);
                        }
                        else
                        {
                            strHtml.AppendFormat(" <ul>", level);
                        }
                        //level++;
                    }
                    levels++;
                    strHtml.Append("<li>");
                    if (Vbtn == true)
                    {
                        if (drv["Resource_Type"].ToString() == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                        {//teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id=ecb24ae7-903c-4df0-aa99-e039731673ed
                            strHtml.AppendFormat("<div class='mtree_link mtree-link-hook' data-url='{0}'><div class='mtree_indent mtree-indent-hook'></div><div class='mtree_btn mtree-btn-hook'></div><div class='mtree_name mtree-name-hook'>{1}</div>{2}</div>"
                                , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : (FloginUser.UserIdentity == "T" ? ("../teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id=" + drv["ResourceFolder_Id"].ToString()) : ("../student/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id=" + drv["ResourceFolder_Id"].ToString()))
                                , drv["ResourceFolder_Name"].ToString().ReplaceForFilter()
                                , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : "<span class='btn btn-success btn-sm view-hook'>预览</span>"
                                                   );
                        }
                        else
                        {
                            strHtml.AppendFormat("<div class='mtree_link mtree-link-hook' data-url='{0}'><div class='mtree_indent mtree-indent-hook'></div><div class='mtree_btn mtree-btn-hook'></div><div class='mtree_name mtree-name-hook'>{1}</div>{2}</div>"
                                , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "javascript:;" : "../teacher/FileView.aspx?ResourceToResourceFolder_Id=" + drv["ResourceFolder_Id"].ToString() + "&Resource_Class=" + drv["Resource_Class"].ToString()
                            , drv["ResourceFolder_Name"].ToString().ReplaceForFilter()
                                , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : "<span class='btn btn-success btn-sm view-hook'>预览</span>"
                                               );
                        }
                    }
                    else
                    {
                        if (drv["Resource_Type"].ToString() == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                        {//teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id=ecb24ae7-903c-4df0-aa99-e039731673ed
                            strHtml.AppendFormat("<div class='mtree_link mtree-link-hool' data-url='{0}'><div class='mtree_indent mtree-indent-hook'></div><div class='mtree_btn mtree-btn-hook'></div><div class='mtree_name mtree-name-hook'>{1}</div>{2}</a>"
                                , levels < 6 ? (string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : (FloginUser.UserIdentity == "T" ? ("../teacher/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id=" + drv["ResourceFolder_Id"].ToString()) : ("../student/HomeworkPreviewT.aspx?ResourceToResourceFolder_Id=" + drv["ResourceFolder_Id"].ToString()))) : "javascript:;"
                                , drv["ResourceFolder_Name"].ToString().ReplaceForFilter()
                                , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : (levels < 6 ? ("<span class='btn btn-success btn-sm view-hook'>预览</span>") : "<span class='btn btn-default btn-sm'><i class='material-icons'>&#xE899;</i>&nbsp;购买</span>")
                                                   );
                        }
                        else
                        {
                            strHtml.AppendFormat("<div class='mtree_link mtree-link-hook' data-url='{0}'><div class='mtree_indent mtree-indent-hook'></div><div class='mtree_btn mtree-btn-hook'></div><div class='mtree_name mtree-name-hook'>{1}</div>{2}</a>"
                                , levels < 6 ? (string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : "../teacher/FileView.aspx?ResourceToResourceFolder_Id=" + drv["ResourceFolder_Id"].ToString() + "&Resource_Class=" + drv["Resource_Class"].ToString()) : "javascript:;"
                            , drv["ResourceFolder_Name"].ToString().ReplaceForFilter()
                                //, string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "data-disabled = 'true'" : ""
                                , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : (levels < 6 ? ("<span class='btn btn-success btn-sm view-hook'>预览</span>") : "<span class='btn btn-default btn-sm'><i class='material-icons'>&#xE899;</i>&nbsp;购买</span>")
                                               );
                        }
                    }

                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), strResource_Class, dvw, level + 1);
                    strHtml.Append("</li>");
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</ul>");
                    }
                }
            }
            return strHtml;
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            try
            {
                BLL_Bookshelves bll = new BLL_Bookshelves();
                DataTable dt = bll.GetList("ResourceFolder_Id='" + ResourceFolder_ID + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    BookPrice = dt.Rows[0]["BookPrice"].ToString();
                }
                if (BookPrice == "0.00")
                {
                    #region 用户购买资源表
                    Model_UserBuyResources buyModel = new Model_UserBuyResources();
                    buyModel.UserBuyResources_ID = Guid.NewGuid().ToString();
                    buyModel.UserId = FloginUser.UserId;
                    buyModel.Book_id = ResourceFolder_ID;
                    buyModel.BookPrice = 0;
                    buyModel.BuyType = UserOrder_PaytoolEnum.FREE.ToString();
                    buyModel.CreateTime = DateTime.Now;
                    buyModel.CreateUser = FloginUser.UserId;
                    if (new BLL_UserBuyResources().Add(buyModel))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(Request.Url.ToString(), string.Format("免费资源。购买资源成功，购买人：{0}，资源标识：{1}"
                            , FloginUser.UserId, ResourceFolder_ID));
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('购买成功',{icon:1,time:1000},function(){window.location.href=window.location;});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('购买失败',{icon:2,time:2000});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    if (Type == "1")
                    {
                        Response.Redirect("/student/Payment.aspx?orderType=2&rid=" + ResourceFolder_ID);
                    }
                    else
                    {
                        Response.Redirect("/teacher/Payment.aspx?orderType=1&rid=" + ResourceFolder_ID);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}