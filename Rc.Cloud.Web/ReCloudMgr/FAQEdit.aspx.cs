using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FAQEdit : Rc.Cloud.Web.Common.InitData
    {
        public string help_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            help_id = Request.QueryString["help_id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(help_id))
                {
                    LoadData();
                }
            }
        }
        private void LoadData()
        {
            try
            {
                BLL_HelpCenter bll = new BLL_HelpCenter();
                Model_HelpCenter model = new Model_HelpCenter();
                model = bll.GetModel(help_id);
                if (model != null)
                {
                    this.txt_title.Text = model.help_title;
                    this.txt_content.Value = model.help_content;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(help_id))
                {
                    BLL_HelpCenter bll = new BLL_HelpCenter();
                    Model_HelpCenter model = new Model_HelpCenter();
                    model = bll.GetModel(help_id);
                    model.help_title = this.txt_title.Text.TrimEnd();
                    model.help_content = this.txt_content.Value;
                    model.create_time = DateTime.Now;
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改成功', { time: 2000, icon: 1},function(){parent.loadData();parent.layer.close(index)})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改失败', { time: 2000, icon: 2})})})</script>");
                        return;
                    }
                }
                else
                {
                    BLL_HelpCenter bll = new BLL_HelpCenter();
                    Model_HelpCenter model = new Model_HelpCenter();
                    model.help_title = this.txt_title.Text.TrimEnd();
                    model.help_content = this.txt_content.Value;
                    model.help_id = Guid.NewGuid().ToString();
                    model.create_time = DateTime.Now;
                    model.create_userid = loginUser.SysUser_ID;
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('添加成功', { time: 2000, icon: 1},function(){parent.loadData();parent.layer.close(index)})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('添加失败', { time: 2000, icon: 2})})})</script>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}