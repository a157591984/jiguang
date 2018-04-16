using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.StrUtility;
using System.Data;

namespace Rc.Cloud.Web.teacher
{
    public partial class JoinClass : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUser_ApplicationReason.Text = string.Format("我是{0}", string.IsNullOrEmpty(FloginUser.TrueName) ? FloginUser.UserName : FloginUser.TrueName);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Model_UserGroup_Member model = new Model_UserGroup_Member();
                BLL_UserGroup_Member bll = new BLL_UserGroup_Member();
                string userGroupId = txtUserGroup_Id.Text.Trim().Filter();
                if (new BLL_UserGroup().GetRecordCount("UserGroup_Id='" + userGroupId + "' and UserGroup_AttrEnum='" + UserGroup_AttrEnum.Class.ToString() + "' ") == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.ready(function(){layer.msg('班级号(" + userGroupId + ")不存在',{icon:2,time:2000,offset:'10px'});})", true);
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
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.ready(function(){layer.msg('已发送过加入申请(" + userGroupId + ")<br>正在审核中',{icon:4,time:2000,offset:'10px'});})", true);
                        return;
                    }
                    if (model.User_ApplicationStatus == "passed" && model.UserStatus == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.ready(function(){layer.msg('已经加入班级(" + userGroupId + ")<br>请更换班级号重新操作',{icon:4,time:2000,offset:'10px'});})", true);
                        return;
                    }
                    else if (model.User_ApplicationStatus == "passed" && model.UserStatus == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Save", "layer.ready(function(){layer.msg('不可加入此班(退班)<br>请联系您的老师',{icon:4,time:2000,offset:'10px'});})", true);
                        return;
                    }
                }
                if (!isExistData) model.UserGroup_Member_Id = Guid.NewGuid().ToString();
                model.UserGroup_Id = userGroupId;
                model.User_ID = FloginUser.UserId;
                model.User_ApplicationStatus = "applied";
                model.User_ApplicationTime = DateTime.Now;
                model.User_ApplicationReason = txtUser_ApplicationReason.Text.Trim().Filter();
                model.MembershipEnum = MembershipEnum.teacher.ToString();
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
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('申请成功',{icon:1,time:1000,offset:'10px'},function(){parent.loadData();parent.layer.closeAll();})})</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('申请失败',{icon:2,time:2000,offset:'10px'})})</script>");
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>layer.ready(function(){layer.msg('操作失败',{icon:2,time:2000,offset:'10px'})})</script>");
            }
        }
    }
}