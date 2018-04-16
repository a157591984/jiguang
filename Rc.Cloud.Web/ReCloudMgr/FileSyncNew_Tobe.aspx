<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSyncNew_Tobe.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncNew_Tobe" %>

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
                    <button type="button" class="btn btn-primary btn-sm" onclick="showIframe('1')">同步生产数据</button>
                    <button type="button" class="btn btn-primary btn-sm" onclick="showIframe('2')">同步教案文件（教案以及预览图片）</button>
                    <button type="button" class="btn btn-primary btn-sm" onclick="showIframe('3')">同步作业</button>
                    <div class="input-group">
                        <%--<input type="hidden" id="hidtxtSchool" runat="server" clientidmode="Static" />
                        <input type="text" id="txtSchool" clientidmode="Static" runat="server" autocomplete="off" class="form-control input-sm" placeholder="学校名称"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="SCHOOL"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="0"
                            pautocompletepagesize="10" />--%>
                        <div class="input-group-btn">
                            <button type="button" class="btn btn-primary btn-sm" onclick="showIframe('4')">同步学校教案/习题集</button>
                        </div>
                    </div>
                </div>
                <h3 id="iframeTitle">同步生产数据</h3>
                <iframe id='iframDrugFiled' frameborder='0' width='100%' height="550" src='<%=strFileSyncUrlData%>'></iframe>
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
            if (type == 1) {
                page = '<%=strFileSyncUrlData%>';
                $('#iframeTitle').html('同步生产数据');
            }
            else if (type == 2) {
                page = '<%=strFileSyncUrlPlan%>';
                $('#iframeTitle').html('同步教案文件（教案以及预览图片）');
            }
            else if (type == 3) {
                page = '<%=strFileSyncUrlTestPaper%>';
                $('#iframeTitle').html('同步作业');
            }
            else if (type == 4) {
                page = 'SyncFileSchoolList_TobeNew.aspx';
                $('#iframeTitle').html('同步学校教案/习题集');

                <%--if ($.trim($("#hidtxtSchool").val()) == "") {
                    layer.msg("请选择学校", { icon: 2, time: 1000 });
                    return false;
                }
                var dto = {
                    schoolId: $.trim($("#hidtxtSchool").val()),
                    x: Math.random()
                };
                var schoolip_local = "";
                $.ajaxWebService("FileSyncNew_Tobe.aspx/GetSchoolUrl", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);
                    if (json.err == "null") {
                        page = json.SchoolIP + "<%=strFileSyncUrlFileSchool%>" + "&SchoolId=" + $.trim($("#hidtxtSchool").val());

                        schoolip_local = json.SchoolIP_Local;

                        $('#iframeTitle').html('同步学校教案/习题集');
                    }
                    else {
                        layer.msg(json.err, { icon: 2, time: 1000 });
                        return false;
                    }
                }, function () { }, false);

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
                            page = schoolip_local + "<%=strFileSyncUrlFileSchool%>" + "&SchoolId=" + $.trim($("#hidtxtSchool").val());
                        }
                        layer.close(idx);
                    },
                    error: function () {
                        layer.close(idx);
                    }
                });--%>
            }

    if (page != "") $("#iframDrugFiled").attr('src', page);
}
    </script>
</asp:Content>
