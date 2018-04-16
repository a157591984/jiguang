using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Ajax
{
    public partial class UserControlAjax : System.Web.UI.Page
    {
        string strPageName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.QueryString["key"];
            StringBuilder strStr = new StringBuilder();
            //string HospitalID = string.Empty;
            string userControlID = string.Empty;
            string D_Id = string.Empty;
            string D_Name = string.Empty;
            string D_Type = string.Empty;
            string strTemp = string.Empty;
            int PageIndex = 0;
            int PageSize = 0;
            int rCount = 0;
            int pCount = 0;
            string IsPY = "0";
            string strJson = string.Empty;
            string strIds = string.Empty;
            string strWhere = string.Empty;
            string DocColJson = string.Empty;
            strPageName = Request["strPageName"];

            if (string.IsNullOrEmpty(key))
            {
                key = Request["key"];
            }
            switch (key)
            {

                #region 获取通用视图字典 NOW
                #region 获取已选取通用视图字典项 NOW
                case "CommonCtrlMultiple":

                    strJson = "2";
                    try
                    {
                        if (!String.IsNullOrEmpty(Request["Ids"]))
                        {
                            strIds = Request["Ids"].ToString();
                            for (int i = 0; i < strIds.Split(',').Length; i++)
                            {
                                strTemp += "'" + strIds.Split(',')[i] + "',";
                            }
                            strTemp = strTemp.TrimEnd(',');
                            strWhere = String.Format(" Common_Dict_ID in({0})", strTemp);
                            if (!String.IsNullOrWhiteSpace(Request["D_Type"]))
                            {
                                strWhere += String.Format(" AND D_Type ='{0}'", Request["D_Type"].ToString());
                            }
                            strStr = new StringBuilder();
                            var varData = new BLL_PublicClass().GetCommon_Dict_List(strWhere, Request["D_Type"].Trim());
                            if (varData.Count == 0)
                            {
                                strJson = "2";
                            }
                            else
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
                                strJson = objJson.Serialize(varData);
                            }
                        }
                    }
                    catch 
                    {
                        //strJson = ex.Message.ToString();
                        strJson = "0";
                    }
                    Response.Write(strJson);
                    Response.End();
                    break;
                #endregion
                #region 分页面获取通用字典项
                case "CommonCtrlMultiplePaged":
                    strJson = string.Empty;
                    string strCondition = string.Empty;
                    strWhere = string.Empty;
                    if (!String.IsNullOrEmpty(Request["userControlID"]))
                    {
                        userControlID = Request["userControlID"].ToString();
                    }
                    if (!String.IsNullOrEmpty(Request["PageIndex"]))
                    {
                        int.TryParse(Request["PageIndex"].ToString(), out PageIndex);
                    }
                    if (!String.IsNullOrEmpty(Request["PageSize"]))
                    {
                        int.TryParse(Request["PageSize"].ToString(), out PageSize);
                    }
                    if (!String.IsNullOrEmpty(Request["D_Name"]))
                    {
                        D_Name = Request["D_Name"].ToString();
                    }
                    if (!String.IsNullOrEmpty(Request["D_Type"]))
                    {
                        D_Type = Request["D_Type"].ToString();
                    }
                    if (!String.IsNullOrEmpty(Request["Condition"]))
                    {
                        //字段名1,字段名2|值1,值2
                        strCondition = Request["Condition"].ToString();
                        strTemp = string.Empty;
                       
                        strTemp = clsUtility.Decrypt(strCondition);
                        string strT1 = strTemp.Split('|')[0];
                        string strT2 = strTemp.Split('|')[1];

                        if (strT1.Split(',').Count()==strT2.Split(',').Count())
                        {
                            for (int i = 0; i < strT1.Split(',').Count(); i++)
                            {
                                strWhere += " and " + strT1.Split(',')[i] + " = '" + strT2.Split(',')[i]+"'";
                            }  
                        }
                        else
                        {
                            strWhere = string.Empty;
                        }

                    }
                    #region 分页得到通用字典项
                    try
                    {
                        strStr = new StringBuilder();
                        var varData = new BLL_PublicClass().GetCommonMultiselect(IsPY, D_Name, D_Type, strWhere, PageIndex, PageSize, out rCount, out pCount);
                        if (varData.Count == 0)
                        {
                            strJson = "2";
                        }
                        else
                        {
                            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
                            strJson = objJson.Serialize(varData);
                            strJson = strJson + "~" + GetHtmlPage(rCount, PageIndex, PageSize);
                        }
                    }
                    catch
                    {
                        //strJson = ex.Message.ToString();
                        strJson = "0";
                    }
                    Response.Write(strJson.ToString());
                    Response.End();
                    break;
                    #endregion
                #endregion
                #endregion

            }
        }
        
        #region 公用方法
        #region 分页

        private string GetHtmlPage(int rCount, int PageIndex, int PageSize)
        {

            int PageCount = 0;
            int PageNoCount = 4;

            int PageNoCount_temp = 0;
            bool isListA = false;
            //PageNoCount_temp=

            if (Math.Ceiling(double.Parse(PageIndex.ToString()) / double.Parse(PageNoCount.ToString())) == Math.Ceiling(double.Parse(rCount.ToString()) / double.Parse((PageSize * PageNoCount).ToString())))
            {
                PageNoCount_temp = int.Parse(Math.Ceiling(double.Parse((rCount % (PageSize * PageNoCount)).ToString()) / double.Parse(PageSize.ToString())).ToString());
                isListA = false;
            }
            else
            {
                isListA = true;
                PageNoCount_temp = PageNoCount;
            }
            if (rCount == PageSize * PageNoCount)
            {
                PageNoCount_temp = PageNoCount;
            }

            //PageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
            if (PageIndex == 0) { PageIndex = 1; }
            //PageSize = Convert.ToInt32(Request.QueryString["PageSize"]);
            if (PageSize == 0) { PageSize = 10; }
            PageCount = rCount / PageSize;
            if (rCount % PageSize != 0)
            {
                PageCount += 1;
            }
            StringBuilder stbHTML = new StringBuilder();
            #region 分页

            if (PageCount > 1)
            {
                stbHTML.Append("<div class='div_page' >");
                stbHTML.Append("<ul class='ul_page'>");
                int PageIndexChange = 0;
                if (Math.Ceiling(double.Parse(PageIndex.ToString()) / double.Parse(PageNoCount.ToString())) > 1)
                {
                    PageIndexChange = (PageIndex / PageNoCount) * PageNoCount;
                    stbHTML.Append("<li><a href=\"##\" onclick=\"UcPageIndexChange" + strPageName + "(" + (PageIndexChange) + ", " + PageSize + ")\">...</a></li>");
                }

                for (int i = 1; i <= PageNoCount_temp; i++)
                {
                    PageIndexChange = (((PageIndex - 1) / PageNoCount) * PageNoCount + i);
                    if (PageIndexChange == PageIndex)
                    {
                        stbHTML.Append("<li class=\"li_page_selected\">" + PageIndexChange + "</li>");
                    }
                    else
                    {
                        stbHTML.Append("<li><a href=\"##\" onclick=\"UcPageIndexChange" + strPageName + "(" + PageIndexChange + ", " + PageSize + ")\">" + PageIndexChange + "</a></li>");
                    }
                }
                if (isListA)
                {
                    stbHTML.Append("<li><a href=\"##\" onclick=\"UcPageIndexChange" + strPageName + "(" + (PageIndexChange + 1) + ", " + PageSize + ")\">...</a></li>");
                }

                stbHTML.Append("</ul>");
                stbHTML.Append("</div>");
            }
            if (PageCount <= 1)
            {
                return "";
            }
            else
            {
                return stbHTML.ToString();
            }

            #endregion
        #endregion
        }
        #endregion
    }
}