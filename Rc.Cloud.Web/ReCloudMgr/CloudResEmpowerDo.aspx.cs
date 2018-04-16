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
    public partial class CloudResEmpowerDo : Rc.Cloud.Web.Common.InitData
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
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID");
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
            string strWhere1 = string.Empty;
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
                strWhere += " and t1.Book_id is not null";
            }
            else if (ddlPower.SelectedValue == "0")
            {
                strWhere += " and t1.Book_id is null";
            }
            if (txtSearchNameS.Value != "")
            {
                strWhere += string.Format(" and (t.UserName like '%{0}%' or t.TrueName like '%{0}%')", txtSearchNameS.Value.Filter());
            }
            if (!string.IsNullOrEmpty(hidtxtSchool.Value.Trim()))
            {
                strWhere1 += string.Format(@"  inner join VW_UserOnClassGradeSchool vw on vw.userId=t.userid and vw.SchoolId='{0}' ", hidtxtSchool.Value);
            }

            string strBookId = string.Empty;
            StringBuilder strHtml = new StringBuilder();
            if (!String.IsNullOrEmpty(Request["bid"]))
            {
                strBookId = Request["bid"].ToString();

                strSql += string.Format(@"select row_number() over(order by UserName desc) AS r_n,*  from (  SELECT distinct t.*
,t3.D_Name as SubjectName
,(CASE WHEN t1.Book_id IS NULL THEN '-1' ELSE  '1' END )
 isUserBuyResources FROM F_USER t
left join UserBuyResources t1 on t.UserId=t1.userid and Book_id='{0}'
left join Common_Dict t3 on t.Subject=t3.Common_Dict_ID {2} {1} )a ", strBookId, strWhere, strWhere1);
                DataTable dt = new DataTable();
                dt = Rc.Common.DBUtility.sys.GetRecordByPage(strSql, PageIndex, PageSize, out  rCount, out pCount).Tables[0];

                DataTable dtUserSchool = Rc.Common.DBUtility.DbHelperSQL.Query("select distinct UserId,UserName,TrueName,SchoolId,SchoolName from VW_UserOnClassGradeSchool where SchoolId is not null ").Tables[0];

                strHtml.Append(" <table class='table table-bordered table-hover' id='table_content'>");
                strHtml.Append(" <thead>");
                strHtml.Append(" <tr>");
                strHtml.Append(" <th style='width: 120px'>");
                strHtml.Append(" <input type='checkbox' id='checkall' name='checkAll' />");
                strHtml.Append(" 全选</th>");
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
                    strHtml.Append(" <tr class='tr_pop_002' >");


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
            string _dSql = "delete UserBuyResources where UserId in ('" + _IDs.Replace(",", "','") + "') and book_id ='" + _bid + "';";
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
                if (result > 0)
                {
                    ShowMsg(true, "操作成功！");
                }
                else
                {
                    ShowMsg(false, "操作失败！");
                }
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
            BLL_UserBuyResources bll = new BLL_UserBuyResources();
            List<Model_UserBuyResources> listModel = new List<Model_UserBuyResources>();
            string _bid = Request["bid"];
            string _IDs = hidUserIDs.Value.TrimEnd(',');
            string _dSql = "delete UserBuyResources where UserId in ('" + _IDs.Replace(",", "','") + "') and book_id ='" + _bid + "';";
            int result = 0;
            foreach (string item in _IDs.Split(','))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Model_UserBuyResources model = new Model_UserBuyResources();
                    model.UserBuyResources_ID = Guid.NewGuid().ToString();
                    model.UserId = item;
                    model.Book_id = _bid;
                    model.BuyType = UserOrder_PaytoolEnum.NBSQ.ToString();//内部授权
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
                            , loginUser.SysUser_ID, item.Book_id, item.UserId));
                    }
                    else
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("给老师授权失败，操作人：{0}，资源标识：{1}，老师标识：{2}"
                           , loginUser.SysUser_ID, item.Book_id, item.UserId));
                    }
                    result += flag == true ? 1 : 0;
                }
                if (result > 0)
                {
                    ShowMsg(true, "操作成功！");
                }
                else
                {
                    ShowMsg(false, "操作失败！");
                }
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