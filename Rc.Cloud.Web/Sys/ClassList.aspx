<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ClassList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ClassList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-heading">
                    <div class="panel-title">
                        <asp:Literal runat="server" ID="ltlCount"></asp:Literal>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="hidden" id="hidtxtGrade" clientidmode="Static" class="txt" />
                        <input type="text" placeholder="年级名称" id="txtGrade" clientidmode="Static" class="form-control input-sm" runat="server"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="GRADE"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="0"
                            pautocompletepagesize="10" />
                        <input type="text" id="txtSName" class="form-control input-sm" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                        <input type="button" id="btnBack" class="btn btn-default btn-sm" value="返回" onclick="historyBack();" />
                        <asp:HiddenField runat="server" ID="hidBackurl" ClientIDMode="Static" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr class="tr_title">
                                <td>年级名称</td>
                                <td>名称</td>
                                <td>身份</td>
                                <td>负责人</td>
                                <td>简介</td>
                                <td>创建时间</td>
                                <td>成员</td>
                                <td>操作</td>
                            </tr>
                        </thead>
                        <tbody id="tbSchool">
                        </tbody>
                    </table>
                    <textarea id="template_School" style="display: none">
                        {#foreach $T.list as record}
                            <tr class="tr_con_001">
                                <td>{$T.record.ParentUserGroup_Name}</td>
                                <td>{$T.record.UserGroup_Name}</td>
                                <td>{$T.record.PostName}</td>
                                <td>{$T.record.ClassUser}</td>
                                <td>{$T.record.UserGroup_BriefIntroduction}</td>
                                <td>{$T.record.ClassCreateTime}</td>
                                <td>{$T.record.GroupMemberCount}</td>
                                <td class="opera">
                                    {#if $T.record.MembershipEnum=='classrc'}
                                    <a href="javascript:;" data-name="ClassMemberList" val="{$T.record.UserGroup_Id}">成员列表</a>
                                    <a href="javascript:;" data-name="ImportClassMember" val="{$T.record.UserGroup_Id}"  grade="{$T.record.ParentUserGroup_Name}" class="{$T.record.UserGroup_Name}">导入班级成员</a>
                                     <a href="javascript:;" data-name="ClearClassMember" val="{$T.record.UserGroup_Id}">清空班级成员</a>
                                     <a href="javascript:;" data-name="DeleteClass" val="{$T.record.UserGroup_Id}">删除班级</a>
                                    {#/if}
                                </td>
                            </tr>
                        {#/for}
                    </textarea>
                    <hr />
                    <div class="page"></div>
                </div>
            </div>
        </div>
    </div>
    <!--智能匹配载体-->
    <div id="AutoCompleteVectors" class="AutoCompleteVectors">
        <div id="topAutoComplete" class="topAutoComplete">
            简拼/汉字或↑↓
        </div>
        <div id="divAutoComplete" class="divAutoComplete">
            <ul id="AutoCompleteDataList" class="AutoCompleteDataList"></ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">

        var loadData = function () {
            setBasicUrl();
            var dto = {
                UserGroup_ParentId: "<%=userGroupParentId%>",
                GradeId: $("#hidtxtGrade").val(),
                GroupName: $.trim($("#txtSName").val()),
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("ClassList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
            basicUrl = "ClassList.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex + "&sname=" + escape($.trim($("#txtSName").val())) + "&userGroupParentId=" + "<%=userGroupParentId%>&backurl=" + $("#hidBackurl").val() + "&txtGrade=" + escape($.trim($("#txtGrade").val())) + "&hidtxtGrade=" + $("#hidtxtGrade").val());
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            $("#txtSName").val(unescape(getUrlVar("sname")));
            $("#txtGrade").val(unescape(getUrlVar("txtGrade")));
            $("#hidtxtGrade").val(getUrlVar("hidtxtGrade"));
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
            $(document).on("click", '[data-name="ClassMemberList"]', function () {
                window.location.href = "ClassMemberList.aspx?backurl=" + backurl + "&userGroupParentId=" + $(this).attr("val");
            });
            $(document).on("click", '[data-name="ImportClassMember"]', function () {
                window.location.href = "ImportClassMember.aspx?backurl=" + backurl + "&userGroupParentId=" + $(this).attr("val") + "&grade=" + $(this).attr("grade") + "&class=" + $(this).attr("class");
            });
            $(document).on("click", '[data-name="ClearClassMember"]', function () {
                var bid = $(this).attr("val");
                layer.ready(function () {
                    layer.confirm('确定要清空此班级的所有成员吗？', { icon: 4 }, function () {
                        var dto = {
                            UserGroup_Id: bid,
                            x: Math.random()
                        };
                        $.ajaxWebService("ClassList.aspx/ClearClassMember", JSON.stringify(dto), function (data) {
                            var json = $.parseJSON(data.d);
                            if (json.err == "null") {
                                layer.ready(function () {
                                    layer.msg(json.msg, { icon: 1, time: 1000 }, function () {
                                        loadData();
                                    });
                                })
                            }
                            else {
                                layer.ready(function () {
                                    layer.msg(json.msg, { icon: 2 });
                                })
                            }
                        }, function () { }, false);
                    }, function () { });
                })
            });
            $(document).on("click", '[data-name="DeleteClass"]', function () {
                var bid = $(this).attr("val");
                layer.ready(function () {
                    layer.confirm('此班级的成员将一并删除，确定要删除吗？', { icon: 4 }, function () {
                        var dto = {
                            UserGroup_Id: bid,
                            x: Math.random()
                        };
                        $.ajaxWebService("ClassList.aspx/DeleteClass", JSON.stringify(dto), function (data) {
                            var json = $.parseJSON(data.d);
                            if (json.err == "null") {
                                layer.ready(function () {
                                    layer.msg(json.msg, { icon: 1, time: 1000 }, function () {
                                        loadData();
                                    });
                                })
                            }
                            else {
                                layer.ready(function () {
                                    layer.msg(json.msg, { icon: 2 });
                                })
                            }
                        }, function () { }, false);
                    }, function () { });
                });
            })
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
