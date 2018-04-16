<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SchoolList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SchoolList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <input type="text" id="txtSName" class="form-control input-sm" placeholder="学校名称" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr class="tr_title">
                                <th>学校名称</th>
                                <th>负责人</th>
                                <th width="30%">简介</th>
                                <th>创建时间</th>
                                <th>成员</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="tbSchool"></tbody>
                    </table>
                    <textarea id="template_School" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.UserGroup_Name}</td>
                                <td>{$T.record.CreateUser}</td>
                                <td>{$T.record.UserGroup_BriefIntroduction}</td>
                                <td>{$T.record.CreateTime}</td>
                                <td>{$T.record.GroupMemberCount}</td>
                                <td class="opera">      
                                    <a href="javascript:;" data-name="GradeList" val="{$T.record.UserGroup_Id}">成员列表</a> 
                                    <a href="javascript:;" data-name="ImportGrade" val="{$T.record.UserGroup_Id}" school="{$T.record.UserGroup_Name}">导入年级</a>
                                    <a href="javascript:;" data-name="ClearSchoolMember" val="{$T.record.UserGroup_Id}">清空成员</a>
                                    <a href="javascript:;" data-name="DeleteSchool" val="{$T.record.UserGroup_Id}">删除学校</a>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var loadData = function () {
            setBasicUrl();
            var dto = {
                GroupName: $.trim($("#txtSName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("SchoolList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
            basicUrl = "SchoolList.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex + "&sname=" + escape($.trim($("#txtSName").val())));
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
            $(document).on("click", '[data-name="GradeList"]', function () {
                window.location.href = "GradeList.aspx?backurl=" + backurl + "&userGroupParentId=" + $(this).attr("val");
            });
            $(document).on("click", '[data-name="ImportGrade"]', function () {
                window.location.href = "ImportGrade.aspx?backurl=" + backurl + "&userGroupParentId=" + $(this).attr("val") + "&school=" + $(this).attr("school");
            });
            $(document).on("click", '[data-name="ClearSchoolMember"]', function () {
                var bid = $(this).attr("val");
                layer.ready(function () {
                    layer.confirm('确定要清空学校的所有成员吗？', { icon: 4 }, function () {
                        var dto = {
                            UserGroup_Id: bid,
                            x: Math.random()
                        };
                        $.ajaxWebService("SchoolList.aspx/ClearSchoolMember", JSON.stringify(dto), function (data) {
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
                    });
                });
            });
            $(document).on("click", '[data-name="DeleteSchool"]', function () {
                var bid = $(this).attr("val");//此学校的成员将一并删除，
                layer.ready(function () {
                    layer.confirm('确定要删除吗？', { icon: 4 }, function () {
                        var dto = {
                            UserGroup_Id: bid,
                            x: Math.random()
                        };
                        $.ajaxWebService("SchoolList.aspx/DeleteSchool", JSON.stringify(dto), function (data) {
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
                    });
                });
            });
        });
    </script>
</asp:Content>
