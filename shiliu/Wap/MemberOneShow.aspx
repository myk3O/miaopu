<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberOneShow.aspx.cs" Inherits="Wap_MemberOneShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>我的分销 - 微店管理</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script type="text/javascript">
        $(document).ready(function () {

            $("#firstpane .menu_body:eq(0)").show();
            $("#firstpane h3.menu_head").click(function () {
                $(this).addClass("current").next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().removeClass("current");
            });

            $("#secondpane .menu_body:eq(0)").show();
            $("#secondpane h3.menu_head").mouseover(function () {
                $(this).addClass("current").next("div.menu_body").slideDown(500).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().removeClass("current");
            });

        });
    </script>
</head>
<body aria-atomic="False">
    <div class="inContent">
        <div class="StylistT">
            <img src="Img/Styl_01.png" />
            <h1>昵称：XXXXXXXXX</h1>
            <h2>关注时间：2015-08-21</h2>
        </div>
        <div class="StylistZ"><span>累计销售：8316.00元</span><span>累计佣金：5092.96元</span></div>
        <div id="firstpane" class="menu_list">
            <h3 class="menu_head current">他的一级会员<span>400人</span></h3>
            <div style="display: block" class="menu_body">
                <a href="javascript:;">代理人数<span>30人<img src="Img/pro_03.png"/></span></a>
                <a href="javascript:;">消费金额<span>130人<img src="Img/pro_03.png"/></span></a>
                <a href="javascript:;">贡献佣金<span>230人<img src="Img/pro_03.png"/></span></a>
            </div>
            <h3 class="menu_head">他的二级会员<span>200人</span></h3>
            <div style="display: none" class="menu_body">
                <a href="javascript:;">代理人数<span>30人<i class="fa fa-angle-right"></i></span></a>
                <a href="javascript:;">消费金额<span>130人<i class="fa fa-angle-right"></i></span></a>
                <a href="javascript:;">贡献佣金<span>230人<i class="fa fa-angle-right"></i></span></a>
            </div>
            <h3 class="menu_head">他的三级会员<span>1026人</span></h3>
            <div style="display: none" class="menu_body">
                <a href="javascript:;">代理人数<span>30人<i class="fa fa-angle-right"></i></span></a>
                <a href="javascript:;">消费金额<span>130人<i class="fa fa-angle-right"></i></span></a>
                <a href="javascript:;">贡献佣金<span>230人<i class="fa fa-angle-right"></i></span></a>
            </div>
        </div>
    </div>
    <div class="FootbqK"></div>
       <div class="Footer">
        <script>
            $(function () {
                var lg = window.localStorage.getItem("LoginUserId");
                if (lg != null && lg != "") {
                    $.ajax({
                        type: "post",
                        async: false,
                        cache: false,
                        url: "Handler.ashx",
                        dataType: "json",
                        data: {
                            "Method": "Memberisfxs",
                            "userID": lg
                        },
                        success: function (data) {
                            if (data) {
                                var text1 = "<a href='BuyVideo.aspx?uid=" + lg + "'>";
                                text1 += " <img src='Img/Micro_06.png' />";
                                text1 += "<p>已购视频</p></a>";
                                $("#footer1").empty();
                                $("#footer1").html(text1);


                                var text2 = "<a href='MyLogin.aspx?uid=" + lg + "'>";
                                text2 += " <img src='Img/foot_02.jpg' />";
                                text2 += "<p>学习中心</p></a>";
                                $("#footer2").empty();
                                $("#footer2").html(text2);

                            }
                        },
                        error: function () {
                            alert('连接超时');
                        }

                    });

                }
            });

        </script>
        <ul>
            <li><a href="index.aspx">
                <img src="Img/foot_01.png" />
                <p>首页</p>
            </a></li>
            <li><a href="MonthRanking.aspx">
                <img src="Img/foot_02.png" />
                <p>榜上有名</p>
            </a></li>
            <li id="footer1"><a href="Product.aspx">
                <img src="Img/foot_03.png" />
                <p>成为学员</p>
            </a></li>
            <li id="footer2"><a href="Login.aspx" class="cur">
                <img src="Img/foot_04.png" />
                <p>学习中心</p>
            </a></li>
        </ul>
    </div>
</body>
</html>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        // 通过下面这个API隐藏右上角按钮hideOptionMenu
        WeixinJSBridge.call('hideOptionMenu');
    });

</script>