using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using Rc.Cloud.Web.Common;
using System.Text;
using System.Data;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysFilter : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90100400";
            UserFun = new BLL_clsAuth().GetUserFunc(loginUser.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), Module_Id);
            if (!IsPostBack)
            {
                DataTable dt = new BLL_SysFilter().GetList("").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    this.txtKeyWord.Text = dt.Rows[0]["KeyWord"].ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLL_SysFilter bll = new BLL_SysFilter();
                DataTable dt = bll.GetList("").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Model_SysFilter model = new Model_SysFilter();
                    model = bll.GetModel(dt.Rows[0]["SysFilter_id"].ToString());
                    model.KeyWord = this.txtKeyWord.Text.Trim();
                    model.Create_Time = DateTime.Now;
                    model.Create_UserId = loginUser.SysUser_ID;
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('修改关键字成功', { time: 2000, icon: 1})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('修改关键字失败', { time: 2000, icon: 2})</script>");
                        return;
                    }
                }
                else
                {
                    Model_SysFilter model = new Model_SysFilter();
                    model.SysFilter_id = Guid.NewGuid().ToString();
                    model.Create_UserId = loginUser.SysUser_ID;
                    model.Create_Time = DateTime.Now;
                    model.KeyWord = this.txtKeyWord.Text.Trim();
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('添加关键字成功', { time: 2000, icon: 1})</script>");

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('添加关键字失败', { time: 2000, icon: 2})</script>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('添加关键字失败', { time: 2000, icon: 2})</script>");
                return;
            }
        }



    }
}