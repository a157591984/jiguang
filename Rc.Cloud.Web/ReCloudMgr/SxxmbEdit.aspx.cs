using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SxxmbEdit : Rc.Cloud.Web.Common.InitPage
    {
        public string Two_WayChecklist_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].Filter();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "--教材版本--");
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlYear, years - 5, years + 1, true, "入学年份");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "--年级学期--");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--学科--");
                if (!string.IsNullOrEmpty(Two_WayChecklist_Id))
                {
                    LoadData();
                }
            }
        }

        public void LoadData()
        {
            try
            {
                Model_Two_WayChecklist model = new Model_Two_WayChecklist();
                BLL_Two_WayChecklist bll = new BLL_Two_WayChecklist();
                model = bll.GetModel(Two_WayChecklist_Id);
                if (model != null)
                {
                    ddlYear.SelectedValue = model.ParticularYear.ToString();
                    ddlGradeTerm.SelectedValue = model.GradeTerm;
                    ddlResource_Version.SelectedValue = model.Resource_Version;
                    ddlSubject.SelectedValue = model.Subject;
                    txtRemark.Text = model.Remark;
                    txtTwo_WayChecklist_Name.Text = model.Two_WayChecklist_Name;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Two_WayChecklist_Id))
                {

                    Model_Two_WayChecklist model = new Model_Two_WayChecklist();
                    BLL_Two_WayChecklist bll = new BLL_Two_WayChecklist();
                    model = bll.GetModel(Two_WayChecklist_Id);
                    model.ParticularYear = Convert.ToInt32(ddlYear.SelectedValue);
                    model.GradeTerm = ddlGradeTerm.SelectedValue;
                    model.Resource_Version = ddlResource_Version.SelectedValue;
                    model.Subject = ddlSubject.SelectedValue;
                    model.Two_WayChecklist_Name = txtTwo_WayChecklist_Name.Text.TrimEnd();
                    model.Remark = txtRemark.Text.TrimEnd();
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                }
                else
                {
                    Model_Two_WayChecklist model = new Model_Two_WayChecklist();
                    BLL_Two_WayChecklist bll = new BLL_Two_WayChecklist();
                    model.Two_WayChecklist_Id = Guid.NewGuid().ToString();
                    model.ParticularYear = Convert.ToInt32(ddlYear.SelectedValue);
                    model.GradeTerm = ddlGradeTerm.SelectedValue;
                    model.Resource_Version = ddlResource_Version.SelectedValue;
                    model.Subject = ddlSubject.SelectedValue;
                    model.Two_WayChecklist_Name = txtTwo_WayChecklist_Name.Text.TrimEnd();
                    model.Remark = txtRemark.Text.TrimEnd();
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;
                    if (bll.Add(model))
                    { ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>"); }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}