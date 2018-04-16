<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="TestPaper_FrameDetail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TestPaper_FrameDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/base64.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            TestPaper_Frame_Id = "<%=TestPaper_Frame_Id%>";
            loadData();
            $("#backurl").click(function () {
                var backurl = getUrlVar("backurl");
                if (backurl != "") {
                    b = new Base64();
                    window.location.href = b.decode(backurl);
                }
                else {
                    window.location.href = "TestPaper_FrameList.aspx";
                }
            })
        })
        function loadData() {
            $.ajaxWebService("TestPaper_FrameDetail.aspx/GetList", "{TestPaper_Frame_Id:'" + TestPaper_Frame_Id + "',x:" + Math.random() + "}", function (data) {
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
                    正在编辑试卷结构【<asp:Literal ID="ltlName" runat="server"></asp:Literal>】的明细
                </div>
            </div>
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增大标题" onclick="EditData('');" />
                    <input type="button" class="btn btn-default btn-sm" value="返回" id="backurl" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th width="5%">题号</th>
                            <th width="6%">题型</th>
                            <th width="8%">前端题型</th>
                            <th width="5%">序号</th>
                            <th>测量目标</th>
                            <th>知识点</th>
                            <th width="10%">难易度</th>
                            <th width="5%">分值</th>
                            <th width="15%">操作</th>
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
                    area: ["420px", "320px"],
                    content: "BigTitleEdit.aspx?TestPaper_FrameDetail_Id=" + Id + "&TestPaper_Frame_Id=" + TestPaper_Frame_Id
                })
            })
        }
        function AddSmall(Id, TestQuestions_Type) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '增加小题',
                    fix: false,
                    area: ["650px", "330px"],
                    content: "SmallTitleEdit.aspx?TestPaper_FrameDetail_Id=" + Id + "&TestPaper_Frame_Id=" + TestPaper_Frame_Id + "&TestQuestions_Type=" + TestQuestions_Type
                })
            })
        }
        function UpdateSmall(Id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '修改小题',
                    fix: false,
                    area: ["650px", "330px"],
                    content: "SmallTitleEdit.aspx?TestPaper_FrameDetail_Id=" + Id + "&TestPaper_Frame_Id=" + TestPaper_Frame_Id + "&type=1"
                })
            })
        }
        function DeleteBigData(ParentId) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("TestPaper_FrameDetail.aspx/DeleteBigData", "{ParentId:'" + ParentId + "',x:" + Math.random() + "}", function (data) {
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
        function DeleteSmallData(TestPaper_FrameDetail_Id, TestPaper_Frame_Id) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("TestPaper_FrameDetail.aspx/DeleteSmallData", "{TestPaper_FrameDetail_Id:'" + TestPaper_FrameDetail_Id + "',TestPaper_Frame_Id:'" + TestPaper_Frame_Id + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else if (data.d == "2") {
                            layer.msg("删除失败,此试卷结构已关联试卷。", { icon: 2, time: 2000 });
                        }
                        else if (data.d == "3") {
                            layer.msg("删除失败,此试卷结构明细已导入试题。", { icon: 2, time: 2000 });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }
        //导入试题
        function IntoTQ(TestPaper_Frame_Id, TestPaper_FrameDetail_Id, TestPaper_FrameDetail_Name) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '导入试题',
                    area: ["950px", "90%"],
                    content: "RelationTQ.aspx?TestPaper_Frame_Id=" + TestPaper_Frame_Id + "&TestPaper_FrameDetail_Id=" + TestPaper_FrameDetail_Id + "&TestPaper_FrameDetail_Name=" + TestPaper_FrameDetail_Name,
                    cancel: function () { loadData(); }
                })
            })
        }
    </script>
</asp:Content>
