using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.teacher
{
    public partial class CreateGrade : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //年级类型
                strWhere = " D_Type='9' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeType, dt, "D_Name", "Common_Dict_ID", "-请选择-");
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlStartSchoolYear, years - 5, years + 1, false);
                ddlStartSchoolYear.SelectedValue = years.ToString();
            }
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Model_UserGroup userGroup = new Model_UserGroup();
                userGroup.UserGroup_Id = Rc.Cloud.Web.Common.pfunction.GetNewUserGroupID();
                if (string.IsNullOrEmpty(userGroup.UserGroup_Id))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('无可用年级号',{icon:4,time:2000})})</script>");
                    return;
                }
                userGroup.User_ID = FloginUser.UserId;
                if (pfunction.FilterKeyWords(this.txtClassName.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('年级名称存在敏感词汇，请重新填写',{icon:2,time:2000})});</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_Name = this.txtClassName.Text.Trim();
                }
                userGroup.GradeType = this.ddlGradeType.SelectedValue;
                userGroup.Grade = Convert.ToDecimal(this.HidGrade.Value);
                userGroup.StartSchoolYear = Convert.ToDecimal(this.ddlStartSchoolYear.SelectedValue);
                userGroup.UserGroupp_Type = "8842F17C-E3F7-4B65-BBF1-FDD950328CB1";//群类型，班
                if (pfunction.FilterKeyWords(this.txtClassIntro.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('年级简介存在敏感词汇，请重新填写',{icon:2,time:2000})});</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_BriefIntroduction = this.txtClassIntro.Text.Trim();
                }
                userGroup.CreateTime = DateTime.Now;
                userGroup.UserGroup_AttrEnum = UserGroup_AttrEnum.Grade.ToString();
                int intOrder = 1;
                int.TryParse(txtSort.Text, out intOrder);
                userGroup.UserGroupOrder = intOrder;

                Model_ClassPool modelClassPool = new BLL_ClassPool().GetModelByClass_Id(userGroup.UserGroup_Id);
                modelClassPool.IsUsed = 1;

                Model_UserGroup_Member modelUGM = new Model_UserGroup_Member();
                modelUGM.UserGroup_Member_Id = Guid.NewGuid().ToString();
                modelUGM.UserGroup_Id = userGroup.UserGroup_Id;
                modelUGM.User_ID = FloginUser.UserId;
                modelUGM.User_ApplicationStatus = "passed";
                modelUGM.User_ApplicationTime = DateTime.Now;
                modelUGM.User_ApplicationReason = "";
                modelUGM.User_ApplicationPassTime = DateTime.Now;
                modelUGM.UserStatus = 0;
                modelUGM.MembershipEnum = MembershipEnum.gradedirector.ToString();

                if (new BLL_UserGroup().AddGroupUpClassPoolAddMember(userGroup, modelClassPool, modelUGM))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('创建成功',{icon:1,time:1000},function(){parent.window.location.reload()})})</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('创建失败',{icon:2,time:2000})})</script>");
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('未知错误',{icon:2,time:2000})})</script>");
            }
        }

    }
}