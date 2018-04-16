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
using System.Text;

namespace Rc.Cloud.Web.Sys
{
    public partial class PersonalInformation : Rc.Cloud.Web.Common.InitPage
    {
        private static string sysUser_ID;
        private static string sysUser_PassWord;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90104000";
            if (!IsPostBack)
            {
                sysUser_ID = loginUser.SysUser_ID;
                Band();
            }
        }

        //界面赋值
        private void Band()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new SysUserBLL().GetSysUserInfo(sysUser_ID).Tables[0];
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sysUser_ID = dt.Rows[i]["SysUser_ID"].ToString();
                        sysUser_PassWord = dt.Rows[i]["SysUser_PassWord"].ToString();
                        lblNamelogin.Text = dt.Rows[i]["SysUser_LoginName"].ToString();
                        //txtpwdlogin.Text = (dt.Rows[i]["SysUser_PassWord"] == null || dt.Rows[i]["SysUser_PassWord"] == DBNull.Value || dt.Rows[i]["SysUser_PassWord"].ToString().Trim() == "") ? "" : PHHC.Share.StrUtility.DESEncryptLogin.DecryptString(dt.Rows[i]["SysUser_PassWord"].ToString());
                        txtSysUserName.Text = dt.Rows[i]["SysUser_Name"].ToString();
                        lblDepartmentName.Text = dt.Rows[i]["SysDepartment_Name"].ToString();
                        //if (dt.Rows[i]["SysUser_Enable"].ToString() != "1")
                        //{
                        //    ckbIsEnable.Checked = true;
                        //}
                        //else
                        //{
                        //    ckbIsEnable.Checked = false;
                        //}                      
                        txtTel.Text = dt.Rows[i]["SysUser_Tel"].ToString();
                        txtContent.Text = dt.Rows[i]["SysUser_Remark"].ToString();                    
                    }
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //获取界面参数
        private Model_SysUser GetSysUserModel()
        {
            try
            {
                Model_SysUser usermodel = new Model_SysUser();

                usermodel.SysUser_ID = sysUser_ID;
                usermodel.SysUser_Name = txtSysUserName.Text.Trim();
                usermodel.SysUser_LoginName = lblNamelogin.Text.Trim();
                //if (string.IsNullOrEmpty(txtpwdlogin.Text.Trim()))
                //{
                //    usermodel.SysUser_PassWord = sysUser_PassWord;
                //}
                //else
                //{
                //    usermodel.SysUser_PassWord = DESEncryptLogin.EncryptString(txtpwdlogin.Text.Trim());
                //}
                usermodel.SysUser_Tel = txtTel.Text.Trim();
                //if (ckbIsEnable.Checked)
                //{
                //    usermodel.SysUser_Enable = true;
                //}
                //else
                //{
                //    usermodel.SysUser_Enable = false;
                //}
                usermodel.SysUser_Remark = txtContent.Text.Trim();
                usermodel.UpdateTime = DateTime.Now;
                usermodel.UpdateUser = loginUser.SysUser_ID;

                return usermodel;
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    Model_SysUser usermodel = new Model_SysUser();
                    bool result = false;
                    usermodel = GetSysUserModel();
                    result = new SysUserBLL().MyInfoUpdate(usermodel);
                    if (result)
                    {                         
                        StringBuilder strLog = new StringBuilder();
                        strLog.AppendFormat("【修改个人信息】【ID】： {0}【是否可用】：{1}【手机】：{2}", usermodel.SysUser_ID, usermodel.SysUser_Enable, usermodel.SysUser_Tel);
                        new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>PublicHandel('1','操作成功！','');</script>");
                    }
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>PublicHandel('2','操作失败！','');</script>");
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        private bool CheckInput()
        {
            try
            {
                //if (ddlHospital.SelectedValue == "-1" || string.IsNullOrEmpty(ddlHospital.SelectedValue))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>PublicHandel('2','请选择科室！','');</script>");
                //    return false;
                //}
                if (!string.IsNullOrEmpty(txtTel.Text.Trim()) && !txtTel.Text.Trim().IsMobile())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>PublicHandel('2','手机格式错误！','');</script>");
                    return false;
                }
                //if (!string.IsNullOrEmpty(txtTel.Text.Trim()) && !txtTel.Text.Trim().IsTelphone())
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>PublicHandel('2','电话格式错误！','');</script>");
                //    return false;
                //}
              
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }

            return true;
        }
    }
}