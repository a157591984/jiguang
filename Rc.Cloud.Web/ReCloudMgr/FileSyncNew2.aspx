<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSyncNew2.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncNew2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa file_sync">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar">
                    <asp:DropDownList ID="ddlSource" CssClass="form-control input-sm" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="来源服务器" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlTarget" CssClass="form-control input-sm" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="目标服务器" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                        <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                        <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <%--<div class="input-group">
                        <input type="hidden" id="hidtxtSchool3" runat="server" clientidmode="Static" />
                        <input type="text" id="txtSchool3" clientidmode="Static" runat="server" autocomplete="off" class="form-control input-sm" placeholder="学校名称"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="SCHOOL"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="0"
                            pautocompletepagesize="10" />

                    </div>--%>
                    <button type="button" class="btn btn-primary btn-sm" onclick="showIframe(1)">处理老师自有教案</button>
                    <button type="button" class="btn btn-primary btn-sm" onclick="showIframe(2)">处理老师自有习题集</button>
                    <button type="button" class="btn btn-primary btn-sm" onclick="showIframe(3)">处理作业文件</button>
                </div>
                <h3 id="iframeTitle"></h3>
                <iframe id='iframDrugFiled' frameborder='0' width='100%' height="550" src=''></iframe>
            </div>
        </div>
    </div>

    <!--智能匹配载体-->
    <div id="AutoCompleteVectors" class="AutoCompleteVectors">
        <div id="topAutoComplete" class="topAutoComplete">
            简拼/汉字或↑↓
        </div>
        <div id="divAutoComplete" class="divAutoComplete">
            <ul id="AutoCompleteDataList" class="AutoCompleteDataList">
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            e.target
            e.relatedTarget
            var $_dropdowm = $(this).next('.dropdown-menu');
            if ($_dropdowm.length == 1 && $_dropdowm.is(':hidden')) {
                $_dropdowm.show();
            } else {
                $('#schoolPlan').hide();
            }
        })
        function showIframe(type) {
            var page;
            if (type == 1 || type == 2) {
                if ($.trim($("#ddlSource").val()) == "") {
                    layer.msg("请选择来源服务器", { icon: 2, time: 2000 }, function () { $("#ddlSource").focus(); });
                    return false;
                }
                if ($.trim($("#ddlTarget").val()) == "") {
                    layer.msg("请选择目标服务器", { icon: 2, time: 2000 }, function () { $("#ddlTarget").focus(); });
                    return false;
                }
                if ($.trim($("#ddlSource").val()) == $.trim($("#ddlTarget").val())) {
                    layer.msg("来源服务器地址不能与目标服务器地址相同", { icon: 2, time: 2000 }, function () { $("#ddlTarget").focus(); });
                    return false;
                }

                var schoolId_Target = "";// 目标服务器 学校标识
                var schoolId = "";
                var sourceServerUrl;
                var targetServerUrl;
                var sourceServer = $.trim($("#ddlSource").val()).split("^");
                var targetServer = $.trim($("#ddlTarget").val()).split("^");
                if (sourceServer[0] != "") {
                    schoolId = sourceServer[0];
                }
                sourceServerUrl = sourceServer[1];
                if (schoolId == "" && targetServer[0] != "") {
                    schoolId = targetServer[0];
                    schoolId_Target = schoolId;
                }
                targetServerUrl = targetServer[1];

                if (sourceServerUrl == "") {
                    layer.msg("来源服务器为空", { icon: 2, time: 2000 });
                    return false;
                }
                if (targetServerUrl == "") {
                    layer.msg("目标服务器为空", { icon: 2, time: 2000 });
                    return false;
                }

                $('#iframeTitle').html('处理老师自有教案');
                var fileUrl = "<%=strFileSyncUrlTchPlanFileSchool%>";//执行下载文件路径
                if (type == "2") {
                    $('#iframeTitle').html('处理老师自有习题集');
                    fileUrl = "<%=strFileSyncUrlTchTestpaperFileSchool%>";
                }
                page = targetServerUrl + fileUrl
                    + "&SchoolId=" + schoolId
                    + "&sourceServerUrl=" + encodeURIComponent(sourceServerUrl)
                    + "&targetServerUrl=" + encodeURIComponent(targetServerUrl);
                

                if (schoolId_Target != "") {
                    var schoolip_local = "";
                    var dto = {
                        schoolId: schoolId,
                        x: Math.random()
                    };
                    $.ajaxWebService("FileSyncNew2.aspx/GetSchoolUrl", JSON.stringify(dto), function (data) {
                        var json = $.parseJSON(data.d);
                        if (json.err == "null") {
                            schoolip_local = json.SchoolIP_Local;
                        }
                    }, function () { }, false);
                    if (schoolip_local != "") {
                        $.ajax({
                            async: false,
                            timeout: 2000,
                            type: "get",
                            url: schoolip_local + "AuthApi/onlinecheck.ashx",
                            data: "",
                            dataType: "text",
                            beforeSend: function () {
                                idx = layer.load();
                            },
                            complete: function (XMLHttpRequest, status) {
                                if (status == "timeout") {//超时,status还有success,error等值的情况

                                }
                            },
                            success: function (data) {
                                if (data == "ok") {
                                    page = schoolip_local + fileUrl
                                        + "&SchoolId=" + schoolId
                                        + "&sourceServerUrl=" + encodeURIComponent(sourceServerUrl)
                                        + "&targetServerUrl=" + encodeURIComponent(targetServerUrl);
                                }
                                layer.close(idx);
                            },
                            error: function () {
                                layer.close(idx);
                            }
                        });
                    }
                }

            }
            else if (type == 3) {
                if ($.trim($("#ddlSource").val()) == "") {
                    layer.msg("请选择来源服务器", { icon: 2, time: 2000 }, function () { $("#ddlSource").focus(); });
                    return false;
                }
                if ($.trim($("#ddlTarget").val()) == "") {
                    layer.msg("请选择目标服务器", { icon: 2, time: 2000 }, function () { $("#ddlTarget").focus(); });
                    return false;
                }
                if ($.trim($("#ddlSource").val()) == $.trim($("#ddlTarget").val())) {
                    layer.msg("来源服务器地址不能与目标服务器地址相同", { icon: 2, time: 2000 }, function () { $("#ddlTarget").focus(); });
                    return false;
                }

                var schoolId_Target = "";// 目标服务器 学校标识
                var schoolId = "";
                var sourceServerUrl;
                var targetServerUrl;
                var sourceServer = $.trim($("#ddlSource").val()).split("^");
                var targetServer = $.trim($("#ddlTarget").val()).split("^");
                if (sourceServer[0] != "") {
                    schoolId = sourceServer[0];
                }
                sourceServerUrl = sourceServer[1];
                if (schoolId == "" && targetServer[0] != "") {
                    schoolId = targetServer[0];
                    schoolId_Target = schoolId;
                }
                targetServerUrl = targetServer[1];

                if (sourceServerUrl == "") {
                    layer.msg("来源服务器为空", { icon: 2, time: 2000 });
                    return false;
                }
                if (targetServerUrl == "") {
                    layer.msg("目标服务器为空", { icon: 2, time: 2000 });
                    return false;
                }

                page = targetServerUrl + "<%=strFileSyncUrlHWFileSchool%>"
                    + "&SchoolId=" + schoolId
                    + "&sourceServerUrl=" + encodeURIComponent(sourceServerUrl)
                    + "&targetServerUrl=" + encodeURIComponent(targetServerUrl);
                $('#iframeTitle').html('处理作业文件');

                if (schoolId_Target != "") {
                    var schoolip_local = "";
                    var dto = {
                        schoolId: schoolId,
                        x: Math.random()
                    };
                    $.ajaxWebService("FileSyncNew2.aspx/GetSchoolUrl", JSON.stringify(dto), function (data) {
                        var json = $.parseJSON(data.d);
                        if (json.err == "null") {
                            schoolip_local = json.SchoolIP_Local;
                        }
                    }, function () { }, false);
                    if (schoolip_local != "") {
                        $.ajax({
                            async: false,
                            timeout: 2000,
                            type: "get",
                            url: schoolip_local + "AuthApi/onlinecheck.ashx",
                            data: "",
                            dataType: "text",
                            beforeSend: function () {
                                idx = layer.load();
                            },
                            complete: function (XMLHttpRequest, status) {
                                if (status == "timeout") {//超时,status还有success,error等值的情况

                                }
                            },
                            success: function (data) {
                                if (data == "ok") {
                                    page = schoolip_local + "<%=strFileSyncUrlHWFileSchool%>"
                                        + "&SchoolId=" + schoolId
                                        + "&sourceServerUrl=" + encodeURIComponent(sourceServerUrl)
                                        + "&targetServerUrl=" + encodeURIComponent(targetServerUrl);
                                }
                                layer.close(idx);
                            },
                            error: function () {
                                layer.close(idx);
                            }
                        });
                    }
                }

            }
        if (page != "") $("#iframDrugFiled").attr('src', page);
    }
    </script>
</asp:Content>
