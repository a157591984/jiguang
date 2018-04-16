using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.Sys
{
    public partial class DictRelation_Add : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = " select * from Common_Dict where D_Type='0'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                pfunction.SetDdl(ddlHead, dt, "D_Name", "Common_Dict_ID", false);
                pfunction.SetDdl(ddlSon, dt, "D_Name", "Common_Dict_ID", false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (new Rc.BLL.Resources.BLL_DictRelation().GetRecordCount("HeadDict_Id='" + ddlHead.SelectedValue + "' and SonDict_Id='" + ddlSon.SelectedValue + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('新增失败:关系已存在。', {icon: 2})})})</script>");
                    return;
                }
                else
                {
                    Rc.Model.Resources.Model_DictRelation dic = new Rc.Model.Resources.Model_DictRelation();
                    dic.DictRelation_Id = Guid.NewGuid().ToString();
                    dic.HeadDict_Id = ddlHead.SelectedValue;
                    dic.SonDict_Id = ddlSon.SelectedValue;
                    dic.CreateTime = DateTime.Now;
                    dic.CreateUser = loginUser.SysUser_ID;
                    if (new Rc.BLL.Resources.BLL_DictRelation().Add(dic))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('新增成功', { time: 1000, icon: 1},function(){parent.loadData();parent.layer.close(index)})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('新增失败', {icon: 2})})})</script>");
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