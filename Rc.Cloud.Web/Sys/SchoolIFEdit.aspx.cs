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
    public partial class SchoolIFEdit : Rc.Cloud.Web.Common.InitPage
    {
        protected string SchoolIF_Id = string.Empty;
        BLL_TPSchoolIF bll = new BLL_TPSchoolIF();
        Model_TPSchoolIF model = new Model_TPSchoolIF();
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolIF_Id = Request.QueryString["SchoolIF_Id"].ToString().Filter();
            if (!IsPostBack)
            {
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.ThirdPartyEnum)))
                {
                    ddlSchoolIF_Code.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.ThirdPartyEnum>(item.ToString()), item.ToString()));
                }
                if (!string.IsNullOrEmpty(SchoolIF_Id))
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
            model = bll.GetModel(SchoolIF_Id);
            if (model == null)
            {
                return;
            }
            else
            {
                Model_UserGroup modelUG = new BLL_UserGroup().GetModel(model.SchoolId);
                if (modelUG!=null)
                {
                    txtSchool.Value = modelUG.UserGroup_Name;
                }
                hidtxtSchool.Value = model.SchoolId;
                txtSchoolIF_Name.Text = model.SchoolIF_Name;
                ddlSchoolIF_Code.SelectedValue = model.SchoolIF_Code;
                txtRemark.Text = model.Remark;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SchoolIF_Id))
                {
                    #region 添加
                    model = new Model_TPSchoolIF();
                    model.SchoolIF_Id = Guid.NewGuid().ToString();
                    model.SchoolIF_Name = txtSchoolIF_Name.Text;
                    model.SchoolIF_Code = ddlSchoolIF_Code.SelectedValue;
                    model.SchoolId = hidtxtSchool.Value;
                    model.Remark = txtRemark.Text.Trim();
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;

                    if (bll.Add(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("90200500", "修改学校接口配置成功");
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(SchoolIF_Id);
                    model.SchoolIF_Name = txtSchoolIF_Name.Text;
                    model.SchoolIF_Code = ddlSchoolIF_Code.SelectedValue;
                    model.SchoolId = hidtxtSchool.Value;
                    model.Remark = txtRemark.Text.Trim();
                    if (bll.Update(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("90200500", "修改学校接口配置成功");
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("90200500", "操作学校接口配置失败");
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2},function(){parent.loadData();parent.layer.close(index);});</script>");
            }
        }


        /// <summary>
        /// 验证学校接口配置标识是否已存在 17-12-13TS
        /// </summary>
        [WebMethod]
        public static string VerifySchoolIF_CodeIsExists(string SchoolIF_Id, string SchoolIF_Code)
        {
            string strJson = string.Empty;
            try
            {

                BLL_TPSchoolIF bll = new BLL_TPSchoolIF();
                string strWhere = string.Format(" SchoolIF_Code='{0}' ", SchoolIF_Code.Filter());
                if (!string.IsNullOrEmpty(SchoolIF_Id))
                {
                    strWhere += string.Format(" and SchoolIF_Id!='{0}' ", SchoolIF_Id.Filter());
                }
                if (bll.GetRecordCount(strWhere) > 0)
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "error", errMsg = "标识已添加" });
                }
                else
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "null", errMsg = "" });
                }
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { err = "error", errMsg = "出现异常错误" });
            }
            return strJson;
        }
    }
}