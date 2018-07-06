<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="Web_about" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>关于马赛特</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="generator" content="ShopEx 4.8.5">
    <meta name="robots" content="noindex,noarchive,nofollow" />
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
</head>
<body>
    <hd:header runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span>首页</span> > <span>关于马赛特</span> > <span class="now"><%=_title%></span>
            </div>
        </div>
        <div class="MemberBox">            
            <%=dataSource%>
        </div>
    </div>
    <ft:footer runat="server" />
</body>
</html>
