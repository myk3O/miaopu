<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopHead.ascx.cs" Inherits="Web_TopHead" %>
<script src="../Wap/Js/jquery.js"></script>
<script charset="utf-8" type="text/javascript" src="../Wap/Js/jquery_002.js"></script>
<div class="topbar">
    <div class="innerbox">
        <div class="loginbar">
            <span id="foobar_399" style="position: relative;">您好,<span id="uname_399"></span> <span
                id="memberBar_399"><a href="member.aspx">
                    <%=UserName %></a>
                <%--                <asp:Button runat="server" ID="btnPay" Text="退出" />--%>
            </span></span>
            <script>
                if ((null != Cookie.get('S[NAME]')) || (null != Cookie.get('S[UNAME]'))) {
                    $('uname_399').setText('：' + (Cookie.get('S[NAME]') ? Cookie.get('S[NAME]') : Cookie.get('S[UNAME]')));
                }
            </script>
        </div>
        <div class="top_right">
            <a href="member-orders.aspx" class="myorder">我的订单</a> <a href="javascript:void(0);"
                onclick="AddFavorite('马赛特酒庄（masetchina.com.cn）',location.href)" class="addfav">收藏本站</a>
        </div>
        <script type="text/javascript">
            function AddFavorite(title, url) {
                try {
                    window.external.addFavorite(url, title);
                }
                catch (e) {
                    try {
                        window.sidebar.addPanel(title, url, "");
                    }
                    catch (e) {
                        alert("抱歉，您所使用的浏览器无法完成此操作。\n\n加入收藏失败，请使用Ctrl+D进行添加");
                    }
                }
            }
        </script>
    </div>
</div>
<div class="header innerbox">
    <div class="logo">
        <div class="border2" id="">
            <div class="border-top">
                <h3>
                </h3>
            </div>
            <div class="border-body">
                <div class="bg">
                    <a href="index.aspx">
                        <img src="statics/logo.png" border="0" /></a></div>
            </div>
            <div class="border-foot">
            </div>
        </div>
    </div>
    <div class="searchbar">
        <script>

            (function ($) {
                $.getUrlParam = function (name) {
                    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                    var r = window.location.search.substr(1).match(reg);
                    if (r != null) return decodeURI(r[2]); return null;
                }
            })(jQuery);

            $(function () {

                var msg = $.getUrlParam('msg');
                if (msg != null && msg != "") {
                    $('#searchVal').val(msg);
                }
            });
            function searchp() {
                var msg = $('#searchVal').val();
                if (msg != '请输入关键词') {
                    window.location.href = "search-index.aspx?msg=" + msg;
                }
            }
        </script>
        <input name="name[]" id="searchVal" size="10" class="search_input" onblur="this.value=(this.value=='') ? '请输入关键词':this.value;"
            onfocus="this.value=(this.value=='请输入关键词') ? '':this.value;" value="请输入关键词" x-webkit-speech />
        <input type="button" value="搜索" class="search_btn" onfocus='this.blur();' onclick="searchp()" />
        <%--  <a href="search-index.aspx" class="btn_advsearch">高级搜索</a>
        <div class="hotwords">
            热门搜索： <a href="">红酒</a>
        </div>--%>
    </div>
    <div class="cart">
        <style type="text/css">
            .Cart_info
            {
                position: relative;
                display: block;
            }
            .Cart_info .fmenu
            {
                width: 358px;
                right: 0;
                top: 0;
                left: auto;
                border: 1px solid #9D0E0E;
                background: #fff;
            }
            .Cart_info .fmenu .boxGray
            {
                background: #fff;
                border: none;
            }
        </style>
        <div id="cartpop" class="Cart_info shop-cart-mini">
            <dl>
                <dt><s></s><a href="cart-index.aspx" class="cartbox cartfull pay"><span class="CartIco">
                    <i class="icon-common icon-common-carthover"></i>购物车 </span><span class="shopping"><b
                        id="Cart_439" class="cart-number">
                        <%=cartCount%></b> </span></a></dt>
            </dl>
        </div>
    </div>
    <div class="clearfix">
    </div>
</div>
<div class="menubar">
    <div class="innerbox">
        <div class="category">
            <h4>
                所有商品分类</h4>
            <div class="category_box">
                <div class="mod_cate_hd">
                    <div class="mod_cate_hd_con">
                        全部商品分类</div>
                    <i style="display: none;" class="mod_cate_hd_arrow"></i>
                </div>
                <div id="cat_ex_vertical_433" class="DuceDropNavs allsort_out cats mod_cate_bd">
                    <ul class="cat-ex-vertical allsort">
                        <%=productClass%>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <div class="menu">
            <ul id="sample-menu-1" class="sf-menu MenuList">
                <li><a title="url" href="index.aspx">首页</a></li>
                <li><a title="url" href="about.aspx">关于马赛特</a>
                    <ul>
                        <%=aboutUrl %>
                        <%--                        <li><a href="about.aspx">酒庄介绍</a></li>
                        <li><a href="about.aspx">马赛特酒庄</a></li>--%>
                    </ul>
                </li>
                <li><a title="url" href="news.aspx">活动资讯</a></li>
                <li><a title="url" href="member.aspx">会员中心</a></li>
            </ul>
        </div>
        <script type="text/javascript">
            window.addEvent('domready', function () {
                $$('.MenuList li a').each(function (item, index) {
                    if (item.href == location.href || location.href + '?' == item.href) {
                        item.getParent().addClass('curr');
                    }
                });
            });
            //            window.onload = function () {
            //                $("a[title='url']").on("click", function () {
            //                    $(this).siblings().removeClass('curr');
            //                    $(this).addClass("curr");
            //                });
            //            };
            //            $(function () {
            //                $("a[title='url']").on("click", function () {
            //                    $(this).siblings().removeClass('curr');
            //                    $(this).addClass("curr");
            //                });
            //            });
        </script>
    </div>
</div>
<style type="text/css">
    .category_box
    {
        display: none;
    }
</style>
<script type="text/javascript">
    $$('.category').addEvents({
        'mouseenter': function () {
            this.getElement('.category_box').setStyle('display', 'block');
        },
        'mouseleave': function () {
            this.getElement('.category_box').setStyle('display', 'none');
        }
    });

</script>
