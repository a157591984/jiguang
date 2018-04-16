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
using Rc.Common.Config;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class AudioVideoEdit : Rc.Cloud.Web.Common.InitPage
    {
        protected string AudioVideoBookId = string.Empty;
        BLL_AudioVideoBook bll = new BLL_AudioVideoBook();
        Model_AudioVideoBook model = new Model_AudioVideoBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            AudioVideoBookId = Request.QueryString["AudioVideoBookId"].ToString().Filter();
            if (!IsPostBack)
            {
                BLL_SysUserTask bll_sysusertask = new BLL_SysUserTask();
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //if (loginUser.SysUser_ID == Consts.AdminID || loginUser.SysUser_ID == Consts.CAdminID)
                //{
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "--请选择--");
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlYear, years - 5, years + 1, true);
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "--请选择--");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--请选择--");
                //}
                //else
                //{
                //    ddlYear.Items.Clear();
                //    DataTable dtTask = bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='NF' order by TaskId ").Tables[0];
                //    ListItem item = null;
                //    item = new ListItem("--请选择--", "-1");//请选择
                //    ddlYear.Items.Add(item);
                //    for (int i = 0; i < dtTask.Rows.Count; i++)
                //    {
                //        ddlYear.Items.Add(new ListItem(dtTask.Rows[i]["TaskId"].ToString(), dtTask.Rows[i]["TaskId"].ToString()));
                //    }
                //    Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='NJXQ' order by TaskName").Tables[0], "TaskName", "TaskId", "--请选择--");
                //    Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='XK' order by TaskName").Tables[0], "TaskName", "TaskId", "--请选择--");
                //    Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='JCBB' order by TaskName").Tables[0], "TaskName", "TaskId", "--请选择--");
                //}
                if (!string.IsNullOrEmpty(AudioVideoBookId))
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
            model = bll.GetModel(AudioVideoBookId);
            if (model == null)
            {
                return;
            }
            else
            {
                ddlGradeTerm.SelectedValue = model.GradeTerm;
                ddlResource_Version.SelectedValue = model.Resource_Version;
                ddlSubject.SelectedValue = model.Subject;
                ddlYear.SelectedValue = model.ParticularYear.ToString();
                txtBookName.Text = model.BookName;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(AudioVideoBookId))
                {
                    #region 添加
                    model = new Model_AudioVideoBook();
                    model.BookName = txtBookName.Text.Trim();
                    model.GradeTerm = ddlGradeTerm.SelectedValue;
                    model.ParticularYear = Convert.ToInt32(ddlYear.SelectedValue);
                    model.Resource_Version = ddlResource_Version.SelectedValue;
                    model.Subject = ddlSubject.SelectedValue;
                    model.AudioVideoBookId = Guid.NewGuid().ToString();
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;

                    if (bll.Add(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "新增视频图书成功,操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('新增成功!',{ time: 1000,icon:1},function(){parent.loadData();parent.layer.close(index)});})})</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(AudioVideoBookId);
                    model.BookName = txtBookName.Text.Trim();
                    model.GradeTerm = ddlGradeTerm.SelectedValue;
                    model.ParticularYear = Convert.ToInt32(ddlYear.SelectedValue);
                    model.Resource_Version = ddlResource_Version.SelectedValue;
                    model.Subject = ddlSubject.SelectedValue;

                    if (bll.Update(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "修改视频图书成功,操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('编辑成功!',{ time: 1000,icon:1},function(){parent.loadData();parent.layer.close(index);});})})</script>");
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "操作异常," + ex + "操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败!',{icon:2},function(){parent.loadData();parent.layer.close(index);});})})</script>");
            }
        }
    }
}