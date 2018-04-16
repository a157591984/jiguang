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
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Sys
{
    public partial class SchoolSMSAdd : Rc.Cloud.Web.Common.InitPage
    {
        BLL_SchoolSMS bll = new BLL_SchoolSMS();
        Model_SchoolSMS model = new Model_SchoolSMS();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int intSMSCount = 0;
                int.TryParse(txtSMSCount.Text, out intSMSCount);
                if (new BLL_SchoolSMS().GetRecordCount("School_Id='" + hidtxtSchool.Value.Trim() + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.ready(function(){layer.msg('学校短信数据已存在,无法添加.', { time: 2000, icon: 2})});</script>");
                    return;
                }
                #region 添加
                model = new Model_SchoolSMS();
                model.School_Id = hidtxtSchool.Value.Trim();
                model.School_Name = txtSchool.Value.Trim();
                model.SMSCount = intSMSCount;
                model.Remark = txtRemark.Text.Trim();
                model.CreateUser = loginUser.SysUser_ID;
                model.CreateTime = DateTime.Now;
                if (bll.Add(model))
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("90200400", "新增学校短信配置信息成功");
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});});</script>");
                    return;
                }
                #endregion
            }
            catch (Exception)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("90207000", "操作学校短信配置信息失败");
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('操作失败!',{ time: 2000,icon:2},function(){parent.loadData();parent.layer.close(index);});});</script>");
            }
        }
    }
}