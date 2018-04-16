using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Common;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.Sys
{
    public partial class ConfigSchoolEdit : Rc.Cloud.Web.Common.InitPage
    {
        protected string ConfigEnum = string.Empty;
        BLL_ConfigSchool bll = new BLL_ConfigSchool();
        Model_ConfigSchool model = new Model_ConfigSchool();
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigEnum = Request.QueryString["ConfigEnum"].ToString().Filter();
            litIp.Text = Rc.Cloud.Web.Common.pfunction.GetRealIP(); ;
            if (!IsPostBack)
            {
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.ConfigSchoolTypeEnum)))
                {
                    ddlDType.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.ConfigSchoolTypeEnum>(item.ToString()), item.ToString()));
                }
                if (!string.IsNullOrEmpty(ConfigEnum))
                {
                    loadData();
                }
            }
        }

        /// <summary>
        /// 修改时的默认值
        /// </summary>
        protected void loadData()
        {
            model = bll.GetModel(ConfigEnum);
            if (model == null)
            {
                return;
            }
            else
            {
                txtConfigEnum.Attributes.Add("readonly", "true");
                txtConfigEnum.Text = model.ConfigEnum;
                hidtxtSchool.Value = model.School_ID;
                txtSchool.Value = model.School_Name;
                txtDName.Text = model.D_Name;
                txtDValue.Text = model.D_Value;
                txtSchoolIP.Text = model.SchoolIP;
                txtDPublicValue.Text = model.D_PublicValue;
                ddlDType.SelectedValue = model.D_Type;
                txtRemark.Text = model.D_Remark;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int intOrder = 0;
                int.TryParse(txtDSort.Text, out intOrder);
                if (string.IsNullOrEmpty(ConfigEnum))
                {
                    #region 添加
                    model = new Model_ConfigSchool();
                    model.ConfigEnum = txtConfigEnum.Text.Trim();
                    model.School_ID = hidtxtSchool.Value.Trim();
                    model.School_Name = txtSchool.Value.Trim();
                    model.D_Name = txtDName.Text.Trim();
                    model.D_Value = txtDValue.Text.Trim();
                    model.D_PublicValue = txtDPublicValue.Text.Trim();
                    model.D_Type = ddlDType.SelectedValue;
                    model.D_Order = intOrder;
                    model.D_Remark = txtRemark.Text.Trim();
                    model.D_CreateUser = loginUser.SysUser_ID;
                    model.D_CreateTime = DateTime.Now;
                    model.SchoolIP = txtSchoolIP.Text.Trim();
                    //验证标识是否已存在
                    if (bll.GetRecordCount("ConfigEnum='" + txtConfigEnum.Text.Trim() + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('标识已存在', { time: 2000, icon: 2})</script>");
                        return;
                    }
                    //验证学校是否已配置某一类型的数据
                    if (bll.GetRecordCount("School_ID='" + hidtxtSchool.Value.Trim() + "' and D_Type='" + ddlDType.SelectedValue + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('该学校已添加此类型数据', { time: 2000, icon: 2})</script>");
                        return;
                    }
                    if (bll.Add(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("90207000", "新增学校配置标识成功");
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(ConfigEnum);
                    model.School_ID = hidtxtSchool.Value.Trim();
                    model.School_Name = txtSchool.Value.Trim();
                    model.D_Name = txtDName.Text.Trim();
                    model.D_Value = txtDValue.Text.Trim();
                    model.D_PublicValue = txtDPublicValue.Text.Trim();
                    model.D_Type = ddlDType.SelectedValue;
                    model.D_Order = intOrder;
                    model.D_Remark = txtRemark.Text.Trim();
                    model.D_UpdateUser = loginUser.SysUser_ID;
                    model.D_UpdateTime = DateTime.Now;
                    model.SchoolIP = txtSchoolIP.Text.Trim();
                    //验证学校是否已配置某一类型的数据
                    if (bll.GetRecordCount("ConfigEnum!='" + txtConfigEnum.Text.Trim() + "' and School_ID='" + hidtxtSchool.Value.Trim() + "' and D_Type='" + ddlDType.SelectedValue + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('该学校已添加此类型数据', { time: 2000, icon: 2})</script>");
                        return;
                    }
                    if (bll.Update(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("90207000", "修改学校配置标识成功");
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("90207000", "操作学校配置标识失败");
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2},function(){parent.loadData();parent.layer.close(index);});</script>");
            }
        }

        /// <summary>
        /// 验证 某一学校，某一配置类型，是否已存在TS2016-06-14
        /// </summary>
        [WebMethod]
        public static string VerifyConfigEnumTypeIsExists(string strConfigEnum, string strSchool_ID, string strD_Type)
        {
            string strJson = string.Empty;
            try
            {

                BLL_ConfigSchool bll = new BLL_ConfigSchool();
                if (bll.GetRecordCount("ConfigEnum!='" + strConfigEnum.Filter() + "' and School_ID='" + strSchool_ID.Filter() + "' and D_Type='" + strD_Type.Filter() + "'") > 0)
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "error", errMsg = "该学校已添加此类型数据" });
                }
                else
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "null", errMsg = "" });
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("90207000", "验证【学校配置类型】是否存在出现异常：" + ex.Message.ToString());
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "error", errMsg = "出现异常错误" });
            }
            return strJson;
        }

        /// <summary>
        /// 验证学校配置标识是否已存在TS2016-06-14
        /// </summary>
        [WebMethod]
        public static string VerifyConfigEnumIsExists(string strConfigEnum)
        {
            string strJson = string.Empty;
            try
            {

                BLL_ConfigSchool bll = new BLL_ConfigSchool();
                if (bll.GetRecordCount("ConfigEnum='" + strConfigEnum.Filter() + "'") > 0)
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "error", errMsg = "标识已存在" });
                }
                else
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "null", errMsg = "" });
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("90207000", "验证【学校配置标识】是否存在出现异常：" + ex.Message.ToString());
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "error", errMsg = "出现异常错误" });
            }
            return strJson;
        }

    }
}