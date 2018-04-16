using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.teacher
{
    public partial class TeacherJoinGrade : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUser_ApplicationReason.Text = string.Format("我是{0}：{1}", pfunction.GetCommon_DictName(FloginUser.UserPost), FloginUser.UserName);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Model_UserGroup_Member model = new Model_UserGroup_Member();
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                string userGroupId = txtUserGroup_Id.Text.Trim().Filter();
                if (new BLL_UserGroup().GetRecordCount("UserGroup_Id='" + userGroupId + "' and UserGroup_AttrEnum='" + UserGroup_AttrEnum.Grade.ToString() + "' ") == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.msg('年级号" + userGroupId + "不存在',{icon:2,time:2000});", true);
                    return;
                }
                List<Model_UserGroup_Member> listModelUGM = bll.GetModelList(string.Format("User_ID='{0}' and UserGroup_Id='{1}'", FloginUser.UserId, userGroupId));
                bool isExistData = false;//是否已存在成员数据
                if (listModelUGM.Count != 0)
                {
                    isExistData = true;
                    model = listModelUGM[0];
                    if (model.User_ApplicationStatus == "applied")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.msg('已经申请加入年级" + userGroupId + "，正在等待审核',{icon:2,time:2000});", true);
                        return;
                    }
                    if (model.User_ApplicationStatus == "passed" && model.UserStatus == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.msg('已经加入年级" + userGroupId + "，请更换年级号重新操作',{icon:2,time:2000});", true);
                        return;
                    }
                    else if (model.User_ApplicationStatus == "passed" && model.UserStatus == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.msg('不可加入此年级(退出)，请联系您的年级组长',{icon:2,time:2000});", true);
                        return;
                    }
                }
                if (!isExistData) model.UserGroup_Member_Id = Guid.NewGuid().ToString();
                model.UserGroup_Id = userGroupId;
                model.User_ID = FloginUser.UserId;
                model.User_ApplicationStatus = "applied";
                model.User_ApplicationTime = DateTime.Now;
                model.User_ApplicationReason = txtUser_ApplicationReason.Text.Trim().Filter();
                string strMembershipEnum = string.Empty;
                switch (FloginUser.UserPost)
                {
                    case "b159d237-197d-49ea-ac7b-c4df817a1d5b":
                        strMembershipEnum = MembershipEnum.gradedirector.ToString();
                        break;
                    case "adbd4fc0-24d0-418c-aade-e988f8aeabdd":
                        strMembershipEnum = MembershipEnum.GroupLeader.ToString();
                        break;
                }
                model.MembershipEnum = strMembershipEnum;
                model.CreateUser = FloginUser.UserId;
                bool flag = false;
                if (isExistData)
                {
                    flag = bll.Update(model);
                }
                else
                {
                    flag = bll.Add(model);
                }
                if (flag)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.msg('申请成功',{icon:1,time:1000},function(){parent.loadData();parent.layer.closeAll();})</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.msg('申请失败',{icon:2,time:2000})</script>");
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.msg('操作失败',{icon:2,time:2000})</script>");
            }
        }
    }
}