<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSyncFail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncFail" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/laydate/laydate.js"></script>
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/AutoComplete.js?<%=new Random().Next() %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"></div>
        <%=siteMap%>
    </div>
    <div class="clearDiv"></div>
    <div class="clear"></div>
    <div style="width: 100%">
        <div class="div_right_search">
            <table class="table_search_001">
                <tr>
                    <%--    <td>资源生产日期：<asp:TextBox ID="txtStartTime" runat="server" ClientIDMode="Static" CssClass="txt_Search myTextBox laydate-icon" placeholder="开始日期"></asp:TextBox>-
                        <asp:TextBox ID="txtEndTiem" runat="server" ClientIDMode="Static" CssClass="txt_Search myTextBox laydate-icon" placeholder="结束日期"></asp:TextBox></td>
                    <asp:DropDownList ID="ddlFiletype" runat="server" CssClass="user_ddl"></asp:DropDownList>
                    <td>书名：<input type="hidden" id="hidtxtBook" clientidmode="Static" runat="server" class="txt" />
                        <input type="text" id="txtBook" clientidmode="Static" class="txt" runat="server"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="BOOK"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="0"
                            pautocompletepagesize="10" /></td>
                    <td>
                        <asp:Button runat="server" ID="btnOn" ClientIDMode="Static" OnClick="btnOn_Click" Text="同步失败文件" CssClass="btn" />
                    </td>--%>
                    <td>
                        <input type="button" class="btn" value="检测教案失败数据" id="btnClass" />
                    </td>
                    <td>
                        <input type="button" class="btn" value="检测习题集失败数据" id="btnTest" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnDate" ClientIDMode="Static" OnClick="btnDate_Click" Text="同步失败数据" CssClass="btn" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnUsering" ClientIDMode="Static" OnClick="btnUsering_Click" Text="同步失败文件" CssClass="btn" />
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2"><span style="color: red; margin: 0;">注意：同步文件过多时会有延时，请注意同步频率（建议同步时间间隔在10分钟以上）！</span></td>
                </tr>--%>
            </table>
        </div>
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>资源名称</td>
                        <td>检测时间</td>
                        <td>文件路径</td>
                        <td>资源类型</td>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <hr />
            <div class="page"></div>
        </div>
    </div>
    <textarea id="template_tb1" style="display: none">
    {#foreach $T.list as record}
    <tr class="tr_con_001">
        <td style="text-align:center">{$T.record.Name}</td>
        <td style="text-align:center"> {$T.record.SyncFailTime}</td>
        <td style="width:50%"> {$T.record.url}</td>
        <td style="text-align:center">{$T.record.type}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            pageIndex = 1; //页码
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadData();

            $("#btnDate").click(function () { layer.load(); loadData(); });

            $("#btnUsering").click(function () {

                layer.load();
            });

            $("#btnClass").click(function () {
                var _strReveiveWebSiteUrl = "<%=strReveiveWebSiteUrl%>";
                layer.open({
                    type: 2,
                    title: '检测教案同步失败数据',
                    area: ['60%', '80%'],
                    content: _strReveiveWebSiteUrl + 'ReCloudMgr/FileSyncFailClass.aspx',
                    cancel: function (index) {
                        layer.close(index);

                        Syn();
                    }
                });
            })
            $("#btnTest").click(function () {
                var _SynTestWebSiteUrl = "<%=SynTestWebSiteUrl%>";
                layer.open({
                    type: 2,
                    title: '检测习题集同步失败数据',
                    area: ['60%', '80%'],
                    content: _SynTestWebSiteUrl + 'ReCloudMgr/FileSyncFailText.aspx',
                    cancel: function (index) {
                        layer.close(index);

                        Syn();

                    }
                });
            })
        });



        var Syn = function () {
            var dto = {
                x: Math.random()
            };
            $.ajaxWebService("http://192.168.1.16:81/ReCloudMgr/FileSyncFail.aspx/Syn", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    parent.layer.msg('正在同步失败数据.......', { time: 5000, icon: 6 }, function () {
                        loadData();
                    });
                }
                else {
                }
            }, function () { });
        }

        var loadData = function () {
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };

            $.ajaxWebService("FileSyncFail.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_tb1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);

                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr class='tr_con_001'><td style=\"padding-right: 4px; text-align: center;color:red;\" colspan=\"100\">暂无数据</td></tr>");

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

        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        $(function () {
            /**
             * 日期插件
             */

            $('#txtStartTime').val(laydate.now(0, 'YYYY-MM-DD hh:mm:ss'));
            $('#txtEndTiem').val(laydate.now(1, 'YYYY-MM-DD hh:mm:ss'));
            var starTime = {
                elem: '#txtStartTime',
                format: 'YYYY-MM-DD hh:mm:ss',
                istime: true, //是否开启时间选择
                choose: function (datas) {
                    endTime.min = datas; //开始日选好后，重置结束日的最小日期
                    endTime.start = datas //将结束日的初始值设定为开始日
                }
            };
            var endTime = {
                elem: '#txtEndTiem',
                format: 'YYYY-MM-DD hh:mm:ss',
                istime: true, //是否开启时间选择
                choose: function (datas) {
                    starTime.max = datas; //结束日选好后，重置开始日的最大日期
                }
            };
            laydate(starTime);
            laydate(endTime);
        })
    </script>
</asp:Content>
