<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="FileSyncFailText.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncFailText" Async="true" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/styles003/style01.css" rel="stylesheet" />
    <script src="../Scripts/js001/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/function.js"></script>
    <link href="../Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/laydate/laydate.js"></script>
    <script type="text/javascript" src="../Scripts/js001/Tree.js"></script>
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/AutoComplete.js?<%=new Random().Next() %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 20px;">
            <table class="table_search_001">
                <tr>
                    <td>资源生产日期：</td>
                    <td>
                        <asp:TextBox ID="txtStartTime" runat="server" ClientIDMode="Static" CssClass="txt_Search myTextBox laydate-icon" placeholder="开始日期"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="10px" colspan="2"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtEndTiem" runat="server" ClientIDMode="Static" CssClass="txt_Search myTextBox laydate-icon" placeholder="结束日期"></asp:TextBox></td>
                </tr>
                <tr>
                    <td height="10px" colspan="2"></td>
                </tr>
                <tr>
                    <td>书名：</td>
                    <td>
                        <input type="hidden" id="hidtxtBook" clientidmode="Static" runat="server" class="txt" />
                        <input type="text" id="txtBook" placeholder="简拼/汉字" clientidmode="Static" class="txt" runat="server"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="BOOK"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="0"
                            pautocompletepagesize="10" />
                    </td>
                </tr>
                <tr>
                    <td height="10px" colspan="2"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnOn" ClientIDMode="Static" OnClick="btnOn_Click" Text="检查同步失败文件" CssClass="btn" /></td>
                </tr>
                <%--<tr>
                    <td colspan="2"><span style="color: red; margin: 0;">注意：同步文件过多时会有延时，请注意同步频率（建议同步时间间隔在10分钟以上）！</span></td>
                </tr>--%>
            </table>
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
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {

        $("#btnOn").click(function () {
            if ($.trim($("#txtStartTime").val()) == "") {
                layer.msg("请选择开始时间", { icon: 2, time: 2000 });
                return false;
            }
            if ($.trim($("#txtEndTiem").val()) == "") {
                layer.msg("请选择结束时间", { icon: 2, time: 2000 });
                return false;
            }
            layer.load();
        });
    });


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
