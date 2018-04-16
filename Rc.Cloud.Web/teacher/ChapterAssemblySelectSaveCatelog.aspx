<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChapterAssemblySelectSaveCatelog.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ChapterAssemblySelectSaveCatelog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>目录</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../plugin/mtree-2.0/mtree.js"></script>
    <script src="../js/function.js"></script>
    <script src="../js/json2.js"></script>
    <script>
        $(function () {
            $("#btnSave").click(function () {
                var reId = $(".mtree div.active").data('id');
                if (reId == "" || reId == undefined) {
                    layer.msg('请选择文件夹', { icon: 2, time: 2000 });
                }
                else {
                    ConfirmTestpaper(reId);
                }
            })
            $('.mtree-hook').mtree({
                html: true,
                opera: true,
                onClick: function (obj) {
                    //console.log(obj);
                },
                onAdd: function (obj, name) {
                    //console.log(name);
                },
                onDel: function (obj, data) {
                    var dto = {
                        id: data[0],
                        x: Math.random()
                    };
                    $.ajaxWebService("ChapterAssemblySelectSaveCatelog.aspx/DelFolder", JSON.stringify(dto), function (data) {
                        if (data.d == "2") {
                            layer.msg("存在子级目录，删除失败", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                        }
                        else if (data.d == "3") {
                            layer.msg("目录中存在资源文件，删除失败", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                        }
                        else if (data.d == "0") {
                            layer.msg("删除失败", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                        }
                    }, function () {
                        layer.msg("操作失败", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                    });
                },
                onDone: function (obj, name, id, pid) {
                    var dto = {
                        userId: "<%=userId%>",
                        name: name,
                        id: id,
                        pid: pid,
                        x: Math.random()
                    };
                    $.ajaxWebService("ChapterAssemblySelectSaveCatelog.aspx/ModifyFolder", JSON.stringify(dto), function (data) {
                        if (data.d == "2") {
                            layer.msg("文件夹名称已存在", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                        }
                        else if (data.d == "1") {
                            layer.msg("操作失败", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                        }
                        else {
                            $(obj).data("id", data.d);
                        }
                    }, function () {
                        layer.msg("操作失败", { icon: 2, time: 2000 }, function () { window.location.reload(); });
                    });
                }
            });
        })
        // 确认组卷
        var ConfirmTestpaper = function (ResourceFolder_Id) {
            $("#btnSave").hide();
            //var index = layer.load();
            var dto = {
                key: "combination_testpaperchapter_confirm",
                Identifier_Id: "<%=Identifier_Id%>",
                ReName: "<%=ReName%>",
                Title: "<%=Title%>",
                ResourceFolder_Id: ResourceFolder_Id,
                CreateUser: "<%=userId%>",
                ComplexityTexts: "<%=ComplexityTexts%>",
                RtrfType: "<%=RtrfType%>",
                x: Math.random()
            }
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                //layer.close(index);
                if (data == "ok") {
                    layer.msg("组卷成功", { icon: 1, time: 2000 }, function () {
                        //parent.layer.close(index);
                        parent.window.location.href = "ChapterTestPaper.aspx?ugid=<%=ugid%>";
                        window.close();
                    });
                }
                else {
                    layer.msg("组卷失败", { icon: 2, time: 2000 }, function () {
                        $("#btnSave").hide();
                    });
                }
            }, function () {
                //layer.close(index);
                layer.msg("组卷失败err", { icon: 2, time: 2000 }, function () {
                    $("#btnSave").hide();
                });
            });

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="select_catelog_panel">
            <div class="panel_body">
                <div class="mtree mtree-hook">
                    <asp:Literal runat="server" ID="ltlRF"></asp:Literal>
                </div>
            </div>
            <div class="panel_footer clearfix">
                <%--<input type="text" name="name" value="" placeholder="文件名称" class="form-control" />--%>
                <button type="button" id="btnSave" class="btn btn-primary">确定</button>
            </div>
        </div>
    </form>
</body>
</html>
