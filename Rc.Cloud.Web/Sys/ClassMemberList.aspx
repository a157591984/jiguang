<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ClassMemberList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ClassMemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap %>
        <div class="panel">
            <div class="panel-heading">
                <div class="panel-title">
                    <asp:Literal runat="server" ID="ltlCount"></asp:Literal>
                </div>
            </div>
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="text" id="txtSName" class="form-control input-sm" placeholder="名称" />
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    <input type="button" id="btnBack" class="btn btn-default btn-sm" value="返回" onclick="historyBack();" />
                    <asp:HiddenField runat="server" ID="hidBackurl" ClientIDMode="Static" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>班级名称</th>
                            <th>姓名</th>
                            <th>身份</th>
                            <th>邮箱</th>
                            <th>加入时间</th>
                        </tr>
                    </thead>
                    <tbody id="tbSchool">
                    </tbody>
                </table>
                <textarea id="template_School" class="hidden">
                    {#foreach $T.list as record}
                        <tr class="tr_con_001">
                            <td>{$T.record.ClassName}</td>
                            <td>{$T.record.UserName}</td>
                            <td class="align_center">{$T.record.MembershipEnumName}</td>
                            <td class="align_center">{$T.record.Email}</td>
                            <td class="align_center">{$T.record.User_ApplicationPassTime}</td>
                        </tr>
                    {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        var loadData = function () {
            setBasicUrl();
            var dto = {
                UserGroup_ParentId: "<%=userGroupParentId%>",
                GroupName: $.trim($("#txtSName").val()),
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("ClassMemberList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbSchool").setTemplateElement("template_School", null, { filter_data: false });
                    $("#tbSchool").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbSchool").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".page").html("");
                }
                if (json.list == null || json.list == "") {
                    pageIndex--;
                    if (pageIndex > 0) {
                        loadData();
                    }
                    else {
                        pageIndex = 1;
                    }
                }
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var setBasicUrl = function () {
            basicUrl = "ClassMemberList.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex + "&sname=" + escape($.trim($("#txtSName").val())) + "&userGroupParentId=" + "<%=userGroupParentId%>&backurl=" + $("#hidBackurl").val());
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            $("#txtSName").val(unescape(getUrlVar("sname")));
        }
        $(function () {
            b = new Base64();
            pageIndex = 1; //页码
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadParaFromLink();
            loadData();

            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
        });
        function historyBack() {
            b = new Base64();
            var backurl = getUrlVar("backurl");
            if (backurl != "") {
                window.location.href = b.decode(backurl);
            }
            else {
                window.location.href = "SchoolList.aspx";
            }
        }
    </script>
</asp:Content>
