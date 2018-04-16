using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.AuthApi
{

    public partial class Auto_SubmitStuAnswers : System.Web.UI.Page
    {
        public string SchoolId = string.Empty;
        public string uploadAnswerPath = string.Empty;
        public string uploadPath = string.Empty;
        protected string strFileSyncExecRecord_id = string.Empty;
        Rc.Model.Resources.Model_FileSyncExecRecordDetail model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
        Rc.BLL.Resources.BLL_FileSyncExecRecordDetail bll_FileSyncExecRecordDetail = new Rc.BLL.Resources.BLL_FileSyncExecRecordDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolId = Request["SchoolId"].Filter();
            strFileSyncExecRecord_id = Request["strFileSyncExecRecord_id"].Filter();
            uploadAnswerPath = "..\\Upload\\Resource\\studentAnswerForSubmit\\";//学生答案json路径
            uploadPath = Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
            //string strSql = string.Empty;
            //strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='自动提交学生答案' order by FileSyncExecRecord_TimeStart desc";
            //string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            //if (str == "")
            //{

            model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
            model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecordDetail.Detail_Remark = "开始检测：" + pfunction.getHostPath();
            model_FileSyncExecRecordDetail.Detail_Status = "0";
            model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
            bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

            SubmitStudentAnswer();

            //Thread thread1 = new Thread(new ThreadStart(ResSynResource));
            //if (!thread1.IsAlive)
            //{
            //    thread1.Start();
            //}

            //            }
            //            else
            //            {
            //                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行60分钟，超时，自动更新状态'
            //where FileSyncExecRecord_Type='自动提交学生答案' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>60 ";
            //                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
            //            }
        }

        public void SubmitStudentAnswer()
        {
            string StrSql = string.Empty;
            if (string.IsNullOrEmpty(SchoolId))
            {
                StrSql = @"select hw.HomeWork_Id,hw.CreateTime,shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwSubmit.Student_HomeWork_Status,shwSubmit.OpenTime,shwSubmit.StudentIP,shwSubmit.Student_Answer_Time 
from Student_HomeWork shw
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status='2'
inner join HomeWork hw on hw.HomeWork_Id=shw.HomeWork_Id
order by hw.CreateTime";
            }
            else
            {
                StrSql = string.Format(@"select hw.HomeWork_Id,hw.CreateTime,shw.Student_HomeWork_Id,shw.HomeWork_Id,shw.Student_Id,shw.CreateTime,shwSubmit.Student_HomeWork_Status,shwSubmit.OpenTime,shwSubmit.StudentIP,shwSubmit.Student_Answer_Time 
from Student_HomeWork shw
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwSubmit.Student_HomeWork_Status='2'
inner join HomeWork hw on hw.HomeWork_Id=shw.HomeWork_Id
where shw.Student_Id in(select UserId from VW_UserOnClassGradeSchool vw where vw.SchoolId='{0}')
order by hw.CreateTime", SchoolId);
            }

            DataTable dtShw = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
            if (dtShw.Rows.Count == 0)
            {
                #region 记录执行结束信息并保存数据
                model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                model_FileSyncExecRecordDetail.Detail_Status = "1";
                model_FileSyncExecRecordDetail.Detail_Remark = "执行完成.没有待执行数据";
                bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                #endregion
                return;
            }

            try
            {
                #region 自动提交学生答案
                int sucNum = 0;
                int failNum = 0;
                string resInfo = string.Empty;
                foreach (DataRow itemShw in dtShw.Rows)
                {
                    try
                    {
                        #region 提交单份学生作业
                        string savePath = string.Empty;
                        Model_HomeWork modelHW = new BLL_HomeWork().GetModel(itemShw["HomeWork_Id"].ToString());
                        string homeWorkId = modelHW.HomeWork_Id;
                        string rtrfId = modelHW.ResourceToResourceFolder_Id;
                        string resourceId = itemShw["Student_HomeWork_Id"].ToString();//学生作业ID
                        string userId = itemShw["Student_Id"].ToString();//学生ID
                        Model_ResourceToResourceFolder modelRTRFolder = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                        string filePath = pfunction.ConvertToLongDateTime(itemShw["CreateTime"].ToString(), "yyyy-MM-dd") + "\\" + itemShw["HomeWork_Id"] + "\\" + itemShw["Student_HomeWork_Id"].ToString() + ".txt";

                        if (File.Exists(Server.MapPath(uploadAnswerPath) + filePath))
                        {
                            resInfo = File.ReadAllText(Server.MapPath(uploadAnswerPath) + filePath);
                            Rc.Interface.testPaperAnswerSubmitModel resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Rc.Interface.testPaperAnswerSubmitModel>(resInfo);

                            Rc.Common.SystemLog.SystemLog.AddLogFromBS("", resourceId, string.Format("开始自动提交学生作业|操作人{0}|学生作业Id{1}|方法{2}", userId, resourceId, "testpaperanswersubmit"));
                            #region 学生作业表
                            Model_Student_HomeWork_Submit modelSHWSubmit = new Model_Student_HomeWork_Submit();
                            modelSHWSubmit = new BLL_Student_HomeWork_Submit().GetModel(itemShw["Student_HomeWork_Id"].ToString());
                            modelSHWSubmit.Student_HomeWork_Status = 1;
                            #endregion
                            #region 学生作业答案表
                            if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                            {
                                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                                   , pfunction.ToShortDate(modelHW.CreateTime.ToString())//作业布置时间
                                   , modelRTRFolder.ParticularYear, modelRTRFolder.GradeTerm
                                   , modelRTRFolder.Resource_Version, modelRTRFolder.Subject);
                            }
                            if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                            {
                                savePath = string.Format("{0}\\"
                                    , pfunction.ToShortDate(modelHW.CreateTime.ToString()));//作业布置时间);
                            }

                            //获取这个试卷的所有试题分值
                            string strSqlScore = string.Empty;
                            strSqlScore = string.Format(@"select [TestQuestions_Score_ID],TestQuestions_ID ,TestQuestions_Score,TestQuestions_OrderNum,TestType
 FROM TestQuestions_Score where ResourceToResourceFolder_Id='{0}'  ", rtrfId);
                            DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                            List<Model_Student_HomeWorkAnswer> listSHWA = new List<Model_Student_HomeWorkAnswer>();
                            List<Rc.Interface.TestPaperAnswerModel> listTestPaperAnswerModel = resModel.answerJson;//普通题型
                            List<Rc.Interface.TestPaperAnswerModelBig> TestPaperAnswerModelBig = resModel.listBig;//综合题型
                            #region 普通题型 list
                            int num = 0;
                            if (listTestPaperAnswerModel != null)
                            {
                                foreach (var item in listTestPaperAnswerModel)
                                {
                                    if (item != null && item.list != null)
                                    {
                                        num++;
                                        int detailNum = 0;
                                        foreach (var itemSub in item.list)
                                        {
                                            if (itemSub != null)
                                            {
                                                detailNum++;
                                                DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format(" TestQuestions_ID='{0}'  and TestQuestions_OrderNum={1} ", item.Testid, detailNum));
                                                if (drTQ_Score != null && drTQ_Score.Length > 0)
                                                {
                                                    #region 学生答题表
                                                    Model_Student_HomeWorkAnswer modelSHWA = new Model_Student_HomeWorkAnswer();
                                                    modelSHWA.TestQuestions_Id = item.Testid;
                                                    string student_HomeWorkAnswer_Id = Guid.NewGuid().ToString();
                                                    modelSHWA.TestQuestions_Score_ID = drTQ_Score[0]["TestQuestions_Score_ID"].ToString();
                                                    modelSHWA.Student_HomeWorkAnswer_Id = student_HomeWorkAnswer_Id;
                                                    modelSHWA.Student_HomeWork_Id = resourceId;
                                                    modelSHWA.Student_Id = userId;
                                                    modelSHWA.HomeWork_Id = homeWorkId;
                                                    modelSHWA.TestQuestions_Num = num;
                                                    modelSHWA.TestQuestions_Detail_OrderNum = detailNum;

                                                    if (itemSub.isHTML == "F")//是否以HTML提交 F: 文本提交； T: HTML提交（选择题，文本提交， 其余均为HTML提交）
                                                    {
                                                        modelSHWA.Student_Answer = itemSub.answerChooses;
                                                    }
                                                    else if (itemSub.isHTML == "T")//HTML提交
                                                    {
                                                        Model_TestQuestions modelTQ = new BLL_TestQuestions().GetModel(item.Testid);
                                                        if (modelTQ.TestQuestions_Type == "truefalse")//判断题答案保存到数据库
                                                        {
                                                            modelSHWA.Student_Answer = itemSub.answerHTML;
                                                        }
                                                        else
                                                        {
                                                            #region 学生答案 保存文件
                                                            if (!String.IsNullOrEmpty(itemSub.answerHTML))
                                                            {
                                                                pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerHTML, true);
                                                            }
                                                            if (!String.IsNullOrEmpty(itemSub.answerImageBase64))
                                                            {
                                                                pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.i.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerImageBase64, true);
                                                            }
                                                            if (!String.IsNullOrEmpty(itemSub.answerDocBase64))
                                                            {
                                                                pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.d.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerDocBase64, true);
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    else //默认存HTML
                                                    {
                                                        #region 学生答案 保存文件
                                                        if (!String.IsNullOrEmpty(itemSub.answerHTML))
                                                        {
                                                            pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerHTML, true);
                                                        }
                                                        if (!String.IsNullOrEmpty(itemSub.answerImageBase64))
                                                        {
                                                            pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.i.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerImageBase64, true);
                                                        }
                                                        if (!String.IsNullOrEmpty(itemSub.answerDocBase64))
                                                        {
                                                            pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.d.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerDocBase64, true);
                                                        }
                                                        #endregion
                                                    }
                                                    //写入客户端的判卷
                                                    modelSHWA.Student_Score = 0;//学生得分默认值(NULL的话统计的时候会出现数据不正确)
                                                    if (itemSub.isRight == "true")
                                                    {
                                                        modelSHWA.Student_Answer_Status = "right";
                                                        decimal temp_decimal = 0;
                                                        decimal.TryParse(drTQ_Score[0]["TestQuestions_Score"].ToString(), out temp_decimal);
                                                        modelSHWA.Student_Score = temp_decimal;
                                                        modelSHWA.isRead = 1;
                                                    }
                                                    else if (itemSub.isRight == "false")
                                                    {
                                                        modelSHWA.Student_Answer_Status = "wrong";
                                                        modelSHWA.isRead = 1;
                                                    }
                                                    else
                                                    {
                                                        modelSHWA.Student_Answer_Status = "unknown";
                                                        modelSHWA.isRead = 0;
                                                    }
                                                    modelSHWA.CreateTime = DateTime.Now;
                                                    string strTestQuestions_NumStr = item.topicNumber.Filter();
                                                    if (!string.IsNullOrEmpty(strTestQuestions_NumStr))
                                                    {
                                                        if (strTestQuestions_NumStr.Length >= 10) strTestQuestions_NumStr = strTestQuestions_NumStr.Substring(0, 10);
                                                    }
                                                    modelSHWA.TestQuestions_NumStr = strTestQuestions_NumStr;//item.topicNumber
                                                    listSHWA.Add(modelSHWA);
                                                    #endregion

                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            #endregion
                            #region 综合题型 listBig
                            if (TestPaperAnswerModelBig != null)
                            {
                                num = 0;
                                foreach (var itemBig in TestPaperAnswerModelBig)
                                {
                                    if (itemBig != null && itemBig.list != null)
                                    {
                                        foreach (var item in itemBig.list)
                                        {
                                            if (item != null && item.list != null)
                                            {
                                                num++;
                                                int detailNum = 0;
                                                foreach (var itemSub in item.list)
                                                {
                                                    if (itemSub != null)
                                                    {
                                                        detailNum++;
                                                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format(" TestQuestions_ID='{0}'  and TestQuestions_OrderNum={1} ", item.Testid, detailNum));
                                                        if (drTQ_Score != null && drTQ_Score.Length > 0)
                                                        {
                                                            #region 学生答题表
                                                            Model_Student_HomeWorkAnswer modelSHWA = new Model_Student_HomeWorkAnswer();
                                                            modelSHWA.TestQuestions_Id = item.Testid;
                                                            string student_HomeWorkAnswer_Id = Guid.NewGuid().ToString();
                                                            modelSHWA.TestQuestions_Score_ID = drTQ_Score[0]["TestQuestions_Score_ID"].ToString();
                                                            modelSHWA.Student_HomeWorkAnswer_Id = student_HomeWorkAnswer_Id;
                                                            modelSHWA.Student_HomeWork_Id = resourceId;
                                                            modelSHWA.Student_Id = userId;
                                                            modelSHWA.HomeWork_Id = homeWorkId;
                                                            modelSHWA.TestQuestions_Num = num;
                                                            modelSHWA.TestQuestions_Detail_OrderNum = detailNum;

                                                            //if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
                                                            //{
                                                            //    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\"
                                                            //       , pfunction.ToShortDate(modelHW.CreateTime.ToString())//作业布置时间
                                                            //       , modelRTRFolder.ParticularYear, modelRTRFolder.GradeTerm
                                                            //       , modelRTRFolder.Resource_Version, modelRTRFolder.Subject);
                                                            //}
                                                            //if (modelRTRFolder.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
                                                            //{
                                                            //    savePath = string.Format("{0}\\"
                                                            //        , pfunction.ToShortDate(modelHW.CreateTime.ToString()));//作业布置时间);
                                                            //}
                                                            if (itemSub.isHTML == "F")//是否以HTML提交 F: 文本提交； T: HTML提交（选择题，文本提交， 其余均为HTML提交）
                                                            {
                                                                modelSHWA.Student_Answer = itemSub.answerChooses;
                                                            }
                                                            else if (itemSub.isHTML == "T")//HTML提交
                                                            {
                                                                Model_TestQuestions modelTQ = new BLL_TestQuestions().GetModel(item.Testid);
                                                                if (modelTQ.TestQuestions_Type == "truefalse")//判断题答案保存到数据库
                                                                {
                                                                    modelSHWA.Student_Answer = itemSub.answerHTML;
                                                                }
                                                                else
                                                                {
                                                                    #region 学生答案 保存文件
                                                                    if (!String.IsNullOrEmpty(itemSub.answerHTML))
                                                                    {
                                                                        pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerHTML, true);
                                                                    }
                                                                    if (!String.IsNullOrEmpty(itemSub.answerImageBase64))
                                                                    {
                                                                        pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.i.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerImageBase64, true);
                                                                    }
                                                                    if (!String.IsNullOrEmpty(itemSub.answerDocBase64))
                                                                    {
                                                                        pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.d.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerDocBase64, true);
                                                                    }
                                                                    #endregion
                                                                }
                                                            }
                                                            else //默认存HTML
                                                            {
                                                                #region 学生答案 保存文件
                                                                if (!String.IsNullOrEmpty(itemSub.answerHTML))
                                                                {
                                                                    pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerHTML, true);
                                                                }
                                                                if (!String.IsNullOrEmpty(itemSub.answerImageBase64))
                                                                {
                                                                    pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.i.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerImageBase64, true);
                                                                }
                                                                if (!String.IsNullOrEmpty(itemSub.answerDocBase64))
                                                                {
                                                                    pfunction.WriteToFile(string.Format("{0}studentAnswer\\{1}{2}.d.txt", uploadPath, savePath, student_HomeWorkAnswer_Id), itemSub.answerDocBase64, true);
                                                                }
                                                                #endregion
                                                            }
                                                            //写入客户端的判卷
                                                            modelSHWA.Student_Score = 0;//学生得分默认值(NULL的话统计的时候会出现数据不正确)
                                                            if (itemSub.isRight == "true")
                                                            {
                                                                modelSHWA.Student_Answer_Status = "right";
                                                                decimal temp_decimal = 0;
                                                                decimal.TryParse(drTQ_Score[0]["TestQuestions_Score"].ToString(), out temp_decimal);
                                                                modelSHWA.Student_Score = temp_decimal;
                                                                modelSHWA.isRead = 1;
                                                            }
                                                            else if (itemSub.isRight == "false")
                                                            {
                                                                modelSHWA.Student_Answer_Status = "wrong";
                                                                modelSHWA.isRead = 1;
                                                            }
                                                            else
                                                            {
                                                                modelSHWA.Student_Answer_Status = "unknown";
                                                                modelSHWA.isRead = 0;
                                                            }
                                                            modelSHWA.CreateTime = DateTime.Now;
                                                            string strTestQuestions_NumStr = item.topicNumber.Filter();
                                                            if (!string.IsNullOrEmpty(strTestQuestions_NumStr))
                                                            {
                                                                if (strTestQuestions_NumStr.Length >= 10) strTestQuestions_NumStr = strTestQuestions_NumStr.Substring(0, 10);
                                                            }
                                                            modelSHWA.TestQuestions_NumStr = strTestQuestions_NumStr;//item.topicNumber
                                                            listSHWA.Add(modelSHWA);
                                                            #endregion

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            #endregion
                            #endregion

                            if (new BLL_Student_HomeWorkAnswer().StudentAnswerSubmit(modelSHWSubmit, listSHWA) > 0)
                            {
                                sucNum++;
                                #region 保存学生答题JSON串
                                //重置answerHTML为空，保存分值
                                List<Model_Student_HomeWorkAnswer> listStuScore = new BLL_Student_HomeWorkAnswer().GetModelList("Student_HomeWork_Id='" + resourceId + "'");
                                #region 普通题型 list
                                if (resModel.answerJson != null)
                                {
                                    foreach (var item in resModel.answerJson)
                                    {
                                        if (item != null && item.list != null)
                                        {
                                            int sNum = 0;
                                            foreach (var itemTQ_S in item.list)
                                            {
                                                if (itemTQ_S != null)
                                                {
                                                    itemTQ_S.answerHTML = "";
                                                    sNum++;
                                                    List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == item.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                    if (listStuScoreSub.Count > 0) itemTQ_S.studentScore = listStuScoreSub[0].Student_Score.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region 综合题型 listBig
                                if (resModel.listBig != null)
                                {
                                    foreach (var item in resModel.listBig)
                                    {
                                        if (item != null && item.list != null)
                                        {
                                            foreach (var itemSub in item.list)
                                            {
                                                if (itemSub != null && itemSub.list != null)
                                                {
                                                    int sNum = 0;
                                                    foreach (var itemTQ_S in itemSub.list)
                                                    {
                                                        if (itemTQ_S != null)
                                                        {
                                                            itemTQ_S.answerHTML = "";
                                                            sNum++;
                                                            List<Model_Student_HomeWorkAnswer> listStuScoreSub = listStuScore.Where(o => o.TestQuestions_Id == itemSub.Testid && o.TestQuestions_Detail_OrderNum == sNum).ToList();
                                                            if (listStuScoreSub.Count > 0) itemTQ_S.studentScore = listStuScoreSub[0].Student_Score.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                string savePathForTch = string.Format("{0}\\", pfunction.ToShortDate(modelHW.CreateTime.ToString()));
                                savePathForTch = "{0}{1}\\" + savePathForTch + "{2}\\{2}.txt";
                                object stuAnswer = new
                                {
                                    list = resModel.answerJson,
                                    listBig = resModel.listBig
                                };
                                pfunction.WriteToFile(string.Format(savePathForTch, uploadPath, "studentAnswerForMarking", resourceId)
                                    , Newtonsoft.Json.JsonConvert.SerializeObject(stuAnswer), true);
                                #endregion
                                Rc.Common.SystemLog.SystemLog.AddLogFromBS("", resourceId, string.Format("成功-自动提交学生作业|操作人{0}|学生作业Id{1}|方法{2}", userId, resourceId, "testpaperanswersubmit"));
                            }
                            else
                            {
                                failNum++;
                                Rc.Common.SystemLog.SystemLog.AddLogFromBS("", resourceId, string.Format("失败-自动提交学生作业|操作人{0}|学生作业Id{1}|方法{2}", userId, resourceId, "testpaperanswersubmit"));
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        failNum++;
                        Rc.Common.SystemLog.SystemLog.AddLogFromBS("", itemShw["Student_HomeWork_Id"].ToString()
                            , string.Format("失败-自动提交学生作业|操作人{0}|学生作业Id{1}|方法{2}。错误：{3}"
                                , itemShw["Student_Id"].ToString()
                                , itemShw["Student_HomeWork_Id"].ToString()
                                , "testpaperanswersubmit"
                                , ex.Message.ToString().Filter()
                                )
                            );
                    }


                    Rc.Common.SystemLog.SystemLog.AddLogFromBS("", itemShw["Student_HomeWork_Id"].ToString(), string.Format("完成-自动提交学生作业|操作人{0}|学生作业Id{1}|方法{2}", itemShw["Student_Id"].ToString(), itemShw["Student_HomeWork_Id"].ToString(), "testpaperanswersubmit"));
                }

                #endregion
                #region 记录执行结束信息并保存数据
                model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                model_FileSyncExecRecordDetail.Detail_Status = "1";
                model_FileSyncExecRecordDetail.Detail_Remark = string.Format("执行完成.数据总数{0}，成功数{1}，失败数{2}", dtShw.Rows.Count, sucNum, failNum);
                bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                #endregion
            }
            catch (Exception ex)
            {
                model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                model_FileSyncExecRecordDetail.Detail_Status = "2";
                model_FileSyncExecRecordDetail.Detail_Remark = "执行失败：" + ex.Message.ToString();
                bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
            }
        }
    }
}