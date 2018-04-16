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

namespace Rc.Cloud.Web.Sys
{
    public partial class ClientTabEdit : Rc.Cloud.Web.Common.InitPage
    {
        string tId = string.Empty;
        BLL_ClientTab bll = new BLL_ClientTab();
        Model_ClientTab model = new Model_ClientTab();
        protected void Page_Load(object sender, EventArgs e)
        {
            tId = Request.QueryString["tId"].ToString().Filter();
            if (!IsPostBack)
            {
                foreach (var item in Enum.GetValues(typeof(ClientTabTypeEnum)))
                {
                    ddlTabType.Items.Add(new ListItem(Rc.Common.EnumService.GetDescription<ClientTabTypeEnum>(item.ToString()), item.ToString()));
                }
                if (!string.IsNullOrEmpty(tId))
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
            model = bll.GetModel(tId);
            if (model != null)
            {
                txtTabindex.Attributes.Add("readonly", "true");
                txtTabindex.Text = model.Tabindex;
                txtTabName.Text = model.TabName;
                ddlTabType.SelectedValue = model.TabType;
                txtRemark.Text = model.Remark;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tId))
                {
                    #region 添加
                    //验证该Tabindex是否已添加
                    if (bll.GetRecordCount("Tabindex='" + txtTabindex.Text.Trim() + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.msg('该Tab标识数据已添加。', { time: 2000, icon: 4})</script>");
                        return;
                    }
                    model.Tabindex = txtTabindex.Text.Trim();
                    model.TabName = txtTabName.Text.Trim();
                    model.TabType = ddlTabType.SelectedValue;
                    model.Remark = txtRemark.Text.Trim();
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "新增客户端Tab");
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(tId);
                    model.Tabindex = txtTabindex.Text.Trim();
                    model.TabName = txtTabName.Text.Trim();
                    model.TabType = ddlTabType.SelectedValue;
                    model.Remark = txtRemark.Text.Trim();
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUser = loginUser.SysUser_ID;
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "修改客户端Tab");
                    #endregion
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('操作失败!',{ time: 2000,icon:2});});</script>");
            }
        }
    }
}