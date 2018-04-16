<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPaperView.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TestPaperView" %>
<%@ Import Namespace="Rc.Common.StrUtility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预览</title>
    <link rel="stylesheet" href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../SysLib/css/style.css" />
    <script type="text/javascript" src="../SysLib/js/jquery.min-1.11.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
</head>
<body class="bg_white">
    <div class="elevator" id="divBack" runat="server" visible="false">
        <div class="item">返回</div>        
    </div>
    <div class="container paper_preview_container pt">
        <%
            string strSqlScoreFormat = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum,tqs.testIndex from TestQuestions_Score tqs  where tqs.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'";
            System.Data.DataTable dtScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScoreFormat).Tables[0];

            string strSql = @"select TQ.* from TestQuestions TQ where tq.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' order by TestQuestions_Num ";
            System.Data.DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            StringBuilder stbHtml = new StringBuilder();
            Rc.Model.Resources.Model_ResourceToResourceFolder modelRTRF = new Rc.BLL.Resources.BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
            if (modelRTRF == null)
            {
                Response.Write("数据不存在或已删除");
                Response.End();
            }
            string uploadPath = Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl") + "Upload/Resource/"; //存储文件基础路径
            //生成存储路径
            string savePath = string.Empty;
            string saveOwnPath = string.Empty;
            if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.云资源)
            {
                savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm, modelRTRF.Resource_Version, modelRTRF.Subject);
            }
            string fileUrl = uploadPath + "{0}{1}\\" + savePath + "{2}.{3}";//文件详细路径
            stbHtml = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Data.DataRow[] drScore = dtScore.Select("TestQuestions_Id='" + dt.Rows[i]["TestQuestions_Id"] + "'", "TestQuestions_OrderNum");

                //题干
                string strTestQuestionBody = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionBody", dt.Rows[i]["TestQuestions_Id"], "htm"), "", Encoding.UTF8, "Get");

                //选择题、完形填空题选项
                string strOption = string.Empty;
                if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest")
                {
                    for (int ii = 0; ii < drScore.Length; ii++)
                    {
                        //从文件读取选择题选项
                        string strTestQuestionOption = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionOption", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                        List<Rc.Interface.TestSelections> listTestSelections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rc.Interface.TestSelections>>(strTestQuestionOption);
                        if (listTestSelections != null && listTestSelections.Count > 0)
                        {
                            foreach (var item in listTestSelections)
                            {
                                if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class=\"col-xs-6\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(item.selectionHTML));
                            }
                            if (ii < drScore.Length - 1 && !string.IsNullOrEmpty(strOption))
                            {
                                strOption += "<br/><hr style=' height:1px;border:none;border-top:1px dotted #185598;' />";
                            }
                        }
                    }
                }

                if (dt.Rows[i]["TestQuestions_Type"].ToString() != "title" && dt.Rows[i]["TestQuestions_Type"].ToString() != "")
                {
                    stbHtml.AppendFormat("<a name=\"{0}\"></a><div class=\"panel panel-default\">", dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                    stbHtml.Append("<div class='panel-body'>");
                    stbHtml.AppendFormat("<div>{0}</div><div class='row clearfix'>{1}</div>"
                        , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody) //题干
                        , strOption //选项
                        );

                    stbHtml.Append("</div>");
                    stbHtml.Append("<div class='panel-footer question_info'>");
                    for (int ii = 0; ii < drScore.Length; ii++)
                    {
                        stbHtml.Append("<div class='row'>");
                        //正确答案
                        if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                        {
                            stbHtml.AppendFormat("<div class='col-xs-2'><div class='question_info_heading'>【{0}分值】</div><div class='score'>{1}</div></div>"
                                , dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString()) ? ("(" + drScore[ii]["testIndex"].ToString() + ")") : ""
                                , drScore[ii]["TestQuestions_Score"].ToString().clearLastZero());
                            stbHtml.AppendFormat("<div class='col-xs-6'><div class='question_info_heading'>【标准答案】</div><div class='anwser'>{0}</div></div>"
                                , drScore[ii]["TestCorrect"]);
                        }
                        if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                        {
                            //从文件读取正确答案
                            string strTestQuestionCurrent = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                            {
                                stbHtml.AppendFormat("<div class='col-xs-2'><div class='question_info_heading'>【{0}分值】</div><div class='score'>{1}</div></div>"
                                    , (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" && !string.IsNullOrEmpty(drScore[ii]["testIndex"].ToString())) ? "(" + drScore[ii]["testIndex"].ToString() + ")" : ""
                                    , Rc.Cloud.Web.Common.pfunction.ConvertTQScore(drScore[ii]["TestQuestions_Score"].ToString()));
                                stbHtml.AppendFormat("<div class='col-xs-6'><div class='question_info_heading'>【标准答案】</div><div class='anwser'>{0}</div></div>"
                                    , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCurrent).Replace("|||", "或"));

                            }
                        }
                        stbHtml.Append("</div>");
                    }
                    stbHtml.Append("</div>");
                    stbHtml.Append("</div>");
                }
                else
                {
                    stbHtml.AppendFormat("<h4 class='p_t'>{0}</h4>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody));
                }

            }
            Response.Write(stbHtml);
        %>
    </div>
    <script type="text/javascript">
        $(function () {
            //去掉字体限制
            $('font').each(function () {
                $(this).prop('face', '');
                $(this).css({
                    'font-size': '',
                })
            });
            $("#divBack").click(function () {
                history.back();
            });
        });
    </script>
</body>
</html>
