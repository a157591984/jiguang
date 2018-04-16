using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Sys
{
    public partial class DictRelation_Edit : Rc.Cloud.Web.Common.InitPage
    {
        public string Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Request.QueryString["Id"].Filter();
            if (!IsPostBack)
            {
                string sql = " select * from Common_Dict where D_Type='0'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                pfunction.SetDdl(ddlHead, dt, "D_Name", "Common_Dict_ID", false);
                pfunction.SetDdl(ddlSon, dt, "D_Name", "Common_Dict_ID", false);
                if (!string.IsNullOrEmpty(Id))
                {
                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            try
            {
                Rc.Model.Resources.Model_DictRelation dic = new Rc.Model.Resources.Model_DictRelation();
                Rc.BLL.Resources.BLL_DictRelation blldic = new Rc.BLL.Resources.BLL_DictRelation();
                dic = blldic.GetModel(Id);
                if (dic != null)
                {
                    ddlHead.SelectedValue = dic.HeadDict_Id;
                    ddlSon.SelectedValue = dic.SonDict_Id;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (new Rc.BLL.Resources.BLL_DictRelation().GetRecordCount("HeadDict_Id='" + ddlHead.SelectedValue + "' and SonDict_Id='" + ddlSon.SelectedValue + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改失败:关系已存在。', {icon: 2})})})</script>");
                    return;
                }
                else
                {
                    Rc.Model.Resources.Model_DictRelation dic = new Rc.Model.Resources.Model_DictRelation();
                    Rc.BLL.Resources.BLL_DictRelation blldic = new Rc.BLL.Resources.BLL_DictRelation();
                    dic = blldic.GetModel(Id);
                    dic.HeadDict_Id = ddlHead.SelectedValue;
                    dic.SonDict_Id = ddlSon.SelectedValue;
                    dic.CreateTime = DateTime.Now;
                    dic.CreateUser = loginUser.SysUser_ID;
                    if (new Rc.BLL.Resources.BLL_DictRelation().Update(dic))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改成功', { time: 1000, icon: 1},function(){parent.loadData();parent.layer.close(index)})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改失败', {icon: 2})})})</script>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('异常', {icon: 2})})})</script>");
                return;
            }
        }
    }
}