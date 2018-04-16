using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Common;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class Person_Edit : Rc.Cloud.Web.Common.InitPage
    {
        protected string SchoolSMS_Person_Id = string.Empty;
        protected string School_Id = string.Empty;
        Model_SchoolSMS_Person model = new Model_SchoolSMS_Person();
        BLL_SchoolSMS_Person bll = new BLL_SchoolSMS_Person();
        protected void Page_Load(object sender, EventArgs e)
        {
            School_Id = Request["School_Id"].Filter();
            SchoolSMS_Person_Id = Request["SchoolSMS_Person_Id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SchoolSMS_Person_Id))
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
            model = bll.GetModel(SchoolSMS_Person_Id);
            if (model == null)
            {
                return;
            }
            else
            {
                txtName.Text = model.Name;
                txtJob.Text = model.Job;
                txtCompany.Text = model.Company;
                txtPhoneNum.Text = model.PhoneNum;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SchoolSMS_Person_Id))
                {
                    #region 添加
                    model = new Model_SchoolSMS_Person();
                    model.SchoolSMS_Person_Id = Guid.NewGuid().ToString();
                    model.Name = txtName.Text.TrimEnd();
                    model.Job = txtJob.Text.TrimEnd();
                    model.PhoneNum = txtPhoneNum.Text.TrimEnd();
                    model.Company = txtCompany.Text.TrimEnd();
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    model.School_Id = School_Id;
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('新增成功!',{ time: 2000,icon:1},function(){window.parent.loadData();window.parent.layer.closeAll()})});</script>");

                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(SchoolSMS_Person_Id);
                    model.Name = txtName.Text.TrimEnd();
                    model.Job = txtJob.Text.TrimEnd();
                    model.PhoneNum = txtPhoneNum.Text.TrimEnd();
                    model.Company = txtCompany.Text.TrimEnd();
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.ready(function(){layer.msg('修改成功!',{ time: 2000,icon:1},function(){window.parent.loadData();window.parent.layer.closeAll()})});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2}</script>");
            }
        }
    }
}