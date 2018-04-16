<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="noticeBoard.aspx.cs" Inherits="Rc.Cloud.Web.noticeBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .letter {
            overflow: hidden;
            background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAAGCAIAAACq3OKOAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA4ZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNS1jMDE0IDc5LjE1MTQ4MSwgMjAxMy8wMy8xMy0xMjowOToxNSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDoxNzNjYjQ4My0xNDY1LTQ1NzQtYjc2NS1iY2E3NDRmZTI3MjMiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6RUEwMzg2RjQ4RDNEMTFFNjk5QkRFOTRDRUQ5RERBQjUiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6RUEwMzg2RjM4RDNEMTFFNjk5QkRFOTRDRUQ5RERBQjUiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUgKE1hY2ludG9zaCkiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpiNDAxYTc3Zi03OTRmLTQ3NzEtOGRhOC1iNTk4ZmRlMTBjNTYiIHN0UmVmOmRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDoyYTA1MWMyMy1kMjcwLTExNzktYjA2NS1lNDVlZGE4OWFiMDMiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz62qq5JAAAAiElEQVR42mKcdvgLAwZg/P/f4eI6qbf30MT/MzIe1A9mYsAGdB+cwNQABFcVLJ8KK2LRI/32vs79o5jiz4SVLilaARnoenh+fLK6ugXoNjTxrxz8x7S9gW5D18Py74/dpY1sv3+gafjLxHJIz+8nKweEi6LH5OZewc8vMF11Rt3lHa8EnAsQYADH8y5ADqrQsAAAAABJRU5ErkJggg==) repeat-x left top;
        }

        .panel {
            margin-bottom: 54px;
        }

        .panel-footer {
            background: transparent;
            border: 0;
        }

        .close_notice {
            background: #fff;
            position: fixed;
            padding: 10px 10px 10px 0;
            bottom: 0;
            left: 0;
            right: 0;
        }
    </style>
    <script src="js/jquery.min-1.11.1.js"></script>
    <script src="plugin/layer/layer.js"></script>
    <script>
        $(function () {
            $('#close').on('click', function () {
                var index = parent.layer.getFrameIndex(window.name);
                parent.layer.close(index);
            })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="letter">
            <h2 class="text-center page-header text-primary">云平台升级公告</h2>
            <div class="panel">
                <div class="panel-body">
                    <asp:Literal ID="ltlContent" runat="server" ClientIDMode="Static"></asp:Literal>
                </div>
                <div class="panel-footer text-right">
                    <p><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></p>
                    <p>
                        <asp:Literal ID="ltlCreateTime" runat="server" ClientIDMode="Static"></asp:Literal>
                    </p>
                </div>
            </div>
            <div class="text-right close_notice">
                <button type="button" class="btn btn-link" id="close">我知道了</button>
            </div>
        </div>
    </form>
</body>
</html>
