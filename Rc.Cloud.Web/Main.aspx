<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="Rc.Cloud.Web.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    #main-content{ margin:20px; height:500px;}
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div id="main-content">
     <h1>
        你好，<span>Test</span></h1>
        <div class="btnToolBox">
            <ul>
                <li><a href="javascript:void(0)">处方数据导入</a></li>
            </ul>
        </div>
        <div class="btnToolBox">
            <ul>
                <li><a href="javascript:void(0)">门诊处方抽样</a></li>
            </ul>
        </div>
        <div class="btnToolBox">
            <ul>
                <li><a href="javascript:void(0)">急诊处方抽样</a></li>
            </ul>
        </div>

        <div class="btnToolBox">
            <ul>
                <li><a href="javascript:void(0)">门诊处方点评</a></li>
            </ul>
        </div>

        <div class="btnToolBox">
            <ul>
                <li><a href="javascript:void(0)">急诊处方点评</a></li>
            </ul>
        </div>

        <div class="btnToolBox">
            <ul>
                <li><a href="javascript:void(0)">查看消息</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#div_right_001").click();
        });
    </script>
</asp:Content>
