using Rc.BLL.Resources;
using Rc.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class StatsLogOperate : Rc.Cloud.Web.Common.InitPage
    {
        BLL_ConfigSchool bll = new BLL_ConfigSchool();
        Model_ConfigSchool model = new Model_ConfigSchool();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                Rc.Cloud.Model.Model_VSysUserRole loginUser = HttpContext.Current.Session["LoginUser"] as Rc.Cloud.Model.Model_VSysUserRole;
                if (ddlType.Value == "1")
                {
                    List<Model_HomeWork> listHW = new BLL_HomeWork().GetModelList("HomeWork_Status='1' and HomeWork_FinishTime between '"
                        + txtSTime.Text.Filter() + "' and '" + txtETime.Text.Filter() + "' ");
                    foreach (var item in listHW)
                    {
                        #region 按日期 执行数据分析，记录日志
                        Model_StatsLog modelLog = new Model_StatsLog();
                        modelLog.StatsLogId = Guid.NewGuid().ToString();
                        modelLog.DataId = item.HomeWork_Id;
                        modelLog.DataName = item.HomeWork_Name;
                        modelLog.DataType = "1";
                        modelLog.LogStatus = "2";
                        modelLog.CTime = DateTime.Now;
                        modelLog.CUser = loginUser.SysUser_ID;

                        flag = new BLL_StatsLog().ExecuteStatsAddLog(modelLog);
                        #endregion
                    }
                }
                else
                {
                    string rtrfId = hidtxtRTRFName.Value.Trim().Filter();
                    if (string.IsNullOrEmpty(rtrfId))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('请选择试卷名称!',{ time: 2000,icon:2},function(){});</script>");
                        return;
                    }
                    else
                    {
                        #region 按试卷 执行数据分析，记录日志
                        Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        Model_StatsLog modelLog = new Model_StatsLog();
                        modelLog.StatsLogId = Guid.NewGuid().ToString();
                        modelLog.DataId = modelRTRF.ResourceToResourceFolder_Id;
                        modelLog.DataName = txtRTRFName.Value;
                        modelLog.DataType = "2";
                        modelLog.LogStatus = "2";
                        modelLog.CTime = DateTime.Now;
                        modelLog.CUser = loginUser.SysUser_ID;

                        flag = new BLL_StatsLog().ExecuteStatsAddLog(modelLog);
                        #endregion
                    }
                }
                if (flag)
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogFromBS("10255000", "手动执行统计成功");
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('执行统计成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index);});</script>");
                }
                else
                {
                    new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("10255000", "手动执行统计失败：执行SQL异常");
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('执行统计失败!',{ time: 2000,icon:2},function(){parent.loadData();parent.layer.close(index);});</script>");
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("10255000", "手动执行统计失败：" + ex.Message.ToString());
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('执行统计失败!',{ time: 2000,icon:2},function(){parent.loadData();parent.layer.close(index);});</script>");
            }
        }
    }
}