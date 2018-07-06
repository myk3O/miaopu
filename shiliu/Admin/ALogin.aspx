<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALogin.aspx.cs" Inherits="Admin_ALogin" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../js/jquery.js"></script>
    <script src="../js/cloud.js" type="text/javascript"></script>
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        $(function () {
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            $(window).resize(function () {
                $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            })
        });
    </script>
</head>

<body style="background-color: #1c77ac; background-image: url(images/light.png); background-repeat: no-repeat; background-position: center top; overflow: hidden;">
    <form id="Form1" runat="server">
        <div id="mainBody">
            <div id="cloud1" class="cloud"></div>
            <div id="cloud2" class="cloud"></div>
        </div>
        <div class="logintop">
            <span>欢迎登录网站后台管理系统</span>
            <ul>
                <li><a href="http://www.nowwin.cn" target="_blank">皓赢科技出品</a></li>
            </ul>
        </div>
        <div class="loginbody">
            <font class="systemlogo"></font>
            <div class="loginbox">
                <ul>
                    <li>
                        <input name="" id="txtname" runat="server" type="text" class="loginuser" value="admin" placeholder="登录名"  onclick="JavaScript: this.value = ''" /></li>
                    <li>
                        <input name="" id="txtpass" runat="server" type="password" class="loginpwd" value="密码" placeholder="密码" onclick="JavaScript: this.value = ''" /></li>
                    <li>
                        <asp:Button runat="server" ID="btnLogin" Text="登录" class="btn btn-xs btn-default" OnClick="btnLogin_Click" />
                        <label>
                            <input name="" id="ckb" runat="server" type="checkbox" value="" checked="checked" />记住密码</label></li>
                    <li>
                        <asp:Label ID="lbltxt" runat="server" Text=""></asp:Label>
                    </li>
                </ul>
            </div>
        </div>
        <div class="loginbm">版权所有  2015  马赛特</div>
    </form>
</body>

</html>

