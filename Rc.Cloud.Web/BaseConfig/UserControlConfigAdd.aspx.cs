using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using Rc.Common.StrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.BaseConfig
{
    public partial class UserControlConfigAdd : Rc.Cloud.Web.Common.InitData
    {
        //页面命令类型
        DictionarySQlMaintenance actionType;
        //模板ID
        string dictionarySQlMaintenanceID;

        BLL_DictionarySQlMaintenance BLL = new BLL_DictionarySQlMaintenance();

        protected void Page_Load(object sender, EventArgs e)
        {
            //设置页面类型，并接受Request参数
            SetActionType();
            if (!IsPostBack)
            {
                //如果是修改，则绑定页面信息
                BindDictionarySQlMaintenanceInfo();
            }
        }

        //绑定页面信息
        private void BindDictionarySQlMaintenanceInfo()
        {
            #region 

            //
            if (actionType == DictionarySQlMaintenance.Edit)
            {
                try
                {
                    //获取数据
                    var sysTemp = GetDictionarySQlMaintenanceInfo();

                    //绑定控件
                    if (sysTemp != null)
                    {
                        txtConfigName.Text = sysTemp.DictionarySQlMaintenance_Mark;
                        txtName.Text = sysTemp.DictionarySQlMaintenance_Name;
                        txtDesc.Text = sysTemp.DictionarySQlMaintenance_Explanation;
                        txtSqlString.Text = clsUtility.Decrypt(sysTemp.DictionarySQlMaintenance_SQL);
                    }
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }
            #endregion
        }

        //获取模板信息
        public Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceInfo()
        {
            return BLL.GetDictionarySQlMaintenanceModelByPKNew(dictionarySQlMaintenanceID);
        }

        //设置页面命令类型
        private void SetActionType()
        {
            dictionarySQlMaintenanceID = Request.QueryString["DictionarySQlMaintenanceID"];
            if (!string.IsNullOrWhiteSpace(dictionarySQlMaintenanceID))
            {
                dictionarySQlMaintenanceID = clsUtility.Decrypt(dictionarySQlMaintenanceID);
                actionType = DictionarySQlMaintenance.Edit;
            }
            else
            {
                actionType = DictionarySQlMaintenance.Add;
            }
        }
        //修改
        private void Edit()
        {
            //修改
            try
            {                
                    //从页面上取值
                    var SysUserTemp = GetDictionarySQlMaintenanceFromPage();
                    if (SysUserTemp == null)
                    {
                        //提示消息：不存在模板
                        ShowMsg(false, "不存在");
                    }
                    else
                    {
                        //SysHospitalArea.Delete(HospitalArea);
                        if (!IsOrNotExists(SysUserTemp, 0))
                        {
                            int result = BLL.UpdateNew(SysUserTemp);
                            if (result > 0)
                            {                                
                                ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>parent.Handel(1);</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>parent.Handel(2);</script>");
                            }
                        }
                        else
                        {
                            ShowMsg(false, "此配置标识已存在，请换一下配置标识！");
                        }
                    }
            }
            catch (Exception ex)
            {
                //new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //新增
        private void Add()
        {
            try
            {
                    //从页面上取值
                    var sysModel = GetDictionarySQlMaintenanceFromPage();
                    //验证是否存在；1添加
                    if (!IsOrNotExists(sysModel, 1))
                    {
                        int result = BLL.AddNew(sysModel);
                        if (result > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>parent.Handel(1);</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "InfoSave", "<script type='text/javascript'>parent.Handel(2);</script>");
                        }
                    }
                    else
                    {
                        ShowMsg(false, "此配置标识已存在，请换一下配置标识！");
                    }
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }

        //从页面上获取模板数据
        private Model_DictionarySQlMaintenance GetDictionarySQlMaintenanceFromPage()
        {
            Model_DictionarySQlMaintenance sysModel = null;
            switch (actionType)
            {
                case DictionarySQlMaintenance.Add:
                    sysModel = new Model_DictionarySQlMaintenance();
                    sysModel.DictionarySQlMaintenance_ID = Guid.NewGuid().ToString();
                    sysModel.DictionarySQlMaintenance_CreateTime = DateTime.Now;
                    sysModel.DictionarySQlMaintenance_CretateUser = loginUser.SysUser_ID;
                    break;
                case DictionarySQlMaintenance.Edit:
                    sysModel = GetDictionarySQlMaintenanceInfo();
                    sysModel.DictionarySQlMaintenance_UpdateTime = DateTime.Now;
                    sysModel.DictionarySQlMaintenance_UpdateUser = loginUser.SysUser_ID;
                    break;
            }
            sysModel.DictionarySQlMaintenance_Mark = txtConfigName.Text.Trim();
            sysModel.DictionarySQlMaintenance_Name = txtName.Text.Trim();
            sysModel.DictionarySQlMaintenance_Explanation = txtDesc.Text.Trim();
            sysModel.DictionarySQlMaintenance_SQL = clsUtility.Encrypt(txtSqlString.Text.Trim());
            return sysModel;
        }

        //保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (actionType)
            {
                case DictionarySQlMaintenance.Add:
                    Add();
                    break;
                case DictionarySQlMaintenance.Edit:
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
            scriptStr.Append("<script type='text/javascript'>");

            if (resutl)
                scriptStr.AppendFormat("parent.Handel('1','')");
            else
                scriptStr.AppendFormat("parent.Handel('{0}','{1}')", "2", errorMsg);

            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());

        }

        private void ShowAlert(string AValue)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");
            scriptStr.AppendFormat("parent.ShowAlert('" + AValue + "')");
            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());
        }

        //验证是否存在
        private bool IsOrNotExists(Model_DictionarySQlMaintenance model, int i)
        {
            return BLL.IsOrNotExists(model, i);
        }

        //命令类型
        enum DictionarySQlMaintenance
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

      
    }
}