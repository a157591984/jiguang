using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using System.Data;

namespace Rc.Cloud.Web.Common
{
    public class PageFuntion
    {

        /// <summary>
        /// 得到站点域名包括端口
        /// </summary>
        /// <returns></returns>
        public static string getHostPath()
        {
            string hostPath = string.Empty;
            string hostPathTemp = string.Empty;
            hostPath = HttpContext.Current.Request.Url.AbsoluteUri;
            hostPathTemp = hostPath.Replace("//", "~~");
            hostPathTemp = hostPathTemp.Remove(hostPathTemp.IndexOf("/"));
            hostPath = hostPathTemp.Replace("~~", "//");
            hostPath = hostPath + ConfigurationManager.AppSettings["VirtualPath"];
            return hostPath;
        }
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示的数据条数</param>
        /// <param name="rCount">数据的总条数</param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPageIndex(int PageIndex, int PageSize, int rCount, int pCount, string url)
        {
            StringBuilder sbHTML = new StringBuilder();
            if (rCount == 0) { return ""; }
            string first = String.Format(url, 1, PageSize);
            string back = String.Format(url, PageIndex - 1, PageSize);
            string next = String.Format(url, PageIndex + 1, PageSize);
            string last = String.Format(url, pCount, PageSize);
            sbHTML.Append("<div class='page'>");
            sbHTML.Append("<div class='pagination_info'>");
            sbHTML.AppendFormat("共{0}条数据"
                , rCount);
            sbHTML.Append("&nbsp;&nbsp;跳转至第&nbsp;&nbsp;");
            sbHTML.Append("<select id=\"select_pageindex\" onchange=\"PageIndexChange(this);\">");
            if (pCount > 100)
            {
                pCount = 100;
            }
            for (int i = 1; i <= pCount; i++)
            {
                sbHTML.AppendFormat("<option value='{0}' {1}>{2}</option>"
                    , string.Format(url, i, PageSize)
                    , (i == PageIndex) ? "selected=selected" : ""
                    , i);
            }
            sbHTML.Append("</select>&nbsp;&nbsp;页");
            sbHTML.Append("&nbsp;&nbsp;每页显示&nbsp;&nbsp;");
            sbHTML.Append("<select id=\"select_pagesize\" onchange=\"PageSizeChange(this);\"  style=\"font-size:13px;\">");
            int[] arrnum = { 5, 10, 15, 20, 36, 50, 75, 100, 500 };
            foreach (int n in arrnum)
            {
                sbHTML.AppendFormat("<option value='{0}' {1}>{2}</option>"
                    , string.Format(url, 1, n)
                    , (n == PageSize) ? "selected=selected" : ""
                    , n);
            }
            sbHTML.Append("</select>&nbsp;&nbsp;条");
            sbHTML.Append("</div>");
            sbHTML.Append("<ul class='pagination'>");
            if (PageIndex == 1)
            {
                sbHTML.Append("<li><a href='javascript:;'>首页</a></li>");
            }
            else
            {
                sbHTML.AppendFormat("<li><a href='{0}'>首页</a></li>"
                    , first);
            }
            if (PageIndex == 1)
            {
                sbHTML.Append("<li><a href='javascript:;'>上一页</a></li>");
            }
            else
            {
                sbHTML.AppendFormat("<li><a href='{0}'>上一页</a></li>"
                       , back);
            }
            sbHTML.AppendFormat("<li><a href='javascript:;'>{0}/{1}</a></li>"
                , PageIndex
                , pCount);
            if (PageIndex == pCount)
            {
                sbHTML.Append("<li><a href='javascript:;'>下一页</a></li>");
            }
            else
            {
                sbHTML.AppendFormat("<li><a href='{0}'>下一页</a></li>"
                       , next);
            }
            if (PageIndex == pCount)
            {
                sbHTML.Append("<li><a href='javascript:;'>末页</a></li>");
            }
            else
            {
                sbHTML.AppendFormat("<li><a href='{0}'>末页</a></li>"
                       , last);
            }
            sbHTML.Append("</ul>");
            sbHTML.Append("</div>");
            return sbHTML.ToString();
        }
    }
}