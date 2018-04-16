using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Cloud.Model;
using System.Text;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysUserAdd : Rc.Cloud.Web.Common.InitPage
    {
        //页面命令类型
        SysUser actionType;
        //模板ID
        string sysUser_ID;

        SysUserBLL BLL = new SysUserBLL();
        //sysUserLoginInfoBLL BLLN = new sysUserLoginInfoBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90103000";
            //设置页面类型，并接受Request参数
            SetActionType();
            if (!IsPostBack)
            {
                //如果是修改，则绑定页面信息
                BindSysCodeInfo();
            }
        }

        //绑定页面信息
        private void BindSysCodeInfo()
        {
            #region 获取所有角色信息

            try
            {
                DataTable dtRole = new DataTable();
                dtRole = BLL.GetRoleList().Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetCbl(cblRole, dtRole, "SysRole_Name", "SysRole_ID");
            }
            catch (Exception ex)
            {
                ////MS.Authority.clsAuth.AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }

            #endregion

            #region 设置角色选中

            //获取用户角色信息
            DataTable dt = BLL.GetUserRoleInfo(sysUser_ID).Tables[0];

            string strRoleIDs = string.Empty;
            //把角色id拼成字符串
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strRoleIDs = strRoleIDs + dt.Rows[i]["SysRole_ID"].ToString() + ",";
                }
            }
            strRoleIDs = strRoleIDs.TrimEnd(',');

            //选中角色复选框
            for (int i = 0; i < cblRole.Items.Count; i++)
            {
                if (strRoleIDs.Contains(cblRole.Items[i].Value))
                {
                    cblRole.Items[i].Selected = true;
                }
            }

            #endregion

            #region 用户信息赋值

            //用户信息赋值
            if (actionType == SysUser.Edit)
            {
                try
                {
                    //获取数据
                    var sysUserInfo = GetSysUserInfo();

                    //绑定控件
                    if (sysUserInfo != null)
                    {
                        //sysUser_PassWord = sysUserInfo.SysUser_PassWord;
                        txtNamelogin.Text = sysUserInfo.SysUser_LoginName;
                        //if (!string.IsNullOrEmpty(sysUserInfo.SysUser_PassWord))
                        //{
                        //    txtpwdlogin.Attributes["value"] = DESEncryptLogin.DecryptString(sysUserInfo.SysUser_PassWord);
                        //}
                        txtName.Text = sysUserInfo.SysUser_Name;
                        txtPhone.Text = sysUserInfo.SysUser_Tel;
                        if (sysUserInfo.SysUser_Enable == false)
                        {
                            ckbIsEnable.Checked = false;
                        }
                        else
                        {
                            ckbIsEnable.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                    throw ex;
                }
            }

            #endregion

        }

        //获取模板信息
        public Model_SysUser GetSysUserInfo()
        {
            return BLL.GetSysUserModelByPK(sysUser_ID);
        }

        //设置页面命令类型
        private void SetActionType()
        {
            sysUser_ID = Request.QueryString["SysUser_ID"];
            if (!string.IsNullOrWhiteSpace(sysUser_ID))
            {
                sysUser_ID = clsUtility.Decrypt(sysUser_ID);
                actionType = SysUser.Edit;
                txtNamelogin.Attributes.Add("readonly", "true");
            }
            else
            {
                actionType = SysUser.Add;
            }
        }
        //修改
        private void Edit()
        {
            //修改
            try
            {
                if (CheckInput())
                {
                    //从页面上取值
                    var SysUserTemp = GetSysUserFromPage();
                    var sysUserLogin = sysUserLoginFromPage();
                    if (SysUserTemp == null)
                        //提示消息：不存在模板
                        ShowMsg(false, "不存在模板");

                    else
                    {
                        //SysHospitalArea.Delete(HospitalArea);
                        if (!IsExists(SysUserTemp, 0))
                        {
                            int result = BLL.AddSysUser(SysUserTemp, sysUserLogin, "2");

                            if (result > 0)
                            {
                                StringBuilder strLog = new StringBuilder();
                                strLog.AppendFormat("【修改用户】【ID】： {0}【登录名】：{1}【用户姓名】：{2}【角色】 ： {3}【联系方式】 ： {4}【是否可用】：{5}",
                                    SysUserTemp.SysUser_ID, SysUserTemp.SysUser_LoginName, SysUserTemp.SysUser_Name, sysUserLogin.SysRole_Name, SysUserTemp.SysUser_Tel, SysUserTemp.SysUser_Enable);
                                new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                                ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作成功',{icon:1,time:1000},function(){parent.window.location.href=window.parent.pageUrl})});})</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败',{icon:2})})});</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('此用户已存在',{icon:4})})});</script>");
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

        //新增
        private void Add()
        {
            try
            {
                if (CheckInput())
                {
                    //从页面上取值
                    var Model_SysUser = GetSysUserFromPage();
                    var sysUserLoginModel = sysUserLoginFromPage();
                    //sysUserLoginModel.sysUser_ID = Model_SysUser.SysUser_ID;

                    //验证用户是否存在
                    if (!IsExists(Model_SysUser, 1))
                    {
                        int result = BLL.AddSysUser(Model_SysUser, sysUserLoginModel, "1");
                        if (result > 0)
                        {
                            StringBuilder strLog = new StringBuilder();
                            strLog.AppendFormat("【添加用户】【ID】： {0}【登录名】：{1}【用户姓名】：{2}【角色】 ： {3}【联系方式】 ： {4}【是否可用】：{5}",
                                Model_SysUser.SysUser_ID, Model_SysUser.SysUser_LoginName, Model_SysUser.SysUser_Name, sysUserLoginModel.SysRole_Name, Model_SysUser.SysUser_Tel, Model_SysUser.SysUser_Enable);
                            new BLL_clsAuth().AddLogFromBS(Module_Id, strLog.ToString());
                            ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作成功',{icon:1,time:1000},function(){parent.window.location.href=window.parent.pageUrl})});})</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败',{icon:2})})});</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('此用户已存在',{icon:4})})});</script>");
                    }
                }

            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        string sysUser_IDs = Guid.NewGuid().ToString();
        //从页面上获取模板数据
        private Model_SysUser GetSysUserFromPage()
        {
            Model_SysUser Model_SysUser = null;
            switch (actionType)
            {
                case SysUser.Add:
                    Model_SysUser = new Model_SysUser();
                    Model_SysUser.SysUser_ID = sysUser_IDs;
                    Model_SysUser.SysUser_Name = txtName.Text.Trim();
                    Model_SysUser.SysUser_LoginName = txtNamelogin.Text.Trim();
                    if (!string.IsNullOrEmpty(txtpwdlogin.Text.Trim()))
                    {
                        Model_SysUser.SysUser_PassWord = DESEncryptLogin.EncryptString(txtpwdlogin.Text.Trim());
                    }
                    else
                    {
                        Model_SysUser.SysUser_PassWord = GetSysUser_PassWord();
                    }
                    Model_SysUser.SysUser_Tel = txtPhone.Text.Trim();
                    if (ckbIsEnable.Checked)
                    {
                        Model_SysUser.SysUser_Enable = true;
                    }
                    else
                    {
                        Model_SysUser.SysUser_Enable = false;
                    }

                    Model_SysUser.CreateTime = DateTime.Now;
                    Model_SysUser.CreateUser = loginUser.SysUser_ID;
                    break;
                case SysUser.Edit:
                    Model_SysUser = GetSysUserInfo();
                    Model_SysUser.SysUser_ID = sysUser_ID;
                    Model_SysUser.SysUser_Name = txtName.Text.Trim();
                    Model_SysUser.SysUser_LoginName = txtNamelogin.Text.Trim();
                    if (!string.IsNullOrEmpty(txtpwdlogin.Text.Trim()))
                    {
                        Model_SysUser.SysUser_PassWord = DESEncryptLogin.EncryptString(txtpwdlogin.Text.Trim());
                    }
                    Model_SysUser.SysUser_Tel = txtPhone.Text.Trim();
                    if (ckbIsEnable.Checked)
                    {
                        Model_SysUser.SysUser_Enable = true;
                    }
                    else
                    {
                        Model_SysUser.SysUser_Enable = false;
                    }
                    Model_SysUser.UpdateTime = DateTime.Now;
                    Model_SysUser.UpdateUser = loginUser.SysUser_ID;
                    break;
            }
            return Model_SysUser;
        }
        //从页面上获取模板数据
        private SysRoleModel sysUserLoginFromPage()
        {
            SysRoleModel SysRole = new SysRoleModel();
            SysUserRoleRelationModel SysUserRoleRelation = null;
            string selectedRoleIDs = Rc.Cloud.Web.Common.pfunction.GetCblCheckedValue(cblRole, ",");
            string[] arryselectedRoleIDs = selectedRoleIDs.Split(',');

            switch (actionType)
            {
                case SysUser.Add:
                    #region 用户角色实例

                    //选择了角色
                    if (selectedRoleIDs != string.Empty)
                    {
                        foreach (string selectedRoleID in arryselectedRoleIDs)
                        {
                            SysUserRoleRelation = new SysUserRoleRelationModel();
                            SysUserRoleRelation.SysUser_ID = sysUser_IDs;
                            SysUserRoleRelation.SysRole_ID = selectedRoleID;
                            SysUserRoleRelation.CreateTime = DateTime.Now;
                            SysUserRoleRelation.CreateUser = loginUser.SysUser_ID;
                            SysRole.SysUserRoleList.Add(SysUserRoleRelation);
                        }
                    }
                    #endregion

                    break;
                case SysUser.Edit:
                    if (selectedRoleIDs != string.Empty)
                    {
                        foreach (string selectedRoleID in arryselectedRoleIDs)
                        {
                            SysUserRoleRelation = new SysUserRoleRelationModel();
                            SysUserRoleRelation.SysUser_ID = sysUser_ID;
                            SysUserRoleRelation.SysRole_ID = selectedRoleID;
                            SysUserRoleRelation.UpdateUser = loginUser.SysUser_ID;
                            SysUserRoleRelation.CreateTime = DateTime.Now;
                            SysRole.SysUserRoleList.Add(SysUserRoleRelation);
                        }
                    }

                    break;
            }


            return SysRole;
        }

        //保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (actionType)
            {
                case SysUser.Add:
                    Add();
                    break;
                case SysUser.Edit:
                    Edit();
                    break;
            }
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
            scriptStr.Append("<script type='text/javascript'>$(function(){");

            if (resutl)
                scriptStr.AppendFormat("layer.ready(layer.msg('操作成功',{icon:1,time:1000}))");
            else
                scriptStr.AppendFormat("layer.ready(layer.msg('{0}',{icon:2}))", errorMsg);

            scriptStr.Append("})</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());

        }

        private void ShowAlert(string AValue)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");
            scriptStr.AppendFormat("$(function(){layer.ready(layer.msg('{0}'))})", AValue);
            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());
        }

        //验证是否存在
        private bool IsExists(Model_SysUser model, int i)
        {
            return BLL.Exists(model, i);
        }
        //验证是否存在
        //private bool isSysCodeExists(Model_SysUser model)
        //{
        //    return sysCode.Scds(model);
        //}

        //命令类型
        enum SysUser
        {
            /// <summary>
            /// 添加
            /// </summary>
            Add = 1,
            /// <summary>
            /// 修改
            /// </summary>
            Edit
        }

        //获得PharmacyName
        private string GetSysUser_PassWord()
        {
            string strSql = string.Empty;
            string strTemp = string.Empty;
            strSql = "SELECT SysUser_ID,SysUser_PassWord FROM dbo.SysUser where SysUser_ID='" + loginUser.SysUser_ID + "'";
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            strTemp = dt.Rows[0]["SysUser_PassWord"].ToString();
            return strTemp;
        }

        private bool CheckInput()
        {
            //TextBox tb = (TextBox)tab.Rows[4].Controls[1].FindControl("txtPharmacyName");

            if (txtNamelogin.Text.Trim().Length == 0)
            {
                //弹出提示
                ShowAlert("请输入登录名！");
                return false;
            }
            return true;
        }
    }
}