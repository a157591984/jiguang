using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using Rc.Cloud.BLL;


namespace PHHC.SysManagement.Web.Ajax
{
    public partial class AutoSelectAjax : System.Web.UI.Page
    {
        string strPageName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.QueryString["key"];
            StringBuilder strStr = new StringBuilder();

            string D_Id = string.Empty;
            string D_Name = string.Empty;
            string D_Type = string.Empty;
            string strIsClick = string.Empty;
            string strTemp = string.Empty;
            int PageIndex = 0;
            int PageSize = 0;
            int rCount = 0;
            int pCount = 0;
            string strJson = string.Empty;
            string strIds = string.Empty;
            string strWhere = string.Empty;
            string DocColJson = string.Empty;
            strPageName = Request["strPageName"];
            string IsPY = "1";
            if (string.IsNullOrEmpty(key))
            {
                key = Request["key"];
            }
            switch (key)
            {
                #region 分页面获取通用字典项
                case "AjaxAutoCompletePaged":
                    strJson = string.Empty;
                    string strCondition = string.Empty;

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
                    if (!String.IsNullOrEmpty(Request["IsPY"]))
                    {
                        IsPY = Request["IsPY"].ToString();
                    }
                    if (!String.IsNullOrEmpty(Request["isClick"]))
                    {
                        strIsClick = Request["isClick"].ToString();
                    }

                    if (!string.IsNullOrWhiteSpace(Request["ConditionIn"]))
                    {
                        string[] arr = Request["ConditionIn"].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        strWhere += " and " + arr[0] + " in(" + arr[1] + ")";
                    }
                    if (!string.IsNullOrWhiteSpace(Request["ConditionNotIn"]))
                    {
                        string[] arr = Request["ConditionNotIn"].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        strWhere += " and " + arr[0] + " not in(" + arr[1] + ")";
                    }
                    //TextColumn: TextColumn         , ValueColumn: ValueColumn
                    //查询字段
                    string Like = string.IsNullOrWhiteSpace(Request["Like"]) ? "0" : Request["Like"].ToString().Trim();
                    string IsJP = string.IsNullOrWhiteSpace(Request["IsJP"]) ? "1" : Request["IsJP"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(Request["WhereColumn"]))
                    {
                        string[] arr = Request["WhereColumn"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (D_Name != "")
                        {
                            strWhere += " and ( 1<>1 ";
                            for (int i = 0; i < arr.Length; i++)
                            {

                                strWhere += " or " + arr[i] + " like '" + getLikeStr(D_Name, Like) + "'";

                                if (IsJP == "1")
                                {
                                    strWhere += " or dbo.f_GetPy(" + arr[i] + ") like '" + getLikeStr(D_Name, Like) + "'";
                                }
                            }
                            strWhere += ") ";
                        }
                    }
                    else
                    {
                        strWhere += " and (";
                        strWhere += " D_Name like '" + getLikeStr(D_Name, Like) + "'";
                        if (IsJP == "1")
                        {
                            strWhere += " or dbo.f_GetPy(d_name) like '" + getLikeStr(D_Name, Like) + "'";
                        }
                        strWhere += ")";
                    }


                    //显示字段： 排序字段， 显示文本， 隐藏值。
                    string select = string.Empty;
                    //排序字段
                    if (!string.IsNullOrWhiteSpace(Request["Order"]))
                    {
                        select = " row_number() over(order by " + Request["Order"] + ") AS r_n,";
                    }
                    else
                    {
                        select = " row_number() over(order by D_Order,D_Name) AS r_n,";
                    }
                    //显示文本
                    if (!string.IsNullOrWhiteSpace(Request["TextColumn"]))
                    {
                        select += "(" + getSelectRegex(Request["TextColumn"]) + ") as D_name,";
                    }
                    else
                    {
                        select += "D_name,";
                    }
                    //隐藏值
                    if (!string.IsNullOrWhiteSpace(Request["ValueColumn"]))
                    {
                        select += "(" + getSelectRegex(Request["ValueColumn"]) + ") as Common_Dict_ID,";
                    }
                    else
                    {
                        select += "Common_Dict_ID, ";
                    }
                    //其他显示列
                    select += "D_Value,D_Code,D_Level,D_Order,D_Type,D_Remark,D_CreateUser,D_CreateTime,D_ModifyUser,D_ModifyTime ";

                    #region 分页得到通用字典项
                    try
                    {
                        strStr = new StringBuilder();
                        var varData = new BLL_PublicClass().GetIntelligentAssociationList(D_Type, select, strWhere, PageIndex, PageSize, out rCount, out pCount);
                        if (varData.Count == 0)
                        {
                            strJson = "2";
                        }
                        else
                        {
                            System.Web.Script.Serialization.JavaScriptSerializer objJson = new System.Web.Script.Serialization.JavaScriptSerializer();
                            strJson = objJson.Serialize(varData);
                            strJson = strJson + "~" + GetHtmlPage(rCount, PageIndex, PageSize, pCount, strIsClick);
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

            }
        }

        //将正则表达式的SELECT语句，转换成SQL语句
        private string getSelectRegex(string str)
        {
            MatchCollection coll = Regex.Matches(str, @"(?<text>.*?)(?<column>\{\w+?\})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            string select = "''";

            for (int i = 0; i < coll.Count; i++)
            {
                select += "+'" + coll[i].Groups["text"].Value + "'";
                select += '+' + coll[i].Groups["column"].Value.Trim().TrimStart('{').TrimEnd('}');
            }

            return select;
        }

        //根据like标识，对数据进行%添加。
        private string getLikeStr(string str, string like)
        {
            string likeStr = string.Empty;
            switch (like.Trim())
            {
                case "0":
                    likeStr = "%" + str + "%";
                    break;
                case "1":
                    likeStr = "%" + str;
                    break;
                case "2":
                    likeStr = str + "%";
                    break;
                default:
                    likeStr = str;
                    break;
            }
            return likeStr;
        }


        #region 公用方法
        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="rCount">记录总数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页显示数据条数</param>
        /// <param name="PageSize">总页数</param>
        /// <returns>分页的HTML</returns>
        private string GetHtmlPage(int rCount, int PageIndex, int PageSize, int pCount, string strIsClick)
        {
            int leftLinkPage = 0;//前一页的页码
            int rightLinkPage = 0;//后一页的页码
            if (PageIndex <= 1)
            {
                leftLinkPage = 1;
            }
            else
            {
                leftLinkPage = PageIndex - 1;
            }
            if (PageIndex >= pCount)
            {
                rightLinkPage = PageIndex;
            }
            else
            {
                rightLinkPage = PageIndex + 1;
            }

            StringBuilder stbHTML = new StringBuilder();
            #region 分页

            if (pCount > 1)
            {
                stbHTML.Append("  <div class=\"clearDiv\"></div><div class=\"flipAutoComplete\">");
                stbHTML.Append("<span style=\"float: left; \"");

                if (PageIndex <= 1)
                {
                    stbHTML.Append(" class=\"spanNoLinkAutoComplete\"");
                }
                else
                {
                    stbHTML.Append(" class=\"spanLinkAutoComplete\"");
                    stbHTML.AppendFormat(" onclick=\"AutoCompleteGetDataListAll({0}, {1},{2})\" ", leftLinkPage, PageSize, strIsClick);
                }

                stbHTML.Append(">");
                stbHTML.Append("«&nbsp;向前");
                stbHTML.Append("</span>");
                stbHTML.Append("<span style=\"float: right; \" ");
                if (PageIndex >= pCount)
                {
                    stbHTML.Append(" class=\"spanNoLinkAutoComplete\"");
                }
                else
                {
                    stbHTML.Append(" class=\"spanLinkAutoComplete\"");
                    stbHTML.AppendFormat(" onclick=\"AutoCompleteGetDataListAll({0}, {1},{2})\" ", rightLinkPage, PageSize, strIsClick);

                }
                stbHTML.Append(">");
                stbHTML.Append("向后&nbsp;»");
                stbHTML.Append("</span>");

                stbHTML.Append("</div>");
            }
            if (pCount <= 1)
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