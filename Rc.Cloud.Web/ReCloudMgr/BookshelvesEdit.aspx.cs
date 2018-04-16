using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Text;
using System.IO;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class BookshelvesEdit : Rc.Cloud.Web.Common.InitData
    {
        protected string edit = "";//等于1时是修改，等0时是新增。
        Model_Bookshelves model;
        BLL_Bookshelves bll = new BLL_Bookshelves();
        protected void Page_Load(object sender, EventArgs e)
        {
            edit = Request.QueryString["edit"];
            if (!IsPostBack)
            {
                isit_load();
            }
        }

        private void isit_load()
        {
            hidBook_Name.Value = Request.QueryString["bookname"];
            hidResourceFolder_Id.Value = Request.QueryString["bookid"];
            model = new Model_Bookshelves();
            model = bll.GetModel(hidResourceFolder_Id.Value);
            if (model != null)
            {
                if (File.Exists(Server.MapPath(model.BookImg_Url)))
                {
                    Image1.ImageUrl = model.BookImg_Url;
                }
                else
                {
                    Image1.Visible = false;
                }
                txtBookPrice.Text = model.BookPrice.ToString();
                BookBrief.Value = model.BookBrief;
            }
            else
            {
                Image1.Visible = false;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            model = bll.GetModel(hidResourceFolder_Id.Value);
            bool isUpate = true;
            if (model == null)
            {
                isUpate = false;
                model = new Model_Bookshelves();
                model.CreateTime = DateTime.Now;
                model.CreateUser = loginUser.SysUser_ID;
                model.ResourceFolder_Id = hidResourceFolder_Id.Value;
            }
            string savePath = Server.MapPath("~/Upload/BookCover/");//指定上传文件在服务器上的保存路径
            if (fpBookImg_Url.HasFile)
            {

                //检查服务器上是否存在这个物理路径，如果不存在则创建
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                string strFileName = Guid.NewGuid().ToString();
                string strExt = Path.GetExtension(fpBookImg_Url.PostedFile.FileName);
                savePath = savePath + "\\" + strFileName + strExt;
                fpBookImg_Url.SaveAs(savePath);
                model.BookImg_Url = "/Upload/BookCover/" + strFileName + strExt;
            }
            model.Book_Name = hidBook_Name.Value;
            model.BookPrice = Convert.ToDecimal(txtBookPrice.Text);
            model.BookShelvesState = "1";
            model.PutUpTime = DateTime.Now;
            model.BookBrief = BookBrief.Value;
            if (isUpate)
            {
                result = bll.Update(model);
            }
            else
            {
                result = bll.Add(model);
            }

            Model_BookshelvesLog modelLog = new Model_BookshelvesLog();
            modelLog.BookshelvesLog_Id = Guid.NewGuid().ToString();
            modelLog.BookId = model.ResourceFolder_Id;
            modelLog.LogTypeEnum = "1";
            modelLog.CreateUser = loginUser.SysUser_ID;
            modelLog.CreateTime = DateTime.Now;
            new BLL_BookshelvesLog().Add(modelLog);

            ShowMsg(result == true ? 1 : 0, result == true ? "保存成功。" : "保存失败。");
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="resutl">成功/失败</param>
        /// <param name="url">成功后跳转的连接</param>
        /// <param name="errorMsg">错误后提示的内容</param>
        private void ShowMsg(int resutl, string errorMsg)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");

            if (resutl == 1)
                scriptStr.AppendFormat("parent.Handel('1','{0}')", errorMsg);
            else
                scriptStr.AppendFormat("parent.Handel('{0}','{1}')", "2", errorMsg);

            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());

        }
    }
}