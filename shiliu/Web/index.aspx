<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Web_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>马赛特酒庄</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="">
    <meta name="description" content="马赛特酒庄">
    <meta name="generator" content="ShopEx 4.8.5">
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <link rel="stylesheet" href="themes/1394732638/images/css.css" type="text/css" />
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
    <script type="text/javascript" src="statics/script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="statics/script/jquery.eislideshow.js"></script>
    <script type="text/javascript" src="statics/script/jquery.easing.1.3.js"></script>
    <script src="../Wap/Js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript">
<!--
        jQuery.noConflict();
        jQuery(function () {
            jQuery('#ei-slider').eislideshow({
                easing: 'easeOutExpo',
                titleeasing: 'easeOutExpo',
                autoplay: true,
                titlespeed: 1200
            });
        });
-->
    </script>
</head>
<body class="body_home">
    <hd:header runat="server" />
    <div id="ei-slider" class="ei-slider">
        <ul class="ei-slider-large">
            <!-- 图片大小：1280*550 px -->
            <li><a href="#" title='马赛特酒庄'>
                <img class="img" src="themes/1394732638/images/focus.jpg" alt="" /></a></li>
            <li><a href="#" title='马赛特酒庄'>
                <img class="img" src="themes/1394732638/images/focus.jpg" alt="" /></a></li>
            <li><a href="#" title='马赛特酒庄'>
                <img class="img" src="themes/1394732638/images/focus.jpg" alt="" /></a></li>
        </ul>
        <div class="thumbs">
            <ul class="ei-slider-thumbs">
                <li class="ei-slider-element"></li>
                <li><a href="#"></a></li>
                <li><a href="#"></a></li>
                <li><a href="#"></a></li>
            </ul>
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
                        alert("添加成功");
                        //window.location.reload();
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
    <script type="text/javascript">

        jQuery.noConflict();
        jQuery(function () {
            //            jQuery.ajax({
            //                type: "post",
            //                async: false,
            //                cache: false,
            //                url: "Handler.ashx",
            //                dataType: "json",
            //                data: { "Method": "GetBanner" },
            //                success: function (e) {
            //                    if (e != null && e != "") {
            //                        jQuery('#ei-slider').html(e);
            //                        jQuery('#ei-slider').eislideshow({
            //                            easing: 'easeOutExpo',
            //                            titleeasing: 'easeOutExpo',
            //                            autoplay: true,
            //                            titlespeed: 1200
            //                        });
            //                    }
            //                },
            //                error: function () {
            //                    alert('连接超时');
            //                }

            //            });

        });

    </script>
    <!-- ei-slider -->
    <div class="main main_home">
        <div class="innerbox">
            <div class="ads_bottom">
                <div class="ads_list">
                    <div class="AdvBanner">
                        <a href="about.aspx">
                            <img src='themes/1394732638/images/ads1.jpg' width="321" height="193" />
                        </a>
                    </div>
                </div>
                <div class="ads_list">
                    <div class="AdvBanner">
                        <a href="about.aspx">
                            <img src='themes/1394732638/images/ads2.jpg' width="321" height="193" />
                        </a>
                    </div>
                </div>
                <div class="ads_list last">
                    <div class="AdvBanner">
                        <a href="about.aspx">
                            <img src='themes/1394732638/images/ads3.jpg' width="321" height="193" />
                        </a>
                    </div>
                </div>
            </div>
            <div class="banner1">
                <div class="AdvBanner">
                    <a href="#" target="_blank">
                        <img src='themes/1394732638/images/banner1.jpg' style="" />
                    </a>
                </div>
            </div>
            <%=ProductTab%>
        </div>
    </div>
    <ft:footer runat="server" />
</body>
</html>
