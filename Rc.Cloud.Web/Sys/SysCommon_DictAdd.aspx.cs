using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;
using System.Text;
using System.Data;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysCommon_DictAdd : Rc.Cloud.Web.Common.InitPage
    {
        static string sysCommon_Dict_ID;
        Common_DictBLL BLL = new Common_DictBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90300100";
            SetActionType();
            if (!IsPostBack)
            {
                DataTable ddlDType = new DataTable();
                StringBuilder condition = new StringBuilder();
                if (loginUser.SysUser_ID != Rc.Common.Config.Consts.AdminID)
                {
                    //非级管理员按类型排除加载数据（教材版本，年级学期，学科，前端题型）
                    condition.AppendFormat("  Common_Dict_ID in ('{0}','{1}','{2}','{3}')"
                       , "74958B74-D2A4-4ACD-BB4E-F48C59329F40"
                       , "722CE025-A876-4880-AAC1-5E416F3BDB1E"
                       , "934A3541-116E-438C-B9BA-4176368FCD9B"
                       , "3EF9506E-4C4B-407E-AA5D-451E0B20F0DI");
                }
                else
                {
                    condition.AppendFormat(" D_Type='{0}'", 0);
                }
                ddlDType = new Rc.BLL.Resources.BLL_Common_Dict().GetList(condition.ToString()).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlD_Type, ddlDType, "D_Name", "D_Value", false);

                //如果是修改，则绑定页面信息
                BindSysCommon_Dict();

            }
        }

        //设置页面命令类型
        private void SetActionType()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                sysCommon_Dict_ID = clsUtility.Decrypt(Request.QueryString["id"]);
            }
            else
            {
                sysCommon_Dict_ID = "";
            }
        }

        //绑定页面信息
        private void BindSysCommon_Dict()
        {
            if (!string.IsNullOrEmpty(sysCommon_Dict_ID))
            {
                try
                {
                    //获取数据
                    var sysCommon_DictInfo = GetSysCommon_Dict();

                    //绑定控件
                    if (sysCommon_DictInfo != null)
                    {
                        txtD_Name.Text = sysCommon_DictInfo.D_Name;
                        txtD_Value.Text = sysCommon_DictInfo.D_Value.ToString();
                        //txtD_Code.Text = sysCommon_DictInfo.D_Code;
                        txtD_Order.Text = sysCommon_DictInfo.D_Order.ToString();
                        ddlD_Type.SelectedValue = sysCommon_DictInfo.D_Type.ToString();
                        txtRemark.Text = sysCommon_DictInfo.D_Remark;
                        //txtD_Name.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    // //MS.Authority.clsAuth.AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                    throw ex;
                }

            }
        }

        //获取模板信息
        public Common_DictModel GetSysCommon_Dict()
        {
            return BLL.GetCommon_DictModelByPK(sysCommon_Dict_ID);
        }

        //新增
        private void Add()
        {
            if (!CheckInput())
                return;

            try
            {
                //从页面上取值
                var samplingTemp = GetSysCodeFromPage(1);
                if (!SysCommon_DictExists(samplingTemp))
                {
                    var res = BLL.Add(samplingTemp);
                    string strContent = string.Empty;

                    ShowMsg(res > 0, "保存成功");
                }
                else
                {
                    ShowMsg(false, "此名称已存在");
                }

            }
            catch (Exception ex)
            {
                ////MS.Authority.clsAuth.AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //修改
        private void Edit()
        {
            if (!CheckInput())
                return;

            //修改
            try
            {
                //从页面上取值
                var sysCodeTemp = GetSysCodeFromPage(2);
                if (sysCodeTemp == null)
                    //提示消息：不存在模板
                    ShowMsg(false, "不存在模板");

                else
                {
                    if (!Exists(sysCodeTemp))
                    {
                        var res = BLL.Update(sysCodeTemp);
                        string strContent = string.Empty;
                        ShowMsg(res > 0, "修改成功");
                    }
                    else
                    {
                        ShowMsg(false, "此名称已存在");
                    }
                }
            }
            catch (Exception ex)
            {
                ////MS.Authority.clsAuth.AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //从页面上获取模板数据
        private Common_DictModel GetSysCodeFromPage(int i)
        {
            Common_DictModel Common_DictTemp = null;
            if (i == 1)
            {
                Common_DictTemp = new Common_DictModel();
                Common_DictTemp.Common_Dict_ID = Guid.NewGuid().ToString();
                Common_DictTemp.D_CreateUser = loginUser.SysUser_ID;
                Common_DictTemp.D_CreateTime = DateTime.Now;
            }
            else
            {
                Common_DictTemp = new Common_DictModel();
                Common_DictTemp.Common_Dict_ID = sysCommon_Dict_ID;
                Common_DictTemp.D_ModifyUser = loginUser.SysUser_ID;
                Common_DictTemp.D_ModifyTime = DateTime.Now;
            }


            Common_DictTemp.D_Name = txtD_Name.Text.Trim();
            if (!string.IsNullOrEmpty(txtD_Value.Text.Trim()))
            {
                Common_DictTemp.D_Value = int.Parse(txtD_Value.Text.Trim());
            }
            //Common_DictTemp.D_Code = txtD_Code.Text.Trim();
            if (!string.IsNullOrEmpty(txtD_Order.Text.Trim()))
            {
                Common_DictTemp.D_Order = int.Parse(txtD_Order.Text.Trim());
            }
            if (!string.IsNullOrEmpty(ddlD_Type.SelectedValue.Trim()))
            {
                Common_DictTemp.D_Type = int.Parse(ddlD_Type.SelectedValue.Trim());
            }
            Common_DictTemp.D_Remark = txtRemark.Text.Trim();
            return Common_DictTemp;
        }

        //保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sysCommon_Dict_ID))
            {
                Add();
            }
            else
            {
                Edit();
            }

        }

        //页面输入验证
        private bool CheckInput()
        {
            if (txtD_Name.Text.Trim().Length == 0)
            {
                //弹出提示
                ShowAlert("请输入名称！");
                return false;
            }
            return true;
        }

        private void ShowAlert(string AValue)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");
            scriptStr.AppendFormat("$(function(){layer.ready(function(){layer.msg('" + AValue + "')})})");
            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="resutl">成功/失败</param>
        /// <param name="url">成功后跳转的连接</param>
        /// <param name="errorMsg">错误后提示的内容</param>
        private void ShowMsg(bool resutl, string errorMsg)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");
            scriptStr.Append("$(function () {");
            scriptStr.Append("layer.ready(function () {");
            if (resutl)
            {
                scriptStr.Append("layer.msg('" + errorMsg + "',{icon:1,time:1000}, function () {parent.window.location.href=window.parent.pageUrl});");
            }
            else
            {
                scriptStr.Append("layer.msg('" + errorMsg + "',{icon:1});");
            }
            scriptStr.Append("});");
            scriptStr.Append("})");
            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());
            //var scriptStr = new StringBuilder();
            //scriptStr.Append("<script type='text/javascript'>");
            //if (resutl)
            //    scriptStr.Append("layer.msg('" + errorMsg + "',{icon:1})");
            //else
            //    scriptStr.Append("layer.msg('" + errorMsg + "',{icon:2})");

            //scriptStr.Append("</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());

        }

        //验证添加是否存在
        private bool SysCommon_DictExists(Common_DictModel model)
        {
            return BLL.SysCommon_DictExists(model);
        }

        //验证编辑是否存在
        private bool Exists(Common_DictModel model)
        {
            return BLL.Exists(model);
        }

    }
}