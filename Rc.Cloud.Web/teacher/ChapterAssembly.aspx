<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="ChapterAssembly.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ChapterAssembly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="cHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">习题集</a></li>
            <li><a href="mHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">自有习题集</a></li>
            <li id="apHomework" runat="server" visible="false"><a href="pHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">集体备课习题集</a></li>
            <%--<li id="testPaper" runat="server" visible="false"><a href="simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">组卷</a></li>
            <li id="testPaper2" runat="server" visible="false"><a href="historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">已组试卷</a></li>--%>
            <li class="active dropdown" id="testPaper" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">组卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper" runat="server" visible="false" class="active"><a href="ChapterAssembly.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper" runat="server" visible="false"><a href="simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">双向细目表组卷</a></li>
                </ul>
            </li>
            <li class="dropdown" id="testPaper2" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">已组试卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper2" runat="server" visible="false"><a href="ChapterTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper2" runat="server" visible="false"><a href="historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">双向细目表组卷</a></li>
                </ul>
            </li>
        </ul>
    </div>
    <div class="iframe-container test_assembly">
        <div class="iframe-sidebar">
            <div class="dropdown selection_attr_dropdown">
                <a href="javascript:;" class="attr-dropdown-hook" id="Catalog"></a>
                <div class="dropdown-menu dropdown-menu-hook">
                    <dl>
                        <dt>年级学期</dt>
                        <dd ajax-name="GradeTerm">
                            <asp:Repeater runat="server" ID="rptGradeTerm">
                                <ItemTemplate>
                                    <a href="##" ajax-value="<%#Eval("Parent_Id") %>" class='<%# Container.ItemIndex==0 ? "active" :"" %>' ajax-text="<%#Eval("D_Name") %>"><%#Eval("D_Name") %></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </dd>
                        <dt>教材版本</dt>
                        <dd ajax-name="Resource_Version"></dd>
                        <dt>课本</dt>
                        <dd ajax-name="Book_Type"></dd>
                    </dl>
                    <button type="button" id="btnSearch" class="btn btn-primary btn-sm close-attr-dropdown-hook">确定</button>
                </div>
            </div>
            <div class="mtree mtree-hook" id="KPTree">
            </div>
        </div>
        <div class="iframe-main pa">
            <div class="page_title">试卷属性</div>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="control-label col-xs-1">类型：</div>
                    <div class="col-xs-8 radio">
                        <label>
                            <input type="radio" name="type" value="week_test" id="" checked />周测</label>
                        <label>
                            <input type="radio" name="type" value="month_test" id="" />月测</label>
                        <label>
                            <input type="radio" name="type" value="chapter_test" id="" />单元测</label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-1">难度：</div>
                    <div class="col-xs-8 radio">
                        <label>
                            <input type="radio" name="difficulty" value="easily" data-difficulty="easy" id="" checked />容易</label>
                        <label>
                            <input type="radio" name="difficulty" value="medium" data-difficulty="medium" id="" />中等</label>
                        <label>
                            <input type="radio" name="difficulty" value="difficult" data-difficulty="difficult" id="" />困难</label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-1">题型：</div>
                    <div class="col-xs-8 checkbox">
                        <asp:Literal ID="ltlTQ_Type" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="form-group text-center structure-content-hook">
                    <asp:Literal ID="ltlStructure" runat="server"></asp:Literal>
                </div>
                <div class="test-paper-content-hook">
                </div>
                <div class="form-group next-content-hook">
                    <div class="col-xs-1 col-xs-offset-1">
                        <button type="button" class="btn btn-primary next-btn-hook" id="btnNext">下一步</button>
                        <%--<a href="CombinationTestPaperToChapter.aspx?Identifier_Id=<%=Identifier_Id%>&ugid=<%=strUserGroup_IdActivity%>" class="btn btn-primary next-btn-hook" id="btnShrow" target="_blank">下一步</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript" src="../plugin/mtree-2.0/mtree.js"></script>
    <script type="text/javascript">
        $(function () {
            ///只能输入数字
            $(document).on('keyup blur', '.test-paper-content-hook input[type="text"]', function () {
                this.value = this.value.replace(/\D/g, '');
            })
            ///每道题相加等于总数
            $(document).on('keyup', '.test-paper-content-hook input[type="text"][name="singular"]', function () {
                var type = $(this).attr("data-type");
                var total = 0;
                $('.test-paper-content-hook input[type="text"][name="singular"][data-type="' + type + '"]').each(function () {
                    count = $(this).val();
                    if (count != "" && count != undefined && count != "0") {
                        total = parseInt(total) + parseInt(count);
                    }
                })
                $('.test-paper-content-hook input[type = "text"][name = "total_' + type + '"]').val(total);
            })
            //$('[ajax-name="GradeTerm"] a.active').click();
            loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", "<%=Subject%>", $('[ajax-name="Resource_Version"]'), 1);
            //首次加载默认点击年级学期的第一个
            //点击年级学期直接根据当前登陆人学科加载教材版本
            $('[ajax-name="GradeTerm"] a').on('click', function () {
                $(this).addClass('active').siblings().removeClass("active");
                // 学科934A3541-116E-438C-B9BA-4176368FCD9B，教材版本74958B74-D2A4-4ACD-BB4E-F48C59329F40
                loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", "<%=Subject%>", $('[ajax-name="Resource_Version"]'));
            });
            //点击教材版本加载课本
            $(document).on('click', '[ajax-name="Resource_Version"] a', function () {
                $(this).addClass('active').siblings().removeClass("active");
                // 教材版本74958B74-D2A4-4ACD-BB4E-F48C59329F40，课本3EF9506E-4C4B-407E-AA5D-451E0B20F0DA
                loadSubDict("74958B74-D2A4-4ACD-BB4E-F48C59329F40", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DA", $(this).attr("ajax-value"), $('[ajax-name="Book_Type"]'));

            });
            //点击课本事件
            $(document).on('click', '[ajax-name="Book_Type"] a', function () {
                $(this).addClass('active').siblings().removeClass("active");
            });
            //点击确定按钮事件
            $(document).on("click", "#btnSearch", function () {
                var _Suject = "<%=Subject%>";
                var _Book_Type = $('[ajax-name="Book_Type"] a[class="active"]').attr("ajax-value");
                var _GradeTerm = $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value");
                var _Resource_Version = $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value");
                if (_Suject == "" || _Suject == undefined || _Book_Type == "" || _Book_Type == undefined || _GradeTerm == "" || _GradeTerm == undefined || _Resource_Version == "" || _Resource_Version == undefined) {
                    layer.msg('查询条件不全，无法执行查询，请检查', { icon: 2, time: 4000 });
                }
                else {
                    loadCatalog($('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-text"), $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-text"), $('[ajax-name="Book_Type"] a[class="active"]').attr("ajax-text"));
                    loadData("<%=Subject%>", $('[ajax-name="Book_Type"] a[class="active"]').attr("ajax-value"), $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value"), $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value"));
                }
            });

            //加载默认题型
            $(window).on('load', function (e) {
                $('input[name="question_type"]').each(function (e) {
                    var name = $(this).data('name');
                    var type = $(this).val();
                    var index = $(this).data('index');
                    QuestionType($(this), name, type, index);
                })
            });

            //手动选择题型
            $('input[name="difficulty"]').on('change', function (e) {
                $('input[name="question_type"]:checked').each(function () {
                    InitTQCount($(this).val());
                })
            });

            //手动选择题型
            $('input[name="question_type"]').on('change', function (e) {
                var name = $(this).data('name');
                var type = $(this).val();
                var index = $(this).data('index');
                QuestionType($(this), name, type, index);
            });

            //属性面板
            $('.attr-dropdown-hook').on('click', function (e) {
                var $DropdownMenu = $(this).next('.dropdown-menu-hook');
                if ($DropdownMenu.is(':visible')) {
                    $DropdownMenu.hide();
                } else {
                    $DropdownMenu.show();
                }
                e.preventDefault();
                e.stopPropagation();
            });
            //关闭属性面板
            $('.close-attr-dropdown-hook').on('click', function (e) {
                $(this).closest('.dropdown-menu-hook').hide();
            })
            $(document).on('click', '#btnNext', function () {
                BtnNext();
            });
        })
        //下一步
        var BtnNext = function () {
            totalCountTQ = 0;
            totalCountTQ1 = 0;
            var arrKPCked = new Array();//知识点串（‘1’，‘2’）
            var arrTQType = new Array();//题型串（‘1，2’）
            var arrTQCount = new Array();//实体数量（'题型&难度&数量，题型&难度&数量'）
            var rtrfType = $(".form-horizontal input[name='type']:checked").val();//试卷类型
            var complexityText = $(".form-horizontal input[name='difficulty']:checked").val();//试卷难度
            $('.mtree input[type="checkbox"][name="name"]:checked').each(function () {
                if ($(this).attr("tt") == 1) {
                    arrKPCked.push($(this).attr("value"));
                }
            })
            if (arrKPCked.length < 1) {
                layer.msg('请先选择知识点再进行下一步', { icon: 2, time: 4000 });
                return false;
            }
            $('.form-horizontal input[type="checkbox"][name="question_type"]:checked').each(function () {
                arrTQType.push($(this).attr("value"));
            })
            if (arrTQType.length < 1) {
                layer.msg('请先选择题型再进行下一步', { icon: 2, time: 4000 });
                return false;
            }
            $('.test-paper-content-hook input[type="text"][name="singular"]').each(function () {
                type = $(this).attr("data-type");
                cpt = $(this).attr("data-id");
                count = $(this).val();
                if (count != "" && count != undefined && count != "0") {
                    totalCountTQ = parseInt(totalCountTQ) + parseInt(count);
                    var temp = type + "&" + cpt + "&" + count;
                    arrTQCount.push(temp)
                }
            })
            if (arrTQCount.length < 1) {
                layer.msg('请填写试题数量再进行下一步', { icon: 2, time: 4000 });
                return false;
            }
            //data-value="total"
            $('.test-paper-content-hook input[type="text"][data-value="total"]').each(function () {
                var count = $(this).val();
                if (count != "" && count != undefined && count != "0") {
                    totalCountTQ1 = parseInt(totalCountTQ1) + parseInt(count);
                }
            })
            if (totalCountTQ != totalCountTQ1) {
                layer.msg('填写试题总数量不等于没个小题的数量，请仔细检查', { icon: 2, time: 4000 });
                return false;
            }


            $("#btnNext").data("kpcked", arrKPCked.join(','));
            $("#btnNext").data("tqcount", arrTQCount.join(','));
            $("#btnNext").data("tqtype", arrTQType.join(','));
            $("#btnNext").data("totalcounttq", totalCountTQ);
            window.open("CheckChapterStatsHelper.aspx?Identifier_Id=<%=Identifier_Id%>&ugid=<%=strUserGroup_IdActivity%>");
        }
        var getDataForSub = function (type) {
            return $("#btnNext").data(type);
        }
        //题型表单
        function QuestionType(obj, name, type, index) {
            var QuestionTypeCheckLen = $('input[name="question_type"]:checked').length;
            var $StructureContent = $('.structure-content-hook');
            var $NextContent = $('.next-content-hook')
            if (QuestionTypeCheckLen) {
                $StructureContent.show();
                $NextContent.show();
            } else {
                $StructureContent.hide();
                $NextContent.hide();
            }
            var Complext = $.parseJSON('<%=Complext%>');
            //模板
            var html = '';
            html += '<div class="form-group form-group-hook" id="{type}" data-index="{index}">';
            html += '<div class="control-label col-xs-1">{name}：</div>';
            html += '<div class="col-xs-2">';
            html += '<input type="text" value="" name="total_{type}" data-value="total" class="form-control" />';
            html += '</div>';
            $.each(Complext, function (e) {
                html += '<div class="col-xs-1">';
                html += '<input type="text" name="singular" data-id="' + Complext[e].id + '" data-type="{type}" value="" class="form-control" />';
                html += '</div>';
            });
            html += '</div>';
            if (obj.is(':checked')) {
                tpl = html.replace(/\{type\}/g, type)
                    .replace('{name}', name)
                    .replace('{index}', index);
                $('.test-paper-content-hook').append(tpl);
            } else {
                $('#' + type).remove();
            }
            InitTQCount(type);
            SortFilter();
        }

        function SortFilter() {
            var $TestPaperContent = $('.test-paper-content-hook');
            var $FormGroup = $('.test-paper-content-hook .form-group-hook');
            if ($FormGroup.length > 0) {
                var list = $FormGroup.toArray().sort(function (a, b) {
                    return parseInt($(a).data("index")) - parseInt($(b).data("index"));
                });
                $(list).appendTo($TestPaperContent);
            }
        }

        //默认出题数量
        var _jsonTQCount = [[[6, 3, 2, 1], [6, 3, 2, 1, ], [3, 2, 1, 0]], [[6, 2, 3, 1], [6, 2, 2, 2, ], [3, 1, 1, 1]], [[6, 2, 2, 2], [6, 2, 2, 2, ], [3, 0, 1, 2]]];
        function InitTQCount(type) {
            var complexityText = $(".form-horizontal input[name='difficulty']:checked").val();//试卷难度
            var _objTQCount = null;
            if (complexityText == "easily") {//容易
                _objTQCount = _jsonTQCount[0];
            }
            else if (complexityText == "medium") {//中等
                _objTQCount = _jsonTQCount[1];
            }
            else if (complexityText == "difficult") {//困难
                _objTQCount = _jsonTQCount[2];
            }
            if (_objTQCount != undefined) {
                if (type == "20179dee-5e11-4f73-934b-2602e41df493") {//选择题
                    _objTQCount = _objTQCount[0];
                }
                else if (type == "19b77ea8-c6be-4de5-91b5-a57c0b0f619e") {//填空题
                    _objTQCount = _objTQCount[1];
                }
                else if (type == "44260cc7-15d1-459e-885c-555e77889767") {//简答题
                    _objTQCount = _objTQCount[2];
                }
            }
            if (_objTQCount != undefined) {
                $("#" + type + " input").each(function (i) {
                    if (_objTQCount[i] != undefined) {
                        $(this).val(_objTQCount[i]);
                    }
                });
            }
        }

        //加载子级数据字典
        var loadSubDict = function (hId, sId, pId, objContainer, type) {
            var dto = {
                HeadDict_Id: hId,
                SonDict_Id: sId,
                Parent_Id: pId
            };
            $.ajaxWebService("ChapterAssembly.aspx/GetSubDictList", JSON.stringify(dto), function (data) {
                $(objContainer).html(data.d);
                if (type == 1) {//初始化加载数据  
                    $('[ajax-name="Resource_Version"] a').eq(0).addClass("active");
                    loadSubDict("74958B74-D2A4-4ACD-BB4E-F48C59329F40", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DA", $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value"), $('[ajax-name="Book_Type"]'), 2);
                }
                else if (type == 2) {
                    $('[ajax-name="Book_Type"] a').eq(0).addClass("active");
                    loadCatalog($('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-text"), $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-text"), $('[ajax-name="Book_Type"] a[class="active"]').attr("ajax-text"));
                    loadData("<%=Subject%>", $('[ajax-name="Book_Type"] a[class="active"]').attr("ajax-value"), $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value"), $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value"));
                }
                else if (type == undefined) {
                    switch (hId) {
                        case '934A3541-116E-438C-B9BA-4176368FCD9B'://年级学期
                            $('[ajax-name="Resource_Version"] a').eq(0).addClass("active");
                            $('[ajax-name="Resource_Version"] a:eq(0)').click();
                            //loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", "<%=Subject%>", $('[ajax-name="Resource_Version"]'));
                            break;
                        case '74958B74-D2A4-4ACD-BB4E-F48C59329F40'://教材版本
                            $('[ajax-name="Book_Type"]  a').eq(0).addClass("active");
                            $('[ajax-name="Book_Type"] a:eq(0)').click();
                            //loadSubDict("74958B74-D2A4-4ACD-BB4E-F48C59329F40", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DA", $(this).attr("ajax-value"), $('[ajax-name="Book_Type"]'));
                            break;
                    }
                }

                //if (data.d == "") {
                //    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                //    $(".page").html("");
                //}
            }, function () { }, false);
        }

///加载知识点
function loadData(Subject, BookType, GradeTerm, Resource_Version) {
    var dto = {
        Subject: Subject,
        BookType: BookType,
        GradeTerm: GradeTerm,
        Resource_Version: Resource_Version,
        x: Math.random()
    };
    $.ajaxWebService("ChapterAssembly.aspx/GetKPList", JSON.stringify(dto), function (data) {
        if (data.d != "") {
            $("#KPTree").html(data.d);
        }
        else {
            $("#KPTree").html("");
        }
        $('.mtree-hook').mtree({
            display: 1,
            checkbox: true,
            activeClass: '',
            onClick: function (obj) {
            }
        });
    }, function () { });
}
//加载目录
var loadCatalog = function (GradeTerm, Resource_Version, BookType) {
    $("#Catalog").html(GradeTerm + ">" + Resource_Version + ">" + BookType + "&nbsp;<i class=\"material-icons\">&#xE5CF;</i>");
}
    </script>
</asp:Content>
