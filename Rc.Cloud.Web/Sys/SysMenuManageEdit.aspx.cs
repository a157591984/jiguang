using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using Rc.Common.StrUtility;
namespace Rc.HospitalConfigManage.Web.Sys
{
    public partial class SysMenuManageEdit : Rc.Cloud.Web.Common.InitData
    {
        protected string actionType = ""; //1是类似新增，2是修改 ""是新增

        protected string module_ID = "";
        BLL_SysModule sysModuleBll = new BLL_SysModule();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["moduleID"]))
            {
                module_ID = Request["moduleID"].ToString().Trim();
            }
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                actionType = Request["action"].ToString().Trim();
            }

            if (!IsPostBack)
            {
                // DataTable dt = new BLL_SysCode().GetSysCodeList_TopAndOrder(0, " SysOrder", "DESC", "").Tables[0];
                // Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSysCode, dt, "SysName", "SysCode", "--请选择--");
                if (module_ID != null && module_ID != "")
                {
                    BindData(module_ID);

                }
            }
        }

        //绑定控件
        protected void BindData(string moduleID)
        {
            try
            {
                Model_SysModule model = sysModuleBll.GetSysModuleModelBySyscodeAndModuleID(moduleID);
                //如果是类似新增
                if (actionType == "1")
                {
                    txtMODULEID.Text = model.MODULEID;
                }
                else
                {
                    txtMODULEID.Text = model.MODULEID;
                    txtMODULEID.Enabled = false;
                }
                txtMODULENAME.Text = model.MODULENAME;
                txtPARENTID.Text = model.PARENTID;
                txtSLEVEL.Text = model.SLEVEL;
                txtURL.Text = model.URL;
                if (model.ISINTREE == "Y")
                {
                    rbtISINTREE1.Checked = true;
                }
                else
                {
                    rbtISINTREE0.Checked = true;
                }
                txtDepth.Text = model.Depth.ToString().Trim();

                if (model.isLast == 1)
                {
                    rbtisLast1.Checked = true;
                }
                else
                {
                    rbtisLast0.Checked = true;
                }
                txtDefaultOrder.Text = model.DefaultOrder.ToString().Trim();
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //绑定模块
        protected Model_SysModule InsertOrUpdate()
        {
            try
            {
                Model_SysModule model = new Model_SysModule();
                model.MODULEID = txtMODULEID.Text.Trim();
                model.MODULENAME = txtMODULENAME.Text.Trim();
                model.PARENTID = txtPARENTID.Text.Trim();
                model.SLEVEL = txtSLEVEL.Text.Trim();
                model.URL = txtURL.Text.Trim();
                if (rbtISINTREE1.Checked)
                {
                    model.ISINTREE = "Y";
                }
                else
                {
                    model.ISINTREE = "N";
                }
                if (!string.IsNullOrEmpty(txtDepth.Text.Trim()) && txtDepth.Text.Trim().IsInt())
                {
                    model.Depth = int.Parse(txtDepth.Text.Trim());
                }
                if (rbtisLast1.Checked)
                {
                    model.isLast = 1;
                }
                else
                {
                    model.isLast = 0;
                }

                if (txtDefaultOrder.Text.Trim() != "" && txtDefaultOrder.Text.Trim().IsInt())
                {
                    model.DefaultOrder = int.Parse(txtDefaultOrder.Text.Trim());
                }
                return model;
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {

                Model_SysModule model = InsertOrUpdate();
                if (module_ID != null && module_ID != "" && actionType == "2")
                {
                    if (sysModuleBll.UpdateSysModuleBySyscodeAndModuleID(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('编辑成功',{icon:1,time:1000},function(){parent.window.location.href=window.parent.pageUrl})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('修改失败',{icon:2})})})</script>");
                    }
                }
                else
                {
                    if (CheckExists(model, "1"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('此模块ID已存在',{icon:4})})})</script>");
                    }
                    else
                    {
                        if (sysModuleBll.AddSysModule(model))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作成功',{icon:1,time:1000},function(){parent.window.location.href=window.parent.pageUrl})})})</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('新增模块失败',{icon:2})})})</script>");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="type">1添加时，2修改时</param>
        /// <returns>已存在返回true</returns>
        private bool CheckExists(Model_SysModule model, string type)
        {
            try
            {
                return sysModuleBll.ExistsSysModule(model, type);
            }
            catch (Exception)
            {

                return true;
            }
        }
    }
}