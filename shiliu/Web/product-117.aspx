<%@ Page Language="C#" AutoEventWireup="true" CodeFile="product-117.aspx.cs" Inherits="Web_product_117" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>产品详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="红酒">
    <meta name="description" content="红酒A现价69.000;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;">
    <meta name="generator" content="ShopEx 4.8.5">
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript">
        var Shop = { "set": { "path": "\/", "buytarget": "3", "dragcart": null, "refer_timeout": 15 }, "url": { "addcart": "http:\/\/www.马赛特624.com\/cart-ajaxadd.html", "shipping": "http:\/\/www.马赛特624.com\/cart-shipping.html", "payment": "http:\/\/www.马赛特624.com\/cart-payment.html", "total": "http:\/\/www.马赛特624.com\/cart-total.html", "viewcart": "http:\/\/www.马赛特624.com\/cart-view.html", "ordertotal": "http:\/\/www.马赛特624.com\/cart-total.html", "applycoupon": "http:\/\/www.马赛特624.com\/cart-applycoupon.html", "diff": "http:\/\/www.马赛特624.com\/product-diff.html"} };
    </script>
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
    <script charset="utf-8" type="text/javascript" src="../Wap/Js/jquery_002.js"></script>
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <form id="form1" runat="server">
    <input value="" id="price" type="hidden" runat="server" /><%--产品单价--%>
    <input value="" id="goodId" type="hidden" runat="server" />
    <input value="" id="productId" type="hidden" runat="server" /><%--产品id--%>
    <input value="" name="kuCun" id="kuCun" type="hidden" runat="server" /><%--库存--%>
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span><a href="index.aspx" alt="" title="">首页</a></span> <span>&raquo;</span>
                <%--<span><a href="gallery-100-grid.aspx" alt="" title="">E系列</a></span> <span>&raquo;</span>--%>
                <%=_title%>
            </div>
        </div>
        <script type="text/javascript">

            //添加购物车商品
            function add_cart(nID, a) {

                var loginUser = $('#LoginUserID').val();
                if (loginUser == null || loginUser == "") {
                    //MyChangePage("Person/Individual.html");
                    alert('请先登录');
                    window.location.href = "passport-login.aspx";
                    return false;
                }

                if (Number($(a).attr('kucun')) < 1) {//如果库存小于1
                    alert('产品已售完');
                    return false;
                }

                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "../Wap/Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "InsertCart",
                        "UserID": loginUser,
                        "ProID": nID,
                        "ProCount": 1
                    },
                    success: function (data) {
                        if (data) {
                            //alert("添加成功");
                            window.location.reload();
                        } else {
                            alert("添加失败");
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }
                });
            }

            //添加商品
            function drop_cart_item(rec_id, a) {
                add_cart(rec_id, a);
            }                                 


        </script>
        <input value="" id="LoginUserID" type="hidden" runat="server" /><%--session  id--%>
        <div class="goods_details">
            <script type="text/javascript">
                $(function () {

                });
                function change_quantity(value) {
                    var kucun = $('#kuCun');
                }
            </script>
            <script type="text/javascript">
                function change_quantity(value) {
                    var item = $('#quantity');
                    var kucun = $('#kuCun');
                    if (Number(kucun.val()) < 1) {
                        alert('产品已售完');
                        return false;
                    }

                    var ss = value.value;
                    var orig = Number(item.val());
                    if (isNaN(orig)) {
                        item.val(1);
                        item.attr('changed', orig);
                        item.keyup();
                    }
                    if (orig < 1) {
                        item.val(1);
                        item.attr('changed', orig);
                        item.keyup();
                    }
                    if (orig > kucun.val()) {
                        item.val(kucun.val());
                        item.attr('changed', orig);
                        item.keyup();
                    }

                }

                function decrease_quantity(rec_id) {
                    var kucun = $("#kuCun");
                    if (Number(kucun.val()) < 1) {
                        alert('产品已售完');
                        return false;
                    }

                    var item = $('#quantity');
                    var orig = Number(item.val());
                    if (orig > 1) {
                        item.val(orig - 1);
                        item.attr('changed', orig);
                        item.keyup();
                    }
                }
                function add_quantity(rec_id) {

                    var kucun = $("input[name='kuCun']");
                    if (Number(kucun.val()) < 1) {
                        alert('产品已售完');
                        return false;
                    }

                    var item = $('#quantity');
                    var orig = Number(item.val());
                    item.attr('changed', orig);
                    item.val(orig + 1);
                    item.keyup();
                }
            
            </script>
            <script type="text/javascript">
                function buys() {
                    var loginUser = $('#LoginUserID').val();
                    if (loginUser == null || loginUser == "") {
                        //MyChangePage("Person/Individual.html");
                        alert('请先登录');
                        window.location.href = "passport-login.aspx";
                        return false;
                    } else {
                        var kucun = $('#kuCun');
                        if (Number(kucun.val()) < 1) {
                            alert('产品已售完');
                            return false;
                        }
                        var quantity = $("#quantity").val();
                        var pid = $("#productId").val();
                        if (quantity == '') {
                            alert('请输入购买数量');
                            return;
                        }

                        window.location.href = "cart-checkout.aspx?uid=" + loginUser + "&pid=" + pid + "&quantity=" + quantity + "&Pri=" + $("#price").val();
                    }
                }



                function add_cart() { //，购买数量  ,商品ID   

                    var kucun = $('#kuCun');
                    if (Number(kucun.val()) < 1) {
                        alert('产品已售完');
                        return false;
                    }
                    var loginUser = $('#LoginUserID').val();
                    if (loginUser == null || loginUser == "") {
                        //MyChangePage("Person/Individual.html");
                        alert('请先登录');
                        window.location.href = "passport-login.aspx";
                        return false;
                    } else {

                        var quantity = $("#quantity").val();
                        var pid = $("#productId").val();
                        if (quantity == '') {
                            alert('请输入购买数量');
                            return;
                        }
                        $.ajax({
                            type: "post",
                            async: false,
                            cache: false,
                            url: "../Wap/Handler.ashx",
                            dataType: "json",
                            data: {
                                "Method": "InsertCart",
                                "UserID": loginUser,
                                "ProID": pid,
                                "ProCount": quantity
                            },
                            success: function (data) {
                                if (data) {
                                    alert("添加成功");
                                } else {
                                    alert("添加失败");
                                }
                            },
                            error: function () {
                                alert('连接超时');
                            }
                        });
                    }

                }
            </script>
            <div class="GoodsInfoWrap">
                <div id="goods-viewer">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="center">
                                <div class='goodspic'>
                                    <%=imgStr %>
                                </div>
                            </td>
                            <td width="60%" valign="top">
                                <%=productStr %>
                                <div class='hightline'>
                                    <div class='hightbox'>
                                        <div class='buyinfo'>
                                            <table width='auto'>
                                                <tr>
                                                    <td>
                                                        <span class="num_buy">购买数量：</span>
                                                    </td>
                                                    <td>
                                                        <div class="Numinput">
                                                            <input type="text" id="quantity" value="1" orig="1" changed="12" onkeyup="change_quantity(this);" />
                                                            <span class="numadjust increase" onclick="add_quantity(694);"></span><span class="numadjust decrease"
                                                                onclick="decrease_quantity(694);"></span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <span>&nbsp;&nbsp;(库存<span class='store'><%=kucun%></span>)</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="btnBar clearfix">
                                            <div class="floatLeft">
                                                <h1>
                                                    <span class="dialog_title" style="color: Red"></span>
                                                </h1>
                                                <%-- <asp:Button runat="server" ID="btnBuy" CssClass="actbtn btn-fastbuy" Text="立即购买" OnClick="btnBuy_Click" />
                                                <asp:Button runat="server" ID="btnAddCart" CssClass="actbtn btn-buy" Text="加入购物车" OnClick="btnAddCart_Click" />--%>
                                                <input class="actbtn btn-fastbuy" value="立即购买" type="button" onclick="javascript:buys();" />
                                                <input class="actbtn btn-buy" value="加入购物车" type="button" onclick="javascript:add_cart();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both">
                    </div>
                    <div class="p_leftbar">
                        <div class="goods-detail-tab clearfix">
                        </div>
                        <div class="clear">
                        </div>
                        <div class="section pdtdetail" tab="商品详情">
                            <div class="goodsprop_ultra clearfix">
                            </div>
                            <div class="body indent uarea-output" id="goods-intro">
                                <%=productMemo %>
                            </div>
                        </div>
                        <%--              <div class="section pdtdetail" tab="商品评论 (<em>0</em>)">
                            <div class="commentTabLeft floatLeft">
                                <strong>商品评论</strong><span><a href="comment-144-discuss-commentlist.html">（已有<em>0</em>条评论）</a></span></div>
                            <div class="commentTabRight floatLeft">
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div class="FormWrap" style="background: #f6f6f6; margin-top: 0px;">
                                <div class="boxBrown division">
                                    <style type="text/css">
                                        .ShowShopRb
                                        {
                                            width: 100%;
                                        }
                                        .ShowShopRb ul
                                        {
                                        }
                                        .ShowShopRb ul li
                                        {
                                            width: 100%;
                                            float: left;
                                            margin: 0 0 14px 0;
                                            padding-bottom: 8px;
                                            border-bottom: 1px dashed #e9e9e9;
                                        }
                                        .ShowShopRb ul li img
                                        {
                                            width: 73px;
                                            height: 73px;
                                            border-radius: 5px;
                                            float: left;
                                            margin-right: 10px;
                                        }
                                        .ShowShopRb ul li h2
                                        {
                                            font-size: 14px;
                                            color: #3e3a39;
                                        }
                                        .ShowShopRb ul li h2 span
                                        {
                                            margin-left: 10px;
                                            color: #CCC;
                                        }
                                        .ShowShopRb ul li h3
                                        {
                                            font-size: 12px;
                                            color: #a5a5a5;
                                            margin-top: 4px;
                                        }
                                        .ShowShopRb ul h4
                                        {
                                            margin-top: 16px;
                                        }
                                        .ShowShopRb ul h4 textarea
                                        {
                                            width: 320px;
                                            height: 100px;
                                            padding: 4px;
                                        }
                                        .ShowShopRb ul h4 input
                                        {
                                            width: 80px;
                                            height: 30px;
                                            background: #939393;
                                            float: right;
                                            margin-top: 6px;
                                            color: #FFF;
                                            border: none;
                                            cursor: pointer;
                                        }
                                        .ShowShopRb ul h4 input:hover
                                        {
                                            background: #5a5a5a;
                                        }
                                        .cls
                                        {
                                            clear: both;
                                        }
                                    </style>
                                    <div class="ShowShopRb">
                                        <ul>
                                            <li>
                                                <h2>
                                                    代用名 <span>2014-12-17 10:28:20</span></h2>
                                                <h3>
                                                    裙子很漂亮，做工也不错，这个价能买到这么好的东西，太值了。下次还会光顾。</h3>
                                            </li>
                                            <li>
                                                <h2>
                                                    代用名 <span>2014-12-17 10:28:20</span></h2>
                                                <h3>
                                                    裙子很漂亮，做工也不错，这个价能买到这么好的东西，太值了。下次还会光顾。</h3>
                                            </li>
                                            <li><a href="ShowChurch.html">
                                                <img src="../img/touxiang3.jpg" /></a>
                                                <h2>
                                                    代用名 <span>2014-12-17 10:28:20</span></h2>
                                                <h3>
                                                    裙子很漂亮，做工也不错，这个价能买到这么好的东西，太值了。下次还会光顾。</h3>
                                            </li>
                                            <li><a href="ShowChurch.html">
                                                <img src="../img/touxiang.jpg" /></a>
                                                <h2>
                                                    代用名 <span>2014-12-17 10:28:20</span></h2>
                                                <h3>
                                                    裙子很漂亮，做工也不错，这个价能买到这么好的东西，太值了。下次还会光顾。</h3>
                                            </li>
                                            <li><a href="ShowChurch.html">
                                                <img src="../img/touxiang2.jpg" /></a>
                                                <h2>
                                                    代用名 <span>2014-12-17 10:28:20</span></h2>
                                                <h3>
                                                    裙子很漂亮，做工也不错，这个价能买到这么好的东西，太值了。下次还会光顾。</h3>
                                            </li>
                                            <li><a href="ShowChurch.html">
                                                <img src="../img/touxiang3.jpg" /></a>
                                                <h2>
                                                    代用名 <span>2014-12-17 10:28:20</span></h2>
                                                <h3>
                                                    裙子很漂亮，做工也不错，这个价能买到这么好的东西，太值了。下次还会光顾。</h3>
                                            </li>
                                            <div class="cls">
                                            </div>
                                        </ul>
                                    </div>
                                </div>
                                <h4>
                                    发表评论</h4>
                                <div class='title' style="color: #666;">
                                    标题：
                                    <input autocomplete="off" class="x-input inputstyle blur" type="text" class="inputstyle blur"
                                        required="true" size="50" name="title" value="[评论]浴·莲安·精华滋养霜" vtype="text" />
                                </div>
                                <div class="division">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="forform">
                                        <tr>
                                            <th>
                                                <em>*</em>评论内容：
                                            </th>
                                            <td>
                                                <textarea class="x-input inputstyle" vtype="required" rows="5" name="comment" style="width: 80%;"></textarea>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                联系方式：
                                            </th>
                                            <td>
                                                <input autocomplete="off" class="x-input inputstyle" type="text" class="inputstyle"
                                                    size="20" name="contact" vtype="text" />
                                                <span class="infotips">(可以是电话、email、qq等).</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <input type="submit" value="提交评论">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class="p_rightbar">
                        <div class="recommend">
                            <div class="border3" id="">
                                <h3>
                                    相关推荐</h3>
                                <div class="l_contentbox">
                                    <ul class="goodslist">
                                        <%=tuijian %>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="p_ranking">
                            <div class="border3" id="">
                                <h3>
                                    热销排行</h3>
                                <div class="l_contentbox">
                                    <ul class="goodslist">
                                        <%=hotSold %>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
            <script>
                $$('.addcomment .title input').addEvents({
                    'focus': function () { this.removeClass('blur'); },
                    'blur': function () { this.addClass('blur'); }
                });
            </script>
            <script>

                var buycoutText = $E('#goods-viewer .buyinfo input[type=text]').addEvent('keydown', function (e) {
                    //                    if ($A(keyCodeFix).include(e.code).length > 25) {
                    //                        e.stop();
                    //                    }
                });
                var getStore = function () {

                    return $E('#goods-viewer .buyinfo .store').get('text').toInt()

                };

                buycoutText.addEvent('keyup', function (e) {
                    var orig = Number(this.value);
                    if (isNaN(orig)) {
                        this.value = 1;
                    } else {
                        if (getStore() < this.value) this.value = getStore();
                        if (!this.value || this.value.toInt() < 1) this.value = 1;
                    }

                });
                /*购买数量调节*/
                $$('#goods-viewer .buyinfo .numadjust').addEvent('click', function (e) {
                    var countText = $E('#goods-viewer .buyinfo input[name^=goods[num]');
                    if (this.hasClass('increase')) {
                        countText.set('value', (countText.value.toInt() + 1).limit(1, getStore()));
                    } else {
                        countText.set('value', (countText.value.toInt() - 1).limit(1, getStore()));
                    }
                    this.blur();
                });

                $$('#goods-viewer .buyinfo .numadjust').addEvents({
                    'mousedown': function () {
                        this.addClass('active');
                    },
                    'mouseup': function () {
                        this.removeClass('active');
                    }
                });










                /*hightline*/
                $$('#goods-viewer .hightline').addEvents({
                    mouseenter: function () {

                        this.addClass('hightline-enter');

                    },
                    mouseleave: function () {

                        this.removeClass('hightline-enter');

                    }

                });


            </script>
        </div>
    </div>
    </form>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
