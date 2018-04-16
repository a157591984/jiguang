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
using System.IO;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class AudioVideoIntroEdit : Rc.Cloud.Web.Common.InitPage
    {
        protected string AudioVideoIntroId = string.Empty;
        public string AudioVideoBookId = string.Empty;
        BLL_AudioVideoIntro bll = new BLL_AudioVideoIntro();
        Model_AudioVideoIntro model = new Model_AudioVideoIntro();
        protected void Page_Load(object sender, EventArgs e)
        {
            AudioVideoIntroId = Request.QueryString["AudioVideoIntroId"].ToString().Filter();
            AudioVideoBookId = Request.QueryString["AudioVideoBookId"].ToString().Filter();
            if (!IsPostBack)
            {
                ddlAudioVideoTypeEnum.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.AudioVideoTypeEnum)))
                {
                    ddlAudioVideoTypeEnum.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.AudioVideoTypeEnum>(item.ToString()), item.ToString()));
                }
                if (!string.IsNullOrEmpty(AudioVideoIntroId))
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
            model = bll.GetModel(AudioVideoIntroId);
            if (model == null)
            {
                return;
            }
            else
            {
                ddlAudioVideoTypeEnum.SelectedValue = model.AudioVideoTypeEnum;
                txtAudioVideoUrl.Text = model.AudioVideoName;
                txtFileName.Text = model.FileName;
                if (!string.IsNullOrEmpty(model.AudioVideoUrl))
                {
                    string fileId = string.Empty;
                    fileId = model.AudioVideoUrl;
                    fileId = fileId.Substring(fileId.LastIndexOf("/") + 1);
                    fileId = fileId.Substring(0, fileId.LastIndexOf("."));
                    hidFileID.Value = fileId;
                    if (File.Exists(Server.MapPath(model.AudioVideoUrl))) ltlPriview.Text = "<video src=\"" + model.AudioVideoUrl + "\" controls=\"controls\" style=\"width:100%;\">your browser does not support the video tag</video>";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(AudioVideoIntroId))
                {
                    #region 添加
                    model = new Model_AudioVideoIntro();
                    model.FileName = txtFileName.Text.Trim();
                    model.AudioVideoName = txtAudioVideoUrl.Text.Trim();
                    model.AudioVideoBookId = AudioVideoBookId;
                    model.AudioVideoIntroId = Guid.NewGuid().ToString();
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;
                    model.AudioVideoTypeEnum = ddlAudioVideoTypeEnum.SelectedValue;
                    model.AudioVideoUrl = hidFileUrl.Value;
                    if (bll.Add(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "新增音/视频成功,操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('新增成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});})})</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 修改
                    model = bll.GetModel(AudioVideoIntroId);
                    model.FileName = txtFileName.Text.Trim();
                    model.AudioVideoName = txtAudioVideoUrl.Text.Trim();
                    model.AudioVideoTypeEnum = ddlAudioVideoTypeEnum.SelectedValue;
                    if (!string.IsNullOrEmpty(hidFileUrl.Value))
                    {
                        model.AudioVideoUrl = hidFileUrl.Value;
                    }
                    if (bll.Update(model))
                    {
                        new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "修改音/视频成功,操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                        ClientScript.RegisterStartupScript(this.GetType(), "update", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});})})</script>");
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10401000", "操作异常," + ex + "操作人：(" + loginUser.SysUser_ID + ")" + loginUser.SysUser_Name);
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('操作失败!',{ time: 2000,icon:2},function(){parent.loadData();parent.layer.close(index);});})})</script>");
            }
        }
    }
}