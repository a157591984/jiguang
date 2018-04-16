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
    public partial class SysCommon_DictAdd1 : Rc.Cloud.Web.Common.InitPage
    {
        static string sysCommon_Dict_ID;
        BLL_Common_DictNew BLL = new BLL_Common_DictNew();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "35201000";
            SetActionType();
            if (!IsPostBack)
            {
                DataTable ddlDType = new DataTable();
                ddlDType = BLL.GetD_Type().Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlD_Type, ddlDType, "D_Remark", "D_type",false);
                //如果是修改，则绑定页面信息
                BindSysCommon_Dict(); 
            }
        }

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
                        //txtD_Value.Text = sysCommon_DictInfo.D_Value.ToString();
                        //txtD_Code.Text = sysCommon_DictInfo.D_Code;
                        //txtD_Order.Text = sysCommon_DictInfo.D_Order.ToString();
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

        private Model_Common_Dict GetSysCommon_Dict()
        {
            return BLL.GetCommon_DictModelByPK(sysCommon_Dict_ID);
        }

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
            scriptStr.AppendFormat("parent.ShowAlert('" + AValue + "')");
            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());
        }
        //从页面上获取模板数据
        private Model_Common_Dict GetSysCodeFromPage(int i)
        {
            Model_Common_Dict Common_DictTemp = null;
            if (i == 1)
            {
                Common_DictTemp = new Model_Common_Dict();
                Common_DictTemp.Common_Dict_ID = Guid.NewGuid().ToString();
                Common_DictTemp.D_CreateUser = loginUser.SysUser_ID;
                Common_DictTemp.D_CreateTime = DateTime.Now;
            }
            else
            {
                Common_DictTemp = GetSysCommon_Dict();
                Common_DictTemp.D_UpdateUser = loginUser.SysUser_ID;
                Common_DictTemp.D_UpdateTime = DateTime.Now;
            }


            Common_DictTemp.D_Name = txtD_Name.Text.Trim();
            if (!string.IsNullOrEmpty(txtD_Value.Text.Trim()))
            {
                Common_DictTemp.D_Value = int.Parse(txtD_Value.Text.Trim());
            }
            //Common_DictTemp.D_Code = txtD_Code.Text.Trim();
            //yyk自己增加
            //if (!string.IsNullOrEmpty(txtD_Order.Text.Trim()))
            //{
            //    Common_DictTemp.D_Order = int.Parse(txtD_Order.Text.Trim());
            //}
            if (!string.IsNullOrEmpty(ddlD_Type.SelectedValue.Trim()))
            {
                Common_DictTemp.D_Type = int.Parse(ddlD_Type.SelectedValue.Trim());
            }
            Common_DictTemp.D_Remark = txtRemark.Text.Trim();            
            return Common_DictTemp;
        }

        //验证添加是否存在
        private bool SysCommon_DictExists(Model_Common_Dict model)
        {
            return BLL.SysCommon_DictExists(model);
        }

        private Model_Common_Dict GetD_OrderNext(Model_Common_Dict samplingTemp)
        {
            int type=Convert.ToInt32(samplingTemp.D_Type);
            samplingTemp.D_Order = BLL.GetTheTypeMaxOrder(type);
            return samplingTemp;
        }

        //新增
        private void Add()
        {
            if (!CheckInput())
                return;            
            try
            {
                //从页面上取值
                Model_Common_Dict samplingTemp = GetSysCodeFromPage(1);
                samplingTemp= GetD_OrderNext(samplingTemp);
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

        //验证编辑是否存在
        private bool Exists(Model_Common_Dict model)
        {
            return BLL.Exists(model);
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
    }
}