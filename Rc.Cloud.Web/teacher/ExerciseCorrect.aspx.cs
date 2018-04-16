using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.Common;
using System.Text;

namespace Rc.Cloud.Web.teacher
{
    public partial class ExerciseCorrect : Rc.Cloud.Web.Common.FInitData
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
                    listReturn.Add(new
                    {
                        HomeWork_Id = item.HomeWork_Id,
                        ResourceToResourceFolder_Id = item.ResourceToResourceFolder_Id,
                        HomeWork_Name = item.HomeWork_Name,
                        HomeWork_AssignTeacher = item.HomeWork_AssignTeacher,
                        HomeWork_FinishTime = item.HomeWork_FinishTime,
                        HomeWork_Status = item.HomeWork_Status,
                        CreateTime = pfunction.ConvertToLongDateTime(item.CreateTime.ToString(), "MM-dd"),
                        UserGroup_Id = item.UserGroup_Id,
                        CorrectModes = item.CorrectMode,
                        IsUpdate = (stopTime > DateTime.Now || string.IsNullOrEmpty(item.CorrectMode)) ? "no" : "yes"
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

        /// <summary>
        /// 作业习题
        /// </summary>
        [WebMethod]
        public static string GetHomeworkTestQuestions(string HomeWork_Id, string ResourceToResourceFolder_Id)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();

                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                strSql = string.Format(@"select tq.*
,ConfirmCount=(select count(1) from (select distinct TestQuestions_Id from HomeWorkQuestionConfirm where TestQuestions_Id=tq.TestQuestions_Id and HomeWork_Id='{0}')t)
from TestQuestions tq where tq.TestQuestions_Type!='title' and tq.ResourceToResourceFolder_Id='{1}' order by tq.TestQuestions_Num "
                    , HomeWork_Id
                    , ResourceToResourceFolder_Id);

                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                Rc.Model.Resources.Model_ResourceToResourceFolder modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
                string uploadPath = pfunction.GetResourceHost("TestWebSiteUrl") + "Upload/Resource/"; //存储文件基础路径
                //生成存储路径
                string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                string fileUrl = uploadPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strTestQuestions_Type = string.Empty;
                    switch (dt.Rows[i]["TestQuestions_Type"].ToString())
                    {
                        case "selection":
                            strTestQuestions_Type = "选择题";
                            break;
                        case "clozeTest":
                            strTestQuestions_Type = "完形填空题";
                            break;
                        case "truefalse":
                            strTestQuestions_Type = "判断题";
                            break;
                        case "fill":
                            strTestQuestions_Type = "填空题";
                            break;
                        case "answers":
                            strTestQuestions_Type = "简答题";
                            break;
                    }
                    //题干
                    string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");
                    listReturn.Add(new
                    {
                        HomeWork_Id = HomeWork_Id,
                        ResourceToResourceFolder_Id = ResourceToResourceFolder_Id,
                        TestQuestions_Id = dt.Rows[i]["TestQuestions_Id"].ToString(),
                        topicNumber = dt.Rows[i]["topicNumber"].ToString().TrimEnd('.'),
                        TestQuestions_Type = dt.Rows[i]["TestQuestions_Type"].ToString(),
                        TestQuestions_TypeName = strTestQuestions_Type,
                        TestQuestionBody = pfunction.NoHTML(strTestQuestionBody),
                        TestQuestions_SumScore = dt.Rows[i]["TestQuestions_SumScore"].ToString().clearLastZero(),
                        ConfirmCount = Convert.ToInt32(dt.Rows[i]["ConfirmCount"].ToString()),
                        IsCorrect = modelHW.HomeWork_Status == 1 ? "no" : "yes"//作业已标记完成，不允许批改
                    });
                }
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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

    }
}