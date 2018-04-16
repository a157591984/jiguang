using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.AuthApi
{
    public partial class VerifyStudentAnswer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='自动提交学生答案' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str == "")
            {
                Thread thread1 = new Thread(new ThreadStart(VerifyUrl));
                if (!thread1.IsAlive)
                {
                    thread1.Start();

                }
            }
            else
            {
                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行20分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='自动提交学生答案' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>20 ";
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
            }
        }

        private void VerifyUrl()
        {
            string operateIP = Rc.Cloud.Web.Common.pfunction.GetRealIP();
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            #region 记录同步开始信息
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动提交学生答案";
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = operateIP + "正在检测...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
            #endregion
            try
            {
                string basicUrl = "/AuthApi/Auto_SubmitStuAnswers.aspx?strFileSyncExecRecord_id=" + strFileSyncExecRecord_id;
                #region 检测 习题集服务器
                string strTestWebSiteUrl = Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl");
                string getResult = Rc.Common.RemotWeb.PostDataToServer(strTestWebSiteUrl + basicUrl, "", System.Text.Encoding.UTF8, "GET");
                #endregion

                #region 检测 学校服务器
                List<Model_ConfigSchool> list = new List<Model_ConfigSchool>();
                list = new BLL_ConfigSchool().GetModelList("");
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.D_PublicValue))
                    {
                        getResult = Rc.Common.RemotWeb.PostDataToServer(item.D_PublicValue + basicUrl + "&SchoolId=" + item.School_ID, "", System.Text.Encoding.UTF8, "GET");
                    }
                }
                #endregion

                #region 记录同步结束信息并保存数据
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = operateIP + "检测成功完成";
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";

                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                #endregion
            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = operateIP + "检测失败：" + ex.Message.ToString();
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
            }

        }
    }
}