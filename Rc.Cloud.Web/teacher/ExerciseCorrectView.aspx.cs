using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Text;
using System.Data;
using Rc.Cloud.Web.Common;
using System.Web.Script.Serialization;

namespace Rc.Cloud.Web.teacher
{
    public partial class ExerciseCorrectView : Rc.Cloud.Web.Common.FInitData
    {
        protected string HomeWork_Id = string.Empty;
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string TestQuestions_Id = string.Empty;
        protected StringBuilder stbTQAnswerScore = new StringBuilder();//试题答案得分
        protected StringBuilder stbStuAnswer = new StringBuilder();//学生答题详情
        string strCorrectStatus = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
                ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
                TestQuestions_Id = Request.QueryString["TestQuestions_Id"].Filter();
                strCorrectStatus = Request.QueryString["correctStatus"].Filter();
                if (strCorrectStatus == "1")
                {
                    btnSubmit.Visible = false;
                }
                if (!IsPostBack)
                {
                    LoadData();
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadData()
        {
            Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
            if (modelRTRF != null)
            {
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);
                ltlHomeWorkName.Text = modelRTRF.File_Name;
                string uploadTestPaperPath = pfunction.GetResourceHost("TestWebSiteUrl") + "Upload/Resource/"; //存储文件基础路径
                string uploadStudentAnswerPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("StudentAnswerWebSiteUrl") + "Upload/Resource/";
                //生成存储路径
                string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
                string fileTestPaperUrl = uploadTestPaperPath + "{0}\\" + savePath + "{1}.{2}";//试题文件详细路径
                string fileStudentAnswerUrl = uploadStudentAnswerPath + "{0}\\" + Rc.Cloud.Web.Common.pfunction.ToShortDate(modelHW.CreateTime.ToString()) + "\\" + savePath + "{1}.{2}";//学生答案文件详细路径

                Model_UserGroup modelUG = new BLL_UserGroup().GetModel(modelHW.UserGroup_Id);
                ltlClassName.Text = modelUG.UserGroup_Name;

                Model_TestQuestions modelTQ = new BLL_TestQuestions().GetModel(TestQuestions_Id);

                //题干
                string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileTestPaperUrl, "testQuestionBody", modelTQ.TestQuestions_Id, "htm"), "", Encoding.UTF8, "Get");


                ltlTQSumScore.Text = modelTQ.TestQuestions_SumScore.ToString().clearLastZero();
                DataTable dtTQScore = new DataTable();
                string strSql = "select * from TestQuestions_Score where TestQuestions_Id='" + TestQuestions_Id + "' order by TestQuestions_OrderNum ";
                dtTQScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                //选择题选项
                string strOption = string.Empty;
                int num = 0;
                foreach (DataRow item in dtTQScore.Rows)
                {
                    if (modelTQ.TestQuestions_Type == "selection" || modelTQ.TestQuestions_Type == "clozeTest")//选择题、完形填空题选项
                    {
                        //从文件读取选择题选项
                        string strTestQuestionOption = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileTestPaperUrl, "testQuestionOption", item["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                        if (listTestSelections != null && listTestSelections.Count > 0)
                        {
                            foreach (var itemTS in listTestSelections)
                            {
                                if(!string.IsNullOrEmpty(itemTS.selectionHTML))strOption += string.Format("<div class=\"option_item\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(itemTS.selectionHTML));
                            }
                            if (num < dtTQScore.Rows.Count - 1 && !string.IsNullOrEmpty(strOption))
                            {
                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                            }
                        }
                    }

                    stbTQAnswerScore.Append("<div class=\"answer_main clearfix\">");
                    //从文件读取正确答案图片
                    string strTestQuestionCorrect = string.Empty;
                    if (modelTQ.TestQuestions_Type == "selection" || modelTQ.TestQuestions_Type == "clozeTest" || modelTQ.TestQuestions_Type == "truefalse")
                    {
                        strTestQuestionCorrect = item["TestCorrect"].ToString();
                    }
                    else if (modelTQ.TestQuestions_Type == "fill" || modelTQ.TestQuestions_Type == "answers")
                    {
                        strTestQuestionCorrect = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileTestPaperUrl, "testQuestionCurrent", item["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                        strTestQuestionCorrect = string.Format("<div>{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCorrect));
                    }
                    stbTQAnswerScore.AppendFormat("<div class=\"correct_answer\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCorrect));
                    stbTQAnswerScore.Append("<div class=\"score\">");
                    stbTQAnswerScore.AppendFormat("<div data-name=\"FillScore\"><input type=\"text\" name=\"fill_Score\" readonly value=\"{0}\" /></div>"
                        , item["TestQuestions_Score"].ToString().clearLastZero());
                    stbTQAnswerScore.Append("</div>");
                    stbTQAnswerScore.Append("</div>");

                    num++;
                }
                ltlTestQuestionsBody.Text = pfunction.NoHTML(strTestQuestionBody) + strOption;

                #region 学生答题详情
                string strWhere = string.Empty;
                string strSqlAnswer = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,shwa.Student_HomeWorkAnswer_Id,shwa.Student_Id,shwa.Student_Answer,shwa.Student_Score,shwa.Student_Answer_Status,shwa.Comment
,fu.UserName,fu.TrueName,shwa.CreateTime
from TestQuestions_Score tqs 
                left join Student_HomeWorkAnswer shwa on TQS.TestQuestions_Id=SHWA.TestQuestions_Id and TQS.TestQuestions_Score_ID=SHWA.TestQuestions_Score_ID 
left join F_User fu on fu.UserId=shwa.Student_Id
where tqs.TestQuestions_Id='" + TestQuestions_Id + "' and shwa.HomeWork_Id='" + HomeWork_Id + "' " + strWhere + " order by tqs.TestQuestions_OrderNum,shwa.CreateTime";

                DataTable dtAnswerScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlAnswer).Tables[0];
                DataTable dtDistinct = dtAnswerScore.DefaultView.ToTable(true, "UserName", "TrueName", "Student_Id", "Comment");
                foreach (DataRow itemDistinct in dtDistinct.Rows)
                {
                    stbStuAnswer.Append("<div class=\"student_answer_box\">");
                    stbStuAnswer.Append("<div class=\"title clearfix\">");
                    stbStuAnswer.AppendFormat("<div class=\"student_name\">{0}的答案<a href='##' data-name=\"remark\" data-content=\"{1}\" id=\"{2}\">批注</a></div>"
                        , string.IsNullOrEmpty(itemDistinct["TrueName"].ToString()) ? itemDistinct["UserName"] : itemDistinct["TrueName"]
                        , itemDistinct["Comment"]
                        , itemDistinct["Student_Id"]);

                    stbStuAnswer.Append("<div class=\"score_name\">得分</div>");
                    stbStuAnswer.Append("</div>");
                    stbStuAnswer.Append("<div class=\"con\">");
                    DataRow[] drScore = dtAnswerScore.Select("Student_Id='" + itemDistinct["Student_Id"] + "'");
                    foreach (DataRow item in drScore)
                    {
                        stbStuAnswer.Append("<div class=\"answer_score clearfix\">");
                        string strStudentAnswer = string.Empty;
                        if (modelTQ.TestQuestions_Type == "selection" || modelTQ.TestQuestions_Type == "clozeTest" || modelTQ.TestQuestions_Type == "truefalse")
                        {
                            strStudentAnswer = item["Student_Answer"].ToString();
                        }
                        else if (modelTQ.TestQuestions_Type == "fill" || modelTQ.TestQuestions_Type == "answers")
                        {
                            strStudentAnswer = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileStudentAnswerUrl, "studentAnswer", item["Student_HomeWorkAnswer_Id"], "txt"), "", Encoding.UTF8, "Get");
                            strStudentAnswer = Rc.Cloud.Web.Common.pfunction.NoHTML(strStudentAnswer);
                            if (string.IsNullOrWhiteSpace(strStudentAnswer))
                            {
                                strStudentAnswer = "&nbsp;&nbsp;";
                            }
                            strStudentAnswer = string.Format("<div>{0}</div>", strStudentAnswer);
                        }
                        if (string.IsNullOrWhiteSpace(strStudentAnswer))
                        {
                            strStudentAnswer = "&nbsp;&nbsp;";
                        }
                        stbStuAnswer.AppendFormat("<div class=\"answer\">{0}</div>", strStudentAnswer);
                        stbStuAnswer.AppendFormat("<div class=\"score\"><input type=\"text\" data-actual-marks=\"{0}\" data-name=\"ScoreTxt\" value=\"{1}\" maxlength=\"3\" id=\"{2}\" tqsId=\"{3}\" /></div>"
                            , item["TestQuestions_Score"].ToString().clearLastZero()
                            , item["Student_Score"].ToString().clearLastZero()
                            , item["Student_HomeWorkAnswer_Id"].ToString()
                            , item["TestQuestions_Score_ID"].ToString());
                        stbStuAnswer.Append("</div>");
                    }
                    stbStuAnswer.Append("</div>");
                    stbStuAnswer.Append("</div>");
                }
                #endregion

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userId = FloginUser.UserId;
            try
            {
                BLL_Student_HomeWorkAnswer bll = new BLL_Student_HomeWorkAnswer();
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(HomeWork_Id, "", string.Format("开始解析按试题批改数据|操作人{0}|作业Id{1}|方法{2}", userId, HomeWork_Id, "ExerciseCorrect"));
                string correctData = hidCorrect.Value;
                JavaScriptSerializer json = new JavaScriptSerializer();
                List<StudentAnswerData> listCorrect = json.Deserialize<List<StudentAnswerData>>(correctData);
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(HomeWork_Id, "", string.Format("完成解析按试题批改数据|操作人{0}|学生作业Id{1}|方法{2}", userId, HomeWork_Id, "ExerciseCorrect"));
                List<Model_Student_HomeWorkAnswer> listSHWA = new List<Model_Student_HomeWorkAnswer>();

                Rc.Common.SystemLog.SystemLog.AddLogFromBS(HomeWork_Id, "", string.Format("开始处理按试题批改数据|操作人{0}|学生作业Id{1}|方法{2}", userId, HomeWork_Id, "ExerciseCorrect"));
                foreach (var item in listCorrect)
                {
                    #region 学生答题表
                    Model_Student_HomeWorkAnswer modelSHWA = bll.GetModel(item.Student_HomeWorkAnswer_Id);
                    modelSHWA.Student_Score = item.score;
                    modelSHWA.Comment = item.comment.Filter();
                    if (item.score == item.actualscore)
                    {
                        modelSHWA.Student_Answer_Status = "right";//对
                    }
                    else if (item.score == 0)
                    {
                        modelSHWA.Student_Answer_Status = "wrong";//错
                    }
                    else
                    {
                        modelSHWA.Student_Answer_Status = "partright";//部分对
                    }
                    listSHWA.Add(modelSHWA);
                    #endregion
                }
                #region 作业试题批改确认表
                Model_HomeWorkQuestionConfirm modelHWQC = new Model_HomeWorkQuestionConfirm();
                modelHWQC.HomeWorkQuestionConfirm_ID = Guid.NewGuid().ToString();
                modelHWQC.HomeWork_Id = HomeWork_Id;
                modelHWQC.TestQuestions_Id = TestQuestions_Id;
                modelHWQC.Confirm_Status = "1";
                modelHWQC.CreateUser = userId;
                modelHWQC.CreateTime = DateTime.Now;
                #endregion

                int result = bll.TeacherCorrectStuHomeWorkByTQ(listSHWA, modelHWQC);

                if (result > 0)
                {
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(HomeWork_Id, "", string.Format("完成处理按试题批改数据|操作人{0}|学生作业Id{1}|方法{2}", userId, HomeWork_Id, "ExerciseCorrect"));
                    string strWhere = string.Format(@"TestQuestions_Type!='title' and ResourceToResourceFolder_Id='{0}' and 
TestQuestions_Num>(select TestQuestions_Num from TestQuestions where TestQuestions_Id='{1}')
and TestQuestions_Id not in( select TestQuestions_Id from HomeWorkQuestionConfirm hwqc where hwqc.HomeWork_Id='{2}' ) order by TestQuestions_Num", ResourceToResourceFolder_Id, TestQuestions_Id, HomeWork_Id);
                    List<Model_TestQuestions> listTQ = new BLL_TestQuestions().GetModelList(strWhere);
                    if (listTQ.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>window.opener.loadHomeWorkTQData('" + HomeWork_Id + "');window.close();</script>");
                    }
                    else
                    {
                        Response.Redirect("ExerciseCorrectView.aspx?HomeWork_Id=" + HomeWork_Id + "&ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&TestQuestions_Id=" + listTQ[0].TestQuestions_Id);
                    }
                }
                else
                {
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(HomeWork_Id, "", string.Format("按试题批改失败：result 为0，|操作人{0}|学生作业Id{1}|方法{2}", userId, HomeWork_Id, "ExerciseCorrect"));
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('批改失败',{icon:2,time:2000});</script>");
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(HomeWork_Id, "", string.Format("按试题批改失败：|操作人{0}|学生作业Id{1}|方法{2}|错误信息{3}", userId, HomeWork_Id, "ExerciseCorrect", ex.Message.ToString()));
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('批改失败err',{icon:2,time:2000});</script>");
            }
        }

        public class StudentAnswerData
        {
            public string Student_HomeWorkAnswer_Id { set; get; }
            public string TestQuestions_Score_ID { set; get; }
            public decimal actualscore { set; get; }
            public decimal score { set; get; }
            public string comment { set; get; }
        }

    }
}