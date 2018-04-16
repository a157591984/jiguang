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

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class ResEdit : Rc.Cloud.Web.Common.InitPage
    {
        string iid = string.Empty;
        string rtype = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            iid = Request.QueryString["iid"].ToString().Filter();
            rtype = Request.QueryString["rtype"].ToString().Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(iid) && !string.IsNullOrEmpty(rtype))
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
            if (rtype == "0")//文件夹
            {
                Model_ResourceFolder model = new BLL_ResourceFolder().GetModel(iid);
                txtName.Text = model.ResourceFolder_Name.ReplaceForFilter();
            }
            else if (rtype == "1")//文件
            {
                Model_ResourceToResourceFolder model = new BLL_ResourceToResourceFolder().GetModel(iid);
                txtName.Text = model.Resource_Name.ReplaceForFilter();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                if (rtype == "0")//文件夹
                {
                    Model_ResourceFolder model = new BLL_ResourceFolder().GetModel(iid);
                    model.ResourceFolder_Name = txtName.Text.Trim();
                    flag = new BLL_ResourceFolder().Update(model);
                }
                else if (rtype == "1")//文件
                {
                    Model_ResourceToResourceFolder model = new BLL_ResourceToResourceFolder().GetModel(iid);
                    model.Resource_Name = txtName.Text.Trim();
                    model.File_Name = txtName.Text.Trim();
                    flag = new BLL_ResourceToResourceFolder().Update(model);
                }
                if (flag)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('修改成功!',{ time: 1000,icon:1},function(){parent.loadData();parent.layer.close(index);});})})</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败!',{icon:2});})})</script>");
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "操作异常," + ex + "操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败!',{icon:2},function(){parent.loadData();parent.layer.close(index);});})})</script>");
            }
        }
    }
}