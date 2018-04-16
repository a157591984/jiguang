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

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class StatsActivity : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "20100200";
            if (!IsPostBack)
            {
                GetSchoolName();
            }
        }
        public void GetSchoolName()
        {
            try
            {
                string temp = "<a href=\"javascript:;\" ajax-value=\"{0}\" >{1}</a>";
                string StrSchool = string.Empty;
                BLL_UserGroup bll = new BLL_UserGroup();
                DataTable dt = bll.GetList(" UserGroup_AttrEnum='School'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StrSchool += string.Format(temp, dt.Rows[i]["UserGroup_Id"]
                                                       , dt.Rows[i]["UserGroup_Name"]);
                    }
                }
                this.ltlSchoolName.Text = StrSchool;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [WebMethod]
        public static string GetData(string ProductType, string ResourceClass, string DateType, string SchoolId, string datetime)
        {
            try
            {
                ProductType = ProductType.Filter();
                ResourceClass = ResourceClass.Filter();
                DateType = DateType.Filter();
                datetime = datetime.Filter();
                SchoolId = SchoolId.Filter();
                string strWhere = " 1=1 ";
                string strDate = string.Empty;
                string strCount = string.Empty;
                string strCount_N = string.Empty;
                if (!string.IsNullOrEmpty(ProductType))
                {
                    strWhere += " and  ProductType='" + ProductType + "'";
                }
                if (!string.IsNullOrEmpty(ResourceClass))
                {
                    strWhere += " and ResourceClass='" + ResourceClass + "'";
                }
                if (!string.IsNullOrEmpty(DateType))
                {
                    strWhere += " and DateType='" + DateType + "'";
                }
                if (DateType == "day")
                {
                    switch (datetime)
                    {
                        case "-1":
                            strWhere += "";
                            break;
                        case "week":
                            strWhere += " and DateData>CONVERT(nvarchar(10),DATEADD(D,-7,GETDATE()),23)";
                            break;
                        case "month":
                            strWhere += " and DateData>CONVERT(nvarchar(10),DATEADD(D,-30,GETDATE()),23)";
                            break;
                        case "threemonth":
                            strWhere += " and DateData>CONVERT(nvarchar(10),DATEADD(D,-90,GETDATE()),23)";
                            break;
                    }
                }
                if (DateType == "week")
                {
                    switch (datetime)
                    {
                        case "-1":
                            strWhere += "";
                            break;
                        case "month":
                            strWhere += " and DateData>'" + GetWeekOfYear(1) + "'";
                            break;
                        case "threemonth":
                            strWhere += " and DateData>'" + GetWeekOfYear(3) + "'";
                            break;
                        case "year":
                            strWhere += " and DateData>'" + GetWeekOfYear(12) + "'";
                            break;
                    }
                }
                if (DateType == "month")
                {
                    switch (datetime)
                    {
                        case "-1":
                            strWhere += "";
                            break;
                        case "year":
                            strWhere += " and DateData>CONVERT(nvarchar(7),DATEADD(YY,-1,getdate()),23)";
                            break;
                        case "threeyear":
                            strWhere += " and DateData>CONVERT(nvarchar(7),DATEADD(YY,-3,getdate()),23) ";
                            break;
                        case "tenyear":
                            strWhere += " and DateData>CONVERT(nvarchar(7),DATEADD(YY,-10,getdate()),23) ";
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(SchoolId) && SchoolId != "-1")
                {
                    strWhere += " and SchoolId='" + SchoolId + "'";
                    List<Model_StatsSchoolActivity> StatsSchoolActivityList = new List<Model_StatsSchoolActivity>();
                    BLL_StatsSchoolActivity StatsSchoolActivityBll = new BLL_StatsSchoolActivity();
                    StatsSchoolActivityList = StatsSchoolActivityBll.GetModelList(strWhere + " order by DateData");
                    foreach (var item in StatsSchoolActivityList)
                    {
                        strDate += item.DateData.ToString() + ",";
                        strCount += item.Activity.ToString() + ",";
                        strCount_N += item.NActivity.ToString() + ",";
                    }
                }
                else
                {
                    List<Model_StatsAllActivity> StatsAllActivityList = new List<Model_StatsAllActivity>();
                    BLL_StatsAllActivity StatsAllActivityBll = new BLL_StatsAllActivity();
                    StatsAllActivityList = StatsAllActivityBll.GetModelList(strWhere + " order by DateData");
                    foreach (var item in StatsAllActivityList)
                    {
                        strDate += item.DateData.ToString() + ",";
                        strCount += item.Activity.ToString() + ",";
                        strCount_N += item.NActivity.ToString() + ",";
                    }
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    strDate = strDate.TrimEnd(','),
                    strCount = strCount.TrimEnd(','),
                    strCount_N = strCount_N.TrimEnd(',')
                });
            }
            catch (Exception)
            {

                return "err";
            }
        }
        private static string GetWeekOfYear(int month)
        {
            DateTime d = DateTime.Now.AddMonths(-month);

            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(d.Year + "-1-1").DayOfWeek);
            //2016年第50周
            //二.获取今天是一年当中的第几天
            int currentDay = d.DayOfYear;// DateTime.Today.DayOfYear;
            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //    刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return d.Year + "年第" + (Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1) + "周";
        }
    }
}