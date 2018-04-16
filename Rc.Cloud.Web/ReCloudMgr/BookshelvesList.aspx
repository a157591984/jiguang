<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="BookshelvesList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.BookshelvesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
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
                    <asp:DropDownList ID="ddlResource_Type" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="书本名称"></asp:TextBox>
                    <asp:DropDownList ID="ddlBookShelvesState" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="已上架" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未上架" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <input type="button" class="btn btn-sm btn-default" id="btnSearch" value="查询" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>缩略图</th>
                            <th>年级学期</th>
                            <th>学科</th>
                            <th>教材版本</th>
                            <th>文档类型</th>
                            <th width="30%">名称</th>
                            <th>价格</th>
                            <th>状态</th>
                            <th>上架日期</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>
                        {#if $T.record.edit==1}
                        <image src="{$T.record.BookImg_Url}" height="80" data-content="<image src='{$T.record.BookImg_Url}' height='120' />"/>
                        {#/if}
                        </td>
                        <td>{$T.record.GradeTerm}</td>
                        <td>{$T.record.Subject}</td>
                        <td>{$T.record.Resource_Version}</td>
                        <td>{$T.record.Resource_Type}</td>
                        <td>{$T.record.docName}</td>       
                        <td>{$T.record.BookPrice}</td>
                        <td>{$T.record.BookShelvesState}</td>
                        <td>{$T.record.PutDownTime}</td>
                        <td class="opera">
                        {#if $T.record.edit==0}
                        <a href="javascript:;" onclick="showPop('{$T.record.docId}','{$T.record.docName}','{$T.record.edit}')" >上架</a>           
                        {#elseif  $T.record.edit==1}
                        <a href="javascript:;" onclick="EditBookShelvesImg('{$T.record.docId}')" >更换封皮</a>
                        <a href="javascript:;" onclick="SetBookShelvesState('{$T.record.docId}')" >下架</a>
                        {#/if}
                        <a title=""  href="javascript:;" onclick="ShowRelist('ReList.aspx?resourceFolderId={$T.record.docId}&resTitle={$T.record.GradeTerm}--{$T.record.Subject}--{$T.record.Resource_Version}--{$T.record.Resource_Type}--{$T.record.docName}')" >资源列表</a>
                        </td>
                    </tr>
                {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        function showPop(bid, bname, edit) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: (edit == 1 ? '下架' : '上架'),
                    area: ['650px', '600px'],
                    content: "BookshelvesEdit.aspx?bookid=" + bid + "&bookname=" + bname + "&edit=" + edit
                });
            });

        }
        function SetBookShelvesState(bid) {
            layer.ready(function () {
                layer.confirm('下架？', { icon: 3 }, function () {
                    var dto = {
                        id: bid,
                        x: Math.random()
                    };
                    $.ajaxWebService("BookshelvesList.aspx/SetBookShelvesState", JSON.stringify(dto), function (data) {
                        var json = $.parseJSON(data.d);
                        if (json.err == "null") {
                            layer.msg('下架成功', { icon: 1, time: 1000 }, function () {
                                window.location.reload();
                            })
                        }
                        else {
                            layer.msg('下架失败', { icon: 2 });
                        }
                    }, function () { }, false);
                });
            });
        }
        function EditBookShelvesImg(rid) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '更换封皮',
                    area: ['400px', '300px'],
                    content: "BookshelvesImgEdit.aspx?rid=" + rid
                });
            });
        }
        var ShowRelist = function (url) {
            window.location.href = url + "&backurl=" + backurl;
        }
        function SetWH(num, obj) {
            if (num == 1) {
                $("#div_Pop").css({ width: "250px", height: "300px", left: "100px" });
                $("#divIfram").css({ padding: "10px" });
                $("#divIfram").html("<image src='" + $(obj).attr("src") + "' width='100%'/>");
                $("#div_Pop").show();
                SetDialogPosition("div_Pop", 10);
            }
            else {
                CloseDialog();
            }
        }
        //操作提示，一般直接复制即可
        function Handel(sign, strMessage) {
            layer.ready(function () {
                if (sign == "1") {
                    layer.msg('操作成功', { icon: 1, time: 1000 }, function () {
                        layer.closeAll();
                        loadData();
                    })
                }
                else {
                    layer.msg(strMessage, { icon: 4 })
                }
            });
        }
        function ShowSubDocument(str, strDoctumentID, strDoctumentName) {
            //ShowUpload(str, strDoctumentID)
            catalogID = strDoctumentID;
            tp = "0";
            loadData();
        }
        var loadData = function () {
            setBasicUrl();

            var strResource_Type = $("#ddlResource_Type").val();//
            var strResource_Class = '<%=strResource_Class%>';//

            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var strBookShelvesState = $("#ddlBookShelvesState").find("option:selected").val();//
            var docName = $.trim($("#txtKey").val());
            var dto = {
                DocName: docName,
                strResource_Type: strResource_Type,
                strResource_Class: strResource_Class,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                strBookShelvesState: strBookShelvesState,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("BookshelvesList.aspx/GetCloudBooks", JSON.stringify(dto), function (data) {
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

                $('#tbRes img').popover({
                    trigger: 'hover',
                    html: true
                });
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var setBasicUrl = function () {
            basicUrl = "BookshelvesList.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex + "&DocName=" + escape($.trim($("#txtKey").val()))
                + "&strResource_Type=" + $("#ddlResource_Type").val()
                + "&strGradeTerm=" + $("#ddlGradeTerm").val()
                + "&strSubject=" + $("#ddlSubject").val()
                + "&strResource_Version=" + $("#ddlResource_Version").val()
                + "&strBookShelvesState=" + $("#ddlBookShelvesState").find("option:selected").val());
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            $("#txtKey").val(unescape(getUrlVar("DocName")));
            $("#ddlResource_Type").val(getUrlVar("strResource_Type"));
            $("#ddlGradeTerm").val(getUrlVar("strGradeTerm"));
            $("#ddlSubject").val(getUrlVar("strSubject"));
            $("#ddlResource_Version").val(getUrlVar("strResource_Version"));
            $("#ddlBookShelvesState").val(getUrlVar("strBookShelvesState"));
        }
        $(function () {
            pageIndex = 1;
            catalogID = "";
            tp = "1";
            b = new Base64();
            loadParaFromLink();
            loadData();
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlGradeTerm").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlResource_Version").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlResource_Type").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlBookShelvesState").change(function () {
                pageIndex = 1;
                loadData();
            });
        });
    </script>
</asp:Content>
