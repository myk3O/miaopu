<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Main_Left" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            //导航切换
            $(".menuson li").click(function () {
                $(".menuson li.active").removeClass("active")
                $(this).addClass("active");
            });

            $('.title').click(function () {
                var $ul = $(this).next('ul');
                $('dd').find('ul').slideUp();
                if ($ul.is(':visible')) {
                    $(this).next('ul').slideUp();
                } else {
                    $(this).next('ul').slideDown();
                }
            });
        })
    </script>
</head>
<body style="background: #f0f9fd;">
    <div class="lefttop">
        <span></span>信息管理</div>
    <dl class="leftmenu">
   <%--     <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>我的信息</div>
            <ul class="menuson">
                <li><cite></cite><a href="../SetConfig.aspx" target="rightFrame">账户信息</a><i></i></li>
            </ul>
        </dd>--%>
        <%=urlLeft%>
        <%--  <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>网站基本设置</div>
            <ul class="menuson">
                <li><cite></cite><a href="../SetConfig.aspx" target="rightFrame">网站信息设置</a><i></i></li>
             
        <li><cite></cite><a href="../ImgConfig/ImgMain.aspx" target="rightFrame">首页图片设置</a><i></i></li>
        
        <li><cite></cite><a href="../CustomerConfig.aspx" target="rightFrame">QQ客服</a><i></i></li>
        </ul> </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>企业信息管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../Info/InfoMain.aspx" target="rightFrame">企业信息列表</a><i></i></li>
                <li><cite></cite><a href="../Info/InfoEdit.aspx" target="rightFrame">企业信息添加</a><i></i></li>
                <li><cite></cite><a href="../Info/InfoClass.aspx" target="rightFrame">企业信息分类</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>活动资讯管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../News/NewsMain.aspx" target="rightFrame">活动资讯列表</a><i></i></li>
                <li><cite></cite><a href="../News/NewsEdit.aspx" target="rightFrame">活动资讯添加</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>营销活动管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../Activity/ActiveMage.aspx" target="rightFrame">营销活动列表</a><i></i></li>
                <li><cite></cite><a href="../Activity/ActiveEdit.aspx" target="rightFrame">营销活动添加</a><i></i></li>
                <li><cite></cite><a href="../Activity/ActiveClass.aspx" target="rightFrame">营销活动分类</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>代理商管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../DaiLi/HomeMakMain.aspx" target="rightFrame">代理商列表</a><i></i></li>
                <li><cite></cite><a href="../DaiLi/HomeMakEdit.aspx" target="rightFrame">代理商添加</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>订单管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../Order/OrderMain.aspx" target="rightFrame">订单列表</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>产品管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../Pruduct/ServceMain.aspx" target="rightFrame">产品列表</a><i></i></li>
                <li><cite></cite><a href="../Pruduct/ServceEdit.aspx" target="rightFrame">添加产品</a><i></i></li>
                <li><cite></cite><a href="../Pruduct/ServceClass.aspx" target="rightFrame">产品分类</a><i></i></li>
                <li><cite></cite><a href="../Pruduct/ServceClass3.aspx" target="rightFrame">酒的颜色</a><i></i></li>
                <li><cite></cite><a href="../Pruduct/ServceClass2.aspx" target="rightFrame">酒内糖份</a><i></i></li>
                <li><cite></cite><a href="../Pruduct/ServceClass1.aspx" target="rightFrame">含不含二氧化碳</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>代理商产品管理
            </div>
            <ul class="menuson">
                <li><cite></cite><a href="../Pruduct/ServceMain_A.aspx" target="rightFrame">产品列表</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>用户信息管理</div>
            <ul class="menuson">
                <li><cite></cite><a href="../Members/Member_Main.aspx" target="rightFrame">会员信息列表</a><i></i></li>
            </ul>
        </dd>
        <dd>
            <div class="title">
                <span>
                    <img src="../../images/leftico02.png" /></span>管理员管理</div>
            <ul class="menuson">
                <li><cite></cite><a href="../AdminManag/AdminMain.aspx" target="rightFrame">管理员列表</a><i></i></li>
                <li><cite></cite><a href="../AdminManag/AdminEdit.aspx" target="rightFrame">管理员添加</a><i></i></li>
            </ul>
        </dd>
        --%>
    </dl>
</body>
</html>
