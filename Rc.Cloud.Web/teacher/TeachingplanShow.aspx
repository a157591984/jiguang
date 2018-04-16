<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeachingplanShow.aspx.cs" Inherits="Rc.Cloud.Web.teacher.TeachingplanShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/mtree-2.0/mtree.js"></script>
    <script src="../plugin/jquery.gaussian-blur.js"></script>
    <script src="../plugin/portamento/portamento-min.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/raty/lib/jquery.raty.min.js"></script>
    <script src="../js/function.js"></script>
    <script src="../Scripts/plug-in/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var _ResourceFolder_ID = "<%=ResourceFolder_ID%>";
            var _Type = "<%=Type%>";
            //$(".buy_btn").click(function () {
            //    if (_Type == "1") {
            //        window.location.href = "/student/Payment.aspx?orderType=2&rid=" + _ResourceFolder_ID;
            //    }
            //    else {
            //        window.location.href = "/teacher/Payment.aspx?orderType=1&rid=" + _ResourceFolder_ID;
            //    }
            //});
            //星级评分插件
            $('[data-name="star"]').raty({
                path: "../plugin/raty/lib/img",
                readOnly: true,
                hints: ['1分', '2分', '3分', '4分', '5分'],
                score: function () {
                    return $(this).data('score');
                }
            });
            //关闭页面
            $('.close-page-hook').on('click', function (e) {
                e.preventDefault;
                e.stopPropagation;
                window.close();
            });
            //背景高斯模糊
            $('.blur-hook').gaussianBlur({
                deviation: 20,
                imageClass: 'blur-item-hook'
            });
            //目录
            $('.mtree-hook').mtree({
                activeClass: '',
                display: 2,
                onClick: function (obj) { },
                onLoad: function (obj) { },
            });
            //预览
            $('.view-hook').on('click', function (e) {
                var url = $(this).closest('.mtree-link-hook').data('url');
                if (url) window.open(url);
                e.preventDefault();
                e.stopPropagation();
            })
        })
    </script>
    <title>教材详情</title>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="goods_detail_panel">
            <div class="panel_heading blur-hook">
                <div class="mask blur-hook">
                    <asp:Literal runat="server" ID="ltlMaskImg"></asp:Literal>
                </div>
                <div class="container">
                    <div class="panel_control">
                        <a href="javascript:;" class="close-page-hook"><i class="material-icons">&#xE5CD;</i></a>
                    </div>
                    <div class="panel_info clearfix">
                        <div class="left">
                            <asp:Literal ID="ltlImg" runat="server"></asp:Literal>
                        </div>
                        <div class="right">
                            <h4 class="title">
                                <asp:Literal runat="server" ID="ltlBookName"></asp:Literal></h4>
                            <%--<div class="desc"></div>--%>
                            <asp:Button ID="btnBuy" CssClass="buy_btn" runat="server" Text="立即购买" OnClick="btnBuy_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel_body">
                <div class="container pb">
                    <div class="page_title">简介</div>
                    <div class="intro">
                        <asp:Literal ID="ltlBookBif" runat="server"></asp:Literal>
                    </div>
                    <div class="page_title">目录</div>
                    <div class="catalog">
                        <div class="mtree mtree-hook">
                            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="page_title">评价</div>
                    <div class="comment">
                        <ul class="media-list">
                            <asp:Repeater runat="server" ID="rptcourse_comment">
                                <ItemTemplate>
                                    <li class="media">
                                        <%--<div class="media-left">
                                            <%#Eval("Commenter") %>
                                        </div>--%>
                                        <div class="media-body">
                                            <div class="media-heading clearfix">
                                                <span class="pull-left pr"><b><%#Eval("Commenter") %></b></span>
                                                <div class="pull-left" data-name="star" data-score="<%#Eval("comment_evaluate") %>"></div>
                                            </div>
                                            <p><%#Eval("comment_content") %></p>
                                            <p class="text-muted"><%# Rc.Cloud.Web.Common.pfunction.ToShortDate(Eval("create_time").ToString()) %></p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <li <%#rptcourse_comment.Items.Count==0?"":"style='display:none'" %> class="media">暂无评论</li>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
