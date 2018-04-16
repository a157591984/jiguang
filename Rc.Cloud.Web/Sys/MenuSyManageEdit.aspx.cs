using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using System.Text.RegularExpressions;

namespace Rc.Cloud.Web.Sys
{
    public partial class MenuSyManageEdit : Rc.Cloud.Web.Common.InitData
    {
        protected string actionType = ""; //1是类似新增，2是修改 ""是新增

        protected string module_ID = "";
        protected string sysCode = "";
        BLL_SysCode sysModuleBll = new BLL_SysCode();

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
            if (!string.IsNullOrEmpty(Request["sysCode"]))
            {
                sysCode = Request["sysCode"].ToString().Trim();
            }
            if (!IsPostBack)
            {
                DataTable dt = new BLL_SysCode().GetSysName().Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSysCode, dt, "SysName", "SysCode", "");
                if (module_ID != null && module_ID != "" && sysCode != null && sysCode != "")
                {
                    BindData(module_ID, sysCode);

                }
            }
        }

        //绑定控件
        protected void BindData(string moduleID, string sysCode)
        {
            try
            {
                Model_SysModule model = sysModuleBll.GetSysModuleModelBySyscodeAndModuleID(sysCode, moduleID);
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
                ddlSysCode.SelectedValue = model.SYSCODE;
                //修改时
                if (actionType == "2")
                {
                    ddlSysCode.Enabled = false;
                }
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
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(loginUser.SysUser_ID, Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
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
                if (!string.IsNullOrEmpty(txtDepth.Text.Trim()) &&IsInt(txtDepth.Text.Trim()))
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
                if (ddlSysCode.SelectedValue.Trim() != null && ddlSysCode.SelectedValue.Trim() != "" && ddlSysCode.SelectedValue.Trim() != "-1")
                {
                    model.SYSCODE = ddlSysCode.SelectedValue.Trim();
                }
                if (txtDefaultOrder.Text.Trim() != "" && IsInt(txtDefaultOrder.Text.Trim()))
                {
                    model.DefaultOrder = int.Parse(txtDefaultOrder.Text.Trim());
                }
                return model;
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(loginUser.SysUser_ID, Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInput();
                Model_SysModule model = InsertOrUpdate();
                if (module_ID != null && module_ID != "" && actionType == "2")
                {
                    if (sysModuleBll.UpdateSysModuleBySyscodeAndModuleID(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.Handel('操作成功', '');</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.showTipsErr('修改模块失败！', '4');</script>");
                    }
                }
                else
                {
                    if (CheckExists(model, "1"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.showTipsErr('此模块ID已存在', '4');</script>");
                    }
                    else
                    {
                        if (sysModuleBll.AddSysModule(model))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.Handel('操作成功', '');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.showTipsErr('新增模块失败！', '4');</script>");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(loginUser.SysUser_ID, Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
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

        //页面输入验证
        private bool CheckInput()
        {
            var sysName = ddlSysCode.SelectedValue.Trim();

            if (string.IsNullOrEmpty(sysName) || sysName == "-1")
            {
                //弹出提示
                ShowMsg(false, null, "请选择系统名称！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="resutl">成功/失败</param>
        /// <param name="url">成功后跳转的连接</param>
        /// <param name="errorMsg">错误后提示的内容</param>
        private void ShowMsg(bool resutl, string url, string errorMsg)
        {
            string js;
            if (resutl)
                js = string.Format("ShowSuccess('{0}')", url);
            else
                js = string.Format("ShowError('{0}')", errorMsg);

            ClientScript.RegisterStartupScript(this.GetType(), "showMsg", string.Format("<script type=\"text/javascript\">{0}</script>", js));
        }

        private bool IsInt(string Input)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                return IsNotInteger(Input, false);
            }
        }

        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Plus">正整数</param>
        /// <returns></returns>
        private bool IsNotInteger(string Input, bool Plus)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus)
                    pattern = "^[0-9]+$";
                if (Regex.Match(Input, pattern, RegexOptions.Compiled).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}