<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="SysModuleFunction.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysModuleFunction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <label>选择系统</label>
                    <input type="hidden" id="hidSysCode" runat="server" clientidmode="Static" />
                    <select id='selSysCode' class="form-control input-sm" clientidmode="Static" onchange="SetFirstToSel_2('selSysCode','selFirstModule','hidSysCode','hidFirstModule','GetSysCodeToJson','GetModuleFristToJson');">
                    </select>
                    <label>选择一级模块</label>
                    <input type="hidden" id="hidFirstModule" runat="server" clientidmode="Static" />
                    <select id='selFirstModule' class="form-control input-sm" clientidmode="Static" onchange="SetSecondToHid('selFirstModule','hidFirstModule');Search();">
                    </select>
                    <asp:Button ID="btnSearch" Text="查询" CssClass="btn btn-default btn-sm" ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                </div>
                <input type="hidden" id="hidModuleId" runat="server" />
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Label runat="server" ID="lblAlert">请选择需要设置功能的模块</asp:Label>
                    </div>
                    <div class="col-xs-4">
                        <asp:TreeView ID="tvModuleFirst" runat="server" ViewStateMode="Enabled" OnSelectedNodeChanged="tvModuleFirst_SelectedNodeChanged">
                        </asp:TreeView>
                    </div>
                    <div class="col-xs-6">
                        <div class="checkbox checkbox_1">
                            <asp:CheckBoxList ID="cblFunction" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" CellSpacing="10">
                            </asp:CheckBoxList>
                        </div>
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="确定" Visible="false"
                            OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //初始化二级联动数据列表
        SetFirstToSel_Init_2('selSysCode', 'selFirstModule', 'hidSysCode', 'hidFirstModule', 'GetSysCodeToJson', 'GetModuleFristToJson');

        //
        function Search() {
            $("#btnSearch").click();
        }
        $(function () {
            if ($("#selSysCode option:selected").val() == "-1" && $('#selSysCode option').length > 1) {
                $("#selSysCode ").get(0).selectedIndex = 1;
                var syscode = $("selSysCode").val();
                var sysname = $("selSysCode").text();
                $("hidSysCode").val(syscode + "|" + sysname);
                SetFirstToSel_2('selSysCode', 'selFirstModule', 'hidSysCode', 'hidFirstModule', 'GetSysCodeToJson', 'GetModuleFristToJson');
            }
            else {
                $("#selSysCode").change(function () {
                    if ($("#selFirstModule option:selected").val() == "-1" && $('#selFirstModule option').length > 1) {
                        $("#selFirstModule ").get(0).selectedIndex = 1;
                        var sysfirstcode = $("selFirstModule").val();
                        var sysfirstname = $("selFirstModule").text();
                        $("hidFirstModule").val(sysfirstcode + "|" + sysfirstname);
                        SetSecondToHid('selFirstModule', 'hidFirstModule');
                    }
                    Search();
                })

            }
            if ($("#selFirstModule option:selected").val() == "-1" && $('#selFirstModule option').length > 1) {
                $("#selFirstModule ").get(0).selectedIndex = 1;
                var sysfirstcode = $("selFirstModule").val();
                var sysfirstname = $("selFirstModule").text();
                $("hidFirstModule").val(sysfirstcode + "|" + sysfirstname);
                SetSecondToHid('selFirstModule', 'hidFirstModule'); Search();
            }

        })
    </script>
</asp:Content>
