<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="evaAnswer.aspx.cs" Inherits="Rc.Cloud.Web.teacher.evaAnswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--子导航-->
    <div class="sub_nav">
        <ul>
            <li class="userinfo">我是老师：laoshi1<asp:Literal runat="server" ID="ltlTrueName"></asp:Literal></li>
            <li><a href="evaPerformance.aspx">成绩分析</a></li>
            <li><a href="evaExamination.aspx">试卷分析</a></li>
            <li><a href="evaAnswer.aspx" class="active">答题分析</a></li>
            <li><a href="evaCommenting.aspx">试卷讲评</a></li>
            <li><a href="evaTracking.aspx">作业全记录跟踪</a></li>
            <li><a href="evaTranscript.aspx">成绩单</a></li>
        </ul>
    </div>

    <!--内容-->
    <div class="container">
        <div class="data_list wrap_data_list">
            <table>
                <thead>
                    <tr>
                        <td>题号</td>
                        <td>题型</td>
                        <td>测量目标</td>
                        <td>考查内容</td>
                        <td>难易度</td>
                        <td>步骤</td>
                        <td>分值</td>
                        <td>平均分</td>
                        <td>标准差</td>
                        <td>区分度</td>
                        <td>班的错误率</td>
                        <td>掌握程度</td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>1</td>
                        <td>A标记</td>
                        <td>现代汉语普通话常用字字音的标记</td>
                        <td>一般</td>
                        <td>1</td>
                        <td>3.00</td>
                        <td>3</td>
                        <td>1.3</td>
                        <td>0.5</td>
                        <td class="error_rate" data-content="<p>选A:5人</p><p>选B:2人</p><p>选C:6人</p><p>选D:1人</p><p>未选:0人</p>">20%</td>
                        <td>需努力</td>
                        <td class="tab_list_opera">
                            <a href="##" data-name="analysis">本题分析</a>
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>1</td>
                        <td>A标记</td>
                        <td>现代常用规范汉字的识记与正确书写</td>
                        <td>一般</td>
                        <td>1</td>
                        <td>3.00</td>
                        <td>3</td>
                        <td>1.3</td>
                        <td>0.5</td>
                        <td class="error_rate" data-content="<p>选A:5人</p><p>选B:2人</p><p>选C:6人</p><p>选D:1人</p><p>未选:0人</p>">20%</td>
                        <td>薄弱点</td>
                        <td class="tab_list_opera">
                            <a href="##" data-name="analysis">本题分析</a>
                        </td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>1</td>
                        <td>E表达应用</td>
                        <td>词语（包括熟语）的识记、理解和正确使用</td>
                        <td>一般</td>
                        <td>1</td>
                        <td>3.00</td>
                        <td>3</td>
                        <td>1.3</td>
                        <td>0.5</td>
                        <td class="error_rate" data-content="<p>选A:5人</p><p>选B:2人</p><p>选C:6人</p><p>选D:1人</p><p>未选:0人</p>">20%</td>
                        <td>一般</td>
                        <td class="tab_list_opera">
                            <a href="##" data-name="analysis">本题分析</a>
                        </td>
                    </tr>
                    <tr>
                        <td>4</td>
                        <td>1</td>
                        <td>E表达应用</td>
                        <td>病句的解析和修改</td>
                        <td>一般</td>
                        <td>1</td>
                        <td>3.00</td>
                        <td>3</td>
                        <td>1.3</td>
                        <td>0.5</td>
                        <td class="error_rate" data-content="<p>选A:5人</p><p>选B:2人</p><p>选C:6人</p><p>选D:1人</p><p>未选:0人</p>">20%</td>
                        <td>良好</td>
                        <td class="tab_list_opera">
                            <a href="##" data-name="analysis">本题分析</a>
                        </td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td>4</td>
                        <td>E表达应用</td>
                        <td>语言表达的连赏、得体</td>
                        <td>一般</td>
                        <td>1</td>
                        <td>6.00</td>
                        <td>3</td>
                        <td>1.3</td>
                        <td>0.5</td>
                        <td class="error_rate" data-content="<p>选A:5人</p><p>选B:2人</p><p>选C:6人</p><p>选D:1人</p><p>未选:0人</p>">20%</td>
                        <td>闪光点</td>
                        <td class="tab_list_opera">
                            <a href="##" data-name="analysis">本题分析</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script src="../Scripts/plug-in/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //导航默认状态
            $(".nav li:eq(4) a").addClass("active");

            //班的错误率
            $('.error_rate').live({
                mouseover: function () {
                    var content = $(this).attr('data-content');
                    layer.tips(content, this, {
                        tips: [4, '#000'],
                        time: 0
                    });
                },
                mouseout: function () {
                    layer.closeAll('tips');
                }
            });

            //试题分析
            $('a[data-name="analysis"]').live({
                click: function () {
                    var data = '<div style="font-size:14px; line-height:26px; padding:10px;">'
                    data += '<p>第②段加点词“展开”在文中的意思是</p>';
                    data += '<p>【参考答案】整幅完整展现。</p>';
                    data += '<p>【测量目标】理解词语在语言环境中的意义。</p>';
                    data += '<p>【知识内容】理解词句的含义及其内在的联系。</p>';
                    data += '<p>【试题分析】“展开”再字典中与词句相关的义项是“铺开、打开”，具体到文本的语境中，“展开”的意义有两个要素：一是全幅的、完全的，二是铺开、展开的一种状态。</p>';
                    data += '<p>【得分情况】第1题得分分布（%）</p>';
                    data += '<p>【难度】0.66</p>';
                    data += '<p>【区分度】0.22</p>'
                    data += '<p style="padding-top:15px; text-align:right;"><a href="##" class="create_btn">上一题</a>&nbsp;&nbsp;<a href="##" class="create_btn">下一题</a></p>';
                    data += '</div>';
                    layer.open({
                        type: 1,
                        title: '试题分析',
                        area: ['500px', '50%'],
                        content: data
                    });
                }
            });

        })
    </script>
</asp:Content>
