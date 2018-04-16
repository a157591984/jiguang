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
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class BookshelvesImgEdit : Rc.Cloud.Web.Common.InitData
    {
        Model_Bookshelves model;
        BLL_Bookshelves bll = new BLL_Bookshelves();
        string rid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rid = Request["rid"].Filter();
            if (!IsPostBack)
            {
                isit_load();
            }
        }

        private void isit_load()
        {
            model = new Model_Bookshelves();
            model = bll.GetModel(rid);
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
            }
            else
            {
                Image1.Visible = false;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            model = bll.GetModel(rid);

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

            result = bll.Update(model);

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