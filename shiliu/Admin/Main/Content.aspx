<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="Main_Content" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>

</head>


<body>

    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">首页</a></li>
        </ul>
    </div>

    <div class="mainindex">


        <div class="welinfo">
            <span>
                <img src="../../images/sun.png" alt="天气" /></span>
            <b><label id="lbltilte" runat="server"></label></b>
   <%-- <a href="#">帐号设置</a>--%>
        </div>

        <div class="welinfo">
            <span>
                <img src="../../images/time.png" alt="时间" /></span>
            <i><label id="lbltime" runat="server"></label></i> （不是您登录的？<a href="#">请点这里</a>）
        </div>

        <div class="xline"></div>

        <ul class="iconlist">

            <li>
                <img src="../../images/ico01.png" /><p><a href="#">管理设置</a></p>
            </li>
            <li>
                <img src="../../images/ico02.png" /><p><a href="#">发布文章</a></p>
            </li>
            <li>
                <img src="../../images/ico03.png" /><p><a href="#">数据统计</a></p>
            </li>
            <li>
                <img src="../../images/ico04.png" /><p><a href="#">文件上传</a></p>
            </li>
            <li>
                <img src="../../images/ico05.png" /><p><a href="#">目录管理</a></p>
            </li>
            <li>
                <img src="../../images/ico06.png" /><p><a href="#">查询</a></p>
            </li>

        </ul>

        <div class="ibox"><a>
            </div>

        <div class="xline"></div>
        <div class="box"></div>

        <div class="welinfo">
            <span>
                <img src="../../images/dp.png" alt="提醒" /></span>
            <b>网站后台管理系统使用指南</b>
        </div>
        <br />
        <ul>
            <li><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您可以快速进行新闻发布管理操作</span></li>
            <li><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您可以快速发布视频</span></li>
            <li><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您可以进行密码修改、账户设置等操作</span></li>
        </ul>
    </div>



</body>

</html>
