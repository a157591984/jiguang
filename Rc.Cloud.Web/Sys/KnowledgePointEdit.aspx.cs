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
    public partial class KnowledgePointEdit : Rc.Cloud.Web.Common.InitPage
    {
        string kpId = string.Empty;
        string parentId = string.Empty;
        string kpId_Copy = string.Empty;
        protected string GradeTerm = string.Empty;
        protected string Subject = string.Empty;
        string Resource_Version = string.Empty;
        string Book_Type = string.Empty;
        BLL_S_KnowledgePoint bll = new BLL_S_KnowledgePoint();

        protected void Page_Load(object sender, EventArgs e)
        {
            kpId = Request["kpId"].Filter();
            parentId = Request["parentId"].Filter();
            kpId_Copy = Request["kpId_Copy"].Filter();
            GradeTerm = Request["GradeTerm"].Filter();
            Subject = Request["Subject"].Filter();
            Resource_Version = Request["Resource_Version"].Filter();
            Book_Type = Request["Book_Type"].Filter();
            if (!IsPostBack)
            {
                DataTable dtDict = new BLL_Common_Dict().GetList("D_Type=19 order by D_Order").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlKPLevel, dtDict, "D_Name", "Common_Dict_Id", false);
                dtDict = new BLL_Common_Dict().GetList("D_Type=23 order by D_Order").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlCognitive_Level, dtDict, "D_Name", "Common_Dict_Id", "请选择");
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
                ddlKPLevel.SelectedValue = dt.Rows[0]["KPLevel"].ToString();
                if (dt.Rows[0]["IsLast"].ToString() == "1")
                {
                    rbtIsLast0.Checked = false;
                    rbtIsLast1.Checked = true;
                }
                txtKPName.Text = dt.Rows[0]["KPName"].ToString();
                txtKPCode.Text = dt.Rows[0]["KPCode"].ToString();
                hidKPNameBasic_Id.Value = dt.Rows[0]["S_KnowledgePointBasic_Id"].ToString();
                hidKPNameBasic.Value = dt.Rows[0]["KPNameBasic"].ToString();
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
                Model_S_KnowledgePoint model = new Model_S_KnowledgePoint();
                if (string.IsNullOrEmpty(kpId))
                {
                    #region 添加
                    #region 验证KPCode是否已存在
                    string strWhereCount = " KPCode='" + txtKPCode.Text.Trim()
                        + "' and GradeTerm='" + GradeTerm
                        + "' and Subject='" + Subject
                        + "' and Resource_Version='" + Resource_Version
                        + "' and Book_Type='" + Book_Type + "'";
                    if (bll.GetRecordCount(strWhereCount) > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('编码已存在。', { time: 2000, icon: 4})</script>");
                        return;
                    }
                    #endregion

                    if (rbtIsLast0.Checked || (rbtIsLast1.Checked && !string.IsNullOrEmpty(hidKPNameBasic_Id.Value)))
                    {
                        #region 不是最后一级 或 是最后一级且基本知识点已存在
                        #region 知识点数据表
                        model.S_KnowledgePoint_Id = Guid.NewGuid().ToString();
                        model.GradeTerm = GradeTerm;
                        model.Subject = Subject;
                        model.Resource_Version = Resource_Version;
                        model.Book_Type = Book_Type;
                        model.Parent_Id = parentId;
                        model.IsLast = "0";
                        model.S_KnowledgePointBasic_Id = "";
                        model.KPName = txtKPName.Text.Trim();
                        if (rbtIsLast1.Checked)//是最后一级
                        {
                            model.IsLast = "1";
                            model.S_KnowledgePointBasic_Id = hidKPNameBasic_Id.Value;
                            model.KPName = "";
                        }
                        model.KPCode = txtKPCode.Text.Trim();
                        model.KPLevel = ddlKPLevel.SelectedValue;
                        model.Cognitive_Level = ddlCognitive_Level.SelectedValue;
                        model.CreateTime = DateTime.Now;
                        model.CreateUser = loginUser.SysUser_ID;
                        flag = bll.Add(model);
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region 是最后一级 且 基本知识点不存在
                        #region 基本知识点表
                        Model_S_KnowledgePointBasic modelBasic = new Model_S_KnowledgePointBasic();
                        string S_KnowledgePointBasic_Id = Guid.NewGuid().ToString();
                        modelBasic.S_KnowledgePointBasic_Id = S_KnowledgePointBasic_Id;
                        modelBasic.GradeTerm = GradeTerm;
                        modelBasic.Subject = Subject;
                        modelBasic.KPNameBasic = hidKPNameBasic.Value.Trim();
                        modelBasic.CreateTime = DateTime.Now;
                        modelBasic.CreateUser = loginUser.SysUser_ID;
                        #endregion
                        #region 知识点数据表
                        model.S_KnowledgePoint_Id = Guid.NewGuid().ToString();
                        model.GradeTerm = GradeTerm;
                        model.Subject = Subject;
                        model.Resource_Version = Resource_Version;
                        model.Book_Type = Book_Type;
                        model.Parent_Id = parentId;
                        model.IsLast = "1";
                        model.S_KnowledgePointBasic_Id = S_KnowledgePointBasic_Id;
                        model.KPName = "";
                        model.KPCode = txtKPCode.Text.Trim();
                        model.KPLevel = ddlKPLevel.SelectedValue;
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
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "新增同步知识点");
                    #endregion
                }
                else
                {
                    #region 修改
                    if (rbtIsLast0.Checked || (rbtIsLast1.Checked && !string.IsNullOrEmpty(hidKPNameBasic_Id.Value)))
                    {
                        #region 不是最后一级 或 是最后一级且基本知识点已存在
                        #region 知识点数据表
                        model = bll.GetModel(kpId);
                        model.IsLast = "0";
                        model.S_KnowledgePointBasic_Id = "";
                        model.KPName = txtKPName.Text.Trim();
                        if (rbtIsLast1.Checked)//是最后一级
                        {
                            model.IsLast = "1";
                            model.S_KnowledgePointBasic_Id = hidKPNameBasic_Id.Value;
                            model.KPName = "";
                        }
                        model.KPCode = txtKPCode.Text.Trim();
                        model.KPLevel = ddlKPLevel.SelectedValue;
                        model.Cognitive_Level = ddlCognitive_Level.SelectedValue;
                        model.UpdateTime = DateTime.Now;
                        model.UpdateUser = loginUser.SysUser_ID;
                        flag = bll.Update(model);
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region 是最后一级 且 基本知识点不存在
                        model = bll.GetModel(kpId);
                        #region 基本知识点表
                        Model_S_KnowledgePointBasic modelBasic = new Model_S_KnowledgePointBasic();
                        string S_KnowledgePointBasic_Id = Guid.NewGuid().ToString();
                        modelBasic.S_KnowledgePointBasic_Id = S_KnowledgePointBasic_Id;
                        modelBasic.GradeTerm = model.GradeTerm;
                        modelBasic.Subject = model.Subject;
                        modelBasic.KPNameBasic = hidKPNameBasic.Value.Trim();
                        modelBasic.CreateTime = DateTime.Now;
                        modelBasic.CreateUser = loginUser.SysUser_ID;
                        #endregion
                        #region 知识点数据表
                        model.IsLast = "1";
                        model.S_KnowledgePointBasic_Id = S_KnowledgePointBasic_Id;
                        model.KPName = "";
                        model.KPCode = txtKPCode.Text.Trim();
                        model.KPLevel = ddlKPLevel.SelectedValue;
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