<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order-detail.aspx.cs" Inherits="Web_order_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>订单详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="generator" content="ShopEx 4.8.5">
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript">
        var Shop = { "set": { "path": "\/", "buytarget": "3", "dragcart": null, "refer_timeout": 15 }, "url": {} };
    </script>
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script src="../Wap/Js/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="../Wap/Js/jquery_002.js"></script>
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置：<span><a href="member-orders.aspx" alt="" title="">我的订单</a></span> > 订单详情</a></span>
            </div>
        </div>
        <form>
        <style>
            .time-item strong
            {
                background: #C71C60;
                color: #fff;
                line-height: 49px;
                font-size: 36px;
                font-family: Arial;
                padding: 0 10px;
                margin-right: 10px;
                border-radius: 5px;
                box-shadow: 1px 1px 3px rgba(0,0,0,0.2);
            }
            #day_show
            {
                float: left;
                line-height: 49px;
                color: #c71c60;
                font-size: 32px;
                margin: 0 10px;
                font-family: Arial,Helvetica,sans-serif;
            }
            .item-title .unit
            {
                background: none;
                line-height: 49px;
                font-size: 24px;
                padding: 0 10px;
                float: left;
            }
        </style>
        <script type="text/javascript">
            (function ($) {
                $.getUrlParam = function (name) {
                    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                    var r = window.location.search.substr(1).match(reg);
                    if (r != null) return unescape(r[2]); return null;
                }
            })(jQuery);
            var intDiff; //倒计时总秒数量

            $(function () {
                var orderid = $.getUrlParam('id');
                if (orderid != null && orderid != "") {
                    $.ajax({
                        type: "post",
                        async: false,
                        cache: false,
                        url: "../Wap/Handler.ashx",
                        dataType: "json",
                        data: { "Method": "GetTotalSeconds",
                            "orderid": orderid
                        },
                        success: function (e) {
                            if (e != null) {
                                intDiff = parseInt(e);
                                timer(e);
                            }
                        },
                        error: function () {
                            alert('连接超时');
                        }
                    });
                }
            });

            function timer(intDiff) {
                window.setInterval(function () {
                    var day = 0,
                    hour = 0,
                    minute = 0,
                    second = 0; //时间默认值        
                    if (intDiff > 0) {
                        day = Math.floor(intDiff / (60 * 60 * 24));
                        hour = Math.floor(intDiff / (60 * 60)) - (day * 24);
                        minute = Math.floor(intDiff / 60) - (day * 24 * 60) - (hour * 60);
                        second = Math.floor(intDiff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
                    }
                    if (minute <= 9) minute = '0' + minute;
                    if (second <= 9) second = '0' + second;
                    $('#day_show').html(day + "天");
                    $('#hour_show').html('<s id="h"></s>' + hour + '时');
                    $('#minute_show').html('<s></s>' + minute + '分');
                    $('#second_show').html('<s></s>' + second + '秒');
                    intDiff--;
                    if (intDiff <= 0) {//系统默认确认收货
                        ordersh(false);
                    }
                }, 1000);
            }


            //确认收货
            function ordersh(mark) {
                var orderid = $.getUrlParam('id');
                if (orderid != null && orderid != "") {
                    if (mark) {
                        if (confirm('在您未收到货物的时候确认收货，可能会导致您钱财两空！')) {
                            $.ajax({
                                type: "post",
                                async: false,
                                cache: false,
                                url: "../Wap/Handler.ashx",
                                dataType: "json",
                                data: { "Method": "UpdateOrder",
                                    "orderid": orderid
                                },
                                success: function (e) {
                                    if (e) {
                                        //window.location.href = "member.aspx";
                                        window.location.reload();
                                    } else {
                                        alert('确认收货失败');
                                    }
                                },
                                error: function () {
                                    alert('连接超时');
                                }
                            });
                        }
                    } else {
                        $.ajax({
                            type: "post",
                            async: false,
                            cache: false,
                            url: "../Wap/Handler.ashx",
                            dataType: "json",
                            data: { "Method": "UpdateOrder",
                                "orderid": orderid
                            },
                            success: function (e) {
                                if (e) {
                                    //window.location.href = "member.aspx";
                                    window.location.reload();
                                } else {
                                    //alert('确认收货失败');
                                }
                            },
                            error: function () {
                                //alert('连接超时');
                            }
                        });
                    }
                } else {
                    alert('页面超时，系统将返回上一页');
                    window.location.href = "member-orders.aspx";
                }
            }
        </script>
        <div class="time-item" id="timeitem" runat="server">
            <span id="day_show">0天</span> <strong id="hour_show">0时</strong> <strong id="minute_show">
                0分</strong> <strong id="second_show">0秒</strong>后，系统将默认确认收货
        </div>
        <div class="CartWrap">
            <div class="FormWrap " style="background: #F5F4EC; border: 1px solid #E5DDC7;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="liststyle data">
                    <col class="span-auto ColColorBlue"></col>
                    <col class="span-5 ColColorGray"></col>
                    <col class="span-4 ColColorGray "></col>
                    <col class="span-5 ColColorGray "></col>
                    <%=orderheader %>
                    <%--         <tr>
                        <td>
                            <h4>
                                订单编号：20150409094983</h4>
                        </td>
                        <td>
                            下单日期：2015-04-09 09:56
                        </td>
                        <td>
                            支付方式：货到付款
                        </td>
                        <td>
                            状态：未付款 [未发货]
                        </td>
                    </tr>--%>
                </table>
            </div>
            <div class="FormWrap " style="background: #F5F4EC; border: 1px solid #E5DDC7;">
                <h4>
                    购买的商品</h4>
                <div class="division">
                    <table width="100%" cellpadding="3" cellspacing="0" class="liststyle">
                        <col class='span-auto'></col>
                        <col class="span-auto"></col>
                        <col class="span-2"></col>
                        <col class="span-3"></col>
                        <col class="span-2 ColColorOrange"></col>
                        <thead>
                            <tr>
                                <th>
                                    图片
                                </th>
                                <th>
                                    商品名称
                                </th>
                                <th>
                                    价格
                                </th>
                                <th>
                                    数量
                                </th>
                                <th>
                                    小计
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=Product %>
                            <%--     <tr>
                                <td>
                                    <img src='statics/loading.gif' style='width: 50px; height: 50px;' />
                                </td>
                                <td>
                                    <a href="product-131.html" target="_blank">浴•莲安•斋浴礼盒</a>
                                </td>
                                <td>
                                    ￥399.00
                                </td>
                                <td>
                                    2
                                </td>
                                <td>
                                    ￥798.00
                                </td>
                            </tr>--%>
                        </tbody>
                    </table>
                </div>
                <h4>
                    收货人信息</h4>
                <div class="division">
                    <table width="100%" cellspacing="0" cellpadding="0" class="liststyle data">
                        <%=orderbody %>
                        <%-- <tr>
                            <th>
                                收货人姓名:
                            </th>
                            <td>
                                伟仔
                            </td>
                            <th>
                                联系电话:
                            </th>
                            <td>
                                912719465
                            </td>
                        </tr>
                        <tr>
                            <th>
                                物流公司:
                            </th>
                            <td>
                                顺丰
                            </td>
                            <th>
                                物流单号:
                            </th>
                            <td>
                                200020200020200020200020
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                配送地区:
                            </th>
                            <td colspan="3" valign="top">
                                上海-上海市-宝山区
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                收货人地址:
                            </th>
                            <td colspan="3" valign="top">
                                上海市宝山区蕴川路
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                订单附言:
                            </th>
                            <td colspan="3" valign="top">
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </div>
        </div>
        <div class="FormWrap ">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="liststyle data">
                <tr>
                    <td>
                        <strong style="color: Black">订单总金额:</strong> <span class="hueorange fontcolorRed font20px">
                            ￥<%=AllPrice%>.00</span>
                    </td>
                    <td style="text-align: left">
                        <%--       <asp:Button runat="server" ID="btnPay" CssClass="actbtn btn-pay" Text="立刻支付" OnClientClick="BtnPay()" />--%>
                        <input type="button" id="qrShouhuo" value="确认收货" title="确认收货" onclick="ordersh(true);"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
