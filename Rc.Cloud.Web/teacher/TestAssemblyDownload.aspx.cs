using Rc.Cloud.Web.Common;
using Rc.Common.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.teacher
{
    public partial class TestAssemblyDownload : System.Web.UI.Page
    {
        string type = string.Empty; // 类型 1标题
        string title = string.Empty;    // 标题内容（Encrypt）
        string remoteUrl = string.Empty; // 远程下载url（Encrypt）
        string savePath = string.Empty; // 本地存储路径（Encrypt）
        string fileName = string.Empty; // 原文件名称（Encrypt）
        string saveName = string.Empty; // 新文件名称（Encrypt）

        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.QueryString["type"];
            title = Request.QueryString["title"];
            remoteUrl = Request.QueryString["remoteUrl"];
            savePath = Request.QueryString["savePath"];
            fileName = Request.QueryString["fileName"];
            saveName = Request.QueryString["saveName"];

            if (type == "1") //标题
            {
                if (!string.IsNullOrEmpty(title)) pfunction.WriteToFile(Server.MapPath(DESEncrypt.Decrypt(savePath)), DESEncrypt.Decrypt(title), true);
            }
            else
            {
                pfunction.DownLoadFileByWebClient(DESEncrypt.Decrypt(remoteUrl)
                    , Server.MapPath(DESEncrypt.Decrypt(savePath))
                    , DESEncrypt.Decrypt(fileName)
                    , DESEncrypt.Decrypt(saveName));
                pfunction.WriteToFile(Server.MapPath("111.txt")
                    , DESEncrypt.Decrypt(remoteUrl) + "^^"
                    + DESEncrypt.Decrypt(savePath) + "^^"
                    + DESEncrypt.Decrypt(fileName) + "^^"
                    + DESEncrypt.Decrypt(saveName) + "^^", true);
            }
        }
    }
}