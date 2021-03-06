﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member-receiver.aspx.cs"
    Inherits="Web_member_receiver" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>收货地址</title>
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
    <script src="../Wap/Js/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="../Wap/Js/jquery_002.js"></script>
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">收货地址</span>
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
        <script type="text/javascript">

            //根据Id，删除购物车商品
            function del_addr(nID) {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "../Wap/Handler.ashx",
                    dataType: "json",
                    data: { "Method": "DelAddr",
                        "nID": nID
                    },
                    success: function (Cart) {
                        if (Cart) {
                            window.location.reload();
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }

            //删除商品
            function drop_addr_item(rec_id) {
                del_addr(rec_id);
            }
       
      
        </script>
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
                                        <li><a href="member-security.aspx" target="_self">修改密码</a></li>
                                        <li class="current"><a href="member-receiver.aspx" target="_self">收货地址</a></li>
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
                        <div class="title">
                            收货地址</div>
                        <span><a href="member-addReceiver.aspx" class="lnk">
                            <img src="statics/btn-addaddress.gif" /></a></span>
                        <table class="memberlist" width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th>
                                    收货人
                                </th>
                                <th>
                                    地址
                                </th>
                                <th>
                                    电话
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                            <tbody>
                                <%=addresslist%>
                                <%--         <tr>
                                    <td>
                                        <a href="#">gahgag</a>
                                    </td>
                                    <td class="textwrap" style="text-align: left;">
                                        河北省唐山市路北区gag
                                    </td>
                                    <td>
                                        15845632541
                                    </td>
                                    <td>
                                        <a href="#">修改</a>&nbsp;&nbsp;<a href="#">删除</a>
                                    </td>
                                </tr>--%>
                            </tbody>
                        </table>
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
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
