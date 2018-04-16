using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;

namespace Rc.Cloud.Web.student
{
    public partial class CheckStudentStatsHelper : System.Web.UI.Page
    {
        protected string HomeWork_ID = string.Empty;
        protected string HomeWork_Name = string.Empty;
        string ResourceToResourceFolder_Id = string.Empty;
        string StudentId = string.Empty;
        string type = string.Empty;
        protected string link = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Student_HomeWork_Id = Request.QueryString["Student_HomeWork_Id"].Filter();
            string CorrectMode = Request.QueryString["CorrectMode"].Filter();
            HomeWork_ID = Request.QueryString["HomeWork_ID"].Filter();
            HomeWork_Name = Request.QueryString["HomeWork_Name"].Filter();
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            StudentId = Request.QueryString["StudentId"].Filter();
            type = Request.QueryString["type"].Filter();

            if (type == "1")//批改详情
            {
                if (CorrectMode == "1")//客户端批改
                {
                    link = string.Format("ohomeworkview_client.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}&StudentId={2}&Student_HomeWork_Id={3}"
                                , ResourceToResourceFolder_Id
                                , HomeWork_ID
                                , StudentId
                                , Student_HomeWork_Id);
                }
                else//web端批改
                {
                    link = string.Format("oHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}&StudentId={2}&Student_HomeWork_Id={3}"
                                                    , ResourceToResourceFolder_Id
                                                    , HomeWork_ID
                                                    , StudentId
                                                    , Student_HomeWork_Id);                    
                }
            }
            else//分析报告
            {
                link = string.Format("../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id={0}&HomeWork_ID={1}&StudentId={2}&Student_HomeWork_Id={3}"
                , ResourceToResourceFolder_Id
                , HomeWork_ID
                , StudentId
                , CorrectMode == "1" ? Student_HomeWork_Id : "");
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
                modelLog.DataType = "1";
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