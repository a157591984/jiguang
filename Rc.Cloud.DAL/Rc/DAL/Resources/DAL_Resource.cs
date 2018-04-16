namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using Rc.Model.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DAL_Resource
    {
        public bool Add(Model_Resource model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Resource(");
            builder.Append("Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength)");
            builder.Append(" values (");
            builder.Append("@Resource_Id,@Resource_MD5,@Resource_DataStrem,@Resource_ContentHtml,@CreateTime,@Resource_ContentLength)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_MD5", SqlDbType.Char, 0x20), new SqlParameter("@Resource_DataStrem", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Resource_ContentHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Resource_ContentLength", SqlDbType.Decimal, 9) };
            cmdParms[0].Value = model.Resource_Id;
            cmdParms[1].Value = model.Resource_MD5;
            cmdParms[2].Value = model.Resource_DataStrem;
            cmdParms[3].Value = model.Resource_ContentHtml;
            cmdParms[4].Value = model.CreateTime;
            cmdParms[5].Value = model.Resource_ContentLength;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public int ClientUploadScienceWord(Model_Resource modelResoucrce, Model_ResourceToResourceFolder modelRTRFolder, List<Model_ResourceToResourceFolder_img> listModelRTRF_img, Model_BookProductionLog modelBPL)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into Resource(");
            builder.Append("Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength)");
            builder.Append(" values (");
            builder.Append("@Resource_Id,@Resource_MD5,@Resource_DataStrem,@Resource_ContentHtml,@CreateTime,@Resource_ContentLength)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_MD5", SqlDbType.Char, 0x20), new SqlParameter("@Resource_DataStrem", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Resource_ContentHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Resource_ContentLength", SqlDbType.Decimal, 9) };
            parameterArray[0].Value = modelResoucrce.Resource_Id;
            parameterArray[1].Value = modelResoucrce.Resource_MD5;
            parameterArray[2].Value = modelResoucrce.Resource_DataStrem;
            parameterArray[3].Value = modelResoucrce.Resource_ContentHtml;
            parameterArray[4].Value = modelResoucrce.CreateTime;
            parameterArray[5].Value = modelResoucrce.Resource_ContentLength;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder(");
            builder.Append("ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,GradeTerm,Subject,Resource_Domain,Resource_Url,Book_ID,ParticularYear,ResourceToResourceFolder_Order)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_Id,@ResourceFolder_Id,@Resource_Id,@File_Name,@Resource_Type,@Resource_Name,@Resource_Class,@Resource_Version,@File_Owner,@CreateFUser,@CreateTime,@UpdateTime,@File_Suffix,@GradeTerm,@Subject,@Resource_Domain,@Resource_Url,@Book_ID,@ParticularYear,@ResourceToResourceFolder_Order)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Domain", SqlDbType.VarChar, 200), 
                new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Order", SqlDbType.Int, 4)
             };
            parameterArray2[0].Value = modelRTRFolder.ResourceToResourceFolder_Id;
            parameterArray2[1].Value = modelRTRFolder.ResourceFolder_Id;
            parameterArray2[2].Value = modelRTRFolder.Resource_Id;
            parameterArray2[3].Value = modelRTRFolder.File_Name;
            parameterArray2[4].Value = modelRTRFolder.Resource_Type;
            parameterArray2[5].Value = modelRTRFolder.Resource_Name;
            parameterArray2[6].Value = modelRTRFolder.Resource_Class;
            parameterArray2[7].Value = modelRTRFolder.Resource_Version;
            parameterArray2[8].Value = modelRTRFolder.File_Owner;
            parameterArray2[9].Value = modelRTRFolder.CreateFUser;
            parameterArray2[10].Value = modelRTRFolder.CreateTime;
            parameterArray2[11].Value = modelRTRFolder.UpdateTime;
            parameterArray2[12].Value = modelRTRFolder.File_Suffix;
            parameterArray2[13].Value = modelRTRFolder.GradeTerm;
            parameterArray2[14].Value = modelRTRFolder.Subject;
            parameterArray2[15].Value = modelRTRFolder.Resource_Domain;
            parameterArray2[0x10].Value = modelRTRFolder.Resource_Url;
            parameterArray2[0x11].Value = modelRTRFolder.Book_ID;
            parameterArray2[0x12].Value = modelRTRFolder.ParticularYear;
            parameterArray2[0x13].Value = modelRTRFolder.ResourceToResourceFolder_Order;
            dictionary.Add(builder.ToString(), parameterArray2);
            int num = 0;
            foreach (Model_ResourceToResourceFolder_img _img in listModelRTRF_img)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into ResourceToResourceFolder_img(");
                builder.Append("ResourceToResourceFolder_img_id,ResourceToResourceFolder_id,ResourceToResourceFolderImg_Url,ResourceToResourceFolderImg_Order,CreateTime)");
                builder.Append(" values (");
                builder.Append("@ResourceToResourceFolder_img_id,@ResourceToResourceFolder_id,@ResourceToResourceFolderImg_Url,@ResourceToResourceFolderImg_Order,@CreateTime)");
                SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_img_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolderImg_Url", SqlDbType.VarChar, 500), new SqlParameter("@ResourceToResourceFolderImg_Order", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime) };
                parameterArray3[0].Value = _img.ResourceToResourceFolder_img_id;
                parameterArray3[1].Value = _img.ResourceToResourceFolder_id;
                parameterArray3[2].Value = _img.ResourceToResourceFolderImg_Url;
                parameterArray3[3].Value = _img.ResourceToResourceFolderImg_Order;
                parameterArray3[4].Value = _img.CreateTime;
                dictionary.Add(builder.ToString(), parameterArray3);
            }
            builder = new StringBuilder();
            builder.Append("insert into BookProductionLog(");
            builder.Append("BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark)");
            builder.Append(" values (");
            builder.Append("@BookProductionLog_Id,@BookId,@ResourceToResourceFolder_Id,@ParticularYear,@Resource_Type,@LogTypeEnum,@CreateUser,@CreateTime,@LogRemark)");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200) };
            parameterArray4[0].Value = modelBPL.BookProductionLog_Id;
            parameterArray4[1].Value = modelBPL.BookId;
            parameterArray4[2].Value = modelBPL.ResourceToResourceFolder_Id;
            parameterArray4[3].Value = modelBPL.ParticularYear;
            parameterArray4[4].Value = modelBPL.Resource_Type;
            parameterArray4[5].Value = modelBPL.LogTypeEnum;
            parameterArray4[6].Value = modelBPL.CreateUser;
            parameterArray4[7].Value = modelBPL.CreateTime;
            parameterArray4[8].Value = modelBPL.LogRemark;
            dictionary.Add(builder.ToString(), parameterArray4);
            object obj2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int ClientUploadTestPaper(Model_Resource modelResoucrce, Model_ResourceToResourceFolder modelRTRFolder, Model_ResourceToResourceFolder_Property modelRTRFPropety, List<Model_TestQuestions> listModelTQ, List<Model_TestQuestions_Score> listModelTQScore, List<Model_TestQuestions_Option> listModelTQOption, Model_BookProductionLog modelBPL)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("insert into Resource(");
            builder.Append("Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength)");
            builder.Append(" values (");
            builder.Append("@Resource_Id,@Resource_MD5,@Resource_DataStrem,@Resource_ContentHtml,@CreateTime,@Resource_ContentLength)");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_MD5", SqlDbType.Char, 0x20), new SqlParameter("@Resource_DataStrem", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Resource_ContentHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Resource_ContentLength", SqlDbType.Decimal, 9) };
            parameterArray[0].Value = modelResoucrce.Resource_Id;
            parameterArray[1].Value = modelResoucrce.Resource_MD5;
            parameterArray[2].Value = modelResoucrce.Resource_DataStrem;
            parameterArray[3].Value = modelResoucrce.Resource_ContentHtml;
            parameterArray[4].Value = modelResoucrce.CreateTime;
            parameterArray[5].Value = modelResoucrce.Resource_ContentLength;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder(");
            builder.Append("ResourceToResourceFolder_Id,ResourceFolder_Id,Resource_Id,File_Name,Resource_Type,Resource_Name,Resource_Class,Resource_Version,File_Owner,CreateFUser,CreateTime,UpdateTime,File_Suffix,LessonPlan_Type,GradeTerm,Subject,Resource_Domain,Resource_Url,Resource_shared,Book_ID,ParticularYear,ResourceToResourceFolder_Order)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_Id,@ResourceFolder_Id,@Resource_Id,@File_Name,@Resource_Type,@Resource_Name,@Resource_Class,@Resource_Version,@File_Owner,@CreateFUser,@CreateTime,@UpdateTime,@File_Suffix,@LessonPlan_Type,@GradeTerm,@Subject,@Resource_Domain,@Resource_Url,@Resource_shared,@Book_ID,@ParticularYear,@ResourceToResourceFolder_Order)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), 
                new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@ResourceToResourceFolder_Order", SqlDbType.Int, 4)
             };
            parameterArray2[0].Value = modelRTRFolder.ResourceToResourceFolder_Id;
            parameterArray2[1].Value = modelRTRFolder.ResourceFolder_Id;
            parameterArray2[2].Value = modelRTRFolder.Resource_Id;
            parameterArray2[3].Value = modelRTRFolder.File_Name;
            parameterArray2[4].Value = modelRTRFolder.Resource_Type;
            parameterArray2[5].Value = modelRTRFolder.Resource_Name;
            parameterArray2[6].Value = modelRTRFolder.Resource_Class;
            parameterArray2[7].Value = modelRTRFolder.Resource_Version;
            parameterArray2[8].Value = modelRTRFolder.File_Owner;
            parameterArray2[9].Value = modelRTRFolder.CreateFUser;
            parameterArray2[10].Value = modelRTRFolder.CreateTime;
            parameterArray2[11].Value = modelRTRFolder.UpdateTime;
            parameterArray2[12].Value = modelRTRFolder.File_Suffix;
            parameterArray2[13].Value = modelRTRFolder.LessonPlan_Type;
            parameterArray2[14].Value = modelRTRFolder.GradeTerm;
            parameterArray2[15].Value = modelRTRFolder.Subject;
            parameterArray2[0x10].Value = modelRTRFolder.Resource_Domain;
            parameterArray2[0x11].Value = modelRTRFolder.Resource_Url;
            parameterArray2[0x12].Value = modelRTRFolder.Resource_shared;
            parameterArray2[0x13].Value = modelRTRFolder.Book_ID;
            parameterArray2[20].Value = modelRTRFolder.ParticularYear;
            parameterArray2[0x15].Value = modelRTRFolder.ResourceToResourceFolder_Order;
            dictionary.Add(builder.ToString(), parameterArray2);
            builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder_Property(");
            builder.Append("ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_Id,@BooksCode,@BooksUnitCode,@GuidDoc,@TestPaperName,@CreateTime,@paperHeaderDoc,@paperHeaderHtml)");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@BooksCode", SqlDbType.VarChar, 50), new SqlParameter("@BooksUnitCode", SqlDbType.VarChar, 50), new SqlParameter("@GuidDoc", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestPaperName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@paperHeaderDoc", SqlDbType.VarChar, 0x3e8), new SqlParameter("@paperHeaderHtml", SqlDbType.VarChar, 0x3e8) };
            parameterArray3[0].Value = modelRTRFPropety.ResourceToResourceFolder_Id;
            parameterArray3[1].Value = modelRTRFPropety.BooksCode;
            parameterArray3[2].Value = modelRTRFPropety.BooksUnitCode;
            parameterArray3[3].Value = modelRTRFPropety.GuidDoc;
            parameterArray3[4].Value = modelRTRFPropety.TestPaperName;
            parameterArray3[5].Value = modelRTRFPropety.CreateTime;
            parameterArray3[6].Value = modelRTRFPropety.paperHeaderDoc;
            parameterArray3[7].Value = modelRTRFPropety.paperHeaderHtml;
            dictionary.Add(builder.ToString(), parameterArray3);
            int num = 0;
            foreach (Model_TestQuestions questions in listModelTQ)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into TestQuestions(");
                builder.Append("TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,type)");
                builder.Append(" values (");
                builder.Append("@TestQuestions_Id,@ResourceToResourceFolder_Id,@TestQuestions_Num,@TestQuestions_Type,@TestQuestions_SumScore,@TestQuestions_Content,@TestQuestions_Answer,@CreateTime,@topicNumber,@Parent_Id,@type)");
                SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TestQuestions_SumScore", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestions_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Answer", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@type", SqlDbType.VarChar, 50) };
                parameterArray4[0].Value = questions.TestQuestions_Id;
                parameterArray4[1].Value = questions.ResourceToResourceFolder_Id;
                parameterArray4[2].Value = questions.TestQuestions_Num;
                parameterArray4[3].Value = questions.TestQuestions_Type;
                parameterArray4[4].Value = questions.TestQuestions_SumScore;
                parameterArray4[5].Value = questions.TestQuestions_Content;
                parameterArray4[6].Value = questions.TestQuestions_Answer;
                parameterArray4[7].Value = questions.CreateTime;
                parameterArray4[8].Value = questions.topicNumber;
                parameterArray4[9].Value = questions.Parent_Id;
                parameterArray4[10].Value = questions.type;
                dictionary.Add(builder.ToString(), parameterArray4);
            }
            foreach (Model_TestQuestions_Score score in listModelTQScore)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into TestQuestions_Score(");
                builder.Append("TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex)");
                builder.Append(" values (");
                builder.Append("@TestQuestions_Score_ID,@ResourceToResourceFolder_Id,@TestQuestions_Id,@TestQuestions_Num,@TestQuestions_OrderNum,@TestQuestions_Score,@AnalyzeHyperlink,@AnalyzeHyperlinkData,@AnalyzeHyperlinkHtml,@AnalyzeText,@ComplexityHyperlink,@ComplexityText,@ContentHyperlink,@ContentText,@DocBase64,@DocHtml,@ScoreHyperlink,@ScoreText,@TargetHyperlink,@TrainHyperlinkData,@TrainHyperlinkHtml,@TargetText,@TestCorrect,@TestType,@TrainHyperlink,@TrainText,@TypeHyperlink,@TypeText,@CreateTime,@AreaHyperlink,@AreaText,@kaofaText,@testIndex)");
                SqlParameter[] parameterArray5 = new SqlParameter[] { 
                    new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Score", SqlDbType.Decimal, 5), new SqlParameter("@AnalyzeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocBase64", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocHtml", SqlDbType.NVarChar, 0x3e8), 
                    new SqlParameter("@ScoreHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ScoreText", SqlDbType.NVarChar, 10), new SqlParameter("@TargetHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TargetText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestCorrect", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestType", SqlDbType.VarChar, 50), new SqlParameter("@TrainHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeText", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AreaHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AreaText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@kaofaText", SqlDbType.NVarChar, 0x3e8), 
                    new SqlParameter("@testIndex", SqlDbType.VarChar, 50)
                 };
                parameterArray5[0].Value = score.TestQuestions_Score_ID;
                parameterArray5[1].Value = score.ResourceToResourceFolder_Id;
                parameterArray5[2].Value = score.TestQuestions_Id;
                parameterArray5[3].Value = score.TestQuestions_Num;
                parameterArray5[4].Value = score.TestQuestions_OrderNum;
                parameterArray5[5].Value = score.TestQuestions_Score;
                parameterArray5[6].Value = score.AnalyzeHyperlink;
                parameterArray5[7].Value = score.AnalyzeHyperlinkData;
                parameterArray5[8].Value = score.AnalyzeHyperlinkHtml;
                parameterArray5[9].Value = score.AnalyzeText;
                parameterArray5[10].Value = score.ComplexityHyperlink;
                parameterArray5[11].Value = score.ComplexityText;
                parameterArray5[12].Value = score.ContentHyperlink;
                parameterArray5[13].Value = score.ContentText;
                parameterArray5[14].Value = score.DocBase64;
                parameterArray5[15].Value = score.DocHtml;
                parameterArray5[0x10].Value = score.ScoreHyperlink;
                parameterArray5[0x11].Value = score.ScoreText;
                parameterArray5[0x12].Value = score.TargetHyperlink;
                parameterArray5[0x13].Value = score.TrainHyperlinkData;
                parameterArray5[20].Value = score.TrainHyperlinkHtml;
                parameterArray5[0x15].Value = score.TargetText;
                parameterArray5[0x16].Value = score.TestCorrect;
                parameterArray5[0x17].Value = score.TestType;
                parameterArray5[0x18].Value = score.TrainHyperlink;
                parameterArray5[0x19].Value = score.TrainText;
                parameterArray5[0x1a].Value = score.TypeHyperlink;
                parameterArray5[0x1b].Value = score.TypeText;
                parameterArray5[0x1c].Value = score.CreateTime;
                parameterArray5[0x1d].Value = score.AreaHyperlink;
                parameterArray5[30].Value = score.AreaText;
                parameterArray5[0x1f].Value = score.kaofaText;
                parameterArray5[0x20].Value = score.testIndex;
                dictionary.Add(builder.ToString(), parameterArray5);
            }
            foreach (Model_TestQuestions_Option option in listModelTQOption)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into TestQuestions_Option(");
                builder.Append("TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID)");
                builder.Append(" values (");
                builder.Append("@TestQuestions_Option_Id,@TestQuestions_Id,@TestQuestions_OptionParent_OrderNum,@TestQuestions_Option_Content,@TestQuestions_Option_OrderNum,@CreateTime,@TestQuestions_Score_ID)");
                SqlParameter[] parameterArray6 = new SqlParameter[] { new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_OptionParent_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Option_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Option_OrderNum", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24) };
                parameterArray6[0].Value = option.TestQuestions_Option_Id;
                parameterArray6[1].Value = option.TestQuestions_Id;
                parameterArray6[2].Value = option.TestQuestions_OptionParent_OrderNum;
                parameterArray6[3].Value = option.TestQuestions_Option_Content;
                parameterArray6[4].Value = option.TestQuestions_Option_OrderNum;
                parameterArray6[5].Value = option.CreateTime;
                parameterArray6[6].Value = option.TestQuestions_Score_ID;
                dictionary.Add(builder.ToString(), parameterArray6);
            }
            builder = new StringBuilder();
            builder.Append("insert into BookProductionLog(");
            builder.Append("BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark)");
            builder.Append(" values (");
            builder.Append("@BookProductionLog_Id,@BookId,@ResourceToResourceFolder_Id,@ParticularYear,@Resource_Type,@LogTypeEnum,@CreateUser,@CreateTime,@LogRemark)");
            SqlParameter[] parameterArray7 = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200) };
            parameterArray7[0].Value = modelBPL.BookProductionLog_Id;
            parameterArray7[1].Value = modelBPL.BookId;
            parameterArray7[2].Value = modelBPL.ResourceToResourceFolder_Id;
            parameterArray7[3].Value = modelBPL.ParticularYear;
            parameterArray7[4].Value = modelBPL.Resource_Type;
            parameterArray7[5].Value = modelBPL.LogTypeEnum;
            parameterArray7[6].Value = modelBPL.CreateUser;
            parameterArray7[7].Value = modelBPL.CreateTime;
            parameterArray7[8].Value = modelBPL.LogRemark;
            dictionary.Add(builder.ToString(), parameterArray7);
            object obj2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int ClientUploadTestPaperUpdate(Model_Resource modelResoucrce, Model_ResourceToResourceFolder modelRTRFolder, Model_ResourceToResourceFolder_Property modelRTRFPropety, List<Model_TestQuestions> listModelTQ, List<Model_TestQuestions_Score> listModelTQScore, List<Model_TestQuestions_Option> listModelTQOption, Model_BookProductionLog modelBPL)
        {
            Dictionary<string, SqlParameter[]> dictionary = new Dictionary<string, SqlParameter[]>();
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("update Resource set ");
            builder.Append("Resource_MD5=@Resource_MD5,");
            builder.Append("Resource_DataStrem=@Resource_DataStrem,");
            builder.Append("Resource_ContentHtml=@Resource_ContentHtml,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Resource_ContentLength=@Resource_ContentLength");
            builder.Append(" where Resource_Id=@Resource_Id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@Resource_MD5", SqlDbType.Char, 0x20), new SqlParameter("@Resource_DataStrem", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Resource_ContentHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Resource_ContentLength", SqlDbType.Decimal, 9), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24) };
            parameterArray[0].Value = modelResoucrce.Resource_MD5;
            parameterArray[1].Value = modelResoucrce.Resource_DataStrem;
            parameterArray[2].Value = modelResoucrce.Resource_ContentHtml;
            parameterArray[3].Value = modelResoucrce.CreateTime;
            parameterArray[4].Value = modelResoucrce.Resource_ContentLength;
            parameterArray[5].Value = modelResoucrce.Resource_Id;
            dictionary.Add(builder.ToString(), parameterArray);
            builder = new StringBuilder();
            builder.Append("update ResourceToResourceFolder set ");
            builder.Append("ResourceFolder_Id=@ResourceFolder_Id,");
            builder.Append("Resource_Id=@Resource_Id,");
            builder.Append("File_Name=@File_Name,");
            builder.Append("Resource_Type=@Resource_Type,");
            builder.Append("Resource_Name=@Resource_Name,");
            builder.Append("Resource_Class=@Resource_Class,");
            builder.Append("Resource_Version=@Resource_Version,");
            builder.Append("File_Owner=@File_Owner,");
            builder.Append("CreateFUser=@CreateFUser,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("UpdateTime=@UpdateTime,");
            builder.Append("File_Suffix=@File_Suffix,");
            builder.Append("LessonPlan_Type=@LessonPlan_Type,");
            builder.Append("GradeTerm=@GradeTerm,");
            builder.Append("Subject=@Subject,");
            builder.Append("Resource_Domain=@Resource_Domain,");
            builder.Append("Resource_Url=@Resource_Url,");
            builder.Append("Resource_shared=@Resource_shared,");
            builder.Append("ParticularYear=@ParticularYear,");
            builder.Append("Book_ID=@Book_ID");
            builder.Append(" where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { 
                new SqlParameter("@ResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24), new SqlParameter("@File_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Name", SqlDbType.NVarChar, 250), new SqlParameter("@Resource_Class", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Version", SqlDbType.Char, 0x24), new SqlParameter("@File_Owner", SqlDbType.Char, 0x24), new SqlParameter("@CreateFUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@File_Suffix", SqlDbType.VarChar, 10), new SqlParameter("@LessonPlan_Type", SqlDbType.Char, 0x24), new SqlParameter("@GradeTerm", SqlDbType.Char, 0x24), new SqlParameter("@Subject", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Domain", SqlDbType.NVarChar, 100), 
                new SqlParameter("@Resource_Url", SqlDbType.VarChar, 500), new SqlParameter("@Resource_shared", SqlDbType.VarChar, 10), new SqlParameter("@ParticularYear", SqlDbType.Int), new SqlParameter("@Book_ID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24)
             };
            parameterArray2[0].Value = modelRTRFolder.ResourceFolder_Id;
            parameterArray2[1].Value = modelRTRFolder.Resource_Id;
            parameterArray2[2].Value = modelRTRFolder.File_Name;
            parameterArray2[3].Value = modelRTRFolder.Resource_Type;
            parameterArray2[4].Value = modelRTRFolder.Resource_Name;
            parameterArray2[5].Value = modelRTRFolder.Resource_Class;
            parameterArray2[6].Value = modelRTRFolder.Resource_Version;
            parameterArray2[7].Value = modelRTRFolder.File_Owner;
            parameterArray2[8].Value = modelRTRFolder.CreateFUser;
            parameterArray2[9].Value = modelRTRFolder.CreateTime;
            parameterArray2[10].Value = modelRTRFolder.UpdateTime;
            parameterArray2[11].Value = modelRTRFolder.File_Suffix;
            parameterArray2[12].Value = modelRTRFolder.LessonPlan_Type;
            parameterArray2[13].Value = modelRTRFolder.GradeTerm;
            parameterArray2[14].Value = modelRTRFolder.Subject;
            parameterArray2[15].Value = modelRTRFolder.Resource_Domain;
            parameterArray2[0x10].Value = modelRTRFolder.Resource_Url;
            parameterArray2[0x11].Value = modelRTRFolder.Resource_shared;
            parameterArray2[0x12].Value = modelRTRFolder.ParticularYear;
            parameterArray2[0x13].Value = modelRTRFolder.Book_ID;
            parameterArray2[20].Value = modelRTRFolder.ResourceToResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray2);
            builder = new StringBuilder();
            builder.Append("delete from TestQuestions_Option where TestQuestions_Id in(select TestQuestions_Id from TestQuestions where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id);");
            builder.Append("delete from TestQuestions_Score where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            builder.Append("delete from TestQuestions where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            builder.Append("delete from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24) };
            parameterArray3[0].Value = modelRTRFolder.ResourceToResourceFolder_Id;
            dictionary.Add(builder.ToString(), parameterArray3);
            builder = new StringBuilder();
            builder.Append("insert into ResourceToResourceFolder_Property(");
            builder.Append("ResourceToResourceFolder_Id,BooksCode,BooksUnitCode,GuidDoc,TestPaperName,CreateTime,paperHeaderDoc,paperHeaderHtml)");
            builder.Append(" values (");
            builder.Append("@ResourceToResourceFolder_Id,@BooksCode,@BooksUnitCode,@GuidDoc,@TestPaperName,@CreateTime,@paperHeaderDoc,@paperHeaderHtml)");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@BooksCode", SqlDbType.VarChar, 50), new SqlParameter("@BooksUnitCode", SqlDbType.VarChar, 50), new SqlParameter("@GuidDoc", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestPaperName", SqlDbType.NVarChar, 250), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@paperHeaderDoc", SqlDbType.VarChar, 0x3e8), new SqlParameter("@paperHeaderHtml", SqlDbType.VarChar, 0x3e8) };
            parameterArray4[0].Value = modelRTRFPropety.ResourceToResourceFolder_Id;
            parameterArray4[1].Value = modelRTRFPropety.BooksCode;
            parameterArray4[2].Value = modelRTRFPropety.BooksUnitCode;
            parameterArray4[3].Value = modelRTRFPropety.GuidDoc;
            parameterArray4[4].Value = modelRTRFPropety.TestPaperName;
            parameterArray4[5].Value = modelRTRFPropety.CreateTime;
            parameterArray4[6].Value = modelRTRFPropety.paperHeaderDoc;
            parameterArray4[7].Value = modelRTRFPropety.paperHeaderHtml;
            dictionary.Add(builder.ToString(), parameterArray4);
            int num = 0;
            foreach (Model_TestQuestions questions in listModelTQ)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into TestQuestions(");
                builder.Append("TestQuestions_Id,ResourceToResourceFolder_Id,TestQuestions_Num,TestQuestions_Type,TestQuestions_SumScore,TestQuestions_Content,TestQuestions_Answer,CreateTime,topicNumber,Parent_Id,type)");
                builder.Append(" values (");
                builder.Append("@TestQuestions_Id,@ResourceToResourceFolder_Id,@TestQuestions_Num,@TestQuestions_Type,@TestQuestions_SumScore,@TestQuestions_Content,@TestQuestions_Answer,@CreateTime,@topicNumber,@Parent_Id,@type)");
                SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Type", SqlDbType.VarChar, 50), new SqlParameter("@TestQuestions_SumScore", SqlDbType.Decimal, 5), new SqlParameter("@TestQuestions_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Answer", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@topicNumber", SqlDbType.NVarChar, 50), new SqlParameter("@Parent_Id", SqlDbType.Char, 0x24), new SqlParameter("@type", SqlDbType.VarChar, 50) };
                parameterArray5[0].Value = questions.TestQuestions_Id;
                parameterArray5[1].Value = questions.ResourceToResourceFolder_Id;
                parameterArray5[2].Value = questions.TestQuestions_Num;
                parameterArray5[3].Value = questions.TestQuestions_Type;
                parameterArray5[4].Value = questions.TestQuestions_SumScore;
                parameterArray5[5].Value = questions.TestQuestions_Content;
                parameterArray5[6].Value = questions.TestQuestions_Answer;
                parameterArray5[7].Value = questions.CreateTime;
                parameterArray5[8].Value = questions.topicNumber;
                parameterArray5[9].Value = questions.Parent_Id;
                parameterArray5[10].Value = questions.type;
                dictionary.Add(builder.ToString(), parameterArray5);
            }
            foreach (Model_TestQuestions_Score score in listModelTQScore)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into TestQuestions_Score(");
                builder.Append("TestQuestions_Score_ID,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Num,TestQuestions_OrderNum,TestQuestions_Score,AnalyzeHyperlink,AnalyzeHyperlinkData,AnalyzeHyperlinkHtml,AnalyzeText,ComplexityHyperlink,ComplexityText,ContentHyperlink,ContentText,DocBase64,DocHtml,ScoreHyperlink,ScoreText,TargetHyperlink,TrainHyperlinkData,TrainHyperlinkHtml,TargetText,TestCorrect,TestType,TrainHyperlink,TrainText,TypeHyperlink,TypeText,CreateTime,AreaHyperlink,AreaText,kaofaText,testIndex)");
                builder.Append(" values (");
                builder.Append("@TestQuestions_Score_ID,@ResourceToResourceFolder_Id,@TestQuestions_Id,@TestQuestions_Num,@TestQuestions_OrderNum,@TestQuestions_Score,@AnalyzeHyperlink,@AnalyzeHyperlinkData,@AnalyzeHyperlinkHtml,@AnalyzeText,@ComplexityHyperlink,@ComplexityText,@ContentHyperlink,@ContentText,@DocBase64,@DocHtml,@ScoreHyperlink,@ScoreText,@TargetHyperlink,@TrainHyperlinkData,@TrainHyperlinkHtml,@TargetText,@TestCorrect,@TestType,@TrainHyperlink,@TrainText,@TypeHyperlink,@TypeText,@CreateTime,@AreaHyperlink,@AreaText,@kaofaText,@testIndex)");
                SqlParameter[] parameterArray6 = new SqlParameter[] { 
                    new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Num", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Score", SqlDbType.Decimal, 5), new SqlParameter("@AnalyzeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AnalyzeText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ComplexityText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ContentText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocBase64", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@DocHtml", SqlDbType.NVarChar, 0x3e8), 
                    new SqlParameter("@ScoreHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ScoreText", SqlDbType.NVarChar, 10), new SqlParameter("@TargetHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkData", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainHyperlinkHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TargetText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestCorrect", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestType", SqlDbType.VarChar, 50), new SqlParameter("@TrainHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TrainText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TypeText", SqlDbType.NVarChar, 50), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@AreaHyperlink", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@AreaText", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@kaofaText", SqlDbType.NVarChar, 0x3e8), 
                    new SqlParameter("@testIndex", SqlDbType.VarChar, 50)
                 };
                parameterArray6[0].Value = score.TestQuestions_Score_ID;
                parameterArray6[1].Value = score.ResourceToResourceFolder_Id;
                parameterArray6[2].Value = score.TestQuestions_Id;
                parameterArray6[3].Value = score.TestQuestions_Num;
                parameterArray6[4].Value = score.TestQuestions_OrderNum;
                parameterArray6[5].Value = score.TestQuestions_Score;
                parameterArray6[6].Value = score.AnalyzeHyperlink;
                parameterArray6[7].Value = score.AnalyzeHyperlinkData;
                parameterArray6[8].Value = score.AnalyzeHyperlinkHtml;
                parameterArray6[9].Value = score.AnalyzeText;
                parameterArray6[10].Value = score.ComplexityHyperlink;
                parameterArray6[11].Value = score.ComplexityText;
                parameterArray6[12].Value = score.ContentHyperlink;
                parameterArray6[13].Value = score.ContentText;
                parameterArray6[14].Value = score.DocBase64;
                parameterArray6[15].Value = score.DocHtml;
                parameterArray6[0x10].Value = score.ScoreHyperlink;
                parameterArray6[0x11].Value = score.ScoreText;
                parameterArray6[0x12].Value = score.TargetHyperlink;
                parameterArray6[0x13].Value = score.TrainHyperlinkData;
                parameterArray6[20].Value = score.TrainHyperlinkHtml;
                parameterArray6[0x15].Value = score.TargetText;
                parameterArray6[0x16].Value = score.TestCorrect;
                parameterArray6[0x17].Value = score.TestType;
                parameterArray6[0x18].Value = score.TrainHyperlink;
                parameterArray6[0x19].Value = score.TrainText;
                parameterArray6[0x1a].Value = score.TypeHyperlink;
                parameterArray6[0x1b].Value = score.TypeText;
                parameterArray6[0x1c].Value = score.CreateTime;
                parameterArray6[0x1d].Value = score.AreaHyperlink;
                parameterArray6[30].Value = score.AreaText;
                parameterArray6[0x1f].Value = score.kaofaText;
                parameterArray6[0x20].Value = score.testIndex;
                dictionary.Add(builder.ToString(), parameterArray6);
            }
            foreach (Model_TestQuestions_Option option in listModelTQOption)
            {
                num++;
                builder = new StringBuilder();
                builder.AppendFormat("select {0};", num);
                builder.Append("insert into TestQuestions_Option(");
                builder.Append("TestQuestions_Option_Id,TestQuestions_Id,TestQuestions_OptionParent_OrderNum,TestQuestions_Option_Content,TestQuestions_Option_OrderNum,CreateTime,TestQuestions_Score_ID)");
                builder.Append(" values (");
                builder.Append("@TestQuestions_Option_Id,@TestQuestions_Id,@TestQuestions_OptionParent_OrderNum,@TestQuestions_Option_Content,@TestQuestions_Option_OrderNum,@CreateTime,@TestQuestions_Score_ID)");
                SqlParameter[] parameterArray7 = new SqlParameter[] { new SqlParameter("@TestQuestions_Option_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_Id", SqlDbType.Char, 0x24), new SqlParameter("@TestQuestions_OptionParent_OrderNum", SqlDbType.Int, 4), new SqlParameter("@TestQuestions_Option_Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@TestQuestions_Option_OrderNum", SqlDbType.Int, 4), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@TestQuestions_Score_ID", SqlDbType.Char, 0x24) };
                parameterArray7[0].Value = option.TestQuestions_Option_Id;
                parameterArray7[1].Value = option.TestQuestions_Id;
                parameterArray7[2].Value = option.TestQuestions_OptionParent_OrderNum;
                parameterArray7[3].Value = option.TestQuestions_Option_Content;
                parameterArray7[4].Value = option.TestQuestions_Option_OrderNum;
                parameterArray7[5].Value = option.CreateTime;
                parameterArray7[6].Value = option.TestQuestions_Score_ID;
                dictionary.Add(builder.ToString(), parameterArray7);
            }
            builder = new StringBuilder();
            builder.Append("insert into BookProductionLog(");
            builder.Append("BookProductionLog_Id,BookId,ResourceToResourceFolder_Id,ParticularYear,Resource_Type,LogTypeEnum,CreateUser,CreateTime,LogRemark)");
            builder.Append(" values (");
            builder.Append("@BookProductionLog_Id,@BookId,@ResourceToResourceFolder_Id,@ParticularYear,@Resource_Type,@LogTypeEnum,@CreateUser,@CreateTime,@LogRemark)");
            SqlParameter[] parameterArray8 = new SqlParameter[] { new SqlParameter("@BookProductionLog_Id", SqlDbType.Char, 0x24), new SqlParameter("@BookId", SqlDbType.Char, 0x24), new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@ParticularYear", SqlDbType.Int, 4), new SqlParameter("@Resource_Type", SqlDbType.Char, 0x24), new SqlParameter("@LogTypeEnum", SqlDbType.Char, 1), new SqlParameter("@CreateUser", SqlDbType.Char, 0x24), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@LogRemark", SqlDbType.NVarChar, 200) };
            parameterArray8[0].Value = modelBPL.BookProductionLog_Id;
            parameterArray8[1].Value = modelBPL.BookId;
            parameterArray8[2].Value = modelBPL.ResourceToResourceFolder_Id;
            parameterArray8[3].Value = modelBPL.ParticularYear;
            parameterArray8[4].Value = modelBPL.Resource_Type;
            parameterArray8[5].Value = modelBPL.LogTypeEnum;
            parameterArray8[6].Value = modelBPL.CreateUser;
            parameterArray8[7].Value = modelBPL.CreateTime;
            parameterArray8[8].Value = modelBPL.LogRemark;
            dictionary.Add(builder.ToString(), parameterArray8);
            object obj2 = DbHelperSQL.ExecuteSqlTran(dictionary);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public Model_Resource DataRowToModel(DataRow row)
        {
            Model_Resource resource = new Model_Resource();
            if (row != null)
            {
                if (row["Resource_Id"] != null)
                {
                    resource.Resource_Id = row["Resource_Id"].ToString();
                }
                if (row["Resource_MD5"] != null)
                {
                    resource.Resource_MD5 = row["Resource_MD5"].ToString();
                }
                if (row["Resource_DataStrem"] != null)
                {
                    resource.Resource_DataStrem = row["Resource_DataStrem"].ToString();
                }
                if (row["Resource_ContentHtml"] != null)
                {
                    resource.Resource_ContentHtml = row["Resource_ContentHtml"].ToString();
                }
                if ((row["CreateTime"] != null) && (row["CreateTime"].ToString() != ""))
                {
                    resource.CreateTime = new DateTime?(DateTime.Parse(row["CreateTime"].ToString()));
                }
                if ((row["Resource_ContentLength"] != null) && (row["Resource_ContentLength"].ToString() != ""))
                {
                    resource.Resource_ContentLength = new decimal?(decimal.Parse(row["Resource_ContentLength"].ToString()));
                }
            }
            return resource;
        }

        public bool Delete(string Resource_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Resource ");
            builder.Append(" where Resource_Id=@Resource_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Resource_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Resource_Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Resource ");
            builder.Append(" where Resource_Id in (" + Resource_Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteResource(string ResourceToResourceFolder_Id, string Resource_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder = new StringBuilder();
            builder.Append("delete from AlreadyedTestpaper where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            builder.Append("delete from TestQuestions_Option where TestQuestions_Id in(select TestQuestions_Id from TestQuestions where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id);");
            builder.Append("delete from TestQuestions_Score where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            builder.Append("delete from TestQuestions where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            builder.Append("delete from ResourceToResourceFolder_Property where ResourceToResourceFolder_Id=@ResourceToResourceFolder_Id;");
            builder.Append("delete from ResourceToResourceFolder where Resource_Id=@Resource_Id;");
            builder.Append("delete from [Resource] where Resource_Id=@Resource_Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ResourceToResourceFolder_Id", SqlDbType.Char, 0x24), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = ResourceToResourceFolder_Id;
            cmdParms[1].Value = Resource_Id;
            if (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) == 0)
            {
                return false;
            }
            return true;
        }

        public bool Exists(string Resource_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Resource");
            builder.Append(" where Resource_Id=@Resource_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Resource_Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public string ExistsByMD5(string Resource_MD5)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Resource_Id from Resource");
            builder.Append(" where Resource_MD5=@Resource_MD5 ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_MD5", SqlDbType.Char, 0x20) };
            cmdParms[0].Value = Resource_MD5;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
            {
                return "";
            }
            return single.ToString();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength ");
            builder.Append(" FROM Resource ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength ");
            builder.Append(" FROM Resource ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.Resource_Id desc");
            }
            builder.Append(")AS Row, T.*  from Resource T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Model_Resource GetModel(string Resource_Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Resource_Id,Resource_MD5,Resource_DataStrem,Resource_ContentHtml,CreateTime,Resource_ContentLength from Resource ");
            builder.Append(" where Resource_Id=@Resource_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = Resource_Id;
            new Model_Resource();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Resource ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Model_Resource model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Resource set ");
            builder.Append("Resource_MD5=@Resource_MD5,");
            builder.Append("Resource_DataStrem=@Resource_DataStrem,");
            builder.Append("Resource_ContentHtml=@Resource_ContentHtml,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Resource_ContentLength=@Resource_ContentLength");
            builder.Append(" where Resource_Id=@Resource_Id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Resource_MD5", SqlDbType.Char, 0x20), new SqlParameter("@Resource_DataStrem", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Resource_ContentHtml", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@Resource_ContentLength", SqlDbType.Decimal, 9), new SqlParameter("@Resource_Id", SqlDbType.Char, 0x24) };
            cmdParms[0].Value = model.Resource_MD5;
            cmdParms[1].Value = model.Resource_DataStrem;
            cmdParms[2].Value = model.Resource_ContentHtml;
            cmdParms[3].Value = model.CreateTime;
            cmdParms[4].Value = model.Resource_ContentLength;
            cmdParms[5].Value = model.Resource_Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

