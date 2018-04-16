using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.Sys
{
    public partial class SelectTeacher : Rc.Cloud.Web.Common.InitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10200100";
            SetSearchP();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //学校
                //strWhere = " D_Type='8' order by d_order";
                //dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                //Rc.Cloud.Web.Common.pfunction.SetDdl(ddlShoole, dt, "D_Name", "Common_Dict_ID", "请选择");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "学科");
                SetSearchO();
                litContent.Text = GetHtmlList().ToString();

            }

        }
        //给查询对象附值
        private void SetSearchO()
        {
            //ddlShoole.SelectedValue = Request["ddlShoole"];
            hidtxtSchool.Value = Request["hidtxtSchool"];
            txtSchool.Value = Request["txtSchool"];
            ddlSubject.SelectedValue = Request["ddlSubject"];
            ddlPower.SelectedValue = Request["ddlPower"];
            txtSearchNameS.Value = Request["txtSearchNameS"];
        }
        private void SetSearchP()
        {
            if (!string.IsNullOrEmpty(Request["PageIndex"]))
            {
                if (!int.TryParse(Request["PageIndex"].ToString(), out PageIndex)) { PageIndex = 1; }
                if (PageIndex <= 0) { PageIndex = 1; }
            }
            if (!string.IsNullOrEmpty(Request["PageSize"]))
            {
                if (!int.TryParse(Request["PageSize"].ToString(), out PageSize)) { PageSize = 10; }
            }

            strUrlPara = strPageName + "?PageIndex={0}&PageSize={1}";
            //strUrlPara += "&ddlShoole=" + Request["ddlShoole"];
            strUrlPara += "&hidtxtSchool=" + Request["hidtxtSchool"];
            strUrlPara += "&txtSchool=" + Request["txtSchool"];
            strUrlPara += "&ddlSubject=" + Request["ddlSubject"];
            strUrlPara += "&ddlPower=" + Request["ddlPower"];
            strUrlPara += "&txtSearchNameS=" + Request["txtSearchNameS"];
            strUrlPara += "&bid=" + Request["bid"];
            strUrlPara += "&dType=" + Request["dType"];
        }
        private StringBuilder GetHtmlList()
        {
            string strWhere = string.Empty;
            string strSql = string.Empty;
            // string _t = Request.QueryString["dType"];//1为老师，2为学生。     
            strWhere += "  where UserIdentity='T'";

            //if (ddlShoole.SelectedValue != "-1")
            //{
            //    strWhere += string.Format(" and School='{0}'", ddlShoole.SelectedValue);
            //}
            if (ddlSubject.SelectedValue != "-1")
            {
                strWhere += string.Format(" and Subject='{0}'", ddlSubject.SelectedValue);
            }
            if (ddlPower.SelectedValue == "-1")
            {
                //strWhere += " and t1.Book_id is not null";
            }
            else if (ddlPower.SelectedValue == "1")
            {
                strWhere += " and t1.Two_WayChecklist_Id is not null";
            }
            else if (ddlPower.SelectedValue == "0")
            {
                strWhere += " and t1.Two_WayChecklist_Id is null";
            }
            if (txtSearchNameS.Value != "")
            {
                strWhere += string.Format(" and (t.UserName like '%{0}%' or t.TrueName like '%{0}%')", txtSearchNameS.Value.Filter());
            }
            if (!string.IsNullOrEmpty(hidtxtSchool.Value.Trim()))
            {
                strWhere += " and t.UserId in(select UserId from VW_UserOnClassGradeSchool where SchoolId='" + hidtxtSchool.Value.Trim() + "') ";
            }

            string strBookId = string.Empty;
            StringBuilder strHtml = new StringBuilder();
            if (!String.IsNullOrEmpty(Request["bid"]))
            {
                strBookId = Request["bid"].ToString();

                strSql += string.Format(@"select row_number() over(order by UserName desc) AS r_n,*  from (  SELECT t.*
,t3.D_Name as SubjectName
,(CASE WHEN t1.Two_WayChecklist_Id IS NULL THEN '-1' ELSE  '1' END )
 isUserBuyResources FROM F_USER t
left join Two_WayChecklistAuth t1 on t.userid=t1.user_id and Two_WayChecklist_Id='{0}'
left join Common_Dict t3 on t.Subject=t3.Common_Dict_ID {1} )a ", strBookId, strWhere);
                DataTable dt = new DataTable();
                dt = Rc.Common.DBUtility.sys.GetRecordByPage(strSql, PageIndex, PageSize, out  rCount, out pCount).Tables[0];

                DataTable dtUserSchool = Rc.Common.DBUtility.DbHelperSQL.Query("select distinct UserId,UserName,TrueName,SchoolId,SchoolName from VW_UserOnClassGradeSchool where SchoolId is not null ").Tables[0];

                strHtml.Append(" <table class='table table-bordered table-hover' id='table_content' cellpadding='0' cellspacing='0'>");
                strHtml.Append(" <thead>");
                strHtml.Append(" <tr>");
                strHtml.Append(" <th>");
                strHtml.Append(" <input type='checkbox' id='checkall' name='checkAll' />全选");
                strHtml.Append(" </th>");
                strHtml.Append(" <th>老师姓名</th>");
                strHtml.Append(" <th>学校</th>");
                strHtml.Append(" <th>学科</th>");
                strHtml.Append(" </thead>");
                strHtml.Append(" <tbody>");


                strHtml.Append(" </tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drUserSchool = dtUserSchool.Select("UserId='" + dt.Rows[i]["UserId"] + "'");
                    strHtml.Append(" <tr>");
                    strHtml.Append(" <td>");
                    strHtml.AppendFormat(" <input type='checkbox' name='name' value='{0}' />{1}</td>"
                     , dt.Rows[i]["userid"].ToString(), dt.Rows[i]["isUserBuyResources"].ToString() == "-1" ? "未授权" : "<font color='green'>已授权</font>");

                    strHtml.AppendFormat(" <td>{0}</td>", dt.Rows[i]["UserName"].ToString());
                    strHtml.AppendFormat(" <td>{0}</td>"
                        , drUserSchool.Length > 0 ? drUserSchool[0]["SchoolName"].ToString() : "");
                    strHtml.AppendFormat(" <td>{0}</td>", dt.Rows[i]["SubjectName"].ToString());


                    strHtml.Append(" </tr>");
                }
                if (dt.Rows.Count == 0)
                {
                    strHtml.Append(" <tr>");
                    strHtml.AppendFormat(" <td colspan='4' style='text-align: center; line-height:50px; font-size:20px;'>{0}</td>", "没找到你想要的老师。");
                    strHtml.Append(" </tr>");
                }

                strHtml.Append(" </tbody>");
                strHtml.Append(" </table>");
            }
            else
            {
                strHtml.Append("<div>参数错误</div>");
            }

            return strHtml;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            //strUrlPara += "&ddlShoole=" + ddlShoole.SelectedValue;
            strUrlPara += "&hidtxtSchool=" + hidtxtSchool.Value.Trim();
            strUrlPara += "&txtSchool=" + txtSchool.Value.Trim();
            strUrlPara += "&ddlSubject=" + ddlSubject.SelectedValue;
            strUrlPara += "&ddlPower=" + ddlPower.SelectedValue;
            strUrlPara += "&txtSearchNameS=" + txtSearchNameS.Value;
            strUrlPara += "&bid=" + Request["bid"];
            strUrlPara += "&dType=" + Request["dType"];
            Response.Redirect(strUrlPara);

        }
        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        protected void btnDeleteBePower_Click(object sender, EventArgs e)
        {
            string _IDs = hidUserIDs.Value.TrimEnd(',');
            string _bid = Request["bid"];
            string _dSql = "delete Two_WayChecklistAuth where User_Id in ('" + _IDs.Replace(",", "','") + "') and Two_WayChecklist_id ='" + _bid + "';";
            try
            {
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(_dSql);
                if (result > 0)
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(Module_Id, string.Format("取消老师授权成功，操作人：{0}，资源标识：{1}，老师标识：{2}"
                        , loginUser.SysUser_ID, _bid, _IDs));
                }
                else
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("取消老师授权失败，操作人：{0}，资源标识：{1}，老师标识：{2}"
                        , loginUser.SysUser_ID, _bid, _IDs));
                }
                ShowMsg(result > 0 ? true : false, "操作成功！");
            }
            catch (Exception ex)
            {
                //记录错误日志
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("取消老师授权失败，操作人：{0}，类：{1}，方法{2}，错误信息：{3}"
                    , loginUser.SysUser_ID
                    , ex.TargetSite.DeclaringType.ToString()
                    , ex.TargetSite.Name.ToString()
                    , ex.Message));
                ShowMsg(false, "操作失败！");
                return;
            }
        }
        protected void btnBePower_Click(object sender, EventArgs e)
        {
            BLL_Two_WayChecklistAuth bll = new BLL_Two_WayChecklistAuth();
            List<Model_Two_WayChecklistAuth> listModel = new List<Model_Two_WayChecklistAuth>();
            string _bid = Request["bid"];
            string _IDs = hidUserIDs.Value.TrimEnd(',');
            string _dSql = "delete Two_WayChecklistAuth where User_Id in ('" + _IDs.Replace(",", "','") + "') and Two_WayChecklist_Id ='" + _bid + "';";
            int result = 0;
            foreach (string item in _IDs.Split(','))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Model_Two_WayChecklistAuth model = new Model_Two_WayChecklistAuth();
                    model.Two_WayChecklistAuth_Id = Guid.NewGuid().ToString();
                    model.User_Id = item;
                    model.Two_WayChecklist_Id = _bid;
                    model.Auth_Type = UserOrder_PaytoolEnum.NBSQ.ToString();//内部授权
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    listModel.Add(model);

                }
            }
            try
            {
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(_dSql);
                foreach (var item in listModel)
                {
                    bool flag = bll.Add(item);
                    if (flag)
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(Module_Id, string.Format("给老师授权成功，操作人：{0}，资源标识：{1}，老师标识：{2}"
                            , loginUser.SysUser_ID, item.Two_WayChecklist_Id, item.User_Id));
                    }
                    else
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("给老师授权失败，操作人：{0}，资源标识：{1}，老师标识：{2}"
                           , loginUser.SysUser_ID, item.Two_WayChecklist_Id, item.User_Id));
                    }
                    result += flag == true ? 1 : 0;
                }
                ShowMsg(result > 0 ? true : false, "操作成功！");
            }
            catch (Exception ex)
            {
                //记录错误日志
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("给老师授权失败，操作人：{0}，类：{1}，方法{2}，错误信息：{3}"
                    , loginUser.SysUser_ID
                    , ex.TargetSite.DeclaringType.ToString()
                    , ex.TargetSite.Name.ToString()
                    , ex.Message));
                ShowMsg(false, "操作失败！");
                return;
            }

        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="resutl">成功/失败</param>
        /// <param name="url">成功后跳转的连接</param>
        /// <param name="errorMsg">错误后提示的内容</param>
        private void ShowMsg(bool resutl, string errorMsg)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");

            if (resutl)
                scriptStr.Append("layer.ready(function () {layer.msg('" + errorMsg + "', { icon: 1,time:1000 },function(){window.location.href='" + Request.Url.ToString() + "';});})");
            else
                scriptStr.Append("layer.ready(function () {layer.msg('" + errorMsg + "',{icon: 2})})");

            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());

        }
    }

}