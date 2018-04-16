namespace Rc.Cloud.BLL
{
    using Rc.Cloud.Model;
    using Rc.Common.StrUtility;
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Web.UI;

    public class BLL_loginPath
    {
        public static string GetPageHtmlCode(string accessUrl, int timeOut = 0x2710)
        {
            string str = string.Empty;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(accessUrl);
            request.Timeout = timeOut;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                str = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
            return str;
        }

        public void LoginRedirect(Page page, string id, string url)
        {
            BLL_DatabaseCopyLibrary library = new BLL_DatabaseCopyLibrary();
            Model_DatabaseCopyLibrary model = new Model_DatabaseCopyLibrary();
            model = library.GetModel(id);
            if (model != null)
            {
                string accessUrl = model.DataBaseAddress.TrimEnd(new char[] { '/' }) + "/loginPath.aspx?p=" + (model.LoginName + "[end]" +   Rc.Common.StrUtility.EncryptUtility.MyEncryptString( model.LoginPassword) + "[end]" + url).Encrypt();
                try
                {
                    GetPageHtmlCode(accessUrl, 0x2710);
                }
                catch
                {
                    page.Response.Write(string.Format("<script>alert('{0}');window.close();</script>", "获取页面异常，请检查副本库的地址或用户密码是否配置正确。"));
                    return;
                }
                page.Response.Redirect(accessUrl);
            }
            else
            {
                page.Response.Write("<script>alert('操作失败，请重新再试，或联系管理人员！');window.close();</script>");
                page.Response.End();
            }
        }
    }
}

