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

namespace Rc.Cloud.Web.Sys
{
    public partial class CustomizeHtmlEdit : Rc.Cloud.Web.Common.InitPage
    {
        string tId = string.Empty;
        BLL_CustomizeHtml bll = new BLL_CustomizeHtml();
        Model_CustomizeHtml model = new Model_CustomizeHtml();
        protected void Page_Load(object sender, EventArgs e)
        {
            tId = Request.QueryString["tId"].ToString().Filter();
            if (!IsPostBack)
            {
                //类型
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.CustomizeHtmlTypeEnum)))
                {
                    ddlSType.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.CustomizeHtmlTypeEnum>(item.ToString()), item.ToString()));
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
                ddlSType.SelectedValue = model.HtmlType;
                txt_content.Value = model.HtmlContent;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tId))
                {
                    #region 添加
                    //验证该类型是否已添加数据
                    if (bll.GetRecordCount("HtmlType='" + ddlSType.SelectedValue + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.ready(function(){layer.msg('该类型数据已添加。', { time: 2000, icon: 4})});</script>");
                        return;
                    }

                    model.CustomizeHtml_Id = Guid.NewGuid().ToString();
                    model.HtmlType = ddlSType.SelectedValue;
                    model.HtmlContent = txt_content.Value;
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});});</script>");
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(layer.msg('新增失败!',{ time: 2000,icon:2}););</script>");
                        return;
                    }
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "新增自定义html");
                    #endregion
                }
                else
                {
                    #region 修改
                    //验证该类型是否已添加数据
                    if (bll.GetRecordCount("CustomizeHtml_Id!='" + tId + "' and HtmlType='" + ddlSType.SelectedValue + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>layer.ready(function(){layer.msg('该类型数据已添加。', { time: 2000, icon: 4})});</script>");
                        return;
                    }
                    model = bll.GetModel(tId);
                    model.HtmlType = ddlSType.SelectedValue;
                    model.HtmlContent = txt_content.Value;
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.ready(function(){layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('修改失败!',{ time: 2000,icon:2});});</script>");
                        return;
                    }
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(loginUser.SysUser_ID, "", "修改自定义html");
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