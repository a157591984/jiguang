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
    public partial class ExamPointEdit : Rc.Cloud.Web.Common.InitPage
    {
        string kpId = string.Empty;
        string parentId = string.Empty;
        string kpId_Copy = string.Empty;
        public string GradeTerm = string.Empty;
        public string Subject = string.Empty;
        string Syllabus = string.Empty;
        string Exam_Type = string.Empty;
        BLL_S_TestingPoint bll = new BLL_S_TestingPoint();
        Model_S_TestingPoint model = new Model_S_TestingPoint();
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
                DataTable dtDict1 = new BLL_Common_Dict().GetList("D_Type=22 order by D_Order").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlCognitive_Level, dtDict1, "D_Name", "Common_Dict_Id", "--请选择--");

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
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(kpId))
            {
                dt = bll.GetDataForEdit(kpId_Copy).Tables[0];
            }
            else
            {
                dt = bll.GetDataForEdit(kpId).Tables[0];
            }
            if (dt.Rows.Count > 0)
            {
                ddlKPLevel.SelectedValue = dt.Rows[0]["TPLevel"].ToString();
                if (dt.Rows[0]["IsLast"].ToString() == "1")
                {
                    rbtIsLast0.Checked = false;
                    rbtIsLast1.Checked = true;
                }
                if (dt.Rows[0]["Importance"].ToString().TrimEnd() == "1")
                {
                    rbt0.Checked = true;
                    rbt1.Checked = false;
                }

                txtTPName.Text = dt.Rows[0]["TPName"].ToString();
                txtTPCode.Text = dt.Rows[0]["TPCode"].ToString();
                hidTPNameBasic_Id.Value = dt.Rows[0]["S_TestingPointBasic_Id"].ToString();
                hidTPNameBasic.Value = dt.Rows[0]["TPNameBasic"].ToString();
                ddlCognitive_Level.SelectedValue = dt.Rows[0]["Cognitive_Level"].ToString();
                GradeTerm = dt.Rows[0]["GradeTerm"].ToString();
                Subject = dt.Rows[0]["Subject"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                Model_S_TestingPoint model = new Model_S_TestingPoint();
                if (string.IsNullOrEmpty(kpId))
                {
                    #region 添加
                    #region 验证TPCode是否已存在
                    string strWhereCount = " TPCode='" + txtTPCode.Text.Trim()
                        + "' and GradeTerm='" + GradeTerm
                        + "' and Subject='" + Subject
                        + "' and Syllabus='" + Syllabus
                        + "' and Test_Category='" + Exam_Type + "'";
                    if (bll.GetRecordCount(strWhereCount) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('编码已存在。', { time: 2000, icon: 4})</script>");
                        return;
                    }
                    #endregion

                    if (rbtIsLast0.Checked || (rbtIsLast1.Checked && !string.IsNullOrEmpty(hidTPNameBasic_Id.Value)))
                    {
                        #region 不是最后一级 或 是最后一级且基本考点已存在
                        #region 考点数据表
                        model.S_TestingPoint_Id = Guid.NewGuid().ToString();
                        model.GradeTerm = GradeTerm;
                        model.Subject = Subject;
                        model.Syllabus = Syllabus;
                        model.Test_Category = Exam_Type;
                        model.Parent_Id = parentId;
                        model.IsLast = "0";
                        model.S_TestingPointBasic_Id = "";
                        model.TPName = txtTPName.Text.Trim();
                        if (rbtIsLast1.Checked)//是最后一级
                        {
                            model.IsLast = "1";
                            model.S_TestingPointBasic_Id = hidTPNameBasic_Id.Value;
                            model.TPName = "";
                        }
                        if (rbt0.Checked)
                        {
                            model.Importance = "1";
                        }
                        else
                        {
                            model.Importance = "0";
                        }
                        model.TPCode = txtTPCode.Text.Trim();
                        model.TPLevel = ddlKPLevel.SelectedValue;
                        model.Cognitive_Level = ddlCognitive_Level.SelectedValue;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                        flag = bll.Add(model);
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region 是最后一级 且 基本考点不存在
                        #region 基本考点表
                        Model_S_TestingPointBasic modelBasic = new Model_S_TestingPointBasic();
                        string S_TestingPointBasic_Id = Guid.NewGuid().ToString();
                        modelBasic.S_TestingPointBasic_Id = S_TestingPointBasic_Id;
                        modelBasic.GradeTerm = GradeTerm;
                        modelBasic.Subject = Subject;
                        modelBasic.TPNameBasic = hidTPNameBasic.Value.Trim();
                        modelBasic.CreateTime = DateTime.Now;
                        modelBasic.CreateUser = loginUser.SysUser_ID;
                        #endregion
                        #region 考点数据表
                        model.S_TestingPoint_Id = Guid.NewGuid().ToString();
                        model.GradeTerm = GradeTerm;
                        model.Subject = Subject;
                        model.Syllabus = Syllabus;
                        model.Test_Category = Exam_Type;
                        model.Parent_Id = parentId;
                        model.IsLast = "1";
                        if (rbt0.Checked)
                        {
                            model.Importance = "1";
                        }
                        else
                        {
                            model.Importance = "0";
                        }
                        model.S_TestingPointBasic_Id = S_TestingPointBasic_Id;
                        model.TPName = "";
                        model.TPCode = txtTPCode.Text.Trim();
                        model.TPLevel = ddlKPLevel.SelectedValue;
                        model.Cognitive_Level = ddlCognitive_Level.SelectedValue;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                        #endregion
                        flag = bll.AddBasic(model, modelBasic);
                        #endregion
                    }

                    if (flag)
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
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "新增同步考点");
                    #endregion
                }
                else
                {
                    #region 修改
                    if (rbtIsLast0.Checked || (rbtIsLast1.Checked && !string.IsNullOrEmpty(hidTPNameBasic_Id.Value)))
                    {
                        #region 不是最后一级 或 是最后一级且基本考点已存在
                        #region 考点数据表
                        model = bll.GetModel(kpId);
                        model.IsLast = "0";
                        model.S_TestingPointBasic_Id = "";
                        model.TPName = txtTPName.Text.Trim();
                        if (rbtIsLast1.Checked)//是最后一级
                        {
                            model.IsLast = "1";
                            model.S_TestingPointBasic_Id = hidTPNameBasic_Id.Value;
                            model.TPName = "";
                        }
                        if (rbt0.Checked)
                        {
                            model.Importance = "1";
                        }
                        else
                        {
                            model.Importance = "0";
                        }
                        model.TPCode = txtTPCode.Text.Trim();
                        model.TPLevel = ddlKPLevel.SelectedValue;
                        model.Cognitive_Level = ddlCognitive_Level.SelectedValue;
                        model.UpdateTime = DateTime.Now;
                        model.UpdateUser = loginUser.SysUser_ID;
                        flag = bll.Update(model);
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region 是最后一级 且 基本考点不存在
                        model = bll.GetModel(kpId);
                        #region 基本考点表
                        Model_S_TestingPointBasic modelBasic = new Model_S_TestingPointBasic();
                        string S_TestingPointBasic_Id = Guid.NewGuid().ToString();
                        modelBasic.S_TestingPointBasic_Id = S_TestingPointBasic_Id;
                        modelBasic.GradeTerm = model.GradeTerm;
                        modelBasic.Subject = model.Subject;
                        modelBasic.TPNameBasic = hidTPNameBasic.Value.Trim();
                        modelBasic.CreateTime = DateTime.Now;
                        modelBasic.CreateUser = loginUser.SysUser_ID;
                        #endregion
                        #region 考点数据表
                        model.IsLast = "1";
                        if (rbt0.Checked)
                        {
                            model.Importance = "1";
                        }
                        else
                        {
                            model.Importance = "0";
                        }
                        model.S_TestingPointBasic_Id = S_TestingPointBasic_Id;
                        model.TPName = "";
                        model.TPCode = txtTPCode.Text.Trim();
                        model.TPLevel = ddlKPLevel.SelectedValue;
                        model.Cognitive_Level = ddlCognitive_Level.SelectedValue;
                        model.UpdateTime = DateTime.Now;
                        model.UpdateUser = loginUser.SysUser_ID;
                        #endregion
                        flag = bll.UpdateBasic(model, modelBasic);
                        #endregion
                    }



                    if (flag)
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
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "修改同步考点");
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