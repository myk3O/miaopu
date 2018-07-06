<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Wap_Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>共学视频</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
</head>
<body aria-atomic="False" style="background: #eeeeee;">

    <script>


        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        $(function () {
            var type = $.getUrlParam('type');
            if (type == null || type == "") {
                LoadVideo("");
            } else {
                LoadVideo(type);
            }
            $("li > a[title='po']").each(function () {
                var id = $(this).attr("id");
                if (id == type) {
                    $(this).addClass("cur");
                    $(this).parent().siblings().each(function () {
                        $(this).children().removeClass("cur");
                    });
                }

            });

            //点击了以后
            $("li > a[title='po']").on("click", function () {
                orderState = $(this).attr("id");
                $(this).addClass("cur");

                $(this).parent().siblings().each(function () {
                    //alert($(this).children().attr("id"));
                    $(this).children().removeClass("cur");
                });

                LoadVideo(orderState);
            });


            function LoadVideo(type) {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetVideoByType",
                        "type": type
                    },
                    success: function (data) {
                        if (data != null && data != "") {
                            //alert(data);
                            $("#vlist").empty();
                            $("#vlist").append(data);
                        } else {
                            $("#vlist").empty();
                            $("#vlist").append("<h1 style='text-align: center;margin-top: 50px'>未找到相关视频</h1>");
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }

        });

    </script>
    <div class="subProT">
        <ul id="menu">

            <li><a title="po" id="" class="cur">视频列表</a></li>
            <%--       <li><a title="po" id="3"></a></li>
            <li><a title="po" id="4">美睫</a></li>
            <li><a title="po" id="5">半永久</a></li>--%>
            <%-- <li><a href="javascript:void(0);">分类</a>
                <ul>
                    <li><a href="#">综合</a></li>
                    <li><a href="#">最热</a></li>
                    <li><a href="#">最新</a></li>
                </ul>
            </li>--%>
        </ul>
    </div>
    <div class="inContent">
        <dl class="subProduct" id="vlist">
            <%--<dd><a href="ProductShow.aspx">
                <img src="Img/sub_01.jpg" />
                <h1>韩式美甲视频教程</h1>
                <p><span>27.56万</span> 次点播</p>
            </a></dd>
            <dd><a href="ProductShow.aspx">
                <img src="Img/sub_01.jpg" />
                <h1>韩式美甲视频教程</h1>
                <p><span>27.56万</span> 次点播</p>
            </a></dd>
            <dd><a href="ProductShow.aspx">
                <img src="Img/sub_01.jpg" />
                <h1>韩式美甲视频教程</h1>
                <p><span>27.56万</span> 次点播</p>
            </a></dd>--%>
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
