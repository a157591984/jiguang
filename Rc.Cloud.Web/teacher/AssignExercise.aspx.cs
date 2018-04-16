using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Common;
using Rc.Common.Config;
using System.Text;
using Rc.Common.DBUtility;
using System.IO;

namespace Rc.Cloud.Web.teacher
{
    public partial class AssignExercise : Rc.Cloud.Web.Common.FInitData
    {
        protected string rtrfId = string.Empty;
        protected string rtrfName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            rtrfId = Request["rtrfId"].Filter();
            rtrfName = Request["Resource_Name"].Filter();
            string Resource_Name = Request["Resource_Name"].Filter();

            if (!IsPostBack)
            {
                txtHomeWork_Name.Text = Resource_Name;

                DataTable dtUserGroupList = new DataTable();
                string strWhere = string.Empty;
                strWhere = string.Format("UserGroup_AttrEnum='{1}' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}') order by UserGroupOrder "
                    , FloginUser.UserId
                    , UserGroup_AttrEnum.Class);
                dtUserGroupList = new BLL_UserGroup().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlClass, dtUserGroupList, "UserGroup_Name", "UserGroup_Id", false);

            }
        }

        [WebMethod]
        public static string GetTQList(string rtrfName, string classId, string rtrfId)
        {
            try
            {
                string strSql = string.Format(@"
select t.*,(case when htq.HomeworkToTQ_Id is null then 0 else 1 end) as IsExists from (
select TestQuestions_Id,topicNumber,TestQuestions_Num from TestQuestions where ResourceToResourceFolder_Id='{0}'
and Parent_Id<>'0' and [type]='simple' and TestQuestions_Type<>'' and TestQuestions_Type<>'title'
union all
select TestQuestions_Id,topicNumber,TestQuestions_Num from TestQuestions where ResourceToResourceFolder_Id='{0}'
and Parent_Id='0' and [type]='complex'
) t left join HomeworkToTQ htq on htq.TestQuestions_Id=t.TestQuestions_Id and htq.UserGroup_Id='{1}' and htq.rtrfId_Old='{0}'
 order by t.TestQuestions_Num
", rtrfId.Filter(), classId.Filter());
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                List<object> listReturn = new List<object>();

                string hwName = string.Empty;
                #region 获取已布置作业次数
                DataTable dtAssignCount = new DataTable();
                dtAssignCount = Rc.Common.DBUtility.DbHelperSQL.Query("select HomeWork_Id from HomeworkToTQ where rtrfId_Old='" + rtrfId + "' and UserGroup_Id='" + classId + "' group by HomeWork_Id").Tables[0];
                hwName += string.Format("{0} 第{1}部分", rtrfName, dtAssignCount.Rows.Count + 1);
                #endregion

                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        TestQuestions_Id = item["TestQuestions_Id"],
                        topicNumber = item["topicNumber"].ToString().Trim(),
                        IsExists = item["IsExists"]
                    });
                }
                if (dt.Rows.Count == 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        list = listReturn,
                        hwName = hwName
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "err"
                });
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strHomeWork_Id = Guid.NewGuid().ToString();
            try
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题：1开始验证");
                // 学校IP
                string hostUrl = pfunction.GetResourceHost("TestWebSiteUrl");
                //if (hostUrl != Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl"))
                //{
                //    #region 学校配置URL
                //    DataTable dtUrl = new BLL_ConfigSchool().GetSchoolPublicUrl(FloginUser.UserId).Tables[0];
                //    if (dtUrl.Rows.Count > 0)
                //    {
                //        hostUrl = dtUrl.Rows[0]["apiUrlList"].ToString();
                //    }
                //    #endregion
                //}                

                #region 验证
                if (pfunction.FilterKeyWords(this.txtHomeWork_Name.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('作业名称存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                DataTable dtHWDetail = Rc.Common.DBUtility.DbHelperSQL.Query("select * from VW_ClassGradeSchool where ClassId='" + ddlClass.SelectedValue + "' and GradeId is not null and SchoolId is not null ").Tables[0];
                if (dtHWDetail.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('加入年级后才可布置作业',{time:2000,icon:2});", true);
                    return;
                }
                if (new BLL_UserGroup_Member().GetRecordCount("UserStatus='0' and User_Id='" + FloginUser.UserId + "' and UserGroup_Id='" + ddlClass.SelectedValue + "' ") == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.ready(function(){layer.msg('老师与所布置作业班级关系错误',{time:2000,icon:2});});", true);
                    return;
                }

                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

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

                //Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业：验证网络是否通畅" + hostUrl);
                ////检测网络是否通畅
                //if (RemotWeb.PostDataToServer(hostUrl + "/AuthApi/index.aspx?key=onlinecheck", "", Encoding.UTF8, "Get") != "ok")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('服务器网络不通畅。',{icon:2,time:3000});</script>");
                //    return;
                //}
                #endregion

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题：1结束验证");

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题：2开始组织提交数据");

                #region 组织提交数据
                DateTime hw_time = DateTime.Now;
                string SubjectId = FloginUser.Subject;
                if (modelRTRF != null && modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                {
                    SubjectId = modelRTRF.Subject;
                }
                int intTimeLength = 0;
                int.TryParse(txtTimeLength.Text, out intTimeLength);
                string strIsCountdown = "0";
                if (chkIsCountdown.Checked) strIsCountdown = "1";
                object objAssign = new
                {
                    tqInfo = hidTQ.Value,
                    rtrfId_Old = rtrfId,
                    HomeWork_Id = strHomeWork_Id,
                    HomeWork_Name = txtHomeWork_Name.Text,
                    HomeWork_AssignTeacher = FloginUser.UserId,
                    BeginTime = DateTime.Now,
                    IsHide = 0,
                    HomeWork_Status = 0,
                    CreateTime = hw_time,
                    UserGroup_Id = ddlClass.SelectedValue,
                    isTimeLimt = 1,
                    isTimeLength = intTimeLength,
                    SubjectId = SubjectId,
                    IsShowAnswer = 1,
                    IsCountdown = strIsCountdown
                };
                #endregion

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题：2结束组织提交数据");

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题：3开始提交到学校服务器");

                hostUrl += "teacher/tchAssignTQ.aspx";
                string result = Rc.Common.RemotWeb.PostDataToServer(hostUrl, JsonConvert.SerializeObject(objAssign), System.Text.Encoding.UTF8, "POST");
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题：3结束提交到学校服务器");
                if (result == "ok")
                {
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题成功");
                    string strJ = "layer.msg('布置试题成功',{time:1000,icon:1},function(){window.parent.location.href='CommentCountdown.aspx?hwId=" + strHomeWork_Id + "';});";
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", strJ, true);
                }
                else
                {
                    //RevokeHW 布置作业失败 撤销作业
                    new BLL_HomeWork().RevokeHW(strHomeWork_Id);
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题失败：学校未布置成功");
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('布置试题失败',{time:2000,icon:2});", true);
                }

            }
            catch (Exception ex)
            {
                //RevokeHW 布置作业失败 撤销作业
                new BLL_HomeWork().RevokeHW(strHomeWork_Id);
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置试题失败" + ex.Message.ToString());
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('布置试题失败!',{ time: 2000,icon:2});</script>");
            }
        }

    }
}