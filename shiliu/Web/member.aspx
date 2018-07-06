<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member.aspx.cs" Inherits="Web_member" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>会员中心</title>
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
    <form runat="server">
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">会员中心</span>
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
        <div class="MemberBox">
            <div class="MemberCenter">
                <div class="MemberSidebar">
                    <div class="MemberMenu">
                        <div class="title">
                        </div>
                        <div class="body">
                            <ul>
                                <li class="MemberMenuList"><span>
                                    <div class="m_0" style="font-size: 14px;">
                                        交易记录</div>
                                </span>
                                    <ul>
                                        <li><a href="member-orders.aspx" target="_self"><b>我的订单</b></a></li>
                                    </ul>
                                </li>
                                <li class="MemberMenuList"><span>
                                    <div class="m_3" style="font-size: 14px;">
                                        个人设置</div>
                                </span>
                                    <ul>
                                        <%--                                       <li><a href="member-setting.aspx" target="_self">个人信息</a></li>--%>
                                        <li><a href="member-security.aspx" target="_self">修改密码</a></li>
                                        <li><a href="member-receiver.aspx" target="_self">收货地址</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <div class="foot">
                        </div>
                    </div>
                </div>
                <div class="MemberMain">
                    <div style="">
                        <div class="MemberMain-title">
                            <div class="title" style="float: left;">
                                您好，<%=UserName%>，欢迎进入用户中心</div>
                            <asp:Button runat="server" ID="btnPay" OnClick="btnPay_Click" Text="退出" CssClass="Tue" />
                            <div class="clear">
                            </div>
                        </div>
                        <div class="MemberMain-basicinfo">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <div class="info">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="left">
                                                    </td>
                                                    <td width="135" style="padding-left: 5px;">
                                                        您的订单交易总数量：
                                                    </td>
                                                    <td>
                                                        <span class="point">
                                                            <%=orderCount%></span>个
                                                    </td>
                                                    <td width="90">
                                                        <li><a class="lnk" href="member-orders.aspx">进入订单列表</a></li>
                                                    </td>
                                                    <td class="right">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <div class="title">
                            待付款订单</div>
                        <table class="memberlist" width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th>
                                    商品名称
                                </th>
                                <th>
                                    订单号
                                </th>
                                <th>
                                    下单日期
                                </th>
                                <th>
                                    总金额
                                </th>
                                <th>
                                    订单状态
                                </th>
                            </tr>
                            <%=orderList %>
                            <%--       <tr>
                                <td width="40%">
                                    <a class="intro" href="order-detail.aspx">红酒A(1)</a>
                                </td>
                                <td>
                                    <a href="order-detail.aspx">20140727123413</a>
                                </td>
                                <td>
                                    2014-07-27 12:41
                                </td>
                                <td>
                                    ￥69.00
                                </td>
                                <td>
                                    <span class="point"><a href="order-detail.aspx">等待付款</a> </span>
                                </td>
                            </tr>--%>
                        </table>
                        <div class="more">
                            <a class="lnk" href="member-orders.aspx">更多订单>></a></div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
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
    <ft:footer runat="server" />
</body>
</html>
