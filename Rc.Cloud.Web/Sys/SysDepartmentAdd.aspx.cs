using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using System.Text;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysDepartmentAdd : Rc.Cloud.Web.Common.InitPage
    {
        static string departmentID;
        BLL_SysDepartment bll = new BLL_SysDepartment();
        string strMessage;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90101000";
            if (!IsPostBack)
            {
                Bind();
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    departmentID = Request["id"].ToString();
                    SetData();

                }
                else
                {
                    departmentID = String.Empty;
                }
            }
        }

        //界面赋值
        private void SetData()
        {
            try
            {
                if (departmentID != null && departmentID != "")
                {
                    Model_SysDepartment model = new BLL_SysDepartment().GetModel_SysDepartmentByPK(departmentID);

                    if (model != null)
                    {

                        TbStepName.Text = model.SysDepartment_Name;
                        TbRemark.Text = model.SysDepartment_Remark;
                        TbTel.Text = model.SysDepartment_Tel;
                        DropDownListUser.SelectedValue = model.SysUser_ID;
                        DropDownListState.SelectedValue = (bool)model.SysDepartment_Enable ? "1" : "0";

                    }

                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        //把数据库中的SysUser表中数据绑定dropdownlist
        void Bind()
        {
            try
            {
                DataTable dtRole = new DataTable();
                dtRole = new BLL_SysUser().GetDataList().Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(DropDownListUser, dtRole, "SysUser_Name", "SysUser_ID", "");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //保存按钮事件
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int n = Update(out strMessage);
                ShowMsg(n, strMessage);
            }
            else
            {

                int n = Add(out strMessage);

                ShowMsg(n, strMessage);

            }

        }

        //更新方法
        private int Update(out string strMessage)
        {
            int result = 0;
            strMessage = string.Empty;
            Model_SysDepartment sysDepartmentModel = setModel(departmentID);
            try
            {
                if (string.IsNullOrEmpty(sysDepartmentModel.SysDepartment_Name))
                {
                    strMessage = "部门名称不能为空";
                }
                else
                {
                    if (!IsOrNotExists(sysDepartmentModel, 0))
                    {
                        result = bll.Update(sysDepartmentModel);
                        if (result == 1)
                        {
                            StringBuilder strLog = new StringBuilder();
                            strLog.AppendFormat("【修改部门】【ID】： {0}【部门名】：{1}【负责人ID】：{2} 【联系电话】 ： {3}【部门状态】 ： {4}",
                            sysDepartmentModel.SysDepartment_ID, sysDepartmentModel.SysDepartment_Name, sysDepartmentModel.SysUser_ID, sysDepartmentModel.SysDepartment_Tel, sysDepartmentModel.SysDepartment_Enable);
                        }
                    }
                    else
                    {
                        ShowMsg(2, "此部门名称存在");
                    }
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }

            return result;
        }

        // 添加方法
        private int Add(out string strMessage)
        {
            int result = 0;
            strMessage = string.Empty;
            departmentID = Guid.NewGuid().ToString();
            Model_SysDepartment sysDepartmentModel = setModel(departmentID);
            try
            {
                if (string.IsNullOrEmpty(sysDepartmentModel.SysDepartment_Name))
                {
                    strMessage = "部门名称不能为空";
                }

                else
                {
                    if (!IsOrNotExists(sysDepartmentModel, 1))
                    {
                        result = bll.Add(sysDepartmentModel);
                        if (result == 1)
                        {
                            StringBuilder strLog = new StringBuilder();
                            strLog.AppendFormat("【添加部门】【ID】： {0}【部门名】：{1}【负责人ID】：{2} 【联系电话】 ： {3}【部门状态】 ： {4}",
                            sysDepartmentModel.SysDepartment_ID, sysDepartmentModel.SysDepartment_Name, sysDepartmentModel.SysUser_ID, sysDepartmentModel.SysDepartment_Tel, sysDepartmentModel.SysDepartment_Enable);
                        }
                    }
                    else
                    {
                        ShowMsg(2, "此部门名称存在");
                    }
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }

            return result;
        }

        // 验证存在
        public bool IsOrNotExists(Model_SysDepartment model, int i)
        {
            return bll.IsOrNotExists(model, i);
        }

        // 为实体赋值
        private Model_SysDepartment setModel(string ID)
        {
            Model_SysDepartment mcd;
            string type = Request["handel"];
            if (type == "1")//更新方法时setmodel
            {
                mcd = bll.GetModel_SysDepartmentByPK(ID);
                mcd.SysDepartment_Name = TbStepName.Text.Trim();
                mcd.SysDepartment_Remark = TbRemark.Text.Trim();
                mcd.SysUser_ID = DropDownListUser.SelectedValue;
                mcd.SysDepartment_Enable = DropDownListState.SelectedValue == "1" ? true : false;
                mcd.SysDepartment_Tel = TbTel.Text.Trim();
                mcd.UpdateUser = loginUser.SysUser_ID;
                mcd.UpdateTime = DateTime.Now;

            }
            else  //添加方法时setmodel
            {
                mcd = new Model_SysDepartment();
                mcd.SysDepartment_ID = ID;
                mcd.SysDepartment_Name = TbStepName.Text.Trim();
                mcd.SysDepartment_Remark = TbRemark.Text.Trim();
                mcd.SysUser_ID = DropDownListUser.SelectedValue;
                mcd.SysDepartment_Enable = DropDownListState.SelectedValue == "1" ? true : false;
                mcd.SysDepartment_Tel = TbTel.Text.Trim();
                mcd.CreateUser = loginUser.SysUser_ID;
                mcd.CreateTime = DateTime.Now;
            }
            return mcd;
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="resutl">成功/失败</param>
        /// <param name="url">成功后跳转的连接</param>
        /// <param name="errorMsg">错误后提示的内容</param>
        private void ShowMsg(int resutl, string errorMsg)
        {
            var scriptStr = new StringBuilder();
            scriptStr.Append("<script type='text/javascript'>");
            scriptStr.Append("$(function(){");
            scriptStr.Append("layer.ready(function(){");
            if (resutl == 1)
            {
                scriptStr.Append("layer.msg('操作成功',{icon:1,time:1000},function(){parent.window.location.href=window.parent.strPageNameAndParm;});");
            }
            else
            {
                scriptStr.AppendFormat("layer.msg('{0}',{icon:2});", errorMsg);
            }
            scriptStr.Append("});});");
            scriptStr.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "fildError", scriptStr.ToString());

        }

    }


}