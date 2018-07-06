<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Main_Top" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../js/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            //顶部导航切换
            $(".nav li a").click(function () {
                $(".nav li a.selected").removeClass("selected")
                $(this).addClass("selected");
            })
        })
    </script>
</head>
<body style="background: url(../../images/topbg.gif) repeat-x;">
    <div class="topleft">
        <a href="main.html" target="_parent">
            <img src="../../images/logo.png" title="系统首页" /></a>
    </div>
    <ul class="nav">
        <li><a href="Right.aspx" target="rightFrame" class="selected">
            <img src="../../images/083.png" style="width: 45px; height: 45px" title="工作台" /><h2>
                工作台</h2>
        </a></li>
        <%--<li><a href="imgtable.html" target="rightFrame"><img src="../../images/054.png" style="width:45px;height:45px" title="审核管理" /><h2>审核管理</h2></a></li>--%>
        <li><a href="../HighChart.aspx" target="rightFrame">
            <img src="../../images/i05.png" style="width: 45px; height: 45px" title="统计模块" /><h2>
                统计模块</h2>
        </a></li>
    </ul>
    <div class="topright">
        <ul>
            <li><a href="#" onclick="javascript:top.location.href='Main.aspx';">首页</a></li>
            <li><a href="#" onclick="javascript:parent.centerFrame.rightFrame.history.back();">后退</a></li>
            <li><a href="#" onclick="javascript:parent.centerFrame.rightFrame.history.forward();">
                前进</a></li>
            <li><a href="#" onclick="javascript:parent.centerFrame.rightFrame.location.reload();">
                刷新</a></li>
            <li><a href="#" onclick="javascript:top.location.href='../Login.aspx';">退出</a></li>
        </ul>
        <div class="user">
            <span>
                <%=adminName %></span>
        </div>
    </div>
</body>
</html>
