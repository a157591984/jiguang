using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.Common.Config;
using Rc.Common;
using System.Text;
using Rc.Common.DBUtility;

namespace Homework.teacher
{
    public partial class layoutIframe : Rc.Cloud.Web.Common.FInitData
    {
        string rtrId = string.Empty;
        string classId = string.Empty;
        protected string className = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rtrId = Request.QueryString["rtrId"].Filter();
            classId = Request.QueryString["classId"].Filter();
            if (!IsPostBack)
            {
                txtBeginTime.Attributes.Add("readonly", "true");
                txtStopTime.Attributes.Add("readonly", "true");
                LoadData();
            }
        }

        private void LoadData()
        {
            string strWhere = string.Format(" User_ApplicationStatus='passed' and UserStatus='0' and UserGroup_Id='{0}' and MembershipEnum='{1}' ", classId, MembershipEnum.student);
            rptStudentList.DataSource = new BLL_UserGroup_Member().GetClassMemberListByPageEX(strWhere, "TrueName", 1, 1000).Tables[0];
            rptStudentList.DataBind();

            Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrId);
            if (modelRTRF != null)
            {
                txtHomeWork_Name.Text = modelRTRF.File_Name.ReplaceForFilter();
            }
            Model_UserGroup modelUserGroup = new BLL_UserGroup().GetModel(classId);
            if (modelUserGroup != null)
            {
                className = modelUserGroup.UserGroup_Name;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strHomeWork_Id = Guid.NewGuid().ToString();
            try
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：1开始验证");
                // 学校公网IP
                string hostUrl = pfunction.GetResourceHost2("TestWebSiteUrl");
                #region 验证
                DataTable dtHWDetail = Rc.Common.DBUtility.DbHelperSQL.Query("select * from VW_ClassGradeSchool where ClassId='" + classId + "' and GradeId is not null and SchoolId is not null ").Tables[0];
                if (dtHWDetail.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('加入年级后才可布置作业',{time:2000,icon:2});", true);
                    return;
                }
                BLL_HomeWork bll = new BLL_HomeWork();
                if (bll.GetRecordCount("ResourceToResourceFolder_Id='" + rtrId + "' and HomeWork_AssignTeacher='" + FloginUser.UserId + "' and UserGroup_Id='" + classId + "' ") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('该作业已被布置',{time:2000,icon:2},function(){window.parent.location.reload();parent.layer.close(parent.layer.getFrameIndex(window.name));});", true);
                    return;
                }

                if (new BLL_UserGroup_Member().GetRecordCount("UserStatus='0' and User_Id='" + FloginUser.UserId + "' and UserGroup_Id='" + classId + "' ") == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('老师与所布置作业班级关系错误',{time:2000,icon:2},function(){window.parent.location.reload();parent.layer.close(parent.layer.getFrameIndex(window.name));});", true);
                    return;
                }

                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrId);

                if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                {
                    if (modelRTRF.Resource_Type != Resource_TypeConst.集体备课文件 && modelRTRF.CreateFUser != FloginUser.UserId)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('此资源不是您的',{time:2000,icon:2},function(){window.parent.location.reload();parent.layer.close(parent.layer.getFrameIndex(window.name));});", true);
                        return;
                    }
                }
                else if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                {
                    if (new BLL_UserBuyResources().GetRecordCount("UserId='" + FloginUser.UserId + "' and Book_Id='" + modelRTRF.Book_ID + "' ") == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('老师还未购买此资源',{time:2000,icon:2},function(){window.parent.location.reload();parent.layer.close(parent.layer.getFrameIndex(window.name));});", true);
                        return;
                    }

                }
                if (pfunction.FilterKeyWords(this.txtHomeWork_Name.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('作业名称存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                //Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：验证网络是否通畅" + hostUrl);
                ////检测网络是否通畅
                //if (RemotWeb.PostDataToServer(hostUrl + "/AuthApi/index.aspx?key=onlinecheck", "", Encoding.UTF8, "Get") != "ok")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('服务器网络不通畅。',{icon:2,time:3000});</script>");
                //    return;
                //}
                #endregion
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：1结束验证");

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：2开始组织提交数据");
                #region 组织提交数据

                DateTime hw_time = DateTime.Now;
                int isTimeLength = 0;
                int.TryParse(txtTimeLength.Text, out isTimeLength);
                int isTimeLimt = 1;
                isTimeLimt = (hwType_1.Checked == true) ? 1 : 2;//作业类型：1=作业，2=考试
                DateTime BeginTime = DateTime.Parse(txtBeginTime.Text);
                DateTime StopTime = DateTime.Now;
                if (isTimeLimt == 2)
                {
                    StopTime = DateTime.Parse(BeginTime.ToString()).AddMinutes(int.Parse(isTimeLength.ToString()));
                }
                else
                {
                    StopTime = Convert.ToDateTime(txtStopTime.Text);
                }
                string strStudent = Rc.Cloud.Web.Common.pfunction.CheckImp(hidStudentId.Value);
                strStudent = strStudent.TrimEnd(',');
                string SubjectId = FloginUser.Subject;
                if (modelRTRF != null && modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                {
                    SubjectId = modelRTRF.Subject;
                }
                object objAssign = new
                {
                    stuInfo = strStudent,
                    HomeWork_Id = strHomeWork_Id,
                    ResourceToResourceFolder_Id = rtrId,
                    HomeWork_Name = txtHomeWork_Name.Text,
                    HomeWork_AssignTeacher = FloginUser.UserId,
                    BeginTime = BeginTime,
                    StopTime = StopTime,
                    IsHide = chkIsHide.Checked ? 1 : 0,
                    HomeWork_Status = 0,
                    CreateTime = hw_time,
                    UserGroup_Id = classId,
                    isTimeLimt = isTimeLimt,
                    isTimeLength = isTimeLength,
                    SubjectId = SubjectId,
                    IsShowAnswer = (chkIsShowAnswer.Checked == true) ? 1 : 0
                };
                #endregion
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：2结束组织提交数据");

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：3开始提交到学校服务器");

                hostUrl += "teacher/tchAssignHW.aspx";
                string result = Rc.Common.RemotWeb.PostDataToServer(hostUrl, JsonConvert.SerializeObject(objAssign), System.Text.Encoding.UTF8, "POST");
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：3结束提交到学校服务器");
                if (result == "ok")
                {
                    string strJ = "layer.msg('布置作业成功" + result + "',{time:1000,icon:1},function(){";
                    if (Request.QueryString["tp"] != "1")
                    {
                        strJ += "window.parent.loadData();";
                    }
                    else
                    {
                        strJ += "window.parent.location.reload();";
                    }
                    strJ += "parent.layer.close(parent.layer.getFrameIndex(window.name));});";
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业成功" + result);
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", strJ, true);
                }
                else
                {
                    //RevokeHW 布置作业失败 撤销作业
                    new BLL_HomeWork().RevokeHW(strHomeWork_Id);
                    Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(FloginUser.UserId, "", "布置作业失败：学校未布置成功" + result + "; 。");
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('布置作业失败',{time:2000,icon:2});", true);
                }

            }
            catch (Exception ex)
            {
                //RevokeHW 布置作业失败 撤销作业
                new BLL_HomeWork().RevokeHW(strHomeWork_Id);
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(FloginUser.UserId, "", "布置作业失败：" + ex.Message.ToString());
                ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('布置作业异常，请联系管理员',{time:2000,icon:2});", true);
            }
        }

    }
}