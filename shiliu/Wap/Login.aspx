<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Wap_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="description" content="共学教育、共学视频、共学、共学微信、微信共学" />
    <meta name="keywords" content="共学教育、共学视频、共学、共学微信、微信共学" />
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>学习中心-共学教育</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script>
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        $(function () {
            var agent = window.localStorage.getItem("agent");
            if (agent == null || agent == "" || agent == "null") {
                agent = 0;
            }
            var loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存

            if (loginUser == null || loginUser == "" || loginUser == "null") {
                loginUser = $.getUrlParam("mid"); //再看url里面有没有
                if (loginUser != null && loginUser != "" && loginUser != "null") {
                    //url里有 uid,
                    var backurl = window.localStorage.getItem("VideoUrl");
                    if (backurl != null && backurl != "" && backurl != "null") {
                        window.location.href = backurl; //授权结束后返回分享过来的视频地址
                    }
                }
            }
            if (loginUser == null || loginUser == "" || loginUser == "null") {//回传还是没有

                window.location.href = "UserAuth.ashx?agent=" + agent; //重新授权获取

            } else {


                window.localStorage.setItem("LoginUserId", loginUser); //存缓存
                $('#hiduser').val(loginUser);
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetMemberBynID",
                        "userID": loginUser
                    },
                    success: function (data) {
                        if (data != null && data != "") {
                            //alert(data);
                            $("#memberList").empty();
                            $("#memberList").append(data);
                        } else {
                            window.localStorage.setItem("LoginUserId", "");
                            window.location.href = "UserAuth.ashx"; //重新授权获取
                            //window.location.href = window.location.reload(); //重新授权获取
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });



                //判断新消息是否已读取
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "IsNewsRead",
                        "userID": loginUser
                    },
                    success: function (e) {
                        if (e) {
                            var text = "<a href='Teacher.aspx?uid=" + loginUser + "' ><img src='Img/Micro_00.png' />最新公告<i class='fa fa-angle-right'></i></a>";
                            $("#ddnews").append(text);
                        } else {
                            var text = "<a href='Teacher.aspx?uid=" + loginUser + "' ><img src='Img/Micro_00.png' />最新公告<i class='fa fa-angle-right'></i><img src='Img/new_tubiao.gif' /></a>";
                            $("#ddnews").append(text);
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }

        });


        function LoginOut() {
            window.localStorage.setItem("LoginUserId", "");
            window.location.reload();
        }

        function Jump() {
            window.location.href = "Product.aspx";
        }
    </script>
</head>
<body aria-atomic="False">

    <div class="inContent">

        <div class="StylistT" id="memberList">
            <img />
            <h1></h1>
            <h2></h2>
            <h3></h3>
        </div>
        <div class="StylistZ">
            <abbr>
                您还未购买过任何视频
                <input type="button" value="我去看看" onclick="Jump()" /></abbr>
        </div>

        <dl class="StylistX">
            <dd id="ddnews"><%--<a href="Teacher.aspx">
                <img src="Img/Micro_00.png" />最新公告<i class="fa fa-angle-right"></i><img src="Img/new_tubiao.gif" /></a>--%>
            </dd>
            <dd><a href="Product.aspx">
                <img src="Img/Micro_02.png" />立即购买，即刻成为共学学员<i class="fa fa-angle-right"></i></a></dd>
            <dd><a href="MyProfile.aspx">
                <img src="Img/Micro_09.png" />我的资料<i class="fa fa-angle-right"></i></a></dd>
            <dd><a href="MyBankcard.aspx">
                <img src="Img/Micro_08.png" />我的银行卡<i class="fa fa-angle-right"></i></a></dd>
            <dd></dd>
            <dd><a href="javascript:void(0)" onclick="LoginOut()">
                <img src="Img/Micro_04.png" />同步微信资料<i class="fa fa-angle-right"></i></a></dd>
            <dd><a href="Ranking.aspx">
                <img src="Img/Micro_05.png" />共学排行榜<i class="fa fa-angle-right"></i></a></dd>
        </dl>

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
                                //window.location.href = "MyLogin.aspx";

                                window.location.href = "MyLogin.aspx?uid=" + lg;

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
        // 通过下面这个API隐藏右上角按钮
        WeixinJSBridge.call('hideOptionMenu');
    });

</script>
