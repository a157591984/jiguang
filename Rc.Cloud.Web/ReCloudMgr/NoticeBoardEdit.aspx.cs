using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.BLL;
using Rc.Model;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class NoticeBoardEdit : Rc.Cloud.Web.Common.InitData
    {
        public string notice_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            notice_id = Request.QueryString["notice_id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(notice_id))
                {
                    LoadData();
                }
            }
        }
        private void LoadData()
        {
            try
            {
                BLL_NoticeBoard bll = new BLL_NoticeBoard();
                Model_NoticeBoard model = new Model_NoticeBoard();
                model = bll.GetModel(notice_id);
                if (model != null)
                {
                    this.txt_title.Text = model.notice_title;
                    this.txt_content.Value = model.notice_content;
                    this.txtStartTime.Value = pfunction.ConvertToLongDateTime(model.start_time.ToString(), "yyyy-MM-dd HH:mm");
                    this.txtEndTime.Value = pfunction.ConvertToLongDateTime(model.end_time.ToString(), "yyyy-MM-dd HH:mm");
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
                if (!string.IsNullOrEmpty(notice_id))
                {
                    BLL_NoticeBoard bll = new BLL_NoticeBoard();
                    Model_NoticeBoard model = new Model_NoticeBoard();
                    model = bll.GetModel(notice_id);
                    model.notice_title = this.txt_title.Text.TrimEnd();
                    model.notice_content = this.txt_content.Value;
                    model.create_time = DateTime.Now;
                    model.start_time = Convert.ToDateTime(this.txtStartTime.Value);
                    model.end_time = Convert.ToDateTime(this.txtEndTime.Value);
                    if (bll.Update(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改成功', { time: 1000, icon: 1},function(){parent.loadData();parent.layer.close(index)})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('修改失败', {icon: 2})})})</script>");
                        return;
                    }
                }
                else
                {
                    BLL_NoticeBoard bll = new BLL_NoticeBoard();
                    Model_NoticeBoard model = new Model_NoticeBoard();
                    model.notice_title = this.txt_title.Text.TrimEnd();
                    model.notice_content = this.txt_content.Value;
                    model.notice_id = Guid.NewGuid().ToString();
                    model.create_time = DateTime.Now;
                    model.start_time = Convert.ToDateTime(this.txtStartTime.Value);
                    model.end_time = Convert.ToDateTime(this.txtEndTime.Value);
                    model.create_userid = loginUser.SysUser_ID;
                    if (bll.Add(model))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('添加成功', { time: 1000, icon: 1},function(){parent.loadData();parent.layer.close(index)})})})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script>$(function(){layer.ready(function(){layer.msg('添加失败', {icon: 2})})})</script>");
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