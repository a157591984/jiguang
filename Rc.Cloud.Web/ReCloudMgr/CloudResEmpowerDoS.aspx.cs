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
    public partial class CloudResEmpowerDoS : Rc.Cloud.Web.Common.InitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10200200";
            SetSearchP();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                SetSearchO();
                litContent.Text = GetHtmlList().ToString();
            }

        }
        //给查询对象附值
        private void SetSearchO()
        {
            txt_BanJI.Text = Request["txt_BanJI"];
            txt_SName.Text = Request["txt_SName"];
            ddlPower.SelectedValue = Request["ddlPower"];
            txtTeacherNameS.Value = Request["txtTeacherNameS"];
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
            strUrlPara += "&txt_BanJI=" + Request["txt_BanJI"];
            strUrlPara += "&txt_SName=" + Request["txt_SName"];
            strUrlPara += "&ddlPower=" + Request["ddlPower"];
            strUrlPara += "&txtTeacherNameS=" + Request["txtTeacherNameS"];
            strUrlPara += "&bid=" + Request["bid"];
            strUrlPara += "&dType=" + Request["dType"];
        }
        private StringBuilder GetHtmlList()
        {
            string strWhere = string.Empty;
            string strSql = string.Empty;
            strWhere += " UserIdentity='S' ";

            string strBookId = string.Empty;
            if (!String.IsNullOrEmpty(Request["bid"]))
            {
                strBookId = Request["bid"].ToString().Filter();
            }
            if (txt_BanJI.Text != "")
            {
                strWhere += string.Format(@" and u.userid in(select ugm.User_ID
from UserGroup ug
inner join UserGroup_Member ugm 
on ug.UserGroup_Id=ugm.UserGroup_Id
inner join F_User u on u.UserId=ug.User_ID
where (ug.UserGroup_Id like '%{0}%' or ug.UserGroup_Name like '%{0}%'))", txt_BanJI.Text.Filter());
            }
            if (txt_SName.Text != "")
            {
                strWhere += string.Format(" and u.userName like '%{0}%' ", txt_SName.Text.Filter());
            }
            if (txtTeacherNameS.Value != "")
            {
                strWhere += string.Format(@" and u.userid in(select ugm.User_ID from UserGroup_Member ugm where ugm.MembershipEnum='{0}' and UserGroup_Id in(
select UserGroup_Id from UserGroup_Member ugm2
inner join F_User u on u.UserId=ugm2.User_ID
where ugm2.User_ApplicationStatus='passed' and ugm2.UserStatus='0' and ugm2.MembershipEnum in('{1}','{2}') and (u.UserName like '%{3}%' or u.TrueName like '%{3}%') ) ) "
                    , MembershipEnum.student
                    , MembershipEnum.headmaster
                    , MembershipEnum.teacher
                    , txtTeacherNameS.Value);
            }
            if (ddlPower.SelectedValue == "1")
            {
                strWhere += string.Format(@" and USERID in
(
select UserId from UserBuyResources a where a.UserId=u.userid and a.Book_id='{0}'
)", strBookId);

            }
            else if (ddlPower.SelectedValue == "2")
            {
                strWhere += string.Format(@" and USERID not in
(
select UserId from UserBuyResources a where a.UserId=u.userid and a.Book_id='{0}'
)", strBookId);
            }

            StringBuilder strHtml = new StringBuilder();
            if (!String.IsNullOrEmpty(Request["bid"]))
            {
                strBookId = Request["bid"].ToString().Filter();

                //                strSql += string.Format(@"select row_number() over(order by UserGroup_Name desc) AS r_n,*  from ( SELECT u.*,ug.UserGroup_Id,ug.UserGroup_Name,(CASE WHEN t1.Book_id IS NULL THEN '-1' ELSE  '1' END )
                // isUserBuyResources FROM F_User U 
                //INNER join UserGroup ug on u.UserId=ug.User_ID
                //inner join UserGroup_Member ugm on ug.UserGroup_Id=ugm.UserGroup_Id
                //left join UserBuyResources t1 on t1.UserId=u.UserId  and Book_id='{0}' where 1=1 {1} )a ", strBookId, strWhere);
                strSql += string.Format(@"select row_number() over(order by USERNAME desc) AS r_n,*  from ( SELECT u.* 
FROM F_User U 
 where {0} )a ", strWhere);
                DataTable dt = new DataTable();
                dt = Rc.Common.DBUtility.sys.GetRecordByPage(strSql, PageIndex, PageSize, out  rCount, out pCount).Tables[0];
                strHtml.Append(" <table class='table table-bordered table-hover' id='table_content'>");
                strHtml.Append(" <thead>");
                strHtml.Append(" <tr>");
                strHtml.Append(" <th style='width: 120px'>");
                strHtml.Append(" <input type='checkbox' id='checkall' name='checkAll' />");
                strHtml.Append(" 全选</th>");
                strHtml.Append(" <th>学生姓名</th>");
                strHtml.Append(" <th>所在班</th>");
                strHtml.Append(" <th>所属老师</th>");
                strHtml.Append(" </tr>");
                strHtml.Append(" </thead>");
                strHtml.Append(" <tbody>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strHtml.Append(" <tr>");
                    strHtml.Append(" <td>");
                    int strIsBook = 0; ;
                    strIsBook = GetStudentBuyBookIs(strBookId, dt.Rows[i]["userid"].ToString());
                    strHtml.AppendFormat(" <input type='checkbox' name='name' value='{0}' />{1}</td>"
                        , dt.Rows[i]["userid"].ToString(), strIsBook == 0 ? "未授权" : "<font color='green'>已授权</font>");

                    // strHtml.AppendFormat(" <td>{0}({1})</td>",dt.Rows[i]["UserGroup_Id"].ToString(), dt.Rows[i]["UserGroup_Name"].ToString());
                    strHtml.AppendFormat(" <td>{0}</td>", dt.Rows[i]["userName"].ToString());
                    strHtml.AppendFormat(" <td>{0}</td>", GetStudentClass(dt.Rows[i]["userid"].ToString(), ""));
                    strHtml.AppendFormat(" <td>{0}</td>", GetStudentTeacher(dt.Rows[i]["userid"].ToString()));
                    strHtml.Append(" </tr>");
                }
                if (dt.Rows.Count == 0)
                {
                    strHtml.Append(" <tr class='tr_pop_002' >");


                    strHtml.AppendFormat(" <td colspan='4' style='text-align: center; line-height:50px; font-size:20px;'>{0}</td>", "没找到你想要的学生。");
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
        /// <summary>
        /// 学生所属老师
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        private string GetStudentTeacher(string studentid)
        {
            string strTemp = string.Empty;
            string strSql = string.Empty;

            strSql = string.Format(@"select u.UserId,u.UserName
from UserGroup ug
inner join UserGroup_Member ugm 
on ug.UserGroup_Id=ugm.UserGroup_Id
inner join F_User u on u.UserId=ug.User_ID
where ugm.User_ID='{0}'", studentid);

            DataTable dt = new DataTable();
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strTemp += string.Format("{0}；", dt.Rows[i]["UserName"].ToString());
            }
            return strTemp;
        }
        /// <summary>
        /// 学生所在班
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        private string GetStudentClass(string studentid, string teacherid)
        {
            string strTemp = string.Empty;
            string strSql = string.Empty;

            strSql = string.Format(@"select ug.UserGroup_Id,ug.UserGroup_Name
from UserGroup ug
inner join UserGroup_Member ugm 
on ug.UserGroup_Id=ugm.UserGroup_Id
where 1=1");
            if (teacherid != "")
            {
                strSql += string.Format(" and  ug.User_ID='{0}'", teacherid);
            }
            strSql += string.Format(" and ugm.User_ID='{0}'", studentid);
            DataTable dt = new DataTable();
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strTemp += string.Format("{0}({1})；", dt.Rows[i]["UserGroup_Name"].ToString(), dt.Rows[i]["UserGroup_Id"].ToString());
            }
            return strTemp;
        }
        /// <summary>
        /// 学生是否有权限
        /// </summary>
        /// <param name="strBookid"></param>
        /// <param name="studentid"></param>
        /// <returns></returns>
        private int GetStudentBuyBookIs(string strBookid, string studentid)
        {
            int strTemp = 0;
            string strSql = string.Empty;
            strSql = string.Format("select count(*) from UserBuyResources where UserId='{0}' and Book_id='{1}'"
                , studentid, strBookid);
            strTemp = int.Parse(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString());
            return strTemp;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
            strUrlPara += "&txt_BanJI=" + txt_BanJI.Text;
            strUrlPara += "&txt_SName=" + txt_SName.Text;
            strUrlPara += "&ddlPower=" + ddlPower.SelectedValue;
            strUrlPara += "&txtTeacherNameS=" + txtTeacherNameS.Value;
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
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(Module_Id, string.Format("取消学生授权成功，操作人：{0}，资源标识：{1}，学生标识：{2}"
                        , loginUser.SysUser_ID, _bid, _IDs));
                }
                else
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("取消学生授权失败，操作人：{0}，资源标识：{1}，学生标识：{2}"
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
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("取消学生授权失败，操作人：{0}，类：{1}，方法{2}，错误信息：{3}"
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
            Rc.BLL.Resources.BLL_UserBuyResources bll = new Rc.BLL.Resources.BLL_UserBuyResources();
            List<Rc.Model.Resources.Model_UserBuyResources> listModel = new List<Rc.Model.Resources.Model_UserBuyResources>();
            string _bid = Request["bid"];
            string _IDs = hidUserIDs.Value.TrimEnd(',');
            string _dSql = "delete UserBuyResources where UserId in ('" + _IDs.Replace(",", "','") + "') and book_id ='" + _bid + "';";
            int result = 0;
            foreach (string item in _IDs.Split(','))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Rc.Model.Resources.Model_UserBuyResources model = new Rc.Model.Resources.Model_UserBuyResources();
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
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS(Module_Id, string.Format("给学生授权成功，操作人：{0}，资源标识：{1}，学生标识：{2}"
                            , loginUser.SysUser_ID, item.Book_id, item.UserId));
                    }
                    else
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("给学生授权失败，操作人：{0}，资源标识：{1}，学生标识：{2}"
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
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("给学生授权失败，操作人：{0}，类：{1}，方法{2}，错误信息：{3}"
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