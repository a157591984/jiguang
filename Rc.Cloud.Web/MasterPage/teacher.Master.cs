using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;

namespace Homework.MasterPage
{
    public partial class teacher : System.Web.UI.MasterPage
    {
        protected string listLi = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTemp = string.Empty;
            string strS = string.Empty;
            if (!String.IsNullOrEmpty(Request["s"]))
            {
                strS = Request["s"].ToString();
            }
            strTemp += "";

            //if (Request["psdTips"].Filter() == "1")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.confirm('密码过于简单，请修改密码！', {icon:4}, function () { window.location.href = '/teacher/safeSetting.aspx'; });});</script>");
            //}

            Model_F_User modelFUser = (Model_F_User)Session["FLoginUser"];
            if (modelFUser != null)
            {
                BLL_UserGroup_Member bllUGM = new BLL_UserGroup_Member();
                string strUserName = string.IsNullOrEmpty(modelFUser.TrueName) ? modelFUser.UserName : modelFUser.TrueName;
                //int classMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='" + modelFUser.UserId + "')");
                int classMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Class
                    + "' and User_Id='" + modelFUser.UserId + "')");
                //是否有学科
                bool HasSubject = (!string.IsNullOrEmpty(modelFUser.Subject.Trim()) && modelFUser.Subject.Trim() != "-1");
                //是否带班
                int classCount = new BLL_UserGroup().GetRecordCount(" UserGroup_AttrEnum='Class' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where USER_ID='" + modelFUser.UserId + "' and User_ApplicationStatus='passed' and UserStatus='0') ");
                if (pfunction.GetWebMdlIsShow("FAQ")) ahelp.Visible = true;
                switch (modelFUser.UserPost)
                {
                    #region 校长/副校长/教务主任
                    case UserPost.校长://"我是校长：" + 
                        int SchoolMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.School
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        int GradeMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        int MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";
                        if (classCount > 0)
                        {
                            if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                            listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                            listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        }
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Principal/AnalysisGradeList.aspx\">数据分析</a></li>";
                        if (HasSubject && classCount > 0) listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";

                        listLi += "<li class='dropdown'><a href='javascript:;' data-toggle='dropdown'>学校管理 <span class='caret'></span></a>";
                        listLi += "<ul class='dropdown-menu'>";
                        listLi += string.Format("<li><a href=\"/teacher/SchoolList.aspx\">学校{0}</a></li>"
                            , (SchoolMesCount > 0) ? "<span class='badge'>" + SchoolMesCount + "</span>" : "");
                        listLi += string.Format("<li><a href=\"/teacher/GradeList.aspx\">年级{0}</a></li>"
                            , (GradeMesCount > 0) ? "<span class='badge'>" + GradeMesCount + "</span>" : "");
                        if (HasSubject || classCount > 0) listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += "</ul>";
                        listLi += "</li>";

                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                    case UserPost.副校长://"我是副校长：" + 
                        SchoolMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.School
                   + "' and User_Id='" + modelFUser.UserId + "')");
                        GradeMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                   + "' and User_Id='" + modelFUser.UserId + "')");
                        MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";
                        if (classCount > 0)
                        {
                            if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                            listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                            listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        }
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Principal/AnalysisGradeList.aspx\">数据分析</a></li>";
                        if (HasSubject && classCount > 0) listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";

                        listLi += "<li class='dropdown'><a href='javascript:;' data-toggle='dropdown'>学校管理 <span class='caret'></span></a>";
                        listLi += "<ul class='dropdown-menu'>";
                        listLi += string.Format("<li><a href=\"/teacher/SchoolList.aspx\">学校{0}</a></li>"
                            , (SchoolMesCount > 0) ? "<span class='badge'>" + SchoolMesCount + "</span>" : "");
                        listLi += string.Format("<li><a href=\"/teacher/GradeList.aspx\">年级{0}</a></li>"
                            , (GradeMesCount > 0) ? "<span class='badge'>" + GradeMesCount + "</span>" : "");
                        if (HasSubject || classCount > 0) listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += "</ul>";
                        listLi += "</li>";
                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                    case UserPost.教务主任://"我是教务主任：" + 
                        SchoolMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.School
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        GradeMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";
                        if (classCount > 0)
                        {
                            if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                            listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                            listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        }
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Principal/AnalysisGradeList.aspx\">数据分析</a></li>";
                        if (HasSubject && classCount > 0) listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";

                        listLi += "<li class='dropdown'><a href='javascript:;' data-toggle='dropdown'>学校管理 <span class='caret'></span></a>";
                        listLi += "<ul class='dropdown-menu'>";
                        listLi += string.Format("<li><a href=\"/teacher/SchoolList.aspx\">学校{0}</a></li>"
                            , (SchoolMesCount > 0) ? "<span class='badge'>" + SchoolMesCount + "</span>" : "");
                        listLi += string.Format("<li><a href=\"/teacher/GradeList.aspx\">年级{0}</a></li>"
                            , (GradeMesCount > 0) ? "<span class='badge'>" + GradeMesCount + "</span>" : "");
                        if (HasSubject || classCount > 0) listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += "</ul>";
                        listLi += "</li>";
                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                    case UserPost.教研组长://"我是教研组长：" + 
                        SchoolMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.School
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        GradeMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";
                        if (classCount > 0)
                        {
                            if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                            listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                            listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        }
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Principal/AnalysisGradeList.aspx\">数据分析</a></li>";
                        if (HasSubject && classCount > 0) listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";

                        listLi += string.Format("<li class='dropdown'><a href='javascript:;' data-toggle='dropdown'>学校管理 <span class='caret'></span>{0}</a>"
                            , (SchoolMesCount > 0) ? "<span class='badge'>" + SchoolMesCount + "</span>" : "");
                        listLi += "<ul class='dropdown-menu'>";
                        listLi += string.Format("<li><a href=\"/teacher/GradeList.aspx\">学校{0}</a></li>"
                            , (SchoolMesCount > 0) ? "<span class='badge'>" + SchoolMesCount + "</span>" : "");
                        listLi += string.Format("<li><a href=\"/teacher/GradeList.aspx\">年级{0}</a></li>"
                            , (GradeMesCount > 0) ? "<span class='badge'>" + GradeMesCount + "</span>" : "");
                        if (HasSubject || classCount > 0) listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += "</ul>";
                        listLi += "</li>";

                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                    #endregion
                    #region 年级组长/备课组长
                    case UserPost.年级组长://"我是年级组长：" + 
                        GradeMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";
                        if (classCount > 0)
                        {
                            if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                            listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                            listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        }
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Principal/AnalysisGradeList.aspx\">数据分析</a></li>";
                        if (HasSubject && classCount > 0) listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";

                        listLi += "<li class='dropdown'><a href='javascript:;' data-toggle='dropdown'>年级管理 <span class='caret'></span></a>";
                        listLi += "<ul class='dropdown-menu'>";
                        listLi += string.Format("<li><a href='/teacher/GradeList.aspx'>年级{0}</a>"
                            , (GradeMesCount > 0) ? "<span class='badge'>" + GradeMesCount + "</span>" : "");
                        if (HasSubject || classCount > 0) listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += "</ul>";
                        listLi += "</li>";

                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                    case UserPost.备课组长://"我是备课组长：" + 
                        GradeMesCount = new BLL_UserGroup_Member().GetRecordCount("User_ApplicationStatus='applied' and UserGroup_Id in(select UserGroup_Id from UserGroup where UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade
                    + "' and User_Id='" + modelFUser.UserId + "')");
                        MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";

                        if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                        listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                        listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Principal/AnalysisGradeList.aspx\">数据分析</a></li>";
                        if (HasSubject && classCount > 0) listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";

                        listLi += "<li class='dropdown'><a href='javascript:;' data-toggle='dropdown'>年级管理 <span class='caret'></span></a>";
                        listLi += "<ul class='dropdown-menu'>";
                        listLi += string.Format("<li><a href='/teacher/GradeList.aspx'>年级{0}</a>"
                            , (GradeMesCount > 0) ? "<span class='badge'>" + GradeMesCount + "</span>" : "");
                        if (HasSubject || classCount > 0) listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += "</ul>";
                        listLi += "</li>";
                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                    #endregion
                    #region 普通老师
                    case UserPost.普通老师://"我是普通老师：" + 
                        MesCount = new BLL_Msg().GetRecordCount("MsgStatus='Unread' and MsgAccepter='" + modelFUser.UserId + "'");
                        ltlUserPost.Text = "<a href='javascript:;' data-toggle='dropdown'><i class='material-icons'>&#xE853;</i>&nbsp;" + strUserName + "&nbsp;老师</a>";

                        if (pfunction.GetWebMdlIsShow("cTeachPlan")) listLi += "<li><a href=\"/teacher/cTeachPlan.aspx\">我的教案</a></li>";
                        listLi += "<li><a href=\"/teacher/cHomework.aspx\">布置作业</a></li>";
                        listLi += "<li><a href=\"/teacher/cCorrectHomework.aspx\">批改作业</a></li>";
                        if (pfunction.GetWebMdlIsShow("CollectiveLessonPreparation")) listLi += "<li><a href=\"/teacher/CollectiveLessonPreparation.aspx\">集体备课</a></li>";
                        listLi += "<li><a href=\"/Evaluation/EachHWAnalysis.aspx\">数据分析</a></li>";
                        listLi += "<li><a href=\"/teacher/Comment.aspx\">讲评</a></li>";
                        listLi += "<li><a href=\"/teacher/allTeachingPlan.aspx\">购买教案/习题</a></li>";
                        listLi += string.Format("<li><a href=\"/teacher/classList.aspx\">班级管理{0}</a></li>"
                            , (classMesCount > 0) ? "<span class='badge'>" + classMesCount + "</span>" : "");
                        listLi += string.Format("<li><a href=\"/teacher/Message.aspx\">消息{0}</a></li>"
                            , (MesCount > 0) ? "<span class='badge'>" + MesCount + "</span>" : "");
                        //if (pfunction.GetWebMdlIsShow("FAQ")) listLi += " <li><a href=\"../Help/FAQ.aspx\" target=\"_blank\">帮助</a></li>";
                        break;
                        #endregion
                }
            }
        }

    }
}