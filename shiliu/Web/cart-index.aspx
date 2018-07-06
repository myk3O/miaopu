<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cart-index.aspx.cs" Inherits="Web_cart_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>查看购物车</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta name="generator" content="ShopEx 4.8.5" />
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
    <hd:header runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">购物车</span>
            </div>
        </div>
        <script type="text/javascript">

            //根据Id，删除购物车商品
            function del_cart(nID) {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "../Wap/Handler.ashx",
                    dataType: "json",
                    data: { "Method": "DelCart",
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
            function drop_cart_item(rec_id, input, p_id) {
                del_cart(rec_id);
            }
            function change_quantity(rec_id, input, p_id) { //改变

                if (Number($(input).attr('changed')) < 1) {//如果库存小于1
                    alert('产品已售完');
                    return false;
                }
                if (isNaN(input.value) || input.value < 1) {
                    alert('请输入大于0的数字');
                    //$(input).attr('changed', _v);
                    $(input).val($(input).attr('changed'));
                    return false;
                }
                if (input.value > Number($(input).attr('changed'))) {
                    $(input).val($(input).attr('changed'));
                    return false;
                }
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "../Wap/Handler.ashx",
                    dataType: "json",
                    data: { "Method": "UpdateCart",
                        "nID": rec_id,
                        "ProCount": input.value
                    },
                    success: function (Cart) {
                        if (Cart) {
                            //window.location.reload();
                            var price = $('#price' + rec_id).text();
                            $('#item' + rec_id + '_subtotal').text('￥' + input.value * $('#price' + rec_id).text() + '.00');
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });

            }
            function decrease_quantity(rec_id) {//减少
                var item = $('#input_item_' + rec_id);
                var orig = Number(item.val()); //文本框中的值
                if (orig > 1) {
                    item.val(orig - 1);
                    // item.attr('changed', orig);
                    item.keyup();
                    var price = $('#price' + rec_id).text();
                    // $('#item' + rec_id + '_subtotal').text((orig - 1) * $('#price' + rec_id).text());
                }
            }
            function add_quantity(rec_id) { //增加
                var item = $('#input_item_' + rec_id);

                var orig = Number(item.val());
                //item.attr('changed', orig);
                item.val(orig + 1);
                item.keyup();
                var price = $('#price' + rec_id).text();
                //$('#item' + rec_id + '_subtotal').text((orig + 1) * $('#price' + rec_id).text());
            }


            function buys() {
                var loginUser = $('#LoginUserID').val();
                if (loginUser == null || loginUser == "") {
                    window.location.href = "passport-login.aspx";
                    return false;
                }
                window.location.href = "cart-checkout.aspx?uid=" + loginUser + "&cart=right"; //从购物车过去的

                //                if (isNaN(quantity)) {
                //                    alert("请输入数字!");
                //                    return;
                //                }

                //                if (parseInt(quantity) < 1) {
                //                    alert("购买数量不能小于1");
                //                    return;
                //                }
            }
        </script>
        <form runat="server">        
        <input value="" id="LoginUserID" type="hidden" runat="server" /><%--session  id--%>
        <div class="CartWrap" id="cart-index">
            <div class="CartNav clearfix">
                <div class="floatLeft">
                    <img src="statics/cartnav-step1.gif" alt="购物流程--查看购物车" /></div>
                <div class="floatRight">
                    <img src="statics/cartnav-cart.gif" /></div>
            </div>
            <div class="section" id="cartitems" runat="server">
                <div class="FormWrap" id="cartItems">
                    <h3>
                        已放入购物车的商品:</h3>
                    请在此确认你要购买的商品<br>
                    （此为默认内容，具体内容可以在后台"页面管理-提示信息管理"中修改）
                    <br>
                    <div id="goodsbody" class="division">
                        <table width="100%" cellpadding="3" cellspacing="0" class="liststyle">
                            <col class="span-2 " />
                            <col class="span-auto" />
                            <col class="span-2" />
                            <col class="span-2" />
                            <col class="span-2" />
                            <col class="span-2" />
                            <col class="span-2 ColColorOrange" />
                            <col class="span-2" />
                            <thead>
                                <tr>
                                    <th>
                                        图片
                                    </th>
                                    <th>
                                        商品名称
                                    </th>
                                    <th>
                                        销售价格
                                    </th>
                                    <th>
                                        优惠价格
                                    </th>
                                    <th>
                                        数量
                                    </th>
                                    <th>
                                        小计
                                    </th>
                                    <th>
                                        删除
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=cartStr%>
                                <%--<tr>
                                    <td>
                                        <div class='cart-product-img' style='width: 50px; height: 50px;'>
                                            <a href='product-117.html' target='_blank'>
                                                <img src='statics/loading.gif' /></a>
                                        </div>
                                    </td>
                                    <td style='text-align: left'>
                                        <a href='product-117.html' target='_blank'>红酒</a>
                                    </td>
                                    <td class=" mktprice1" style='width: 50px; '>
                                        ￥998.00
                                    </td>
                                    <td style='width: 50px; '>
                                        ￥998.00
                                    </td>
                                    <td>
                                        <div class="Numinput">
                                            <input type="text" class="_x_ipt textcenter" value="1" onchange="Cart.ItemNumUpdate(this,this.value,event);"
                                                size="3" name="cartNum[g][113-366-na]" />
                                            <span class="numadjust increase"></span><span class="numadjust decrease"></span>
                                        </div>
                                    </td>
                                    <td class="itemTotal fontcolorRed">
                                        ￥998.00
                                    </td>
                                    <td>
                                        <span class="lnk quiet fontcolorRed" onclick='Cart.removeItem(this,event);'>
                                            <img src="statics/transparent.gif" alt="删除" style="width: 13px; height: 13px; background-image: url(statics/bundle.gif);
                                                background-repeat: no-repeat; background-position: 0 -27px;" /></span>
                                    </td>
                                </tr>--%>
                            </tbody>
                        </table>
                    </div>
                    <div class="floatRight" style='width: 45%'>
                        <div id="cartTotal">
                            <div class="division total-item" id='cart-total-item' style='display: none'>
                                <table width="100%" cellpadding="3" cellspacing="0" class="liststyle_data">
                                    <col class="span-auto"></col>
                                    <col class="span-2"></col>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            <script>
                                if ($$('#cart-total-item tr').length) {
                                    $('cart-total-item').setStyle('display', '');
                                }
                            </script>
                            <table width="100%" cellpadding="3" cellspacing="0" class="liststyle_data">
                                <col class="span-auto"></col>
                                <col class="span-2"></col>
                                <%--          <tbody>
                                    <tr>
                                        <th align='right'>
                                            <span style="font-size: 14px; color: #FFF;">商品总额：</span>
                                        </th>
                                        <td>
                                            <span class="totalprice price1" style="padding-right: 15px;">￥<%=Price%>.00</span>
                                        </td>
                                    </tr>
                                </tbody>--%>
                            </table>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="CartBtn clearfix" style="margin-bottom: 5px;">
                    <div class="span-auto">
                        <a href="index.aspx" class="actbtn btn-return">&laquo;继续购物</a></div>
                    <%--     <div class="span-auto">
                        <a id="clearCart" class="actbtn btn-clearcat">
                            清空购物车</a></div>--%>
                    <div class="span-auto floatRight">
                        <input class="actbtn btn-next" type="button" onclick="buys();" value="下单结算&raquo;" />
                    </div>
                </div>
            </div>
            <div id="cartnonemsg" runat="server">
                <br />
                <br />
                <div class='success'>
                    购物车里啥都没有</div>
                <h3>
                    现在您可以:</h3>
                <ul class='list'>
                    <li><a href='index.aspx'>返回网站首页</a></li>
                    <li><a href='javascript:opener=null;window.close();'>关闭此页面</a></li>
                </ul>
            </div>
        </div>
        </form>
    </div>
    <ft:footer runat="server" />
</body>
</html>
