using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.teacher
{
    public partial class CorrectMode : Rc.Cloud.Web.Common.FInitData
    {
        string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptClass.DataSource = new BLL_UserGroup().GetList("UserGroup_AttrEnum='" + UserGroup_AttrEnum.Class + "' and UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='" + FloginUser.UserId + "') order by UserGroupOrder ").Tables[0];
            rptClass.DataBind();
        }

        /// <summary>
        /// 作业列表
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCorrectHomework(string ClassId)
        {
            try
            {
                ClassId = ClassId.Filter();

                DataTable dtCHW = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strWhereC = string.Empty;
                string strWhere = string.Empty;

                Model_F_User modelFUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string userId = modelFUser.UserId;

                strWhere += string.Format(" and UserGroup_Id='{0}' and HomeWork_AssignTeacher='{1}' ", ClassId, userId);
                int inum = 0;
                Model_PagerInfo<List<Model_HomeWork>> pageInfo = new BLL_HomeWork().SearhList(strWhere, " CreateTime DESC", 1, 1000);
                List<Model_HomeWork> list = pageInfo.PageData;
                foreach (var item in list)
                {
                    inum++;
                    DateTime stopTime = DateTime.Now;
                    DateTime.TryParse(item.StopTime.ToString(), out stopTime);
                    TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan ts2 = new TimeSpan(stopTime.Ticks);
                    TimeSpan ts3 = ts1.Subtract(ts2).Duration();
                    //你想转的格式

                    listReturn.Add(new
                    {
                        HomeWork_Id = item.HomeWork_Id,
                        HomeWork_Name = item.HomeWork_Name,
                        HomeWork_AssignTeacher = item.HomeWork_AssignTeacher,
                        HomeWork_FinishTime = item.HomeWork_FinishTime,
                        HomeWork_Status = item.HomeWork_Status,
                        CreateTime = pfunction.ConvertToLongDateTime(item.CreateTime.ToString(), "MM-dd"),
                        IsCorrect = stopTime > DateTime.Now ? ts3.TotalMinutes.ToString().Substring(0, ts3.TotalMinutes.ToString().IndexOf('.')) : "yes",
                        UserGroup_Id = item.UserGroup_Id,
                        CorrectModes = item.CorrectMode
                    });
                }
                if (inum > 0)
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }
        [WebMethod]
        public static string UpdateHomeWork(string HomeWorkID, string CorrectMode)
        {
            try
            {
                HomeWorkID = HomeWorkID.Filter();
                CorrectMode = CorrectMode.Filter();
                Model_HomeWork model = new Model_HomeWork();

                BLL_HomeWork bll = new BLL_HomeWork();
                model = bll.GetModel(HomeWorkID);
                model.CorrectMode = CorrectMode;
                if (bll.UpdateCorrectMode(model) > 0)
                {
                    return "1";
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}
