<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Right.aspx.cs" Inherits="Main_Right" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="http://www.js-css.cn/jscode/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.6.2.min.js"></script>
    <script src="../../js/jquery.date_input.pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/highcharts.js"></script>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jsapi.js"></script>
    <script type="text/javascript" src="../../js/format+zh_CN,default,corechart.I.js"></script>
    <script type="text/javascript" src="../../js/jquery.gvChart-1.0.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.ba-resize.min.js"></script>
    <script type="text/javascript">
        //gvChartInit();
        //jQuery(document).ready(function(){

        //jQuery('#myTable5').gvChart({
        //		chartType: 'PieChart',
        //		gvSettings: {
        //			vAxis: {title: 'No of players'},
        //			hAxis: {title: 'Month'},
        //			width: 650,
        //			height: 250
        //			}
        //	});
        //});
    </script>
</head>
<body>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">首页</a></li>
            <li><a href="#">工作台</a></li>
        </ul>
    </div>
    <div class="mainbox">
        <div class="mainleft">
            <div class="leftinfo">
                <div class="listtitle">
                    <a href="../HighChart.aspx" class="more1">更多</a>数据统计
                </div>
                <div runat="server" id="BrokenlineChartShow" style="margin-top: 20px; font-size: 12px;">
                    <div runat="server" id="noData" visible="false" style="text-align: center; color: Red;">
                        <asp:Literal ID="Literal14" runat="server" Text="当前">
                        </asp:Literal>！
                    </div>
                    <div runat="server" id="DivChart" style="width: 100%;">
                        <div id="Linecontainer" style="min-width: 740px; height: 290px;">
                        </div>
                    </div>
                    <script type="text/javascript">
                        var chart;
                        $(document).ready(function () {
                                <%=result%>
                            });
                    </script>
                </div>
            </div>
            <!--leftinfo end-->
            <div class="leftinfos">
                <div class="infoleft">
                    <br />
                    <br />
                    <br />
                    <div class="listtitle">
                        <a href="../Members/MemberMain.aspx" class="more1">更多</a>交易订单
                    </div>
                    <ul class="newlist">
                        <%=strQr %>
                        <%--           <li><a href="#">深圳代理商</a><b>10-09</b></li>
                        <li><a href="#">浙江代理商</a><b>10-09</b></li>--%>
                    </ul>
                </div>
                <div class="inforight">
                    <br />
                    <br />
                    <br />
                    <div class="listtitle">
                        <a href="#" class="more1"></a>快速导航
                    </div>
                    <ul class="tooli">
                        <li><span>
                            <img src="../../images/d01.png" /></span><p>
                                <a href="../Order/MoneyOrder.aspx">提现申请</a>
                            </p>
                        </li>
                        <li><span>
                            <img src="../../images/d02.png" /></span><p>
                                <a href="../Pruduct/ProductClassMain.aspx">视频管理</a>
                            </p>
                        </li>
                        <%--     <li><span>
                            <img src="../../images/d03.png" /></span><p>
                                <a href="#">记事本</a></p>
                        </li>
                        <li><span>
                            <img src="../../images/d04.png" /></span><p>
                                <a href="#">任务日历</a></p>
                        </li>
                        <li><span>
                            <img src="../../images/d05.png" /></span><p>
                                <a href="#">图片管理</a></p>
                        </li>--%>
                        <li><span>
                            <img src="../../images/d06.png" /></span><p>
                                <a href="../Order/OrderMain.aspx">订单查询</a>
                            </p>
                        </li>
                        <li><span>
                            <img src="../../images/d07.png" /></span><p>
                                <a href="../Members/Member_Main.aspx">我的会员</a>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <div class="dflist">
                <div class="listtitle">
                    <a href="../Order/OrderMain.aspx" class="more1">更多</a>新增客户
                </div>
                <ul class="newlist">
                    <%=strDaiLiList %>

                    <%--                   <li><a href="#">经典红酒</a><b>2015-1-09 15:32:22</b></li>
                    <li><a href="#">红葡萄酒</a><b>2015-1-04 7:32:22</b></li>--%>
                </ul>
            </div>
            <div class="dflist1">
                <div class="listtitle">
                    <a href="#" class="more1">更多</a>信息统计
                </div>
                <ul class="newlist">
                    <%=strTongJ %>
                    <%--                    <li><i><a>总代理：</a></i>252个</li>
                    <li><i><a>总会员：</a></i>25333个</li>
                    <li><i><a>总成交量：</a></i>5546</li>
                    <li><i><a>月成交量：</a></i>15</li>--%>
                </ul>
            </div>
        </div>
        <!--mainright end-->
    </div>
</body>
<script type="text/javascript">
    setWidth();
    $(window).resize(function () {
        setWidth();
    });
    function setWidth() {
        var width = ($('.leftinfos').width() - 12) / 2;
        $('.infoleft,.inforight').width(width);
    }
</script>
</html>
