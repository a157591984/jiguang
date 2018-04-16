<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSyncAuto_FailDetail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncAuto_FailDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../Scripts/base64.js"></script>
    <script src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnBack").click(function () {
                window.location.href = "FileSyncAuto_Fail_Book.aspx";
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"><%--<a href="javascript:history.back(-1);">返回上一级</a>--%></div>
        <%=siteMap%><div class="div_right_title_002"></div>
        <div class="div_right_title_001" id="div_right_title_1"><%=ResourceFolder_NameE %></div>
    </div>
    <div class="clearDiv"></div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tbody>
                <tr>
                    <td>
                        <input type="button" class="btn" onclick="history.back(-1);" value="返回"  />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="width: 100%">
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>目录明细</td>
                        <td style="width: 30%;">文件名称</td>
                        <td style="width: 30%;">路径</td>
                        <td style="width: 5%;">题号</td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal ID="ltlTest" runat="server" ClientIDMode="Static"></asp:Literal>
                </tbody>
            </table>

        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
