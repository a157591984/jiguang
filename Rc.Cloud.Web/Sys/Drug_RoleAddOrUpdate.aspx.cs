using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using System.Text;
namespace Rc.Cloud.Web.Sys
{
    public partial class Drug_RoleAddOrUpdate : Rc.Cloud.Web.Common.InitPage
    {
        BLL_SysRole dal = new BLL_SysRole();
        private static string check_dict_id = "";
        private Model_VSysUserRole _loginUser;
        private static string user_id = "";
        private static string D_type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90102000";
            if (!IsPostBack)
            {
                check_dict_id = Request["id"].ToString();
                txtName.Text = Request["n"].ToString();
                _loginUser = Rc.Common.StrUtility.clsUtility.IsPageFlag(this.Page) as Model_VSysUserRole;
                txtUserName.Text = _loginUser.SysUser_LoginName;
                user_id = _loginUser.SysUser_ID;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strMessage = string.Empty;
            Boolean result = Save(out strMessage);
            if (result == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('" + strMessage + "',{icon:2,time:1000});});});</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作成功',{icon:1},function(){parent.window.location.href=window.parent.pageUrl});});});</script>");
            }
        }

        private bool Save(out string strMessage)
        {
            Boolean result = false;
            strMessage = string.Empty;
            Model_SysRole mcd = setModel(check_dict_id);

            try
            {
                if (check_dict_id != "")
                {
                    if (CheckExists(mcd, "2"))
                    {
                        strMessage = "已存在此项。";
                    }
                    else
                    {
                        //MS.Authority.clsAuth.AddLogFromBS("Drug_CheckAddOrUpdate", "【修改】【SysRole字典】【操作人】" + txtUserName.Text.Trim() + "【操作内容】：" + mcd.SysRole_Name + "");
                        result = dal.UpdateSysRoleByID(mcd);
                        if (result)
                        {
                            StringBuilder strLog = new StringBuilder();
                            strLog.AppendFormat("【修改角色】【ID】： {0}【角色名】：{1} ", mcd.SysRole_ID, mcd.SysRole_Name);
                            new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                        }

                    }
                }
                else
                {
                    if (txtName.Text.Trim() != "")
                    {
                        if (CheckExists(mcd, "1"))
                        {
                            strMessage = "已存在此项。";
                        }
                        else
                        {
                            //MS.Authority.clsAuth.AddLogFromBS("Drug_CheckAddOrUpdate", "【添加】【SysRole字典】【操作人】" + txtUserName.Text.Trim() + "【操作内容】：" + mcd.SysRole_Name + "");
                            result = dal.AddSysRole(mcd);
                            if (result)
                            {
                                StringBuilder strLog = new StringBuilder();
                                strLog.AppendFormat("【添加角色】【ID】： {0}【角色名】：{1} ", mcd.SysRole_ID, mcd.SysRole_Name);
                                new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                            }
                        }

                    }
                    else
                    {
                        strMessage = "名称为空。";
                        result = false;
                    }
                }
            }
            catch
            {

                throw;
            }
            return result;
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="type">1添加时，2修改时</param>
        /// <returns>已存在返回true</returns>
        private bool CheckExists(Model_SysRole mcd, string type)
        {
            try
            {
                return new Rc.Cloud.BLL.BLL_SysRole().ExistsSysRole(mcd, type);
            }
            catch (Exception)
            {

                return true;
            }
        }
        private Model_SysRole setModel(string Check_Dict_ID)
        {
            Model_SysRole mcd = new Model_SysRole();
            if (Check_Dict_ID != "" && D_type == "")
            {
                mcd.SysRole_ID = Check_Dict_ID;
                mcd.SysRole_Name = txtName.Text.Trim();
                mcd.CreateTime = DateTime.Now;
            }
            else
            {
                mcd.SysRole_ID = Guid.NewGuid().ToString();
                mcd.SysRole_Name = txtName.Text.Trim();
                mcd.CreateTime = DateTime.Now;
            }
            return mcd;
        }
    }
}