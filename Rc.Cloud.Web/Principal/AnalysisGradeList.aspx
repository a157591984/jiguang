<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="AnalysisGradeList.aspx.cs" Inherits="Rc.Cloud.Web.Principal.AnalysisGradeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <asp:Repeater runat="server" ID="rptStudysection" OnItemDataBound="rptStudysection_ItemDataBound">
                <ItemTemplate>
                    <h3 class="page_title"><%#Eval("GroupGradeTypeName") %></h3>
                    <div class="grade_panel">
                        <asp:Repeater runat="server" ID="rptGrade">
                            <ItemTemplate>
                                <div class="panel_item <%#((System.Data.DataRow)Container.DataItem)["ClassCount"].ToString()=="0"?"disabled warning":"" %>">
                                    <div class='item_label'>无用户</div>
                                    <a href="EachGreadAnalysisList.aspx?GradeId=<%#((System.Data.DataRow)Container.DataItem)["GradeId"] %>&GradeName=<%#Server.UrlEncode(((System.Data.DataRow)Container.DataItem)["GradeName"].ToString()) %>">
                                        <div class="item_heading">
                                            <div class="name">
                                                <%#((System.Data.DataRow)Container.DataItem)["GradeName"]%>
                                            </div>
                                        </div>
                                        <div class="item_footer">
                                            <ul>
                                                <li><%#((System.Data.DataRow)Container.DataItem)["ClassCount"]%> 班级</li>
                                                <li><%#((System.Data.DataRow)Container.DataItem)["StudentCount"]%> 成员</li>
                                            </ul>
                                        </div>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            // LoadData();
            $('a.disabled').each(function () {
                $(this).attr('href', 'javascript:;');
            })
        })
        var LoadData = function () {
            var $_objBox = $("#listBox");
            var objBox = "listBox";
            var $_objList = $("#list");
            var dto = {
                UserID: "<%=UserID%>",
                x: Math.random()
            };
            $.ajaxWebService("AnalysisGradeList.aspx/GetGradeList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                    if (json.list == null || json.list == "") {
                        $_objBox.html("暂无数据");
                    }
                }
                else {
                    $_objList.html("暂无数据");
                }
            }, function () { })
        }
    </script>
</asp:Content>
