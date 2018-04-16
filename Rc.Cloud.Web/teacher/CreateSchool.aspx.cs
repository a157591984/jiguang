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
    public partial class CreateSchool : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('无可用学校号',{icon:4,time:2000,offset:'10px'})})</script>");
                    return;
                }
                userGroup.User_ID = FloginUser.UserId;
                if (pfunction.FilterKeyWords(this.txtClassName.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('学校名称存在敏感词汇，请重新填写',{icon:4,time:2000,offset:'10px'})})</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_Name = this.txtClassName.Text.Trim();
                }
                userGroup.UserGroupp_Type = "8842F17C-E3F7-4B65-BBF1-FDD950328CB1";//群类型，班
                if (pfunction.FilterKeyWords(this.txtClassIntro.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('学校简介存在敏感词汇，请重新填写',{icon:4,time:2000,offset:'10px'})})</script>");
                    return;
                }
                else
                {
                    userGroup.UserGroup_BriefIntroduction = this.txtClassIntro.Text.Trim();
                }

                userGroup.CreateTime = DateTime.Now;
                userGroup.UserGroup_AttrEnum = UserGroup_AttrEnum.School.ToString();

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
                modelUGM.MembershipEnum = MembershipEnum.principal.ToString();

                if (new BLL_UserGroup().AddGroupUpClassPoolAddMember(userGroup, modelClassPool, modelUGM))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('创建成功',{icon:1,time:1000,offset:'10px'},function(){parent.window.location.reload()})})</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('创建失败',{icon:2,time:2000,offset:'10px'})})</script>");
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('未知错误',{icon:2,time:2000,offset:'10px'})})</script>");
            }
        }

    }
}