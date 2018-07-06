<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Wap_index" %>

<%@ OutputCache Duration="100" VaryByParam="none" %>
<!---添加上这一句代码意思是，添加此页面缓存十秒，这十秒之内，刷新页面将读缓存起来页面的值，不再执行Page_Load方法。
     Page_Load方法是每十秒执行一次-->
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
    <title>共学教育</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/index.css" />
    <script type="text/javascript">
        $(function () {
            $('.tab ul.menu li').click(function () {
                //获得当前被点击的元素索引值
                var Index = $(this).index();
                //给菜单添加选择样式
                $(this).addClass('active').siblings().removeClass('active');
                //显示对应的div
                $('.tab').children('div').eq(Index).show().siblings('div').hide();

            });
            //window.localStorage.setItem("LoginUserId", "");
        });
    </script>
    <script>
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        $(function () {
            var agent = $.getUrlParam("agent");
            if (agent == null || agent == "" || agent == "null") {
                agent = window.localStorage.getItem("agent");
            }
            if (agent == null || agent == "" || agent == "null") {

                $('#hidagent').val("0");
                agent = 0;
            }
            $('#hidagent').val(agent);
            window.localStorage.setItem("agent", agent);
        });

    </script>
</head>
<body aria-atomic="False" style="background: #e1e1e1;">
    <input type="hidden" runat="server" id="hidagent" />

    <%--    <div class="indheader">
          <div class="indheadL">
            <img src="Img/ind_01.png" />
        </div>
              <div class="indheadZ">
            <img src="Img/ind_02.png" /><input type="search" name="" placeholder="搜索更多石榴视频" />
        </div>
                <div class="indheadR">
            <a href="ViewRecord.html">
                <img src="Img/ind_03.png" /></a>
        </div>
    </div>--%>
    <!--幻灯片 开始-->
    <script type="text/javascript" src="js/jquery.flexslider-min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.flexslider').flexslider({
                controlNav: true,
                directionNav: false,
                animation: "slide"
                //manualControls: ".myflexslider .slidenav"
            });
        });
    </script>
    <div id="banner" class="flexslider">
        <ul class="slides">
            <%=banner %>
            <%--    <li>
                <img src="Img/banner.jpg" /></li>
            <li>
                <img src="Img/banner.jpg" /></li>
            <li>
                <img src="Img/banner.jpg" /></li>--%>
        </ul>
    </div>
    <!--幻灯片 结束-->
    <div class="inContent">

        <div class="indChen">
            <dl>
                <dt>
                    <img src="img/ind_04.png" />体验课程</dt>
                <%=freevideo %>
                <%--                <dd><a href="CourseShow.html">
                    <img src="Img/ind_05.jpg" />
                    <p>大汉情缘之云中歌</p>
                </a></dd>
                <dd><a href="CourseShow.html">
                    <img src="Img/ind_05.jpg" />
                    <p>大汉情缘之云中歌</p>
                </a></dd>--%>
            </dl>
        </div>
        <div class="indChen"><%--indPro--%>
            <dl>
                <dt>
                    <img src="img/ind_06.png" />精品课程</dt>
                <%--      <dd><a href="Product.aspx">
                    <img src="Img/ind_05.jpg" />
                    <h1>¥ 200.00 </h1>
                    <p>大汉情缘之云中歌</p>
                </a></dd>--%>
                <%=pro3 %>
            </dl>
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
