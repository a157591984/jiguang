using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Homework.student
{
    public partial class basicSetting : Rc.Cloud.Web.Common.FInitData
    {
        BLL_F_User bll_f_user = new BLL_F_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                init_load();
            }
        }

        private void init_load()
        {
            if (FloginUser != null)
            {
                Model_F_User model = new Model_F_User();
                model = bll_f_user.GetModel(FloginUser.UserId);
                if (model == null)
                    return;
                labUserName.Text = model.UserName;
                txtTrueUserName.Text = model.TrueName;
                ddlSex.SelectedValue = model.Sex;
                txtEmail.Text = model.Email;
                txtMoblie.Text = model.Mobile;
                txtSchool.Text = model.School;
                txtAge.Text = (model.Birthday != null ? DateTime.Parse(model.Birthday.ToString()).ToString("yyyy-MM-dd") : "");
                BindProvince();
                ddlProvince.SelectedValue = model.Province;
                BindCity();
                ddlCity.SelectedValue = model.City;
                BindCountry();
                ddlCountry.SelectedValue = model.County;
            }
        }

        private void BindProvince()
        {
            try
            {
                DataTable dtProvince = new DataTable();
                dtProvince = new Rc.Cloud.BLL.BLL_DictForHospital().GetHospitalRegional(" D_PartentID='' ").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlProvince, dtProvince, "D_Name", "Regional_Dict_ID", "--请选择--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindCity()
        {
            try
            {
                string province = ddlProvince.SelectedValue;
                DataTable dtCity = new DataTable();
                dtCity = new Rc.Cloud.BLL.BLL_DictForHospital().GetHospitalRegional(" D_PartentID='" + province + "' ").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlCity, dtCity, "D_Name", "Regional_Dict_ID", "--请选择--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindCountry()
        {
            try
            {
                string city = ddlCity.SelectedValue;
                DataTable dtCountry = new DataTable();
                dtCountry = new Rc.Cloud.BLL.BLL_DictForHospital().GetHospitalRegional(" D_PartentID='" + city + "' ").Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlCountry, dtCountry, "D_Name", "Regional_Dict_ID", "--请选择--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity();
            BindCountry();
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountry();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Model_F_User model = new Model_F_User();
            model = bll_f_user.GetModel(FloginUser.UserId);
            model.TrueName = txtTrueUserName.Text.Trim();
            if (txtAge.Text != "")
            {
                model.Birthday = Convert.ToDateTime(txtAge.Text.Trim());
            }
            else
            {
                model.Birthday = null;
            }
            model.Sex = ddlSex.SelectedValue;
            model.Mobile = txtMoblie.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.School = txtSchool.Text.Trim();
            model.Province = ddlProvince.SelectedValue;
            model.City = ddlCity.SelectedValue;
            model.County = ddlCountry.SelectedValue;
            bool result = bll_f_user.Update(model);
            if (result)
            {
                Session["FLoginUser"] = model;
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('保存成功',{icon:1,time:1000},function(){window.location.href='basicSetting.aspx';});</script>");

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('保存失败',{icon:2,time:2000});</script>");
            }
        }
    }
}