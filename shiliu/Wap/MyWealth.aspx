<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyWealth.aspx.cs" Inherits="Wap_MyWealth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>我的学分</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
</head>
<body aria-atomic="False">
    <div class="inContent">
        <dl class="MyWealth">
            <dd><a href="Sales.aspx?uid=<%=nid%>">累计分享学分<i class="fa fa-angle-right"></i><span>￥<%=allMypri %></span></a></dd>
            <dd><a href="Global.aspx?uid=<%=nid%>">累计奖学金<i class="fa fa-angle-right"></i><span>￥<%=allMoney %></span></a></dd>
            <%--<dd><a href="Royalty.aspx?uid=<%=nid%>">累计津贴奖<i class="fa fa-angle-right"></i><span>￥<%=jingtie %></span></a></dd>--%>
            <%--   <dd><a>
                累计慈善旅游基金<i class="fa fa-angle-right"></i><span>￥4780.00</span></a></dd>--%>
            <dd><a href="Shopping.aspx?uid=<%=nid%>">累计可用学习币总额<i class="fa fa-angle-right"></i><span>￥<%=xuexibi %></span></a></dd>
            <dd><a href="AttentionList.aspx?uid=<%=nid%>">邀请关注奖励<i class="fa fa-angle-right"></i><span>￥<%=guanzhu %></span></a></dd>
            <dd><a href="MyPresent.aspx?uid=<%=nid%>">当前可兑换学分总额<i class="fa fa-angle-right"></i><span>￥<%=CanGetPri %></span></a></dd>
            <dd><a href="MyGold.aspx?uid=<%=nid%>">学分转赠<i class="fa fa-angle-right"></i><span>前往转赠</span></a></dd>
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
        // 通过下面这个API隐藏右上角按钮hideOptionMenu
        WeixinJSBridge.call('hideOptionMenu');
    });

</script>
