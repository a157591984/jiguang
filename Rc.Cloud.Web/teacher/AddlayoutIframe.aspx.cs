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

namespace Homework.teacher
{
    public partial class AddlayoutIframe : Rc.Cloud.Web.Common.FInitData
    {
        string rtrId = string.Empty;
        string classId = string.Empty;
        string HomeWork_Id = string.Empty;
        protected string className = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            rtrId = Request.QueryString["rtrId"].Filter();
            classId = Request.QueryString["classId"].Filter();
            HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
            if (!IsPostBack)
            {
                txtStopTime.Attributes.Add("readonly", "true");
                LoadData();
            }
        }

        private void LoadData()
        {
            Model_HomeWork hwmodel = new Model_HomeWork();
            BLL_HomeWork hwbll = new BLL_HomeWork();
            hwmodel = hwbll.GetModel(HomeWork_Id);
            this.txtStopTime.Text = pfunction.ConvertToLongerDateTime(hwmodel.StopTime.ToString());
            //this.txtBeginTime.Text = pfunction.ConvertToLongerDateTime(hwmodel.BeginTime.ToString());
            string txtTimeLength = hwmodel.isTimeLength.ToString();

            string strWhere = string.Format(" User_ApplicationStatus='passed' and UserStatus='0' and UserGroup_Id='{0}' and MembershipEnum='{1}' ", classId, MembershipEnum.student);
            rptStudentList.DataSource = new BLL_UserGroup_Member().GetClassMemberListEX(strWhere, HomeWork_Id, "HomeWork_Id,TrueName").Tables[0];
            rptStudentList.DataBind();


            Model_UserGroup modelUserGroup = new BLL_UserGroup().GetModel(classId);
            if (modelUserGroup != null)
            {
                className = modelUserGroup.UserGroup_Name;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Model_HomeWork model = new Model_HomeWork();
                BLL_HomeWork bll = new BLL_HomeWork();

                #region 布置作业表
                string guid = HomeWork_Id;
                model = bll.GetModel(guid);
                model.StopTime = Convert.ToDateTime(txtStopTime.Text);
                #endregion

                #region 学生作业表
                List<Model_Student_HomeWork> listSHomwWork = new List<Model_Student_HomeWork>();
                List<Model_Student_HomeWork_Submit> listSHomwWorkSubmit = new List<Model_Student_HomeWork_Submit>();
                List<Model_Student_HomeWork_Correct> listSHomwWorkCorrect = new List<Model_Student_HomeWork_Correct>();
                string strStudent = Rc.Cloud.Web.Common.pfunction.CheckImp(hidStudentId.Value);
                strStudent = strStudent.TrimEnd(',');
                string[] strArrStudent = strStudent.Split(',');
                for (int i = 0; i < strArrStudent.Length; i++)
                {
                    string ShwGuid = Guid.NewGuid().ToString();
                    Model_Student_HomeWork modelSHomeWork = new Model_Student_HomeWork();
                    modelSHomeWork.Student_HomeWork_Id = ShwGuid;
                    modelSHomeWork.HomeWork_Id = guid;
                    modelSHomeWork.Student_Id = strArrStudent[i];
                    modelSHomeWork.CreateTime = DateTime.Now;
                    listSHomwWork.Add(modelSHomeWork);
                    #region 作业提交状态
                    Model_Student_HomeWork_Submit modelSHomeWorkSubmit = new Model_Student_HomeWork_Submit();
                    modelSHomeWorkSubmit.Student_HomeWork_Id = ShwGuid;
                    modelSHomeWorkSubmit.Student_HomeWork_Status = 0;
                    listSHomwWorkSubmit.Add(modelSHomeWorkSubmit);
                    #endregion
                    #region 作业批改状态
                    Model_Student_HomeWork_Correct modelSHomeWorkCorrect = new Model_Student_HomeWork_Correct();
                    modelSHomeWorkCorrect.Student_HomeWork_Id = ShwGuid;
                    modelSHomeWorkCorrect.Student_HomeWork_CorrectStatus = 0;
                    listSHomwWorkCorrect.Add(modelSHomeWorkCorrect);
                    #endregion
                }
                #endregion

                #region 统计帮助表
                DataTable dtHWDetail = bll.GetHWDetail(HomeWork_Id).Tables[0];
                Model_StatsHelper modelSH_HW = new Model_StatsHelper();
                modelSH_HW.ResourceToResourceFolder_Id = model.ResourceToResourceFolder_Id;
                modelSH_HW.Homework_Id = model.HomeWork_Id;
                modelSH_HW.SchoolId = dtHWDetail.Rows[0]["SchoolId"].ToString();
                modelSH_HW.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();
                #endregion

                if (bll.UpdateHomework(model, listSHomwWork, listSHomwWorkSubmit, listSHomwWorkCorrect, modelSH_HW))
                {
                    string strJ = "layer.msg('布置作业成功',{time:1000,icon:1},function(){";
                    if (Request.QueryString["tp"] != "1") { strJ += "window.parent.loadData();"; }
                    else { strJ += "window.parent.location.reload();"; }
                    strJ += "parent.layer.close(parent.layer.getFrameIndex(window.name));});";
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", strJ, true);
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(FloginUser.UserId, "", "布置作业成功");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('布置作业失败',{time:2000,icon:2});", true);
                    Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(FloginUser.UserId, "", "布置作业失败");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('布置作业失败',{time:2000,icon:2});", true);
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(FloginUser.UserId, "", "布置作业失败：" + ex.Message.ToString());
            }
        }

    }
}