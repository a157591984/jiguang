<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SxxmbDetail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SxxmbDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/base64.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            Two_WayChecklist_Id = "<%=Two_WayChecklist_Id%>";
            loadData();
            $("#backurl").click(function () {
                var backurl = getUrlVar("backurl");
                if (backurl != "") {
                    b = new Base64();
                    window.location.href = b.decode(backurl);
                }
                else {
                    window.location.href = "Sxxmb.aspx";
                }
            })
        })
        function loadData() {
            $.ajaxWebService("SxxmbDetail.aspx/GetList", "{Two_WayChecklist_Id:'" + Two_WayChecklist_Id + "',x:" + Math.random() + "}", function (data) {
                if (data.d == "1") {
                    layer.ready(function () { layer.msg("异常", { icon: 2, time: 2000 }); })
                }
                else {
                    $("#tb").html(data.d);
                }
            }, function () { })
        }
    </script>

    <div class="pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-heading">
                <div class="panel-title">
                    正在编辑双向细目表【<asp:Literal ID="ltlName" runat="server"></asp:Literal>】的明细
                </div>
            </div>
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <%-- <input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增大标题" onclick="EditData('');" />--%>
                    <input type="button" class="btn btn-default btn-sm" value="返回" id="backurl" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th width="4%">题号</th>
                            <th width="6%">题型</th>
                            <th width="4%">序号</th>
                            <th>测量目标</th>
                            <th>知识点</th>
                            <th width="8%">难易度</th>
                            <th width="5%">分值</th>
                            <th width="5%">试题数 </th>
                            <th width="5%">操作</th>
                        </tr>
                    </thead>
                    <tbody id="tb">
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script>
        $(function () {
            Two_WayChecklist_Id = "<%=Two_WayChecklist_Id%>";
            knowledge = '';
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
                $.ajaxWebService("SxxmbDetail.aspx/GetInsertAttr", "{Two_WayChecklistDetail_Id:'" + Two_WayChecklistDetail_Id + "',Type:'" + Type + "',x:" + Math.random() + "}", function (data) {
                    knowledge = data.d;
                }, function () { }, false)
                layer.ready(function () {
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
                                AddAttr(Two_WayChecklist_Id, Two_WayChecklistDetail_Id, Type, arrId.join(","), arrKnowledge.join(","));
                            }


                            //关闭弹窗
                            layer.close(index);
                        }
                    });
                });
            })
        });

        ///添加知识点测量目标难易度
        function AddAttr(Two_WayChecklist_Id, Two_WayChecklistDetail_Id, Type, arrId, arrKnowledge) {
            var dto = {
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                Two_WayChecklistDetail_Id: Two_WayChecklistDetail_Id,
                Type: Type,
                arrId: arrId,
                arrKnowledge: arrKnowledge,
                x: Math.random()
            };
            $.ajaxWebService("SxxmbDetail.aspx/AddAttr", JSON.stringify(dto), function (data) {
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
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                Two_WayChecklistDetail_Id: Two_WayChecklistDetail_Id,
                x: Math.random()
            }
            $.ajaxWebService("SxxmbDetail.aspx/DeleteAttr", JSON.stringify(dto), function (data) {
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

        //编辑
        function EditData(Id) {
            layer.ready(function () {
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
                    content: "SxxmbBigEdit.aspx?Two_WayChecklistDetail_Id=" + Id + "&Two_WayChecklist_Id=" + Two_WayChecklist_Id
                })
            })
        }
        function AddSmall(Id, TestQuestions_Type) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '增加小题',
                    fix: false,
                    area: ["600px", "400px"],
                    content: "SxxmbDetailEdit.aspx?Two_WayChecklistDetail_Id=" + Id + "&Two_WayChecklist_Id=" + Two_WayChecklist_Id + "&TestQuestions_Type=" + TestQuestions_Type
                })
            })
        }
        function UpdateSmall(Id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '修改小题',
                    fix: false,
                    area: ["600px", "400px"],
                    content: "SxxmbDetailEdit.aspx?Two_WayChecklistDetail_Id=" + Id + "&Two_WayChecklist_Id=" + Two_WayChecklist_Id + "&type=1"
                })
            })
        }
        function DeleteBigData(ParentId) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("SxxmbDetail.aspx/DeleteBigData", "{ParentId:'" + ParentId + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else if (data.d == "2") {
                            layer.msg("删除失败,此大标题中有明细。", { icon: 2, time: 2000 });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }
        function DeleteSmallData(Two_WayChecklistDetail_Id) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("SxxmbDetail.aspx/DeleteSmallData", "{Two_WayChecklistDetail_Id:'" + Two_WayChecklistDetail_Id + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }
    </script>
</asp:Content>
