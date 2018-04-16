using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Sys
{
    public partial class SysFunction : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90500100";

            UserFun = new BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            if (!UserFun.page)
            {
                Rc.Common.StrUtility.clsUtility.ErrorDispose(this.Page, 2, false);
            }

            this.btnAdd.Visible = UserFun.Add;
            if (!IsPostBack)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Model_Sys_Function model = new Model_Sys_Function();
                BLL_Sys_Function bll = new BLL_Sys_Function();
                model.FUNCTIONID = txtFunctionId.Text.Trim();
                model.FUNCTIONName = txtFunctionName.Text.Trim();
                if (hidCtrl.Value == "1")
                {
                    bool result = bll.AddSys_Function(model);
                    if (result)
                    {
                        StringBuilder strLog = new StringBuilder();
                        strLog.AppendFormat("【添加功能】【ID】： {0}【功能名】：{1}", model.FUNCTIONID, model.FUNCTIONName);
                        new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                    }
                }
                else if (hidCtrl.Value == "2")
                {
                    bool result = bll.UpdateSys_FunctionByID(model);
                    if (result)
                    {
                        StringBuilder strLog = new StringBuilder();
                        strLog.AppendFormat("【修改功能】【ID】： {0}【功能名】：{1}", model.FUNCTIONID, model.FUNCTIONName);
                        new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected string GetHtmlData()
        {
            try
            {
                var modelList = new BLL_Sys_Function().GetSys_FunctionModelList("");
                int i = 0;
                StringBuilder strHtmlData = new StringBuilder();
                strHtmlData.Append("<table class='table table-hover table-bordered'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr>");
                strHtmlData.Append("<th>编码</th>");
                strHtmlData.Append("<th>名称</th>");
                strHtmlData.Append("<th>操作</th>");
                strHtmlData.Append("</tr>");
                strHtmlData.Append("</thead>");
                strHtmlData.Append("<tbody>");
                foreach (var item in modelList)
                {
                    i++;
                    strHtmlData.Append("<tr>");
                    strHtmlData.Append("<td>" + item.FUNCTIONID + "</td>");
                    strHtmlData.Append("<td>" + item.FUNCTIONName + "</td>");
                    strHtmlData.Append("<td class='opera'>");
                    if (UserFun.Edit)
                    {
                        strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"showPop('2','{0}','{1}')\">编辑</a>"
                            , item.FUNCTIONID
                            , item.FUNCTIONName);
                    }
                    else
                    {
                        strHtmlData.Append("<a href='javascript:;' class='disabled'>编辑</a>");
                    }
                    if (UserFun.Delete)
                    {
                        strHtmlData.AppendFormat("<a href='javascript:;' onclick=\"DeleteFunction('{0}')\">删除</a>", item.FUNCTIONID);
                    }
                    else
                    {
                        strHtmlData.Append("<a href='javascript:;'>删除</a>");
                    }
                    strHtmlData.Append("</td>");
                    strHtmlData.Append("  ");
                    strHtmlData.Append("</tr>");
                }
                strHtmlData.Append("</tbody>");
                strHtmlData.Append("</table>");
                if (i == 0)
                {
                    strHtmlData.Append("<div class='nodata'>暂无数据</div>");
                }
                return strHtmlData.ToString();
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

    }
}