<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ReCloudMgr.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.ReCloudMgr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script language="javascript" src="../Scripts/json2.js"></script>
    <script src="../Scripts/jq.pagination.js"></script>
    <script src="../Scripts/jquery-jtemplates.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/js001/common.js"></script>
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
                <td>教材版本：
                </td>
                <td>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>教案类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlLessonPlan_Type" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>年级学期：
                </td>
                <td>
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>学科：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubject" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btsSearch" Text="确定资源目录位置" runat="server" CssClass="btn" OnClick="btsSearch_Click" /></td>
            </tr>
        </table>
    </div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tr>
                <td>
                    <a href="#" id="aUploadYes" onclick="Edit()" class="btn display_none">上传资源</a>
                    <a href="##" id="aUploadNo" class="btn" title="只有最后一级目录才可上传文件。">当前下不可上传资源</a>
                    <input type="hidden" id="hidCatalog_ID" />
                    <a href="Catalog.aspx?t=<%=t %>&s=<%=s %>" id="a1" class="btn">管理目录</a>
                </td>
                <td>文件名：<asp:TextBox ID="txtKey" runat="server" CssClass="txt_Search myTextBox" ClientIDMode="Static"></asp:TextBox>
                    <input type="button" class="btn" id="btnSearch" value="查询" />

                </td>
            </tr>
        </table>
    </div>
    <div class="clear"></div>
    <div style="width: 100%">
        <div class="left_tree">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>

        <div class="right_main">
            <div class="" id="userDocumentContent">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                    <thead>
                        <tr class="tr_title">
                            <td>文件名</td>
                            <td>大小</td>
                            <td>修改日期</td>
                            <td>操作</td>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>


    <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pop" runat="server">
        <div class="div_ShowDailg_Title" id="div_Pop_Title">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="CloseDialog();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div_iframe" runat="server">
        </div>
    </div>
    <div class="div_ShowDailg" id="div_Pop2">
        <div class="div_ShowDailg_Title" id="div_Pop_Title2">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left2">
            </div>
            <div class="div_Close_Dailg" id="div4" title="关闭" onclick="CloseDialog();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div5" runat="server">
            <table cellpadding="0" cellspacing="0" border="0" class="table_content">
                <tr>
                    <td class="td_content_001">文件名</td>
                    <td class="td_content_002">
                        <asp:TextBox ID="txtDocumentName" CssClass="txt_area myTextBox" TextMode="MultiLine" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <span></span>
                        <input type="hidden" id="hidDocType" />
                        <input type="hidden" id="hidDocId" />
                    </td>
                </tr>
                <tr>
                    <td class="td_content_001"></td>
                    <td class="td_content_002">
                        <input type="button" id="btnSave" value="保存" class="btn MyButton" />
                        <input id="Button1" class="btn MyButton" type="button" value="取 消" onclick="CloseDialog();" /></td>
                </tr>
            </table>
        </div>
    </div>
    <textarea id="template_Res" style="display: none">
    {#foreach $T.list as record}
    <tr class="tr_con_001">
        <td>{$T.record.docName}</td>
        <td>{$T.record.docSize}</td>
        <td>{$T.record.docTime}</td>
        <td>
            <a href="../common/downLoadFile.aspx?iid={$T.record.docId}">下载</a>
            <a href="javascript:;" onclick="EditDocument('{$T.record.docId}','{$T.record.docName}');">修改</a>
            <a href="javascript:;" onclick="DeleteDocument('{$T.record.docId}');">删除</a></td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $("#div_Pop2").easydrag();
        $("#div_Pop2").setHandler("div_Pop_Title2");
        $("#<%=div_Pop.ClientID %>").easydrag();
        $("#<%=div_Pop.ClientID %>").setHandler("div_Pop_Title");
        function Edit() {
            var documentid;
            var iurl = "";
            documentid = $("#hidCatalog_ID").val();
            iurl = "?fid=" + documentid
                + "&R_Version=" + $("#ddlResource_Version").val()//教材版本
                + "&LP_Type=" + $("#ddlLessonPlan_Type").val()//教案类型
                + "&GT=" + $("#ddlGradeTerm").val()//年级学期
                + "&subj=" + $("#ddlSubject").val()//学科
                + "&R_Type=<%=strResource_Type%>"//资源类型（教案、作业、试卷）
                + "&R_Class=<%=strResource_Class%>"//资源类别（云资源，自有资源）
                + "&uid=<%=loginUser.SysUser_ID%>";

            $("#<%=div_Pop.ClientID %>").css({ width: "395px", hight: "350px" });
            $("#<%=div_iframe.ClientID %>").html("<iframe  height=\"350\"  frameborder='0' width='100%' style='margin: 0px' src='UploadFile.aspx" + iurl + "'></iframe>");
            jQuery("#div_ShowDailg_Title_left").html("上传资源");
            SetDialogPosition("<%=div_Pop.ClientID %>");
            ShowDocumentDivBG();
            jQuery("#<%=div_Pop.ClientID %>").show();
        }
        //操作提示，一般直接复制即可
        function Handel(sign, strMessage) {
            if (sign == "1") {
                //showTips('操作成功', '<%=strPageUrl %>', '1');
                //CloseDialog('<%=div_Pop.ClientID %>');
                showTips(strMessage, '', '1');
                setTimeout("CloseDialog('<%=div_Pop.ClientID %>');", 1000);

                loadData();
            }
            else {
                showTipsErr(strMessage, '3')
            }
        }
        function ShowUpload(str, strDoctumentID) {
            if (str == "1") {
                $("#aUploadYes").css({ "display": "inline-block" });
                $("#aUploadNo").hide();
                $("#hidCatalog_ID").val(strDoctumentID);
            }
            else {
                $("#aUploadYes").css({ "display": "none" });
                $("#aUploadNo").show();
                $("#hidCatalog_ID").val("");
            }
        }
        function ShowSubDocument(str, strDoctumentID, strDoctumentName) {
            ShowUpload(str, strDoctumentID)
            catalogID = strDoctumentID;
            tp = "0";
            loadData();
        }
        var loadData = function () {
            var strResource_Type = '<%=strResource_Type%>';
            var strResource_Class = '<%=strResource_Class%>';
            var dto = {
                ResourceFolder_Id: catalogID,
                DocName: docName,
                tp: tp,//0加载全部 1加载文件夹
                strResource_Type: strResource_Type,
                strResource_Class: strResource_Class,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("ReCloudMgr.aspx/GetCloudResource", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var DeleteDocument = function (_docId) {
            ShowDocumentDivBG();
            $.dialog.confirm('确定要删除吗？', function (topWin) {
                var dto = {
                    docId: _docId,
                    x: Math.random()
                }
                var flag = false;
                $.ajaxWebService("ReCloudMgr.aspx/DeleteCloudResource", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        showTips("删除成功", '', '1');
                        setTimeout("loadData();", 1000);
                        setTimeout("CloseDocumentDivBG()", 1000);
                    }
                    else {
                        showTipsErr("删除失败", '3')
                        setTimeout("CloseDocumentDivBG()", 3000);
                    }
                }, function () { }, false);
            }, function () { CloseDocumentDivBG(); });
        }

        var EditDocument = function (_docId, _docName) {
            $("#hidDocId").val(_docId);
            $("#txtDocumentName").val(_docName);
            $("#div_Pop2").css({ width: "600px", hight: "400px" });
            jQuery("#div_ShowDailg_Title_left2").html("修改文件名");
            SetDialogPosition("div_Pop2");
            ShowDocumentDivBG();
            jQuery("#div_Pop2").show();
        }


        $(function () {
            pageIndex = 1;
            catalogID = "";
            docName = "";
            tp = "1";

            loadData();

            $("#btnSearch").click(function () {
                docName = $.trim($("#txtKey").val());
                tp = "1";
                loadData();
            });
            $("#btnSave").click(function () {
                if ($.trim($("#txtDocumentName").val()) == "") {
                    $.dialog.alert("文件名不能为空");
                    return false;
                }
                var dto = {
                    docId: $("#hidDocId").val(),
                    docName: $("#txtDocumentName").val(),
                    x: Math.random()
                }
                $.ajaxWebService("ReCloudMgr.aspx/UpdateCloudResource", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        showTips("修改成功", '', '1');
                        setTimeout("loadData();CloseDocumentDivBG();", 1000);
                    }
                    else {
                        showTipsErr("修改失败", '3')
                        setTimeout("CloseDocumentDivBG();", 3000);
                    }

                    jQuery("#div_Pop2").hide();
                }, function () { }, false);
            });
            $("#txtDocumentName").bind({
                blur: function () {
                    if ($.trim($(this).val()).length > 30) {
                        $(this).val($.trim($(this).val()).slice(0, 30));
                    }
                },
                keyup: function () {
                    if ($.trim($(this).val()).length > 30) {
                        $(this).val($.trim($(this).val()).slice(0, 30));
                    }
                }
            });
        });

    </script>
</asp:Content>
