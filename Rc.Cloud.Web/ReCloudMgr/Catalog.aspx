<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/js001/common.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"><%--<a href="javascript:history.back(-1);">返回上一级</a>--%></div>
        <%=siteMap%>
    </div>
    <div class="clearDiv"></div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tr>
                <td>教材版本：</td>
                <td>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>教案类型：</td>
                <td>
                    <asp:DropDownList ID="ddlLessonPlan_Type" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>年级学期：</td>
                <td>
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>学科：</td>
                <td>
                    <asp:DropDownList ID="ddlSubject" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btsSearch" Text="确定资源目录位置" runat="server" CssClass="btn" OnClick="btsSearch_Click" /></td>
            </tr>
        </table>
    </div>
    <div class="clearDiv"></div>
    <div>
        <div class="left_tree">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>
        <div class="right_main">
            <table cellpadding="0" cellspacing="0" border="0" class="table_content" id="table_content" runat="server" clientidmode="Static">
                <tr>
                    <td class="td_content_001">目录名称：</td>
                    <td class="td_content_002">

                        <asp:HiddenField ID="hidResourceFolder_Id" ClientIDMode="Static" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidResourceFolder_ParentId" Value="0" ClientIDMode="Static" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidResourceFolder_Level" Value="0" ClientIDMode="Static" runat="server"></asp:HiddenField>
                        <asp:TextBox ID="txtResourceFolder_Name" CssClass="user_txt basic_info_tab_txt" ClientIDMode="Static" runat="server"></asp:TextBox>

                    </td>
                </tr>

                <tr>
                    <td class="td_content_001">是否为最后一级：</td>
                    <td class="td_content_002">
                        <asp:DropDownList ID="ddlResourceFolder_isLast" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                            <asp:ListItem Text="不是" Value="0"></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td_content_001"></td>
                    <td class="td_content_002">
                        <asp:Button ID="btnNewCreate" CssClass="btn" runat="server" Text="新增目录" OnClick="btnNewCreate_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnUpdate" CssClass="btn" runat="server"
                            Text="修改" OnClick="btnUpdate_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnSave" CssClass="btn" runat="server"
                            Text="添加到同级" OnClick="btnSave_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnSub" CssClass="btn" runat="server"
                            Text="添加到下级" OnClick="btnSub_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnDelete" CssClass="btn" runat="server"
                            Text="删除" OnClick="btnDelete_Click" ClientIDMode="Static" />
                        <%--<input type="button" value="测试" id="btnOk"  />--%>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" border="0" style="margin-top: 50px;" class="div_nodata_001" id="tableConfirm" runat="server" clientidmode="Static">
                <tr>
                    <td>确定资源目录位置后，方可维护资源目录</td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">

        function ShowSubDocument(ResourceFolder_Id, ResourceFolder_ParentId, ResourceFolder_Name, ResourceFolder_Level, Resource_Version, ResourceFolder_isLast) {
            $("#hidResourceFolder_Id").val(ResourceFolder_Id);
            $("#hidResourceFolder_ParentId").val(ResourceFolder_ParentId);
            $("#txtResourceFolder_Name").val(ResourceFolder_Name);
            $("#hidResourceFolder_Level").val(ResourceFolder_Level);
            $("#ddlResource_Version").val(Resource_Version);
            if (ResourceFolder_isLast == "1") {
                $("#ddlResourceFolder_isLast").val("1");
            }
            else {
                $("#ddlResourceFolder_isLast").val("0");
            }
            $("#btnSub").show();
            if (ResourceFolder_isLast == "1") {//最后一级
                $("#btnSub").hide();
            }

        }

        var Handel = function (sign, strMessage) {

            if (sign == "1") {
                showTips(strMessage, '', '2');
            }
            else {
                showTipsErr(strMessage, '3')
            }

        };
        $(function () {

            $("#btnNewCreate").click(function () {
                $("#hidResourceFolder_Level").val("0");//
                $("#hidResourceFolder_ParentId").val("0");
                if ($.trim($("#txtResourceFolder_Name").val()) == "") {
                    $.dialog.alert("目录名称不能为空");
                    layer.ready(function () { layer.msg('请填写表说明', { icon: 4 }); });
                    return false;
                }
            });
            $("#btnUpdate").click(function () {
                if ($("#hidResourceFolder_Id").val() == "") {
                    layer.ready(function () { layer.msg('请先选择目录', { icon: 4 }); });
                    return false;
                }
                if ($.trim($("#txtResourceFolder_Name").val()) == "") {
                    layer.ready(function () { layer.msg('目录名称不能为空', { icon: 4 }); });
                    return false;
                }
            });
            $("#btnSave").click(function () {
                if ($("#hidResourceFolder_Id").val() == "") {
                    layer.ready(function () { layer.msg('请先选择目录', { icon: 4 }); });
                    return false;
                }
                if ($.trim($("#txtResourceFolder_Name").val()) == "") {
                    layer.ready(function () { layer.msg('目录名称不能为空', { icon: 4 }); });
                    return false;
                }
                alert($("#hidResourceFolder_Level").val());
            });
            $("#btnSub").click(function () {
                var level = parseInt($("#hidResourceFolder_Level").val())
                //添加到下一级 级别+1
                if (parseInt($("#hidResourceFolder_Level").val()) != "NaN") {
                    $("#hidResourceFolder_Level").val(parseInt($("#hidResourceFolder_Level").val()) + 1);
                }

                else {
                    $("#hidResourceFolder_Level").val("0");
                }
                //父级ID为当前ID
                $("#hidResourceFolder_ParentId").val($("#hidResourceFolder_Id").val());
                $("#hidResourceFolder_Level").val(ResourceFolder_Level);
                if ($("#hidResourceFolder_Id").val() == "") {
                    layer.ready(function () { layer.msg('请先选择目录', { icon: 4 }); });
                    return false;
                }
                if ($.trim($("#txtResourceFolder_Name").val()) == "") {
                    layer.ready(function () { layer.msg('目录名称不能为空', { icon: 4 }); });
                    return false;
                }
            });
            $("#btnDelete").click(function () {
                if ($("#hidResourceFolder_Id").val() == "") {
                    layer.ready(function () { layer.msg('请先选择目录', { icon: 4 }); });
                    return false;
                }
            });
        });

        $(function () {
            $("#btnOk").click(function () {
                $.ajax({
                    type: "Post",
                    url: "Catalog.aspx/GetTreeHtml",
                    //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字   
                    //data: "{'str':'我是','str2':'XXX'}",
                    contentType: "application/json",
                    dataType: "json",
                    success: function (data) {
                        //返回的数据用data.d获取内容   
                        $("#divTree").html(data.d)
                    },
                    error: function (err) {
                        $("#divTree").html("加载错误：" + err)
                    }
                });

                //禁用按钮的提交   
                return false;
            });
        });
        $(function () {
            $(document).on('click', 'div[data-type="tree"] a', function () {
                $('div[data-type="tree"] a').each(function () {
                    $(this).closest('div').removeClass('active');
                });
                $(this).closest('div').addClass('active');
            });
        });
    </script>
</asp:Content>
