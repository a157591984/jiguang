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
    public partial class tchAssignTQ : System.Web.UI.Page
    {
        /// <summary>
        /// 老师布置试题 17-09-21TS（在习题集服务器或学校服务器运行）
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
                Rc.Common.SystemLog.SystemLog.AddLogFromBS("", "", "布置试题失败（学校服务器）：" + ex.Message.ToString());
            }
        }

        private bool LoadData()
        {
            bool flag = false;
            Stream resStream = HttpContext.Current.Request.InputStream;
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
            string testJsion = sr.ReadToEnd();
            string resInfo = testJsion;
            tchAssignModel modelAssign = JsonConvert.DeserializeObject<tchAssignModel>(resInfo);

            BLL_HomeWork bll = new BLL_HomeWork();
            Model_HomeWork model = new Model_HomeWork();
            model.SubjectId = modelAssign.SubjectId;

            // 新资源标识
            string rId = Guid.NewGuid().ToString();
            string rtrfId_New = Guid.NewGuid().ToString();
            string rTime = DateTime.Now.ToString();

            Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题（学校服务器）：4.0开始写入作业对应试题数据");
            #region 写入作业对应试题数据
            List<Model_HomeworkToTQ> listMHTTQ = new List<Model_HomeworkToTQ>();
            string[] arrTQ = modelAssign.tqInfo.Split(',');
            int intSort = 0;
            foreach (var strTQInfo in arrTQ)
            {
                string[] arrTQSub = strTQInfo.Split('^');
                intSort++;
                Model_HomeworkToTQ modelHTTQ = new Model_HomeworkToTQ();
                modelHTTQ.HomeworkToTQ_Id = Guid.NewGuid().ToString();
                modelHTTQ.HomeWork_Id = modelAssign.HomeWork_Id;
                modelHTTQ.ResourceToResourceFolder_Id = rtrfId_New;
                modelHTTQ.TestQuestions_Id = arrTQSub[0];
                modelHTTQ.topicNumber = arrTQSub[1];
                modelHTTQ.UserGroup_Id = modelAssign.UserGroup_Id;
                modelHTTQ.rtrfId_Old = modelAssign.rtrfId_Old;
                modelHTTQ.Sort = intSort;
                modelHTTQ.CreateUser = modelAssign.HomeWork_AssignTeacher;
                modelHTTQ.CreateTime = DateTime.Now;
                listMHTTQ.Add(modelHTTQ);
            }
            if (!new BLL_HomeworkToTQ().AddMultiData(listMHTTQ))
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题（学校服务器）：写入试题错误");
                return false;
            }
            #endregion
            Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题（学校服务器）：4.0结束写入作业对应试题数据");

            Model_ResourceToResourceFolder modelRTRF_Old = new BLL_ResourceToResourceFolder().GetModel(modelAssign.rtrfId_Old);
            //生成存储路径
            string saveOwnerPath = string.Empty;
            if (modelRTRF_Old.Resource_Class == Resource_ClassConst.自有资源)
            {
                saveOwnerPath = pfunction.ToShortDate(modelRTRF_Old.CreateTime.ToString()) + "\\";
            }
            string savePath = string.Empty;
            if (modelRTRF_Old.Resource_Class == Resource_ClassConst.云资源)
            {
                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\"
                 , modelRTRF_Old.ParticularYear
                 , modelRTRF_Old.GradeTerm
                 , modelRTRF_Old.Resource_Version
                 , modelRTRF_Old.Subject);
            }
            #region 生成新试卷
            StringBuilder stbSql = new StringBuilder();
            #region 资源

            #region 插入 Resource
            stbSql.AppendFormat(@"INSERT INTO [dbo].[Resource]([Resource_Id],[Resource_MD5],[Resource_DataStrem],[Resource_ContentHtml],[CreateTime],[Resource_ContentLength])VALUES('{0}','{1}','{2}','{3}','{4}','{5}');", rId, "", "", "", DateTime.Now, "0");
            #endregion
            #region 插入 ResourceToResourceFolder
            stbSql.AppendFormat(@"INSERT INTO [dbo].[ResourceToResourceFolder]
           ([ResourceToResourceFolder_Id]
           ,[ResourceFolder_Id]
           ,[Resource_Id]
           ,[File_Name]
           ,[Resource_Type]
           ,[Resource_Name]
           ,[Resource_Class]
           ,[Resource_Version]
           ,[File_Owner]
           ,[CreateFUser]
           ,[CreateTime]
           ,[UpdateTime]
           ,[File_Suffix]
           ,[LessonPlan_Type]
           ,[GradeTerm]
           ,[Subject]
           ,[Resource_Domain]
           ,[Resource_Url]
           ,[Resource_shared]
           ,[Book_ID]
           ,[ParticularYear]
           ,[ResourceToResourceFolder_Order])
     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}');"
                , rtrfId_New
                , Consts.ResFolderId_Tch
                , rId
                , modelAssign.HomeWork_Name
                , Resource_TypeConst.testPaper类型文件
                , modelAssign.HomeWork_Name
                , Resource_ClassConst.自有资源
                , ""
                , modelAssign.HomeWork_AssignTeacher
                , modelAssign.HomeWork_AssignTeacher
                , rTime
                , ""
                , "testPaper"
                , LessonPlan_TypeConst.老师按题布置作业
                , ""
                , ""
                , ""
                , ""
                , ""
                , ""
                , ""
                , 0);
            #endregion
            #endregion
            //试题数据
            string sqlTQ = @"select tq.*
,rtf.ResourceToResourceFolder_Id,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject
from HomeworkToTQ htq 
inner join TestQuestions tq on tq.TestQuestions_Id=htq.TestQuestions_Id 
inner join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where htq.HomeWork_Id='" + modelAssign.HomeWork_Id + "' order by htq.Sort ";
            DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ).Tables[0];
            //试题分值数据
            string sqlTQScore = @"select tqs.* from HomeworkToTQ htq
inner join TestQuestions_Score tqs on tqs.TestQuestions_Id=htq.TestQuestions_Id 
where htq.HomeWork_Id='" + modelAssign.HomeWork_Id + "' ";
            DataTable dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQScore).Tables[0];
            string uploadPath = Server.MapPath("/Upload/Resource/"); //存储文件基础路径              
            int Num = 0;
            bool isInsertBig = false; // 是否插入大标题
            string parentId = "";
            foreach (DataRow item in dtTQ.Rows)
            {
                Num++;
                if (isInsertBig && item["type"].ToString() == "simple")
                {
                    isInsertBig = false;
                    #region 插入标题
                    string TempTQ = @"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type])
     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');";
                    string strTQ = string.Format(TempTQ
                        , parentId
                        , rtrfId_New
                        , Num
                        , "title"
                        , "0"
                        , ""
                        , ""
                        , DateTime.Now
                        , ""
                        , "0"
                        , "simple");
                    stbSql.Append(strTQ);//插入标题
                    #endregion
                    Num++;
                }
                #region 普通题
                if (item["type"].ToString() == "simple")
                {
                    #region 插入题
                    string TQSonGuid = Guid.NewGuid().ToString();
                    stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}','{4}','{5}',[type] from TestQuestions
	 where TestQuestions_Id='{6}';", TQSonGuid, rtrfId_New, Num, DateTime.Now.ToString(), item["topicNumber"].ToString(), parentId, item["TestQuestions_Id"].ToString());
                    #endregion
                    #region 存文件

                    string fileUrl = uploadPath + saveOwnerPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                    string fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "txt")
                        , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionBody", TQSonGuid, "txt"));

                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "htm")
                        , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionBody", TQSonGuid, "htm"));

                    #endregion
                    System.Data.DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                    if (drScore.Length > 0)
                    {
                        for (int jj = 0; jj < drScore.Length; jj++)
                        {
                            #region 插入分

                            string ScoreGuid = Guid.NewGuid().ToString();
                            string tempAnalyzeData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=AnalyzeData
", rtrfId_New, ScoreGuid);
                            string tempTrainData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=TrainData
", rtrfId_New, ScoreGuid);
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Score]
           ([TestQuestions_Score_ID]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_OrderNum]
           ,[TestQuestions_Score]
           ,[AnalyzeHyperlink]
           ,[AnalyzeHyperlinkData]
           ,[AnalyzeHyperlinkHtml]
           ,[AnalyzeText]
           ,[ComplexityHyperlink]
           ,[ComplexityText]
           ,[ContentHyperlink]
           ,[ContentText]
           ,[DocBase64]
           ,[DocHtml]
           ,[ScoreHyperlink]
           ,[ScoreText]
           ,[TargetHyperlink]
           ,[TrainHyperlinkData]
           ,[TrainHyperlinkHtml]
           ,[TargetText]
           ,[TestCorrect]
           ,[TestType]
           ,[TrainHyperlink]
           ,[TrainText]
           ,[TypeHyperlink]
           ,[TypeText]
           ,[CreateTime]
           ,[AreaHyperlink]
           ,[AreaText]
           ,[kaofaText]
           ,[testIndex] )
     select '{0}','{1}','{2}',TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,'{5}',AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,'{6}',TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,'{3}',AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score where TestQuestions_Score_ID='{4}' ;"
                                , ScoreGuid, rtrfId_New, TQSonGuid, DateTime.Now.ToString(), drScore[jj]["TestQuestions_Score_ID"].ToString()
                                , string.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()) ? "" : tempAnalyzeData
                                , string.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()) ? "" : tempTrainData);
                            #endregion
                            #region 存文件

                            #region 正确答案
                            //填空题解答题 下载正确答案
                            if (drScore[jj]["TestType"].ToString() == "fill" || drScore[jj]["TestType"].ToString() == "answers")
                            {
                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionCurrent", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionCurrent", ScoreGuid, "txt"));
                            }
                            #endregion
                            #region 选择题的选项
                            if (drScore[jj]["TestType"].ToString() == "selection" || drScore[jj]["TestType"].ToString() == "clozeTest")
                            {
                                pfunction.CopyToFile(string.Format(fileUrl, "testQuestionOption", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionOption", ScoreGuid, "txt"));
                            }
                            #endregion
                            #region 解析 保存文件
                            if (!String.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()))
                            {
                                pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "AnalyzeData", ScoreGuid, "txt"));

                                pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "AnalyzeHtml", ScoreGuid, "htm"));
                            }
                            #endregion
                            #region 强化训练 保存文件
                            if (!String.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()))
                            {
                                pfunction.CopyToFile(string.Format(fileUrl, "TrainData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "TrainData", ScoreGuid, "txt"));

                                pfunction.CopyToFile(string.Format(fileUrl, "TrainHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "TrainHtml", ScoreGuid, "htm"));
                            }
                            #endregion
                            #region 子题题干Html 保存文件bodySub
                            pfunction.CopyToFile(string.Format(fileUrl, "bodySub", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                    , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "bodySub", ScoreGuid, "txt"));
                            #endregion
                            #endregion
                            #region 插入选项
                            string sqlOption = "select * from TestQuestions_Option where TestQuestions_Score_ID='" + drScore[jj]["TestQuestions_Score_ID"] + "'";
                            DataTable dtOption = Rc.Common.DBUtility.DbHelperSQL.Query(sqlOption).Tables[0];
                            if (dtOption.Rows.Count > 0)
                            {
                                foreach (DataRow itemOP in dtOption.Rows)
                                {
                                    stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Option]
           ([TestQuestions_Option_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_OptionParent_OrderNum]
           ,[TestQuestions_Option_Content]
           ,[TestQuestions_Option_OrderNum]
           ,[CreateTime]
           ,[TestQuestions_Score_ID] )
     select Newid(),'{0}',TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,'{1}','{2}' from TestQuestions_Option where TestQuestions_Option_Id='{3}' ;"
                                        , TQSonGuid, DateTime.Now.ToString(), ScoreGuid, itemOP["TestQuestions_Option_Id"].ToString());
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion
                #region 综合题
                if (item["type"].ToString() == "complex")
                {
                    #region 插入综合题提干
                    #region 插入题
                    string TQSonGuid = Guid.NewGuid().ToString();
                    stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}','{4}','0',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQSonGuid, rtrfId_New, Num, DateTime.Now.ToString(), item["topicNumber"].ToString(), item["TestQuestions_Id"].ToString());
                    #endregion
                    #region 存文件
                    string fileUrl = uploadPath + saveOwnerPath + "{0}\\" + savePath + "{1}.{2}";//文件详细路径
                    string fileloactionurl = uploadPath + "{0}\\{1}\\" + "{2}.{3}";

                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "txt")
                        , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionBody", TQSonGuid, "txt"));

                    pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "htm")
                        , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionBody", TQSonGuid, "htm"));
                    #endregion
                    #endregion
                    #region 综合题小题
                    string sql = @" select tq.*,rtf.Resource_Class,rtf.ParticularYear,rtf.GradeTerm,rtf.Resource_Version,rtf.Subject 
from TestQuestions tq 
inner join ResourceToResourceFolder rtf on rtf.ResourceToResourceFolder_Id=tq.ResourceToResourceFolder_Id
where Parent_Id='" + item["TestQuestions_id"].ToString() + "'order by TestQuestions_Num";
                    DataTable dtBigTQ = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    string sqlScore = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex,tqs.AnalyzeHyperlinkData,tqs.TrainHyperlinkData,tqs.TestType from TestQuestions_Score tqs where tqs.TestQuestions_Id in(select TestQuestions_Id from TestQuestions 
where Parent_Id='" + item["TestQuestions_id"].ToString() + "')";
                    DataTable dtBigScore = Rc.Common.DBUtility.DbHelperSQL.Query(sqlScore).Tables[0];
                    if (dtBigTQ.Rows.Count > 0)
                    {
                        foreach (DataRow itemBigTQ in dtBigTQ.Rows)
                        {
                            Num++;
                            #region 插入题
                            string TQBGuid = Guid.NewGuid().ToString();
                            stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions]
           ([TestQuestions_Id]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_Type]
           ,[TestQuestions_SumScore]
           ,[TestQuestions_Content]
           ,[TestQuestions_Answer]
           ,[CreateTime]
           ,[topicNumber]
           ,[Parent_Id]
           ,[type] )
     select '{0}','{1}','{2}',TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,'{3}',topicNumber,'{4}',[type] from TestQuestions
	 where TestQuestions_Id='{5}';", TQBGuid, rtrfId_New, Num, DateTime.Now.ToString(), TQSonGuid, itemBigTQ["TestQuestions_Id"].ToString());
                            #endregion
                            #region 存文件
                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemBigTQ["TestQuestions_Id"], "txt")
                                , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionBody", TQBGuid, "txt"));

                            pfunction.CopyToFile(string.Format(fileUrl, "testQuestionBody", itemBigTQ["TestQuestions_Id"], "htm")
                                , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionBody", TQBGuid, "htm"));
                            #endregion
                            System.Data.DataRow[] drScore = dtBigScore.Select("TestQuestions_Id='" + itemBigTQ["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");
                            if (drScore.Length > 0)
                            {
                                for (int jj = 0; jj < drScore.Length; jj++)
                                {
                                    #region 插入分

                                    string ScoreGuid = Guid.NewGuid().ToString();
                                    string tempAnalyzeData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=AnalyzeData
", rtrfId_New, ScoreGuid);
                                    string tempTrainData = string.Format(@"/authApi/?key=getTestAttr&resourceId={0}&testId={1}&attrType=TrainData
", rtrfId_New, ScoreGuid);
                                    stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Score]
           ([TestQuestions_Score_ID]
           ,[ResourceToResourceFolder_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_Num]
           ,[TestQuestions_OrderNum]
           ,[TestQuestions_Score]
           ,[AnalyzeHyperlink]
           ,[AnalyzeHyperlinkData]
           ,[AnalyzeHyperlinkHtml]
           ,[AnalyzeText]
           ,[ComplexityHyperlink]
           ,[ComplexityText]
           ,[ContentHyperlink]
           ,[ContentText]
           ,[DocBase64]
           ,[DocHtml]
           ,[ScoreHyperlink]
           ,[ScoreText]
           ,[TargetHyperlink]
           ,[TrainHyperlinkData]
           ,[TrainHyperlinkHtml]
           ,[TargetText]
           ,[TestCorrect]
           ,[TestType]
           ,[TrainHyperlink]
           ,[TrainText]
           ,[TypeHyperlink]
           ,[TypeText]
           ,[CreateTime]
           ,[AreaHyperlink]
           ,[AreaText]
           ,[kaofaText]
           ,[testIndex] )
     select '{0}','{1}','{2}',TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,'{5}',AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,'{6}',TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,'{3}',AreaHyperlink,AreaText,kaofaText,testIndex from TestQuestions_Score where TestQuestions_Score_ID='{4}' ;"
                                        , ScoreGuid, rtrfId_New, TQBGuid, DateTime.Now.ToString(), drScore[jj]["TestQuestions_Score_ID"].ToString()
                                        , string.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()) ? "" : tempAnalyzeData
                                        , string.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()) ? "" : tempTrainData);
                                    #endregion
                                    #region 存文件

                                    #region 正确答案
                                    //不是选择题填空判断下载正确答案
                                    if (drScore[jj]["TestType"].ToString() == "fill" || drScore[jj]["TestType"].ToString() == "answers")
                                    {
                                        pfunction.CopyToFile(string.Format(fileUrl, "testQuestionCurrent", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                            , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionCurrent", ScoreGuid, "txt"));
                                    }
                                    #endregion
                                    #region 选择题的选项
                                    if (drScore[jj]["TestType"].ToString() == "selection" || drScore[jj]["TestType"].ToString() == "clozeTest")
                                    {
                                        pfunction.CopyToFile(string.Format(fileUrl, "testQuestionOption", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                            , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "testQuestionOption", ScoreGuid, "txt"));
                                    }
                                    #endregion
                                    #region 解析 保存文件
                                    if (!String.IsNullOrEmpty(drScore[jj]["AnalyzeHyperlinkData"].ToString()))
                                    {
                                        pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                            , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "AnalyzeData", ScoreGuid, "txt"));

                                        pfunction.CopyToFile(string.Format(fileUrl, "AnalyzeHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                            , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "AnalyzeHtml", ScoreGuid, "htm"));
                                    }
                                    #endregion
                                    #region 强化训练 保存文件
                                    if (!String.IsNullOrEmpty(drScore[jj]["TrainHyperlinkData"].ToString()))
                                    {
                                        pfunction.CopyToFile(string.Format(fileUrl, "TrainData", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                               , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "TrainData", ScoreGuid, "txt"));

                                        pfunction.CopyToFile(string.Format(fileUrl, "TrainHtml", drScore[jj]["TestQuestions_Score_ID"], "htm")
                                            , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "TrainHtml", ScoreGuid, "htm"));
                                    }
                                    #endregion
                                    #region 子题题干Html 保存文件bodySub
                                    pfunction.CopyToFile(string.Format(fileUrl, "bodySub", drScore[jj]["TestQuestions_Score_ID"], "txt")
                                               , string.Format(fileloactionurl, pfunction.ToShortDate(rTime), "bodySub", ScoreGuid, "txt"));
                                    #endregion
                                    #endregion
                                    #region 插入选项
                                    string sqlOption = "select * from TestQuestions_Option where TestQuestions_Score_ID='" + drScore[jj]["TestQuestions_Score_ID"] + "'";
                                    DataTable dtOption = Rc.Common.DBUtility.DbHelperSQL.Query(sqlOption).Tables[0];
                                    if (dtOption.Rows.Count > 0)
                                    {
                                        foreach (DataRow itemOP in dtOption.Rows)
                                        {
                                            stbSql.AppendFormat(@"INSERT INTO [dbo].[TestQuestions_Option]
           ([TestQuestions_Option_Id]
           ,[TestQuestions_Id]
           ,[TestQuestions_OptionParent_OrderNum]
           ,[TestQuestions_Option_Content]
           ,[TestQuestions_Option_OrderNum]
           ,[CreateTime]
           ,[TestQuestions_Score_ID] )
     select Newid(),'{0}',TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,'{1}','{2}' from TestQuestions_Option where TestQuestions_Option_Id='{3}' ;"
                                                , TQBGuid, DateTime.Now.ToString(), ScoreGuid, itemOP["TestQuestions_Option_Id"].ToString());
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                    isInsertBig = true;
                    parentId = Guid.NewGuid().ToString();
                }
                #endregion
            }
            AddLogInfo(string.Format("--开始生成新试卷--{0}--结束生成新试卷--", stbSql.ToString()), true);
            int k = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(stbSql.ToString());
            #endregion

            #region 布置作业表
            model.SubjectId = modelAssign.SubjectId;
            model.HomeWork_Id = modelAssign.HomeWork_Id;
            model.ResourceToResourceFolder_Id = rtrfId_New;
            model.HomeWork_Name = modelAssign.HomeWork_Name;
            model.HomeWork_AssignTeacher = modelAssign.HomeWork_AssignTeacher;
            model.isTimeLimt = modelAssign.isTimeLimt;
            model.BeginTime = modelAssign.BeginTime;
            model.isTimeLength = modelAssign.isTimeLength;
            model.IsHide = modelAssign.IsHide;
            model.HomeWork_Status = modelAssign.HomeWork_Status;
            model.CreateTime = modelAssign.CreateTime;
            model.UserGroup_Id = modelAssign.UserGroup_Id;
            model.IsShowAnswer = modelAssign.IsShowAnswer;
            model.IsCountdown = modelAssign.IsCountdown;
            model.rtrfId_Old = modelAssign.rtrfId_Old;
            #endregion

            #region 学生作业表
            string strWhere = string.Format(" User_ApplicationStatus='passed' and UserStatus='0' and UserGroup_Id='{0}' and MembershipEnum='{1}' ", modelAssign.UserGroup_Id, MembershipEnum.student);
            DataTable dtStu = new BLL_UserGroup_Member().GetList(strWhere).Tables[0];
            List<Model_Student_HomeWork> listSHomwWork = new List<Model_Student_HomeWork>();
            List<Model_Student_HomeWork_Submit> listSHomwWorkSubmit = new List<Model_Student_HomeWork_Submit>();
            List<Model_Student_HomeWork_Correct> listSHomwWorkCorrect = new List<Model_Student_HomeWork_Correct>();
            if (dtStu.Rows.Count == 0)
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题（学校服务器）：班级暂无学生");
                return false;
            }
            foreach (DataRow item in dtStu.Rows)
            {
                string ShwGuid = Guid.NewGuid().ToString();
                Model_Student_HomeWork modelSHomeWork = new Model_Student_HomeWork();
                modelSHomeWork.Student_HomeWork_Id = ShwGuid;
                modelSHomeWork.HomeWork_Id = modelAssign.HomeWork_Id;
                modelSHomeWork.Student_Id = item["User_ID"].ToString();
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
            //modelSH_HW.Correct_Time = null;
            modelSH_HW.Exec_Status = "0";
            modelSH_HW.SType = "1";
            modelSH_HW.CreateUser = modelAssign.HomeWork_AssignTeacher;
            //modelSH_HW.Exec_Time = null;
            modelSH_HW.SchoolId = dtHWDetail.Rows[0]["SchoolId"].ToString();
            modelSH_HW.GradeId = dtHWDetail.Rows[0]["GradeId"].ToString();
            modelSH_HW.ClassId = modelAssign.UserGroup_Id;
            modelSH_HW.TeacherId = modelAssign.HomeWork_AssignTeacher;
            modelSH_HW.HW_CreateTime = modelAssign.CreateTime;

            #endregion

            Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题（学校服务器）：4开始写入数据");
            if (bll.AddHomework(model, listSHomwWork, listSHomwWorkSubmit, listSHomwWorkCorrect, modelSH_HW))
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题（学校服务器）：4结束写入数据");
                if (GenerateTestPaperFileForStudent(modelAssign.HomeWork_AssignTeacher, modelAssign.HomeWork_Id))
                {
                    flag = true;
                }
                else
                {
                    //RevokeHW 生成学生作业文件失败 撤销作业
                    new BLL_HomeWork().RevokeHW(model.HomeWork_Id);
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题失败（学校服务器）：生成学生作业文件失败");
                }
            }
            else
            {
                //RevokeHW 写入数据失败 撤销作业
                new BLL_HomeWork().RevokeHW(model.HomeWork_Id);
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(modelAssign.HomeWork_AssignTeacher, "", "布置试题失败（学校服务器）：写入数据失败");
            }
            return flag;
        }

        public class tchAssignModel
        {
            /// <summary>
            /// 试题
            /// </summary>
            public string tqInfo { get; set; }
            /// <summary>
            /// 原资源标识
            /// </summary>
            public string rtrfId_Old { get; set; }
            public string HomeWork_Id { get; set; }
            public string HomeWork_Name { get; set; }
            public string HomeWork_AssignTeacher { get; set; }
            public DateTime? BeginTime { get; set; }
            public int IsHide { get; set; }
            public int HomeWork_Status { get; set; }
            public DateTime? CreateTime { get; set; }
            public string UserGroup_Id { get; set; }
            public int isTimeLimt { get; set; }
            public int isTimeLength { get; set; }
            public string SubjectId { get; set; }
            public int IsShowAnswer { get; set; }
            public string IsCountdown { get; set; }

        }

        /// <summary>
        /// 生成学生作业txt 17-07-17TS
        /// </summary>
        private bool GenerateTestPaperFileForStudent(string tchId, string HomeWork_Id)
        {
            bool flag = false;
            try
            {
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：5开始组织试题");
                string uploadPath = Server.MapPath("/Upload/Resource/");//存储文件基础路径
                //string strTestWebSiteUrl = ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl");//学校局域网地址
                //检测网络是否通畅
                //if (RemotWeb.PostDataToServer(strTestWebSiteUrl + "/AuthApi/index.aspx?key=onlinecheck", "", Encoding.UTF8, "Get") != "ok")
                //{
                //    Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：网络不通畅-" + strTestWebSiteUrl);
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
                            //    strtestCorrectBase64 = File.ReadAllText(uploadPath + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt");
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
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：5结束组织试题");
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：6开始写入文件");
                pfunction.WriteToFile(uploadPath + "studentPaper\\" + filePath, strJson, true);

                string strJsonForTch = string.Empty;
                strJsonForTch = JsonConvert.SerializeObject(new
                {
                    list = listTQObjForTch,
                    listBig = listTQObjForTchBig
                });
                pfunction.WriteToFile(uploadPath + "studentPaper\\" + filePathForTch, strJsonForTch, true);
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：6结束写入文件");
                flag = true;
                #region 判断作业文件是否存在，不存在则返回false
                if (!File.Exists(uploadPath + "studentPaper\\" + filePath)
                    || !File.Exists(uploadPath + "studentPaper\\" + filePathForTch))
                {
                    flag = false;
                    Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：作业文件不存在，布置失败");
                }
                #endregion

            }
            catch (Exception ex)
            {
                flag = false;
                Rc.Common.SystemLog.SystemLog.AddLogFromBS(tchId, "", "布置试题（学校服务器）：异常，布置失败");
                Rc.Common.SystemLog.SystemLog.AddLogFromBS("", "", "布置试题失败（学校服务器）：" + ex.Message.ToString());
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

        private void AddLogInfo(string txt, bool isAppend)
        {
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\tchAssignTQDataLog\\";
            DateTime CurrTime = DateTime.Now;

            //拼接日志完整路径
            logPath = logPath + CurrTime.ToString("yyyy-MM-dd") + "\\log.txt";
            string strDirecory = logPath.Substring(0, logPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();
            }

            StreamWriter fs;
            if (isAppend) fs = File.AppendText(logPath);
            else fs = File.CreateText(logPath);

            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

    }
}