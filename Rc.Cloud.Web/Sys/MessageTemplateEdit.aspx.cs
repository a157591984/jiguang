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
using Rc.Common;

namespace Rc.Cloud.Web.Sys
{
    public partial class MessageTemplateEdit : Rc.Cloud.Web.Common.InitPage
    {
        string tId = string.Empty;
        BLL_SendMessageTemplate bll = new BLL_SendMessageTemplate();
        Model_SendMessageTemplate model = new Model_SendMessageTemplate();
        protected void Page_Load(object sender, EventArgs e)
        {
            tId = Request.QueryString["tId"].ToString().Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //类型
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.SMSPAYTemplateEnum)))
                {
                    ddlSType.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.SMSPAYTemplateEnum>(item.ToString()), item.ToString()));
                }
                if (!string.IsNullOrEmpty(tId))
                {
                    loadData();
                }
            }
        }

        /// <summary>
        /// 修改时的默认值
        /// </summary>
        protected void loadData()
        {
            model = bll.GetModel(tId);
            if (model != null)
            {
                ddlSType.SelectedValue = model.SType;
                txtUserId.Text = model.UserId;
                txtUserName.Text = model.UserName;
                txtPassword.Text = model.PassWord;
                txtContent.Text = model.Content;
                txtMobile.Text = model.Mobile;
                this.ddlMethod.SelectedValue = model.Method;
                this.txtUrl.Text = model.MsgUrl;
                if (model.IsStart == 1)
                {
                    rbtisLast0.Checked = false;
                    rbtisLast1.Checked = true;
                }
                else
                {
                    rbtisLast1.Checked = false;
                    rbtisLast0.Checked = true;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tId))
                {
                    #region 添加
                    //验证该类型是否已添加数据
                    if (bll.GetRecordCount("SType='" + ddlSType.SelectedValue + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('该类型数据已添加。', { time: 2000, icon: 4})</script>");
                        return;
                    }

                    model.SendSMSTemplateId = Guid.NewGuid().ToString();
                    model.SType = ddlSType.SelectedValue.Trim();
                    model.UserId = txtUserId.Text.Trim();
                    model.UserName = txtUserName.Text.Trim();
                    model.PassWord = txtPassword.Text.Trim();
                    model.Content = txtContent.Text.Trim();
                    model.IsStart = rbtisLast1.Checked ? 1 : 0;
                    model.MsgUrl = this.txtUrl.Text.TrimEnd();
                    model.Method = this.ddlMethod.SelectedValue;
                    model.CUser = loginUser.SysUser_ID;
                    model.CTime = DateTime.Now;
                    model.Mobile = txtMobile.Text.Trim();

                    bool boolAddRes = bll.Add(model);
                    if (boolAddRes)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    //验证该类型是否已添加数据
                    if (bll.GetRecordCount("SendSMSTemplateId!='" + tId + "' and SType='" + ddlSType.SelectedIndex + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('该类型数据已添加。', { time: 2000, icon: 4})</script>");
                        return;
                    }
                    model = bll.GetModel(tId);
                    model.SType = ddlSType.SelectedValue.Trim();
                    model.UserId = txtUserId.Text.Trim();
                    model.UserName = txtUserName.Text.Trim();
                    model.PassWord = txtPassword.Text.Trim();
                    model.Content = txtContent.Text.Trim();
                    model.CUser = loginUser.SysUser_ID;
                    model.CTime = DateTime.Now;
                    model.IsStart = rbtisLast1.Checked ? 1 : 0;
                    model.MsgUrl = this.txtUrl.Text.TrimEnd();
                    model.Method = this.ddlMethod.SelectedValue;
                    model.Mobile = txtMobile.Text.Trim();
                    bool boolUpdateRes = bll.Update(model);
                    if (boolUpdateRes)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2});</script>");
            }
        }
    }
}