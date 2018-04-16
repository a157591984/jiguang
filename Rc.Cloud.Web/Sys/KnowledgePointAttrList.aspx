<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgePointAttrList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.KnowledgePointAttrList1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/mtree-2.0/mtree.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />

    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/mtree-2.0/mtree.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();
            $("#btnAdd").hide();
            $("#btnAdd").click(function () {
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: '增加',
                        fix: false,
                        area: ["450px", "450px"],
                        content: "KnowledgePointAttrEdit.aspx?S_KnowledgePointBasic_Id=<%=S_KnowledgePointBasic_Id%>"
                    })
                });
            })
        })
        var loadData = function () {
            var dto = {
                S_KnowledgePointBasic_Id: "<%=S_KnowledgePointBasic_Id%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("KnowledgePointAttrList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.Rcount == 2) {
                    $("#btnAdd").hide();
                }
                else {
                    $("#btnAdd").show();
                }
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);

                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");

                }
            }, function () { });
        }
        //编辑
        function UpdateData(S_KnowledgePointAttrExtend_Id) {

            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑',
                    fix: false,
                    area: ["450px", "450px"],
                    content: "KnowledgePointAttrEdit.aspx?S_KnowledgePointAttrExtend_Id=" + S_KnowledgePointAttrExtend_Id + "&S_KnowledgePointBasic_Id=<%=S_KnowledgePointBasic_Id%>"
                })
            });
        }

        //删除
        function DeleteData(S_KnowledgePointAttrExtend_Id) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("KnowledgePointAttrList.aspx/DeleteData", "{S_KnowledgePointAttrExtend_Id:'" + S_KnowledgePointAttrExtend_Id + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }

    </script>
</head>
<body>
    <div class="pa">
        <div class="panel">
            <div class="panel-heading">
                <div class="panel-title">
                    <input type="button" id="btnAdd" class="btn btn-primary" value="新增扩展属性" />
                </div>
            </div>
            <div class="panel-body">
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr class="tr_title">
                            <th>扩展属性名称</th>
                            <th>扩展属性值</th>
                            <th>添加时间</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tb1"></tbody>
                </table>
                <textarea id="template1" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.S_KnowledgePointAttrName}</td>
                                <td>{$T.record.S_KnowledgePointAttrValue}</td>
                                <td>{$T.record.CreateTime}</td>
                                <td class="opera">      
                                    <a href="javascript:;" onclick="UpdateData('{$T.record.S_KnowledgePointAttrExtend_Id}')">修改</a>
                                    <a href="javascript:;" onclick="DeleteData('{$T.record.S_KnowledgePointAttrExtend_Id}')">删除</a>
                                </td>
                            </tr>
                        {#/for}
                    </textarea>
            </div>
        </div>
    </div>
</body>
</html>
