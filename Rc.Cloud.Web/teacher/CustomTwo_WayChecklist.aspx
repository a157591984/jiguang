<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="CustomTwo_WayChecklist.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CustomTwo_WayChecklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="cHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">习题集</a></li>
            <li><a href="mHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">自有习题集</a></li>
            <li id="apHomework" runat="server" visible="false"><a href="pHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">集体备课习题集</a></li>
            <%--<li id="testPaper" runat="server" visible="false"><a href="simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>" class="active">组卷</a></li>
            <li id="testPaper2" runat="server" visible="false"><a href="historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>">已组试卷</a></li>--%>
            <li class="active dropdown" id="testPaper" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">组卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper" runat="server" visible="false"><a href="ChapterAssembly.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper" runat="server" visible="false" class="active"><a href="simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>">双向细目表组卷</a></li>
                </ul>
            </li>
            <li class="dropdown" id="testPaper2" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">已组试卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper2" runat="server" visible="false"><a href="ChapterTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper2" runat="server" visible="false"><a href="historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>">双向细目表组卷</a></li>
                </ul>
            </li>
        </ul>
    </div>
    <div class="iframe-container iframe-container-hook">
        <div class="pa">
            <div class="filter">
                <div class="filter_section">
                    <div class="panel-title">
                        <input type="button" class="btn btn-default btn-sm" value="返回" id="backurl" />
                        正在编辑双向细目表【<asp:Literal ID="ltlName" runat="server"></asp:Literal>】的明细
                    </div>
                </div>
            </div>
            <div class="mn table-head-hook">
                <table class="table table-bordered mn">
                    <colgroup>
                        <col width="4%">
                        <col width="6%">
                        <col width="30%">
                        <col>
                        <col width="10%">
                        <col width="5%">
                        <col width="6%">
                        <col width="5%">
                    </colgroup>
                    <thead>
                        <tr>
                            <th>题号</th>
                            <th>题型</th>
                            <%--<th width="4%" class="hide">序号</th>--%>
                            <th>测量目标</th>
                            <th>知识点</th>
                            <th>难易度</th>
                            <th>分值</th>
                            <th>试题数 </th>
                            <th>操作</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div style="margin-top: -1px;" class="table-body-hook">
                <table class="table table-bordered">
                    <colgroup>
                        <col width="4%">
                        <col width="6%">
                        <col width="30%">
                        <col>
                        <col width="10%">
                        <col width="5%">
                        <col width="6%">
                        <col width="5%">
                    </colgroup>
                    <tbody id="tb">
                    </tbody>
                </table>
            </div>
            <div class="text-right mt">
                <button type="button" id="btnBegin" class="btn btn-primary">
                    开始组卷</button>
                <button type="button" id="btnHistory" class="btn btn-primary">
                    已组试卷</button>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            //表头固定
            $('.iframe-container-hook').on('scroll', function () {
                var scrollbarWidth = $('.iframe-container-hook')[0].offsetWidth - $('.iframe-container-hook')[0].scrollWidth;
                var tableBodyWidth = $('.table-body-hook').width();
                if ($(this).scrollTop() >= 45) {
                    $('.table-head-hook').css({
                        position: 'fixed',
                        top: '112px',
                        left: '15px',
                        right: 15 + scrollbarWidth + 'px',
                        minWidth: tableBodyWidth + 'px',
                        zIndex: '10'
                    });
                } else {
                    $('.table-head-hook').css({
                        position: '',
                        top: '',
                        left: '',
                        right: '',
                        minWidth: '',
                        zIndex: ''
                    });
                }
            })
            //处理出现横向滚动条时的情况
            $(document).on('scroll', function () {
                var scrollLeft = $(this).scrollLeft();
                $('.table-head-hook').css({
                    left: 15 - scrollLeft + 'px'
                });
            })

            TWGuid = "<%=TWGuid%>";
            Two_WayChecklist_Id = "<%=Two_WayChecklist_Id%>";
            TestPaper_Frame_Id = "<%=TestPaper_Frame_Id%>";
            Two_WayChecklist_Name = "<%=Two_WayChecklist_Name%>";
            knowledge = '';
            IsAdd = "<%=IsAdd%>";
            //开始组卷
            $("#btnBegin").click(function () {
                window.open("CombinationTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>");
            })
            //已组试卷
            $("#btnHistory").click(function () {
                window.location.href = "historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>";
            })
            $("#backurl").click(function () {
                window.location.href = "simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=<%=TWGuid %>";
            })

            if (IsAdd == "Add") {
                layer.msg("正在初始化试题数据，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
                InitalData(TWGuid, Two_WayChecklist_Id, TestPaper_Frame_Id);
            }
            else {
                loadData(TWGuid);
            }


            //移除知识点
            $('table').on('click', '[data-name="removeKnowledge"]', function () {
                var questionId = $(this).data('question');
                var TwoId = $(this).data('twoid');
                $(this).closest('span').remove();
                //do something
                DeleteAttr(questionId, TwoId);

            });
            //添加知识点
            $('table').on('click', '[data-name="addKnowledge"]', function () {
                var Two_WayChecklistDetail_Id = $(this).data('question');
                var Type = $(this).data('type');
                var _this = $(this);
                $.ajaxWebService("CustomTwo_WayChecklist.aspx/GetInsertAttr", "{Two_WayChecklistDetail_Id:'" + Two_WayChecklistDetail_Id + "',Type:'" + Type + "',x:" + Math.random() + "}", function (data) {
                    knowledge = data.d;
                }, function () { }, false)
                layer.open({
                    type: 1,
                    title: '请选择',
                    btn: ['确定', '取消'],
                    area: ['550px', '480px'],
                    content: '<div class="pa knowledge_list" data-name="knowledgeList">' + knowledge + '</div>',
                    yes: function (index, layero) {
                        var arrKnowledge = [];
                        var arrId = [];
                        var strKnowledge = '';
                        $('[data-name="knowledgeList"] input:checkbox:checked').each(function () {
                            var knowledgeName = $(this).val();
                            //push选中数据
                            arrKnowledge.push(knowledgeName);
                            var id = $(this).attr("tt");
                            arrId.push(id);
                            //拼接字符串
                            strKnowledge += '<span class="tag">' + knowledgeName + '<i data-name="removeKnowledge" data-twoid="' + Two_WayChecklistDetail_Id + '" data-question="' + id + '">&times;</i></span>';
                        });
                        //将选中数据插入到对应dom
                        _this.before(strKnowledge);


                        //do something
                        if (strKnowledge != null && strKnowledge != "") {
                            AddAttr(TWGuid, Two_WayChecklistDetail_Id, Type, arrId.join(","), arrKnowledge.join(","));
                        }


                        //关闭弹窗
                        layer.close(index);
                    }
                });
            })
        });
        ///复制数据
        function InitalData(TWGuid, Two_WayChecklist_Id, TestPaper_Frame_Id) {
            var dto = {
                TWGuid: TWGuid,
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                TestPaper_Frame_Id: TestPaper_Frame_Id,
                Two_WayChecklist_Name: Two_WayChecklist_Name,
                x: Math.random()
            };
            $.ajaxWebService("CustomTwo_WayChecklist.aspx/InitalData", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg("", { icon: 1, time: 1 });
                    loadData(TWGuid);
                }
                else {
                    layer.ready(function () {
                        layer.msg("初始化数据失败", { icon: 2, time: 2000 })
                    });
                }
            }, function () { })
        }

        ///添加知识点测量目标难易度
        function AddAttr(TWGuid, Two_WayChecklistDetail_Id, Type, arrId, arrKnowledge) {
            var dto = {
                Two_WayChecklist_Id: TWGuid,
                Two_WayChecklistDetail_Id: Two_WayChecklistDetail_Id,
                Type: Type,
                arrId: arrId,
                arrKnowledge: arrKnowledge,
                x: Math.random()
            };
            $.ajaxWebService("CustomTwo_WayChecklist.aspx/AddAttr", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#TQ_" + Two_WayChecklistDetail_Id).html(data.d);
                }
                else {
                    layer.ready(function () {
                        layer.msg("添加失败", { icon: 2, time: 2000 })
                    });
                }
            }, function () { })
        }


        //删除Attr
        function DeleteAttr(Two_WayChecklistDetailToAttr_Id, Two_WayChecklistDetail_Id) {
            var dto = {
                Two_WayChecklistDetailToAttr_Id: Two_WayChecklistDetailToAttr_Id,
                Two_WayChecklist_Id: TWGuid,
                Two_WayChecklistDetail_Id: Two_WayChecklistDetail_Id,
                x: Math.random()
            }
            $.ajaxWebService("CustomTwo_WayChecklist.aspx/DeleteAttr", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#TQ_" + Two_WayChecklistDetail_Id).html(data.d);
                }
                else {
                    layer.ready(function () {
                        layer.msg("删除失败", { icon: 2, time: 2000 })
                    });
                }
            }, function () { })
        }
        //加载数据
        function loadData(TWGuid) {
            $.ajaxWebService("CustomTwo_WayChecklist.aspx/GetList", "{Two_WayChecklist_Id:'" + TWGuid + "',x:" + Math.random() + "}", function (data) {
                if (data.d == "1") {
                    layer.ready(function () { layer.msg("异常", { icon: 2, time: 2000 }); })
                }
                else {
                    $("#tb").html(data.d);
                }
            }, function () { })
        }
        //编辑
        function EditData(Id) {
            if (Id == "") {
                title = "新增";
            } else {
                title = "修改"
            }
            layer.open({
                type: 2,
                title: title,
                fix: false,
                area: ["600px", "400px"],
                content: "SxxmbBigEdit.aspx?Two_WayChecklistDetail_Id=" + Id + "&Two_WayChecklist_Id=" + TWGuid
            })
        }
        function UpdateSmall(Id) {
            layer.open({
                type: 2,
                title: '修改小题',
                fix: false,
                area: ["600px", "400px"],
                content: "SxxmbDetailEdit.aspx?Two_WayChecklistDetail_Id=" + Id + "&Two_WayChecklist_Id=" + TWGuid + "&type=1"
            })
        }
    </script>
</asp:Content>
