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

namespace Rc.Cloud.Web.Sys
{
    public partial class MeasureTargetEdit : Rc.Cloud.Web.Common.InitPage
    {
        string mtId = string.Empty;
        string parentId = string.Empty;
        string mtId_Copy = string.Empty;
        string GradeTerm = string.Empty;
        string Subject = string.Empty;
        string Resource_Version = string.Empty;
        BLL_S_MeasureTarget bll = new BLL_S_MeasureTarget();
        Model_S_MeasureTarget model = new Model_S_MeasureTarget();
        protected void Page_Load(object sender, EventArgs e)
        {
            mtId = Request["mtId"].Filter();
            parentId = Request["parentId"].Filter();
            mtId_Copy = Request["mtId_Copy"].Filter();
            GradeTerm = Request["GradeTerm"].Filter();
            Subject = Request["Subject"].Filter();
            Resource_Version = Request["Resource_Version"].Filter();
            if (!IsPostBack)
            {
                DataTable dtDict = new BLL_Common_Dict().GetList("D_Type=21 order by D_Order").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlMTLevel, dtDict, "D_Name", "Common_Dict_Id", false);
                if (!string.IsNullOrEmpty(mtId) || !string.IsNullOrEmpty(mtId_Copy))
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
            if (string.IsNullOrEmpty(mtId))
            {
                model = bll.GetModel(mtId_Copy);
            }
            else
            {
                model = bll.GetModel(mtId);
            }
            if (model != null)
            {
                ddlMTLevel.SelectedValue = model.MTLevel;
                txtMTName.Text = model.MTName;
                txtMTCode.Text = model.MTCode;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(mtId))
                {
                    #region 添加
                    //验证MTCode是否已存在
                    string strWhereCount = " MTCode='" + txtMTCode.Text.Trim()
                        + "' and GradeTerm='" + GradeTerm
                        + "' and Subject='" + Subject
                        + "' and Resource_Version='" + Resource_Version + "'";
                    if (bll.GetRecordCount(strWhereCount) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('编码已存在。', { time: 2000, icon: 4})</script>");
                        return;
                    }
                    model.S_MeasureTarget_Id = Guid.NewGuid().ToString();
                    model.GradeTerm = GradeTerm;
                    model.Subject = Subject;
                    model.Resource_Version = Resource_Version;
                    model.Parent_Id = parentId;
                    model.MTName = txtMTName.Text.Trim();
                    model.MTCode = txtMTCode.Text.Trim();
                    model.MTLevel = ddlMTLevel.SelectedValue;
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    if (bll.Add(model))
                    {
                        if (parentId == "0")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadSubData2('0');parent.layer.close(index)});</script>");
                            return;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "新增测量目标");
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(mtId);
                    model.MTName = txtMTName.Text.Trim();
                    model.MTCode = txtMTCode.Text.Trim();
                    model.MTLevel = ddlMTLevel.SelectedValue;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUser = loginUser.SysUser_ID;
                    if (bll.Update(model))
                    {
                        if (parentId == "0")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadSubData2('0');parent.layer.close(index);});</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "修改测量目标");
                    #endregion
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('操作失败!',{ time: 2000,icon:2});});</script>");
            }
        }
    }
}