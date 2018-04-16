<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="CloudResUserEmpower.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.CloudResUserEmpower" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlUserTitle" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                    <input type="text" id="txtKey" class="form-control input-sm" placeholder="姓名" clientidmode="Static">
                    <input type="text" id="txtClassId" class="form-control input-sm" placeholder="班级号/名称" clientidmode="Static">
                    <input type="text" id="txtGradeId" class="form-control input-sm" placeholder="年级号/名称" clientidmode="Static">
                    <input type="hidden" id="hidtxtSchool" clientidmode="Static" class="form-control input-sm" runat="server" />
                    <input type="text" id="txtSchool" clientidmode="Static" class="form-control input-sm" runat="server" placeholder="学校名称"
                        pautocomplete="True"
                        pautocompleteajax="AjaxAutoCompletePaged"
                        pautocompleteajaxkey="SCHOOL"
                        pautocompletevectors="AutoCompleteVectors"
                        pautocompleteisjp="0"
                        pautocompletepagesize="10" />
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit">
                    <input type="button" name="btnBePower" value="批量授权" onclick="SelectAllChk();" class="btn btn-default btn-sm">
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th width="5%">
                                <label for="">
                                    <input type="checkbox" name='checkAll' data-name="ChackAll">全选</label></th>
                            <th>姓名</th>
                            <th width="15%">学科</th>
                            <th width="15%">职务</th>
                            <th width="5%">授权</th>
                        </tr>
                    </thead>
                    <tbody id="tb">
                    </tbody>
                </table>
                <div class="page" id="pageU">
                    <ul>
                    </ul>
                </div>
                <asp:HiddenField ID="hidUserlist" runat="server" ClientIDMode="Static" />
            </div>
        </div>
        <!--智能匹配载体-->
        <div id="AutoCompleteVectors" class="AutoCompleteVectors" style="display: none">
            <div id="topAutoComplete" class="topAutoComplete">
                简拼/汉字或↑↓
            </div>
            <div id="divAutoComplete" class="divAutoComplete">
                <ul id="AutoCompleteDataList" class="AutoCompleteDataList">
                </ul>
            </div>
        </div>
    </div>
    <textarea id="template_list" style="display: none">
        {#foreach $T.list as record}
        <tr class="tr_con_001">
            <td>
                <input type='checkbox' name='checkAll' value='{$T.record.UserId}' />
            </td>
            <td>{$T.record.TrueName}（{$T.record.UserName}）</td>
            <td>{$T.record.SubjectName}</td>
            <td>{$T.record.UserTitleName}</td>
            <td class="opera">
                <a title="授权" href="javascript:;" onclick="SQ('{$T.record.UserId}');">授权</a>
            </td>
         </tr>
        {#/for}
    </textarea>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">

    <script type="text/javascript">
        var loadData = function () {
            var dto = {
                Subjectid: $("#ddlSubject").val(),
                UserTitle: $("#ddlUserTitle").val(),
                UserName: $("#txtKey").val(),
                ClassId: $("#txtClassId").val(),
                GradeId: $("#txtGradeId").val(),
                School: $("#hidtxtSchool").val(),
                PageSize: ($("#pageU [data-name='pagination_select']").val() == undefined ? 10 : $("#pageU [data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("CloudResUserEmpower.aspx/GetUserList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb").setTemplateElement("template_list", null, { filter_data: false });
                    $("#tb").processTemplate(json);
                    $("#pageU").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                    if (json.list == null || json.list == "") {
                        pageIndex--;
                        if (pageIndex > 0) {
                            loadData();
                        }
                        else {
                            pageIndex = 1;
                        }
                    }
                }
                else {
                    $("#tb").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $("#pageU").html("");
                }
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }




        //授权弹窗
        function showPop() {
            layer.open({
                type: 2,
                title: '授权',
                area: ['90%', '90%'],
                content: 'CloudResList.aspx'
            });
        }
        function SelectAllChk() {
            var result = false;
            userArr = new Array;
            $("#tb input[name='checkAll']").each(function () {
                if ($(this).prop("checked")) {
                    userArr.push($(this).val());
                }

            });
            if (userArr.length > 0) {
                $("#hidUserlist").val(userArr.join(","));
                result = true;
                showPop();
            }
            else {
                layer.msg("请选择授权人", { icon: 2, time: 1000 })
            }
            return result;
        }

        var SQ = function (userid) {
            $("#hidUserlist").val(userid);
            showPop();
        }
        $(function () {
            pageIndex = 1;
            loadData();

            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlschool").change(function () {
                pageIndex = 1;
                loadData();
            })
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                loadData();
            })
            $("#ddlUserTitle").change(function () {
                pageIndex = 1;
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
            // 全选
            $('[data-name="ChackAll"]').on({
                click: function () {
                    var Name = $(this).prop('name');
                    var IsChecked = $(this).is(':checked');
                    var ChackName = $('input[type="checkbox"][name="' + Name + '"]:enabled')
                    if (IsChecked) {
                        ChackName.prop('checked', true);
                    } else {
                        ChackName.prop('checked', false);
                    }
                }
            })

            $('[data-name="ChackAlls"]').on({
                click: function () {

                    var Name = $(this).prop('name');
                    var IsChecked = $(this).is(':checked');
                    var ChackName = $('input[type="checkbox"][name="' + Name + '"]:enabled')
                    if (IsChecked) {
                        ChackName.prop('checked', true);
                    } else {
                        ChackName.prop('checked', false);
                    }
                }
            });

        });

    </script>

</asp:Content>
