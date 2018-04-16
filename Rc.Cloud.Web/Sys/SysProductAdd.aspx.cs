using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using System.Text;
using Rc.Cloud.Model;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysProductAdd : Rc.Cloud.Web.Common.InitData
    {
        //页面命令类型
        SysProductType actionType;
        //模板ID
        string SysProductPra;

        protected void Page_Load(object sender, EventArgs e)
        {
            //设置页面类型，并接受Request参数
            SetActionType();
            if (!IsPostBack)
            {
                //如果是修改，则绑定页面信息
                BindSysProductInfo();

            }
        }
        BLL_SysProduct BLL = new BLL_SysProduct();
        //绑定页面信息
        private void BindSysProductInfo()
        {
            if (actionType == SysProductType.Edit)
            {
                try
                {
                    //获取数据
                    var SysProductInfo = GetSysProductInfo();

                    //绑定控件
                    if (SysProductInfo != null)
                    {
                        txtSysCode.Text = SysProductInfo.SysCode;
                        txtSysProductName.Text = SysProductInfo.SysName;
                        txtSysProductSort.Text = SysProductInfo.SysOrder.ToString();
                        txtSysCode.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                    throw ex;
                }

            }
        }

        //获取模板信息
        public Model_SysProduct GetSysProductInfo()
        {
            return BLL.GetModel(SysProductPra);
        }

        //设置页面命令类型
        private void SetActionType()
        {
            SysProductPra = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(SysProductPra))
            {
                SysProductPra = Rc.Common.StrUtility.clsUtility.Decrypt(SysProductPra);
                actionType = SysProductType.Edit;
            }
            else
            {
                actionType = SysProductType.Add;
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
                var SysProductTemp = GetSysProductFromPage();
                if (SysProductTemp == null)
                    //提示消息：不存在模板
                    ShowMsg(false, "不存在模板");

                else
                {
                    //SysHospitalArea.Delete(HospitalArea);
                    if (!EditExists(SysProductTemp))
                    {
                        var res = BLL.Update(SysProductTemp);
                        ShowMsg(res, "修改成功");
                    }
                    else
                    {
                        ShowMsg(false, "此信息以存在");
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
            if (!CheckInput())
                return;

            try
            {
                //从页面上取值
                var samplingTemp = GetSysProductFromPage();
                if (!BLL.AddExists(samplingTemp))
                {
                    var res = BLL.Add(samplingTemp);
                    ShowMsg(res, "保存成功");
                }
                else
                {
                    ShowMsg(false, "此名称已存在");
                }

            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //页面输入验证
        private bool CheckInput()
        {
            var tempName = txtSysProductName.Text.Trim();
            if (tempName.Length == 0)
            {
                //弹出提示
                ShowAlert("请输入系统名称！");
                return false;
            }
            else if (tempName.Length > 100)
            {
                //弹出提示
                ShowAlert("系统名称最长100个字符！");
                return false;
            }

            return true;
        }

        //从页面上获取模板数据
        private Model_SysProduct GetSysProductFromPage()
        {
            var tempSysCode = txtSysCode.Text.Trim();
            var tempName = txtSysProductName.Text.Trim();
            var tempSort = txtSysProductSort.Text.Trim();
            Model_SysProduct SysProductTemp = null;
            switch (actionType)
            {
                case SysProductType.Add:
                    SysProductTemp = new Model_SysProduct();                 
                    SysProductTemp.Sys_CreateUser = loginUser.SysUser_ID;
                    SysProductTemp.Sys_CreateTime = DateTime.Now;
                    break;
                case SysProductType.Edit:
                    SysProductTemp =new Model_SysProduct();
                    SysProductTemp.Sys_UpdateUser = loginUser.SysUser_ID;
                    SysProductTemp.Sys_UpdateTime = DateTime.Now;
                    break;
            }
            SysProductTemp.SysCode = tempSysCode;
            SysProductTemp.SysName = tempName;
            SysProductTemp.SysOrder = int.Parse(tempSort);
            return SysProductTemp;
        }

        //保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (actionType)
            {
                case SysProductType.Add:
                    Add();
                    break;
                case SysProductType.Edit:
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

        //修改后验证是否存在
        private bool EditExists(Model_SysProduct model)
        {
            return BLL.EditExists(model);
        }

        //修改后验证是否存在
        private bool AddExists(Model_SysProduct model)
        {
            return BLL.AddExists(model);
        }      

        //命令类型
        enum SysProductType
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