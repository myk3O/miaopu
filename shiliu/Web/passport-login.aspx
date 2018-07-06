<%@ Page Language="C#" AutoEventWireup="true" CodeFile="passport-login.aspx.cs" Inherits="Web_passport_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>登录</title>
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
    <hd:header ID="Header1" runat="server" />
    <form runat="server">
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span>首页</span> > <span class="now">登录</span>
            </div>
        </div>
        <script>

            window.addEvent("domready", function () {

                $$(".MemberMenuList span")[0].setStyle("border-top", "none");

                $$(".MemberMenuList").each(function (item, index) {

                    item.addEvents({

                        mouseenter: function () {
                            this.getElement('span').addClass('hover');
                            $$(".MemberMenuList ul")[index].setStyle("background", "#f2f2f2");
                        },
                        mouseleave: function () {
                            this.getElement('span').removeClass('hover');
                            $$(".MemberMenuList ul")[index].setStyle("background", "#fff");
                        }
                    });

                });

                $$(".memberlist > tbody > tr").each(function (item, index) {

                    if (index > 0 && index % 2 == 0) { item.setStyle("background", "#f7f7f7"); }

                });

            });

        </script>
        <script type="text/javascript" src="statics/script/formplus.js"></script>
        <div class="MemberLogin">
            <div class="MemberLoginL">
                <h4>
                    欢迎登录</h4>
                <ul>
                    <li>
                        <input name="" type="text" id="fname" placeholder="手机号码" runat="server" />
                    </li>
                    <li>
                        <input name="" type="password" id="fpwd" placeholder="密码" runat="server" />
                    </li>
                </ul>
                <h5>
                    <a href="passport-lost.aspx">忘记密码 ?</a></h5>
                <h6>
                    <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" />
                    <%--          <input name="" type="button" value="登录" />--%>
                </h6>
            </div>
            <div class="MemberLoginR">
                <p>
                    如果你还不是会员，</p>
                请 <a href="passport-signup.aspx">注册新会员</a></div>
        </div>
        <div style="display: none;">
            <div class="memberlist-tip">
                <div class="tip">
                    <div class="tip-title">
                    </div>
                    <div class="tip-text">
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
