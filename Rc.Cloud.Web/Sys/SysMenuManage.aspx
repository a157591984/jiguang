<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="SysMenuManage.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysMenuManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>菜单管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input class="btn btn-primary btn-sm" value="新增模块" type="button" id="btn_Add" onclick="showEdit('0', '');" />
                    <asp:TextBox ID="moduleNameTxt" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="模块名称"></asp:TextBox>
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="查询" ID="btn_Search" OnClick="btn_Search_Click" />
                </div>
                <asp:Repeater ID="rptModule" runat="server">
                    <HeaderTemplate>
                        <table class='table table-hover table-bordered'>
                            <thead>
                                <tr>
                                    <%--<th style="width: 100px; display:none;">
                            系统名称
                        </th>--%>
                                    <th>模块编码</th>
                                    <th>模块名称</th>
                                    <th>父模块编码</th>
                                    <th>SLEVEL</th>
                                    <th>链接地址</th>
                                    <th>显示</th>
                                    <th>菜单级别</th>
                                    <th>最后一级</th>
                                    <th title="最大值是默认页">默认页</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%-- <td class="aui_td_content" style="display:none";>
                        <%//#Eval("SysName")%>
                    </td>--%>
                            <td>
                                <%# Eval("MODULEID") %>
                            </td>
                            <td>
                                <%#Eval("MODULENAME")%>
                            </td>
                            <td>
                                <%#Eval("PARENTID")%>
                            </td>
                            <td>
                                <%#Eval("SLEVEL")%>
                            </td>
                            <td>
                                <%#Eval("URL")%>
                            </td>
                            <td>
                                <%#Eval("ISINTREE").ToString()=="Y"?"是":"否"%>
                            </td>
                            <td>
                                <%#Eval("Depth")%>
                            </td>
                            <td>
                                <%#Eval("isLast").ToString()=="1"?"是":"否"%>
                            </td>
                            <td title="最大值是默认页">
                                <%#Eval("DefaultOrder")%>
                            </td>
                            <td class="opera">
                                <a href="javascript:void(0)" onclick="showEdit('1','<%#Eval("MODULEID")%>')">类似新增</a>
                                <a href="javascript:void(0)" onclick="showEdit('2','<%#Eval("MODULEID") %>')">编辑</a>
                                <a href="javascript:void(0)" onclick="DeleteSysModuleByID('<%#Eval("MODULEID") %>')">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var pageUrl = '<%=strPageNameAndParm%>';
        function showEdit(action, moduleID, sysCode) {
            var title = '';
            if (action == '0') {
                title = '新增'
            } else if (action == '1') {
                title = '类似新增'
            } else if (action == '2') {
                title = '编辑'
            }
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: title,
                    area: ['650px','500px'],
                    content: 'SysMenuManageEdit.aspx?action=' + action + '&moduleID=' + moduleID
                });
            });
        }
        function DeleteSysModuleByID(id) {
            layer.ready(function () {
                layer.confirm('删除数据？', function () {
                    jQuery.get("../Ajax/SysAjax.aspx", { key: "DeleteSysModuleByID", id: id, net4: Math.random() },
                        function (data) {
                            if (data == "1") {
                                layer.msg('删除成功', { icon: 1, time: 1000 }, function () {
                                    window.location.reload();
                                });
                            }
                            else {
                                layer.msg('删除失败', { icon: 2 });
                            }
                        }
                    );
                });
            });
        }

    </script>
</asp:Content>
