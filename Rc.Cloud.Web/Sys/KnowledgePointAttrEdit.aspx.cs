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
    public partial class KnowledgePointAttrEdit : Rc.Cloud.Web.Common.InitPage
    {
        protected string S_KnowledgePointAttrExtend_Id = string.Empty;
        public string S_KnowledgePointBasic_Id = string.Empty;
        Model_S_KnowledgePointAttrExtend model = new Model_S_KnowledgePointAttrExtend();
        BLL_S_KnowledgePointAttrExtend bll = new BLL_S_KnowledgePointAttrExtend();
        protected void Page_Load(object sender, EventArgs e)
        {
            S_KnowledgePointAttrExtend_Id = Request["S_KnowledgePointAttrExtend_Id"].Filter();
            S_KnowledgePointBasic_Id = Request["S_KnowledgePointBasic_Id"].Filter();
            if (!IsPostBack)
            {
                InitInfo();
                if (!string.IsNullOrEmpty(S_KnowledgePointAttrExtend_Id))
                {
                    loadData();
                }
            }
        }
        private void InitInfo()
        {
            foreach (KnowledgePointAttrEnum item in Enum.GetValues(typeof(KnowledgePointAttrEnum)))
            {
                ddlAttr.Items.Add(new ListItem(EnumService.GetDescription(item), item.ToString()));
            }

        }
        /// <summary>
        /// 修改时的默认值
        /// </summary>
        protected void loadData()
        {
            model = bll.GetModel(S_KnowledgePointAttrExtend_Id);
            if (model == null)
            {
                return;
            }
            else
            {
                ddlAttr.SelectedValue = model.S_KnowledgePointAttrEnum.Trim();
                txtS_KnowledgePointAttrValue.Text = model.S_KnowledgePointAttrValue.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(S_KnowledgePointAttrExtend_Id))
                {
                    #region 添加
                    if (bll.GetRecordCount("S_KnowledgePointBasic_Id='" + S_KnowledgePointBasic_Id + "' and S_KnowledgePointAttrEnum='" + ddlAttr.SelectedValue + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增失败,该知识点已存在此扩展属性',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    model = new Model_S_KnowledgePointAttrExtend();
                    model.S_KnowledgePointAttrExtend_Id = Guid.NewGuid().ToString();
                    model.S_KnowledgePointAttrEnum = ddlAttr.SelectedValue;
                    model.S_KnowledgePointAttrName = ddlAttr.SelectedItem.Text;
                    model.S_KnowledgePointAttrValue = Convert.ToDecimal(txtS_KnowledgePointAttrValue.Text.TrimEnd());
                    model.S_KnowledgePointBasic_Id = S_KnowledgePointBasic_Id;
                    model.CreateTime = DateTime.Now;
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function(){layer.msg('新增成功!',{ time: 2000,icon:1},function(){window.parent.loadData();window.parent.layer.closeAll()})});</script>");

                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('新增失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    if (bll.GetRecordCount("S_KnowledgePointBasic_Id='" + S_KnowledgePointBasic_Id + "' and S_KnowledgePointAttrEnum='" + ddlAttr.SelectedValue + "' and S_KnowledgePointAttrExtend_Id<>'" + S_KnowledgePointAttrExtend_Id + "'") > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败,该知识点已存在此扩展属性',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    model = bll.GetModel(S_KnowledgePointAttrExtend_Id);
                    model.S_KnowledgePointAttrEnum = ddlAttr.SelectedValue;
                    model.S_KnowledgePointAttrName = ddlAttr.SelectedItem.Text;
                    model.S_KnowledgePointAttrValue = Convert.ToDecimal(txtS_KnowledgePointAttrValue.Text.TrimEnd());
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>layer.ready(function(){layer.msg('修改成功!',{ time: 2000,icon:1},function(){window.parent.loadData();window.parent.layer.closeAll()})});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败!',{ time: 2000,icon:2}</script>");
            }
        }
    }
}