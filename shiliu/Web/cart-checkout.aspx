<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cart-checkout.aspx.cs" Inherits="Web_cart_checkout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>填写订单信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="generator" content="ShopEx 4.8.5">
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
<%--    <script src="../Wap/Js/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="../Wap/Js/jquery_002.js"></script>--%>
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">填写订单信息</span>
            </div>
        </div>
        <input value="" id="LoginUserID" type="hidden" runat="server" /><%--session  id--%>
        <div class="CartWrap" id="cart-index">
            <div class="CartNav clearfix">
                <div class="floatLeft">
                    <img src="statics/cartnav-step3.gif" alt="购物流程--查看购物车" /></div>
                <div class="floatRight">
                    <img src="statics/cartnav-cart.gif" /></div>
            </div>
            <div class="section" id="content">
                <form id="order_form">
                <h1 class="add_title">
                    <a class="enter" href="member-addReceiver.aspx">管理收货地址</a>
                </h1>
                <div class="order_address_list">
                    <h4 class="add_title">
                        收货人地址</h4>
                    <script>

                        (function ($) {
                            $.getUrlParam = function (name) {
                                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                                var r = window.location.search.substr(1).match(reg);
                                if (r != null) return unescape(r[2]); return null;
                            }
                        })(jQuery);

                        $(function () {

                            // var loginUser = window.localStorage.getItem("LoginUserId");
                            var loginUser = $.getUrlParam('uid');
                            if (loginUser == null || loginUser == "") {
                                window.location.href = "passport-login.aspx";
                            }
                            $.ajax({
                                type: "post",
                                async: false,
                                cache: false,
                                url: "../Wap/Handler.ashx",
                                dataType: "json",
                                data: { "Method": "GetOrderAddr",
                                    "userID": loginUser
                                },
                                success: function (address) {
                                    if (address != null && address != "") {
                                        $('#address_list').html(address);
                                    }
                                },
                                error: function () {
                                    alert('连接超时');
                                }

                            });
                            var cartFrom = $.getUrlParam('cart');
                            if (cartFrom != null && cartFrom == "right") {//从购物车跳转过来支付
                                $.ajax({
                                    type: "post",
                                    async: false,
                                    cache: false,
                                    url: "../Wap/Handler.ashx",
                                    dataType: "json",
                                    data: { "Method": "GetCartPrice",
                                        "userID": loginUser
                                    },
                                    success: function (p) {
                                        if (p != null && p != "") {
                                            var price = "￥" + p + ".00";
                                            $("#order_amount2").html(price); ;
                                        }
                                    },
                                    error: function () {
                                        alert('连接超时');
                                    }

                                });

                            } else {
                                var Pri = $.getUrlParam('Pri');
                                var quantity = $.getUrlParam('quantity');
                                if (Pri == null || Pri == "" || quantity == null || quantity == "") {//直接购买产品
                                    alert('链接失效');
                                    return false;
                                } else {

                                    var price = "￥" + Pri * quantity + ".00";
                                    $("#order_amount2").html(price);
                                }
                            }

                        });


                        //提交订单
                        function ordertjqr() {
                            var loginUser = $('#LoginUserID').val();
                            if (loginUser == null || loginUser == "") {
                                window.location.href = "passport-login.aspx";
                                return false;
                            }
                            //禁止按钮，点过一次不能再提交了
                            $('#ordertjqr').attr('disabled', "true");
                            var cartFrom = $.getUrlParam('cart');
                            if (cartFrom != null && cartFrom == "right") {//从购物车跳转过来支付
                                var redioAddr = $("input:radio[name='address_options']:checked").val();
                                $.ajax({
                                    type: "post",
                                    async: false,
                                    cache: false,
                                    url: "../Wap/Handler.ashx",
                                    dataType: "json",
                                    contentType: "application/x-www-form-urlencoded; charset=utf-8",
                                    data: { "Method": "MakeOrderCart",
                                        "userID": loginUser,
                                        "AddrID": redioAddr,
                                        "tContent": $("#postscript").val(),
                                        "AuntID": window.localStorage.getItem("agent"),
                                        "payWay": $("input[name='payment']:checked").attr('paytype')
                                    },
                                    success: function (e) {
                                        if (e) {
                                            //alert('功能暂未开放');
                                            //setTimeout("window.location.href = 'member.aspx';", 1000);
                                            window.location.href = "order-create.aspx?nID=" + e;
                                            //window.location.reload();
                                        } else {
                                            alert('生成失败');
                                        }
                                    },
                                    error: function () {
                                        alert('连接超时');
                                    }

                                });

                            } else {
                                var pid = $.getUrlParam('pid');
                                var quantity = $.getUrlParam('quantity');
                                var Pri = $.getUrlParam('Pri')
                                if (pid == null || pid == "" || quantity == null || quantity == "" || Pri == null || Pri == "") {//直接购买产品
                                    alert('链接失效');
                                    return false;
                                }
                                var redioAddr = $("input:radio[name='address_options']:checked").val();
                                $.ajax({
                                    type: "post",
                                    async: false,
                                    cache: false,
                                    url: "../Wap/Handler.ashx",
                                    dataType: "json",
                                    contentType: "application/x-www-form-urlencoded; charset=utf-8",
                                    data: { "Method": "MakeOrder",
                                        "userID": loginUser,
                                        "AddrID": redioAddr,
                                        "tContent": $("#postscript").val(),
                                        "ProIDquantity": pid + "," + quantity,
                                        "AllPrice": Pri * quantity,
                                        "AuntID": window.localStorage.getItem("agent"),
                                        "payWay": $("input[name='payment']:checked").attr('paytype')
                                    },
                                    success: function (e) {
                                        if (e) {
                                            //alert('功能暂未开放');
                                            //setTimeout("window.location.href = 'member.aspx';", 1000);
                                            window.location.href = "order-create.aspx?nID=" + e;
                                            //window.location.reload();
                                        } else {
                                            alert('生成失败');
                                        }
                                    },
                                    error: function () {
                                        alert('连接超时');
                                    }

                                });

                            }
                        }
                    </script>
                    <div id="address_list">
                        <%--    <ul class="receive_add address_item selected_address">
                <li class="radio">
                    <input checked="checked" name="address_options" value="1342" type="radio">
                </li>
                <li class="particular">重华路2015号201 (收货人：李四 手机：15816466515 邮编：445463)</li>
            </ul>
            <ul class="receive_add address_item selected_address">
                <li class="radio">
                    <input name="address_options" value="1342" type="radio">
                </li>
                <li class="particular">重华路2015号201 (收货人：李四 手机：15816466515 邮编：445463)</li>
            </ul>--%></div>
                </div>
                <div class="message_box">
                    <span class="add_title">给卖家的附言</span>
                    <div class="message">
                        <textarea id="postscript" name="delivery[memo]"></textarea>
                    </div>
                </div>
                <div class="order_delivery">
                    <h4 class="add_title">
                        支付方式</h4>
                    <ul class="receive_add address_item selected_address" style="display: none">
                        <li class="radio">
                            <input id="paytypes[52]" name="payment" type="radio" paytype="支付宝[即时到帐]" value="52"
                                moneyamount="" formatmoney="￥0.00">
                        </li>
                        <li class="particular">支付宝[即时到帐]</li>
                    </ul>
                    <ul class="receive_add address_item selected_address">
                        <li class="radio">
                            <input id="paytypes[44]" name="payment" type="radio" paytype="微信支付" value="44" moneyamount=""
                                checked="checked" formatmoney="￥0.00">
                        </li>
                        <li class="particular">微信支付</li>
                    </ul>
                    <ul class="receive_add address_item selected_address" style="display: none">
                        <li class="radio">
                            <input id="paytypes[33]" class="payment" name="payment" type="radio" paytype="线下支付"
                                value="33" moneyamount="0" formatmoney="￥0.00">
                        </li>
                        <li class="particular">线下支付</li>
                    </ul>
                </div>
                <div class="make_sure">
                    <p class="order_amount">
                        订单总价: <strong class="fontsize3" id="order_amount"><font id="order_amount2">￥3600.00</font></strong>
                        <font id="order_amount2">
                            <input class="btn1" onclick="$(this).parent('p').next().toggle();$('#coupon_sn').val('');"
                                style="display: none" type="button"></font>
                    </p>
                    <%--   <div>
                        <font id="order_amount2"><a onclick="ordertj();" class="btn enter">生成订单并前往支付</a> <a
                            href="javascript:history.go(-1)" class="back">返回</a> </font>
                    </div>--%>
                </div>
                <div class="CartBtn clearfix" style="margin-bottom: 5px;">
                    <div class="span-auto">
                        <a href="cart-index.aspx" class="actbtn btn-return-checkout">&laquo;</a></div>
                    <div class="span-auto floatRight">
                        <a onclick="ordertjqr();" id="ordertjqr" class="actbtn btn-confirm"></a>
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
