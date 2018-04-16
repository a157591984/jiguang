<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SxxmbEmpower.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SxxmbEmpower" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="名称"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit" />
                </div>
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>年级学期</th>
                            <th>学科</th>
                            <th>教材版本</th>
                            <th>名称</th>
                            <th>日期</th>
                            <th>授权</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.GradeTerm}</td>
                    <td>{$T.record.Subject}</td>
                     <td>{$T.record.Resource_Version}</td>
                    <td>{$T.record.docName}</td>
                    <td>{$T.record.CreateTime}</td>
                    <td class="opera">
                        <a href="javascript:;" onclick="showPop('{$T.record.docId}','{$T.record.docName}')" >授权</a>
                    </td>
                </tr>
                {#/for}
                </textarea>
                <hr>
                <div class="page"></div>
            </div>
        </div>
    </div>



    <div class="clear"></div>
    <div style="width: 100%">
        <%--       <div class="left_tree">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>--%>

        <div class="" id="userDocumentContent">
        </div>
    </div>


    <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pop" runat="server" clientidmode="Static" style="width: 1000px; height: 550px;">
        <div class="div_ShowDailg_Title" id="div_Pop_Title" runat="server" clientidmode="Static">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left" runat="server" clientidmode="Static">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="CloseDialog();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv">
        </div>
        <div id="divIfram" class="divIfram">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        //授权
        function showPop(bid, bname) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '双向细目表【' + bname + '】授权',
                    area:['850px','90%'],
                    content: 'SelectTeacher.aspx?bid=' + bid
                });
            })
        }

        function ShowSubDocument(str, strDoctumentID, strDoctumentName) {
            //ShowUpload(str, strDoctumentID)
            catalogID = strDoctumentID;
            tp = "0";
            loadData();
        }
        var loadData = function () {
            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var dto = {
                DocName: docName,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("SxxmbEmpower.aspx/GetTwo_WayChecklist", JSON.stringify(dto), function (data) {
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
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        function OperateReAuth(attrEnum, reId, obj) {
            var dto = {
                AttrEnum: attrEnum,
                ReId: reId,
                AttrValue: $(obj).val(),
                x: Math.random()
            }
            $.ajaxWebService("CloudResEmpower.aspx/OperateResourceAuth", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    //loadData();
                    SubmitDataSuccess("操作成功", "", 1);
                }
            }, function () { }, false);
        }


        $(function () {
            pageIndex = 1;
            catalogID = "";
            docName = "";
            tp = "1";

            loadData();

            $("#btnSearch").click(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });

            $("#ddlGradeTerm").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlResource_Version").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlResource_Type").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
        });
    </script>
</asp:Content>
