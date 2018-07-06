<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="Wap_MyProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>个人信息 </title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script>
        var loginUser;
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        $(function () {

            loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存
            if (loginUser != null || loginUser != "") {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetMemberByMyProfile",
                        "userID": loginUser
                    },
                    success: function (data) {
                        if (data != null && data != "") {
                            //alert(data);
                            $("#divC").empty();
                            $("#divC").append(data);
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });

            }

        });
        function jump() {
            window.location.href = "MyProfileEdit.aspx?uid=" + loginUser;
        }


        function back() {
            window.location.href = "Login.aspx";
        }
    </script>

</head>
<body aria-atomic="False">
    <div class="inContent" id="divC">
<%--        <div class="StylistT">
            <img src="Img/Styl_01.png" />
            <h1>昵称：共学用户</h1>
            <h2>关注时间：2015-08-21</h2>
        </div>
        <dl class="MyProfile">
            <dd><span>姓 名：</span></dd>
            <dd><span>性 别：</span></dd>
            <dd><span>年 龄：</span></dd>
            <dd><span>地 区：</span></dd>
            <dd><span>手 机：</span></dd>
            <dd><span>邮 箱：</span></dd>
            <dt>
                <input type="button" name="" onclick="jump()" value="修改信息" /></dt>
             <dt>
                <input type="button" name="" onclick="back()" value="返回" /></dt>
        </dl>--%>
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