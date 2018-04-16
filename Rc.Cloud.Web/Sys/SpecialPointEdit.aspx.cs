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
    public partial class SpecialPointEdit : Rc.Cloud.Web.Common.InitPage
    {
        string kpId = string.Empty;
        string parentId = string.Empty;
        string kpId_Copy = string.Empty;
        string GradeTerm = string.Empty;
        string Subject = string.Empty;
        string Syllabus = string.Empty;
        string Exam_Type = string.Empty;
        BLL_S_KnowledgePoint bll = new BLL_S_KnowledgePoint();
        Model_S_KnowledgePoint model = new Model_S_KnowledgePoint();
        protected void Page_Load(object sender, EventArgs e)
        {
            kpId = Request["kpId"].Filter();
            parentId = Request["parentId"].Filter();
            kpId_Copy = Request["kpId_Copy"].Filter();
            GradeTerm = Request["GradeTerm"].Filter();
            Subject = Request["Subject"].Filter();
            Syllabus = Request["Syllabus"].Filter();
            Exam_Type = Request["Exam_Type"].Filter();
            if (!IsPostBack)
            {
                DataTable dtDict = new BLL_Common_Dict().GetList("D_Type=20 order by D_Order").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlKPLevel, dtDict, "D_Name", "Common_Dict_Id", false);
                if (!string.IsNullOrEmpty(kpId) || !string.IsNullOrEmpty(kpId_Copy))
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
            if (string.IsNullOrEmpty(kpId))
            {
                model = bll.GetModel(kpId_Copy);
            }
            else
            {
                model = bll.GetModel(kpId);
            }
            if (model != null)
            {
                ddlKPLevel.SelectedValue = model.KPLevel;
                txtKPName.Text = model.KPName;
                txtKPCode.Text = model.KPCode;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(kpId))
                {
                    #region 添加
                    //验证KPCode是否已存在
                    string strWhereCount = " KPCode='" + txtKPCode.Text.Trim()
                        + "' and GradeTerm='" + GradeTerm
                        + "' and Subject='" + Subject
                        + "' and Syllabus='" + Syllabus
                        + "' and Test_Category='" + Exam_Type
                        + "' and Data_Type='3'";
                    if (bll.GetRecordCount(strWhereCount) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('编码已存在。', { time: 2000, icon: 4})</script>");
                        return;
                    }
                    model.S_KnowledgePoint_Id = Guid.NewGuid().ToString();
                    model.Data_Type = "3";
                    model.GradeTerm = GradeTerm;
                    model.Subject = Subject;
                    model.Syllabus = Syllabus;
                    model.Test_Category = Exam_Type;
                    model.Parent_Id = parentId;
                    model.KPName = txtKPName.Text.Trim();
                    model.KPCode = txtKPCode.Text.Trim();
                    model.KPLevel = ddlKPLevel.SelectedValue;
                    model.Importance = hidImportance.Value;
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
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "新增同步知识点");
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(kpId);
                    model.Data_Type = "3";
                    model.KPName = txtKPName.Text.Trim();
                    model.KPCode = txtKPCode.Text.Trim();
                    model.KPLevel = ddlKPLevel.SelectedValue;
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
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "修改同步知识点");
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