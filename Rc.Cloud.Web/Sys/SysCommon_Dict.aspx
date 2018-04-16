<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysCommon_Dict.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysCommon_Dict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="button" value="新增字典" class="btn btn-primary btn-sm" onclick="showPopCommon_Dict('', 1);" runat="server" id="btnAdd" />
                        <input type="button" value="生成年级学科/学科/教材版本目录" class="btn btn-primary btn-sm hidden" onclick="GenerateGradeTermSubjectTeacherBooks();" />
                        <asp:TextBox ID="txtD_Name" runat="server" MaxLength="50" CssClass="form-control input-sm" placeholder="名称"></asp:TextBox>
                        <asp:DropDownList ID="ddlD_Type" onchange="ChangDiceType();" CssClass="form-control input-sm" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" ClientIDMode="Static" Text="查询" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" />
                    </div>
                    <%= GetHtmlData() %>
                    <hr />
                    <%= GetPageIndex()%>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidItemDescID" runat="server" />
    <asp:HiddenField ID="hidHandel" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var pageUrl = '<%=strPageNameAndParm%>';
        $(function () {
            if ("<%=SysName%>" == "内容生产管理平台") $("#ddlD_Type option[value='8']").remove();
        });
        function ChangDiceType() {
            $("#btnSearch").click();
        }

        function DeleteItemDesc(id) {
            layer.ready(function () {
                layer.confirm('删除数据？', { icon: 3 }, function () {
                    $.get("../Ajax/SysAjax.aspx", { key: "SysCommon_DictTempDelete", Aid: id, net4: Math.random() },
                function (data) {
                    if (data == "1") {
                        layer.msg('删除成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                        });
                    }
                    else if (data == "0") {
                        layer.msg('删除失败', { icon: 2 });
                    }
                    else {
                        layer.msg(data, { icon: 4 });
                    }
                });
                });
            });
        }
        function showPopCommon_Dict(id) {
            var page = "SysCommon_DictAdd.aspx?id=" + id;
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: (!id) ? '新增' : '编辑',
                    area: ['385px', '480px'],
                    content: 'SysCommon_DictAdd.aspx?id=' + id
                });
            });
        }
        function GenerateGradeTermSubjectTeacherBooks() {
            layer.ready(function () {
                layer.confirm('生成目录？', { icon: 3 }, function () {
                    $.get("../AuthApi/index.aspx", { key: "GenerateGradeTermSubjectTeacherBooks", net4: Math.random() },
                    function (data) {
                        if (data == "" || data == null) {
                            layer.msg('生成失败', { icon: 2 })
                        }
                        else {
                            var _json = $.parseJSON(data);
                            if (_json.status) {
                                layer.msg('生成成功', { icon: 1, time: 1000 })
                            }
                            else {
                                layer.msg('生成失败', { icon: 2 })
                            }
                        }
                    });
                });
            });
        }
    </script>
</asp:Content>
