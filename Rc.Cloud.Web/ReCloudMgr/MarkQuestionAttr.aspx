<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarkQuestionAttr.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.MarkQuestionAttr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>试题属性维护</title>
    <link rel="stylesheet" href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../SysLib/css/style.css" />
    <script type="text/javascript" src="../SysLib/js/jquery.min-1.11.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script>
        $(function () {
            StrAttr = "";
            //更改试题来源
            $("#ddltq_source").change(function () {
                layer.confirm('更新所有试题来源？', {
                    btn: ['确定', '取消'], //按钮
                    btn2: function () {
                        $('#ddltq_source option:eq(0)').prop('selected', true);
                    }
                }, function () {
                    var dto = {
                        source: $("#ddltq_source").val(),
                        ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                        x: Math.random()
                        };
                        $.ajaxWebService("MarkQuestionAttr.aspx/ChangeAllTQScoreSource", JSON.stringify(dto), function (data) {
                            if (data.d == "1") {
                                layer.msg('操作成功！', { icon: 1, time: 1000 }, function () {
                                    //window.location.reload();
                                    $('select option[value="' + dto.source + '"]').prop('selected', true).siblings().prop('selected', false);
                                });
                                //loadData();
                            }
                            else {
                                layer.msg('操作失败！', { icon: 2, time: 2000 });
                            }
                        }, function () { layer.msg('操作失败！', { icon: 2, time: 2000 }); });
                    });
            })
            //移除知识点
            $(document).on('click', '[data-name="removeKnowledge"]', function () {
                $(this).closest('span').remove();
                var _tkid = $(this).data('tkid');
                //do something
                //删除
                DeleteAtrr(_tkid);

            });
            //移除考点
            $(document).on('click', '[data-name="removeTest"]', function () {
                $(this).closest('span').remove();
                var _tkid = $(this).data('tkid');
                //do something
                //删除
                DeleteTest(_tkid);

            });

            $(document).on('click', '[data-name="addKnowledgeNew"]', function () {
                var scoreid = $(this).data('scoreid');
                var gradeTerm = $(this).data('g');
                var subject = $(this).data('s');
                var resource_Version = $(this).data('v');
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: "添加知识点",
                        fix: false,
                        area: ["90%", "90%"],
                        content: "SelectQuestionAttr.aspx?ScoreId=" + scoreid + "&GradeTerm=" + gradeTerm + "&Subject=" + subject + "&Resource_Version=" + resource_Version
                    })
                })

            })
            $(document).on('click', '[data-name="addTestNew"]', function () {
                var scoreid = $(this).data('scoreid');
                var gradeTerm = $(this).data('g');
                var subject = $(this).data('s');
                var resource_Version = $(this).data('v');
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: "添加考点",
                        fix: false,
                        area: ["90%", "90%"],
                        content: "SelectQuestionTest.aspx?ScoreId=" + scoreid + "&GradeTerm=" + gradeTerm + "&Subject=" + subject
                    })
                })

            })
            //添加知识点
            $(document).on('click', '[data-name="addKnowledge"]', function () {
                var scoreid = $(this).data('scoreid');
                var _this = $(this);
                var knowledge = '';
                var dto = {
                    ScoreId: scoreid,
                    GradeTerm: "<%=GradeTerm%>",
                    Subject: "<%=Subject%>",
                    Resource_Version: "<%=Resource_Version%>",
                    x: Math.random()
                };
                $.ajaxWebService("MarkQuestionAttr.aspx/GetDataList", JSON.stringify(dto), function (data) {
                    knowledge = data.d;
                }, function () { }, false)

                layer.ready(function () {
                    layer.open({
                        type: 1,
                        title: '请选择',
                        closeBtn: false,
                        btn: ['确定'],
                        area: ['550px', '480px'],
                        content: knowledge,
                        yes: function (index, layero) {

                            //移除旧数据
                            _this.siblings('.tag').remove();
                            //将选中数据插入到对应dom
                            LoadAtrr(scoreid);
                            _this.before(StrAttr);
                            //do something
                            //关闭弹窗
                            layer.close(index);
                        }
                    });
                });
            })

            //添加考点
            $(document).on('click', '[data-name="addTest"]', function () {
                var scoreid = $(this).data('scoreid');
                var _this = $(this);
                var knowledge = '';
                var dto = {
                    ScoreId: scoreid,
                    GradeTerm: "<%=GradeTerm%>",
                    Subject: "<%=Subject%>",
                    x: Math.random()
                };
                $.ajaxWebService("MarkQuestionAttr.aspx/GetDataList1", JSON.stringify(dto), function (data) {
                    knowledge = data.d;
                }, function () { }, false)

                layer.ready(function () {
                    layer.open({
                        type: 1,
                        title: '请选择',
                        closeBtn: false,
                        btn: ['确定'],
                        area: ['550px', '480px'],
                        content: knowledge,
                        yes: function (index, layero) {

                            //移除旧数据
                            _this.siblings('.tag').remove();
                            //将选中数据插入到对应dom

                            LoadTest(scoreid);
                            _this.before(StrAttr);
                            //do something
                            //关闭弹窗
                            layer.close(index);
                        }
                    });
                });
            })
            //添加知识点
            $(document).on('click', '.che', function () {
                var _scoreid = $(this).data('scoreid');
                var _tkid = $(this).data('tkid');
                var _attrid = $(this).val();
                if (this.checked) {
                    //增加
                    AddAtrr(_scoreid, _attrid, _tkid);
                }
                else {
                    //删除
                    DeleteAtrr(_tkid);
                }
            });
        })

        //加载子级知识点列表数据
        var loadSubData = function (obj, parentId) {
            if (parseInt($(obj).closest('td').attr("hasChildren")) > 0) {
                _objTD = obj;
                _parentId = parentId;
                _parentIdStr = $(obj).closest('td').data("parentidstr") + "&" + parentId;
                loadSubData2($(_objTD).closest('td').data("toggle"), $(_objTD).closest('td').data("scoreid"));
            }
        }

        var loadSubData2 = function ($toggle, scoreid) {
            if ($(_objTD).closest('td').html() != undefined) {
                $("#divSubData").html("");
                if ($toggle == "0") {//加载子级数据
                    $(_objTD).closest('td').data("toggle", 1);
                    $(_objTD).closest('td').find("i").removeClass().addClass("fa fa-minus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();

                    var dto = {
                        ScoreId: scoreid,
                        parentId: _parentId,
                        parentIdStr: _parentIdStr,
                        x: Math.random()
                    };
                    $.ajaxWebService("MarkQuestionAttr.aspx/GetSubDataList", JSON.stringify(dto), function (data) {

                        if (data.d != "") {
                            $("#divSubData").html(data.d);
                            $(_objTD).closest('td').closest("tr").after($("#divSubData").html());
                        }
                    }, function () { });
                }
                else {//隐藏子级数据
                    $(_objTD).closest('td').data("toggle", 0);
                    $(_objTD).closest('td').find("i").removeClass().addClass("fa fa-plus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();
                }
            }
            else {
            }
        }

        //加载考点子级列表数据
        var loadSubData1 = function (obj, parentId) {
            if (parseInt($(obj).closest('td').attr("hasChildren")) > 0) {
                _objTD = obj;
                _parentId = parentId;
                _parentIdStr = $(obj).closest('td').data("parentidstr") + "&" + parentId;
                loadSubData3($(_objTD).closest('td').data("toggle"), $(_objTD).closest('td').data("scoreid"));
            }
        }

        var loadSubData3 = function ($toggle, scoreid) {
            if ($(_objTD).closest('td').html() != undefined) {
                $("#divSubData").html("");
                if ($toggle == "0") {//加载子级数据
                    $(_objTD).closest('td').data("toggle", 1);
                    $(_objTD).closest('td').find("i").removeClass().addClass("fa fa-minus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();

                    var dto = {
                        ScoreId: scoreid,
                        parentId: _parentId,
                        parentIdStr: _parentIdStr,
                        x: Math.random()
                    };
                    $.ajaxWebService("MarkQuestionAttr.aspx/GetSubDataList1", JSON.stringify(dto), function (data) {

                        if (data.d != "") {
                            $("#divSubData").html(data.d);
                            $(_objTD).closest('td').closest("tr").after($("#divSubData").html());
                        }
                    }, function () { });
                }
                else {//隐藏子级数据
                    $(_objTD).closest('td').data("toggle", 0);
                    $(_objTD).closest('td').find("i").removeClass().addClass("fa fa-plus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();
                }
            }
            else {
            }
        }
        //加载知识点
        var LoadAtrr = function (ScoreId) {
            var rtrfid = "<%=ResourceToResourceFolder_Id%>";
            var dto = {
                ScoreId: ScoreId,
                ResourceToResourceFolder_Id: rtrfid,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/LoadAtrr", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    StrAttr = data.d;
                }
                else {
                    StrAttr = "";
                }
                //else {
                //    layer.msg("异常", { time: 2000, icon: 2 });
                //}
            }, function () { }, false);
        }
        //加载考点
        var LoadTest = function (ScoreId) {
            var rtrfid = "<%=ResourceToResourceFolder_Id%>";
            var dto = {
                ScoreId: ScoreId,
                ResourceToResourceFolder_Id: rtrfid,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/LoadTest", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    StrAttr = data.d;
                } else {
                    StrAttr = "";
                }
                //else {
                //    layer.msg("异常", { time: 2000, icon: 2 });
                //}
            }, function () { }, false);
        }
        //增加知识点
        var AddAtrr = function (ScoreId, AttrId, TestQuestions_Knowledge_ID) {
            var dto = {
                ScoreId: ScoreId,
                AttrId: AttrId,
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/AddAtrr", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
        //删除知识点
        var DeleteAtrr = function (TestQuestions_Knowledge_ID) {
            var dto = {
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/DeleteAtrr", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
        //删除知识点
        var DeleteTest = function (TestQuestions_Knowledge_ID) {
            var dto = {
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/DeleteTest", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }

        //异步获取知识点
        var GetQuestionAttr = function (ScoreId) {
            var rtrfid = "<%=ResourceToResourceFolder_Id%>";
            var dto = {
                ResourceToResourceFolder_Id: rtrfid,
                ScoreId: ScoreId,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/GetQuestionAttr", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#attr_" + ScoreId).html(data.d);
                } else {
                    $("#attr_" + ScoreId).html("");
                }
            }, function () { });
        }
        //异步获取知识点
        var GetQuestionTest = function (ScoreId) {
            var rtrfid = "<%=ResourceToResourceFolder_Id%>";
            var dto = {
                ResourceToResourceFolder_Id: rtrfid,
                ScoreId: ScoreId,
                x: Math.random()
            };
            $.ajaxWebService("MarkQuestionAttr.aspx/GetQuestionTest", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#test_" + ScoreId).html(data.d);
                } else {
                    $("#test_" + ScoreId).html("");
                }
            }, function () { });
        }
        //更改单个空的来源
        function ChangeSource(scoreId, tqId, obj) {
            var dto = {
                scoreId: scoreId,
                tqId: tqId,
                Source: $(obj).val(),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            }
            $.ajaxWebService("MarkQuestionAttr.aspx/ChangeSource", JSON.stringify(dto), function (data) {

            }, function () { }, false);
        }
        //更改单个空难易度、更新题的最大难易度
        function ChangeComplexityText(scoreId, tqId, obj) {
            var dto = {
                scoreId: scoreId,
                tqId: tqId,
                Source: $(obj).val(),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            }
            $.ajaxWebService("MarkQuestionAttr.aspx/ChangeComplexityText", JSON.stringify(dto), function (data) {

            }, function () { }, false);
        }
        //更改题型
        function ChangeTQ_Type(tqId, obj) {
            var dto = {
                tqId: tqId,
                TQ_Type: $(obj).val(),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            }
            $.ajaxWebService("MarkQuestionAttr.aspx/ChangeTQ_Type", JSON.stringify(dto), function (data) {

            }, function () { }, false);

        }
    </script>
</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="test_paper_name_panel">
                <div class="panel_heading">
                    <div class="panel_title"></div>
                    <div class="panel_info">
                        <div class="form-inline">
                            <label>所有来源：</label>
                            <asp:DropDownList ID="ddltq_source" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="test_paper_panel">
                <div class="panel_body">
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
                        //试卷所有题对应的知识点
                        string sqlAttr = @"select TestQuestions_Score_ID,S_TestQuestions_KP_Id,KPName,KPNameBasic,tk.CreateTime from [S_TestQuestions_KP] tk
inner join [dbo].[S_KnowledgePoint] sp on sp.S_KnowledgePoint_Id=tk.S_KnowledgePoint_Id
inner join S_KnowledgePointBasic t on t.S_KnowledgePointBasic_Id=sp.S_KnowledgePointBasic_Id
where  ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'";
                        System.Data.DataTable dtAttr = Rc.Common.DBUtility.DbHelperSQL.Query(sqlAttr).Tables[0];
                        //试卷所有题对应的考点
                        string sqlTest = @"select TestQuestions_Score_ID,S_TestQuestions_TP_Id,TPNameBasic,TPName,tk.CreateTime from [S_TestQuestions_TP] tk
inner join [dbo].[S_TestingPoint] sp on sp.S_TestingPoint_Id=tk.S_TestingPoint_Id
left join S_TestingPointBasic t on t.S_TestingPointBasic_Id=sp.S_TestingPointBasic_Id
where  ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'";
                        System.Data.DataTable dtTest = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTest).Tables[0];
                        //试卷所有空对应的来源、难易度
                        string sqlSource = @"select  t.Attr_Value,t.AttrEnum,cd.D_Name,t.S_TQ_Score_Attr_Id,TestQuestions_Score_Id  from S_TQ_Score_AttrExtend t
left join Common_Dict cd on cd.Common_Dict_ID=t.Attr_Value
where ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'";
                        System.Data.DataTable dtSource = Rc.Common.DBUtility.DbHelperSQL.Query(sqlSource).Tables[0];
                        //试题类型（d_value4）。来源(d_value26)对应学科的数据字典
                        string sqlDIC = string.Format(@" select t.Dict_Id,t3.D_Name,t3.D_Order,t3.D_Type from DictRelation_Detail t 
inner join DictRelation t2 on t2.DictRelation_Id=t.DictRelation_Id
inner join Common_Dict t3 on t3.Common_Dict_Id=t.Dict_Id 
where t2.HeadDict_Id='934A3541-116E-438C-B9BA-4176368FCD9B' and (t2.SonDict_Id='F3BB0E09-CF73-4696-84BF-007C30B249A1' or t2.SonDict_Id='59254430-B8EA-4186-96E1-A9B923D4AE61') and t.Parent_Id='{0}'
order by t3.D_Order", Subject);
                        System.Data.DataTable dtDic = Rc.Common.DBUtility.DbHelperSQL.Query(sqlDIC).Tables[0];
                        //难易度
                        string sqlComplexityText = string.Format(@" select Common_Dict_ID as Dict_Id,D_Name from    [dbo].[Common_Dict]     where D_Type='25' order by D_Order ");
                        System.Data.DataTable dtComplexityText = Rc.Common.DBUtility.DbHelperSQL.Query(sqlComplexityText).Tables[0];
                        //试卷所有题对应的试题类型
                        string sqlTQ_Type = string.Format(@" select S_TQ_Attr_Id,TestQuestions_Id,AttrEnum,AttrValue from S_TQ_AttrExtend t
left join Common_Dict cd on cd.Common_Dict_ID=t.AttrValue where ResourceToResourceFolder_Id='{0}' and AttrEnum='{1}'", ResourceToResourceFolder_Id, Rc.Model.Resources.TQ_AttrExtend.TQ_Type.ToString());
                        System.Data.DataTable dtTQ_Type = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTQ_Type).Tables[0];
                        //生成存储路径
                        string uploadPath = Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl") + "Upload/Resource/"; //存储文件基础路径
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
                                            if (!string.IsNullOrEmpty(item.selectionHTML)) strOption += string.Format("<div class='option_item'>{0}</div>", Rc.Cloud.Web.Common.pfunction.NoHTML(item.selectionHTML));
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
                                stbHtml.AppendFormat("<div class=\"question_panel\"><a name=\"{0}\"></a>", dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                stbHtml.AppendFormat("<div class='panel_stem'>{0}</div><div class='panel_option'>{1}</div>"
                                    , Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody) //题干
                                    , strOption //选项
                                    );


                                stbHtml.Append("<div class='panel_opera'>");

                                stbHtml.Append("<ul class='nav nav-tabs'>");
                                stbHtml.AppendFormat("<li class='active'><a href='#emptyAttr_{0}' data-toggle='tab'>空属性维护</a></li>"
                                     , dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                if (dt.Rows[i]["Parent_Id"].ToString() != "0" && dt.Rows[i]["type"].ToString() == "complex")
                                {

                                }
                                else
                                {
                                    stbHtml.AppendFormat("<li><a href='#questionAttr_{0}' data-toggle='tab'>题属性维护</a></li>"
                                        , dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                }
                                stbHtml.Append("</ul>");

                                stbHtml.Append("</div>");

                                stbHtml.Append("<div class='panel_other tab-content'>");

                                #region 空属性
                                stbHtml.AppendFormat("<div class='tab-pane active' id='emptyAttr_{0}'>"
                                    , dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                stbHtml.Append("<dl class='other_attr'>");

                                #region 知识点
                                stbHtml.Append("<dt>知识点：</dt>");
                                stbHtml.Append("<dd class='clearfix'>");
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.Append("<div>");
                                    stbHtml.AppendFormat("{0}", drScore.Length > 1 ? "<div class='serial_num'>" + (ii + 1).ToString() + ".</div>" : "");
                                    stbHtml.AppendFormat("<div class='attr' id='attr_{0}'>", drScore[ii]["TestQuestions_Score_ID"].ToString());
                                    if (dtAttr.Rows.Count > 0)
                                    {
                                        System.Data.DataRow[] drAttr = dtAttr.Select("TestQuestions_Score_ID='" + drScore[ii]["TestQuestions_Score_ID"].ToString() + "'", "CreateTime desc,KPName");
                                        if (drAttr.Length > 0)
                                        {
                                            foreach (var item in drAttr)
                                            {
                                                stbHtml.AppendFormat("<span class=\"tag\">{0}<i data-name=\"removeKnowledge\" data-tkid=\"{1}\">×</i></span>", item["KPNameBasic"], item["S_TestQuestions_KP_Id"]);
                                            }
                                        }
                                    }
                                    stbHtml.AppendFormat("<span class=\"tag_add\" data-name=\"addKnowledgeNew\" data-scoreid=\"{0}\" data-g=\"{1}\" data-s=\"{2}\" data-v=\"{3}\" data-type=\"1\">+</span>"
                                        , drScore[ii]["TestQuestions_Score_ID"].ToString()
                                        , modelRTRF.GradeTerm
                                        , modelRTRF.Subject
                                        , modelRTRF.Resource_Version);
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");
                                }
                                stbHtml.Append("</dd>");
                                #endregion

                                #region 考点
                                stbHtml.Append("<dt>考点：</dt>");
                                stbHtml.Append("<dd class='clearfix'>");
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.Append("<div>");
                                    stbHtml.AppendFormat("{0}", drScore.Length > 1 ? "<div class='serial_num'>" + (ii + 1).ToString() + ".</div>" : "");
                                    stbHtml.AppendFormat("<div class='attr' id='test_{0}'>", drScore[ii]["TestQuestions_Score_ID"].ToString());
                                    if (dtAttr.Rows.Count > 0)
                                    {
                                        System.Data.DataRow[] drAttr = dtTest.Select("TestQuestions_Score_ID='" + drScore[ii]["TestQuestions_Score_ID"].ToString() + "'", "CreateTime desc,TPName");
                                        if (drAttr.Length > 0)
                                        {
                                            foreach (var item in drAttr)
                                            {
                                                stbHtml.AppendFormat("<span class=\"tag\">{0}<i data-name=\"removeTest\" data-tkid=\"{1}\">×</i></span>", item["TPNameBasic"], item["S_TestQuestions_TP_Id"]);
                                            }
                                        }
                                    }
                                    stbHtml.AppendFormat("<span class=\"tag_add\" data-name=\"addTestNew\" data-scoreid=\"{0}\" data-g=\"{1}\" data-s=\"{2}\" data-type=\"1\">+</span>"
                                        , drScore[ii]["TestQuestions_Score_ID"].ToString()
                                        , modelRTRF.GradeTerm
                                        , modelRTRF.Subject);
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");

                                }
                                stbHtml.Append("</dd>");
                                #endregion

                                #region 来源
                                stbHtml.Append("<dt>来源：</dt>");
                                stbHtml.Append("<dd class='clearfix'>");
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.AppendFormat("{0}", drScore.Length > 1 ? "<div class='serial_num'>" + (ii + 1).ToString() + ".</div>" : "");
                                    stbHtml.AppendFormat("<div class='form-inline' id='source_{0}'>", drScore[ii]["TestQuestions_Score_ID"].ToString());
                                    System.Data.DataRow[] drAttr = dtSource.Select("TestQuestions_Score_ID='" + drScore[ii]["TestQuestions_Score_ID"].ToString() + "' and AttrEnum='" + Rc.Model.Resources.TQ_Score_AttrExtend.Source.ToString() + "'");
                                    System.Data.DataRow[] drDic = dtDic.Select("D_Type='26'");
                                    stbHtml.AppendFormat("<select class=\"form-control input-sm\" onchange=\"ChangeSource('{0}','{1}',this);\">", drScore[ii]["TestQuestions_Score_ID"].ToString(), dt.Rows[i]["TestQuestions_Id"].ToString());
                                    stbHtml.Append("<option value=\"0\">-请选择-</option>");
                                    foreach (System.Data.DataRow item in drDic)
                                    {
                                        string selected = string.Empty;
                                        if (drAttr.Length > 0 && drAttr[0]["Attr_Value"].ToString() == item["Dict_Id"].ToString())
                                        {
                                            selected = " selected ";
                                        }
                                        stbHtml.AppendFormat("<option value=\"{0}\" {1}>{2}</option>"
                                            , item["Dict_Id"], selected, item["D_Name"]);

                                    }
                                    stbHtml.Append("</select>");
                                    stbHtml.Append("</div>");

                                }
                                stbHtml.Append("</dd>");
                                #endregion

                                #region 难易度
                                stbHtml.Append("<dt>难易度：</dt>");
                                stbHtml.Append("<dd class='clearfix'>");
                                for (int ii = 0; ii < drScore.Length; ii++)
                                {
                                    stbHtml.AppendFormat("{0}", drScore.Length > 1 ? "<div class='serial_num'>" + (ii + 1).ToString() + ".</div>" : "");
                                    stbHtml.AppendFormat("<div class='form-inline' id='complexitytext_{0}'>", drScore[ii]["TestQuestions_Score_ID"].ToString());
                                    System.Data.DataRow[] drAttr = dtSource.Select("TestQuestions_Score_ID='" + drScore[ii]["TestQuestions_Score_ID"].ToString() + "' and AttrEnum='" + Rc.Model.Resources.TQ_Score_AttrExtend.ComplexityText.ToString() + "'");
                                    System.Data.DataRow[] drDic = dtComplexityText.Select("");
                                    stbHtml.AppendFormat("<select class=\"form-control input-sm\" onchange=\"ChangeComplexityText('{0}','{1}',this);\">", drScore[ii]["TestQuestions_Score_ID"].ToString(), dt.Rows[i]["TestQuestions_Id"].ToString());
                                    stbHtml.Append("<option value=\"0\">-请选择-</option>");
                                    foreach (System.Data.DataRow item in drDic)
                                    {
                                        string selected = string.Empty;
                                        if (drAttr.Length > 0 && drAttr[0]["Attr_Value"].ToString() == item["Dict_Id"].ToString())
                                        {
                                            selected = " selected ";
                                        }
                                        stbHtml.AppendFormat("<option value=\"{0}\" {1}>{2}</option>"
                                            , item["Dict_Id"], selected, item["D_Name"]);

                                    }
                                    stbHtml.Append("</select>");
                                    stbHtml.Append("</div>");

                                }
                                stbHtml.Append("</dd>");
                                #endregion

                                stbHtml.Append("</dl>");
                                stbHtml.Append("</div>");
                                #endregion


                                #region 题属性
                                stbHtml.AppendFormat("<div class='tab-pane' id='questionAttr_{0}'>"
                                    , dt.Rows[i]["TestQuestions_Num"].ToString().TrimEnd('.'));
                                stbHtml.Append("<dl class='other_attr'>");

                                #region 题型
                                stbHtml.Append("<dt>题型：</dt>");
                                stbHtml.Append("<dd class='clearfix'>");

                                stbHtml.AppendFormat("<div class='form-inline' id='tq_type_{0}'>", dt.Rows[i]["TestQuestions_ID"].ToString());
                                System.Data.DataRow[] drTQ_Type = dtTQ_Type.Select("TestQuestions_ID='" + dt.Rows[i]["TestQuestions_ID"].ToString() + "'");
                                System.Data.DataRow[] drDicTQ_Type = dtDic.Select("D_Type='4'");
                                stbHtml.AppendFormat("<select class=\"form-control input-sm\" onchange=\"ChangeTQ_Type('{0}',this);\">", dt.Rows[i]["TestQuestions_Id"].ToString());
                                stbHtml.Append("<option value=\"0\">-请选择-</option>");
                                foreach (System.Data.DataRow item in drDicTQ_Type)
                                {
                                    string selected = string.Empty;
                                    if (drTQ_Type.Length > 0 && drTQ_Type[0]["AttrValue"].ToString() == item["Dict_Id"].ToString())
                                    {
                                        selected = " selected ";
                                    }
                                    stbHtml.AppendFormat("<option value=\"{0}\" {1}>{2}</option>"
                                        , item["Dict_Id"], selected, item["D_Name"]);

                                }
                                stbHtml.Append("</select>");
                                stbHtml.Append("</div>");

                                stbHtml.Append("</dd>");
                                #endregion

                                stbHtml.Append("</dl>");
                                stbHtml.Append("</div>");
                                #endregion

                                stbHtml.Append("</div>");
                                stbHtml.Append("</div>");
                            }
                            else
                            {
                                if (dt.Rows[i]["Parent_Id"].ToString().Trim() == "0" && dt.Rows[i]["type"].ToString() == "complex")
                                {

                                    stbHtml.AppendFormat("<div class='question_type_panel'><div class='panel_heading'>{0}</div></div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody));
                                    #region 综合题 题型

                                    stbHtml.Append("<div class='question_panel'>");
                                    stbHtml.Append("<div class='panel_opera'><ul class='nav nav-tabs'><li class='active'><a href='javascript:;'>综合题属性维护</a></li></ul></div>");
                                    stbHtml.Append("<div class='panel_other'>");
                                    stbHtml.Append("<dl class='other_attr'>");
                                    stbHtml.Append("<dt>题型：</dt>");
                                    stbHtml.Append("<dd class='clearfix'>");

                                    stbHtml.AppendFormat("<div class='form-inline' id='tq_type_{0}'>", dt.Rows[i]["TestQuestions_ID"].ToString());
                                    System.Data.DataRow[] drTQ_Type = dtTQ_Type.Select("TestQuestions_ID='" + dt.Rows[i]["TestQuestions_ID"].ToString() + "'");
                                    System.Data.DataRow[] drDicTQ_Type = dtDic.Select("D_Type='4'");
                                    stbHtml.AppendFormat("<select class=\"form-control input-sm\" onchange=\"ChangeTQ_Type('{0}',this);\">", dt.Rows[i]["TestQuestions_Id"].ToString());
                                    stbHtml.Append("<option value=\"0\">-请选择-</option>");
                                    foreach (System.Data.DataRow item in drDicTQ_Type)
                                    {
                                        string selected = string.Empty;
                                        if (drTQ_Type.Length > 0 && drTQ_Type[0]["AttrValue"].ToString() == item["Dict_Id"].ToString())
                                        {
                                            selected = " selected ";
                                        }
                                        stbHtml.AppendFormat("<option value=\"{0}\" {1}>{2}</option>"
                                            , item["Dict_Id"], selected, item["D_Name"]);

                                    }
                                    stbHtml.Append("</select>");
                                    #endregion
                                    stbHtml.Append("</div>");

                                    stbHtml.Append("</dd>");
                                    stbHtml.Append("</dl>");
                                    stbHtml.Append("</div>");
                                    stbHtml.Append("</div>");



                                }
                                else
                                {
                                    stbHtml.AppendFormat("<div class='question_type_panel'><div class='panel_heading'>{0}</div></div>", Rc.Cloud.Web.Common.pfunction.NoHTML(strTestQuestionBody));
                                }
                            }

                        }
                        Response.Write(stbHtml);
                    %>
                </div>
            </div>
        </div>
        <div style="display: none;" id="divSubData"></div>
        <script type="text/javascript">
            $(function () {
                //去掉字体限制
                $('font').each(function () {
                    $(this).prop('face', '');
                    $(this).css({
                        'font-size': '',
                    })
                });
            });
        </script>
    </form>
</body>
</html>
