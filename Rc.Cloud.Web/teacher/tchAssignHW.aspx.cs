using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Common.Config;
using Rc.Common;
using System.Text;

namespace Rc.Cloud.Web.teacher
{
    public partial class tchAssignHW : System.Web.UI.Page
    {
        /// <summary>
        /// 老师布置作业 17-08-31TS（在习题集服务器或学校服务器运行）
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (LoadData())
                    {
                        Response.Write("ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", "", "布置作业失败（学校服务器）：" + ex.Message.ToString());
            }
        }

        private bool LoadData()
        {
            bool flag = false;
            try
            {
               
                Stream resStream = HttpContext.Current.Request.InputStream;
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                string testJsion = sr.ReadToEnd();
                string resInfo = testJsion;
                tchAssignModel modelAssign = JsonConvert.DeserializeObject<tchAssignModel>(resInfo);
                Model_HomeWork model = new Model_HomeWork();
                model.SubjectId = modelAssign.SubjectId;

                #region 布置作业表
                model.HomeWork_Id = modelAssign.HomeWork_Id;
                model.ResourceToResourceFolder_Id = modelAssign.ResourceToResourceFolder_Id;
                model.HomeWork_Name = modelAssign.HomeWork_Name;
                model.HomeWork_AssignTeacher = modelAssign.HomeWork_AssignTeacher;
                model.BeginTime = modelAssign.BeginTime;
                model.StopTime = modelAssign.StopTime;
                model.IsHide = modelAssign.IsHide;
                model.HomeWork_Status = modelAssign.HomeWork_Status;
                model.CreateTime = modelAssign.CreateTime;
                model.UserGroup_Id = modelAssign.UserGroup_Id;
                model.isTimeLimt = modelAssign.isTimeLimt;
                model.isTimeLength = modelAssign.isTimeLength;
                model.SubjectId = modelAssign.SubjectId;
                model.IsShowAnswer = modelAssign.IsShowAnswer;
                #endregion

                #region 学生作业表
                List<Model_Student_HomeWork> listSHomwWork = new List<Model_Student_HomeWork>();
                List<Model_Student_HomeWork_Submit> listSHomwWorkSubmit = new List<Model_Student_HomeWork_Submit>();
                List<Model_Student_HomeWork_Correct> listSHomwWorkCorrect = new List<Model_Student_HomeWork_Correct>();
                string strStudent = modelAssign.stuInfo;
                string[] strArrStudent = strStudent.Split(',');
                for (int i = 0; i < strArrStudent.Length; i++)
                {
                    string ShwGuid = Guid.NewGuid().ToString();
                    Model_Student_HomeWork modelSHomeWork = new Model_Student_HomeWork();
                    modelSHomeWork.Student_HomeWork_Id = ShwGuid;
                    modelSHomeWork.HomeWork_Id = modelAssign.HomeWork_Id;
                    modelSHomeWork.Student_Id = strArrStudent[i];
                    modelSHomeWork.CreateTime = modelAssign.CreateTime;
                    listSHomwWork.Add(modelSHomeWork);
                    #region 作业提交状态
                    Model_Student_HomeWork_Submit modelSHomeWorkSubmit = new Model_Student_HomeWork_Submit();
                    modelSHomeWorkSubmit.Student_HomeWork_Id = ShwGuid;
                    modelSHomeWorkSubmit.Student_HomeWork_Status = 0;
                    listSHomwWorkSubmit.Add(modelSHomeWorkSubmit);
                    #endregion
                    #region 作业批改状态
                    Model_Student_HomeWork_Correct modelSHomeWorkCorrect = new Model_Student_HomeWork_Correct();
                    modelSHomeWorkCorrect.Student_HomeWork_Id = ShwGuid;
                    modelSHomeWorkCorrect.Student_HomeWork_CorrectStatus = 0;
                    listSHomwWorkCorrect.Add(modelSHomeWorkCorrect);
                    #endregion
                }
                #endregion

                #region 统计帮助表
                DataTable dtHWDetail = Rc.Common.DBUtility.DbHelperSQL.Query("select * from VW_ClassGradeSchool where ClassId='" + modelAssign.UserGroup_Id + "' and GradeId is not null and SchoolId is not null ").Tables[0];

                Model_StatsHelper modelSH_HW = new Model_StatsHelper();
                modelSH_HW.StatsHelper_Id = Guid.NewGuid().ToString();
                modelSH_HW.ResourceToResourceFolder_Id = model.ResourceToResourceFolder_Id;
                modelSH_HW.Homework_Id = model.HomeWork_Id;
                modelSH_HW.Exec_Status = "0";
                modelSH_HW.SType = "1";
                modelSH_HW.CreateUser = modelAssign.HomeWork_AssignTeacher;
                modelSH_HW.SchoolId = dtHWDetail.Rows[0]["SchoolId"].ToString();
                modelSH_HW.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();
                modelSH_HW.ClassId = modelAssign.UserGroup_Id;
                modelSH_HW.TeacherId = modelAssign.HomeWork_AssignTeacher;
                modelSH_HW.HW_CreateTime = modelAssign.CreateTime;

                #endregion
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置作业（学校服务器）：4开始写入数据123");
                if (new BLL_HomeWork().AddHomework(model, listSHomwWork, listSHomwWorkSubmit, listSHomwWorkCorrect, modelSH_HW))
                {
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置作业（学校服务器）：4结束写入数据");
                    if (GenerateTestPaperFileForStudent(modelAssign.HomeWork_AssignTeacher, modelAssign.HomeWork_Id))
                    {
                        flag = true;
                    }
                    else
                    {
                        //RevokeHW 生成学生作业文件失败 撤销作业
                        new BLL_HomeWork().RevokeHW(model.HomeWork_Id);
                        Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置作业失败（学校服务器）：生成学生作业文件失败");
                    }
                }
                else
                {
                    //RevokeHW 写入数据失败 撤销作业
                    new BLL_HomeWork().RevokeHW(model.HomeWork_Id);
                    Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置作业失败（学校服务器）：写入数据失败");
                }
                
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", "", "布置作业失败（学校服务器）：" + ex.Message.ToString());
                
            }
            return flag;
        }

        public class tchAssignModel
        {
            public string stuInfo { get; set; }
            public string HomeWork_Id { get; set; }
            public string ResourceToResourceFolder_Id { get; set; }
            public string HomeWork_Name { get; set; }
            public string HomeWork_AssignTeacher { get; set; }
            public DateTime? BeginTime { get; set; }
            public DateTime? StopTime { get; set; }
            public int IsHide { get; set; }
            public int HomeWork_Status { get; set; }
            public DateTime? CreateTime { get; set; }
            public string UserGroup_Id { get; set; }
            public int isTimeLimt { get; set; }
            public int isTimeLength { get; set; }
            public string SubjectId { get; set; }
            public int IsShowAnswer { get; set; }

        }

        /// <summary>
        /// 生成学生作业txt 17-07-17TS
        /// </summary>
        private bool GenerateTestPaperFileForStudent(string tchId, string HomeWork_Id)
        {
            bool flag = false;
            try
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：5开始组织试题");
                string uploadPath = Server.MapPath("/Upload/Resource/");//存储文件基础路径
                string strTestWebSiteUrl = ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl");//学校局域网地址
                string hosturl = pfunction.getHostPath();
                ////检测网络是否通畅
                //if (RemotWeb.PostDataToServer(strTestWebSiteUrl + "/AuthApi/index.aspx?key=onlinecheck", "", Encoding.UTF8, "Get") != "ok")
                //{
                //    Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：网络不通畅-" + strTestWebSiteUrl +"访问者URL:"+ hosturl);
                //    flag = false;
                //    return flag;
                //}
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);
                string rtrfId = modelHW.ResourceToResourceFolder_Id;
                string strHomeWork_Name = modelHW.HomeWork_Name;
                int isTimeLimt = 0;
                int isTimeLength = 0;
                bool isShowAnswer = false;
                int.TryParse(modelHW.isTimeLimt.ToString(), out isTimeLimt);
                int.TryParse(modelHW.isTimeLength.ToString(), out isTimeLength);
                if (modelHW.IsShowAnswer == 1)
                {
                    isShowAnswer = true;
                }
                string filePath = pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + HomeWork_Id + ".txt";
                string filePathForTch = pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + HomeWork_Id + ".tch.txt";
                List<object> listTQObjForTch = new List<object>();//老师客户端批改
                List<object> listTQObj = new List<object>();//学生客户端答题

                List<object> listTQObjForTchBig = new List<object>();//老师客户端批改
                List<object> listTQObjBig = new List<object>();//学生客户端答题

                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);

                #region 试题
                //试题数据
                string strSqlTQ = string.Format(@"select  t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type],t1.topicNumber
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
FROM TestQuestions t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
where  t1.ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];

                //获取这个试卷的所有试题分值
                string strSqlScore = string.Empty;
                strSqlScore = string.Format(@"select  [TestQuestions_Score_ID]
      ,[ResourceToResourceFolder_Id]
      ,[TestQuestions_Id]
      ,[TestCorrect]
      ,[AnalyzeHyperlinkData]
      ,[TrainHyperlinkData]
      ,[TestType]
      ,[ScoreText],[testIndex],[TestType]
,TestQuestions_OrderNum FROM TestQuestions_Score where ResourceToResourceFolder_Id='{0}' order by TestQuestions_OrderNum ", rtrfId);
                DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                #region 普通题型 list
                DataRow[] drList = dtTQ.Select("Parent_Id='' ", "TestQuestions_Num");
                foreach (DataRow item in drList)
                {
                    string savePath = string.Empty;
                    string saveOwnerPath = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPath = string.Format("{0}\\", pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    DataRow drTQ_S = null;
                    DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", item["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                    #region 试题分数
                    List<object> listTQ_SObjForTch = new List<object>();//老师客户端批改
                    List<object> listTQ_SObj = new List<object>();//学生客户端答题
                    int intIndex = 0;
                    for (int j = 0; j < drTQ_Score.Length; j++)
                    {

                        drTQ_S = drTQ_Score[j];
                        string strAnalyzeUrl = string.Empty;
                        string strTrainUrl = string.Empty;
                        if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                        {
                            strAnalyzeUrl = pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["AnalyzeHyperlinkData"].ToString());
                        }
                        if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                        {
                            strTrainUrl = pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["TrainHyperlinkData"].ToString());
                        }
                        listTQ_SObj.Add(new
                        {
                            testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                            testIndex = drTQ_S["testIndex"].ToString(),
                            analyzeUrl = strAnalyzeUrl,
                            trainUrl = strTrainUrl
                        });

                        string strtestCorrectBase64 = string.Empty;
                        string strtestCorrect = string.Empty;
                        switch (drTQ_Score[j]["TestType"].ToString())
                        {
                            case "selection":
                            case "clozeTest":
                            case "truefalse":
                                strtestCorrect = drTQ_Score[j]["TestCorrect"].ToString();
                                break;
                            //case "fill":
                            //case "answers":
                            //    strtestCorrectBase64 = pfunction.ReadAllText(uploadPath + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt");
                            //    break;
                        }
                        listTQ_SObjForTch.Add(new
                        {
                            testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                            testIndex = drTQ_S["testIndex"].ToString(),
                            scoreText = drTQ_Score[j]["ScoreText"].ToString(),
                            testCorrect = strtestCorrect
                        });
                    }
                    #endregion
                    string fileUrl = uploadPath + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                    if (drTQ_S != null)
                    {
                        string strtestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()));
                        string strtextTitle = pfunction.ReadAllText(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()));
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObj
                        });

                        if (drTQ_S["TestType"].ToString() == "selection" || drTQ_S["TestType"].ToString() == "clozeTest" || drTQ_S["TestType"].ToString() == "truefalse")
                        {
                            strtestQuestionBody = "";
                            strtextTitle = "";
                        }
                        listTQObjForTch.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObjForTch
                        });
                    }
                    else
                    {
                        string strtestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()));
                        string strtextTitle = pfunction.ReadAllText(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()));
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = ""
                        });

                        listTQObjForTch.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObjForTch
                        });
                    }
                }
                #endregion
                #region 综合题型 listBig
                DataRow[] drListBig = dtTQ.Select("Parent_Id='0' ", "TestQuestions_Num");
                foreach (DataRow item in drListBig)
                {
                    List<object> listTQObjForTchBig_Sub = new List<object>();
                    List<object> listTQObjBig_Sub = new List<object>();
                    DataRow[] drBig_Sub = dtTQ.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                    foreach (DataRow itemSub in drBig_Sub)
                    {
                        string savePath = string.Empty;
                        string saveOwnerPath = string.Empty;
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemSub["ParticularYear"].ToString(), itemSub["GradeTerm"],
                                itemSub["Resource_Version"].ToString(), itemSub["Subject"].ToString());
                        }
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                        {
                            saveOwnerPath = string.Format("{0}\\", pfunction.ConvertToLongDateTime(itemSub["CreateTime"].ToString(), "yyyy-MM-dd"));
                        }
                        DataRow drTQ_S = null;
                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", itemSub["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                        #region 试题分数
                        List<object> listTQ_SObjForTch = new List<object>();//老师客户端批改 分值，正确答案
                        List<object> listTQ_SObj = new List<object>();//学生客户端答题 解析，强化训练
                        int intIndex = 0;
                        for (int j = 0; j < drTQ_Score.Length; j++)
                        {
                            drTQ_S = drTQ_Score[j];
                            string strAnalyzeUrl = string.Empty;
                            string strTrainUrl = string.Empty;
                            if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                            {
                                strAnalyzeUrl = pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["AnalyzeHyperlinkData"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                            {
                                strTrainUrl = pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["TrainHyperlinkData"].ToString());
                            }
                            listTQ_SObj.Add(new
                            {
                                testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                                testIndex = drTQ_S["testIndex"].ToString(),
                                analyzeUrl = strAnalyzeUrl,
                                trainUrl = strTrainUrl
                            });

                            string strtestCorrectBase64 = string.Empty;
                            string strtestCorrect = string.Empty;
                            switch (drTQ_Score[j]["TestType"].ToString())
                            {
                                case "selection":
                                case "clozeTest":
                                case "truefalse":
                                    strtestCorrect = drTQ_Score[j]["TestCorrect"].ToString();
                                    break;
                                //case "fill":
                                //case "answers":
                                //    strtestCorrectBase64 = pfunction.ReadAllText(uploadPath + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt");
                                //    break;
                            }
                            listTQ_SObjForTch.Add(new
                            {
                                testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                                testIndex = drTQ_S["testIndex"].ToString(),
                                scoreText = drTQ_Score[j]["ScoreText"].ToString(),
                                testCorrect = strtestCorrect
                            });
                        }
                        if (listTQ_SObjForTch.Count == 0)
                        {
                            listTQ_SObjForTch.Add(null);
                        }
                        #endregion
                        string fileUrl = uploadPath + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                        if (drTQ_S != null)
                        {
                            string strtestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()));
                            string strtextTitle = pfunction.ReadAllText(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()));
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObj
                            });

                            if (drTQ_S["TestType"].ToString() == "selection" || drTQ_S["TestType"].ToString() == "clozeTest" || drTQ_S["TestType"].ToString() == "truefalse")
                            {
                                strtestQuestionBody = "";
                                strtextTitle = "";
                            }
                            listTQObjForTchBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObjForTch
                            });
                        }
                        else
                        {
                            string strtestQuestionBody = pfunction.ReadAllText(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()));
                            string strtextTitle = pfunction.ReadAllText(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()));
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = ""
                            });

                            listTQObjForTchBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObjForTch
                            });
                        }
                    }
                    string savePathBig = string.Empty;
                    string saveOwnerPathBig = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePathBig = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPathBig = string.Format("{0}\\", pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    string fileUrlBig = uploadPath + saveOwnerPathBig + "{0}\\" + savePathBig + "{1}.{2}";//自有资源 savePath为空，云资源saveOwnerPath为空
                    string strdocBase64 = pfunction.ReadAllText(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "txt"));
                    string strdocHtml = pfunction.ReadAllText(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "htm"));
                    string textTitle = pfunction.ReadAllText(string.Format(fileUrlBig, "textTitle", item["TestQuestions_Id"].ToString(), "txt"));
                    listTQObjBig.Add(new
                    {
                        docBase64 = strdocBase64,
                        docHtml = strdocHtml,
                        textTitle = textTitle,
                        topicNumber = (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷) ? item["topicNumber"].ToString() : "",
                        list = listTQObjBig_Sub,
                        type = item["type"].ToString()
                    });
                    if (drBig_Sub.Length > 0) //没有子级试题，不加载节点
                    {
                        listTQObjForTchBig.Add(new
                        {
                            docBase64 = strdocBase64,
                            docHtml = strdocHtml,
                            textTitle = textTitle,
                            list = listTQObjForTchBig_Sub,
                            type = item["type"].ToString()
                        });
                    }

                }
                #endregion

                #endregion
                string strJson = string.Empty;
                strJson = JsonConvert.SerializeObject(new
                {
                    status = true,
                    errorMsg = "",
                    errorCode = "",
                    paperHeaderDoc = GetPaperHeaderDoc(rtrfId),
                    testPaperName = "",
                    isTimeLimt = isTimeLimt,
                    isTimeLength = isTimeLength,
                    sysTime = DateTime.Now.ToString(),
                    isShowAnswerAfterSubmiting = isShowAnswer,
                    list = listTQObj,
                    listBig = listTQObjBig
                });
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：5结束组织试题");
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：6开始写入文件");
                pfunction.WriteToFile(uploadPath + "studentPaper\\" + filePath, strJson, true);

                string strJsonForTch = string.Empty;
                strJsonForTch = JsonConvert.SerializeObject(new
                {
                    list = listTQObjForTch,
                    listBig = listTQObjForTchBig
                });
                pfunction.WriteToFile(uploadPath + "studentPaper\\" + filePathForTch, strJsonForTch, true);
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：6结束写入文件");
                flag = true;
                #region 判断作业文件是否存在，不存在则返回false
                if (!File.Exists(uploadPath + "studentPaper\\" + filePath)
                    || !File.Exists(uploadPath + "studentPaper\\" + filePathForTch))
                {
                    flag = false;
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：6作业文件不存在，布置失败");
                }
                #endregion

            }
            catch (Exception ex)
            {
                flag = false;
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置作业（学校服务器）：异常，布置失败");
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", "", "布置作业失败（学校服务器）：" + ex.Message.ToString());
            }
            return flag;
        }
        private string GetPaperHeaderDoc(string strResourceToResourceFolder_Id)
        {
            string strTemp = string.Empty;
            object obj = Rc.Common.DBUtility.DbHelperSQL.GetSingle("select paperHeaderDoc from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id ='" + strResourceToResourceFolder_Id + "'");
            if (obj != null)
            {
                strTemp = obj.ToString();
            }
            return strTemp;
        }

    }
}