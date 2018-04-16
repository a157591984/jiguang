using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;
using System.Data;
using System.Text;


namespace Rc.Cloud.Web.Sys
{
    public partial class UpdateDataItemEdit : Rc.Cloud.Web.Common.InitData
    {

        protected string common_dict_id
        {
            get
            {
                if (ViewState["common_dict_id"] != null)
                {
                    return common_dict_id = ViewState["common_dict_id"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { ViewState["common_dict_id"] = value; }
        }
        protected Common_DictBLL bll = new Common_DictBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                common_dict_id = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(common_dict_id))
                {
                    PageBand();
                }
                else
                {
                    common_dict_id = "";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    Common_DictModel model = new Common_DictModel();
                    model = GetData();
                    bool result = false;
                    if (string.IsNullOrEmpty(common_dict_id))//添加
                    {
                        if (bll.Exists(model, 1))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.div_PopEdit('2','已存在同名数据更新项！');</script>");
                            return;
                        }
                        else
                        {
                            result = bll.Add(model) > 0 ? true : false;
                        }
                    }
                    else//修改 
                    {

                        if (bll.Exists(model, 2))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.div_PopEdit('2','已存在同名数据更新项！');</script>");
                            return;
                        }
                        else
                        {
                            result = bll.Update(model) > 0 ? true : false;
                        }
                    }
                    if (result)
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.div_PopEdit('1','保存成功！','" + model.Common_Dict_ID + "');</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.div_PopEdit('2','保存失败！');</script>");
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
        private Common_DictModel GetData()
        {
            Common_DictModel model = new Common_DictModel();
            if (string.IsNullOrEmpty(common_dict_id))
            {
                model.Common_Dict_ID = Guid.NewGuid().ToString();
                model.D_CreateUser = loginUser.SysUser_ID;
                model.D_CreateTime = DateTime.Now;
            }
            else
            {
                model = new Common_DictBLL().GetCommon_DictModelByPK(common_dict_id);
                model.D_ModifyTime = DateTime.Now;
                model.D_ModifyUser = loginUser.SysUser_ID;
            }
            model.D_Name =txtd_name.Text.Trim();
            model.D_Code = txtD_code.Text.Trim();
            model.D_Remark = txtd_Remark.Text.Trim();
            model.D_Type = 43;
            return model;
        }
        //界面赋值
        private void PageBand()
        {
            try
            {
                Common_DictModel model = new Common_DictBLL().GetCommon_DictModelByPK(common_dict_id);
                DataTable dt = new DataTable();
                if (model != null)
                {
                    txtd_Remark.Text = model.D_Remark;
                    txtd_name.Text = model.D_Name;
                    txtD_code.Text = model.D_Code;
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
            return true;
        }
    }
}