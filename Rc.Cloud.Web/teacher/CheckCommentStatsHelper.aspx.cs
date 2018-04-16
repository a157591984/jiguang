using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.BLL.Resources;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.teacher
{
    public partial class CheckCommentStatsHelper : Rc.Cloud.Web.Common.FInitData
    {
        protected string link = string.Empty;
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string HomeWork_Id = string.Empty;
        protected string HomeWork_Name = string.Empty;
        protected string pagetype = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            pagetype = Request.QueryString["pagetype"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
            HomeWork_Name = Request.QueryString["HomeWork_Name"].Filter();
            if (pagetype == "1")
            {
                link = string.Format("../Evaluation/CommentReportSummarize.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}"
                    , ResourceToResourceFolder_Id
                    , HomeWork_Id);
               
            }
            else
            {
                link = string.Format("../Evaluation/CommentReport.aspx?ResourceToResourceFolder_Id={0}&HomeWork_Id={1}"
                   , ResourceToResourceFolder_Id
                   , HomeWork_Id);
            }
        }

        /// <summary>
        /// 验证计算
        /// </summary>
        [WebMethod]
        public static string CheckCalculation(string hwId, string hwName)
        {
            try
            {
                DataTable dtHWDetail = new BLL_HomeWork().GetHWDetail(hwId).Tables[0];
                //System.Threading.Thread.Sleep(10000);
                hwId = hwId.Filter();
                hwName = hwName.Filter();
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                #region 按作业HomeWork 执行数据分析，记录日志
                Model_StatsLog modelLog = new Model_StatsLog();
                modelLog.StatsLogId = Guid.NewGuid().ToString();
                modelLog.DataId = hwId;
                modelLog.DataName = hwName;
                modelLog.DataType = "3";
                modelLog.LogStatus = "2";
                modelLog.CTime = DateTime.Now;
                modelLog.CUser = loginUser.UserId;
                modelLog.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();

                bool flag = new BLL_StatsLog().ExecuteStatsAddLog(modelLog);
                #endregion
                if (flag)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "计算失败：" + ex.Message.ToString());
                return "0";
            }
        }

    }
}