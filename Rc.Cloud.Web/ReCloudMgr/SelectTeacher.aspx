﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectTeacher.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SelectTeacher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-inline search_bar mb">
                <input name="txtSearchNameS" type="text" id="txtSearchNameS" placeholder="姓名" class="form-control input-sm" runat="server">
                <asp:DropDownList ID="ddlPower" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    <asp:ListItem Text="全部状态" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="已授权" Value="1"></asp:ListItem>
                    <asp:ListItem Text="未授权" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <%--<asp:DropDownList ID="ddlShoole" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                                </asp:DropDownList>--%>
                <input type="hidden" id="hidtxtSchool" clientidmode="Static" class="txt" runat="server" />
                <input type="text" id="txtSchool" clientidmode="Static" class="form-control input-sm" runat="server"
                    pautocomplete="True"
                    pautocompleteajax="AjaxAutoCompletePaged"
                    pautocompleteajaxkey="SCHOOL"
                    pautocompletevectors="AutoCompleteVectors"
                    pautocompleteisjp="0"
                    pautocompletepagesize="10" />
                <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnSearch" Text="查询" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" />
                <asp:Button ID="btnBePower" Text="批量授权" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnBePower_Click" OnClientClick="return SelectAllChk()" />
                <asp:Button ID="Button1" Text="批量删除授权" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnDeleteBePower_Click" OnClientClick="return SelectAllChk()" />
            </div>
            <asp:Literal ID="litContent" ClientIDMode="Static" runat="server"></asp:Literal>
            <%= GetPageIndex() %>
        </div>
        <input type="hidden" id="hidUserIDs" runat="server" />
        <input type="hidden" id="Hidden1" runat="server" />
        <!-- 背景层DIV -->
        <div class="div_documentbg" id="div_documentbg">
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
<script>
    $(function () {
        //班复选框
        $(".table input[name='checkAll']").click(function () {
            $(this).closest('thead').next('tbody').find('input:checkbox').prop('checked', $(this).is(':checked'))
        });
    });
    function SelectAllChk() {
        var result = false;
        var _IDs = "";
        $("#table_content input[name='name']").each(function () {
            if ($(this).prop("checked")) {
                _IDs += $(this).val() + ",";
            }
        });
        if (_IDs != "") {
            $("#hidUserIDs").val(_IDs);
            result = true;
        }
        else {
            alert("请选择！")
        }
        return result;
    }
</script>
