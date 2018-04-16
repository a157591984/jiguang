using Rc.Cloud.Model;
using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.BLL;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Diagnostics;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class ExeccuteDataAnalysis : Rc.Cloud.Web.Common.InitPage
    {
        public string TimeLenght = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10251400";
            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetExeccuteDataAnalysis(int PageIndex, int PageSize)
        {
            try
            {
                string strWhere = string.Empty;
                BLL_ExeccuteDataAnalysis bll = new BLL_ExeccuteDataAnalysis();
                strWhere = "";
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                GH_PagerInfo<List<Model_ExeccuteDataAnalysis>> pageInfo = bll.SearhExeccuteDataAnalysis(strWhere, "ExeccuteTime desc,ExeccuteLenght desc", PageIndex, PageSize);
                List<Model_ExeccuteDataAnalysis> list = pageInfo.PageData;
                List<object> listReturn = new List<object>();
                int inum = 1;
                foreach (var item in list)
                {
                    listReturn.Add(new
                    {
                        Num = inum + PageSize * (PageIndex - 1),
                        ExeccuteTime = pfunction.ConvertToLongDateTime(item.ExeccuteTime.ToString()),
                        ExeccuteLenght = item.ExeccuteLenght.ToString(),
                        ExeccuteUserName = item.ExeccuteUserName
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = pageInfo.CurrentPage,
                        PageSize = pageInfo.PageSize,
                        TotalCount = pageInfo.RecordCount,
                        list = listReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }
        /// <summary>
        /// 执行教学数据分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDataAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                Model_ExeccuteDataAnalysis model = new Model_ExeccuteDataAnalysis();
                model.ExeccuteDataAnalysisID = Guid.NewGuid().ToString();
                model.ExeccuteTime = DateTime.Now;

                Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                int result = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("EXEC P_ExecStatsHWData", 0);
                timer.Stop();
                string lenght = timer.Elapsed.ToString();                
                model.ExeccuteUserId = loginUser.SysUser_ID;
                model.ExeccuteUserName = loginUser.SysUser_Name == "" ? loginUser.SysUser_LoginName : loginUser.SysUser_Name;
                model.ExeccuteLenght = lenght;
                BLL_ExeccuteDataAnalysis bll = new BLL_ExeccuteDataAnalysis();
                bool i = bll.Add(model);
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("教学数据生成错误：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                   , ex.TargetSite.Name.ToString(), ex.Message));
                Response.Redirect(Request.Url.ToString());
            }
        }
        public static String formatLongToTimeStr(int l)
        {
            int hour = 00;
            int minute = 00;
            int second = 00;
            if (l > 1000)
            {
                second = l / 1000;

                if (second > 60)
                {
                    minute = second / 60;
                    second = second % 60;
                }
                if (minute > 60)
                {
                    hour = minute / 60;
                    minute = minute % 60;
                }
                string h = hour.ToString() == "0" ? "00" : hour.ToString();

                string m = minute.ToString() == "0" ? "00" : minute.ToString();
                string s = second.ToString() == "0" ? "00" : second.ToString();

                return (h + ":" + m + ":"
                        + s);
            }
            else
            {
                return "00:00:00." + l;
            }
        }

    }
}