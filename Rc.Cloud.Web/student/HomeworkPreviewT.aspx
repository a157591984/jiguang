<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeworkPreviewT.aspx.cs" Inherits="Rc.Cloud.Web.student.HomeworkPreviewT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预览</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
</head>
<body class="body_bg user_select_none">
    <asp:Literal ID="ltlBtn" runat="server" ClientIDMode="Static"></asp:Literal>
    <div class="container ph hw_priview_cont">
        <%
            string strSqlScoreFormat = @"select tqs.TestQuestions_Id,tqs.TestQuestions_Score_ID,tqs.TestCorrect,tqs.TestQuestions_Score,tqs.TestQuestions_Num,tqs.TestQuestions_OrderNum from TestQuestions_Score tqs  where tqs.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'";
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
            string uploadPath = Rc.Cloud.Web.Common.pfunction.GetResourceHost("TestWebSiteUrl") + "Upload/Resource/"; //存储文件基础路径
            //生成存储路径
            string savePath = string.Empty;
            string saveOwnPath = string.Empty;
            if (modelRTRF.Resource_Class == Rc.Common.Config.Resource_ClassConst.自有资源)
            {
                saveOwnPath = string.Format("{0}\\", Convert.ToDateTime(modelRTRF.CreateTime).ToString("yyyy-MM-dd"));
            }
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

                //选择题选项
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

                if (dt.Rows[i]["TestQuestions_Type"].ToString() != "title")
                {
                    stbHtml.AppendFormat("<a name=\"{0}\"></a><div class=\"panel panel-default\">", dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                    stbHtml.Append("<div class='panel-body'>");
                    stbHtml.AppendFormat("<div>{0}</div><div class='row clearfix'>{1}</div>"
                        , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody) //题干
                        , strOption //选项
                        );

                    stbHtml.Append("</div>");
                    stbHtml.Append("<div class='panel-footer'>");
                    for (int ii = 0; ii < drScore.Length; ii++)
                    {
                        stbHtml.Append("<div class='row pb'>");
                        //正确答案
                        if (dt.Rows[i]["TestQuestions_Type"].ToString() == "selection" || dt.Rows[i]["TestQuestions_Type"].ToString() == "clozeTest" || dt.Rows[i]["TestQuestions_Type"].ToString() == "truefalse")
                        {
                            string strScore = string.Empty;//分值
                            strScore = drScore[0]["TestQuestions_Score"].ToString().Replace(".0", "");
                            stbHtml.AppendFormat("<div class='col-xs-2'><span class='btn btn-primary'>分值：{0}</span></div>"
                                , strScore);
                            //stbHtml.AppendFormat("<div class='col-xs-6'><span class='btn btn-success'>标准答案</span><div>{0}</div></div>"
                            //    , drScore[ii]["TestCorrect"]);
                        }
                        if (dt.Rows[i]["TestQuestions_Type"].ToString() == "fill" || dt.Rows[i]["TestQuestions_Type"].ToString() == "answers")
                        {
                            //从文件读取正确答案
                            string strTestQuestionCurrent = Rc.Common.RemotWeb.PostDataToServer(string.Format(fileUrl, saveOwnPath, "testQuestionCurrent", drScore[ii]["TestQuestions_Score_ID"], "txt"), "", Encoding.UTF8, "Get");
                            if (!string.IsNullOrEmpty(strTestQuestionCurrent))
                            {
                                //if (drScore.Length > 1)
                                //    stbHtml.AppendFormat("<div class='col-xs-1'>{0}</div>"
                                //        , "(" + drScore[ii]["TestQuestions_OrderNum"].ToString() + ")");
                                stbHtml.AppendFormat("<div class='col-xs-2'><span class='btn btn-primary'>{0}分值：{1}</span></div>"
                                    , (drScore.Length > 1) ? "(" + drScore[ii]["TestQuestions_OrderNum"].ToString() + ")&nbsp;&nbsp;" : ""
                                    , drScore[ii]["TestQuestions_Score"].ToString().Replace(".0", ""));
                                //stbHtml.AppendFormat("<div class='col-xs-6'><span class='btn btn-success'>标准答案</span><div>{0}</div></div>"
                                //    , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionCurrent));

                            }
                        }
                        stbHtml.Append("</div>");
                    }
                    stbHtml.Append("</div>");
                    stbHtml.Append("</div>");
                }
                else
                {
                    stbHtml.AppendFormat("<div class=\"prompt_info\">{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody));
                }

            }
            Response.Write(stbHtml);
        %>
    </div>
    <script type="text/javascript">
        $(function () {
            //布置作业
            var classid = "<%=groupId%>";
            var HwId = "<%= HomeWork_Id%>";
            if (classid == "") {
                if (HwId == "") {
                    $(".layoutIframe").hide();
                }
                else {
                    $(".AddlayoutIframe").hide(); $(".DellayoutIframe").hide();
                }
            }
            //禁止浏览器右击按钮
            $(document).ready(function () {

                $(document).bind("contextmenu", function (e) {

                    return false;

                });

            });

            $(".layoutIframe").on('click', function () {
                var rtrId = "<%=ResourceToResourceFolder_Id%>";
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '443px'],
                    content: 'layoutIframe.aspx?tp=1&rtrId=' + rtrId + "&classId=" + classid //iframe的url
                });
            });
            $(".AddlayoutIframe").on('click', function () {
                var rtrId = "<%=ResourceToResourceFolder_Id%>";
                var HwId = "<%=HomeWork_Id%>";
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '470px'],
                    content: 'AddlayoutIframe.aspx?tp=1&rtrId=' + rtrId + "&classId=" + classid + "&HomeWork_Id=" + HwId //iframe的url
                });
            });
            $(".DellayoutIframe").on('click', function () {
                var HwId = "<%=HomeWork_Id%>";
                classDisband(HwId);
            })
        });
        var classDisband = function (hwid) {
            var index = layer.confirm("重新布置作业以前布置的内容将被删除,确定要重新布置作业?", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("cHomework.aspx/DeleteHw", "{HomeWorkId:'" + hwid + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        Delete();
                    }
                    if (data.d == "2") {
                        layer.msg('重新布置作业失败', { icon: 2 });
                        return false;
                    }

                }, function () {
                    layer.msg('重新布置作业失败！', { icon: 2 });
                    return false;
                }, false);
            });
        }
        var Delete = function () {

            var rtrId = "<%=ResourceToResourceFolder_Id%>";
            var classid = "<%=groupId%>";
            layer.open({
                type: 2,
                title: '布置作业',
                area: ['680px', '443px'],
                content: 'layoutIframe.aspx?tp=1&rtrId=' + rtrId + "&classId=" + classid //iframe的url
            });

        }
    </script>
</body>
</html>
