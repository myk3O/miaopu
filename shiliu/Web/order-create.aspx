<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order-create.aspx.cs" Inherits="Web_order_create" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>待支付订单</title>
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
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="css/loginDialog.css" />
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置：<span><a href="member-orders.aspx" alt="" title="">我的订单</a></span> <span>> 待支付订单
                </span>
            </div>
        </div>
        <div class='CartWrap'>
            <div class="CartNav clearfix">
                <div class="floatLeft">
                    <img src="statics/cartnav-step4.gif" alt="购物流程--确认订单填写购物信息" />
                </div>
                <div class="floatRight">
                    <img src="statics/cartnav-cart.gif" /></div>
            </div>
        </div>

        <form runat="server">
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
                    <td style="text-align: right">
                        <%--       <asp:Button runat="server" ID="btnPay" CssClass="actbtn btn-pay" Text="立刻支付" OnClientClick="BtnPay()" />--%>
                        <input type="button" id="btnPay" class='actbtn btn-pay' value="1" onclick="BtnPay()" />
                        <input type="button" class='actbtn xiao-pay' value="取消订单" title="取消订单" onclick="drop_addr_item()" />
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">

            var xhr; //XMLHttpRequest对象有abort()方法
            $(function () {
                //关闭
                $(".close_btn").hover(function () { $(this).css({ color: 'black' }) }, function () { $(this).css({ color: '#999' }) }).on('click', function () {
                    $("#LoginBox").fadeOut("fast");
                    $("#mask").css({ display: 'none' });
                    xhr.abort();
                });

            });
            function BtnPay() {
                if ($('#btnPay').attr("value") == 1) {
                    var orderid = $('#litQrcode').val();
                    $('#btnPay').attr("value", "2");

                    $("#qrDiv").html("<img src=QrCode.ashx?orderid=" + orderid + " /> ");
                    $("body").append("<div id='mask'></div>");
                    $("#mask").addClass("mask").fadeIn("slow");
                    $("#LoginBox").fadeIn("slow");

                    handleOrder();
                } else {
                    $("body").append("<div id='mask'></div>");
                    $("#mask").addClass("mask").fadeIn("slow");
                    $("#LoginBox").fadeIn("slow");
                }
            };
            //取消订单
            function drop_addr_item() {
                var orderid = $('#litQrcode').val();
                if (orderid != undefined && orderid != "") {
                    if (confirm('确认取消订单')) {
                        $.ajax({
                            type: "post",
                            async: false,
                            cache: false,
                            url: "../Wap/Handler.ashx",
                            dataType: "json",
                            data: { "Method": "DelOrder",
                                "nID": orderid
                            },
                            success: function (Member) {
                                if (Member) {
                                    window.location.href = "member-orders.aspx";
                                }
                            },
                            error: function () {
                                alert('连接超时');
                            }

                        });
                    }
                }
            }


            function handleOrder() {
                var orderid = $('#litQrcode').val();
                xhr = $.ajax({
                    timeout: 7200000,    //120分钟超时
                    type: "post",
                    url: "Handler.ashx",
                    dataType: "json",
                    cache: false,
                    data: { "Method": "GetOrderHandle",
                        "orderid": orderid
                    },
                    success: function (e) {
                        if (e) {
                            window.location.href = "order-detail.aspx?id=" + orderid;
                        }
                    }
                });
            }


            
        </script>
        <div id="img">
            <div id="LoginBox">
                <div class="row1">
                    微信扫码支付<a href="javascript:void(0)" title="关闭窗口" class="close_btn" id="closeBtn">×</a>
                </div>
                <div class="row" id="qrDiv">
                </div>
            </div>
            <input type="hidden" id="litQrcode" runat="server" />
            <br />
        </div>
        </form>
    </div>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
